using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000959 RID: 2393
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CheckInFileCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033F9 RID: 13305 RVA: 0x0007FA8A File Offset: 0x0007DC8A
		internal CheckInFileCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x060033FA RID: 13306 RVA: 0x0007FA9D File Offset: 0x0007DC9D
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x04002C3F RID: 11327
		private object[] results;
	}
}
