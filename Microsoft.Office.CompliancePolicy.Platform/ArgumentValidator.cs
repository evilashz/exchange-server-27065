using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000030 RID: 48
	internal static class ArgumentValidator
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00003209 File Offset: 0x00001409
		public static void ThrowIfNull(string name, object arg)
		{
			if (arg == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003215 File Offset: 0x00001415
		public static void ThrowIfNullOrEmpty(string name, string arg)
		{
			if (string.IsNullOrEmpty(arg))
			{
				throw new ArgumentException("The value is set to null or empty", name);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000322B File Offset: 0x0000142B
		public static void ThrowIfNullOrWhiteSpace(string name, string arg)
		{
			if (string.IsNullOrWhiteSpace(arg))
			{
				throw new ArgumentException("The value is set to null or empty or white space", name);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003241 File Offset: 0x00001441
		public static void ThrowIfCollectionNullOrEmpty<T>(string name, IEnumerable<T> arg)
		{
			if (arg == null || !arg.Any<T>())
			{
				throw new ArgumentException("The collection is set to null or empty", name);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000325A File Offset: 0x0000145A
		public static void ThrowIfZero(string name, uint arg)
		{
			if (arg == 0U)
			{
				throw new ArgumentException("The number is set to 0", name);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000326B File Offset: 0x0000146B
		public static void ThrowIfZeroOrNegative(string name, int arg)
		{
			if (0 >= arg)
			{
				throw new ArgumentException("The number is set to 0 or negative", name);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000327D File Offset: 0x0000147D
		public static void ThrowIfNegative(string name, int arg)
		{
			if (0 > arg)
			{
				throw new ArgumentException("The number is set to negative", name);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000328F File Offset: 0x0000148F
		public static void ThrowIfNegativeTimeSpan(string name, TimeSpan arg)
		{
			if (arg < TimeSpan.Zero)
			{
				throw new ArgumentException("The time span is set to negative", name);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000032AA File Offset: 0x000014AA
		public static void ThrowIfWrongType(string name, object arg, Type expectedType)
		{
			if (arg.GetType() != expectedType)
			{
				throw new ArgumentException("The argument type is not correct", name);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000032C8 File Offset: 0x000014C8
		public static void ThrowIfOutOfRange<T>(string name, T arg, T min, T max) where T : struct, IComparable<T>
		{
			if (arg.CompareTo(min) < 0 || arg.CompareTo(max) > 0)
			{
				throw new ArgumentOutOfRangeException(name, arg, string.Format("The value is out of the valid range {0}:{1}", min, max));
			}
		}
	}
}
