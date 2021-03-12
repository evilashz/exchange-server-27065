using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000049 RID: 73
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class MemberHasMailboxCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000274 RID: 628 RVA: 0x00006D7C File Offset: 0x00004F7C
		internal MemberHasMailboxCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00006D8F File Offset: 0x00004F8F
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x040000B3 RID: 179
		private object[] results;
	}
}
