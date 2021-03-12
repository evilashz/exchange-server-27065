using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001DB RID: 475
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	public class GetCompanyForeignPrincipalObjectsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E66 RID: 3686 RVA: 0x000234A1 File Offset: 0x000216A1
		internal GetCompanyForeignPrincipalObjectsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x000234B4 File Offset: 0x000216B4
		public ForeignPrincipal[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ForeignPrincipal[])this.results[0];
			}
		}

		// Token: 0x040006E9 RID: 1769
		private object[] results;
	}
}
