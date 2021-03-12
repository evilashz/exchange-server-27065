using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004FB RID: 1275
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class DisconnectPhoneCallCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010DE RID: 4318 RVA: 0x00027886 File Offset: 0x00025A86
		internal DisconnectPhoneCallCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x00027899 File Offset: 0x00025A99
		public DisconnectPhoneCallResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DisconnectPhoneCallResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040017FF RID: 6143
		private object[] results;
	}
}
