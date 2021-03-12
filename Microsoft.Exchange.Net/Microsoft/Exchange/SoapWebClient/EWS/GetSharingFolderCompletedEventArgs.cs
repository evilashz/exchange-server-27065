using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000501 RID: 1281
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSharingFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010F0 RID: 4336 RVA: 0x000278FE File Offset: 0x00025AFE
		internal GetSharingFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00027911 File Offset: 0x00025B11
		public GetSharingFolderResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetSharingFolderResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001802 RID: 6146
		private object[] results;
	}
}
