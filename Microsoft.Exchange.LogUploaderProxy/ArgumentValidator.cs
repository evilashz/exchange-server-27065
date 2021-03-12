using System;
using System.Collections.Generic;
using Microsoft.Office.CompliancePolicy;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x02000002 RID: 2
	public static class ArgumentValidator
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void ThrowIfCollectionNullOrEmpty<T>(string name, IEnumerable<T> arg)
		{
			ArgumentValidator.ThrowIfCollectionNullOrEmpty<T>(name, arg);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D9 File Offset: 0x000002D9
		public static void ThrowIfNegativeTimeSpan(string name, TimeSpan arg)
		{
			ArgumentValidator.ThrowIfNegativeTimeSpan(name, arg);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E2 File Offset: 0x000002E2
		public static void ThrowIfNull(string name, object arg)
		{
			ArgumentValidator.ThrowIfNull(name, arg);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EB File Offset: 0x000002EB
		public static void ThrowIfNullOrEmpty(string name, string arg)
		{
			ArgumentValidator.ThrowIfNullOrEmpty(name, arg);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F4 File Offset: 0x000002F4
		public static void ThrowIfWrongType(string name, object arg, Type expectedType)
		{
			ArgumentValidator.ThrowIfWrongType(name, arg, expectedType);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020FE File Offset: 0x000002FE
		public static void ThrowIfZero(string name, uint arg)
		{
			ArgumentValidator.ThrowIfZero(name, arg);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002107 File Offset: 0x00000307
		public static void ThrowIfZeroOrNegative(string name, int arg)
		{
			ArgumentValidator.ThrowIfZeroOrNegative(name, arg);
		}
	}
}
