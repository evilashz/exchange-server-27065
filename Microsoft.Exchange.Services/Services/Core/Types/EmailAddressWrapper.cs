using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200075E RID: 1886
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "EmailAddress")]
	[XmlType(TypeName = "EmailAddressType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class EmailAddressWrapper
	{
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x000C74E7 File Offset: 0x000C56E7
		// (set) Token: 0x06003857 RID: 14423 RVA: 0x000C74EF File Offset: 0x000C56EF
		[XmlElement("Name")]
		[DataMember(Name = "Name", EmitDefaultValue = false, Order = 1)]
		public string Name { get; set; }

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06003858 RID: 14424 RVA: 0x000C74F8 File Offset: 0x000C56F8
		// (set) Token: 0x06003859 RID: 14425 RVA: 0x000C7500 File Offset: 0x000C5700
		[XmlElement("EmailAddress")]
		[DataMember(Name = "EmailAddress", EmitDefaultValue = false, Order = 2)]
		public string EmailAddress { get; set; }

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x0600385A RID: 14426 RVA: 0x000C7509 File Offset: 0x000C5709
		// (set) Token: 0x0600385B RID: 14427 RVA: 0x000C7511 File Offset: 0x000C5711
		[XmlElement("RoutingType")]
		[DataMember(Name = "RoutingType", EmitDefaultValue = false, Order = 3)]
		public string RoutingType { get; set; }

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x0600385C RID: 14428 RVA: 0x000C751A File Offset: 0x000C571A
		// (set) Token: 0x0600385D RID: 14429 RVA: 0x000C7522 File Offset: 0x000C5722
		[XmlElement("MailboxType")]
		[DataMember(Name = "MailboxType", EmitDefaultValue = false, Order = 4)]
		public string MailboxType { get; set; }

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x0600385E RID: 14430 RVA: 0x000C752B File Offset: 0x000C572B
		// (set) Token: 0x0600385F RID: 14431 RVA: 0x000C7533 File Offset: 0x000C5733
		[XmlElement("ItemId")]
		[DataMember(Name = "ItemId", EmitDefaultValue = false, Order = 5)]
		public ItemId ItemId { get; set; }

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06003860 RID: 14432 RVA: 0x000C753C File Offset: 0x000C573C
		// (set) Token: 0x06003861 RID: 14433 RVA: 0x000C7544 File Offset: 0x000C5744
		[XmlIgnore]
		[DataMember(Name = "SipUri", EmitDefaultValue = false, Order = 6)]
		public string SipUri { get; set; }

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x000C754D File Offset: 0x000C574D
		// (set) Token: 0x06003863 RID: 14435 RVA: 0x000C7555 File Offset: 0x000C5755
		[DataMember(Name = "Submitted", EmitDefaultValue = false, Order = 7)]
		[XmlIgnore]
		public bool? Submitted { get; set; }

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06003864 RID: 14436 RVA: 0x000C755E File Offset: 0x000C575E
		// (set) Token: 0x06003865 RID: 14437 RVA: 0x000C7566 File Offset: 0x000C5766
		[DataMember(Name = "OriginalDisplayName", EmitDefaultValue = false, Order = 8)]
		[XmlElement("OriginalDisplayName")]
		public string OriginalDisplayName { get; set; }

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06003866 RID: 14438 RVA: 0x000C756F File Offset: 0x000C576F
		// (set) Token: 0x06003867 RID: 14439 RVA: 0x000C7577 File Offset: 0x000C5777
		[DataMember(Name = "EmailAddressIndex", EmitDefaultValue = false, Order = 9)]
		[XmlIgnore]
		public string EmailAddressIndex { get; set; }

		// Token: 0x06003868 RID: 14440 RVA: 0x000C7580 File Offset: 0x000C5780
		internal static EmailAddressWrapper FromParticipant(IParticipant participant)
		{
			return EmailAddressWrapper.FromParticipant(participant, EWSSettings.ParticipantInformation);
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000C7590 File Offset: 0x000C5790
		internal static EmailAddressWrapper FromParticipant(IParticipant participant, ParticipantInformationDictionary convertedParticipantsCache)
		{
			if (participant == null)
			{
				return null;
			}
			ParticipantInformation participantInformation;
			if (!convertedParticipantsCache.TryGetParticipant(participant, out participantInformation))
			{
				participantInformation = ParticipantInformationDictionary.ConvertToParticipantInformation(participant);
				convertedParticipantsCache.AddParticipant(participant, participantInformation);
			}
			return EmailAddressWrapper.FromParticipantInformation(participantInformation);
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000C75C4 File Offset: 0x000C57C4
		internal static EmailAddressWrapper FromParticipantInformation(ParticipantInformation participant)
		{
			return new EmailAddressWrapper
			{
				Name = participant.DisplayName,
				EmailAddress = participant.EmailAddress,
				RoutingType = participant.RoutingType,
				MailboxType = participant.MailboxType.ToString(),
				SipUri = participant.SipUri,
				Submitted = participant.Submitted
			};
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x0600386B RID: 14443 RVA: 0x000C762A File Offset: 0x000C582A
		public static IEqualityComparer<EmailAddressWrapper> AddressEqualityComparer
		{
			get
			{
				return EmailAddressWrapper.AddressEqualityComparerImpl.Default;
			}
		}

		// Token: 0x0200075F RID: 1887
		private class AddressEqualityComparerImpl : IEqualityComparer<EmailAddressWrapper>
		{
			// Token: 0x0600386C RID: 14444 RVA: 0x000C7631 File Offset: 0x000C5831
			public bool Equals(EmailAddressWrapper x, EmailAddressWrapper y)
			{
				return object.ReferenceEquals(x, y) || (x != null && y != null && string.Compare(x.RoutingType, y.RoutingType, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(x.EmailAddress, y.EmailAddress, StringComparison.OrdinalIgnoreCase) == 0);
			}

			// Token: 0x0600386D RID: 14445 RVA: 0x000C7674 File Offset: 0x000C5874
			public int GetHashCode(EmailAddressWrapper x)
			{
				int num = 0;
				if (x != null)
				{
					num = ((x.EmailAddress != null) ? x.EmailAddress.GetHashCode() : 0);
					if (num == 0)
					{
						num = ((x.OriginalDisplayName != null) ? x.OriginalDisplayName.GetHashCode() : 0);
					}
					if (num == 0)
					{
						num = ((x.Name != null) ? x.Name.GetHashCode() : 0);
					}
				}
				return num;
			}

			// Token: 0x04001F4B RID: 8011
			public static EmailAddressWrapper.AddressEqualityComparerImpl Default = new EmailAddressWrapper.AddressEqualityComparerImpl();
		}
	}
}
