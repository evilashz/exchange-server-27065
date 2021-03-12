using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Management.ManageDelegation2
{
	// Token: 0x02000DC2 RID: 3522
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class CreateAppIdCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06008659 RID: 34393 RVA: 0x0022502A File Offset: 0x0022322A
		internal CreateAppIdCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170029C6 RID: 10694
		// (get) Token: 0x0600865A RID: 34394 RVA: 0x0022503D File Offset: 0x0022323D
		public AppIdInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AppIdInfo)this.results[0];
			}
		}

		// Token: 0x0400415A RID: 16730
		private object[] results;
	}
}
