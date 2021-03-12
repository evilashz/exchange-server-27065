using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000016 RID: 22
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetDomainAvailabilityCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00006AAA File Offset: 0x00004CAA
		internal GetDomainAvailabilityCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00006ABD File Offset: 0x00004CBD
		public DomainAvailability Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DomainAvailability)this.results[0];
			}
		}

		// Token: 0x040000A2 RID: 162
		private object[] results;
	}
}
