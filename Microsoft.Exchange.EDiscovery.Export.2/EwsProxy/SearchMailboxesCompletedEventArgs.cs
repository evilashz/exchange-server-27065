using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000440 RID: 1088
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class SearchMailboxesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F19 RID: 7961 RVA: 0x0002B31A File Offset: 0x0002951A
		internal SearchMailboxesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0002B32D File Offset: 0x0002952D
		public SearchMailboxesResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SearchMailboxesResponseType)this.results[0];
			}
		}

		// Token: 0x040013C0 RID: 5056
		private object[] results;
	}
}
