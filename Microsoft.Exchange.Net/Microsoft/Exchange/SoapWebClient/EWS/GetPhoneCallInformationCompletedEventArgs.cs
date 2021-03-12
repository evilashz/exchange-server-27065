using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004F9 RID: 1273
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetPhoneCallInformationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010D8 RID: 4312 RVA: 0x0002785E File Offset: 0x00025A5E
		internal GetPhoneCallInformationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00027871 File Offset: 0x00025A71
		public GetPhoneCallInformationResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetPhoneCallInformationResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040017FE RID: 6142
		private object[] results;
	}
}
