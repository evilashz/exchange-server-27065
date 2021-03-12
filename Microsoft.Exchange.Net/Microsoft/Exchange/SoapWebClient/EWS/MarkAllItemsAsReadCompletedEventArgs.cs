using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200052D RID: 1325
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class MarkAllItemsAsReadCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001174 RID: 4468 RVA: 0x00027C6E File Offset: 0x00025E6E
		internal MarkAllItemsAsReadCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x00027C81 File Offset: 0x00025E81
		public MarkAllItemsAsReadResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (MarkAllItemsAsReadResponseType)this.results[0];
			}
		}

		// Token: 0x04001818 RID: 6168
		private object[] results;
	}
}
