// The System.Numerics namespace in C# provides 
// several classes for numerical computations. Here are the key classes and their uses:

using System.Numerics;

// Vector2 for 2D coordinates
Vector2 position = new Vector2(x: 10.0f, y: 20.0f);
Vector2 velocity = new Vector2(5.0f, 5.0f);
Vector2 newPosition = position + velocity;

// Vector3 for 3D coordinates
Vector3 position3D = new Vector3(x: 1.0f, y: 2.0f, z: 3.0f);
Vector3 rotated = Vector3.Transform(position3D, quaternion);

// Vector4
Vector4 color = new Vector4(r: 1.0f, g: 0.0f, b: 0.0f, a: 1.0f);

Matrix4x4