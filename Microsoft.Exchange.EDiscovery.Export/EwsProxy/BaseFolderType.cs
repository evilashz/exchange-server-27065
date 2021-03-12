using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000203 RID: 515
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(FolderType))]
	[XmlInclude(typeof(ContactsFolderType))]
	[XmlInclude(typeof(CalendarFolderType))]
	[XmlInclude(typeof(SearchFolderType))]
	[DebuggerStepThrough]
	[XmlInclude(typeof(TasksFolderType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public abstract class BaseFolderType
	{
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x00025CC2 File Offset: 0x00023EC2
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x00025CCA File Offset: 0x00023ECA
		public FolderIdType FolderId
		{
			get
			{
				return this.folderIdField;
			}
			set
			{
				this.folderIdField = value;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x00025CD3 File Offset: 0x00023ED3
		// (set) Token: 0x06001481 RID: 5249 RVA: 0x00025CDB File Offset: 0x00023EDB
		public FolderIdType ParentFolderId
		{
			get
			{
				return this.parentFolderIdField;
			}
			set
			{
				this.parentFolderIdField = value;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x00025CE4 File Offset: 0x00023EE4
		// (set) Token: 0x06001483 RID: 5251 RVA: 0x00025CEC File Offset: 0x00023EEC
		public string FolderClass
		{
			get
			{
				return this.folderClassField;
			}
			set
			{
				this.folderClassField = value;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x00025CF5 File Offset: 0x00023EF5
		// (set) Token: 0x06001485 RID: 5253 RVA: 0x00025CFD File Offset: 0x00023EFD
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00025D06 File Offset: 0x00023F06
		// (set) Token: 0x06001487 RID: 5255 RVA: 0x00025D0E File Offset: 0x00023F0E
		public int TotalCount
		{
			get
			{
				return this.totalCountField;
			}
			set
			{
				this.totalCountField = value;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x00025D17 File Offset: 0x00023F17
		// (set) Token: 0x06001489 RID: 5257 RVA: 0x00025D1F File Offset: 0x00023F1F
		[XmlIgnore]
		public bool TotalCountSpecified
		{
			get
			{
				return this.totalCountFieldSpecified;
			}
			set
			{
				this.totalCountFieldSpecified = value;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00025D28 File Offset: 0x00023F28
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x00025D30 File Offset: 0x00023F30
		public int ChildFolderCount
		{
			get
			{
				return this.childFolderCountField;
			}
			set
			{
				this.childFolderCountField = value;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00025D39 File Offset: 0x00023F39
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x00025D41 File Offset: 0x00023F41
		[XmlIgnore]
		public bool ChildFolderCountSpecified
		{
			get
			{
				return this.childFolderCountFieldSpecified;
			}
			set
			{
				this.childFolderCountFieldSpecified = value;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00025D4A File Offset: 0x00023F4A
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x00025D52 File Offset: 0x00023F52
		[XmlElement("ExtendedProperty")]
		public ExtendedPropertyType[] ExtendedProperty
		{
			get
			{
				return this.extendedPropertyField;
			}
			set
			{
				this.extendedPropertyField = value;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00025D5B File Offset: 0x00023F5B
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x00025D63 File Offset: 0x00023F63
		public ManagedFolderInformationType ManagedFolderInformation
		{
			get
			{
				return this.managedFolderInformationField;
			}
			set
			{
				this.managedFolderInformationField = value;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00025D6C File Offset: 0x00023F6C
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x00025D74 File Offset: 0x00023F74
		public EffectiveRightsType EffectiveRights
		{
			get
			{
				return this.effectiveRightsField;
			}
			set
			{
				this.effectiveRightsField = value;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00025D7D File Offset: 0x00023F7D
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x00025D85 File Offset: 0x00023F85
		public DistinguishedFolderIdNameType DistinguishedFolderId
		{
			get
			{
				return this.distinguishedFolderIdField;
			}
			set
			{
				this.distinguishedFolderIdField = value;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00025D8E File Offset: 0x00023F8E
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x00025D96 File Offset: 0x00023F96
		[XmlIgnore]
		public bool DistinguishedFolderIdSpecified
		{
			get
			{
				return this.distinguishedFolderIdFieldSpecified;
			}
			set
			{
				this.distinguishedFolderIdFieldSpecified = value;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x00025D9F File Offset: 0x00023F9F
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x00025DA7 File Offset: 0x00023FA7
		public RetentionTagType PolicyTag
		{
			get
			{
				return this.policyTagField;
			}
			set
			{
				this.policyTagField = value;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00025DB0 File Offset: 0x00023FB0
		// (set) Token: 0x0600149B RID: 5275 RVA: 0x00025DB8 File Offset: 0x00023FB8
		public RetentionTagType ArchiveTag
		{
			get
			{
				return this.archiveTagField;
			}
			set
			{
				this.archiveTagField = value;
			}
		}

		// Token: 0x04000E1D RID: 3613
		private FolderIdType folderIdField;

		// Token: 0x04000E1E RID: 3614
		private FolderIdType parentFolderIdField;

		// Token: 0x04000E1F RID: 3615
		private string folderClassField;

		// Token: 0x04000E20 RID: 3616
		private string displayNameField;

		// Token: 0x04000E21 RID: 3617
		private int totalCountField;

		// Token: 0x04000E22 RID: 3618
		private bool totalCountFieldSpecified;

		// Token: 0x04000E23 RID: 3619
		private int childFolderCountField;

		// Token: 0x04000E24 RID: 3620
		private bool childFolderCountFieldSpecified;

		// Token: 0x04000E25 RID: 3621
		private ExtendedPropertyType[] extendedPropertyField;

		// Token: 0x04000E26 RID: 3622
		private ManagedFolderInformationType managedFolderInformationField;

		// Token: 0x04000E27 RID: 3623
		private EffectiveRightsType effectiveRightsField;

		// Token: 0x04000E28 RID: 3624
		private DistinguishedFolderIdNameType distinguishedFolderIdField;

		// Token: 0x04000E29 RID: 3625
		private bool distinguishedFolderIdFieldSpecified;

		// Token: 0x04000E2A RID: 3626
		private RetentionTagType policyTagField;

		// Token: 0x04000E2B RID: 3627
		private RetentionTagType archiveTagField;
	}
}
