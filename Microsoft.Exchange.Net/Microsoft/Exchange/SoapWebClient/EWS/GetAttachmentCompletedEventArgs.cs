using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004D9 RID: 1241
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetAttachmentCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001078 RID: 4216 RVA: 0x000275DE File Offset: 0x000257DE
		internal GetAttachmentCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x000275F1 File Offset: 0x000257F1
		public GetAttachmentResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetAttachmentResponseType)this.results[0];
			}
		}

		// Token: 0x040017EE RID: 6126
		private object[] results;
	}
}
