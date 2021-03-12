using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000027 RID: 39
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetCountMembersCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060001FC RID: 508 RVA: 0x00006BEA File Offset: 0x00004DEA
		internal GetCountMembersCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00006BFD File Offset: 0x00004DFD
		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		// Token: 0x040000AA RID: 170
		private object[] results;
	}
}
