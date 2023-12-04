
# Beam Test Documentation

# Table of Contents

- [Beam Test Documentation](#beam-test-documentation)
  - [Overview](#overview)
  - [`BeamTest` Method](#beamtest-method)
  - [Additional Methods](#additional-methods)
    - [`CreateAndInsertBeam`](#createandinsertbeam)
    - [`DisplayBeamProperties`](#displaybeamproperties)
    - [`TranslateBeamPoints`](#translatebeampoints)
  - [Tekla Structures Classes](#tekla-structures-classes)
    - [`Beam` Class](#beam-class)
    - [`Point` Class](#point-class)
    - [`Profile` Class](#profile-class)
    - [`Material` Class](#material-class)
    - [`Finish` Property](#finish-property)

## Overview

The `BeamTest` method, along with additional methods, interacts with various Tekla Structures API classes to create, modify, and display properties of a structural element in the Tekla model.

## `BeamTest` Method

```csharp
/// <summary>
/// Method to test the creation and modification of a Beam object.
/// </summary>
private void BeamTest()
{
    // Initialize variables
    double test = 0;

    // Output a message indicating the start of BeamTest
    WriteLine("Starting BeamTest...");

    // Create two points to define the start and end of the Beam
    var point = new Point(0, 0, 0);
    var point2 = new Point(1000, 0, 0);

    // Create a Beam object using the defined points
    var beam = new Beam(point, point2);

    // Set properties of the Beam object
    beam.Profile.ProfileString = "L60*6";
    beam.Material.MaterialString = "Steel_Undefined";
    beam.Finish = "PAINT";

    // Attempt to insert the Beam object into the model
    if (!beam.Insert())
    {
        // Output a failure message if the insertion fails
        WriteLine("BeamTest failed - unable to create beam");
        MessageBox.Show("Insert failed!");
        return;
    }
    else
    {
        // Add the successfully inserted Beam object to an object list
        _objectList.Add(beam);
    }

    // Retrieve and display specific properties of the Beam object
    DisplayBeamProperties(beam);

    // Output the identifier of the inserted Beam object
    WriteLine(beam.Identifier.ID + " Inserted");

    // Attempt to select the Beam object
    if (!beam.Select())
        MessageBox.Show("Select failed!");

    // Translate the start and end points of the Beam object
    beam.StartPoint.Translate(0, 1000, 0);
    beam.EndPoint.Translate(0, 1000, 0);

    // Attempt to modify the Beam object
    if (!beam.Modify())
        MessageBox.Show("Modify failed!");

    // Output a completion message for BeamTest
    WriteLine("BeamTest complete!");
}
```

## Additional Methods

### `CreateAndInsertBeam`

```csharp
/// <summary>
/// Creates and inserts a Beam object with predefined points and properties.
/// </summary>
/// <returns>The created Beam object or null if unsuccessful.</returns>
private Beam CreateAndInsertBeam()
{
    // Output a message indicating the start of CreateAndInsertBeam
    WriteLine("Creating and inserting Beam...");

    // Create two points to define the start and end of the Beam
    var startPoint = new Point(0, 0, 0);
    var endPoint = new Point(1000, 0, 0);

    // Create a Beam instance with specified start and end points
    var beam = new Beam(startPoint, endPoint)
    {
        // Set the profile, material, and finish properties of the Beam
        Profile = { ProfileString = "L60*6" },
        Material = { MaterialString = "Steel_Undefined" },
        Finish = "PAINT"
    };

    // Attempt to insert the Beam into the Tekla Structures model
    if (!beam.Insert())
    {
        WriteLine("Beam insertion failed!");
        return null;
    }

    // Add the Beam to the object list for tracking
    _objectList.Add(beam);

    // Output a completion message for CreateAndInsertBeam
    WriteLine("Beam created and inserted successfully!");

    return beam;
}
```

### `DisplayBeamProperties`

```csharp
/// <summary>
/// Displays specific properties of the Beam object.
/// </summary>
/// <param name="beam">The Beam object.</param>
private void DisplayBeamProperties(Beam beam)
{
    double test = 0;

    // Retrieve and display the value of PROFILE.FLANGE_THICKNESS_1 property
    if (beam.GetReportProperty("PROFILE.FLANGE_THICKNESS_1", ref test))
    {
        WriteLine($"PROFILE.FLANGE_THICKNESS_1 returned {test}");
    }

    // Retrieve and display the value of PROFILE.FLANGE_THICKNESS_2 property
    if (beam.GetReportProperty("PROFILE.FLANGE_THICKNESS_2", ref test))
    {
        WriteLine($"PROFILE.FLANGE_THICKNESS_2 returned {test}");
    }
}
```

### `TranslateBeamPoints`

```csharp
/// <summary>
/// Translates the start and end points of the Beam object.
/// </summary>
/// <param name="beam">The Beam object.</param>
/// <param name="translationX">The translation along the X-axis.</param>
/// <param name="translationY">The translation along the Y-axis.</param>
/// <param name="translationZ">The translation along the Z-axis.</param>
private void TranslateBeamPoints(Beam beam, double translationX, double translationY, double translationZ)
{
    // Translate the start and end points of the Beam along the specified axes
    beam.StartPoint.Translate(translationX, translationY, translationZ);
    beam.EndPoint.Translate(translationX, translationY, translationZ);
}
```

## Tekla Structures Classes

### `Beam` Class

The `Beam` class represents a structural beam element in the Tekla Structures model. It provides properties such as `StartPoint` and `EndPoint` to define the geometry of the beam.

- **Methods Used:**
  - `Insert()`: Attempts to insert the beam into the Tekla model.
  - `Select()`: Attempts to select the beam for further manipulation.
  - `Modify()`: Attempts to modify the beam after selection.

- **Properties Used:**
  - `Profile`: Represents the cross-sectional profile of the beam.
  - `Material`: Represents the material of the beam.
  - `Finish`: Specifies the surface treatment or finish of the beam.

### `Point` Class

The `Point` class represents a point in 3D space with X, Y, and Z coordinates.

### `Profile` Class

The `Profile` class represents the cross-sectional profile of a structural element, such as a beam. It includes properties like `ProfileString` to define the profile.

### `Material` Class

The `Material` class represents the material properties of a structural element, such as a beam. It includes properties like `MaterialString` to define the material type.

### `Finish` Property

The `Finish` property specifies the surface treatment or finish of a structural element, such as a beam.


