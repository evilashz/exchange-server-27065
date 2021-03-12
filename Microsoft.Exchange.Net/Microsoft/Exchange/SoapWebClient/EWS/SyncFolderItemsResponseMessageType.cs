using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002DB RID: 731
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SyncFolderItemsResponseMessageType : ResponseMessageType
	{
		// Token: 0x04001259 RID: 4697
		public string SyncState;

		// Token: 0x0400125A RID: 4698
		public bool IncludesLastItemInRange;

		// Token: 0x0400125B RID: 4699
		[XmlIgnore]
		public bool IncludesLastItemInRangeSpecified;

		// Token: 0x0400125C RID: 4700
		public SyncFolderItemsChangesType Changes;
	}
}
