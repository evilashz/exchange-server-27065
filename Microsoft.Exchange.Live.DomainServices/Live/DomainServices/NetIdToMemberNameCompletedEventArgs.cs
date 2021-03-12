using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000025 RID: 37
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class NetIdToMemberNameCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00006BC2 File Offset: 0x00004DC2
		internal NetIdToMemberNameCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00006BD5 File Offset: 0x00004DD5
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x040000A9 RID: 169
		private object[] results;
	}
}
