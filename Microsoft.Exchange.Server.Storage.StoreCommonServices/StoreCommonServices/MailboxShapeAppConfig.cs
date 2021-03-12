using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000B1 RID: 177
	public sealed class MailboxShapeAppConfig
	{
		// Token: 0x060006EC RID: 1772 RVA: 0x00024008 File Offset: 0x00022208
		internal static MailboxShape Get(Guid mailboxGuid)
		{
			Dictionary<Guid, MailboxShape> value = ConfigurationSchema.MailboxShapeQuotas.Value;
			MailboxShape mailboxShape;
			if (value != null && value.TryGetValue(mailboxGuid, out mailboxShape))
			{
				if (ExTraceGlobals.MailboxTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.MailboxTracer.TraceInformation<Guid, long, long>(0, 0L, "Found Mailbox shape configuration override for mailbox: {0}, {1}, {2}", mailboxGuid, mailboxShape.MessagesPerFolderCountWarningQuota, mailboxShape.MessagesPerFolderCountReceiveQuota);
				}
				return mailboxShape;
			}
			if (ExTraceGlobals.MailboxTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.MailboxTracer.TraceInformation<Guid>(0, 0L, "Could not find Mailbox shape configuration override for mailbox: {0}", mailboxGuid);
			}
			return null;
		}
	}
}
