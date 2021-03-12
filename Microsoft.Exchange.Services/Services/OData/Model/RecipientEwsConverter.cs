using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E75 RID: 3701
	internal static class RecipientEwsConverter
	{
		// Token: 0x0600604E RID: 24654 RVA: 0x0012CC00 File Offset: 0x0012AE00
		internal static Recipient ToRecipient(this SingleRecipientType singleRecipientType)
		{
			if (singleRecipientType == null)
			{
				return null;
			}
			return singleRecipientType.Mailbox.ToRecipient();
		}

		// Token: 0x0600604F RID: 24655 RVA: 0x0012CC20 File Offset: 0x0012AE20
		internal static SingleRecipientType ToSingleRecipientType(this Recipient recipient)
		{
			if (recipient == null)
			{
				return null;
			}
			return new SingleRecipientType
			{
				Mailbox = recipient.ToEmailAddressWrapper()
			};
		}

		// Token: 0x06006050 RID: 24656 RVA: 0x0012CC48 File Offset: 0x0012AE48
		internal static Recipient ToRecipient(this EmailAddressWrapper emailAddressWrapper)
		{
			if (emailAddressWrapper == null)
			{
				return null;
			}
			return new Recipient
			{
				Name = emailAddressWrapper.Name,
				Address = emailAddressWrapper.EmailAddress
			};
		}

		// Token: 0x06006051 RID: 24657 RVA: 0x0012CC7C File Offset: 0x0012AE7C
		internal static EmailAddressWrapper ToEmailAddressWrapper(this Recipient recipient)
		{
			if (recipient == null)
			{
				return null;
			}
			return new EmailAddressWrapper
			{
				EmailAddress = recipient.Address,
				Name = recipient.Name
			};
		}
	}
}
