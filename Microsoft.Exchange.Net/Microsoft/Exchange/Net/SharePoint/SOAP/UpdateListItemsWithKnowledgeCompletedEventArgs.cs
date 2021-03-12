using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000948 RID: 2376
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class UpdateListItemsWithKnowledgeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033C5 RID: 13253 RVA: 0x0007F94A File Offset: 0x0007DB4A
		internal UpdateListItemsWithKnowledgeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x0007F95D File Offset: 0x0007DB5D
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C37 RID: 11319
		private object[] results;
	}
}
