using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000864 RID: 2148
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ReplyTo : ParticipantList
	{
		// Token: 0x060050E2 RID: 20706 RVA: 0x001508A8 File Offset: 0x0014EAA8
		internal ReplyTo(PropertyBag propertyBag) : this(propertyBag, false)
		{
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x001508B2 File Offset: 0x0014EAB2
		internal ReplyTo(PropertyBag propertyBag, bool suppressCorruptDataException) : base(propertyBag, InternalSchema.MapiReplyToBlob, InternalSchema.MapiReplyToNames, null, suppressCorruptDataException)
		{
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x001508C8 File Offset: 0x0014EAC8
		internal static ReplyTo CreateInstance(IStorePropertyBag storePropertyBag)
		{
			if (storePropertyBag.GetValueOrDefault<bool>(MessageItemSchema.ReplyToNamesExists, false) && storePropertyBag.GetValueOrDefault<bool>(MessageItemSchema.ReplyToBlobExists, false))
			{
				string valueOrDefault = storePropertyBag.GetValueOrDefault<string>(InternalSchema.MapiReplyToNames, null);
				byte[] valueOrDefault2 = storePropertyBag.GetValueOrDefault<byte[]>(InternalSchema.MapiReplyToBlob, null);
				if (valueOrDefault != null && valueOrDefault2 != null)
				{
					PropertyBag propertyBag = new MemoryPropertyBag();
					propertyBag[InternalSchema.MapiReplyToNames] = valueOrDefault;
					propertyBag[InternalSchema.MapiReplyToBlob] = valueOrDefault2;
					return new ReplyTo(propertyBag);
				}
			}
			return null;
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x00150937 File Offset: 0x0014EB37
		protected override void InsertItem(int index, Participant participant)
		{
			if (base.Count >= 128)
			{
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), "ReplyTo exceeds the maximum number of participants");
				throw new LimitExceededException(ServerStrings.ExReplyToTooManyRecipients(128));
			}
			base.InsertItem(index, participant);
		}

		// Token: 0x04002C35 RID: 11317
		public const int MaxReplyToRecipients = 128;
	}
}
