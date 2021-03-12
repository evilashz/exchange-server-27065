using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000607 RID: 1543
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Permission")]
	[Serializable]
	public class PermissionType : BasePermissionType
	{
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002F8A RID: 12170 RVA: 0x000B4271 File Offset: 0x000B2471
		// (set) Token: 0x06002F8B RID: 12171 RVA: 0x000B4279 File Offset: 0x000B2479
		[XmlElement]
		[IgnoreDataMember]
		public PermissionReadAccess? ReadItems { get; set; }

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002F8C RID: 12172 RVA: 0x000B4284 File Offset: 0x000B2484
		// (set) Token: 0x06002F8D RID: 12173 RVA: 0x000B42B6 File Offset: 0x000B24B6
		[XmlIgnore]
		[DataMember(Name = "ReadItems", EmitDefaultValue = false, Order = 1)]
		public string ReadItemsString
		{
			get
			{
				if (this.ReadItems == null)
				{
					return null;
				}
				return EnumUtilities.ToString<PermissionReadAccess>(this.ReadItems.Value);
			}
			set
			{
				this.ReadItems = new PermissionReadAccess?(EnumUtilities.Parse<PermissionReadAccess>(value));
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002F8E RID: 12174 RVA: 0x000B42CC File Offset: 0x000B24CC
		// (set) Token: 0x06002F8F RID: 12175 RVA: 0x000B42E7 File Offset: 0x000B24E7
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ReadItemsSpecified
		{
			get
			{
				return this.ReadItems != null;
			}
			set
			{
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06002F90 RID: 12176 RVA: 0x000B42E9 File Offset: 0x000B24E9
		// (set) Token: 0x06002F91 RID: 12177 RVA: 0x000B42F1 File Offset: 0x000B24F1
		[XmlElement]
		[IgnoreDataMember]
		public PermissionLevelType PermissionLevel { get; set; }

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x000B42FA File Offset: 0x000B24FA
		// (set) Token: 0x06002F93 RID: 12179 RVA: 0x000B4307 File Offset: 0x000B2507
		[DataMember(Name = "PermissionLevel", EmitDefaultValue = false, Order = 2)]
		[XmlIgnore]
		public string PermissionLevelString
		{
			get
			{
				return EnumUtilities.ToString<PermissionLevelType>(this.PermissionLevel);
			}
			set
			{
				this.PermissionLevel = EnumUtilities.Parse<PermissionLevelType>(value);
			}
		}
	}
}
