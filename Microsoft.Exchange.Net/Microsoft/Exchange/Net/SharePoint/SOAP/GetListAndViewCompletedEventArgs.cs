using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000933 RID: 2355
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DesignerCategory("code")]
	public class GetListAndViewCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003385 RID: 13189 RVA: 0x0007F7BA File Offset: 0x0007D9BA
		internal GetListAndViewCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x0007F7CD File Offset: 0x0007D9CD
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C2D RID: 11309
		private object[] results;
	}
}
