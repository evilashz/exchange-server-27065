using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000577 RID: 1399
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetUMPinCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001252 RID: 4690 RVA: 0x00028236 File Offset: 0x00026436
		internal GetUMPinCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x00028249 File Offset: 0x00026449
		public GetUMPinResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUMPinResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400183D RID: 6205
		private object[] results;
	}
}
