### Class Definition

```csharp
public class Vector : Point
```

- **Inheritance:**
  - Inherits from the `Point` class, which represents a point in three-dimensional space.

### Key Properties

- **`X`, `Y`, `Z`:**
  - Properties inherited from the `Point` class, representing the coordinates of the vector in the three-dimensional space.

- **`EPSILON`:**
  - A constant representing a small value (`1E-06`), used for tolerance comparisons.

### Constructors

1. **Default Constructor:**
   ```csharp
   public Vector()
   ```
   - Instantiates a zero-length vector.

2. **Parameterized Constructor:**
   ```csharp
   public Vector(double X, double Y, double Z) : base(X, Y, Z)
   ```
   - Instantiates a vector with the given coordinates.

3. **Constructor with a Point Parameter:**
   ```csharp
   public Vector(Point Point) : base(Point)
   ```
   - Instantiates a new vector with the given point.

### Methods

1. **`Normalize()`:**
   ```csharp
   public double Normalize()
   ```
   - Normalizes the vector to a length of 1.0 (unit vector).

2. **`Normalize(double NewLength)`:**
   ```csharp
   public double Normalize(double NewLength)
   ```
   - Normalizes the vector to the specified length.

3. **`GetLength()`:**
   ```csharp
   public double GetLength()
   ```
   - Gets the length (magnitude) of the vector.

4. **`GetAngleBetween(Vector Vector)`:**
   ```csharp
   public double GetAngleBetween(Vector Vector)
   ```
   - Gets the angle (in radians) between the current vector and the given vector.

5. **`GetNormal()`:**
   ```csharp
   public Vector GetNormal()
   ```
   - Returns a new normalized equivalent of the current vector.

6. **`Dot(Vector Vector)`:**
   ```csharp
   public double Dot(Vector Vector)
   ```
   - Returns the dot product of the current vector and the given vector.

7. **`Cross(Vector Vector)`:**
   ```csharp
   public Vector Cross(Vector Vector)
   ```
   - Returns a new cross product vector of the current vector and the given vector.

### Operator Overloads

- **Multiplication Operator (`*`):**
  ```csharp
  public static Vector operator *(Vector Vector, double Multiplier)
  ```
  - Calculates the multiplication of the given vector with the given scalar.

- **Multiplication Operator (`*`):**
  ```csharp
  public static Vector operator *(double Multiplier, Vector Vector)
  ```
  - Calculates the multiplication of the given vector with the given scalar.

### Example Usage

```csharp
Vector vector1 = new Vector(1.0, 2.0, 3.0);
Vector vector2 = new Vector(4.0, 5.0, 6.0);

double angle = vector1.GetAngleBetween(vector2);
Console.WriteLine($"Angle between vectors: {angle} radians");

Vector crossProduct = vector1.Cross(vector2);
Console.WriteLine($"Cross product: {crossProduct}");

Vector scaledVector = vector1 * 2.5;
Console.WriteLine($"Scaled vector: {scaledVector}");
```

### Notes

- The `Vector` class provides methods for vector operations such as normalization, calculating angles, dot and cross products, and scalar multiplication.
- It is part of the Tekla Structures API, and documentation for Tekla Structures API should be referenced for detailed information on usage and best practices.

This class is particularly useful for geometric operations in three-dimensional space, commonly encountered in structural design and modeling scenarios.