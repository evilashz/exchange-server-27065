using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200040E RID: 1038
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetUserOofSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E83 RID: 7811 RVA: 0x0002AF32 File Offset: 0x00029132
		internal GetUserOofSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06001E84 RID: 7812 RVA: 0x0002AF45 File Offset: 0x00029145
		public GetUserOofSettingsResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUserOofSettingsResponse)this.results[0];
			}
		}

		// Token: 0x040013A7 RID: 5031
		private object[] results;
	}
}
