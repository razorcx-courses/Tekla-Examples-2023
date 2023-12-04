using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;
using Color = Tekla.Structures.Model.UI.Color;
using Point = Tekla.Structures.Geometry3d.Point;

namespace GraphicsDrawerChatGPT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var example = new Example();
            example.Example1();
        }
    }

    /// <summary>
    /// A class demonstrating the usage of Tekla Structures GraphicsDrawer.
    /// </summary>
    public class Example
    {
        // Constants for commonly used colors
        private readonly Color TextColor = new Color(1.0, 0.5, 0.0);
        private readonly Color LineColor = new Color(1.0, 0.0, 0.0);
        private readonly Color MeshSurfaceColor = new Color(1.0, 0.0, 0.0, 0.5);
        private readonly Color MeshLinesColor = new Color(0.0, 0.0, 1.0);
        private readonly Color RectangleColor = new Color(0.0, 1.0, 0.0);

        /// <summary>
        /// Draws various graphical elements using the GraphicsDrawer.
        /// </summary>
        public void Example1()
        {
            try
            {
                // Initialize GraphicsDrawer
                var drawer = new GraphicsDrawer();

                // Draw text
                DrawText(drawer, new Point(0.0, 1000.0, 1000.0), "TEXT SAMPLE", TextColor);

                // Draw line segment
                DrawLineSegment(drawer, new Point(0.0, 0.0, 0.0), new Point(1000.0, 1000.0, 1000.0), LineColor);

                // Create and draw a simple rectangular mesh
                var mesh = CreateRectangularMesh();
                DrawMesh(drawer, mesh);

                // Draw a rectangle
                DrawRectangle(drawer, new Point(500.0, 500.0, 0.0), 300.0, 200.0, RectangleColor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that none of the provided parameters are null.
        /// </summary>
        /// <param name="parameters">Parameters to validate.</param>
        private void ValidateParameters(params object[] parameters)
        {
            foreach (var parameter in parameters)
            {
                if (parameter == null)
                {
                    throw new ArgumentNullException($"{nameof(parameters)} must not contain null values.");
                }
            }
        }

        /// <summary>
        /// Draws text using the provided GraphicsDrawer.
        /// </summary>
        /// <param name="drawer">The GraphicsDrawer instance.</param>
        /// <param name="position">The 3D position of the text.</param>
        /// <param name="text">The text to be drawn.</param>
        /// <param name="color">The color of the text.</param>
        private void DrawText(GraphicsDrawer drawer, Point position, string text, Color color)
        {
            ValidateParameters(drawer, position);

            drawer.DrawText(position, text, color);
        }

        /// <summary>
        /// Draws a line segment using the provided GraphicsDrawer.
        /// </summary>
        /// <param name="drawer">The GraphicsDrawer instance.</param>
        /// <param name="startPoint">The starting point of the line segment.</param>
        /// <param name="endPoint">The ending point of the line segment.</param>
        /// <param name="color">The color of the line segment.</param>
        private void DrawLineSegment(GraphicsDrawer drawer, Point startPoint, Point endPoint, Color color)
        {
            ValidateParameters(drawer, startPoint, endPoint);

            drawer.DrawLineSegment(startPoint, endPoint, color);
        }

        /// <summary>
        /// Creates a rectangular mesh for demonstration purposes.
        /// </summary>
        /// <returns>The created rectangular mesh.</returns>
        private Mesh CreateRectangularMesh()
        {
            var mesh = new Mesh();
            mesh.AddPoint(new Point(0.0, 0.0, 0.0));
            mesh.AddPoint(new Point(1000.0, 0.0, 0.0));
            mesh.AddPoint(new Point(1000.0, 1000.0, 0.0));
            mesh.AddPoint(new Point(0.0, 1000.0, 0.0));
            mesh.AddTriangle(0, 1, 2);
            mesh.AddTriangle(0, 2, 3);
            mesh.AddLine(0, 1); mesh.AddLine(1, 2); mesh.AddLine(2, 3); mesh.AddLine(3, 1);

            return mesh;
        }

        /// <summary>
        /// Draws the surface and lines of the provided mesh using the GraphicsDrawer.
        /// </summary>
        /// <param name="drawer">The GraphicsDrawer instance.</param>
        /// <param name="mesh">The mesh to be drawn.</param>
        private void DrawMesh(GraphicsDrawer drawer, Mesh mesh)
        {
            ValidateParameters(drawer, mesh);

            drawer.DrawMeshSurface(mesh, MeshSurfaceColor);
            drawer.DrawMeshLines(mesh, MeshLinesColor);
        }

        /// <summary>
        /// Draws a rectangle using the provided GraphicsDrawer.
        /// </summary>
        /// <param name="drawer">The GraphicsDrawer instance.</param>
        /// <param name="centerPoint">The center point of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="color">The color of the rectangle.</param>
        private void DrawRectangle(GraphicsDrawer drawer, Point centerPoint, double width, double height, Color color)
        {
            ValidateParameters(drawer, centerPoint);

            var halfWidth = width / 2.0;
            var halfHeight = height / 2.0;

            var startPoint = new Point(centerPoint.X - halfWidth, centerPoint.Y - halfHeight, centerPoint.Z);
            var endPoint = new Point(centerPoint.X + halfWidth, centerPoint.Y + halfHeight, centerPoint.Z);

            DrawRectangleLines(drawer, startPoint, endPoint, color);
        }

        /// <summary>
        /// Draws the lines of a rectangle using the provided GraphicsDrawer.
        /// </summary>
        /// <param name="drawer">The GraphicsDrawer instance.</param>
        /// <param name="startPoint">The starting point of the rectangle.</param>
        /// <param name="endPoint">The ending point of the rectangle.</param>
        /// <param name="color">The color of the rectangle.</param>
        private void DrawRectangleLines(GraphicsDrawer drawer, Point startPoint, Point endPoint, Color color)
        {
            drawer.DrawLineSegment(startPoint, new Point(endPoint.X, startPoint.Y, startPoint.Z), color);
            drawer.DrawLineSegment(new Point(endPoint.X, startPoint.Y, startPoint.Z), new Point(endPoint.X, endPoint.Y, endPoint.Z), color);
            drawer.DrawLineSegment(new Point(endPoint.X, endPoint.Y, endPoint.Z), new Point(startPoint.X, endPoint.Y, endPoint.Z), color);
            drawer.DrawLineSegment(new Point(startPoint.X, endPoint.Y, endPoint.Z), startPoint, color);
        }
    }
}

