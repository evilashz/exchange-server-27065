using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200043E RID: 1086
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class GetSearchableMailboxesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F13 RID: 7955 RVA: 0x0002B2F2 File Offset: 0x000294F2
		internal GetSearchableMailboxesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x0002B305 File Offset: 0x00029505
		public GetSearchableMailboxesResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetSearchableMailboxesResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013BF RID: 5055
		private object[] results;
	}
}
