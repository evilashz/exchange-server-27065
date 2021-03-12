using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004EF RID: 1263
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class GetUserOofSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010BA RID: 4282 RVA: 0x00027796 File Offset: 0x00025996
		internal GetUserOofSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x000277A9 File Offset: 0x000259A9
		public GetUserOofSettingsResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUserOofSettingsResponse)this.results[0];
			}
		}

		// Token: 0x040017F9 RID: 6137
		private object[] results;
	}
}
