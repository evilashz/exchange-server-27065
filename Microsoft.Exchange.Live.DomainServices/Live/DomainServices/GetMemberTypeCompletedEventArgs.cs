using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000021 RID: 33
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetMemberTypeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00006B72 File Offset: 0x00004D72
		internal GetMemberTypeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00006B85 File Offset: 0x00004D85
		public MemberType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (MemberType)this.results[0];
			}
		}

		// Token: 0x040000A7 RID: 167
		private object[] results;
	}
}
