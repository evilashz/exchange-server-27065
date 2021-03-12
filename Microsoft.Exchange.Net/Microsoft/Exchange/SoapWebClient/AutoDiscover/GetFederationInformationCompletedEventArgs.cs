using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000134 RID: 308
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetFederationInformationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x00018557 File Offset: 0x00016757
		internal GetFederationInformationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001856A File Offset: 0x0001676A
		public GetFederationInformationResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetFederationInformationResponse)this.results[0];
			}
		}

		// Token: 0x040005F0 RID: 1520
		private object[] results;
	}
}
