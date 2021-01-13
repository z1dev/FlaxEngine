// Copyright (c) 2012-2021 Wojciech Figat. All rights reserved.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FlaxEngine.Utilities
{
    /// <summary>
    /// Collection of various extension methods.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Creates deep clone for a class if all members of this class are marked as serializable (uses Json serialization).
        /// </summary>
        /// <param name="instance">The input instance of an object.</param>
        /// <typeparam name="T">The instance type of an object.</typeparam>
        /// <returns>Returns new object of provided class.</returns>
        public static T DeepClone<T>(this T instance)
        where T : new()
        {
            var json = Json.JsonSerializer.Serialize(instance);
            return Json.JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// Creates raw clone for a structure using memory copy. Valid only for value types.
        /// </summary>
        /// <param name="instance">The input instance of an object.</param>
        /// <typeparam name="T">The instance type of an object.</typeparam>
        /// <returns>Returns new object of provided structure.</returns>
        public static T RawClone<T>(this T instance)
        {
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(instance));
            try
            {
                Marshal.StructureToPtr(instance, ptr, false);
                return (T)Marshal.PtrToStructure(ptr, instance.GetType());
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <summary>
        /// Splits string into lines
        /// </summary>
        /// <param name="str">Text to split</param>
        /// <param name="removeEmptyLines">True if remove empty lines, otherwise keep them</param>
        /// <returns>Array with all lines</returns>
        public static string[] GetLines(this string str, bool removeEmptyLines = false)
        {
            return str.Split(new[]
            {
                "\r\n",
                "\r",
                "\n"
            }, removeEmptyLines ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="destination">The <see cref="ICollection{T}"/> to add items to.</param>
        /// <param name="collection">The collection whose elements should be added to the end of the <paramref name="destination"/>. It can contain elements that are <see langword="null"/>, if type <typeparamref name="T"/> is a reference type.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="destination"/> or <paramref name="collection"/> are <see langword="null"/>.</exception>
        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> collection)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (var item in collection)
            {
                destination.Add(item);
            }
        }

        /// <summary>
        /// Enqueues the elements of the specified collection to the <see cref="Queue{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="queue">The <see cref="Queue{T}"/> to add items to.</param>
        /// <param name="collection">The collection whose elements should be added to the <paramref name="queue"/>. It can contain elements that are <see langword="null"/>, if type <typeparamref name="T"/> is a reference type.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queue"/> or <paramref name="collection"/> are <see langword="null"/>.</exception>
        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> collection)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (var item in collection)
            {
                queue.Enqueue(item);
            }
        }

        /// <summary>
        /// Pushes the elements of the specified collection to the <see cref="Stack{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="stack">The <see cref="Stack{T}"/> to add items to.</param>
        /// <param name="collection">The collection whose elements should be pushed on to the <paramref name="stack"/>. It can contain elements that are <see langword="null"/>, if type <typeparamref name="T"/> is a reference type.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="stack"/> or <paramref name="collection"/> are <see langword="null"/>.</exception>
        public static void PushRange<T>(this Stack<T> stack, IEnumerable<T> collection)
        {
            if (stack == null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (var item in collection)
            {
                stack.Push(item);
            }
        }

        /// <summary>
        /// Performs the specified action on each element of the <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the input sequence.</typeparam>
        /// <param name="source">The sequence of elements to execute the <see cref="IEnumerable{T}"/>.</param>
        /// <param name="action">The <see cref="Action{T}"/> delegate to perform on each element of the <see cref="IEnumerable{T}"/>1.</param>
        /// <exception cref="ArgumentException"><paramref name="source"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Chooses a random item from the collection.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the input sequence.</typeparam>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="collection">Collection to choose item from.</param>
        /// <returns>A random item from collection</returns>
        /// <exception cref="ArgumentNullException">If the random argument is null.</exception>
        /// <exception cref="ArgumentNullException">If the collection is null.</exception>
        public static T Choose<T>(this Random random, IList<T> collection)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection[random.Next(collection.Count)];
        }

        /// <summary>
        /// Chooses a random item.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the input sequence.</typeparam>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="collection">Collection to choose item from.</param>
        /// <returns>A random item from collection</returns>
        /// <exception cref="ArgumentNullException">If the random  is null.</exception>
        /// <exception cref="ArgumentNullException">If the collection is null.</exception>
        public static T Choose<T>(this Random random, params T[] collection)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection[random.Next(collection.Length)];
        }

        /// <summary>
        /// Shuffles the collection in place.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the input sequence.</typeparam>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="collection">Collection to shuffle.</param>
        /// <exception cref="ArgumentNullException">If the random argument is null.</exception>
        /// <exception cref="ArgumentNullException">If the random collection is null.</exception>
        public static void Shuffle<T>(this Random random, IList<T> collection)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            int n = collection.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = collection[k];
                collection[k] = collection[n];
                collection[n] = value;
            }
        }

        /// <summary>
        /// Generates a random <see cref="bool"/>.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <returns>A <see cref="bool"/> thats either true or false.</returns>
        public static bool NextBool(this Random random) => random.Next(2) == 1;

        /// <summary>
        /// Generates a random <see cref="bool"/> with a weight value to adjust preference.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="weight">Normalized value that determines the chance to return true.</param>
        /// <returns>A <see cref="bool"/> thats either true or false.</returns>
        public static bool NextBool(this Random random, float weight = 0.5f) => weight >= NextFloat(random);

        /// <summary>
        /// Generates a random <see cref="byte"/> value between min and max.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="min">The lower boundary.</param>
        /// <param name="max">The upper boundary.</param>
        /// <returns>A <see cref="byte"/> between min and max.</returns>
        public static byte NextByte(this Random random, byte min = 0, byte max = 2)
        {
            return (byte)random.Next(min, max);
        }

        /// <summary>
        /// Generates a random <see cref="float"/>  value between min and max.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>A random <see cref="float"/> between min and max.</returns>
        public static float NextFloat(this Random random, float min = 0.0f, float max = 2.0f)
        {
            return (float)random.NextDouble() * (max - min) + min;
        }

        /// <summary>
        /// Generates a random <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="randomRoll">Should the roll value be randomized.</param>
        /// <returns>A random <see cref="Quaternion"/>.</returns>
        public static Quaternion NextQuaternion(this Random random, bool randomRoll = false)
        {

            return Quaternion.Euler(
                NextFloat(random, -180, 180),
                NextFloat(random, -180, 180),
                randomRoll ? NextFloat(random, -180, 180) : 0);
        }

        /// <summary>
        /// Generates a uniformly distributed random unit length vector point on a unit sphere.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <returns>A random <see cref="Vector3"/>.</returns>
        public static Vector3 NextVector3(this Random random)
        {
            Vector3 output;

            do
            {
                // Create random float with a mean of 0 and deviation of ±1;
                output.X = NextFloat(random) * 2.0f - 1.0f;
                output.Y = NextFloat(random) * 2.0f - 1.0f;
                output.Z = NextFloat(random) * 2.0f - 1.0f;

            } while (output.LengthSquared > 1 || output.LengthSquared < 1e-6f);

            output *= (1.0f / (float)Math.Sqrt(output.LengthSquared));

            return output;
        }

        /// <summary>
        /// Generates a uniformly distributed random unit length vector point on a unit sphere in 2D.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <returns>A random <see cref="Vector2"/>.</returns>
        public static Vector2 NextVector2(this Random random)
        {
            Vector2 output;

            do
            {
                output.X = NextFloat(random) * 2.0f - 1.0f;
                output.Y = NextFloat(random) * 2.0f - 1.0f;

            } while (output.LengthSquared > 1 || output.LengthSquared < 1e-6f);

            output *= (1.0f / (float)Math.Sqrt(output.LengthSquared));

            return output;
        }

        /// <summary>
        /// Generates a random <see cref="Color"/>.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="trueRandom">Should the color be generated from a single random Hue value or separate values for each channel.</param>
        /// <param name="randomAlpha">Randomize the alpha value.</param>
        /// <returns>A nice random <see cref="Color"/>.</returns>
        public static Color NextColor(this Random random, bool trueRandom, bool randomAlpha)
        {
            float alpha = randomAlpha ? NextFloat(random) : 1f;

            if (!trueRandom)
                return Color.FromHSV(NextFloat(random, 0f, 360f), 1f, 1f, alpha);

            return new Color(
                NextFloat(random, 0f, 255f),
                NextFloat(random, 0f, 255f),
                NextFloat(random, 0f, 255f), alpha);
        }

        /// <summary>
        /// Gets a random <see cref="double"/>.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        /// <returns>A random <see cref="double"/></returns>
        public static double NextDouble(this Random random, double min = 0.0d, double max = 2.0d)
        {
            return random.NextDouble() * (max - min) + min;
        }

        /// <summary>
        /// Gets a random <see cref="long"/>.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <returns>A random <see cref="long"/></returns>
        internal static long NextLong(this Random random)
        {
            var numArray = new byte[8];
            random.NextBytes(numArray);
            return (long)(BitConverter.ToUInt64(numArray, 0) & 9223372036854775807L);
        }

        /// <summary>
        /// Generates a random normalized 2D direction <see cref="Vector2"/>.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <returns>A random normalized 2D direction <see cref="Vector2"/>.</returns>
        public static Vector2 NextDirection2D(this Random random)
        {
            return Vector2.Normalize(new Vector2((float)random.NextDouble(), (float)random.NextDouble()));
        }

        /// <summary>
        /// Generates a random normalized 3D direction <see cref="Vector3"/>.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <returns>A random normalized 3D direction <see cref="Vector3"/>.</returns>
        public static Vector3 NextDirection3D(this Random random)
        {
            return Vector3.Normalize(new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()));
        }

        /// <summary>
        /// Generates a random <see cref="Vector2"/> point in a circle of a given radius.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="radius">Radius of circle. Default 1.0f.</param>
        /// <returns>A random <see cref="Vector2"/> point in a circle of a given radius.</returns>
        public static Vector2 PointInACircle(this Random random, float radius = 1.0f)
        {
            var randomRadius = (float)random.NextDouble() * radius;

            return new Vector2
            {
                X = (float)Math.Cos(random.NextDouble()) * randomRadius,
                Y = (float)Math.Sin(random.NextDouble()) * randomRadius,
            };
        }
    }
}
