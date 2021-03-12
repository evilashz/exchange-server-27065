using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x0200094C RID: 2380
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class AddWikiPageCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033D1 RID: 13265 RVA: 0x0007F99A File Offset: 0x0007DB9A
		internal AddWikiPageCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x060033D2 RID: 13266 RVA: 0x0007F9AD File Offset: 0x0007DBAD
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C39 RID: 11321
		private object[] results;
	}
}
