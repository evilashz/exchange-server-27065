using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x0200093A RID: 2362
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	public class UpdateListCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600339B RID: 13211 RVA: 0x0007F832 File Offset: 0x0007DA32
		internal UpdateListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x0007F845 File Offset: 0x0007DA45
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C30 RID: 11312
		private object[] results;
	}
}
