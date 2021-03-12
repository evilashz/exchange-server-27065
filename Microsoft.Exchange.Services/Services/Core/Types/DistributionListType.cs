using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005B7 RID: 1463
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "DistributionList")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class DistributionListType : ItemType
	{
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x000B113B File Offset: 0x000AF33B
		// (set) Token: 0x06002C84 RID: 11396 RVA: 0x000B114D File Offset: 0x000AF34D
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string DisplayName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(DistributionListSchema.DisplayName);
			}
			set
			{
				base.PropertyBag[DistributionListSchema.DisplayName] = value;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002C85 RID: 11397 RVA: 0x000B1160 File Offset: 0x000AF360
		// (set) Token: 0x06002C86 RID: 11398 RVA: 0x000B1172 File Offset: 0x000AF372
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string FileAs
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(DistributionListSchema.FileAs);
			}
			set
			{
				base.PropertyBag[DistributionListSchema.FileAs] = value;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002C87 RID: 11399 RVA: 0x000B1185 File Offset: 0x000AF385
		// (set) Token: 0x06002C88 RID: 11400 RVA: 0x000B118D File Offset: 0x000AF38D
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public ContactSourceType ContactSource
		{
			get
			{
				return this.contactSource;
			}
			set
			{
				this.contactSource = value;
				this.contactSourceSpecified = true;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x000B119D File Offset: 0x000AF39D
		// (set) Token: 0x06002C8A RID: 11402 RVA: 0x000B11A5 File Offset: 0x000AF3A5
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ContactSourceSpecified
		{
			get
			{
				return this.contactSourceSpecified;
			}
			set
			{
				this.contactSourceSpecified = value;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x000B11AE File Offset: 0x000AF3AE
		// (set) Token: 0x06002C8C RID: 11404 RVA: 0x000B11BB File Offset: 0x000AF3BB
		[XmlArrayItem("Member", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public MemberType[] Members
		{
			get
			{
				return base.GetValueOrDefault<MemberType[]>(DistributionListSchema.Members);
			}
			set
			{
				this[DistributionListSchema.Members] = value;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x000B11C9 File Offset: 0x000AF3C9
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.DistributionList;
			}
		}

		// Token: 0x04001A6F RID: 6767
		private ContactSourceType contactSource = ContactSourceType.Store;

		// Token: 0x04001A70 RID: 6768
		private bool contactSourceSpecified;
	}
}
