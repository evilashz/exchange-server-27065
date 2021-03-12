using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200090B RID: 2315
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SmtpRoutingTypeDriver : RoutingTypeDriver
	{
		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x060056E6 RID: 22246 RVA: 0x001661DC File Offset: 0x001643DC
		internal override IEqualityComparer<IParticipant> AddressEqualityComparer
		{
			get
			{
				return RoutingTypeDriver.OrdinalCaseInsensitiveAddressEqualityComparerImpl.Default;
			}
		}

		// Token: 0x060056E7 RID: 22247 RVA: 0x001661E4 File Offset: 0x001643E4
		internal override bool? IsRoutable(string routingType, StoreSession session)
		{
			return new bool?(base.IsRoutable(routingType, session) ?? true);
		}

		// Token: 0x060056E8 RID: 22248 RVA: 0x00166212 File Offset: 0x00164412
		internal override bool IsRoutingTypeSupported(string routingType)
		{
			return routingType == "SMTP";
		}

		// Token: 0x060056E9 RID: 22249 RVA: 0x0016621F File Offset: 0x0016441F
		internal override void Normalize(PropertyBag participantPropertyBag)
		{
			participantPropertyBag.SetOrDeleteProperty(ParticipantSchema.SmtpAddress, participantPropertyBag.TryGetProperty(ParticipantSchema.EmailAddress));
			base.Normalize(participantPropertyBag);
		}

		// Token: 0x060056EA RID: 22250 RVA: 0x00166240 File Offset: 0x00164440
		internal override bool TryDetectRoutingType(PropertyBag participantPropertyBag, out string routingType)
		{
			string valueOrDefault = participantPropertyBag.GetValueOrDefault<string>(ParticipantSchema.EmailAddress);
			if (valueOrDefault != null && SmtpRoutingTypeDriver.IsValidSmtpAddress(valueOrDefault))
			{
				routingType = "SMTP";
				return true;
			}
			routingType = null;
			return false;
		}

		// Token: 0x060056EB RID: 22251 RVA: 0x00166271 File Offset: 0x00164471
		internal override ParticipantValidationStatus Validate(Participant participant)
		{
			if (participant.EmailAddress == null)
			{
				return ParticipantValidationStatus.AddressRequiredForRoutingType;
			}
			if (!SmtpRoutingTypeDriver.IsValidSmtpAddress(participant.EmailAddress))
			{
				return ParticipantValidationStatus.InvalidAddressFormat;
			}
			return ParticipantValidationStatus.NoError;
		}

		// Token: 0x060056EC RID: 22252 RVA: 0x0016628F File Offset: 0x0016448F
		internal override string FormatAddress(Participant participant, AddressFormat addressFormat)
		{
			if (addressFormat == AddressFormat.Rfc822Smtp)
			{
				return string.Format("\"{0}\" <{1}>", participant.DisplayName, participant.EmailAddress);
			}
			return base.FormatAddress(participant, addressFormat);
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x001662B3 File Offset: 0x001644B3
		private static bool IsValidSmtpAddress(string address)
		{
			return SmtpAddress.IsValidSmtpAddress(address);
		}

		// Token: 0x04002E5D RID: 11869
		private const string RfcFormat = "\"{0}\" <{1}>";

		// Token: 0x04002E5E RID: 11870
		internal static IEqualityComparer<string> SmtpAddressEqualityComparer = RoutingTypeDriver.OrdinalCaseInsensitiveAddressEqualityComparerImpl.Default;
	}
}
