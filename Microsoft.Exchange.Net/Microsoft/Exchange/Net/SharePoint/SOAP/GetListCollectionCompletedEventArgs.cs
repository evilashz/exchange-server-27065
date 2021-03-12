using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x0200093C RID: 2364
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	public class GetListCollectionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033A1 RID: 13217 RVA: 0x0007F85A File Offset: 0x0007DA5A
		internal GetListCollectionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x060033A2 RID: 13218 RVA: 0x0007F86D File Offset: 0x0007DA6D
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C31 RID: 11313
		private object[] results;
	}
}
