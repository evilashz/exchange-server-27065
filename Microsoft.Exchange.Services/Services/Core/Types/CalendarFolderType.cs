using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005A7 RID: 1447
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "CalendarFolder", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class CalendarFolderType : BaseFolderType
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002941 RID: 10561 RVA: 0x000AD0B7 File Offset: 0x000AB2B7
		// (set) Token: 0x06002942 RID: 10562 RVA: 0x000AD0C9 File Offset: 0x000AB2C9
		[IgnoreDataMember]
		[XmlElement]
		public CalendarPermissionReadAccess SharingEffectiveRights
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<CalendarPermissionReadAccess>(CalendarFolderSchema.SharingEffectiveRights);
			}
			set
			{
				base.PropertyBag[ContactsFolderSchema.SharingEffectiveRights] = value;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002943 RID: 10563 RVA: 0x000AD0E1 File Offset: 0x000AB2E1
		// (set) Token: 0x06002944 RID: 10564 RVA: 0x000AD0F8 File Offset: 0x000AB2F8
		[XmlIgnore]
		[DataMember(Name = "SharingEffectiveRights", EmitDefaultValue = false, Order = 1)]
		public string SharingEffectiveRightsString
		{
			get
			{
				if (!this.SharingEffectiveRightsSpecified)
				{
					return null;
				}
				return EnumUtilities.ToString<CalendarPermissionReadAccess>(this.SharingEffectiveRights);
			}
			set
			{
				this.SharingEffectiveRights = EnumUtilities.Parse<CalendarPermissionReadAccess>(value);
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x000AD106 File Offset: 0x000AB306
		// (set) Token: 0x06002946 RID: 10566 RVA: 0x000AD113 File Offset: 0x000AB313
		[XmlIgnore]
		[IgnoreDataMember]
		public bool SharingEffectiveRightsSpecified
		{
			get
			{
				return base.IsSet(CalendarFolderSchema.SharingEffectiveRights);
			}
			set
			{
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000AD115 File Offset: 0x000AB315
		// (set) Token: 0x06002948 RID: 10568 RVA: 0x000AD127 File Offset: 0x000AB327
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public CalendarPermissionSetType PermissionSet
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<CalendarPermissionSetType>(CalendarFolderSchema.PermissionSet);
			}
			set
			{
				base.PropertyBag[CalendarFolderSchema.PermissionSet] = value;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x000AD13A File Offset: 0x000AB33A
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.CalendarFolder;
			}
		}
	}
}
