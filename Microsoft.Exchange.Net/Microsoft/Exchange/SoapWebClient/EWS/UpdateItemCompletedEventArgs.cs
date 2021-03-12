using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004C9 RID: 1225
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class UpdateItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001048 RID: 4168 RVA: 0x0002749E File Offset: 0x0002569E
		internal UpdateItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x000274B1 File Offset: 0x000256B1
		public UpdateItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateItemResponseType)this.results[0];
			}
		}

		// Token: 0x040017E6 RID: 6118
		private object[] results;
	}
}
