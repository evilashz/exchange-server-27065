using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200059B RID: 1435
	[XmlInclude(typeof(ContactType))]
	[XmlInclude(typeof(AddressEntityType))]
	[XmlInclude(typeof(UrlEntityType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(TaskSuggestionType))]
	[KnownType(typeof(UrlEntityType))]
	[XmlInclude(typeof(MeetingSuggestionType))]
	[XmlInclude(typeof(PhoneEntityType))]
	[XmlInclude(typeof(TaskSuggestionType))]
	[KnownType(typeof(AddressEntityType))]
	[KnownType(typeof(ContactType))]
	[KnownType(typeof(PhoneEntityType))]
	[XmlInclude(typeof(EmailAddressEntityType))]
	[KnownType(typeof(EmailAddressEntityType))]
	[KnownType(typeof(MeetingSuggestionType))]
	[Serializable]
	public abstract class BaseEntityType
	{
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060028A0 RID: 10400 RVA: 0x000AC717 File Offset: 0x000AA917
		// (set) Token: 0x060028A1 RID: 10401 RVA: 0x000AC71F File Offset: 0x000AA91F
		[XmlElement]
		[IgnoreDataMember]
		public EmailPositionType Position { get; set; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x000AC728 File Offset: 0x000AA928
		// (set) Token: 0x060028A3 RID: 10403 RVA: 0x000AC735 File Offset: 0x000AA935
		[XmlIgnore]
		[DataMember(Name = "Position", EmitDefaultValue = false)]
		public string PositionString
		{
			get
			{
				return EnumUtilities.ToString<EmailPositionType>(this.Position);
			}
			set
			{
				throw new NotImplementedException("Position setter");
			}
		}
	}
}
