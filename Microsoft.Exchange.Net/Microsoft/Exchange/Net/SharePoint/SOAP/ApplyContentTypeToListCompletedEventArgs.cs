using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x0200096D RID: 2413
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class ApplyContentTypeToListCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003435 RID: 13365 RVA: 0x0007FC1A File Offset: 0x0007DE1A
		internal ApplyContentTypeToListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x0007FC2D File Offset: 0x0007DE2D
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002C49 RID: 11337
		private object[] results;
	}
}
