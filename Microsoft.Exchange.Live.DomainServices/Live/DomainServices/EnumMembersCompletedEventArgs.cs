using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000029 RID: 41
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class EnumMembersCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000202 RID: 514 RVA: 0x00006C12 File Offset: 0x00004E12
		internal EnumMembersCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00006C25 File Offset: 0x00004E25
		public Member[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Member[])this.results[0];
			}
		}

		// Token: 0x040000AB RID: 171
		private object[] results;
	}
}
