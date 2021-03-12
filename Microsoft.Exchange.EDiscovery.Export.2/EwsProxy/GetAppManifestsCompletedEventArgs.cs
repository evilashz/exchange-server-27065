using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000450 RID: 1104
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class GetAppManifestsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F49 RID: 8009 RVA: 0x0002B45A File Offset: 0x0002965A
		internal GetAppManifestsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x0002B46D File Offset: 0x0002966D
		public GetAppManifestsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetAppManifestsResponseType)this.results[0];
			}
		}

		// Token: 0x040013C8 RID: 5064
		private object[] results;
	}
}
