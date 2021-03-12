using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000014 RID: 20
	internal interface INotificationManagerContext
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060000F4 RID: 244
		BudgetKey BudgetKey { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060000F5 RID: 245
		string SmtpAddress { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060000F6 RID: 246
		DeviceIdentity DeviceIdentity { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060000F7 RID: 247
		Guid MdbGuid { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060000F8 RID: 248
		int MailboxPolicyHash { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060000F9 RID: 249
		uint PolicyKey { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060000FA RID: 250
		int AirSyncVersion { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060000FB RID: 251
		ExDateTime RequestTime { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060000FC RID: 252
		Guid MailboxGuid { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060000FD RID: 253
		CommandType CommandType { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060000FE RID: 254
		// (set) Token: 0x060000FF RID: 255
		IActivityScope ActivityScope { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000100 RID: 256
		IAccountValidationContext AccountValidationContext { get; }
	}
}
