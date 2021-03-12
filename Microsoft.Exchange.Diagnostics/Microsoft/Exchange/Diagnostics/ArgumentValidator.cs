using System;
using System.Globalization;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000E4 RID: 228
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ArgumentValidator
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x0001A638 File Offset: 0x00018838
		public static void ThrowIfNull(string name, object arg)
		{
			if (arg == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001A644 File Offset: 0x00018844
		public static void ThrowIfEmpty(string name, string arg)
		{
			if (arg.Equals(string.Empty))
			{
				throw new ArgumentException("The value is set to empty", name);
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001A65F File Offset: 0x0001885F
		public static void ThrowIfNullOrEmpty(string name, string arg)
		{
			if (string.IsNullOrEmpty(arg))
			{
				throw new ArgumentException("The value is set to null or empty", name);
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001A675 File Offset: 0x00018875
		public static void ThrowIfNullOrEmpty(string name, object[] arg)
		{
			if (arg == null)
			{
				throw new ArgumentNullException(name);
			}
			if (arg.Length == 0)
			{
				throw new ArgumentException("The array value is set to empty", name);
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001A692 File Offset: 0x00018892
		public static void ThrowIfNullOrWhiteSpace(string name, string arg)
		{
			if (string.IsNullOrWhiteSpace(arg))
			{
				throw new ArgumentException("The value is set to null or white space", name);
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001A6A8 File Offset: 0x000188A8
		public static void ThrowIfEmpty(string name, Guid arg)
		{
			if (arg.Equals(Guid.Empty))
			{
				throw new ArgumentException("The value is set to empty", name);
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001A6C4 File Offset: 0x000188C4
		public static void ThrowIfNegative(string name, int arg)
		{
			if (arg < 0)
			{
				throw new ArgumentOutOfRangeException(name, arg, "The value is negative");
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001A6DC File Offset: 0x000188DC
		public static void ThrowIfZeroOrNegative(string name, int arg)
		{
			if (arg <= 0)
			{
				throw new ArgumentOutOfRangeException(name, arg, "The value is zero or negative");
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001A6F4 File Offset: 0x000188F4
		public static void ThrowIfOutOfRange<T>(string name, T arg, T min, T max) where T : struct, IComparable<T>
		{
			if (arg.CompareTo(min) < 0 || arg.CompareTo(max) > 0)
			{
				throw new ArgumentOutOfRangeException(name, arg, string.Format("The value is out of the valid range {0}:{1}", min, max));
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001A748 File Offset: 0x00018948
		public static void ThrowIfTypeInvalid<T>(string name, object arg)
		{
			if (arg == null || (arg.GetType() != typeof(T) && !arg.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid type for arg:{0}, Expected:{1}, Actual:{2}", new object[]
				{
					name,
					typeof(T).Name,
					arg.GetType().Name
				}));
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001A7CC File Offset: 0x000189CC
		public static void ThrowIfTypeInvalid(string name, object arg, Type expectedType)
		{
			if (arg == null || (arg.GetType() != expectedType && !arg.GetType().GetTypeInfo().IsSubclassOf(expectedType)))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid type for arg:{0}, Expected:{1}, Actual:{2}", new object[]
				{
					name,
					expectedType.Name,
					arg.GetType().Name
				}));
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001A838 File Offset: 0x00018A38
		public static void ThrowIfInvalidValue<T>(string name, T arg, Predicate<T> validator)
		{
			if (!validator(arg))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value for arg:{0}, value:{1}", new object[]
				{
					name,
					arg
				}));
			}
		}
	}
}
