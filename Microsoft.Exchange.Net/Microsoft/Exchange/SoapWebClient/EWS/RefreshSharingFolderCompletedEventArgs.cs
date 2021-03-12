using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004FF RID: 1279
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class RefreshSharingFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010EA RID: 4330 RVA: 0x000278D6 File Offset: 0x00025AD6
		internal RefreshSharingFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x000278E9 File Offset: 0x00025AE9
		public RefreshSharingFolderResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RefreshSharingFolderResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001801 RID: 6145
		private object[] results;
	}
}
