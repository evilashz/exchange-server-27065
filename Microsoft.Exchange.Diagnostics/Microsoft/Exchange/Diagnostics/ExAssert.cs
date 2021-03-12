using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000026 RID: 38
	public static class ExAssert
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x000041EF File Offset: 0x000023EF
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string formatString, params object[] parameters)
		{
			if (!condition)
			{
				ExAssert.AssertInternal(formatString, parameters);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000041FB File Offset: 0x000023FB
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string formatString)
		{
			if (!condition)
			{
				ExAssert.AssertInternal(formatString, null);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004207 File Offset: 0x00002407
		public static void RetailAssert(bool condition, string formatString, params object[] parameters)
		{
			if (!condition)
			{
				ExAssert.AssertInternal(formatString, parameters);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004213 File Offset: 0x00002413
		public static void RetailAssert(bool condition, string formatString)
		{
			if (!condition)
			{
				ExAssert.AssertInternal(formatString, null);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004220 File Offset: 0x00002420
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
			string text = stringBuilder.ToString();
			ExTraceGlobals.CommonTracer.TraceDebug(23657, 0L, text);
			throw new ExAssertException(text);
		}
	}
}
