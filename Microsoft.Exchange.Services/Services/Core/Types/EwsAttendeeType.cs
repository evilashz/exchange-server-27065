using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005A1 RID: 1441
	[XmlType(TypeName = "AttendeeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "AttendeeType", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class EwsAttendeeType
	{
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060028C8 RID: 10440 RVA: 0x000AC911 File Offset: 0x000AAB11
		// (set) Token: 0x060028C9 RID: 10441 RVA: 0x000AC919 File Offset: 0x000AAB19
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public EmailAddressWrapper Mailbox { get; set; }

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x000AC922 File Offset: 0x000AAB22
		// (set) Token: 0x060028CB RID: 10443 RVA: 0x000AC92A File Offset: 0x000AAB2A
		[XmlIgnore]
		[DataMember(Name = "ResponseType", EmitDefaultValue = false, Order = 2)]
		public string ResponseTypeString
		{
			get
			{
				return this.responseTypeString;
			}
			set
			{
				this.ResponseTypeSpecified = true;
				this.responseTypeString = value;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060028CC RID: 10444 RVA: 0x000AC93A File Offset: 0x000AAB3A
		// (set) Token: 0x060028CD RID: 10445 RVA: 0x000AC951 File Offset: 0x000AAB51
		[XmlElement("ResponseType")]
		[IgnoreDataMember]
		public ResponseTypeType ResponseType
		{
			get
			{
				if (!this.ResponseTypeSpecified)
				{
					return ResponseTypeType.Unknown;
				}
				return EnumUtilities.Parse<ResponseTypeType>(this.ResponseTypeString);
			}
			set
			{
				this.ResponseTypeString = EnumUtilities.ToString<ResponseTypeType>(value);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060028CE RID: 10446 RVA: 0x000AC95F File Offset: 0x000AAB5F
		// (set) Token: 0x060028CF RID: 10447 RVA: 0x000AC967 File Offset: 0x000AAB67
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ResponseTypeSpecified { get; set; }

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060028D0 RID: 10448 RVA: 0x000AC970 File Offset: 0x000AAB70
		// (set) Token: 0x060028D1 RID: 10449 RVA: 0x000AC978 File Offset: 0x000AAB78
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public string LastResponseTime
		{
			get
			{
				return this.lastResponseTime;
			}
			set
			{
				this.LastResponseTimeSpecified = true;
				this.lastResponseTime = value;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060028D2 RID: 10450 RVA: 0x000AC988 File Offset: 0x000AAB88
		// (set) Token: 0x060028D3 RID: 10451 RVA: 0x000AC990 File Offset: 0x000AAB90
		[XmlIgnore]
		[IgnoreDataMember]
		public bool LastResponseTimeSpecified { get; set; }

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060028D4 RID: 10452 RVA: 0x000AC999 File Offset: 0x000AAB99
		// (set) Token: 0x060028D5 RID: 10453 RVA: 0x000AC9A1 File Offset: 0x000AABA1
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string ProposedStart { get; set; }

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x000AC9AA File Offset: 0x000AABAA
		// (set) Token: 0x060028D7 RID: 10455 RVA: 0x000AC9B2 File Offset: 0x000AABB2
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string ProposedEnd { get; set; }

		// Token: 0x040019E3 RID: 6627
		private string responseTypeString;

		// Token: 0x040019E4 RID: 6628
		private string lastResponseTime;
	}
}
