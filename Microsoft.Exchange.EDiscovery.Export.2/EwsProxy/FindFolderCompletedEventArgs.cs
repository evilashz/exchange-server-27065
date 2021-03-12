using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003BA RID: 954
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class FindFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001D87 RID: 7559 RVA: 0x0002A8A2 File Offset: 0x00028AA2
		internal FindFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x0002A8B5 File Offset: 0x00028AB5
		public FindFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (FindFolderResponseType)this.results[0];
			}
		}

		// Token: 0x0400137D RID: 4989
		private object[] results;
	}
}
