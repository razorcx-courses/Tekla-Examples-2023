
# Contour Plate Test Documentation

The `ContourPlateTest` method is a part of an application or test suite designed to validate the ContourPlate creation and insertion functionality using the Tekla Structures API.

# Table of Contents

- [Contour Plate Test - Tekla Structures API Test](#contourplatetest---tekla-structures-api-test)
  - [Overview](#overview)
    - [1. Contour Plate Test](#contourplatetest)
    - [2. Create Contour Points For Contour Plate](#createcontourpointsforcontourplate)
    - [3. Create And Insert Contour Plate](#createandinsertcontourplate)
  - [How to Use](#how-to-use)

## Overview

The provided code demonstrates a structured approach to test the Tekla Structures API capabilities related to ContourPlates. The test is broken down into three key methods:

### ContourPlateTest

This method is the main entry point for the ContourPlate test. It orchestrates the following steps:

```csharp
private void ContourPlateTest()
{
    WriteLine("Starting ContourPlateTest...");

    // Create ContourPoints for ContourPlate contour.
    var contourPoints = CreateContourPointsForContourPlate();

    // Create and insert a ContourPlate instance.
    var contourPlate = CreateAndInsertContourPlate(contourPoints);
    if (contourPlate == null)
    {
        return;
    }

    // Select, modify, and display completion message.
    if (!contourPlate.Select())
    {
        MessageBox.Show("Select failed!");
        return;
    }

    if (!contourPlate.Modify())
    {
        MessageBox.Show("Modify failed!");
        return;
    }

    WriteLine(contourPlate.Identifier.ID + " Inserted");
    WriteLine("ContourPlateTest complete!");
}
```

### CreateContourPointsForContourPlate

This method generates a list of ContourPoints required to define the contour of the ContourPlate. It returns the list of ContourPoints with specific properties set for enhanced customization.

```csharp
private List<ContourPoint> CreateContourPointsForContourPlate()
{
    var contourPoints = new List<ContourPoint>
    {
        new ContourPoint(new Point(0, 4000, 0), null),
        new ContourPoint(new Point(2000, 4000, 0), new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT)),
        new ContourPoint(new Point(0, 6000, 0), null)
    };

    // Set specific properties for ContourPlate.
    contourPoints[1].Chamfer.DZ1 = 2500;
    contourPoints[1].Chamfer.DZ2 = 300;

    return contourPoints;
}
```

### CreateAndInsertContourPlate

This method is responsible for creating, customizing, and inserting a ContourPlate into the Tekla Structures model. It performs the following actions:

```csharp
private ContourPlate CreateAndInsertContourPlate(List<ContourPoint> contourPoints)
{
    // Create a ContourPlate instance.
    var contourPlate = new ContourPlate();

    // Add ContourPoints to the ContourPlate.
    contourPlate.Contour.ContourPoints.AddRange(contourPoints);

    // Set properties of the ContourPlate.
    contourPlate.Profile.ProfileString = "PL10";
    contourPlate.Finish = "FOO";
    contourPlate.Material.MaterialString = "Concrete_Undefined";
    contourPlate.Name = "FOOSLAB";

    // Insert the ContourPlate and handle failure.
    if (!contourPlate.Insert())
    {
        WriteLine("ContourPlateTest failed - unable to create contour plate");
        MessageBox.Show("Insert failed!");
        return null;
    }

    // Add the ContourPlate to the ObjectList.
    _objectList.Add(contourPlate);

    return contourPlate;
}
```

## How to Use

1. **Prerequisites:**
   - Ensure Tekla Structures software is installed on your machine.
   - Reference the Tekla Structures API assemblies in the project.

2. **Understanding the Code:**
   - The provided test method and helper functions offer a reference for working with ContourPlates in the Tekla Structures API.
   - Consider adapting the code based on your specific project structure and requirements.

3. **Running the Test:**
   - Open the solution in a compatible IDE (e.g., Visual Studio).
   - Build the solution to resolve dependencies.
   - Run the application to execute the ContourPlateTest.

Feel free to explore, modify, or integrate the code into your Tekla Structures automation projects. The provided test method serves as a valuable reference for working with ContourPlates in the Tekla Structures API.
