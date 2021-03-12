using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003BE RID: 958
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001D93 RID: 7571 RVA: 0x0002A8F2 File Offset: 0x00028AF2
		internal GetFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x0002A905 File Offset: 0x00028B05
		public GetFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetFolderResponseType)this.results[0];
			}
		}

		// Token: 0x0400137F RID: 4991
		private object[] results;
	}
}
