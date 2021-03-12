using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x0200096B RID: 2411
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class DeleteContentTypeXmlDocumentCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600342F RID: 13359 RVA: 0x0007FBF2 File Offset: 0x0007DDF2
		internal DeleteContentTypeXmlDocumentCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06003430 RID: 13360 RVA: 0x0007FC05 File Offset: 0x0007DE05
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C48 RID: 11336
		private object[] results;
	}
}
