using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004F7 RID: 1271
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class PlayOnPhoneCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010D2 RID: 4306 RVA: 0x00027836 File Offset: 0x00025A36
		internal PlayOnPhoneCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00027849 File Offset: 0x00025A49
		public PlayOnPhoneResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (PlayOnPhoneResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040017FD RID: 6141
		private object[] results;
	}
}
