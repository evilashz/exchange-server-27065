using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000950 RID: 2384
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class AddAttachmentCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033DD RID: 13277 RVA: 0x0007F9EA File Offset: 0x0007DBEA
		internal AddAttachmentCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x0007F9FD File Offset: 0x0007DBFD
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x04002C3B RID: 11323
		private object[] results;
	}
}
