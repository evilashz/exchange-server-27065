using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000470 RID: 1136
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class UninstallAppCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001FA9 RID: 8105 RVA: 0x0002B6DA File Offset: 0x000298DA
		internal UninstallAppCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x0002B6ED File Offset: 0x000298ED
		public UninstallAppResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UninstallAppResponseType)this.results[0];
			}
		}

		// Token: 0x040013D8 RID: 5080
		private object[] results;
	}
}
