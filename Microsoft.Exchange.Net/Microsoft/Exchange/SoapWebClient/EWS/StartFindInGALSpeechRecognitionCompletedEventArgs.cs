using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000565 RID: 1381
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class StartFindInGALSpeechRecognitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600121C RID: 4636 RVA: 0x000280CE File Offset: 0x000262CE
		internal StartFindInGALSpeechRecognitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x000280E1 File Offset: 0x000262E1
		public StartFindInGALSpeechRecognitionResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (StartFindInGALSpeechRecognitionResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001834 RID: 6196
		private object[] results;
	}
}
