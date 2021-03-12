using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200002D RID: 45
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CreateMemberEncryptedCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00006C62 File Offset: 0x00004E62
		internal CreateMemberEncryptedCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00006C75 File Offset: 0x00004E75
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x040000AD RID: 173
		private object[] results;
	}
}
