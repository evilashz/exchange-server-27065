using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001C9 RID: 457
	internal class ContactItemSchema : CacheSchema
	{
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x00053B2B File Offset: 0x00051D2B
		internal static CachedProperty[] CachedProperties
		{
			get
			{
				return ContactItemSchema.cachedProperties;
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00053B32 File Offset: 0x00051D32
		public static void Set(ADRawEntry entry, MailRecipient recipient)
		{
			RestrictedItemSchema.Set(entry, recipient);
			CacheSchema.Set(ContactItemSchema.cachedProperties, entry, recipient);
		}

		// Token: 0x04000A83 RID: 2691
		public const string ExternalEmailAddress = "Microsoft.Exchange.Transport.DirectoryData.ExternalEmailAddress";

		// Token: 0x04000A84 RID: 2692
		public const string EmailAddresses = "Microsoft.Exchange.Transport.DirectoryData.EmailAddresses";

		// Token: 0x04000A85 RID: 2693
		public const string InternetEncoding = "Microsoft.Exchange.Transport.DirectoryData.InternetEncoding";

		// Token: 0x04000A86 RID: 2694
		public const string UseMapiRichTextFormat = "Microsoft.Exchange.Transport.DirectoryData.UseMapiRichTextFormat";

		// Token: 0x04000A87 RID: 2695
		private static CachedProperty[] cachedProperties = new CachedProperty[]
		{
			new CachedProperty(ADRecipientSchema.ExternalEmailAddress, "Microsoft.Exchange.Transport.DirectoryData.ExternalEmailAddress"),
			new CachedProperty(ADRecipientSchema.EmailAddresses, "Microsoft.Exchange.Transport.DirectoryData.EmailAddresses"),
			new CachedProperty(ADRecipientSchema.InternetEncoding, "Microsoft.Exchange.Transport.DirectoryData.InternetEncoding"),
			new CachedProperty(ADRecipientSchema.MapiRecipient, "Microsoft.Exchange.Transport.DirectoryData.UseMapiRichTextFormat")
		};
	}
}
