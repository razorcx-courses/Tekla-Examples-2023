# Tekla Open API Example

## Beam Splice Connection

A plug-in with Windows Form dialog that splices two beams.

![SpliceConnection](https://github.com/razorcx-courses/Tekla-Examples-2023/assets/100399176/9cccad64-830e-499b-b146-354092d9a6e1)

This repo is a heavily refactored version of Tekla Splice Connection found here:

- https://github.com/TrimbleSolutionsCorporation/TSOpenAPIExamples/tree/2023/Model/Plugins/SpliceConnection
- https://developer.tekla.com/tekla-structures/documentation/code-example-create-splice-connections-between-beams

## Table of Contents

1. [Beam Splice Connection Plugin for Tekla Structures](#beam-splice-connection-plugin-for-tekla-structures)
2. [SpliceConnectionBuilder Class](#spliceconnectionbuilder-class)
3. [PlateBuilder Class](#platebuilder-class)
4. [PlaneBuilder Class](#planebuilder-class)
5. [FittingBuilder Class](#fittingbuilder-class)
6. [BoltBuilder Class](#boltbuilder-class)
7. [Extension Methods Class](#extension-methods-class)
8. [TeklaHelper Class](#teklahelper-class)
9. [StructuresField Attribute](#structuresfield-attribute)
9. [Splice Connection Plugin Form](#splice-connection-plugin-form)


# Beam Splice Connection Plugin for Tekla Structures

This C# code defines a Tekla Structures connection plugin for splicing beams. Let's break down the key components and functionality of the code:

## Overall Purpose:

The code provides a Tekla Structures connection plugin for splicing beams. It includes logic for selecting beams, retrieving dialog values, and creating beam splice connections. The debug mode allows testing the plugin as a standalone application for development purposes.

## Namespaces:

- `System`, `System.Windows.Forms`: Standard C# namespaces for general functionalities and Windows Forms.
- `Newtonsoft.Json`: A popular library for JSON serialization and deserialization.
- `Tekla.Structures`: The main namespace for interacting with Tekla Structures.
- `Tekla.Structures.Plugins`: Provides the base classes and attributes for creating Tekla Structures plugins.
- `Tekla.Structures.Model`: Contains classes for working with the Tekla Structures BIM model.
- `TSMUI`: Alias for `Tekla.Structures.Model.UI`.

## Plugin Declaration:

- The `BeamSpliceConnection` class is declared as a Tekla Structures connection plugin.
- The `Plugin` attribute specifies the name of the connection in the catalog.
- `PluginUserInterface` attribute associates the plugin with a user interface (dialog) class.
- `SecondaryType`, `AutoDirectionType`, and `PositionType` attributes define plugin behavior.

## Class Fields:

- `_structuresData`: Holds data related to the structures.
- `_model`: An instance of the Tekla Structures model.
- `Debug`: A flag indicating whether the plugin is in debug mode.
- `_spliceConnectionBuilder`: An instance of the `SpliceConnectionBuilder` class responsible for creating beam splices.

## Constructor:

- The constructor takes a parameter of type `StructuresData` and initializes the `_structuresData` field.

## Overridden Methods:

### `Run` Method:

- The main method that gets executed when the plugin runs.
- Checks if the plugin is in debug mode or if necessary objects are selected.
- Retrieves values from the dialog using the `GetValuesFromDialog` method.
- Selects primary and secondary beams based on the mode (debug or connection plugin).
- Calls the `_spliceConnectionBuilder` to create the splice connection.
- Handles exceptions and commits changes to the Tekla Structures model.

### `SelectBeamsForConnectionPlugin` and `SelectBeamsForDebugging` Methods:

- Utility methods for selecting primary and secondary beams based on the plugin's mode.

### `GetValuesFromDialog` Method:

- Retrieves values from the dialog and sets default values if needed.
- Utilizes JSON serialization/deserialization with the help of `Newtonsoft.Json`.

## Debug Method:

- The `Main` method serves as a debug entry point for a Windows Forms app.
- Creates an instance of the plugin in debug mode and executes the `Run` method for testing.


# SpliceConnectionBuilder Class

The `SpliceConnectionBuilder` class is a C# component within the `SpliceConn` namespace responsible for creating splice connections between two beams in the Tekla Structures model. It coordinates the creation of gaps, plates, and bolts to form a structural splice connection.

## Key Components and Methods:

### Initialization:

- **Model Instance:** An instance of the Tekla Structures `Model` class is initialized to interact with the Tekla Structures model.
  
```csharp
private readonly Model _model = new Model();
```

### Constants:

- **GAP Constant:** Defines a constant `GAP` with a value of 10.0, representing the desired gap between beams during the splice connection.

```csharp
private const double GAP = 10.0;
```

### Builder Instances:

- Instances of the `FittingBuilder`, `PlateBuilder`, and `BoltBuilder` classes are created to assist in creating fittings, plates, and bolts, respectively.

```csharp
private readonly FittingBuilder _fittingBuilder = new FittingBuilder();
private readonly PlateBuilder _plateBuilder = new PlateBuilder();
private readonly BoltBuilder _boltBuilder = new BoltBuilder();
```

### `Create` Method:

- The `Create` method is the entry point for creating a splice connection. It takes input data (`Data`), primary and secondary beams, and performs the necessary checks and operations.

```csharp
public bool Create(Data data, Beam primaryBeam, Beam secondaryBeam)
```

#### Initial Checks:

- Checks if either the primary or secondary beam is null, and if true, returns `false`.

```csharp
if (primaryBeam == null || secondaryBeam == null) return false;
```

- Checks if the profiles of the primary and secondary beams are equal. If not, returns `false`.

```csharp
if (!TeklaHelper.AreProfilesEqual(primaryBeam, secondaryBeam)) return false;
```

- Checks if the beams are aligned. If not, returns `false`.

```csharp
if (!TeklaHelper.AreBeamsAligned(primaryBeam, secondaryBeam)) return false;
```

#### Splice Connection Creation:

- Calls the `CreateSpliceConnection` method to execute the splice connection creation process.

```csharp
return CreateSpliceConnection(primaryBeam, secondaryBeam, data.PlateLength, data.BoltStandard);
```

### `CreateSpliceConnection` Method:

- This method performs the actual creation of the splice connection, including creating gaps, plates, and bolts.

```csharp
private bool CreateSpliceConnection(Beam primaryBeam, Beam secondaryBeam,
    double plateLength, string boltStandard)
```

#### Beam Profile Properties:

- Retrieves various properties of the secondary beam profile needed for the creation of plates.

```csharp
var webThickness = secondaryBeam.GetReportProperty<double>("PROFILE.WEB_THICKNESS");
var beamHeight = secondaryBeam.GetReportProperty<double>("PROFILE.HEIGHT");
var flangeHeight = secondaryBeam.GetReportProperty<double>("PROFILE.FLANGE_THICKNESS");
```

#### Validity Check:

- Ensures that the retrieved beam profile properties are valid (greater than zero).

```csharp
var validProperties = webThickness > 0.0 && beamHeight > 0.0 && flangeHeight > 0.0;
if (!validProperties) return false;
```

#### Gap and Plate Creation:

- Calls the `CreateGapBetweenBeams` method of the `FittingBuilder` to create a gap between the beams.

```csharp
var canCreateGaps = _fittingBuilder.CreateGapBetweenBeams(primaryBeam, secondaryBeam, GAP);
if (!canCreateGaps) return false;
```

- Calculates parameters for plate creation and calls the `CreatePlates` method of the `PlateBuilder` to create plates on both sides of the beams.

```csharp
var edgeDistance = (flangeHeight + innerRoundingRadius + innerMargin);
var plateHeight = beamHeight - 2 * edgeDistance;
_plateBuilder.CreatePlates(plateLength, webThickness, beamHeight, edgeDistance, 
    out var plate1, out var plate2);
```

#### Coordinate System Handling:

- Changes the coordinate system for bolt creation to that of the first plate.

```csharp
_model.SetTransformationPlane(plate1.GetCoordinateSystem());
```

#### Bolt Array Creation:

- Calls the `CreateBoltArray` method of the `BoltBuilder` to create bolt arrays connecting the plates and beams.

```csharp
return _boltBuilder.CreateBoltArray(primaryBeam, plate1, plate2, plateHeight, 
    true, plateLength, boltStandard) &&
    _boltBuilder.CreateBoltArray(secondaryBeam, plate1, plate2, plateHeight, 
    false, plateLength, boltStandard);
```

#### Exception Handling and Restoration:

- Uses a `try-finally` block to ensure that the original transformation plane is restored even if an exception occurs during the process.

```csharp
catch (Exception e)
{
    return false;
}
finally
{
    _model.SetTransformationPlane(originalTransformationPlane);
}
```

## Overall Purpose:

The `SpliceConnectionBuilder` class orchestrates the creation of a splice connection between two beams in Tekla Structures. It ensures that the beams have equal profiles, are aligned, and then proceeds to create gaps, plates, and bolts to form a structurally sound connection.



# PlateBuilder Class

This C# code defines a class called `PlateBuilder` within the `SpliceConn` namespace, and it contains methods to create two plates in the Tekla Structures model. The plates are represented as `Beam` objects, a common structural element in Tekla Structures.

## Key Components and Functions:

### Namespaces:

- `Tekla.Structures.Geometry3d` and `Tekla.Structures.Model`
  - These namespaces provide classes and functionalities for working with geometry and the Tekla Structures model, respectively.

### PlateBuilder Class:

#### `CreatePlates` Method:

- This method creates two plates (Beams) with specified dimensions and properties.
  - **Parameters:**
    - `length`: The length of the plates.
    - `thickness`: The thickness of the plates.
    - `height`: The height of the plates.
    - `edgeDistance`: The distance from the plate edges to the centerline.
    - `plate1` and `plate2` are out parameters, representing the two created plates.

#### `CreatePlate` Method:

- This private method is responsible for creating a single plate (Beam).
  - **Parameters:**
    - `startPoint` and `endPoint`: Points representing the start and end of the plate.
    - `profile`: A string specifying the profile of the plate.
    - `finish`: A string specifying the finish of the plate.
    - `label`: A string representing the label of the plate.

### Plate Creation Process (Inside Methods):

- **Profile Formation:**
  - A profile string is formed based on the input thickness and length.
- **Plate 1 Creation:**
  - `plate1` is created using the `CreatePlate` method with a specified start point, end point, profile, finish, and label.
- **Plate 2 Creation:**
  - `plate2` is created similarly to `plate1`, but with a different start and end point, positioned on the opposite side of the first plate.

### Beam Properties and Insertion:

- The created plates (`plate1` and `plate2`) are instances of the `Beam` class.
- Various properties of the plates are set, such as position, start and end points, profile, and finish.
- The label is set using the `SetLabel` method.
- The plates are inserted into the Tekla Structures model using the `Insert` method.

## Overall Purpose:

The purpose of this code is to provide a `PlateBuilder` class with methods for creating two plates in Tekla Structures. The plates are represented as `Beam` objects with specified dimensions, positions, profiles, finishes, and labels. The code leverages the Tekla Structures API to interact with the model and create these structural elements.



# PlaneBuilder Class

This C# code defines a class called `PlaneBuilder` within the `SpliceConn` namespace, and it contains methods for constructing a `Plane` object in the Tekla Structures model. This class is part of the Tekla Structures Open API, which provides functionalities for interacting with and manipulating the Tekla Structures BIM (Building Information Modeling) software.

## PlaneBuilder Class:

### PlaneLength Constant:

- This constant defines the length of the constructed plane. It is set to 500 units.

### GetPlaneForFitting Method:

- **Parameters:**
  - `coordinateSystem`: Represents the coordinate system.
  - `point`: Represents a point in space.
  - `distanceVector`: Represents a vector describing a distance in space.
- **Return Type:**
  - Returns a `Plane` object.
- **Functionality:**
  - Calculates the origin point for the plane using the `GetOriginPointForFitting` method.
  - Retrieves the X-axis for the plane using the `GetAxisXForFitting` method.
  - Constructs and returns a `Plane` object using the calculated origin point and X-axis.

### GetOriginPointForFitting Method:

- **Parameters:**
  - `point`: Represents a point in space.
  - `distanceVector`: Represents a vector describing a distance in space.
- **Return Type:**
  - Returns a new `Point` object representing the origin point for the plane.
- **Functionality:**
  - Creates a new point based on the input point.
  - Translates the new point by the components of the `distanceVector`.
  - Returns the translated point.

### GetAxisXForFitting Method:

- **Parameters:**
  - `coordinateSystem`: Represents the coordinate system.
- **Return Type:**
  - Returns a new `Vector` representing the X-axis for the plane.
- **Functionality:**
  - Calculates the X-axis for the plane using cross products and normalization.

### GetPlane Method:

- **Parameters:**
  - `point`: Represents a point in space.
  - `axisX`: Represents the X-axis for the plane.
  - `coordinateSystem`: Represents the coordinate system.
- **Return Type:**
  - Returns a new `Plane` object.
- **Functionality:**
  - Creates a new `Plane` object with the specified origin and X-axis.
  - Normalizes the X-axis to the specified `PlaneLength`.
  - Calculates and sets the Y-axis for the plane based on the cross product of the coordinate system's X and Y axes.
  - Normalizes the Y-axis to the specified `PlaneLength`.
  - Returns the constructed `Plane` object.

## Overall Purpose:

The `PlaneBuilder` class provides methods to construct a `Plane` object in Tekla Structures. This is useful for defining planes in 3D space, often used in structural modeling and analysis within Tekla Structures. The methods consider coordinate systems, points, and vectors to determine the orientation and position of the plane.


# BoltBuilder Class

The `BoltBuilder` class in the `SpliceConn` namespace is responsible for creating bolt arrays to connect beams and plates in the Tekla Structures model. It utilizes Tekla Structures API classes and methods to set bolt properties, positions, and dimensions.

## Methods:

### `CreateBoltArray` Method:

This method creates a bolt array connecting a beam and two plates. It takes various parameters such as the beam, two plates, plate height, primary flag, plate length, and bolt standard.

```csharp
public bool CreateBoltArray(Beam beam, Beam plate1, Beam plate2, double plateHeight,
    bool primary, double plateLength, string boltStandard)
```

#### Steps:

1. **Default Bolt Array:**
   - Retrieves a default bolt array configuration using the `GetDefaultBoltArray` method.

```csharp
var boltArray = GetDefaultBoltArray();
```

2. **Set Parts:**
   - Sets the beam and the first plate as the parts to be bolted and bolted to, respectively.
   - Adds the second plate as another part to be bolted.

```csharp
boltArray.PartToBoltTo = plate1;
boltArray.PartToBeBolted = beam;
boltArray.AddOtherPartToBolt(plate2);
```

3. **Calculate Bolt Position:**
   - Calculates the bolt positions based on plate height and length.
   - Determines the primary and secondary positions.

```csharp
var point1 = new Point(plateHeight / 2.0, plateLength / 2.0, 0.0);
var point2 = new Point(point1);
point2.Y *= -1;

var firstPosition = primary ? point1 : point2;
var secondPosition = primary ? point2 : point1;
```

4. **Set Positions and Standard:**
   - Sets the first and second bolt positions.
   - Assigns the specified bolt standard.

```csharp
boltArray.FirstPosition = firstPosition;
boltArray.SecondPosition = secondPosition;
boltArray.BoltStandard = boltStandard;
```

5. **Set Start and End Point Offsets:**
   - Sets the start and end point offsets based on plate length.

```csharp
var dx = plateLength - 75;
boltArray.StartPointOffset.Dx = dx;
boltArray.EndPointOffset.Dx = dx;
```

6. **Add Bolt Columns & Rows:**
   - Adds bolt distances along the X and Y directions.

```csharp
var distY = plateHeight - 150.0;
boltArray.AddBoltDistX(0.0);
boltArray.AddBoltDistY(distY);
```

7. **Insert Bolt Array:**
   - Inserts the configured bolt array into the Tekla Structures model.

```csharp
return boltArray.Insert();
```

### `GetDefaultBoltArray` Method:

This method returns a default configuration for a bolt array.

```csharp
private static BoltArray GetDefaultBoltArray()
```

#### Default Configuration:

- Sets default values for various properties such as bolt size, tolerance, standard, cut length, length, thread in material, position, etc.
- Specifies the presence of washers and nuts.
- Returns the configured `BoltArray` instance.

## Overall Purpose:

The `BoltBuilder` class facilitates the creation of bolt arrays between beams and plates in Tekla Structures. It encapsulates the configuration details and provides a method to insert bolt arrays into the model.


# Extension Methods Class

This C# code defines a static class named `ExtensionMethods` containing several extension methods. Extension methods allow developers to add new methods to existing types without modifying them directly. Let's break down each method and its purpose:

## `GetReportProperty<T>` Method:

This extension method is applied to objects of the `ModelObject` class. It retrieves a specific property value from the report properties of the given `ModelObject`. It supports properties of types `string`, `int`, and `double`. The method has the following parameters:

- `this ModelObject modelObject`: The target `ModelObject` to retrieve the property from.
- `string property`: The name of the property to retrieve.
- Returns: The value of the specified property cast to the generic type `T`.

## `FindAllChildrenByType<T>` Method:

This extension method is applied to objects of the `Control` class, which is part of the Windows Forms library. It finds and returns a list of all child controls of a given type `T`. The method has the following parameters:

- `this Control control`: The parent control to search for child controls.
- Returns: A list of controls of type `T` found within the hierarchy of child controls.

## `SetTransformationPlane` Methods:

These extension methods are applied to objects of the `Model` class. They set the current transformation plane of the Tekla Structures model. Tekla Structures uses transformation planes to define the orientation and position of drawing and modeling operations. The methods have the following parameters:

- `this Model model`: The target Tekla Structures model.
- `CoordinateSystem coordinateSystem`: The coordinate system to set as the transformation plane.
- `TransformationPlane transformationPlane`: The specific transformation plane to set.

## `GetTransformationPlane` Method:

This extension method is applied to objects of the `Model` class. It retrieves the current transformation plane of the Tekla Structures model. The method has the following parameter:

- `this Model model`: The target Tekla Structures model.
- Returns: The current `TransformationPlane` of the model.

## Overall Purpose:

- **Generic Property Retrieval:** The `GetReportProperty<T>` method facilitates retrieving specific properties from the report properties of `ModelObject` instances, supporting different data types.

- **Child Control Retrieval:** The `FindAllChildrenByType<T>` method is useful in Windows Forms applications, allowing developers to find all child controls of a specific type within a control hierarchy.

- **Transformation Plane Manipulation:** The `SetTransformationPlane` and `GetTransformationPlane` methods provide convenient ways to set and retrieve the transformation plane in the Tekla Structures model, aiding in modeling and drawing operations.


# TeklaHelper Class

This C# code defines a class named `TeklaHelper` that provides helper methods for working with Tekla Structures beams. The class is part of the `SpliceConn` namespace.

## Constants:

- `EPSILON`: A constant with a value of 0.001. It is likely used as a small threshold for numerical comparisons.

## Helper Methods:

### `AreProfilesEqual` Method:

- Compares the profiles of two beams to check if they are equal.
- Parameters:
  - `Beam primaryBeam`: The primary beam to compare.
  - `Beam secondaryBeam`: The secondary beam to compare.
- Returns: `true` if the profiles are equal; otherwise, `false`.

### `AreBeamsAligned` Method:

- Checks if two beams are aligned in a parallel manner.
- Parameters:
  - `Beam primaryBeam`: The primary beam for alignment check.
  - `Beam secondaryBeam`: The secondary beam for alignment check.
- Returns: `true` if the beams are aligned; otherwise, `false`.

### `GetCoordinateSystem` Method:

- Retrieves the coordinate system of the primary beam with adjustments based on the positions of the primary and secondary beams.
- Parameters:
  - `Beam primaryBeam`: The primary beam.
  - `Beam secondaryBeam`: The secondary beam.
- Returns: A `CoordinateSystem` representing the adjusted coordinate system.

#### Adjustment Logic:

- If the end point of the primary beam is close to either the start or end point of the secondary beam (within the specified epsilon value), the coordinate system is adjusted.
- The origin is translated along the X-axis of the coordinate system.
- The X-axis of the coordinate system is multiplied by -1.

## Usage of Tekla Structures API:

The code leverages the Tekla Structures API (`Tekla.Structures.Geometry3d` and `Tekla.Structures.Model`) to perform operations related to beam profiles, alignment checks, and coordinate system adjustments. It utilizes Tekla Structures geometry classes such as `Line`, `Parallel`, and `CoordinateSystem`.

The `TeklaHelper` class provides reusable functionality for common tasks involved in working with beams in the context of Tekla Structures modeling.


# FittingBuilder Class

This C# code defines a class named `FittingBuilder` within the `SpliceConn` namespace, providing methods for creating a gap between two beams in Tekla Structures. The code utilizes the Tekla Structures API, specifically the `Tekla.Structures.Geometry3d` and `Tekla.Structures.Model` namespaces.

## Fields:

- `PlaneBuilder _planeBuilder`: An instance of the `PlaneBuilder` class, used for creating planes that define the fittings.

## Methods:

### `CreateGapBetweenBeams` Method:

- Creates a gap between two beams by inserting fittings.
- Parameters:
  - `Beam primaryBeam`: The primary beam.
  - `Beam secondaryBeam`: The secondary beam.
  - `double gap`: The desired gap distance between the beams.
- Returns: `true` if the fittings are created successfully; otherwise, `false`.

#### Implementation Steps:

1. Check for null beams and a minimum gap distance.
2. Calculate gap vectors for both beams.
3. Reverse gap vectors based on the direction of the beams.
4. Determine start and end points for the gap, adjusting them depending on the relative direction of the beams.
5. Create fittings for both beams using the `CreateFitting` method.

### `CreateFitting` Method:

- Creates a fitting at the end point of a beam to define the gap.
- Parameters:
  - `Beam beam`: The beam for which the fitting is created.
  - `Point beamEndPoint`: The end point of the beam.
  - `Vector beamGapVector`: The vector defining the gap.
- Returns: `true` if the fitting is created successfully; otherwise, `false`.

#### Implementation Steps:

1. Get the coordinate system of the beam.
2. Use the `PlaneBuilder` to create a plane for the fitting.
3. Create a fitting at the specified end point with the calculated plane.

### Helper Methods:

- `ReverseGapVector`: Reverses gap vectors based on the direction of the beams.
- `AdjustStartAndEndPoints`: Adjusts start and end points based on the relative direction of the beams.

## Usage of Tekla Structures API:

The code utilizes Tekla Structures API classes such as `Beam`, `Vector`, `Point`, `Fitting`, and `PlaneBuilder`. It leverages geometry and modeling functionalities provided by the Tekla Structures API to create gaps between beams in a structural model.


# StructuresField Attribute

The code defines a C# class named `StructuresData` within the `SpliceConn` namespace, which serves as a data structure for storing information related to a Tekla Structures model. Each field in the class is associated with a Tekla Structures attribute using the `StructuresField` attribute. This association facilitates the mapping of data between the C# code and Tekla Structures, ensuring that specific attributes in the Tekla Structures environment correspond to the appropriate fields in the C# class.

## Usage of `StructuresField` Attribute:

### Attribute Definition:

When applied to a class field, the `StructuresField` attribute specifies the name of the attribute in Tekla Structures that the field corresponds to. It serves as a mapping between the C# data structure and the Tekla Structures environment.

```csharp
[StructuresField("PlateLength")]
public double PlateLength;
```

In this example, the `PlateLength` field in the `StructuresData` class is associated with the "PlateLength" attribute in Tekla Structures.

### Type Specification:

The `StructuresField` attribute also provides guidance on the type of data that should be stored in the corresponding Tekla Structures attribute. For example, it indicates whether the attribute expects a `double`, `int`, or `string`.

```csharp
[StructuresField("zang1")] // It is mandatory to use type double for this attribute
public double RotationAngleY;
```

Here, the "zang1" attribute is expected to store a double value.

### Integration with Tekla Structures Plugins:

This attribute is often used in conjunction with Tekla Structures plugins. When creating custom plugins or extensions, developers use `StructuresField` to define the input and output parameters that correspond to Tekla Structures attributes.

```csharp
[StructuresField("BoltStandard")]
public string BoltStandard;
```

In this case, the `BoltStandard` field is intended to represent the "BoltStandard" attribute in the context of a Tekla Structures plugin.

In summary, the `StructuresField` attribute simplifies the integration of C# code with Tekla Structures by establishing a clear link between class fields and Tekla Structures attributes, ensuring proper data mapping and type compatibility during the interaction between the C# code and the Tekla Structures environment.



# Splice Connection Plugin Form

The `SpliceConnection` class represents a custom plugin form designed for collecting user input to create a splice connection in Tekla Structures. This documentation provides an overview of the major components and functionality of the code.

## Initialization and Value Loading

### Class Inheritance
- `SpliceConnection` extends the Tekla Structures `PluginFormBase` class, which is a base class for creating custom dialog forms within the Tekla Structures environment.

### LoadValuesPath Method
- The `LoadValuesPath` method initializes default values for various input fields, such as `PlateLengthTextBox`, `BoltStandardTextBox`, `UpDirectionComboBox`, etc.
- It loads additional values from a file using `base.LoadValuesPath(fileName)`.
- The `Apply` method is then called to apply the default values.

## Event Handlers

### Apply, Cancel, Get, Modify, Ok, On/Off Events
- Event handlers for Apply, Cancel, Get, Modify, Ok, and On/Off buttons are implemented.
- `this.Apply()` is called in multiple event handlers to apply changes made in the form.
- `this.Close()` is called in Cancel and Ok event handlers to close the form.
- `this.Get()` and `this.Modify()` methods are called in response to corresponding events.

## Control Interaction

### Textbox and Combobox Events
- Event handlers like `anyTextBox_KeyPress` and `anyComboBox_SelectedIndexChanged` respond to changes in textboxes and comboboxes.
- The `SetThisControlEnableCheckBoxChecked` method is used to check a corresponding checkbox when a textbox or combobox changes.

### UI Control Relationship

- The code utilizes `structuresExtender` to retrieve attribute names associated with UI controls.
- It identifies and checks checkboxes related to the current control when a change occurs.

## UI Control Relationship

### Dialog Box Interaction

- The form provides standard Tekla Structures dialog functionality, including Apply, Cancel, Get, Modify, Ok, and On/Off buttons.

This custom form enhances user experience by facilitating the input of parameters for creating a splice connection in Tekla Structures. The provided event handlers ensure proper handling of user interactions and actions associated with the form's buttons. The UI controls are intelligently linked to corresponding checkboxes for improved user feedback. Overall, the form streamlines the process of defining parameters for a splice connection within the Tekla Structures environment.