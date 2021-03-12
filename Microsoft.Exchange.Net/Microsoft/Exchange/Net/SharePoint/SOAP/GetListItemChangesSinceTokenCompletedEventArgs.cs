using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000944 RID: 2372
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetListItemChangesSinceTokenCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033B9 RID: 13241 RVA: 0x0007F8FA File Offset: 0x0007DAFA
		internal GetListItemChangesSinceTokenCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x060033BA RID: 13242 RVA: 0x0007F90D File Offset: 0x0007DB0D
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C35 RID: 11317
		private object[] results;
	}
}
