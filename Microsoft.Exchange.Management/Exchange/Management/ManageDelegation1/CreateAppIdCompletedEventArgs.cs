using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Management.ManageDelegation1
{
	// Token: 0x02000DB2 RID: 3506
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class CreateAppIdCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06008630 RID: 34352 RVA: 0x00224FB2 File Offset: 0x002231B2
		internal CreateAppIdCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170029C4 RID: 10692
		// (get) Token: 0x06008631 RID: 34353 RVA: 0x00224FC5 File Offset: 0x002231C5
		public AppIdInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AppIdInfo)this.results[0];
			}
		}

		// Token: 0x04004147 RID: 16711
		private object[] results;
	}
}
