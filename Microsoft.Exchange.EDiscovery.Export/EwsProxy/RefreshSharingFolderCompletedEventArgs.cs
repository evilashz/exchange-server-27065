using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200041E RID: 1054
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class RefreshSharingFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001EB3 RID: 7859 RVA: 0x0002B072 File Offset: 0x00029272
		internal RefreshSharingFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x0002B085 File Offset: 0x00029285
		public RefreshSharingFolderResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RefreshSharingFolderResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013AF RID: 5039
		private object[] results;
	}
}
