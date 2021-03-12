using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000023 RID: 35
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class MemberNameToNetIdCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00006B9A File Offset: 0x00004D9A
		internal MemberNameToNetIdCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00006BAD File Offset: 0x00004DAD
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x040000A8 RID: 168
		private object[] results;
	}
}
