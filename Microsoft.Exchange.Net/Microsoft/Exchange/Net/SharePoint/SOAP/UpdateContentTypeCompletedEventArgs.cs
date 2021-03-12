using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000963 RID: 2403
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	public class UpdateContentTypeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003417 RID: 13335 RVA: 0x0007FB52 File Offset: 0x0007DD52
		internal UpdateContentTypeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06003418 RID: 13336 RVA: 0x0007FB65 File Offset: 0x0007DD65
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C44 RID: 11332
		private object[] results;
	}
}
