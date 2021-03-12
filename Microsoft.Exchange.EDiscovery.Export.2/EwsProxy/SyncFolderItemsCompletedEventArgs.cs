using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003DE RID: 990
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class SyncFolderItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DF3 RID: 7667 RVA: 0x0002AB72 File Offset: 0x00028D72
		internal SyncFolderItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x0002AB85 File Offset: 0x00028D85
		public SyncFolderItemsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SyncFolderItemsResponseType)this.results[0];
			}
		}

		// Token: 0x0400138F RID: 5007
		private object[] results;
	}
}
