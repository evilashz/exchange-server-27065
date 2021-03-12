using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000940 RID: 2368
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	public class GetListItemChangesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033AD RID: 13229 RVA: 0x0007F8AA File Offset: 0x0007DAAA
		internal GetListItemChangesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060033AE RID: 13230 RVA: 0x0007F8BD File Offset: 0x0007DABD
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C33 RID: 11315
		private object[] results;
	}
}
