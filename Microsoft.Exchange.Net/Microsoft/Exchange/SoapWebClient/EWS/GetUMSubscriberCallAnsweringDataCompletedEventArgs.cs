using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200057B RID: 1403
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetUMSubscriberCallAnsweringDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x00028286 File Offset: 0x00026486
		internal GetUMSubscriberCallAnsweringDataCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00028299 File Offset: 0x00026499
		public GetUMSubscriberCallAnsweringDataResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUMSubscriberCallAnsweringDataResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400183F RID: 6207
		private object[] results;
	}
}
