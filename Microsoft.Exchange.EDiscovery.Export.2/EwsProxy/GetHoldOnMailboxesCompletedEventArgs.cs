using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000444 RID: 1092
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetHoldOnMailboxesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F25 RID: 7973 RVA: 0x0002B36A File Offset: 0x0002956A
		internal GetHoldOnMailboxesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x0002B37D File Offset: 0x0002957D
		public GetHoldOnMailboxesResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetHoldOnMailboxesResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013C2 RID: 5058
		private object[] results;
	}
}
