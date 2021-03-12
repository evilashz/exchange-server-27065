using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004CD RID: 1229
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class SendItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001054 RID: 4180 RVA: 0x000274EE File Offset: 0x000256EE
		internal SendItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x00027501 File Offset: 0x00025701
		public SendItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SendItemResponseType)this.results[0];
			}
		}

		// Token: 0x040017E8 RID: 6120
		private object[] results;
	}
}
