using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200001C RID: 28
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ReserveDomainCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060001DA RID: 474 RVA: 0x00006B22 File Offset: 0x00004D22
		internal ReserveDomainCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00006B35 File Offset: 0x00004D35
		public DomainInfoEx Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DomainInfoEx)this.results[0];
			}
		}

		// Token: 0x040000A5 RID: 165
		private object[] results;
	}
}
