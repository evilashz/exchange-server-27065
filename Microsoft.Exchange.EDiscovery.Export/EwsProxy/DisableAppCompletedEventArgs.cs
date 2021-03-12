using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000472 RID: 1138
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class DisableAppCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001FAF RID: 8111 RVA: 0x0002B702 File Offset: 0x00029902
		internal DisableAppCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x0002B715 File Offset: 0x00029915
		public DisableAppResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DisableAppResponseType)this.results[0];
			}
		}

		// Token: 0x040013D9 RID: 5081
		private object[] results;
	}
}
