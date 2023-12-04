# Tekla Structures Example Application Documentation

## Overview

This documentation provides detailed information about the Tekla Structures Example Application. The application demonstrates the interaction with Tekla Structures, a structural BIM (Building Information Modeling) software. It is designed to create beams, components, and retrieve attributes from the Tekla Structures model.

## Constants

### ProfileString

- **Type**: `string`
- **Description**: The profile string for beams.

### MaterialString

- **Type**: `string`
- **Description**: The material string for beams.

### AttributeName

- **Type**: `string`
- **Description**: The name of the attribute used in the example.

## Interfaces

### IBeamCreator

- **Description**: Interface for classes responsible for creating beams in the Tekla Structures model.

#### Methods

#### `CreateBeam(Point startPoint, Point endPoint, string profileString, string materialString) : Beam`

- **Parameters**:
  - `startPoint` (Type: `Point`): The start point of the beam.
  - `endPoint` (Type: `Point`): The end point of the beam.
  - `profileString` (Type: `string`): The profile string of the beam.
  - `materialString` (Type: `string`): The material string of the beam.
- **Returns**: The created beam or null if creation fails.

### IComponentCreator

- **Description**: Interface for classes responsible for creating components in the Tekla Structures model.

#### Methods

#### `CreateComponent(Beam beam, string componentName, int componentNumber) : Component`

- **Parameters**:
  - `beam` (Type: `Beam`): The beam to associate with the component.
  - `componentName` (Type: `string`): The name of the component.
  - `componentNumber` (Type: `int`): The number of the component.
- **Returns**: The created component or null if creation fails.

### IAttributeReader

- **Description**: Interface for classes responsible for reading attributes in the Tekla Structures model.

#### Methods

#### `GetComponentAttribute(Component component, string attributeName) : double`

- **Parameters**:
  - `component` (Type: `Component`): The component to retrieve the attribute from.
  - `attributeName` (Type: `string`): The name of the attribute to retrieve.
- **Returns**: The value of the attribute or 0.0 if retrieval fails.

### IErrorLogger

- **Description**: Interface for classes responsible for logging errors.

#### Methods

#### `LogError(string message) : void`

- **Parameters**:
  - `message` (Type: `string`): The error message to log.

## Classes

### BeamCreator

- **Description**: Class responsible for creating beams in the Tekla Structures model.

#### Constructors

#### `BeamCreator(IErrorLogger errorLogger)`

- **Parameters**:
  - `errorLogger` (Type: `IErrorLogger`): The error logger instance.

#### Methods

#### `CreateBeam(Point startPoint, Point endPoint) : Beam`

- **Description**: Creates a beam in the Tekla Structures model.
- **Parameters**:
  - `startPoint` (Type: `Point`): The start point of the beam.
  - `endPoint` (Type: `Point`): The end point of the beam.
- **Returns**: The created beam or null if creation fails.

### ComponentCreator

- **Description**: Class responsible for creating components in the Tekla Structures model.

#### Constructors

#### `ComponentCreator(IErrorLogger errorLogger)`

- **Parameters**:
  - `errorLogger` (Type: `IErrorLogger`): The error logger instance.

#### Methods

#### `CreateComponent(Beam beam, string componentName, int componentNumber) : Component`

- **Description**: Creates a component in the Tekla Structures model.
- **Parameters**:
  - `beam` (Type: `Beam`): The beam to associate with the component.
  - `componentName` (Type: `string`): The name of the component.
  - `componentNumber` (Type: `int`): The number of the component.
- **Returns**: The created component or null if creation fails.

### AttributeReader

- **Description**: Class responsible for reading attributes in the Tekla Structures model.

#### Constructors

#### `AttributeReader(IErrorLogger errorLogger)`

- **Parameters**:
  - `errorLogger` (Type: `IErrorLogger`): The error logger instance.

#### Methods

#### `GetComponentAttribute(Component component, string attributeName) : double`

- **Description**: Gets the value of a component attribute in the Tekla Structures model.
- **Parameters**:
  - `component` (Type: `Component`): The component to retrieve the attribute from.
  - `attributeName` (Type: `string`): The name of the attribute to retrieve.
- **Returns**: The value of the attribute or 0.0 if retrieval fails.

### ErrorLogger

- **Description**: Class for logging errors.

#### Methods

#### `LogError(string message) : void`

- **Description**: Logs an error message.
- **Parameters**:
  - `message` (Type: `string`): The error message to log.

### Example

- **Description**: Main class for the example application.

#### Constructors

#### `Example(IBeamCreator beamCreator, IComponentCreator componentCreator, IAttributeReader attributeReader, IErrorLogger errorLogger)`

- **Parameters**:
  - `beamCreator` (Type: `IBeamCreator`): The beam creator instance.
  - `componentCreator` (Type: `IComponentCreator`): The component creator instance.
  - `attributeReader` (Type: `IAttributeReader`): The attribute reader instance.
  - `errorLogger` (Type: `IErrorLogger`): The error logger instance.

#### Methods

#### `RunExample() : void`

- **Description**: Runs the example application.

## Usage

1. Initialize instances of the required classes implementing the interfaces.
2. Create a new `Example` instance with the initialized instances.
3. Call the `RunExample` method on the `Example` instance to execute the example application.

### Example Usage

```csharp
// Initialize instances of required classes
IBeamCreator beamCreator = new BeamCreator(new ErrorLogger());
IComponentCreator componentCreator = new ComponentCreator(new ErrorLogger());
IAttributeReader attributeReader = new AttributeReader(new ErrorLogger());
IErrorLogger errorLogger = new ErrorLogger();

// Create Example instance
Example example = new Example(beamCreator, componentCreator, attributeReader, errorLogger);

// Run the example application
example.RunExample();
