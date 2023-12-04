# Tekla Events Handling Application

This application demonstrates how to handle events in Tekla Structures using the Tekla Structures Open API. The application provides a simple Windows Form with buttons to activate and deactivate Tekla Structures events, such as `SelectionChange`, `ModelObjectChanged`, `ModelSave`, `Interrupted`, and `TeklaStructuresExit`.

## How it Works

1. **Activate Events:**
   - Clicking the "Activate Events" button initializes and registers Tekla Structures events using the `Events` class.
   - The application subscribes to various events such as `SelectionChange`, `ModelObjectChanged`, etc.
   - A message box is displayed to indicate that the events have been activated.

2. **Deactivate Events:**
   - Clicking the "Deactivate Events" button unregisters Tekla Structures events, stopping event handling.
   - A message box is displayed to indicate that the events have been deactivated.

3. **Event Handlers:**
   - `ModelObjectChanged`: Displays a message box with information about the changed model object.
   - `SelectionChanged`: Displays a message box indicating that the selection has changed.
   - `ModelSave`: Displays a message box indicating that the model has been saved.
   - `Interrupted`: Displays a message box indicating that the operation has been interrupted.
   - `TeklaStructuresExit`: Unregisters Tekla Structures events and exits the application.

4. **Form Closing:**
   - Ensures that Tekla Structures events are properly unregistered when the form is closing.

## Prerequisites

- Tekla Structures installed with the Tekla Structures Open API.

## How to Use

1. Build and run the application.
2. Click "Activate Events" to enable Tekla Structures events handling.
3. Interact with Tekla Structures (e.g., change the selection) to observe event handling.
4. Click "Deactivate Events" to stop Tekla Structures events handling.

## Notes

- Ensure that Tekla Structures is running and a model is open before activating events.
- Handle exceptions that may occur during event initialization.

