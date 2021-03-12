using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003CA RID: 970
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class DeleteFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DB7 RID: 7607 RVA: 0x0002A9E2 File Offset: 0x00028BE2
		internal DeleteFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x0002A9F5 File Offset: 0x00028BF5
		public DeleteFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DeleteFolderResponseType)this.results[0];
			}
		}

		// Token: 0x04001385 RID: 4997
		private object[] results;
	}
}
