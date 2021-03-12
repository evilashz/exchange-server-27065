using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200090C RID: 2316
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ExRoutingTypeDriver : RoutingTypeDriver
	{
		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x060056F0 RID: 22256 RVA: 0x001662CF File Offset: 0x001644CF
		internal override IEqualityComparer<IParticipant> AddressEqualityComparer
		{
			get
			{
				return RoutingTypeDriver.OrdinalCaseInsensitiveAddressEqualityComparerImpl.Default;
			}
		}

		// Token: 0x060056F1 RID: 22257 RVA: 0x001662D6 File Offset: 0x001644D6
		internal static List<PropValue> TryParseExchangeLegacyDN(string inputString)
		{
			if (!ExRoutingTypeDriver.IsValidExAddress(inputString))
			{
				return null;
			}
			return Participant.ListCoreProperties(null, inputString, "EX");
		}

		// Token: 0x060056F2 RID: 22258 RVA: 0x001662F0 File Offset: 0x001644F0
		internal override bool? IsRoutable(string routingType, StoreSession session)
		{
			return new bool?(base.IsRoutable(routingType, session) ?? true);
		}

		// Token: 0x060056F3 RID: 22259 RVA: 0x0016631E File Offset: 0x0016451E
		internal override bool IsRoutingTypeSupported(string routingType)
		{
			return routingType == "EX";
		}

		// Token: 0x060056F4 RID: 22260 RVA: 0x0016632C File Offset: 0x0016452C
		internal override void Normalize(PropertyBag participantPropertyBag)
		{
			string valueOrDefault = participantPropertyBag.GetValueOrDefault<string>(ParticipantSchema.DisplayName);
			string valueOrDefault2 = participantPropertyBag.GetValueOrDefault<string>(ParticipantSchema.EmailAddress);
			string valueOrDefault3 = participantPropertyBag.GetValueOrDefault<string>(ParticipantSchema.SmtpAddress);
			if (valueOrDefault == null)
			{
				if (valueOrDefault3 != null)
				{
					participantPropertyBag[ParticipantSchema.DisplayName] = valueOrDefault3;
				}
				else if (valueOrDefault2 != null && ExRoutingTypeDriver.IsValidExAddress(valueOrDefault2))
				{
					participantPropertyBag.SetOrDeleteProperty(ParticipantSchema.DisplayName, Util.SubstringBetween(valueOrDefault2, "=", null, SubstringOptions.Backward));
				}
			}
			if (PropertyError.IsPropertyNotFound(participantPropertyBag.TryGetProperty(ParticipantSchema.EmailAddressForDisplay)))
			{
				participantPropertyBag.SetOrDeleteProperty(ParticipantSchema.EmailAddressForDisplay, valueOrDefault3);
			}
			participantPropertyBag.SetOrDeleteProperty(ParticipantSchema.LegacyExchangeDN, valueOrDefault2);
			participantPropertyBag.SetOrDeleteProperty(ParticipantSchema.SendRichInfo, true);
			base.Normalize(participantPropertyBag);
		}

		// Token: 0x060056F5 RID: 22261 RVA: 0x001663D8 File Offset: 0x001645D8
		internal override bool TryDetectRoutingType(PropertyBag participantPropertyBag, out string routingType)
		{
			string valueOrDefault = participantPropertyBag.GetValueOrDefault<string>(ParticipantSchema.EmailAddress);
			if (valueOrDefault != null && ExRoutingTypeDriver.IsValidExAddress(valueOrDefault))
			{
				routingType = "EX";
				return true;
			}
			routingType = null;
			return false;
		}

		// Token: 0x060056F6 RID: 22262 RVA: 0x00166409 File Offset: 0x00164609
		internal override ParticipantValidationStatus Validate(Participant participant)
		{
			if (participant.EmailAddress == null)
			{
				return ParticipantValidationStatus.AddressRequiredForRoutingType;
			}
			if (!ExRoutingTypeDriver.IsValidExAddress(participant.EmailAddress))
			{
				return ParticipantValidationStatus.InvalidAddressFormat;
			}
			return ParticipantValidationStatus.NoError;
		}

		// Token: 0x060056F7 RID: 22263 RVA: 0x00166428 File Offset: 0x00164628
		private static bool IsValidExAddress(string address)
		{
			LegacyDN legacyDN;
			return LegacyDN.TryParse(address, out legacyDN);
		}

		// Token: 0x04002E5F RID: 11871
		private const string LegDnNamePartDelimiter = "=";
	}
}
