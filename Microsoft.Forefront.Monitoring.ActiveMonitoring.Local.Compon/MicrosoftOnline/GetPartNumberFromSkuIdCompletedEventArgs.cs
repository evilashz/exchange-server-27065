using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001CD RID: 461
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	public class GetPartNumberFromSkuIdCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E3C RID: 3644 RVA: 0x00023389 File Offset: 0x00021589
		internal GetPartNumberFromSkuIdCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0002339C File Offset: 0x0002159C
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x040006E2 RID: 1762
		private object[] results;
	}
}
