using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IStoreSession : IDisposable
	{
		// Token: 0x06000047 RID: 71
		AggregateOperationResult Delete(DeleteItemFlags deleteFlags, params StoreId[] ids);

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000048 RID: 72
		IXSOMailbox Mailbox { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000049 RID: 73
		IActivitySession ActivitySession { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004A RID: 74
		CultureInfo Culture { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004B RID: 75
		string DisplayAddress { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004C RID: 76
		OrganizationId OrganizationId { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004D RID: 77
		Guid MdbGuid { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004E RID: 78
		IdConverter IdConverter { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004F RID: 79
		bool IsMoveUser { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000050 RID: 80
		IExchangePrincipal MailboxOwner { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000051 RID: 81
		Guid MailboxGuid { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000052 RID: 82
		LogonType LogonType { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000053 RID: 83
		SessionCapabilities Capabilities { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000054 RID: 84
		// (set) Token: 0x06000055 RID: 85
		ExTimeZone ExTimeZone { get; set; }

		// Token: 0x06000056 RID: 86
		StoreObjectId GetParentFolderId(StoreObjectId objectId);

		// Token: 0x06000057 RID: 87
		IRecipientSession GetADRecipientSession(bool isReadOnly, ConsistencyMode consistencyMode);

		// Token: 0x06000058 RID: 88
		IConfigurationSession GetADConfigurationSession(bool isReadOnly, ConsistencyMode consistencyMode);
	}
}
