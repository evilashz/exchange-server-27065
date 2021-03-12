using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x0200095B RID: 2395
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DesignerCategory("code")]
	public class GetListContentTypesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033FF RID: 13311 RVA: 0x0007FAB2 File Offset: 0x0007DCB2
		internal GetListContentTypesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06003400 RID: 13312 RVA: 0x0007FAC5 File Offset: 0x0007DCC5
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C40 RID: 11328
		private object[] results;
	}
}
