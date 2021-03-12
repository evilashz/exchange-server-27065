using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005B8 RID: 1464
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class MemberType
	{
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x000B11D5 File Offset: 0x000AF3D5
		// (set) Token: 0x06002C90 RID: 11408 RVA: 0x000B11DD File Offset: 0x000AF3DD
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public EmailAddressWrapper Mailbox { get; set; }

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x000B11E6 File Offset: 0x000AF3E6
		// (set) Token: 0x06002C92 RID: 11410 RVA: 0x000B11EE File Offset: 0x000AF3EE
		[XmlElement]
		[IgnoreDataMember]
		public MemberStatusType Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.StatusSpecified = true;
				this.status = value;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x000B11FE File Offset: 0x000AF3FE
		// (set) Token: 0x06002C94 RID: 11412 RVA: 0x000B1215 File Offset: 0x000AF415
		[XmlIgnore]
		[DataMember(Name = "Status", EmitDefaultValue = false, Order = 2)]
		public string StatusString
		{
			get
			{
				if (!this.StatusSpecified)
				{
					return null;
				}
				return EnumUtilities.ToString<MemberStatusType>(this.Status);
			}
			set
			{
				this.Status = EnumUtilities.Parse<MemberStatusType>(value);
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002C95 RID: 11413 RVA: 0x000B1223 File Offset: 0x000AF423
		// (set) Token: 0x06002C96 RID: 11414 RVA: 0x000B122B File Offset: 0x000AF42B
		[IgnoreDataMember]
		[XmlIgnore]
		public bool StatusSpecified { get; set; }

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x000B1234 File Offset: 0x000AF434
		// (set) Token: 0x06002C98 RID: 11416 RVA: 0x000B123C File Offset: 0x000AF43C
		[DataMember(EmitDefaultValue = false, Order = 0)]
		[XmlAttribute]
		public string Key { get; set; }

		// Token: 0x04001A71 RID: 6769
		private MemberStatusType status;
	}
}
