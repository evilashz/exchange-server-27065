using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003D2 RID: 978
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CopyFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DCF RID: 7631 RVA: 0x0002AA82 File Offset: 0x00028C82
		internal CopyFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x0002AA95 File Offset: 0x00028C95
		public CopyFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CopyFolderResponseType)this.results[0];
			}
		}

		// Token: 0x04001389 RID: 5001
		private object[] results;
	}
}
