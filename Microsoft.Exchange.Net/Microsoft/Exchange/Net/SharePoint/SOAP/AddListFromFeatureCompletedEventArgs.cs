using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000938 RID: 2360
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class AddListFromFeatureCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003395 RID: 13205 RVA: 0x0007F80A File Offset: 0x0007DA0A
		internal AddListFromFeatureCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x0007F81D File Offset: 0x0007DA1D
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C2F RID: 11311
		private object[] results;
	}
}
