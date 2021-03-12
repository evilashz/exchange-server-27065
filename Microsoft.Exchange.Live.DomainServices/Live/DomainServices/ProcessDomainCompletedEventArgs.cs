using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200001F RID: 31
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class ProcessDomainCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x00006B4A File Offset: 0x00004D4A
		internal ProcessDomainCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00006B5D File Offset: 0x00004D5D
		public DomainInfoEx Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DomainInfoEx)this.results[0];
			}
		}

		// Token: 0x040000A6 RID: 166
		private object[] results;
	}
}
