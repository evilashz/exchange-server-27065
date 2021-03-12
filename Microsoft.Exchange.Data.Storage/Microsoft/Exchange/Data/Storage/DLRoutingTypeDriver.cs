using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200090F RID: 2319
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DLRoutingTypeDriver : RoutingTypeDriver
	{
		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x06005707 RID: 22279 RVA: 0x001665CB File Offset: 0x001647CB
		internal override IEqualityComparer<IParticipant> AddressEqualityComparer
		{
			get
			{
				return DLRoutingTypeDriver.AddressEqualityComparerImpl.Default;
			}
		}

		// Token: 0x06005708 RID: 22280 RVA: 0x001665D2 File Offset: 0x001647D2
		internal override bool? IsRoutable(string routingType, StoreSession session)
		{
			return new bool?(true);
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x001665DA File Offset: 0x001647DA
		internal override bool IsRoutingTypeSupported(string routingType)
		{
			return routingType == "MAPIPDL";
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x001665E7 File Offset: 0x001647E7
		internal override void Normalize(PropertyBag participantPropertyBag)
		{
			participantPropertyBag[ParticipantSchema.DisplayType] = 5;
			base.Normalize(participantPropertyBag);
		}

		// Token: 0x0600570B RID: 22283 RVA: 0x00166601 File Offset: 0x00164801
		internal override bool TryDetectRoutingType(PropertyBag participantPropertyBag, out string routingType)
		{
			routingType = null;
			return false;
		}

		// Token: 0x0600570C RID: 22284 RVA: 0x00166607 File Offset: 0x00164807
		internal override ParticipantValidationStatus Validate(Participant participant)
		{
			if (participant.EmailAddress != null)
			{
				return ParticipantValidationStatus.AddressAndRoutingTypeMismatch;
			}
			if (PropertyError.IsPropertyNotFound(participant.TryGetProperty(ParticipantSchema.OriginItemId)))
			{
				return ParticipantValidationStatus.AddressAndOriginMismatch;
			}
			return ParticipantValidationStatus.NoError;
		}

		// Token: 0x02000910 RID: 2320
		private sealed class AddressEqualityComparerImpl : IEqualityComparer<IParticipant>
		{
			// Token: 0x0600570E RID: 22286 RVA: 0x00166634 File Offset: 0x00164834
			public bool Equals(IParticipant x, IParticipant y)
			{
				object obj = x.GetValueOrDefault<object>(ParticipantSchema.OriginItemId) as StoreObjectId;
				if (obj == null)
				{
					return x.Equals(y);
				}
				return obj.Equals(y.GetValueOrDefault<object>(ParticipantSchema.OriginItemId));
			}

			// Token: 0x0600570F RID: 22287 RVA: 0x00166670 File Offset: 0x00164870
			public int GetHashCode(IParticipant x)
			{
				object obj = x.GetValueOrDefault<object>(ParticipantSchema.OriginItemId) as StoreObjectId;
				if (obj == null)
				{
					return x.GetHashCode();
				}
				return obj.GetHashCode();
			}

			// Token: 0x04002E62 RID: 11874
			public static DLRoutingTypeDriver.AddressEqualityComparerImpl Default = new DLRoutingTypeDriver.AddressEqualityComparerImpl();
		}
	}
}
