using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000543 RID: 1347
	internal static class AssemblyUtil
	{
		// Token: 0x170024C0 RID: 9408
		// (get) Token: 0x06003F69 RID: 16233 RVA: 0x000BF14B File Offset: 0x000BD34B
		internal static string OwaAppVersion
		{
			get
			{
				if (AssemblyUtil.owaApplicationVersion == null)
				{
					AssemblyUtil.owaApplicationVersion = Globals.ApplicationVersion;
				}
				return AssemblyUtil.owaApplicationVersion;
			}
		}

		// Token: 0x0400291C RID: 10524
		private static string owaApplicationVersion;
	}
}
