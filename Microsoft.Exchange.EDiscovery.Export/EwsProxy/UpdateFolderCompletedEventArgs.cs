using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003CE RID: 974
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class UpdateFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DC3 RID: 7619 RVA: 0x0002AA32 File Offset: 0x00028C32
		internal UpdateFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0002AA45 File Offset: 0x00028C45
		public UpdateFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateFolderResponseType)this.results[0];
			}
		}

		// Token: 0x04001387 RID: 4999
		private object[] results;
	}
}
