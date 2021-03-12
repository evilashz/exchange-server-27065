using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200001A RID: 26
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetDomainInfoExCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x00006AFA File Offset: 0x00004CFA
		internal GetDomainInfoExCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00006B0D File Offset: 0x00004D0D
		public DomainInfoEx Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DomainInfoEx)this.results[0];
			}
		}

		// Token: 0x040000A4 RID: 164
		private object[] results;
	}
}
