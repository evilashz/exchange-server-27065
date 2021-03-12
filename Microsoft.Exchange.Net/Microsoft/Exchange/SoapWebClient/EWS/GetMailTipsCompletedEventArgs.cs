using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004F5 RID: 1269
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetMailTipsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010CC RID: 4300 RVA: 0x0002780E File Offset: 0x00025A0E
		internal GetMailTipsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00027821 File Offset: 0x00025A21
		public GetMailTipsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetMailTipsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040017FC RID: 6140
		private object[] results;
	}
}
