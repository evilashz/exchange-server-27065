using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001CF RID: 463
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSkuIdFromPartNumberCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E42 RID: 3650 RVA: 0x000233B1 File Offset: 0x000215B1
		internal GetSkuIdFromPartNumberCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x000233C4 File Offset: 0x000215C4
		public Guid Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Guid)this.results[0];
			}
		}

		// Token: 0x040006E3 RID: 1763
		private object[] results;
	}
}
