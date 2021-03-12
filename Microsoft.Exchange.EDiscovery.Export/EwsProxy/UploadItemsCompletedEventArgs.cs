using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003C0 RID: 960
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class UploadItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001D99 RID: 7577 RVA: 0x0002A91A File Offset: 0x00028B1A
		internal UploadItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0002A92D File Offset: 0x00028B2D
		public UploadItemsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UploadItemsResponseType)this.results[0];
			}
		}

		// Token: 0x04001380 RID: 4992
		private object[] results;
	}
}
