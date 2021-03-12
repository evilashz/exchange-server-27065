using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000961 RID: 2401
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	public class CreateContentTypeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003411 RID: 13329 RVA: 0x0007FB2A File Offset: 0x0007DD2A
		internal CreateContentTypeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x0007FB3D File Offset: 0x0007DD3D
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x04002C43 RID: 11331
		private object[] results;
	}
}
