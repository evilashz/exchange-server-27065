using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005B9 RID: 1465
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class EffectiveRightsType
	{
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x000B1245 File Offset: 0x000AF445
		// (set) Token: 0x06002C9A RID: 11418 RVA: 0x000B124D File Offset: 0x000AF44D
		[DataMember(Order = 1)]
		public bool CreateAssociated { get; set; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002C9B RID: 11419 RVA: 0x000B1256 File Offset: 0x000AF456
		// (set) Token: 0x06002C9C RID: 11420 RVA: 0x000B125E File Offset: 0x000AF45E
		[DataMember(Order = 2)]
		public bool CreateContents { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000B1267 File Offset: 0x000AF467
		// (set) Token: 0x06002C9E RID: 11422 RVA: 0x000B126F File Offset: 0x000AF46F
		[DataMember(Order = 3)]
		public bool CreateHierarchy { get; set; }

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x000B1278 File Offset: 0x000AF478
		// (set) Token: 0x06002CA0 RID: 11424 RVA: 0x000B1280 File Offset: 0x000AF480
		[DataMember(Order = 4)]
		public bool Delete { get; set; }

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x000B1289 File Offset: 0x000AF489
		// (set) Token: 0x06002CA2 RID: 11426 RVA: 0x000B1291 File Offset: 0x000AF491
		[DataMember(Order = 5)]
		public bool Modify { get; set; }

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x000B129A File Offset: 0x000AF49A
		// (set) Token: 0x06002CA4 RID: 11428 RVA: 0x000B12A2 File Offset: 0x000AF4A2
		[DataMember(Order = 6)]
		public bool Read { get; set; }

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002CA5 RID: 11429 RVA: 0x000B12AC File Offset: 0x000AF4AC
		// (set) Token: 0x06002CA6 RID: 11430 RVA: 0x000B12D6 File Offset: 0x000AF4D6
		[DataMember(EmitDefaultValue = false, Order = 7)]
		public bool? ViewPrivateItems
		{
			get
			{
				if (this.ViewPrivateItemsSpecified)
				{
					return new bool?(this.viewPrivateItems);
				}
				return null;
			}
			set
			{
				this.ViewPrivateItemsSpecified = (value != null);
				if (value != null)
				{
					this.viewPrivateItems = value.Value;
				}
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x000B12FB File Offset: 0x000AF4FB
		// (set) Token: 0x06002CA8 RID: 11432 RVA: 0x000B1303 File Offset: 0x000AF503
		[IgnoreDataMember]
		[XmlIgnore]
		public bool ViewPrivateItemsSpecified { get; set; }

		// Token: 0x04001A75 RID: 6773
		private bool viewPrivateItems;
	}
}
