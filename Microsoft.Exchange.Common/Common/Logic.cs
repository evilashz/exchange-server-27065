using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000013 RID: 19
	internal static class Logic
	{
		// Token: 0x0600003F RID: 63 RVA: 0x0000286F File Offset: 0x00000A6F
		public static bool Implies(bool a, bool b)
		{
			return (a && b) || !a;
		}
	}
}
