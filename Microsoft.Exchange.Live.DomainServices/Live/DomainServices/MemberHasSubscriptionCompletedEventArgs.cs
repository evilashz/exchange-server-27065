using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000044 RID: 68
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class MemberHasSubscriptionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000264 RID: 612 RVA: 0x00006D2C File Offset: 0x00004F2C
		internal MemberHasSubscriptionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00006D3F File Offset: 0x00004F3F
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x040000B1 RID: 177
		private object[] results;
	}
}
