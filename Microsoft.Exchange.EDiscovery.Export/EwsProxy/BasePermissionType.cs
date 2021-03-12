using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000207 RID: 519
	[XmlInclude(typeof(CalendarPermissionType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(PermissionType))]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public abstract class BasePermissionType
	{
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x00025E2E File Offset: 0x0002402E
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x00025E36 File Offset: 0x00024036
		public UserIdType UserId
		{
			get
			{
				return this.userIdField;
			}
			set
			{
				this.userIdField = value;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x00025E3F File Offset: 0x0002403F
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x00025E47 File Offset: 0x00024047
		public bool CanCreateItems
		{
			get
			{
				return this.canCreateItemsField;
			}
			set
			{
				this.canCreateItemsField = value;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x00025E50 File Offset: 0x00024050
		// (set) Token: 0x060014AE RID: 5294 RVA: 0x00025E58 File Offset: 0x00024058
		[XmlIgnore]
		public bool CanCreateItemsSpecified
		{
			get
			{
				return this.canCreateItemsFieldSpecified;
			}
			set
			{
				this.canCreateItemsFieldSpecified = value;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x00025E61 File Offset: 0x00024061
		// (set) Token: 0x060014B0 RID: 5296 RVA: 0x00025E69 File Offset: 0x00024069
		public bool CanCreateSubFolders
		{
			get
			{
				return this.canCreateSubFoldersField;
			}
			set
			{
				this.canCreateSubFoldersField = value;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x00025E72 File Offset: 0x00024072
		// (set) Token: 0x060014B2 RID: 5298 RVA: 0x00025E7A File Offset: 0x0002407A
		[XmlIgnore]
		public bool CanCreateSubFoldersSpecified
		{
			get
			{
				return this.canCreateSubFoldersFieldSpecified;
			}
			set
			{
				this.canCreateSubFoldersFieldSpecified = value;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x00025E83 File Offset: 0x00024083
		// (set) Token: 0x060014B4 RID: 5300 RVA: 0x00025E8B File Offset: 0x0002408B
		public bool IsFolderOwner
		{
			get
			{
				return this.isFolderOwnerField;
			}
			set
			{
				this.isFolderOwnerField = value;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x00025E94 File Offset: 0x00024094
		// (set) Token: 0x060014B6 RID: 5302 RVA: 0x00025E9C File Offset: 0x0002409C
		[XmlIgnore]
		public bool IsFolderOwnerSpecified
		{
			get
			{
				return this.isFolderOwnerFieldSpecified;
			}
			set
			{
				this.isFolderOwnerFieldSpecified = value;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x00025EA5 File Offset: 0x000240A5
		// (set) Token: 0x060014B8 RID: 5304 RVA: 0x00025EAD File Offset: 0x000240AD
		public bool IsFolderVisible
		{
			get
			{
				return this.isFolderVisibleField;
			}
			set
			{
				this.isFolderVisibleField = value;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x00025EB6 File Offset: 0x000240B6
		// (set) Token: 0x060014BA RID: 5306 RVA: 0x00025EBE File Offset: 0x000240BE
		[XmlIgnore]
		public bool IsFolderVisibleSpecified
		{
			get
			{
				return this.isFolderVisibleFieldSpecified;
			}
			set
			{
				this.isFolderVisibleFieldSpecified = value;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x00025EC7 File Offset: 0x000240C7
		// (set) Token: 0x060014BC RID: 5308 RVA: 0x00025ECF File Offset: 0x000240CF
		public bool IsFolderContact
		{
			get
			{
				return this.isFolderContactField;
			}
			set
			{
				this.isFolderContactField = value;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00025ED8 File Offset: 0x000240D8
		// (set) Token: 0x060014BE RID: 5310 RVA: 0x00025EE0 File Offset: 0x000240E0
		[XmlIgnore]
		public bool IsFolderContactSpecified
		{
			get
			{
				return this.isFolderContactFieldSpecified;
			}
			set
			{
				this.isFolderContactFieldSpecified = value;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00025EE9 File Offset: 0x000240E9
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x00025EF1 File Offset: 0x000240F1
		public PermissionActionType EditItems
		{
			get
			{
				return this.editItemsField;
			}
			set
			{
				this.editItemsField = value;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x00025EFA File Offset: 0x000240FA
		// (set) Token: 0x060014C2 RID: 5314 RVA: 0x00025F02 File Offset: 0x00024102
		[XmlIgnore]
		public bool EditItemsSpecified
		{
			get
			{
				return this.editItemsFieldSpecified;
			}
			set
			{
				this.editItemsFieldSpecified = value;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00025F0B File Offset: 0x0002410B
		// (set) Token: 0x060014C4 RID: 5316 RVA: 0x00025F13 File Offset: 0x00024113
		public PermissionActionType DeleteItems
		{
			get
			{
				return this.deleteItemsField;
			}
			set
			{
				this.deleteItemsField = value;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x00025F1C File Offset: 0x0002411C
		// (set) Token: 0x060014C6 RID: 5318 RVA: 0x00025F24 File Offset: 0x00024124
		[XmlIgnore]
		public bool DeleteItemsSpecified
		{
			get
			{
				return this.deleteItemsFieldSpecified;
			}
			set
			{
				this.deleteItemsFieldSpecified = value;
			}
		}

		// Token: 0x04000E36 RID: 3638
		private UserIdType userIdField;

		// Token: 0x04000E37 RID: 3639
		private bool canCreateItemsField;

		// Token: 0x04000E38 RID: 3640
		private bool canCreateItemsFieldSpecified;

		// Token: 0x04000E39 RID: 3641
		private bool canCreateSubFoldersField;

		// Token: 0x04000E3A RID: 3642
		private bool canCreateSubFoldersFieldSpecified;

		// Token: 0x04000E3B RID: 3643
		private bool isFolderOwnerField;

		// Token: 0x04000E3C RID: 3644
		private bool isFolderOwnerFieldSpecified;

		// Token: 0x04000E3D RID: 3645
		private bool isFolderVisibleField;

		// Token: 0x04000E3E RID: 3646
		private bool isFolderVisibleFieldSpecified;

		// Token: 0x04000E3F RID: 3647
		private bool isFolderContactField;

		// Token: 0x04000E40 RID: 3648
		private bool isFolderContactFieldSpecified;

		// Token: 0x04000E41 RID: 3649
		private PermissionActionType editItemsField;

		// Token: 0x04000E42 RID: 3650
		private bool editItemsFieldSpecified;

		// Token: 0x04000E43 RID: 3651
		private PermissionActionType deleteItemsField;

		// Token: 0x04000E44 RID: 3652
		private bool deleteItemsFieldSpecified;
	}
}
