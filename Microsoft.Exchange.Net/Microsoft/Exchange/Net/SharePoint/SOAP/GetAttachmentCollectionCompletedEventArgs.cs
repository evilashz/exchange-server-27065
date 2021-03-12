using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000952 RID: 2386
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	public class GetAttachmentCollectionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033E3 RID: 13283 RVA: 0x0007FA12 File Offset: 0x0007DC12
		internal GetAttachmentCollectionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x060033E4 RID: 13284 RVA: 0x0007FA25 File Offset: 0x0007DC25
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C3C RID: 11324
		private object[] results;
	}
}
