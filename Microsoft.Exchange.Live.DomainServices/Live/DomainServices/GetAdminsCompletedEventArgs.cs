using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000050 RID: 80
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetAdminsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600028C RID: 652 RVA: 0x00006DCC File Offset: 0x00004FCC
		internal GetAdminsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00006DDF File Offset: 0x00004FDF
		public Admin[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Admin[])this.results[0];
			}
		}

		// Token: 0x040000B5 RID: 181
		private object[] results;
	}
}
