using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200002B RID: 43
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class CreateMemberCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000208 RID: 520 RVA: 0x00006C3A File Offset: 0x00004E3A
		internal CreateMemberCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00006C4D File Offset: 0x00004E4D
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x040000AC RID: 172
		private object[] results;
	}
}
