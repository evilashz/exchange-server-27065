using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008BB RID: 2235
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "UserIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class UserId
	{
		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x000DB414 File Offset: 0x000D9614
		// (set) Token: 0x06003F51 RID: 16209 RVA: 0x000DB41C File Offset: 0x000D961C
		[XmlElement("SID")]
		[DataMember(Name = "SID", EmitDefaultValue = false)]
		public string Sid { get; set; }

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06003F52 RID: 16210 RVA: 0x000DB425 File Offset: 0x000D9625
		// (set) Token: 0x06003F53 RID: 16211 RVA: 0x000DB42D File Offset: 0x000D962D
		[XmlElement("PrimarySmtpAddress")]
		[DataMember(Name = "PrimarySmtpAddress", EmitDefaultValue = false)]
		public string PrimarySmtpAddress { get; set; }

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06003F54 RID: 16212 RVA: 0x000DB436 File Offset: 0x000D9636
		// (set) Token: 0x06003F55 RID: 16213 RVA: 0x000DB43E File Offset: 0x000D963E
		[DataMember(Name = "DisplayName", EmitDefaultValue = false)]
		[XmlElement("DisplayName")]
		public string DisplayName { get; set; }

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06003F56 RID: 16214 RVA: 0x000DB447 File Offset: 0x000D9647
		// (set) Token: 0x06003F57 RID: 16215 RVA: 0x000DB44F File Offset: 0x000D964F
		[IgnoreDataMember]
		[XmlElement("DistinguishedUser")]
		[DefaultValue(DistinguishedUserType.None)]
		public DistinguishedUserType DistinguishedUser
		{
			get
			{
				return this.distinguishedUser;
			}
			set
			{
				this.distinguishedUser = value;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06003F58 RID: 16216 RVA: 0x000DB458 File Offset: 0x000D9658
		// (set) Token: 0x06003F59 RID: 16217 RVA: 0x000DB46F File Offset: 0x000D966F
		[XmlIgnore]
		[DataMember(Name = "DistinguishedUser", EmitDefaultValue = false)]
		public string DistinguishedUserString
		{
			get
			{
				if (this.DistinguishedUser == DistinguishedUserType.None)
				{
					return null;
				}
				return EnumUtilities.ToString<DistinguishedUserType>(this.DistinguishedUser);
			}
			set
			{
				this.DistinguishedUser = EnumUtilities.Parse<DistinguishedUserType>(value);
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06003F5A RID: 16218 RVA: 0x000DB47D File Offset: 0x000D967D
		// (set) Token: 0x06003F5B RID: 16219 RVA: 0x000DB485 File Offset: 0x000D9685
		[DataMember(Name = "ExternalUserIdentity", EmitDefaultValue = false)]
		[XmlElement("ExternalUserIdentity", IsNullable = false)]
		public string ExternalUserIdentity { get; set; }

		// Token: 0x04002444 RID: 9284
		private DistinguishedUserType distinguishedUser;
	}
}
