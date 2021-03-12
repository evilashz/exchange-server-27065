using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004E3 RID: 1251
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class UpdateDelegateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001096 RID: 4246 RVA: 0x000276A6 File Offset: 0x000258A6
		internal UpdateDelegateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x000276B9 File Offset: 0x000258B9
		public UpdateDelegateResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateDelegateResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040017F3 RID: 6131
		private object[] results;
	}
}
