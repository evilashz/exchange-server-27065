using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000942 RID: 2370
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	public class GetListItemChangesWithKnowledgeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033B3 RID: 13235 RVA: 0x0007F8D2 File Offset: 0x0007DAD2
		internal GetListItemChangesWithKnowledgeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x060033B4 RID: 13236 RVA: 0x0007F8E5 File Offset: 0x0007DAE5
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C34 RID: 11316
		private object[] results;
	}
}
