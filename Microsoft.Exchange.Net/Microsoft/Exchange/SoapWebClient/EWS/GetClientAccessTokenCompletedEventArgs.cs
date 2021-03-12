using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004DB RID: 1243
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class GetClientAccessTokenCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600107E RID: 4222 RVA: 0x00027606 File Offset: 0x00025806
		internal GetClientAccessTokenCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00027619 File Offset: 0x00025819
		public GetClientAccessTokenResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetClientAccessTokenResponseType)this.results[0];
			}
		}

		// Token: 0x040017EF RID: 6127
		private object[] results;
	}
}
