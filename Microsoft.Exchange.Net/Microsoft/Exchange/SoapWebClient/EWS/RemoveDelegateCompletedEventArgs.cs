using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004E1 RID: 1249
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class RemoveDelegateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001090 RID: 4240 RVA: 0x0002767E File Offset: 0x0002587E
		internal RemoveDelegateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x00027691 File Offset: 0x00025891
		public RemoveDelegateResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RemoveDelegateResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040017F2 RID: 6130
		private object[] results;
	}
}
