using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Management.ManageDelegation1
{
	// Token: 0x02000DBA RID: 3514
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetDomainInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600864E RID: 34382 RVA: 0x00224FDA File Offset: 0x002231DA
		internal GetDomainInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170029C5 RID: 10693
		// (get) Token: 0x0600864F RID: 34383 RVA: 0x00224FED File Offset: 0x002231ED
		public DomainInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DomainInfo)this.results[0];
			}
		}

		// Token: 0x04004148 RID: 16712
		private object[] results;
	}
}
