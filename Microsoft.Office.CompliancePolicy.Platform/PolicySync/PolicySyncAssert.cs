using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000112 RID: 274
	internal static class PolicySyncAssert
	{
		// Token: 0x06000782 RID: 1922 RVA: 0x00017267 File Offset: 0x00015467
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string formatString, params object[] parameters)
		{
			if (!condition)
			{
				PolicySyncAssert.AssertInternal(formatString, parameters);
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00017273 File Offset: 0x00015473
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string formatString)
		{
			if (!condition)
			{
				PolicySyncAssert.AssertInternal(formatString, null);
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001727F File Offset: 0x0001547F
		public static void RetailAssert(bool condition, string formatString, params object[] parameters)
		{
			if (!condition)
			{
				PolicySyncAssert.AssertInternal(formatString, parameters);
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001728B File Offset: 0x0001548B
		public static void RetailAssert(bool condition, string formatString)
		{
			if (!condition)
			{
				PolicySyncAssert.AssertInternal(formatString, null);
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00017298 File Offset: 0x00015498
		private static void AssertInternal(string formatString, params object[] parameters)
		{
			StringBuilder stringBuilder = new StringBuilder("ASSERT: ");
			if (formatString != null)
			{
				if (parameters != null)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, formatString, parameters);
				}
				else
				{
					stringBuilder.Append(formatString);
				}
			}
			stringBuilder.ToString();
		}
	}
}
