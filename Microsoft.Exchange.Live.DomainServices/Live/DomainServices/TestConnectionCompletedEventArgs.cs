using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000014 RID: 20
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class TestConnectionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00006A82 File Offset: 0x00004C82
		internal TestConnectionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00006A95 File Offset: 0x00004C95
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x040000A1 RID: 161
		private object[] results;
	}
}
