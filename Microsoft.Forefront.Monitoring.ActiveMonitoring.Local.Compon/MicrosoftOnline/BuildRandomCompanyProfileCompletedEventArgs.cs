using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001E3 RID: 483
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	public class BuildRandomCompanyProfileCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E80 RID: 3712 RVA: 0x00023519 File Offset: 0x00021719
		internal BuildRandomCompanyProfileCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0002352C File Offset: 0x0002172C
		public CompanyProfile Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CompanyProfile)this.results[0];
			}
		}

		// Token: 0x040006EC RID: 1772
		private object[] results;
	}
}
