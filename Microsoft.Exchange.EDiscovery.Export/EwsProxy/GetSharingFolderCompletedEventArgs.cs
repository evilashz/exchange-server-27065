using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000420 RID: 1056
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetSharingFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001EB9 RID: 7865 RVA: 0x0002B09A File Offset: 0x0002929A
		internal GetSharingFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x0002B0AD File Offset: 0x000292AD
		public GetSharingFolderResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetSharingFolderResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013B0 RID: 5040
		private object[] results;
	}
}
