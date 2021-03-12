using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x0200094A RID: 2378
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	public class AddDiscussionBoardItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033CB RID: 13259 RVA: 0x0007F972 File Offset: 0x0007DB72
		internal AddDiscussionBoardItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x0007F985 File Offset: 0x0007DB85
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C38 RID: 11320
		private object[] results;
	}
}
