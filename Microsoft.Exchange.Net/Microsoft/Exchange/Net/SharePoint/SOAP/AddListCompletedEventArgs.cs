using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000936 RID: 2358
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	public class AddListCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600338F RID: 13199 RVA: 0x0007F7E2 File Offset: 0x0007D9E2
		internal AddListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06003390 RID: 13200 RVA: 0x0007F7F5 File Offset: 0x0007D9F5
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C2E RID: 11310
		private object[] results;
	}
}
