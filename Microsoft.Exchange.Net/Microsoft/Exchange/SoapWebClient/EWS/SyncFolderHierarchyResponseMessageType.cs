using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002E1 RID: 737
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SyncFolderHierarchyResponseMessageType : ResponseMessageType
	{
		// Token: 0x04001268 RID: 4712
		public string SyncState;

		// Token: 0x04001269 RID: 4713
		public bool IncludesLastFolderInRange;

		// Token: 0x0400126A RID: 4714
		[XmlIgnore]
		public bool IncludesLastFolderInRangeSpecified;

		// Token: 0x0400126B RID: 4715
		public SyncFolderHierarchyChangesType Changes;
	}
}
