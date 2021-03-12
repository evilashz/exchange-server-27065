using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001F2 RID: 498
	internal sealed class ParticipantConverter : BaseConverter
	{
		// Token: 0x06000D1A RID: 3354 RVA: 0x00042AA4 File Offset: 0x00040CA4
		public static EmailAddressWrapper ConvertParticipantToEmailAddressWrapper(IParticipant xsoEmailAddress, IdConverterWithCommandSettings idConverterWithCommandSettings)
		{
			if (xsoEmailAddress == null)
			{
				return null;
			}
			EmailAddressWrapper emailAddressWrapper = new EmailAddressWrapper
			{
				Name = xsoEmailAddress.DisplayName,
				EmailAddress = xsoEmailAddress.EmailAddress,
				RoutingType = xsoEmailAddress.RoutingType,
				MailboxType = MailboxHelper.GetMailboxType(xsoEmailAddress.Origin, xsoEmailAddress.RoutingType).ToString(),
				OriginalDisplayName = xsoEmailAddress.OriginalDisplayName
			};
			StoreParticipantOrigin storeParticipantOrigin = xsoEmailAddress.Origin as StoreParticipantOrigin;
			if (storeParticipantOrigin != null)
			{
				ConcatenatedIdAndChangeKey concatenatedId = idConverterWithCommandSettings.GetConcatenatedId(storeParticipantOrigin.OriginItemId);
				emailAddressWrapper.ItemId = new ItemId(concatenatedId.Id, concatenatedId.ChangeKey);
				emailAddressWrapper.EmailAddressIndex = storeParticipantOrigin.EmailAddressIndex.ToString();
			}
			return emailAddressWrapper;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00042B5B File Offset: 0x00040D5B
		public override object ConvertToObject(string propertyString)
		{
			return null;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00042B5E File Offset: 0x00040D5E
		public override string ConvertToString(object propertyValue)
		{
			return string.Empty;
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00042B68 File Offset: 0x00040D68
		public override object ConvertToServiceObjectValue(object propertyValue, IdConverterWithCommandSettings idConverterWithCommandSettings)
		{
			Participant xsoEmailAddress = propertyValue as Participant;
			return ParticipantConverter.ConvertParticipantToEmailAddressWrapper(xsoEmailAddress, idConverterWithCommandSettings);
		}
	}
}
