using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000931 RID: 2353
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DesignerCategory("code")]
	public class GetListCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600337F RID: 13183 RVA: 0x0007F792 File Offset: 0x0007D992
		internal GetListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06003380 RID: 13184 RVA: 0x0007F7A5 File Offset: 0x0007D9A5
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C2C RID: 11308
		private object[] results;
	}
}
