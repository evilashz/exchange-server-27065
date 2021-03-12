using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000047 RID: 71
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetMxRecordsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600026E RID: 622 RVA: 0x00006D54 File Offset: 0x00004F54
		internal GetMxRecordsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00006D67 File Offset: 0x00004F67
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x040000B2 RID: 178
		private object[] results;
	}
}
