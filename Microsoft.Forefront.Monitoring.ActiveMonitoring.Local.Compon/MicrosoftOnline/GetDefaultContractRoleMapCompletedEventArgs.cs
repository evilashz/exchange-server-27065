using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001E9 RID: 489
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class GetDefaultContractRoleMapCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E92 RID: 3730 RVA: 0x00023591 File Offset: 0x00021791
		internal GetDefaultContractRoleMapCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x000235A4 File Offset: 0x000217A4
		public RoleMap[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RoleMap[])this.results[0];
			}
		}

		// Token: 0x040006EF RID: 1775
		private object[] results;
	}
}
