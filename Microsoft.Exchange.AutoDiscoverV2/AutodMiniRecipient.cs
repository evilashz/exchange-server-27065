using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x02000008 RID: 8
	public class AutodMiniRecipient : IAutodMiniRecipient
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000296C File Offset: 0x00000B6C
		public AutodMiniRecipient(IADRawEntry rawEntry)
		{
			this.ExternalEmailAddress = (SmtpProxyAddress)rawEntry[ADRecipientSchema.ExternalEmailAddress];
			this.UserPrincipalName = (string)rawEntry[ADUserSchema.UserPrincipalName];
			this.PrimarySmtpAddress = (SmtpAddress)rawEntry[ADRecipientSchema.PrimarySmtpAddress];
			if (rawEntry[ADRecipientSchema.RecipientType] != null)
			{
				this.RecipientType = (RecipientType)rawEntry[ADRecipientSchema.RecipientType];
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000029E4 File Offset: 0x00000BE4
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000029EC File Offset: 0x00000BEC
		public SmtpProxyAddress ExternalEmailAddress { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000029F5 File Offset: 0x00000BF5
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000029FD File Offset: 0x00000BFD
		public SmtpAddress PrimarySmtpAddress { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002A06 File Offset: 0x00000C06
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002A0E File Offset: 0x00000C0E
		public string UserPrincipalName { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002A17 File Offset: 0x00000C17
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002A1F File Offset: 0x00000C1F
		public RecipientType RecipientType { get; private set; }
	}
}
