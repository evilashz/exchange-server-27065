using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000969 RID: 2409
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class UpdateContentTypesXmlDocumentCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003429 RID: 13353 RVA: 0x0007FBCA File Offset: 0x0007DDCA
		internal UpdateContentTypesXmlDocumentCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x0007FBDD File Offset: 0x0007DDDD
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C47 RID: 11335
		private object[] results;
	}
}
