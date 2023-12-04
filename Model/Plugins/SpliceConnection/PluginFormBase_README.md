# Tekla.Structures.Dialog.PluginFormBase Class Documentation

## Overview

The `PluginFormBase` class is a fundamental component of the Tekla Structures Open API, specifically designed to serve as a base class for creating plug-in dialogs. It extends the `FormBase` class, providing additional functionalities for seamless communication with Tekla Structures and managing dialog-related tasks.

## Namespace

```csharp
using Tekla.Structures.Dialog;
```

## Class Definition

```csharp
public class PluginFormBase : FormBase
```

### Events

1. **AttributesLoadedFromModel:**
   - Event triggered after attributes have been loaded from the model into the dialog.

```csharp
public event EventHandler AttributesLoadedFromModel;
```

2. **AttributesSavedToModel:**
   - Event triggered after attributes have been saved to the model.

```csharp
public event EventHandler AttributesSavedToModel;
```

## Constructor

```csharp
public PluginFormBase()
```

- **Description:**
  - Initializes the `PluginFormBase` instance.
  - Loads the default .NET localization file (`DotNetDialogStrings.ail`).
  - Sets the language based on the value retrieved from settings.

## Key Methods

### 1. InitializeForm Method

```csharp
internal override sealed void DoInitializeForm()
```

- **Description:**
  - Registers the dialog and its fields to the Tekla Structures Object Dialog tree.
  - Loads initial values from the standard file.
  - Invoked once per Tekla Structures execution.

### 2. Get Method

```csharp
public void Get()
```

- **Description:**
  - Initiates the process of getting dialog values from the part currently selected in Tekla Structures.

### 3. Apply Method

```csharp
protected override sealed void Apply()
```

- **Description:**
  - Called when the "Apply" button is clicked.
  - Stores the current dialog values locally and to the Tekla Structures Object Dialog tree.

### 4. Modify Method

```csharp
protected override sealed void Modify()
```

- **Description:**
  - Called when the "Modify" button is clicked.
  - Modifies the part currently selected in Tekla Structures.
  - Disables all toggles, then enables the correct ones.

### 5. Create Method

```csharp
protected override sealed void Create()
```

- **Description:**
  - Wrapper method that delegates the request to the `Modify` method.
  - Called when the "Create" button is clicked.

### 6. ReloadForm Method

```csharp
public void ReloadForm()
```

- **Description:**
  - Reloads the dialog values.
  - Useful for refreshing the dialog after changes.

### 7. PutValuesToDialog Method

```csharp
internal void PutValuesToDialog()
```

- **Description:**
  - Puts values to the dialog.
  - Used internally for updating dialog values.

## Data Storage

### 1. SetTemporaryDataStorage Method

```csharp
internal void SetTemporaryDataStorage(DialogDataStorage Storage)
```

- **Description:**
  - Sets the temporary data storage class that communicates with Tekla Structures.
  - Internally handled by Tekla.Structures.Dialog classes.

### 2. GetTemporaryDataStorage Method

```csharp
internal static DialogDataStorage GetTemporaryDataStorage(string Key)
```

- **Description:**
  - Gets the temporary data storage class based on the specified key.
  - Internally handled by Tekla.Structures.Dialog classes.

### 3. SetModelDataStorage Method

```csharp
internal void SetModelDataStorage(DialogDataStorage Storage = null)
```

- **Description:**
  - Sets the model data storage class that communicates with Tekla Structures.
  - Internally handled by Tekla.Structures.Dialog classes.

### 4. GetTemporaryDataStorage Method

```csharp
internal static DialogDataStorage GetTemporaryDataStorage(string Key)
```

- **Description:**
  - Gets the model data storage class based on the specified key.
  - Internally handled by Tekla.Structures.Dialog classes.

## File Path Handling

### 1. LoadValuesPath Method

```csharp
protected override string LoadValuesPath(string FileName)
```

- **Description:**
  - Retrieves the full path for loading values.
  - Override this function to change the default load path or add additional actions on LoadValues.

### 2. SaveValuesPath Method

```csharp
protected override string SaveValuesPath(string FileName)
```

- **Description:**
  - Retrieves the full path for saving values.
  - Override this function to change the default save path or add additional actions on SaveValues.

## Example Usage

### Creating a Custom Dialog

```csharp
using System;
using System.Windows.Forms;
using Tekla.Structures.Datatype;
using Tekla.Structures.Dialog;

public class CustomDialog : PluginFormBase
{
    private Button okButton;
    private Button modifyButton;

    private void InitializeComponent()
    {
        // Initialize components, set up event handlers, etc.
        // Example shows the creation of "OK" and "Modify" buttons.
    }

    public CustomDialog()
    {
        InitializeComponent();
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        // Handle OK button click
        Apply();
        Close();
    }

    private void ModifyButton_Click(object sender, EventArgs e)
    {
        // Handle Modify button click
        Modify();
    }
}
```

## Notes

- It is recommended to refer to the official Tekla Structures API documentation for detailed information and best practices.

---

This documentation provides an overview of the `PluginFormBase` class, its key methods, data storage mechanisms, and an example of how to create a custom dialog. Beginners can use this documentation as a reference to understand and implement dialog functionalities in Tekla Structures plug-ins.