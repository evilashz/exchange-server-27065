using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200013A RID: 314
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	public class GetMailTipsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000888 RID: 2184 RVA: 0x000251AD File Offset: 0x000233AD
		internal GetMailTipsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x000251C0 File Offset: 0x000233C0
		public GetMailTipsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetMailTipsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040006AD RID: 1709
		private object[] results;
	}
}
