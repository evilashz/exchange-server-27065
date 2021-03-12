using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200020E RID: 526
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ManagedFolderInformationType
	{
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00025FAB File Offset: 0x000241AB
		// (set) Token: 0x060014D7 RID: 5335 RVA: 0x00025FB3 File Offset: 0x000241B3
		public bool CanDelete
		{
			get
			{
				return this.canDeleteField;
			}
			set
			{
				this.canDeleteField = value;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x00025FBC File Offset: 0x000241BC
		// (set) Token: 0x060014D9 RID: 5337 RVA: 0x00025FC4 File Offset: 0x000241C4
		[XmlIgnore]
		public bool CanDeleteSpecified
		{
			get
			{
				return this.canDeleteFieldSpecified;
			}
			set
			{
				this.canDeleteFieldSpecified = value;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x00025FCD File Offset: 0x000241CD
		// (set) Token: 0x060014DB RID: 5339 RVA: 0x00025FD5 File Offset: 0x000241D5
		public bool CanRenameOrMove
		{
			get
			{
				return this.canRenameOrMoveField;
			}
			set
			{
				this.canRenameOrMoveField = value;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00025FDE File Offset: 0x000241DE
		// (set) Token: 0x060014DD RID: 5341 RVA: 0x00025FE6 File Offset: 0x000241E6
		[XmlIgnore]
		public bool CanRenameOrMoveSpecified
		{
			get
			{
				return this.canRenameOrMoveFieldSpecified;
			}
			set
			{
				this.canRenameOrMoveFieldSpecified = value;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x00025FEF File Offset: 0x000241EF
		// (set) Token: 0x060014DF RID: 5343 RVA: 0x00025FF7 File Offset: 0x000241F7
		public bool MustDisplayComment
		{
			get
			{
				return this.mustDisplayCommentField;
			}
			set
			{
				this.mustDisplayCommentField = value;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x00026000 File Offset: 0x00024200
		// (set) Token: 0x060014E1 RID: 5345 RVA: 0x00026008 File Offset: 0x00024208
		[XmlIgnore]
		public bool MustDisplayCommentSpecified
		{
			get
			{
				return this.mustDisplayCommentFieldSpecified;
			}
			set
			{
				this.mustDisplayCommentFieldSpecified = value;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00026011 File Offset: 0x00024211
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x00026019 File Offset: 0x00024219
		public bool HasQuota
		{
			get
			{
				return this.hasQuotaField;
			}
			set
			{
				this.hasQuotaField = value;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00026022 File Offset: 0x00024222
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x0002602A File Offset: 0x0002422A
		[XmlIgnore]
		public bool HasQuotaSpecified
		{
			get
			{
				return this.hasQuotaFieldSpecified;
			}
			set
			{
				this.hasQuotaFieldSpecified = value;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00026033 File Offset: 0x00024233
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x0002603B File Offset: 0x0002423B
		public bool IsManagedFoldersRoot
		{
			get
			{
				return this.isManagedFoldersRootField;
			}
			set
			{
				this.isManagedFoldersRootField = value;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00026044 File Offset: 0x00024244
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x0002604C File Offset: 0x0002424C
		[XmlIgnore]
		public bool IsManagedFoldersRootSpecified
		{
			get
			{
				return this.isManagedFoldersRootFieldSpecified;
			}
			set
			{
				this.isManagedFoldersRootFieldSpecified = value;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00026055 File Offset: 0x00024255
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x0002605D File Offset: 0x0002425D
		public string ManagedFolderId
		{
			get
			{
				return this.managedFolderIdField;
			}
			set
			{
				this.managedFolderIdField = value;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x00026066 File Offset: 0x00024266
		// (set) Token: 0x060014ED RID: 5357 RVA: 0x0002606E File Offset: 0x0002426E
		public string Comment
		{
			get
			{
				return this.commentField;
			}
			set
			{
				this.commentField = value;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x00026077 File Offset: 0x00024277
		// (set) Token: 0x060014EF RID: 5359 RVA: 0x0002607F File Offset: 0x0002427F
		public int StorageQuota
		{
			get
			{
				return this.storageQuotaField;
			}
			set
			{
				this.storageQuotaField = value;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00026088 File Offset: 0x00024288
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x00026090 File Offset: 0x00024290
		[XmlIgnore]
		public bool StorageQuotaSpecified
		{
			get
			{
				return this.storageQuotaFieldSpecified;
			}
			set
			{
				this.storageQuotaFieldSpecified = value;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00026099 File Offset: 0x00024299
		// (set) Token: 0x060014F3 RID: 5363 RVA: 0x000260A1 File Offset: 0x000242A1
		public int FolderSize
		{
			get
			{
				return this.folderSizeField;
			}
			set
			{
				this.folderSizeField = value;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x000260AA File Offset: 0x000242AA
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x000260B2 File Offset: 0x000242B2
		[XmlIgnore]
		public bool FolderSizeSpecified
		{
			get
			{
				return this.folderSizeFieldSpecified;
			}
			set
			{
				this.folderSizeFieldSpecified = value;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x000260BB File Offset: 0x000242BB
		// (set) Token: 0x060014F7 RID: 5367 RVA: 0x000260C3 File Offset: 0x000242C3
		public string HomePage
		{
			get
			{
				return this.homePageField;
			}
			set
			{
				this.homePageField = value;
			}
		}

		// Token: 0x04000E6A RID: 3690
		private bool canDeleteField;

		// Token: 0x04000E6B RID: 3691
		private bool canDeleteFieldSpecified;

		// Token: 0x04000E6C RID: 3692
		private bool canRenameOrMoveField;

		// Token: 0x04000E6D RID: 3693
		private bool canRenameOrMoveFieldSpecified;

		// Token: 0x04000E6E RID: 3694
		private bool mustDisplayCommentField;

		// Token: 0x04000E6F RID: 3695
		private bool mustDisplayCommentFieldSpecified;

		// Token: 0x04000E70 RID: 3696
		private bool hasQuotaField;

		// Token: 0x04000E71 RID: 3697
		private bool hasQuotaFieldSpecified;

		// Token: 0x04000E72 RID: 3698
		private bool isManagedFoldersRootField;

		// Token: 0x04000E73 RID: 3699
		private bool isManagedFoldersRootFieldSpecified;

		// Token: 0x04000E74 RID: 3700
		private string managedFolderIdField;

		// Token: 0x04000E75 RID: 3701
		private string commentField;

		// Token: 0x04000E76 RID: 3702
		private int storageQuotaField;

		// Token: 0x04000E77 RID: 3703
		private bool storageQuotaFieldSpecified;

		// Token: 0x04000E78 RID: 3704
		private int folderSizeField;

		// Token: 0x04000E79 RID: 3705
		private bool folderSizeFieldSpecified;

		// Token: 0x04000E7A RID: 3706
		private string homePageField;
	}
}
