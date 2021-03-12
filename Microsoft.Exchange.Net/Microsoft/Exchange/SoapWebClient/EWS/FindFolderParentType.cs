using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000349 RID: 841
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class FindFolderParentType
	{
		// Token: 0x040013F7 RID: 5111
		[XmlArrayItem("CalendarFolder", typeof(CalendarFolderType), IsNullable = false)]
		[XmlArrayItem("Folder", typeof(FolderType), IsNullable = false)]
		[XmlArrayItem("SearchFolder", typeof(SearchFolderType), IsNullable = false)]
		[XmlArrayItem("TasksFolder", typeof(TasksFolderType), IsNullable = false)]
		[XmlArrayItem("ContactsFolder", typeof(ContactsFolderType), IsNullable = false)]
		public BaseFolderType[] Folders;

		// Token: 0x040013F8 RID: 5112
		[XmlAttribute]
		public int IndexedPagingOffset;

		// Token: 0x040013F9 RID: 5113
		[XmlIgnore]
		public bool IndexedPagingOffsetSpecified;

		// Token: 0x040013FA RID: 5114
		[XmlAttribute]
		public int NumeratorOffset;

		// Token: 0x040013FB RID: 5115
		[XmlIgnore]
		public bool NumeratorOffsetSpecified;

		// Token: 0x040013FC RID: 5116
		[XmlAttribute]
		public int AbsoluteDenominator;

		// Token: 0x040013FD RID: 5117
		[XmlIgnore]
		public bool AbsoluteDenominatorSpecified;

		// Token: 0x040013FE RID: 5118
		[XmlAttribute]
		public bool IncludesLastItemInRange;

		// Token: 0x040013FF RID: 5119
		[XmlIgnore]
		public bool IncludesLastItemInRangeSpecified;

		// Token: 0x04001400 RID: 5120
		[XmlAttribute]
		public int TotalItemsInView;

		// Token: 0x04001401 RID: 5121
		[XmlIgnore]
		public bool TotalItemsInViewSpecified;
	}
}
