using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x0200094E RID: 2382
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	public class GetVersionCollectionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033D7 RID: 13271 RVA: 0x0007F9C2 File Offset: 0x0007DBC2
		internal GetVersionCollectionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x060033D8 RID: 13272 RVA: 0x0007F9D5 File Offset: 0x0007DBD5
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C3A RID: 11322
		private object[] results;
	}
}
