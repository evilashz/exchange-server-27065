using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005B0 RID: 1456
	[DataContract(Name = "ContactsFolder", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ContactsFolderType : BaseFolderType
	{
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002B86 RID: 11142 RVA: 0x000AFBF4 File Offset: 0x000ADDF4
		// (set) Token: 0x06002B87 RID: 11143 RVA: 0x000AFC06 File Offset: 0x000ADE06
		[IgnoreDataMember]
		[XmlElement]
		public PermissionReadAccess SharingEffectiveRights
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PermissionReadAccess>(ContactsFolderSchema.SharingEffectiveRights);
			}
			set
			{
				base.PropertyBag[ContactsFolderSchema.SharingEffectiveRights] = value;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x000AFC1E File Offset: 0x000ADE1E
		// (set) Token: 0x06002B89 RID: 11145 RVA: 0x000AFC35 File Offset: 0x000ADE35
		[DataMember(Name = "SharingEffectiveRights", EmitDefaultValue = false, Order = 1)]
		[XmlIgnore]
		public string SharingEffectiveRightsString
		{
			get
			{
				if (!this.SharingEffectiveRightsSpecified)
				{
					return null;
				}
				return EnumUtilities.ToString<PermissionReadAccess>(this.SharingEffectiveRights);
			}
			set
			{
				this.SharingEffectiveRights = EnumUtilities.Parse<PermissionReadAccess>(value);
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x000AFC43 File Offset: 0x000ADE43
		// (set) Token: 0x06002B8B RID: 11147 RVA: 0x000AFC50 File Offset: 0x000ADE50
		[XmlIgnore]
		[IgnoreDataMember]
		public bool SharingEffectiveRightsSpecified
		{
			get
			{
				return base.IsSet(ContactsFolderSchema.SharingEffectiveRights);
			}
			set
			{
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002B8C RID: 11148 RVA: 0x000AFC52 File Offset: 0x000ADE52
		// (set) Token: 0x06002B8D RID: 11149 RVA: 0x000AFC64 File Offset: 0x000ADE64
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public PermissionSetType PermissionSet
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PermissionSetType>(FolderSchema.PermissionSet);
			}
			set
			{
				base.PropertyBag[FolderSchema.PermissionSet] = value;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x000AFC77 File Offset: 0x000ADE77
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.ContactsFolder;
			}
		}
	}
}
