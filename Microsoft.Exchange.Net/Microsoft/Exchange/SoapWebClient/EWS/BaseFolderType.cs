using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002E4 RID: 740
	[DebuggerStepThrough]
	[XmlInclude(typeof(ContactsFolderType))]
	[XmlInclude(typeof(CalendarFolderType))]
	[XmlInclude(typeof(FolderType))]
	[XmlInclude(typeof(TasksFolderType))]
	[XmlInclude(typeof(SearchFolderType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public abstract class BaseFolderType
	{
		// Token: 0x0400126F RID: 4719
		public FolderIdType FolderId;

		// Token: 0x04001270 RID: 4720
		public FolderIdType ParentFolderId;

		// Token: 0x04001271 RID: 4721
		public string FolderClass;

		// Token: 0x04001272 RID: 4722
		public string DisplayName;

		// Token: 0x04001273 RID: 4723
		public int TotalCount;

		// Token: 0x04001274 RID: 4724
		[XmlIgnore]
		public bool TotalCountSpecified;

		// Token: 0x04001275 RID: 4725
		public int ChildFolderCount;

		// Token: 0x04001276 RID: 4726
		[XmlIgnore]
		public bool ChildFolderCountSpecified;

		// Token: 0x04001277 RID: 4727
		[XmlElement("ExtendedProperty")]
		public ExtendedPropertyType[] ExtendedProperty;

		// Token: 0x04001278 RID: 4728
		public ManagedFolderInformationType ManagedFolderInformation;

		// Token: 0x04001279 RID: 4729
		public EffectiveRightsType EffectiveRights;

		// Token: 0x0400127A RID: 4730
		public DistinguishedFolderIdNameType DistinguishedFolderId;

		// Token: 0x0400127B RID: 4731
		[XmlIgnore]
		public bool DistinguishedFolderIdSpecified;

		// Token: 0x0400127C RID: 4732
		public RetentionTagType PolicyTag;

		// Token: 0x0400127D RID: 4733
		public RetentionTagType ArchiveTag;
	}
}
