using System;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000012 RID: 18
	internal static class AutodiscoverEwsWebConfiguration
	{
		// Token: 0x04000068 RID: 104
		public static readonly bool SoapEndpointEnabled = new BoolAppSettingsEntry("SoapEndpointEnabled", false, ExTraceGlobals.VerboseTracer).Value;

		// Token: 0x04000069 RID: 105
		public static readonly bool WsSecurityEndpointEnabled = new BoolAppSettingsEntry("WsSecurityEndpointEnabled", false, ExTraceGlobals.VerboseTracer).Value;

		// Token: 0x0400006A RID: 106
		public static readonly bool WsSecuritySymmetricKeyEndpointEnabled = new BoolAppSettingsEntry("WsSecuritySymmetricKeyEndpointEnabled", false, ExTraceGlobals.VerboseTracer).Value;

		// Token: 0x0400006B RID: 107
		public static readonly bool WsSecurityX509CertEndpointEnabled = new BoolAppSettingsEntry("WsSecurityX509CertEndpointEnabled", false, ExTraceGlobals.VerboseTracer).Value;
	}
}
