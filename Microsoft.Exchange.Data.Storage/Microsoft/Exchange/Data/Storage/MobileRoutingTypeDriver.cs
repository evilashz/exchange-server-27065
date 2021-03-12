using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000913 RID: 2323
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MobileRoutingTypeDriver : RoutingTypeDriver
	{
		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x06005720 RID: 22304 RVA: 0x001668D3 File Offset: 0x00164AD3
		internal override IEqualityComparer<IParticipant> AddressEqualityComparer
		{
			get
			{
				return RoutingTypeDriver.OrdinalCaseInsensitiveAddressEqualityComparerImpl.Default;
			}
		}

		// Token: 0x06005721 RID: 22305 RVA: 0x001668DA File Offset: 0x00164ADA
		internal override bool IsRoutingTypeSupported(string routingType)
		{
			return routingType == "MOBILE";
		}

		// Token: 0x06005722 RID: 22306 RVA: 0x001668E8 File Offset: 0x00164AE8
		internal override bool? IsRoutable(string routingType, StoreSession session)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			if (mailboxSession != null)
			{
				return new bool?(base.IsRoutable(routingType, session).Value && mailboxSession.MailboxOwner.MailboxInfo.Configuration.IsPersonToPersonMessagingEnabled);
			}
			return null;
		}

		// Token: 0x06005723 RID: 22307 RVA: 0x00166938 File Offset: 0x00164B38
		internal static List<PropValue> TryParseMobilePhoneNumber(string inputString)
		{
			if (string.IsNullOrEmpty(inputString))
			{
				return null;
			}
			int num;
			int num2;
			string number;
			string text;
			if ((num = inputString.LastIndexOfAny(MobileRoutingTypeDriver.separatorsRightBracket)) == -1 || (num2 = inputString.LastIndexOfAny(MobileRoutingTypeDriver.separatorsLeftBracket)) == -1)
			{
				number = inputString;
				text = inputString;
			}
			else
			{
				if (num2 >= num || ((inputString[num2] != '[' || inputString[num] != ']') && (inputString[num2] != '<' || inputString[num] != '>')))
				{
					return null;
				}
				text = inputString.Substring(0, num2);
				number = inputString.Substring(num2 + 1, num - num2 - 1);
			}
			E164Number e164Number;
			if (!E164Number.TryParse(number, out e164Number))
			{
				return null;
			}
			int num3 = text.Length;
			int num4 = -1;
			int i = 0;
			while (i < text.Length)
			{
				if (!char.IsWhiteSpace(text[i]))
				{
					if ('"' == text[i])
					{
						num3 = i;
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			int j = text.Length - 1;
			while (j >= num3)
			{
				if (!char.IsWhiteSpace(text[j]))
				{
					if ('"' == text[j])
					{
						num4 = j;
						break;
					}
					break;
				}
				else
				{
					j--;
				}
			}
			if (num3 < num4)
			{
				text = text.Substring(num3 + 1, num4 - num3 - 1);
			}
			else
			{
				text = text.Trim();
			}
			return Participant.ListCoreProperties(text, e164Number.Number, "MOBILE");
		}

		// Token: 0x06005724 RID: 22308 RVA: 0x00166A82 File Offset: 0x00164C82
		internal override ParticipantValidationStatus Validate(Participant participant)
		{
			if (participant.EmailAddress == null)
			{
				return ParticipantValidationStatus.AddressRequiredForRoutingType;
			}
			if (!E164Number.IsValidE164Number(participant.EmailAddress))
			{
				return ParticipantValidationStatus.InvalidAddressFormat;
			}
			return ParticipantValidationStatus.NoError;
		}

		// Token: 0x06005725 RID: 22309 RVA: 0x00166AA0 File Offset: 0x00164CA0
		internal override void Normalize(PropertyBag participantPropertyBag)
		{
			string valueOrDefault = participantPropertyBag.GetValueOrDefault<string>(ParticipantSchema.EmailAddress);
			E164Number e164Number;
			if (!string.IsNullOrEmpty(valueOrDefault) && E164Number.TryParse(valueOrDefault, out e164Number))
			{
				participantPropertyBag.SetOrDeleteProperty(ParticipantSchema.EmailAddress, e164Number.Number);
			}
			base.Normalize(participantPropertyBag);
		}

		// Token: 0x04002E66 RID: 11878
		private static readonly char[] separatorsLeftBracket = new char[]
		{
			'[',
			'<'
		};

		// Token: 0x04002E67 RID: 11879
		private static readonly char[] separatorsRightBracket = new char[]
		{
			']',
			'>'
		};
	}
}
