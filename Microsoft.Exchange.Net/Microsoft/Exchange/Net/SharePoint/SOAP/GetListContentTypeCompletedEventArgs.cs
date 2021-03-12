using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x0200095F RID: 2399
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetListContentTypeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600340B RID: 13323 RVA: 0x0007FB02 File Offset: 0x0007DD02
		internal GetListContentTypeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x0600340C RID: 13324 RVA: 0x0007FB15 File Offset: 0x0007DD15
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C42 RID: 11330
		private object[] results;
	}
}
