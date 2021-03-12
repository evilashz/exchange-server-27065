using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001E5 RID: 485
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	public class BuildRandomUserCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E86 RID: 3718 RVA: 0x00023541 File Offset: 0x00021741
		internal BuildRandomUserCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00023554 File Offset: 0x00021754
		public User Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (User)this.results[0];
			}
		}

		// Token: 0x040006ED RID: 1773
		private object[] results;
	}
}
