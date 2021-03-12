using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000735 RID: 1845
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class InternalRM
	{
		// Token: 0x060051E9 RID: 20969 RVA: 0x0011F14D File Offset: 0x0011D34D
		[Conditional("_LOGGING")]
		public static void InfoSoap(params object[] messages)
		{
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x0011F14F File Offset: 0x0011D34F
		public static bool SoapCheckEnabled()
		{
			return BCLDebug.CheckEnabled("SOAP");
		}
	}
}
