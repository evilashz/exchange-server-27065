using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000018 RID: 24
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetDomainInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060001CE RID: 462 RVA: 0x00006AD2 File Offset: 0x00004CD2
		internal GetDomainInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00006AE5 File Offset: 0x00004CE5
		public DomainInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DomainInfo)this.results[0];
			}
		}

		// Token: 0x040000A3 RID: 163
		private object[] results;
	}
}
