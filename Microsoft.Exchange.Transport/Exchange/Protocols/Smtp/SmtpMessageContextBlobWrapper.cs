using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000415 RID: 1045
	internal class SmtpMessageContextBlobWrapper : ISmtpMessageContextBlob
	{
		// Token: 0x06003035 RID: 12341 RVA: 0x000C06E5 File Offset: 0x000BE8E5
		public bool TryGetOrderedListOfBlobsToReceive(string mailCommandParameter, out MailCommandMessageContextParameters messageContextInfo)
		{
			ArgumentValidator.ThrowIfNull("mailCommandParameter", mailCommandParameter);
			return SmtpMessageContextBlob.TryGetOrderedListOfBlobsToReceive(mailCommandParameter, out messageContextInfo);
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000C06F9 File Offset: 0x000BE8F9
		public List<SmtpMessageContextBlob> GetAdvertisedMandatoryBlobs(IEhloOptions ehloOptions)
		{
			ArgumentValidator.ThrowIfNull("ehloOptions", ehloOptions);
			return SmtpMessageContextBlob.GetAdvertisedMandatoryBlobs(ehloOptions);
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x000C070C File Offset: 0x000BE90C
		public AdrcSmtpMessageContextBlob AdrcSmtpMessageContextBlob
		{
			get
			{
				return SmtpMessageContextBlob.AdrcSmtpMessageContextBlobInstance;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06003038 RID: 12344 RVA: 0x000C0713 File Offset: 0x000BE913
		public ExtendedPropertiesSmtpMessageContextBlob ExtendedPropertiesSmtpMessageContextBlob
		{
			get
			{
				return SmtpMessageContextBlob.ExtendedPropertiesSmtpMessageContextBlobInstance;
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06003039 RID: 12345 RVA: 0x000C071A File Offset: 0x000BE91A
		public FastIndexSmtpMessageContextBlob FastIndexSmtpMessageContextBlob
		{
			get
			{
				return SmtpMessageContextBlob.FastIndexSmtpMessageContextBlobInstance;
			}
		}
	}
}
