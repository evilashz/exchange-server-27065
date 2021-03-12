using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200060F RID: 1551
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RestrictionInfo
	{
		// Token: 0x06003FC3 RID: 16323 RVA: 0x001095CA File Offset: 0x001077CA
		public RestrictionInfo(ContentRight usageRights, ExDateTime expiryTime, string owner)
		{
			EnumValidator.ThrowIfInvalid<ContentRight>(usageRights, "usageRights");
			this.usageRights = usageRights;
			this.expiryTime = expiryTime;
			this.conversationOwner = owner;
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06003FC4 RID: 16324 RVA: 0x001095F2 File Offset: 0x001077F2
		public ContentRight UsageRights
		{
			get
			{
				return this.usageRights;
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06003FC5 RID: 16325 RVA: 0x001095FA File Offset: 0x001077FA
		public ExDateTime ExpiryTime
		{
			get
			{
				return this.expiryTime;
			}
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06003FC6 RID: 16326 RVA: 0x00109602 File Offset: 0x00107802
		public string ConversationOwner
		{
			get
			{
				return this.conversationOwner;
			}
		}

		// Token: 0x0400232E RID: 9006
		private ContentRight usageRights;

		// Token: 0x0400232F RID: 9007
		private ExDateTime expiryTime;

		// Token: 0x04002330 RID: 9008
		private string conversationOwner;
	}
}
