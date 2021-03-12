using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200012C RID: 300
	internal class NativeMethods
	{
		// Token: 0x060008E2 RID: 2274
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern long GetTickCount64();
	}
}
