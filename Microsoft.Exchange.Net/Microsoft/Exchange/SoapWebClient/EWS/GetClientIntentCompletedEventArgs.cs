using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000579 RID: 1401
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetClientIntentCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001258 RID: 4696 RVA: 0x0002825E File Offset: 0x0002645E
		internal GetClientIntentCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00028271 File Offset: 0x00026471
		public GetClientIntentResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetClientIntentResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400183E RID: 6206
		private object[] results;
	}
}
