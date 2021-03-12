using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000531 RID: 1329
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetAppManifestsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001180 RID: 4480 RVA: 0x00027CBE File Offset: 0x00025EBE
		internal GetAppManifestsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x00027CD1 File Offset: 0x00025ED1
		public GetAppManifestsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetAppManifestsResponseType)this.results[0];
			}
		}

		// Token: 0x0400181A RID: 6170
		private object[] results;
	}
}
