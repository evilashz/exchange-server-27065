using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001EB RID: 491
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	public class ForceTransitiveReplicationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E98 RID: 3736 RVA: 0x000235B9 File Offset: 0x000217B9
		internal ForceTransitiveReplicationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x000235CC File Offset: 0x000217CC
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x040006F0 RID: 1776
		private object[] results;
	}
}
