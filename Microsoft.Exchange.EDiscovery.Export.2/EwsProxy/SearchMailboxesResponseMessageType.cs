using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200019C RID: 412
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SearchMailboxesResponseMessageType : ResponseMessageType
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x000243BA File Offset: 0x000225BA
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x000243C2 File Offset: 0x000225C2
		public SearchMailboxesResultType SearchMailboxesResult
		{
			get
			{
				return this.searchMailboxesResultField;
			}
			set
			{
				this.searchMailboxesResultField = value;
			}
		}

		// Token: 0x04000C0A RID: 3082
		private SearchMailboxesResultType searchMailboxesResultField;
	}
}
