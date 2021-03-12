using System;

namespace Microsoft.Exchange.Data.Metering.Throttling
{
	// Token: 0x0200002C RID: 44
	internal enum MeteredEntity
	{
		// Token: 0x040000D5 RID: 213
		Total,
		// Token: 0x040000D6 RID: 214
		AnonymousSender,
		// Token: 0x040000D7 RID: 215
		Sender,
		// Token: 0x040000D8 RID: 216
		SenderMessageSize,
		// Token: 0x040000D9 RID: 217
		SenderSubject,
		// Token: 0x040000DA RID: 218
		SenderDomain,
		// Token: 0x040000DB RID: 219
		SenderDomainSubject,
		// Token: 0x040000DC RID: 220
		SenderDomainRecipient,
		// Token: 0x040000DD RID: 221
		Recipient,
		// Token: 0x040000DE RID: 222
		RecipientSubject,
		// Token: 0x040000DF RID: 223
		RecipientDomain,
		// Token: 0x040000E0 RID: 224
		RecipientDomainSubject,
		// Token: 0x040000E1 RID: 225
		SenderRecipient,
		// Token: 0x040000E2 RID: 226
		SenderRecipientMessageSize,
		// Token: 0x040000E3 RID: 227
		Tenant,
		// Token: 0x040000E4 RID: 228
		IPAddress,
		// Token: 0x040000E5 RID: 229
		Queue,
		// Token: 0x040000E6 RID: 230
		AccountForest,
		// Token: 0x040000E7 RID: 231
		Priority
	}
}
