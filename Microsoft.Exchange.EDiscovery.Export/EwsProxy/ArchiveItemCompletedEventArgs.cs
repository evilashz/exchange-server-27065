using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003F2 RID: 1010
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class ArchiveItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E2F RID: 7727 RVA: 0x0002AD02 File Offset: 0x00028F02
		internal ArchiveItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x0002AD15 File Offset: 0x00028F15
		public ArchiveItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ArchiveItemResponseType)this.results[0];
			}
		}

		// Token: 0x04001399 RID: 5017
		private object[] results;
	}
}
