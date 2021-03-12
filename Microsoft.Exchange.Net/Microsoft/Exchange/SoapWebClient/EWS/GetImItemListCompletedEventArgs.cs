using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200053F RID: 1343
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetImItemListCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011AA RID: 4522 RVA: 0x00027DD6 File Offset: 0x00025FD6
		internal GetImItemListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00027DE9 File Offset: 0x00025FE9
		public GetImItemListResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetImItemListResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001821 RID: 6177
		private object[] results;
	}
}
