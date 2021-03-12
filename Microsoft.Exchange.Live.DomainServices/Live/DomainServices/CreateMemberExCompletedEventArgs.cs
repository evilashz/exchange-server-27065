using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200002F RID: 47
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class CreateMemberExCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000214 RID: 532 RVA: 0x00006C8A File Offset: 0x00004E8A
		internal CreateMemberExCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00006C9D File Offset: 0x00004E9D
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00006CB2 File Offset: 0x00004EB2
		public string slt
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x040000AE RID: 174
		private object[] results;
	}
}
