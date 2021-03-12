using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200057D RID: 1405
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class UpdateMailboxAssociationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001264 RID: 4708 RVA: 0x000282AE File Offset: 0x000264AE
		internal UpdateMailboxAssociationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x000282C1 File Offset: 0x000264C1
		public UpdateMailboxAssociationResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateMailboxAssociationResponseType)this.results[0];
			}
		}

		// Token: 0x04001840 RID: 6208
		private object[] results;
	}
}
