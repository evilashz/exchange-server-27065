using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001D5 RID: 469
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	public class GetCompanyAssignedPlansCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E54 RID: 3668 RVA: 0x00023429 File Offset: 0x00021629
		internal GetCompanyAssignedPlansCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0002343C File Offset: 0x0002163C
		public AssignedPlanValue[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AssignedPlanValue[])this.results[0];
			}
		}

		// Token: 0x040006E6 RID: 1766
		private object[] results;
	}
}
