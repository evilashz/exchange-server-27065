using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000312 RID: 786
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class MovedCopiedEventType : BaseObjectChangedEventType
	{
		// Token: 0x0400130D RID: 4877
		[XmlElement("OldFolderId", typeof(FolderIdType))]
		[XmlElement("OldItemId", typeof(ItemIdType))]
		public object Item1;

		// Token: 0x0400130E RID: 4878
		public FolderIdType OldParentFolderId;
	}
}
