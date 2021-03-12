using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003DC RID: 988
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class SyncFolderHierarchyCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DED RID: 7661 RVA: 0x0002AB4A File Offset: 0x00028D4A
		internal SyncFolderHierarchyCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x0002AB5D File Offset: 0x00028D5D
		public SyncFolderHierarchyResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SyncFolderHierarchyResponseType)this.results[0];
			}
		}

		// Token: 0x0400138E RID: 5006
		private object[] results;
	}
}
