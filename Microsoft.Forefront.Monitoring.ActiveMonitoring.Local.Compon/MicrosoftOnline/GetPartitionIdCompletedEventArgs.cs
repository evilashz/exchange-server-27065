using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001CB RID: 459
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	public class GetPartitionIdCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E36 RID: 3638 RVA: 0x00023361 File Offset: 0x00021561
		internal GetPartitionIdCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x00023374 File Offset: 0x00021574
		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		// Token: 0x040006E1 RID: 1761
		private object[] results;
	}
}
