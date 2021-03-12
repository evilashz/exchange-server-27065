using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000955 RID: 2389
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CheckOutFileCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033ED RID: 13293 RVA: 0x0007FA3A File Offset: 0x0007DC3A
		internal CheckOutFileCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x060033EE RID: 13294 RVA: 0x0007FA4D File Offset: 0x0007DC4D
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x04002C3D RID: 11325
		private object[] results;
	}
}
