using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200090D RID: 2317
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class UnspecifiedRoutingTypeDriver : RoutingTypeDriver
	{
		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x060056F9 RID: 22265 RVA: 0x00166445 File Offset: 0x00164645
		internal override IEqualityComparer<IParticipant> AddressEqualityComparer
		{
			get
			{
				return UnspecifiedRoutingTypeDriver.AddressEqualityComparerImpl.Default;
			}
		}

		// Token: 0x060056FA RID: 22266 RVA: 0x0016644C File Offset: 0x0016464C
		internal static List<PropValue> TryParse(string inputString)
		{
			if (string.IsNullOrEmpty(inputString))
			{
				return null;
			}
			return Participant.ListCoreProperties(inputString, null, null, inputString);
		}

		// Token: 0x060056FB RID: 22267 RVA: 0x00166461 File Offset: 0x00164661
		internal override bool? IsRoutable(string routingType, StoreSession session)
		{
			return new bool?(false);
		}

		// Token: 0x060056FC RID: 22268 RVA: 0x00166469 File Offset: 0x00164669
		internal override bool IsRoutingTypeSupported(string routingType)
		{
			return routingType == null;
		}

		// Token: 0x060056FD RID: 22269 RVA: 0x00166474 File Offset: 0x00164674
		internal override void Normalize(PropertyBag participantPropertyBag)
		{
			string valueOrDefault = participantPropertyBag.GetValueOrDefault<string>(ParticipantSchema.DisplayName);
			string valueOrDefault2 = participantPropertyBag.GetValueOrDefault<string>(ParticipantSchema.EmailAddress);
			if (valueOrDefault2 != null)
			{
				if (valueOrDefault == null)
				{
					participantPropertyBag[ParticipantSchema.DisplayName] = valueOrDefault2;
				}
				else
				{
					participantPropertyBag[ParticipantSchema.EmailAddressForDisplay] = valueOrDefault2;
				}
				participantPropertyBag.Delete(ParticipantSchema.EmailAddress);
			}
			participantPropertyBag.Delete(ParticipantSchema.DisplayTypeEx);
			participantPropertyBag.Delete(ParticipantSchema.DisplayType);
			base.Normalize(participantPropertyBag);
		}

		// Token: 0x060056FE RID: 22270 RVA: 0x001664E1 File Offset: 0x001646E1
		internal override bool TryDetectRoutingType(PropertyBag participantPropertyBag, out string routingType)
		{
			routingType = null;
			return true;
		}

		// Token: 0x060056FF RID: 22271 RVA: 0x001664E8 File Offset: 0x001646E8
		internal override ParticipantValidationStatus Validate(Participant participant)
		{
			if (participant.DisplayName == null)
			{
				if (participant.GetValueOrDefault<string>(ParticipantSchema.EmailAddressForDisplay, null) == null)
				{
					return ParticipantValidationStatus.DisplayNameRequiredForRoutingType;
				}
			}
			else if (participant.EmailAddress != null)
			{
				return ParticipantValidationStatus.AddressAndRoutingTypeMismatch;
			}
			return ParticipantValidationStatus.NoError;
		}

		// Token: 0x06005700 RID: 22272 RVA: 0x0016651C File Offset: 0x0016471C
		internal override string FormatAddress(Participant participant, AddressFormat addressFormat)
		{
			if (addressFormat == AddressFormat.OutlookFormat || addressFormat == AddressFormat.Rfc822Smtp)
			{
				return participant.DisplayName;
			}
			return base.FormatAddress(participant, addressFormat);
		}

		// Token: 0x04002E60 RID: 11872
		private static readonly PropertyDefinition[] meaningfulProperties = new PropertyDefinition[]
		{
			ParticipantSchema.DisplayName,
			ParticipantSchema.EmailAddress,
			ParticipantSchema.EmailAddressForDisplay
		};

		// Token: 0x0200090E RID: 2318
		private sealed class AddressEqualityComparerImpl : IEqualityComparer<IParticipant>
		{
			// Token: 0x06005703 RID: 22275 RVA: 0x0016656E File Offset: 0x0016476E
			public bool Equals(IParticipant x, IParticipant y)
			{
				if (x.DisplayName == null)
				{
					return x.Equals(y);
				}
				return StringComparer.Ordinal.Equals(x.DisplayName, y.DisplayName);
			}

			// Token: 0x06005704 RID: 22276 RVA: 0x00166596 File Offset: 0x00164796
			public int GetHashCode(IParticipant x)
			{
				if (x.DisplayName == null)
				{
					return x.GetHashCode();
				}
				return StringComparer.Ordinal.GetHashCode(x.DisplayName);
			}

			// Token: 0x04002E61 RID: 11873
			public static UnspecifiedRoutingTypeDriver.AddressEqualityComparerImpl Default = new UnspecifiedRoutingTypeDriver.AddressEqualityComparerImpl();
		}
	}
}
