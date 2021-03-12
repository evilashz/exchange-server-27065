using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x0200095D RID: 2397
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DesignerCategory("code")]
	public class GetListContentTypesAndPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003405 RID: 13317 RVA: 0x0007FADA File Offset: 0x0007DCDA
		internal GetListContentTypesAndPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06003406 RID: 13318 RVA: 0x0007FAED File Offset: 0x0007DCED
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C41 RID: 11329
		private object[] results;
	}
}
