using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x0200093E RID: 2366
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class GetListItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033A7 RID: 13223 RVA: 0x0007F882 File Offset: 0x0007DA82
		internal GetListItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060033A8 RID: 13224 RVA: 0x0007F895 File Offset: 0x0007DA95
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C32 RID: 11314
		private object[] results;
	}
}
