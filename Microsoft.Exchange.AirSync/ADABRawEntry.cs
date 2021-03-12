using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ABProviderFramework;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000AC RID: 172
	internal sealed class ADABRawEntry : ABRawEntry
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x00037144 File Offset: 0x00035344
		public ADABRawEntry(ADABSession ownerSession, ABPropertyDefinitionCollection properties, ADRawEntry rawEntry) : base(ownerSession, properties)
		{
			if (rawEntry == null)
			{
				throw new ArgumentNullException("rawEntry");
			}
			this.rawEntry = rawEntry;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00037164 File Offset: 0x00035364
		protected override bool InternalTryGetValue(ABPropertyDefinition property, out object value)
		{
			ADPropertyDefinition adpropertyDefinition = ADABPropertyMapper.GetADPropertyDefinition(property);
			if (adpropertyDefinition != null)
			{
				return this.rawEntry.TryGetValueWithoutDefault(adpropertyDefinition, out value);
			}
			ADABObjectId adabobjectId;
			if (property == ABObjectSchema.Id && ADABUtils.GetId(this.rawEntry, out adabobjectId))
			{
				value = adabobjectId;
				return true;
			}
			string text;
			if (property == ABObjectSchema.EmailAddress && ADABUtils.GetEmailAddress(this.rawEntry, out text))
			{
				value = text;
				return true;
			}
			object obj;
			if (property == ABObjectSchema.CanEmail && this.rawEntry.TryGetValueWithoutDefault(ADRecipientSchema.RecipientType, out obj))
			{
				value = ADABUtils.CanEmailRecipientType((RecipientType)obj);
				return true;
			}
			if (property == ABGroupSchema.MembersCount)
			{
				value = null;
				return true;
			}
			ADABObjectId adabobjectId2;
			if (property == ABGroupSchema.OwnerId && ADABUtils.GetOwnerId(this.rawEntry, out adabobjectId2))
			{
				value = adabobjectId2;
				return true;
			}
			return base.InternalTryGetValue(property, out value);
		}

		// Token: 0x040005E2 RID: 1506
		private ADRawEntry rawEntry;
	}
}
