using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000946 RID: 2374
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	public class UpdateListItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033BF RID: 13247 RVA: 0x0007F922 File Offset: 0x0007DB22
		internal UpdateListItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x0007F935 File Offset: 0x0007DB35
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C36 RID: 11318
		private object[] results;
	}
}
