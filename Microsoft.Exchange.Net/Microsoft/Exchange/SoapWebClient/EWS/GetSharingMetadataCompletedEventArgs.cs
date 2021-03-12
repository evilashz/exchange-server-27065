using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004FD RID: 1277
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetSharingMetadataCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010E4 RID: 4324 RVA: 0x000278AE File Offset: 0x00025AAE
		internal GetSharingMetadataCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x000278C1 File Offset: 0x00025AC1
		public GetSharingMetadataResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetSharingMetadataResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001800 RID: 6144
		private object[] results;
	}
}
