using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Server
{
	// Token: 0x020009F2 RID: 2546
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	public class GetServerInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003784 RID: 14212 RVA: 0x0008CD93 File Offset: 0x0008AF93
		internal GetServerInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003785 RID: 14213 RVA: 0x0008CDA6 File Offset: 0x0008AFA6
		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		// Token: 0x04002F27 RID: 12071
		private object[] results;
	}
}
