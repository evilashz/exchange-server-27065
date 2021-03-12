using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200002C RID: 44
	internal interface IMessageRecipient : IEquatable<IMessageRecipient>
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E9 RID: 233
		IIdentity Identity { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000EA RID: 234
		string DisplayName { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000EB RID: 235
		string EmailAddress { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000EC RID: 236
		string SmtpAddress { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000ED RID: 237
		string SipUri { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000EE RID: 238
		string RoutingType { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000EF RID: 239
		bool IsDistributionList { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F0 RID: 240
		RecipientDisplayType RecipientDisplayType { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F1 RID: 241
		string Alias { get; }

		// Token: 0x060000F2 RID: 242
		void UpdateFromRecipient(IMessageRecipient recipient);
	}
}
