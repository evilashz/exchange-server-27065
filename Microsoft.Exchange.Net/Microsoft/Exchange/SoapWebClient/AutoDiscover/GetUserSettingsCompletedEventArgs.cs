using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000130 RID: 304
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetUserSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x00018507 File Offset: 0x00016707
		internal GetUserSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001851A File Offset: 0x0001671A
		public GetUserSettingsResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUserSettingsResponse)this.results[0];
			}
		}

		// Token: 0x040005EE RID: 1518
		private object[] results;
	}
}
