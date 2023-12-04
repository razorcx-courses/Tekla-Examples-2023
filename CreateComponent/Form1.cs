using System;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Point = Tekla.Structures.Geometry3d.Point;

namespace CreateComponent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Initialize instances of required classes
            IBeamCreator beamCreator = new BeamCreator(new ErrorLogger());
            IComponentCreator componentCreator = new ComponentCreator(new ErrorLogger());
            IAttributeReader attributeReader = new AttributeReader(new ErrorLogger());
            IErrorLogger errorLogger = new ErrorLogger();

            // Create Example instance
            var example = new Example(beamCreator, componentCreator, attributeReader, errorLogger);

            // Run the example application
            example.RunExample();
        }
    }

    // Add appropriate using statements for logging libraries

    /// <summary>
    /// Holds constant values used in the example application.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The profile string for beams.
        /// </summary>
        public const string ProfileString = "380*380";

        /// <summary>
        /// The material string for beams.
        /// </summary>
        public const string MaterialString = "K40-1";

        /// <summary>
        /// The name of the attribute used in the example.
        /// </summary>
        public const string AttributeName = "side_bar_space";
    }

    /// <summary>
    /// Interface for classes responsible for creating beams in the Tekla Structures model.
    /// </summary>
    public interface IBeamCreator
    {
        /// <summary>
        /// Creates a beam in the Tekla Structures model.
        /// </summary>
        /// <param name="startPoint">The start point of the beam.</param>
        /// <param name="endPoint">The end point of the beam.</param>
        /// <param name="profileString">The profile string of the beam.</param>
        /// <param name="materialString">The material string of the beam.</param>
        /// <returns>The created beam or null if creation fails.</returns>
        Beam CreateBeam(Point startPoint, Point endPoint, string profileString, string materialString);
    }

    /// <summary>
    /// Interface for classes responsible for creating components in the Tekla Structures model.
    /// </summary>
    public interface IComponentCreator
    {
        /// <summary>
        /// Creates a component in the Tekla Structures model.
        /// </summary>
        /// <param name="beam">The beam to associate with the component.</param>
        /// <param name="componentName">The name of the component.</param>
        /// <param name="componentNumber">The number of the component.</param>
        /// <returns>The created component or null if creation fails.</returns>
        Component CreateComponent(Beam beam, string componentName, int componentNumber);
    }

    /// <summary>
    /// Interface for classes responsible for reading attributes in the Tekla Structures model.
    /// </summary>
    public interface IAttributeReader
    {
        /// <summary>
        /// Gets the value of a component attribute in the Tekla Structures model.
        /// </summary>
        /// <param name="component">The component to retrieve the attribute from.</param>
        /// <param name="attributeName">The name of the attribute to retrieve.</param>
        /// <returns>The value of the attribute or 0.0 if retrieval fails.</returns>
        double GetComponentAttribute(Component component, string attributeName);
    }

    /// <summary>
    /// Interface for classes responsible for logging errors.
    /// </summary>
    public interface IErrorLogger
    {
        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        void LogError(string message);
    }

    /// <summary>
    /// Class responsible for creating beams in the Tekla Structures model.
    /// </summary>
    public class BeamCreator : IBeamCreator
    {
        private readonly IErrorLogger _errorLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BeamCreator"/> class.
        /// </summary>
        /// <param name="errorLogger">The error logger instance.</param>
        public BeamCreator(IErrorLogger errorLogger)
        {
            _errorLogger = errorLogger ?? throw new ArgumentNullException(nameof(errorLogger));
        }

        /// <inheritdoc/>
        public Beam CreateBeam(Point startPoint, Point endPoint, string profileString, string materialString)
        {
            // Create a new beam with specified start and end points
            var beam = new Beam(startPoint, endPoint)
            {
                Profile =
                {
                    // Set the profile and material properties of the beam
                    ProfileString = profileString
                },
                Material =
                {
                    MaterialString = materialString
                }
            };

            try
            {
                // Attempt to insert the beam into the model
                if (beam.Insert())
                    return beam;

                // Log an error if beam insertion fails
                _errorLogger.LogError("Beam insertion failed");
                return null;
            }
            catch (Exception ex)
            {
                // Log an error if an exception occurs during beam insertion
                _errorLogger.LogError($"Error during beam insertion: {ex.Message}");
                return null;
            }
        }
    }

    /// <summary>
    /// Class responsible for creating components in the Tekla Structures model.
    /// </summary>
    public class ComponentCreator : IComponentCreator
    {
        private readonly IErrorLogger _errorLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentCreator"/> class.
        /// </summary>
        /// <param name="errorLogger">The error logger instance.</param>
        public ComponentCreator(IErrorLogger errorLogger)
        {
            _errorLogger = errorLogger ?? throw new ArgumentNullException(nameof(errorLogger));
        }

        /// <inheritdoc/>
        public Component CreateComponent(Beam beam, string componentName, int componentNumber)
        {
            // Create a new component
            var component = new Component
            {
                // Set the name and number properties of the component
                Name = componentName,
                Number = componentNumber
            };

            // Create a component input and associate it with the beam
            var componentInput = new ComponentInput();
            componentInput.AddInputObject(beam);

            try
            {
                // Attempt to insert the component into the model
                if (component.Insert())
                    return component;

                // Log an error if component insertion fails
                _errorLogger.LogError("Component insertion failed");
                return null;
            }
            catch (Exception ex)
            {
                // Log an error if an exception occurs during component insertion
                _errorLogger.LogError($"Error during component insertion: {ex.Message}");
                return null;
            }
        }
    }

    /// <summary>
    /// Class responsible for reading attributes in the Tekla Structures model.
    /// </summary>
    public class AttributeReader : IAttributeReader
    {
        private readonly IErrorLogger _errorLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeReader"/> class.
        /// </summary>
        /// <param name="errorLogger">The error logger instance.</param>
        public AttributeReader(IErrorLogger errorLogger)
        {
            _errorLogger = errorLogger ?? throw new ArgumentNullException(nameof(errorLogger));
        }

        /// <inheritdoc/>
        public double GetComponentAttribute(Component component, string attributeName)
        {
            // Initialize the attribute value
            var attributeValue = 0.0;

            try
            {
                // Attempt to get the specified attribute value from the component
                if (component.GetAttribute(attributeName, ref attributeValue))
                    return attributeValue;

                // Log an error if getting the attribute fails
                _errorLogger.LogError($"Getting attribute '{attributeName}' failed");
                return 0.0;
            }
            catch (Exception ex)
            {
                // Log an error if an exception occurs during attribute retrieval
                _errorLogger.LogError($"Error getting attribute '{attributeName}': {ex.Message}");
                return 0.0;
            }
        }
    }

    /// <summary>
    /// Class for logging errors.
    /// </summary>
    public class ErrorLogger : IErrorLogger
    {
        /// <inheritdoc/>
        public void LogError(string message)
        {
            // Replace this with your logging mechanism (e.g., log4net, Serilog)
            Console.WriteLine($"ERROR: {message}");
        }
    }

    /// <summary>
    /// Main class for the example application.
    /// </summary>
    public class Example
    {
        private readonly IBeamCreator _beamCreator;
        private readonly IComponentCreator _componentCreator;
        private readonly IAttributeReader _attributeReader;
        private readonly IErrorLogger _errorLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Example"/> class.
        /// </summary>
        /// <param name="beamCreator">The beam creator instance.</param>
        /// <param name="componentCreator">The component creator instance.</param>
        /// <param name="attributeReader">The attribute reader instance.</param>
        /// <param name="errorLogger">The error logger instance.</param>
        public Example(IBeamCreator beamCreator, IComponentCreator componentCreator, IAttributeReader attributeReader, IErrorLogger errorLogger)
        {
            _beamCreator = beamCreator ?? throw new ArgumentNullException(nameof(beamCreator));
            _componentCreator = componentCreator ?? throw new ArgumentNullException(nameof(componentCreator));
            _attributeReader = attributeReader ?? throw new ArgumentNullException(nameof(attributeReader));
            _errorLogger = errorLogger ?? throw new ArgumentNullException(nameof(errorLogger));
        }

        /// <summary>
        /// Runs the example application.
        /// </summary>
        public void RunExample()
        {
            try
            {
                // Define points for the beam
                var startPoint = new Point(12000, 0, 0);
                var endPoint = new Point(12000, 0, 6000);

                // Create a beam
                var beam = CreateBeam(startPoint, endPoint);

                if (beam == null)
                {
                    _errorLogger.LogError("Beam creation failed");
                    return;
                }

                // Create a component using the beam
                var component = _componentCreator.CreateComponent(beam, "Component Test", 30000063);

                if (component == null)
                {
                    _errorLogger.LogError("Component creation failed");
                    return;
                }

                Console.WriteLine($"Component ID: {component.Identifier.ID}");

                // Retrieve and verify a component attribute
                var attributeValue = _attributeReader.GetComponentAttribute(component, Constants.AttributeName);

                if (attributeValue == 333.0)
                    Console.WriteLine("Attribute verification successful");
                else
                    _errorLogger.LogError("Attribute verification failed");
            }
            catch (Exception ex)
            {
                // Log an error if an unexpected exception occurs
                _errorLogger.LogError($"An unexpected error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a beam in the Tekla Structures model.
        /// </summary>
        /// <param name="startPoint">The start point of the beam.</param>
        /// <param name="endPoint">The end point of the beam.</param>
        /// <returns>The created beam or null if creation fails.</returns>
        private Beam CreateBeam(Point startPoint, Point endPoint)
        {
            return _beamCreator.CreateBeam(startPoint, endPoint, Constants.ProfileString, Constants.MaterialString);
        }
    }

}
