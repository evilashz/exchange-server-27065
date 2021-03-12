using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000965 RID: 2405
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	public class DeleteContentTypeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600341D RID: 13341 RVA: 0x0007FB7A File Offset: 0x0007DD7A
		internal DeleteContentTypeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x0007FB8D File Offset: 0x0007DD8D
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C45 RID: 11333
		private object[] results;
	}
}
