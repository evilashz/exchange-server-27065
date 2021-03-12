using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.JunkEmailOptions;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.JunkEmailOptions
{
	// Token: 0x02000118 RID: 280
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MailboxTagging
	{
		// Token: 0x06000B64 RID: 2916 RVA: 0x000494CC File Offset: 0x000476CC
		internal static void TagForProcessing(MailboxSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			MailboxTagging.Tracer.TraceDebug<string, Guid>(0L, "Tagging mailbox {0} (GUID: {1}) for processing.", session.MailboxOwner.MailboxInfo.DisplayName, session.MailboxGuid);
			session.Mailbox.Load(new PropertyDefinition[]
			{
				MailboxSchema.JunkEmailSafeListDirty
			});
			object obj = session.Mailbox.TryGetProperty(MailboxSchema.JunkEmailSafeListDirty);
			if (obj is PropertyError || (int)obj < 1)
			{
				session.Mailbox[MailboxSchema.JunkEmailSafeListDirty] = 3;
				session.Mailbox.Save();
				session.Mailbox.Load();
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00049578 File Offset: 0x00047778
		internal static void TagForRetry(MailboxSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			MailboxTagging.Tracer.TraceDebug<string, Guid>(0L, "Tagging mailbox {0} (GUID: {1}) for retry.", session.MailboxOwner.MailboxInfo.DisplayName, session.MailboxGuid);
			session.Mailbox.Load(new PropertyDefinition[]
			{
				MailboxSchema.JunkEmailSafeListDirty
			});
			object obj = session.Mailbox.TryGetProperty(MailboxSchema.JunkEmailSafeListDirty);
			if (obj is PropertyError || (int)obj <= 1)
			{
				session.Mailbox.Delete(MailboxSchema.JunkEmailSafeListDirty);
				session.Mailbox.Save();
				session.Mailbox.Load();
				return;
			}
			int num = (int)obj - 1;
			session.Mailbox[MailboxSchema.JunkEmailSafeListDirty] = num;
			session.Mailbox.Save();
			session.Mailbox.Load();
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00049654 File Offset: 0x00047854
		internal static void TagFinishedProcessing(MailboxSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			MailboxTagging.Tracer.TraceDebug<string, Guid>(0L, "Finished processing of mailbox {0} (GUID: {1}).", session.MailboxOwner.MailboxInfo.DisplayName, session.MailboxGuid);
			session.Mailbox.Load(new PropertyDefinition[]
			{
				MailboxSchema.JunkEmailSafeListDirty
			});
			session.Mailbox.Delete(MailboxSchema.JunkEmailSafeListDirty);
			session.Mailbox.Save();
			session.Mailbox.Load();
		}

		// Token: 0x04000717 RID: 1815
		private const int UpdateSafeListMaxAttempts = 3;

		// Token: 0x04000718 RID: 1816
		private static readonly Trace Tracer = ExTraceGlobals.JEOAssistantTracer;
	}
}
