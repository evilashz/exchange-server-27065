using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200055D RID: 1373
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetUMPromptCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001204 RID: 4612 RVA: 0x0002802E File Offset: 0x0002622E
		internal GetUMPromptCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x00028041 File Offset: 0x00026241
		public GetUMPromptResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUMPromptResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001830 RID: 6192
		private object[] results;
	}
}
