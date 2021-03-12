using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200046E RID: 1134
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class InstallAppCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001FA3 RID: 8099 RVA: 0x0002B6B2 File Offset: 0x000298B2
		internal InstallAppCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x0002B6C5 File Offset: 0x000298C5
		public InstallAppResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (InstallAppResponseType)this.results[0];
			}
		}

		// Token: 0x040013D7 RID: 5079
		private object[] results;
	}
}
