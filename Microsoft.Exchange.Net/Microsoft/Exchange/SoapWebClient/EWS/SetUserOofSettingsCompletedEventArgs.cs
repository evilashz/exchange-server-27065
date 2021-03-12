using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004F1 RID: 1265
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class SetUserOofSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010C0 RID: 4288 RVA: 0x000277BE File Offset: 0x000259BE
		internal SetUserOofSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x000277D1 File Offset: 0x000259D1
		public SetUserOofSettingsResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SetUserOofSettingsResponse)this.results[0];
			}
		}

		// Token: 0x040017FA RID: 6138
		private object[] results;
	}
}
