using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000846 RID: 2118
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "PhoneNumberType")]
	[XmlType(TypeName = "PersonaPhoneNumber", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PhoneNumber
	{
		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06003D05 RID: 15621 RVA: 0x000D7035 File Offset: 0x000D5235
		// (set) Token: 0x06003D06 RID: 15622 RVA: 0x000D703D File Offset: 0x000D523D
		[IgnoreDataMember]
		[XmlIgnore]
		public PersonPhoneNumberType Type { get; set; }

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06003D07 RID: 15623 RVA: 0x000D7046 File Offset: 0x000D5246
		// (set) Token: 0x06003D08 RID: 15624 RVA: 0x000D704E File Offset: 0x000D524E
		[XmlElement]
		[DataMember(IsRequired = true, Order = 1)]
		public string Number { get; set; }

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06003D09 RID: 15625 RVA: 0x000D7057 File Offset: 0x000D5257
		// (set) Token: 0x06003D0A RID: 15626 RVA: 0x000D7069 File Offset: 0x000D5269
		[DataMember(Name = "Type", IsRequired = true, Order = 2)]
		[XmlElement(ElementName = "Type")]
		public string TypeString
		{
			get
			{
				return this.Type.ToString();
			}
			set
			{
				this.Type = (PersonPhoneNumberType)Enum.Parse(typeof(PersonPhoneNumberType), value);
			}
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x000D7086 File Offset: 0x000D5286
		public PhoneNumber()
		{
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x000D708E File Offset: 0x000D528E
		public PhoneNumber(string number, PersonPhoneNumberType type)
		{
			this.Number = number;
			this.Type = type;
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06003D0D RID: 15629 RVA: 0x000D70A4 File Offset: 0x000D52A4
		// (set) Token: 0x06003D0E RID: 15630 RVA: 0x000D70C0 File Offset: 0x000D52C0
		[XmlIgnore]
		[DataMember(Name = "NormalizedNumber", EmitDefaultValue = false, Order = 3)]
		public string NormalizedNumber
		{
			get
			{
				if (this.normalizedNumberSet)
				{
					return this.normalizedNumber;
				}
				return DtmfString.SanitizePhoneNumber(this.Number);
			}
			set
			{
				this.normalizedNumberSet = true;
				this.normalizedNumber = value;
			}
		}

		// Token: 0x0400219F RID: 8607
		private string normalizedNumber;

		// Token: 0x040021A0 RID: 8608
		private bool normalizedNumberSet;
	}
}
