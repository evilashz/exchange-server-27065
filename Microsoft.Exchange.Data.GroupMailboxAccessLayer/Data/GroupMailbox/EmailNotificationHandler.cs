using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200003C RID: 60
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmailNotificationHandler : IEmailNotificationHandler
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x0000C550 File Offset: 0x0000A750
		public EmailNotificationHandler(ADUser groupMailbox, IExchangePrincipal exchangePrincipal, string clientInfoString, IMessageComposerBuilder messageComposerBuilder)
		{
			ArgumentValidator.ThrowIfNull("groupMailbox", groupMailbox);
			ArgumentValidator.ThrowIfNull("exchangePrincipal", exchangePrincipal);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("clientInfoString", clientInfoString);
			ArgumentValidator.ThrowIfNull("messageComposerBuilder", messageComposerBuilder);
			this.pendingRecipients = new ConcurrentQueue<IMailboxLocator>();
			this.groupMailbox = groupMailbox;
			this.exchangePrincipal = exchangePrincipal;
			this.clientInfoString = clientInfoString;
			this.messageComposerBuilder = messageComposerBuilder;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
		internal ConcurrentQueue<IMailboxLocator> PendingRecipients
		{
			get
			{
				return this.pendingRecipients;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		public void AddNotificationRecipient(IMailboxLocator recipient)
		{
			ArgumentValidator.ThrowIfNull("recipient", recipient);
			EmailNotificationHandler.Tracer.TraceDebug<string, IMailboxLocator>((long)this.GetHashCode(), "EmailNotificationHandler.AddNotificationRecipient: Queuing notification. Group={0}. Recipient={1}.", this.groupMailbox.ExternalDirectoryObjectId, recipient);
			if (!EmailNotificationHandler.IsDuplicateNotification(recipient, this.groupMailbox))
			{
				this.pendingRecipients.Enqueue(recipient);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		public void SendNotification()
		{
			EmailNotificationHandler.Tracer.TraceDebug<string>((long)this.GetHashCode(), "EmailNotificationHandler.SendNotification: Processing notifications for Group {0}.", this.groupMailbox.ExternalDirectoryObjectId);
			IExtensibleLogger logger = MailboxAssociationDiagnosticsFrameFactory.Default.CreateLogger(this.groupMailbox.ExchangeGuid, this.groupMailbox.OrganizationId);
			IMailboxAssociationPerformanceTracker performanceTracker = MailboxAssociationDiagnosticsFrameFactory.Default.CreatePerformanceTracker(null);
			using (MailboxAssociationDiagnosticsFrameFactory.Default.CreateDiagnosticsFrame("EmailNotificationHandler", "SendNotification", logger, performanceTracker))
			{
				GroupMailboxAccessLayerHelper.ExecuteOperationWithRetry(logger, "EmailNotificationHandler.SendNotification", delegate
				{
					using (MailboxSession session = MailboxSession.OpenAsTransport(this.exchangePrincipal, this.clientInfoString))
					{
						EmailNotificationHandler.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "EmailNotificationHandler.SendNotification: Created transport session for mailbox {0}. Group {1}.", session.MailboxGuid, this.groupMailbox.ExternalDirectoryObjectId);
						StoreObjectId draftsFolderId = session.GetDefaultFolderId(DefaultFolderType.Drafts);
						while (!this.pendingRecipients.IsEmpty)
						{
							IMailboxLocator recipient;
							while (this.pendingRecipients.TryDequeue(out recipient))
							{
								performanceTracker.IncrementAssociationsRead();
								GroupMailboxAccessLayerHelper.ExecuteOperationWithRetry(logger, "EmailNotificationHandler.ComposeAndDeliverMessage", delegate
								{
									this.ComposeAndDeliverMessage(session, draftsFolderId, recipient, performanceTracker, logger);
								}, (Exception e) => !(e is StoragePermanentException) && GrayException.IsGrayException(e));
							}
						}
					}
				}, new Predicate<Exception>(GrayException.IsGrayException));
			}
			EmailNotificationHandler.Tracer.TraceDebug((long)this.GetHashCode(), "EmailNotificationHandler.SendNotification: Task completed.");
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000C8D0 File Offset: 0x0000AAD0
		internal static void ResetDuplicateDetection()
		{
			lock (EmailNotificationHandler.recentNotificationsLock)
			{
				EmailNotificationHandler.recentNotifications = new MruDictionary<string, DateTime>(50, StringComparer.OrdinalIgnoreCase, null);
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000C91C File Offset: 0x0000AB1C
		private static bool IsDuplicateNotification(IMailboxLocator recipient, ADUser groupMailbox)
		{
			bool result = false;
			lock (EmailNotificationHandler.recentNotificationsLock)
			{
				string key = recipient.IdentityHash + groupMailbox.ExchangeGuid;
				DateTime d;
				if (EmailNotificationHandler.recentNotifications.TryGetValue(key, out d) && DateTime.UtcNow - d <= EmailNotificationHandler.MaxDuplicateCheckTime)
				{
					result = true;
					EmailNotificationHandler.Tracer.TraceInformation<IMailboxLocator, ADUser>(0, 0L, "EmailNotificationHandler:IsDuplicateNotification. Found duplicate notification. recipient: {0}, groupMailbox: {1}", recipient, groupMailbox);
				}
				EmailNotificationHandler.recentNotifications.Add(key, DateTime.UtcNow);
			}
			return result;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000C9C0 File Offset: 0x0000ABC0
		private void ComposeAndDeliverMessage(MailboxSession session, StoreObjectId draftsFolderId, IMailboxLocator recipient, IMailboxAssociationPerformanceTracker performanceTracker, IExtensibleLogger logger)
		{
			EmailNotificationHandler.Tracer.TraceDebug<IMailboxLocator, string>((long)this.GetHashCode(), "EmailNotificationHandler.ComposeAndDeliverMessage: Processing recipient {0}. Group {1}.", recipient, this.groupMailbox.ExternalDirectoryObjectId);
			using (MessageItem messageItem = MessageItem.Create(session, draftsFolderId))
			{
				ADUser recipient2 = recipient.FindAdUser();
				IMessageComposer messageComposer = this.messageComposerBuilder.Build(recipient2);
				messageComposer.WriteToMessage(messageItem);
				bool flag = false;
				if (this.groupMailbox.WhenCreatedUTC != null)
				{
					TimeSpan timeSpan = DateTime.UtcNow - this.groupMailbox.WhenCreatedUTC.Value;
					if (timeSpan < EmailNotificationHandler.DelaySendTime)
					{
						messageItem[MessageItemSchema.DeferredSendTime] = DateTime.UtcNow + EmailNotificationHandler.DelaySendTime - timeSpan;
						flag = true;
					}
				}
				else
				{
					messageItem[MessageItemSchema.DeferredSendTime] = DateTime.UtcNow + EmailNotificationHandler.DelaySendTime;
					flag = true;
				}
				if (flag)
				{
					logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.Warning>
					{
						{
							MailboxAssociationLogSchema.Warning.Context,
							"EmailNotificationHandler"
						},
						{
							MailboxAssociationLogSchema.Warning.Message,
							string.Format("Welcome email for recipient {0}, Group {1} is delayed to send", recipient, this.groupMailbox.ExternalDirectoryObjectId)
						}
					});
					EmailNotificationHandler.Tracer.TraceDebug<IMailboxLocator, string>((long)this.GetHashCode(), "EmailNotificationHandler.ComposeAndDeliverMessage: Welcome email for recipient {0}, Group {1} is delayed to send.", recipient, this.groupMailbox.ExternalDirectoryObjectId);
				}
				StoreObjectId saveSentMessageFolder = session.GetDefaultFolderId(DefaultFolderType.TemporarySaves) ?? session.CreateDefaultFolder(DefaultFolderType.TemporarySaves);
				messageItem.Send(saveSentMessageFolder, SubmitMessageFlags.IgnoreSendAsRight);
				performanceTracker.IncrementAssociationsUpdated();
			}
		}

		// Token: 0x040000E4 RID: 228
		private const int MaxRecentNotifications = 50;

		// Token: 0x040000E5 RID: 229
		private static readonly Trace Tracer = ExTraceGlobals.GroupEmailNotificationHandlerTracer;

		// Token: 0x040000E6 RID: 230
		private static readonly TimeSpan DelaySendTime = TimeSpan.FromMinutes(15.0);

		// Token: 0x040000E7 RID: 231
		private static readonly TimeSpan MaxDuplicateCheckTime = TimeSpan.FromMinutes(2.0);

		// Token: 0x040000E8 RID: 232
		private static MruDictionary<string, DateTime> recentNotifications = new MruDictionary<string, DateTime>(50, StringComparer.OrdinalIgnoreCase, null);

		// Token: 0x040000E9 RID: 233
		private static object recentNotificationsLock = new object();

		// Token: 0x040000EA RID: 234
		private readonly ConcurrentQueue<IMailboxLocator> pendingRecipients;

		// Token: 0x040000EB RID: 235
		private readonly ADUser groupMailbox;

		// Token: 0x040000EC RID: 236
		private readonly IExchangePrincipal exchangePrincipal;

		// Token: 0x040000ED RID: 237
		private readonly string clientInfoString;

		// Token: 0x040000EE RID: 238
		private readonly IMessageComposerBuilder messageComposerBuilder;
	}
}
