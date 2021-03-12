using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000605 RID: 1541
	[XmlInclude(typeof(CalendarPermissionType))]
	[KnownType(typeof(PermissionType))]
	[XmlInclude(typeof(PermissionType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(CalendarPermissionType))]
	[Serializable]
	public abstract class BasePermissionType
	{
		// Token: 0x06002F5B RID: 12123 RVA: 0x000B402F File Offset: 0x000B222F
		protected BasePermissionType()
		{
			this.UserId = new UserId();
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002F5C RID: 12124 RVA: 0x000B4042 File Offset: 0x000B2242
		// (set) Token: 0x06002F5D RID: 12125 RVA: 0x000B404A File Offset: 0x000B224A
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public UserId UserId { get; set; }

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06002F5E RID: 12126 RVA: 0x000B4053 File Offset: 0x000B2253
		// (set) Token: 0x06002F5F RID: 12127 RVA: 0x000B405B File Offset: 0x000B225B
		[DataMember(Order = 2)]
		public bool CanCreateItems
		{
			get
			{
				return this.canCreateItems;
			}
			set
			{
				this.CanCreateItemsSpecified = true;
				this.canCreateItems = value;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x000B406B File Offset: 0x000B226B
		// (set) Token: 0x06002F61 RID: 12129 RVA: 0x000B4073 File Offset: 0x000B2273
		[XmlIgnore]
		[IgnoreDataMember]
		public bool CanCreateItemsSpecified { get; set; }

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002F62 RID: 12130 RVA: 0x000B407C File Offset: 0x000B227C
		// (set) Token: 0x06002F63 RID: 12131 RVA: 0x000B4084 File Offset: 0x000B2284
		[DataMember(Order = 3)]
		public bool CanCreateSubFolders
		{
			get
			{
				return this.canCreateSubFolders;
			}
			set
			{
				this.CanCreateSubFoldersSpecified = true;
				this.canCreateSubFolders = value;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x000B4094 File Offset: 0x000B2294
		// (set) Token: 0x06002F65 RID: 12133 RVA: 0x000B409C File Offset: 0x000B229C
		[IgnoreDataMember]
		[XmlIgnore]
		public bool CanCreateSubFoldersSpecified { get; set; }

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002F66 RID: 12134 RVA: 0x000B40A5 File Offset: 0x000B22A5
		// (set) Token: 0x06002F67 RID: 12135 RVA: 0x000B40AD File Offset: 0x000B22AD
		[DataMember(Order = 4)]
		public bool IsFolderOwner
		{
			get
			{
				return this.isFolderOwner;
			}
			set
			{
				this.IsFolderOwnerSpecified = true;
				this.isFolderOwner = value;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002F68 RID: 12136 RVA: 0x000B40BD File Offset: 0x000B22BD
		// (set) Token: 0x06002F69 RID: 12137 RVA: 0x000B40C5 File Offset: 0x000B22C5
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsFolderOwnerSpecified { get; set; }

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002F6A RID: 12138 RVA: 0x000B40CE File Offset: 0x000B22CE
		// (set) Token: 0x06002F6B RID: 12139 RVA: 0x000B40D6 File Offset: 0x000B22D6
		[DataMember(Order = 5)]
		public bool IsFolderVisible
		{
			get
			{
				return this.isFolderVisible;
			}
			set
			{
				this.IsFolderVisibleSpecified = true;
				this.isFolderVisible = value;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002F6C RID: 12140 RVA: 0x000B40E6 File Offset: 0x000B22E6
		// (set) Token: 0x06002F6D RID: 12141 RVA: 0x000B40EE File Offset: 0x000B22EE
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsFolderVisibleSpecified { get; set; }

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002F6E RID: 12142 RVA: 0x000B40F7 File Offset: 0x000B22F7
		// (set) Token: 0x06002F6F RID: 12143 RVA: 0x000B40FF File Offset: 0x000B22FF
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public bool IsFolderContact
		{
			get
			{
				return this.isFolderContact;
			}
			set
			{
				this.IsFolderContactSpecified = true;
				this.isFolderContact = value;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000B410F File Offset: 0x000B230F
		// (set) Token: 0x06002F71 RID: 12145 RVA: 0x000B4117 File Offset: 0x000B2317
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsFolderContactSpecified { get; set; }

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x000B4120 File Offset: 0x000B2320
		// (set) Token: 0x06002F73 RID: 12147 RVA: 0x000B4128 File Offset: 0x000B2328
		[IgnoreDataMember]
		[XmlElement("EditItems")]
		public PermissionActionType EditItems
		{
			get
			{
				return this.editItems;
			}
			set
			{
				this.EditItemsSpecified = true;
				this.editItems = value;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x000B4138 File Offset: 0x000B2338
		// (set) Token: 0x06002F75 RID: 12149 RVA: 0x000B414F File Offset: 0x000B234F
		[XmlIgnore]
		[DataMember(Name = "EditItems", EmitDefaultValue = false, Order = 7)]
		public string EditItemsString
		{
			get
			{
				if (!this.EditItemsSpecified)
				{
					return null;
				}
				return EnumUtilities.ToString<PermissionActionType>(this.EditItems);
			}
			set
			{
				this.EditItems = EnumUtilities.Parse<PermissionActionType>(value);
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002F76 RID: 12150 RVA: 0x000B415D File Offset: 0x000B235D
		// (set) Token: 0x06002F77 RID: 12151 RVA: 0x000B4165 File Offset: 0x000B2365
		[XmlIgnore]
		[IgnoreDataMember]
		public bool EditItemsSpecified { get; set; }

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002F78 RID: 12152 RVA: 0x000B416E File Offset: 0x000B236E
		// (set) Token: 0x06002F79 RID: 12153 RVA: 0x000B4176 File Offset: 0x000B2376
		[XmlElement("DeleteItems")]
		[IgnoreDataMember]
		public PermissionActionType DeleteItems
		{
			get
			{
				return this.deleteItems;
			}
			set
			{
				this.DeleteItemsSpecified = true;
				this.deleteItems = value;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002F7A RID: 12154 RVA: 0x000B4186 File Offset: 0x000B2386
		// (set) Token: 0x06002F7B RID: 12155 RVA: 0x000B419D File Offset: 0x000B239D
		[XmlIgnore]
		[DataMember(Name = "DeleteItems", EmitDefaultValue = false, Order = 8)]
		public string DeleteItemsString
		{
			get
			{
				if (!this.DeleteItemsSpecified)
				{
					return null;
				}
				return EnumUtilities.ToString<PermissionActionType>(this.DeleteItems);
			}
			set
			{
				this.DeleteItems = EnumUtilities.Parse<PermissionActionType>(value);
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002F7C RID: 12156 RVA: 0x000B41AB File Offset: 0x000B23AB
		// (set) Token: 0x06002F7D RID: 12157 RVA: 0x000B41B3 File Offset: 0x000B23B3
		[IgnoreDataMember]
		[XmlIgnore]
		public bool DeleteItemsSpecified { get; set; }

		// Token: 0x04001BAB RID: 7083
		private bool canCreateItems;

		// Token: 0x04001BAC RID: 7084
		private bool canCreateSubFolders;

		// Token: 0x04001BAD RID: 7085
		private bool isFolderOwner;

		// Token: 0x04001BAE RID: 7086
		private bool isFolderVisible;

		// Token: 0x04001BAF RID: 7087
		private bool isFolderContact;

		// Token: 0x04001BB0 RID: 7088
		private PermissionActionType editItems;

		// Token: 0x04001BB1 RID: 7089
		private PermissionActionType deleteItems;
	}
}
