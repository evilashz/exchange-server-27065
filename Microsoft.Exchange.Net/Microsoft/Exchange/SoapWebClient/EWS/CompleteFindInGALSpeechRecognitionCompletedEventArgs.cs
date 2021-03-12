using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000567 RID: 1383
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class CompleteFindInGALSpeechRecognitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001222 RID: 4642 RVA: 0x000280F6 File Offset: 0x000262F6
		internal CompleteFindInGALSpeechRecognitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x00028109 File Offset: 0x00026309
		public CompleteFindInGALSpeechRecognitionResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CompleteFindInGALSpeechRecognitionResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001835 RID: 6197
		private object[] results;
	}
}
