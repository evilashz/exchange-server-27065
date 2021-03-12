using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000967 RID: 2407
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DesignerCategory("code")]
	public class UpdateContentTypeXmlDocumentCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003423 RID: 13347 RVA: 0x0007FBA2 File Offset: 0x0007DDA2
		internal UpdateContentTypeXmlDocumentCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x0007FBB5 File Offset: 0x0007DDB5
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C46 RID: 11334
		private object[] results;
	}
}
