using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000465 RID: 1125
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class FindPeopleType : BaseRequestType
	{
		// Token: 0x0400172A RID: 5930
		public PersonaResponseShapeType PersonaShape;

		// Token: 0x0400172B RID: 5931
		public IndexedPageViewType IndexedPageItemView;

		// Token: 0x0400172C RID: 5932
		public RestrictionType Restriction;

		// Token: 0x0400172D RID: 5933
		public RestrictionType AggregationRestriction;

		// Token: 0x0400172E RID: 5934
		[XmlArrayItem("FieldOrder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public FieldOrderType[] SortOrder;

		// Token: 0x0400172F RID: 5935
		public TargetFolderIdType ParentFolderId;

		// Token: 0x04001730 RID: 5936
		public string QueryString;
	}
}
