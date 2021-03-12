using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Management.ManageDelegation2
{
	// Token: 0x02000DCA RID: 3530
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetDomainInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06008677 RID: 34423 RVA: 0x00225052 File Offset: 0x00223252
		internal GetDomainInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170029C7 RID: 10695
		// (get) Token: 0x06008678 RID: 34424 RVA: 0x00225065 File Offset: 0x00223265
		public DomainInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DomainInfo)this.results[0];
			}
		}

		// Token: 0x0400415B RID: 16731
		private object[] results;
	}
}
