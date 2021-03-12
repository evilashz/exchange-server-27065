using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002FC RID: 764
	internal static class Util
	{
		// Token: 0x06000E19 RID: 3609 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		public static bool ArrayEqual(IList<byte> a, IList<byte> b, int offset, int count)
		{
			if (a == null || b == null)
			{
				return object.ReferenceEquals(a, b);
			}
			for (int i = 0; i < count; i++)
			{
				if (a[offset + i] != b[offset + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0001C620 File Offset: 0x0001A820
		public static string DumpBytes(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				return "<null>";
			}
			if (count == 0)
			{
				return string.Empty;
			}
			if (offset < 0 || count < 0 || offset >= data.Length || offset + count > data.Length)
			{
				return "<invalid>";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(BitConverter.ToString(data, offset, Math.Min(count, 8)));
			if (count > 8)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "... ({0} bytes)", new object[]
				{
					count
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0001C6A2 File Offset: 0x0001A8A2
		public static bool ObjectContentEquals<T>(T left, T right) where T : class, IContentEquatable<T>
		{
			if (left == null || right == null)
			{
				return object.ReferenceEquals(left, right);
			}
			return left.ContentEquals(right);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0001C6D4 File Offset: 0x0001A8D4
		public static bool ArrayObjectContentEquals<T>(T[] left, T[] right, int length) where T : class, IContentEquatable<T>
		{
			if (left == null || right == null)
			{
				return object.ReferenceEquals(left, right);
			}
			for (int i = 0; i < length; i++)
			{
				if (!Util.ObjectContentEquals<T>(left[i], right[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0001C714 File Offset: 0x0001A914
		public static bool ArrayStructEquals<T>(T[] left, T[] right, int length) where T : struct
		{
			if (left == null || right == null)
			{
				return object.ReferenceEquals(left, right);
			}
			for (int i = 0; i < length; i++)
			{
				if (!left[i].Equals(right[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0001C760 File Offset: 0x0001A960
		public static T[] DeepCloneArray<T>(T[] value) where T : class, IDeepCloneable<T>
		{
			T[] array = null;
			if (value != null)
			{
				array = new T[value.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = ((value[i] == null) ? default(T) : value[i].DeepClone());
				}
			}
			return array;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0001C7C0 File Offset: 0x0001A9C0
		public static int CalculateHashCode(IEnumerable<int> hashes)
		{
			int num = 0;
			foreach (int num2 in hashes)
			{
				num ^= num2;
				num *= 33;
			}
			return num;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0001C810 File Offset: 0x0001AA10
		public static string AddTrailingDirectorySeparator(string dir)
		{
			if (!string.IsNullOrEmpty(dir))
			{
				char[] trimChars = new char[]
				{
					LibraryHelpers.DirectorySeparatorChar,
					LibraryHelpers.AltDirectorySeparatorChar
				};
				return dir.TrimEnd(trimChars) + LibraryHelpers.DirectorySeparatorChar;
			}
			return dir;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0001C858 File Offset: 0x0001AA58
		public static byte[] ConvertToNullTerminatedAsciiByteArray(string value)
		{
			if (value == null)
			{
				return null;
			}
			byte[] array = new byte[value.Length + 1];
			LibraryHelpers.EncodingASCII.GetBytes(value, 0, value.Length, array, 0);
			array[array.Length - 1] = 0;
			return array;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0001C898 File Offset: 0x0001AA98
		public static byte[] ConvertToNullTerminatedUnicodeByteArray(string value)
		{
			if (value == null)
			{
				return null;
			}
			int byteCount = Encoding.Unicode.GetByteCount(value);
			byte[] array = new byte[byteCount + 2];
			Encoding.Unicode.GetBytes(value, 0, value.Length, array, 0);
			array[array.Length - 2] = 0;
			array[array.Length - 1] = 0;
			return array;
		}
	}
}
