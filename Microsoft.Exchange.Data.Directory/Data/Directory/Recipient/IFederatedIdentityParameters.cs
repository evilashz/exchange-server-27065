using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001DC RID: 476
	internal interface IFederatedIdentityParameters
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001400 RID: 5120
		ADObjectId ObjectId { get; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001401 RID: 5121
		OrganizationId OrganizationId { get; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001402 RID: 5122
		string ImmutableId { get; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001403 RID: 5123
		SmtpAddress WindowsLiveID { get; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001404 RID: 5124
		string ImmutableIdPartial { get; }
	}
}
