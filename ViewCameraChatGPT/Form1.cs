using System;
using System.Windows.Forms;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Geometry3d;
using View = Tekla.Structures.Model.UI.View;
using Point = Tekla.Structures.Geometry3d.Point;

namespace ViewCameraChatGPT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var viewCameraExample = new ViewCameraExample(new ViewProvider());
            viewCameraExample.ViewCameraExample1();
        }
    }

    /// <summary>
    /// Represents a provider for obtaining views.
    /// </summary>
    public interface IViewProvider
    {
        /// <summary>
        /// Gets a collection of visible views.
        /// </summary>
        ModelViewEnumerator GetVisibleViews();
    }

    /// <summary>
    /// Implementation of the <see cref="IViewProvider"/> interface using Tekla Structures API.
    /// </summary>
    public class ViewProvider : IViewProvider
    {
        /// <inheritdoc/>
        public ModelViewEnumerator GetVisibleViews()
        {
            return ViewHandler.GetVisibleViews();
        }
    }

    /// <summary>
    /// Represents an example class for manipulating view cameras.
    /// </summary>
    public class ViewCameraExample
    {
        private const double RotationAngle = Math.PI / 20.0;

        private readonly IViewProvider _viewProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewCameraExample"/> class.
        /// </summary>
        /// <param name="viewProvider">The view provider.</param>
        public ViewCameraExample(IViewProvider viewProvider)
        {
            _viewProvider = viewProvider ?? throw new ArgumentNullException(nameof(viewProvider));
        }

        /// <summary>
        /// Executes the example logic for manipulating view cameras.
        /// </summary>
        public void ViewCameraExample1()
        {
            try
            {
                var currentView = GetCurrentView();
                var camera = new ViewCamera();

                SetCameraView(camera, currentView);

                if (IsPerspectiveView(currentView))
                {
                    ModifyCameraSettings(camera);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the current view from the view provider.
        /// </summary>
        /// <returns>The current view.</returns>
        private View GetCurrentView()
        {
            Console.WriteLine($"Executing method: {nameof(GetCurrentView)}");
            return _viewProvider.GetVisibleViews().Current;
        }

        /// <summary>
        /// Sets the camera view to the specified view.
        /// </summary>
        /// <param name="camera">The view camera.</param>
        /// <param name="view">The target view.</param>
        private void SetCameraView(ViewCamera camera, View view)
        {
            Console.WriteLine($"Executing method: {nameof(SetCameraView)}");
            camera.View = view;
        }

        /// <summary>
        /// Checks whether the specified view is a perspective view.
        /// </summary>
        /// <param name="view">The view to check.</param>
        /// <returns><c>true</c> if the view is a perspective view; otherwise, <c>false</c>.</returns>
        private bool IsPerspectiveView(View view)
        {
            Console.WriteLine($"Executing method: {nameof(IsPerspectiveView)}");
            return view.IsVisible() && view.IsPerspectiveViewProjection();
        }

        /// <summary>
        /// Modifies the settings of the camera.
        /// </summary>
        /// <param name="camera">The view camera.</param>
        private void ModifyCameraSettings(ViewCamera camera)
        {
            Console.WriteLine($"Executing method: {nameof(ModifyCameraSettings)}");
            camera.Select();

            var directionVector = TiltCamera(camera);
            var upVector = OrthogonalizeUpVector(camera, directionVector);

            camera.DirectionVector = directionVector;
            camera.UpVector = upVector;

            camera.Modify();
        }

        /// <summary>
        /// Tilts the camera based on its current settings.
        /// </summary>
        /// <param name="camera">The view camera.</param>
        /// <returns>The tilted direction vector of the camera.</returns>
        private Vector TiltCamera(ViewCamera camera)
        {
            Console.WriteLine($"Executing method: {nameof(TiltCamera)}");
            var directionVector = camera.DirectionVector;
            var upVector = camera.UpVector;
            var crossVector = directionVector.Cross(upVector);

            var rotationMatrix = new Matrix();
            rotationMatrix = MatrixFactory.Rotate(RotationAngle, crossVector);
            var rotatedPoint = rotationMatrix * (Point)directionVector;

            return new Vector(rotatedPoint);
        }

        /// <summary>
        /// Orthogonalizes the up vector of the camera relative to its direction vector.
        /// </summary>
        /// <param name="camera">The view camera.</param>
        /// <param name="directionVector">The direction vector of the camera.</param>
        /// <returns>The orthogonalized up vector of the camera.</returns>
        private Vector OrthogonalizeUpVector(ViewCamera camera, Vector directionVector)
        {
            Console.WriteLine($"Executing method: {nameof(OrthogonalizeUpVector)}");
            var crossVector = directionVector.Cross(camera.UpVector);
            return crossVector.Cross(directionVector);
        }
    }

}


