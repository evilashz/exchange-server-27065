using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001DF RID: 479
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	public class BuildRandomAccountCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E74 RID: 3700 RVA: 0x000234C9 File Offset: 0x000216C9
		internal BuildRandomAccountCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x000234DC File Offset: 0x000216DC
		public Account Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Account)this.results[0];
			}
		}

		// Token: 0x040006EA RID: 1770
		private object[] results;
	}
}
