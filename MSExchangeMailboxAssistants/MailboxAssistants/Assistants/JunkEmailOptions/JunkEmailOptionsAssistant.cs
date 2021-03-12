using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.JunkEmailOptions;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.JunkEmailOptions
{
	// Token: 0x02000112 RID: 274
	internal sealed class JunkEmailOptionsAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x06000B29 RID: 2857 RVA: 0x00048139 File Offset: 0x00046339
		public JunkEmailOptionsAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00048144 File Offset: 0x00046344
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			if (JunkEmailOptionsAssistant.IsJunkEmailOptionsEvent(mapiEvent))
			{
				JunkEmailOptionsAssistant.Tracer.TraceDebug<MapiEventTypeFlags, MapiEventFlags>((long)this.GetHashCode(), "IsEventInteresting: EventMask {0} EventFlags {1} is interesting", mapiEvent.EventMask, mapiEvent.EventFlags);
				return true;
			}
			return false;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00048173 File Offset: 0x00046373
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			this.HandleJunkEmailOptionsEvent(mapiEvent, itemStore);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0004817D File Offset: 0x0004637D
		private static bool IsJunkEmailOptionsEvent(MapiEvent mapiEvent)
		{
			return JunkEmailOptionsAssistant.IsJunkRuleEvent(mapiEvent) || JunkEmailOptionsAssistant.IsMailboxMoveSucceededEvent(mapiEvent) || JunkEmailOptionsAssistant.IsFolderCreationEvent(mapiEvent);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00048197 File Offset: 0x00046397
		private static bool IsJunkRuleEvent(MapiEvent mapiEvent)
		{
			return string.Equals(mapiEvent.ObjectClass, "IPM.ExtendedRule.Message", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000481AA File Offset: 0x000463AA
		private static bool IsMailboxMoveSucceededEvent(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveSucceeded) != (MapiEventTypeFlags)0;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000481BE File Offset: 0x000463BE
		private static bool IsMailboxMoveToThisDatabase(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventFlags & MapiEventFlags.Destination) != MapiEventFlags.None && JunkEmailOptionsAssistant.IsMailboxMoveSucceededEvent(mapiEvent);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000481D6 File Offset: 0x000463D6
		private static bool IsFolderCreationEvent(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.ObjectCreated) != (MapiEventTypeFlags)0 && mapiEvent.ItemType == ObjectType.MAPI_FOLDER && (mapiEvent.ExtendedEventFlags & (MapiExtendedEventFlags)int.MinValue) != MapiExtendedEventFlags.None;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00048204 File Offset: 0x00046404
		private static void EnsureJunkEmailRule(MailboxSession session, MapiEvent mapiEvent)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (mapiEvent == null)
			{
				throw new ArgumentNullException("mapiEvent");
			}
			if (!session.Capabilities.CanHaveJunkEmailRule)
			{
				JunkEmailOptionsAssistant.Tracer.TraceDebug<Guid>(0L, "Skipping mailbox {0} because it can't have a junk e-mail rule.  Possibly an alternate mailbox.", session.MailboxOwner.MailboxInfo.MailboxGuid);
				return;
			}
			JunkEmailOptionsAssistant.Tracer.TraceDebug<Guid>(0L, "Ensuring junk e-mail rule for mailbox {0}", session.MailboxOwner.MailboxInfo.MailboxGuid);
			StoreObjectId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Inbox);
			StoreObjectId defaultFolderId2 = session.GetDefaultFolderId(DefaultFolderType.JunkEmail);
			if (defaultFolderId == null || defaultFolderId2 == null)
			{
				JunkEmailOptionsAssistant.Tracer.TraceDebug<Guid>(0L, "Cannot create junk e-mail rule for mailbox {0} because either the inbox or junk e-mail folder does not exist.", session.MailboxOwner.MailboxInfo.MailboxGuid);
				return;
			}
			if (JunkEmailOptionsAssistant.IsFolderCreationEvent(mapiEvent) && !JunkEmailOptionsAssistant.IsInboxOrJunkFolder(mapiEvent.ItemEntryId, defaultFolderId, defaultFolderId2))
			{
				JunkEmailOptionsAssistant.Tracer.TraceDebug(0L, "Created folder is not Inbox or Junk E-mail folder.  Skipping uninteresting event.");
				return;
			}
			JunkEmailRule.JunkEmailStatus junkEmailRuleStatus = session.GetJunkEmailRuleStatus();
			if (junkEmailRuleStatus != JunkEmailRule.JunkEmailStatus.None)
			{
				return;
			}
			JunkEmailOptionsAssistant.Tracer.TraceDebug<Guid>(0L, "Creating and enabling junk e-mail rule for mailbox {0}.", session.MailboxOwner.MailboxInfo.MailboxGuid);
			JunkEmailRule junkEmailRule = session.JunkEmailRule;
			junkEmailRule.IsEnabled = true;
			junkEmailRule.Save();
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00048320 File Offset: 0x00046520
		private static bool IsInboxOrJunkFolder(byte[] entryId, StoreObjectId inboxEntryId, StoreObjectId junkFolderEntryId)
		{
			if (entryId == null)
			{
				return false;
			}
			StoreObjectId id = StoreObjectId.FromProviderSpecificId(entryId, StoreObjectType.Folder);
			return (inboxEntryId != null && inboxEntryId.Equals(id)) || (junkFolderEntryId != null && junkFolderEntryId.Equals(id));
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00048354 File Offset: 0x00046554
		private static bool IsUserMailbox(MailboxSession session)
		{
			return session.MailboxOwner.RecipientType == RecipientType.UserMailbox || session.MailboxOwner.RecipientType == RecipientType.Invalid;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00048374 File Offset: 0x00046574
		private static bool LogFailedToEnsureJunkEmailRule(Exception e, bool transient, MailboxSession session)
		{
			JunkEmailOptionsAssistant.Tracer.TraceError<ADObjectId, Exception>(0L, "Failed to ensure junk e-mail rule for mailbox {0}.  Exception: {1}", session.MailboxOwner.ObjectId, e);
			if (transient)
			{
				return true;
			}
			if (e is ObjectNotFoundException)
			{
				return false;
			}
			JunkEmailOptionsAssistant.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_FailedToEnsureJunkEmailRule, session.MailboxOwner.MailboxInfo.MailboxGuid.ToString(), new object[]
			{
				session.MailboxOwner.ObjectId,
				e
			});
			StoragePermanentException ex = e as StoragePermanentException;
			if (ex != null)
			{
				MapiExceptionFilter.ThrowInnerIfMapiExceptionHandledbyAI(ex);
				Exception innerException = ex.InnerException;
				if (innerException != null && innerException is NonUniqueRecipientException)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00048468 File Offset: 0x00046668
		private void HandleJunkEmailOptionsEvent(MapiEvent mapiEvent, MailboxSession mailboxSession)
		{
			if (mapiEvent == null)
			{
				throw new ArgumentNullException("mapiEvent");
			}
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (!JunkEmailOptionsAssistant.IsJunkEmailOptionsEvent(mapiEvent))
			{
				JunkEmailOptionsAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "Skipping uninteresting event.");
				return;
			}
			if (!JunkEmailOptionsAssistant.IsUserMailbox(mailboxSession))
			{
				JunkEmailOptionsAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "Skipping event: non-user mailbox.");
				return;
			}
			if (mailboxSession.MailboxOwner.MailboxInfo.IsArchive)
			{
				JunkEmailOptionsAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "Skipping event: archive mailbox.");
				return;
			}
			if (mailboxSession.MailboxOwner.MailboxInfo.IsAggregated)
			{
				JunkEmailOptionsAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "Skipping event: aggregated mailbox.");
				return;
			}
			if (JunkEmailOptionsAssistant.IsJunkRuleEvent(mapiEvent))
			{
				try
				{
					MailboxTagging.TagForProcessing(mailboxSession);
				}
				catch (StoragePermanentException arg)
				{
					JunkEmailOptionsAssistant.Tracer.TraceError<string, Guid, StoragePermanentException>((long)this.GetHashCode(), "Failed at tagging mailbox {0} (GUID: {1}).  Exception: {2}", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxGuid, arg);
				}
				return;
			}
			if (JunkEmailOptionsAssistant.IsMailboxMoveToThisDatabase(mapiEvent))
			{
				MapiExceptionFilter.TryOperation(delegate()
				{
					JunkEmailOptionsAssistant.EnsureJunkEmailRule(mailboxSession, mapiEvent);
				}, (Exception exception, bool transient) => JunkEmailOptionsAssistant.LogFailedToEnsureJunkEmailRule(exception, transient, mailboxSession));
				return;
			}
			if (JunkEmailOptionsAssistant.IsFolderCreationEvent(mapiEvent))
			{
				MapiExceptionFilter.TryOperation(delegate()
				{
					JunkEmailOptionsAssistant.EnsureJunkEmailRule(mailboxSession, mapiEvent);
				}, (Exception exception, bool transient) => JunkEmailOptionsAssistant.LogFailedToEnsureJunkEmailRule(exception, transient, mailboxSession));
				return;
			}
			JunkEmailOptionsAssistant.Tracer.TraceError((long)this.GetHashCode(), "HandleJunkEmailOptionsEvent was called under unexpected conditions");
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0004867D File Offset: 0x0004687D
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00048685 File Offset: 0x00046885
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0004868D File Offset: 0x0004688D
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000707 RID: 1799
		private static readonly Trace Tracer = ExTraceGlobals.JEOAssistantTracer;

		// Token: 0x04000708 RID: 1800
		private static readonly ExEventLog EventLogger = new ExEventLog(ExTraceGlobals.JEOAssistantTracer.Category, "MSExchange Assistants");
	}
}
