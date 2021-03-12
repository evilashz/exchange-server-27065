using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000056 RID: 86
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetMaxMembersCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x00006E1C File Offset: 0x0000501C
		internal GetMaxMembersCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00006E2F File Offset: 0x0000502F
		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		// Token: 0x040000B7 RID: 183
		private object[] results;
	}
}
