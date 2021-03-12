using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.JunkEmailOptions;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.JunkEmailOptions
{
	// Token: 0x02000114 RID: 276
	internal class JunkEmailOptionsCommiterAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x06000B45 RID: 2885 RVA: 0x000486E6 File Offset: 0x000468E6
		public JunkEmailOptionsCommiterAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x000486FC File Offset: 0x000468FC
		public void OnWorkCycleCheckpoint()
		{
			JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "OnWorkCycleCheckpoint");
			ICollection<object> collection = this.skippedMailboxes.RemoveAll();
			if (collection.Count > 0)
			{
				int num = Math.Min(collection.Count, 100);
				StringBuilder stringBuilder = new StringBuilder(num * 64);
				foreach (object obj in collection.Take(num))
				{
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(obj.ToString());
				}
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<int, DatabaseInfo, StringBuilder>((long)this.GetHashCode(), "Safe and block lists could not be maintained up-to-date for the following {0} mailboxes on database {1}. These mailboxes will be automatically retried. Mailboxes: {2}", collection.Count, base.DatabaseInfo, stringBuilder);
				JunkEmailOptionsCommiterAssistant.LogFailedToUpdateSafeLists(collection.Count, base.DatabaseInfo, stringBuilder.ToString());
			}
			this.skippedMailboxes = new JunkEmailOptionsCommiterAssistant.SkippedMailboxCollection();
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000487EC File Offset: 0x000469EC
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
			if (mailboxSession == null)
			{
				JunkEmailOptionsCommiterAssistant.Tracer.TraceError((long)this.GetHashCode(), "invokeArgs.StoreSession has to be an MailboxSession; it cannot be be null or PublicFolderSession.");
				throw new ArgumentNullException("mailboxSession");
			}
			Guid mailboxGuid = mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid;
			if (!mailboxSession.Capabilities.CanHaveJunkEmailRule)
			{
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "Skipping mailbox {0} (GUID: {1}) because it cannot have a junk e-mail rule.  Possibly an alternate mailbox.", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxGuid);
				try
				{
					MailboxTagging.TagFinishedProcessing(mailboxSession);
				}
				catch (StoragePermanentException arg)
				{
					JunkEmailOptionsCommiterAssistant.Tracer.TraceError<string, Guid, StoragePermanentException>((long)this.GetHashCode(), "Failed at tagging mailbox {0} (GUID: {1}).  Exception: {2}", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxGuid, arg);
				}
				return;
			}
			try
			{
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "Updating safe/block lists of mailbox {0} (GUID: {1})", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxGuid);
				this.UpdateSafeLists(mailboxSession);
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "Update of mailbox {0} (GUID: {1}) was successful", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxGuid);
				try
				{
					MailboxTagging.TagFinishedProcessing(mailboxSession);
				}
				catch (StoragePermanentException arg2)
				{
					JunkEmailOptionsCommiterAssistant.Tracer.TraceError<string, Guid, StoragePermanentException>((long)this.GetHashCode(), "Failed at tagging mailbox {0} (GUID: {1}).  Exception: {2}", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxGuid, arg2);
				}
			}
			catch (TransientException exception)
			{
				this.HandleExceptionAtUpdatingSafeList(exception, true, mailboxSession);
			}
			catch (JunkEmailValidationException exception2)
			{
				this.HandleExceptionAtUpdatingSafeList(exception2, false, mailboxSession);
			}
			catch (DataSourceOperationException exception3)
			{
				this.HandleExceptionAtUpdatingSafeList(exception3, false, mailboxSession);
			}
			catch (StoragePermanentException exception4)
			{
				this.HandleExceptionAtUpdatingSafeList(exception4, false, mailboxSession);
			}
			catch (DataValidationException exception5)
			{
				this.HandleExceptionAtUpdatingSafeList(exception5, false, mailboxSession);
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x000489D4 File Offset: 0x00046BD4
		private static void LogFailedToUpdateSafeLists(int failCount, DatabaseInfo database, string mailboxes)
		{
			JunkEmailOptionsCommiterAssistant.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_FailedToUpdateSafeLists, null, new object[]
			{
				failCount,
				database,
				mailboxes
			});
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00048A0C File Offset: 0x00046C0C
		private void HandleExceptionAtUpdatingSafeList(Exception exception, bool transient, MailboxSession mailboxSession)
		{
			JunkEmailOptionsCommiterAssistant.Tracer.TraceError<string, Guid, Exception>((long)this.GetHashCode(), "Update of mailbox {0} (GUID: {1}) failed with an exception: {2}", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxGuid, exception);
			this.skippedMailboxes.Add(mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxGuid);
			try
			{
				if (transient)
				{
					MailboxTagging.TagForRetry(mailboxSession);
				}
				else
				{
					MailboxTagging.TagFinishedProcessing(mailboxSession);
				}
			}
			catch (StoragePermanentException arg)
			{
				JunkEmailOptionsCommiterAssistant.Tracer.TraceError<string, Guid, StoragePermanentException>((long)this.GetHashCode(), "Failed at tagging mailbox {0} (GUID: {1}).  Exception: {2}", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxGuid, arg);
			}
			throw new SkipException(exception);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00048AC0 File Offset: 0x00046CC0
		private void UpdateSafeLists(MailboxSession mailboxSession)
		{
			ADObjectId adobjectId = (mailboxSession.MailboxOwner != null) ? mailboxSession.MailboxOwner.ObjectId : null;
			if (adobjectId == null)
			{
				JunkEmailOptionsCommiterAssistant.Tracer.TraceError<MailboxSession>((long)this.GetHashCode(), "can't determine owner of mailbox {0}", mailboxSession);
				return;
			}
			if (TemplateTenantConfiguration.IsTemplateTenant(mailboxSession.MailboxOwner.MailboxInfo.OrganizationId))
			{
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "Skipping mailbox {0} (GUID: {1}) because it belongs to a consumer tenant.", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid);
				return;
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(mailboxSession.MailboxOwner.MailboxInfo.OrganizationId), 318, "UpdateSafeLists", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\JunkEmailOptions\\JunkEmailOptionsCommiterAssistant.cs");
			ADUser aduser = tenantOrRootOrgRecipientSession.Read(adobjectId) as ADUser;
			if (aduser == null)
			{
				JunkEmailOptionsCommiterAssistant.Tracer.TraceError<ADObjectId>((long)this.GetHashCode(), "can't read user object {0} from AD.", adobjectId);
				return;
			}
			JunkEmailRule filteredJunkEmailRule = mailboxSession.FilteredJunkEmailRule;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			byte[] array = filteredJunkEmailRule.IsEnabled ? this.GetSafeSendersHash(filteredJunkEmailRule, out flag) : null;
			byte[] array2 = filteredJunkEmailRule.IsEnabled ? this.GetSafeRecipientsHash(filteredJunkEmailRule, out flag2) : null;
			byte[] array3 = filteredJunkEmailRule.IsEnabled ? this.GetBlockedSendersHash(filteredJunkEmailRule, out flag3) : null;
			bool flag4 = false;
			if (!ArrayComparer<byte>.Comparer.Equals(array, aduser.SafeSendersHash))
			{
				aduser.SafeSendersHash = array;
				flag4 = true;
			}
			if (!ArrayComparer<byte>.Comparer.Equals(array2, aduser.SafeRecipientsHash))
			{
				aduser.SafeRecipientsHash = array2;
				flag4 = true;
			}
			if (!ArrayComparer<byte>.Comparer.Equals(array3, aduser.BlockedSendersHash))
			{
				aduser.BlockedSendersHash = array3;
				flag4 = true;
			}
			if (flag4)
			{
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "Saving updated recipient object {0} to AD...", adobjectId);
				tenantOrRootOrgRecipientSession.Save(aduser);
				JunkEmailOptionsPerfCounters.TotalRecipientsUpdated.Increment();
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "Recipient object {0} was successfully saved", adobjectId);
				if (flag || flag2 || flag3)
				{
					JunkEmailOptionsPerfCounters.TotalPartialUpdates.Increment();
				}
			}
			Exception ex = null;
			try
			{
				if (flag4 || !filteredJunkEmailRule.AllRestrictionsLoaded)
				{
					filteredJunkEmailRule.Save();
				}
			}
			catch (StoragePermanentException ex2)
			{
				ex = ex2;
			}
			catch (StorageTransientException ex3)
			{
				ex = ex3;
			}
			finally
			{
				if (ex != null)
				{
					JunkEmailOptionsCommiterAssistant.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_FailedToUpdateMailbox, null, new object[]
					{
						mailboxSession.MailboxGuid,
						ex
					});
				}
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00048D3C File Offset: 0x00046F3C
		private byte[] GetSafeSendersHash(JunkEmailRule junkEmailRule, out bool oversized)
		{
			AddressHashes addressHashes = new AddressHashes();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			oversized = false;
			if (Configuration.IncludeSafeDomains)
			{
				foreach (string text in junkEmailRule.TrustedSenderDomainCollection)
				{
					if (addressHashes.Count >= Configuration.MaxSafeSenders)
					{
						break;
					}
					num++;
					addressHashes.Add(text);
					JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Adding safe sender domain {0} to list", text);
				}
				if (junkEmailRule.TrustedSenderDomainCollection.Count != num)
				{
					oversized = true;
					JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<int>((long)this.GetHashCode(), "{0} safe sender domain entries had to be skipped due to size constraints", junkEmailRule.TrustedSenderDomainCollection.Count - num);
				}
			}
			if (junkEmailRule.IsContactsFolderTrusted)
			{
				foreach (string text2 in junkEmailRule.TrustedContactsEmailCollection)
				{
					if (addressHashes.Count >= Configuration.MaxSafeSenders)
					{
						break;
					}
					num2++;
					addressHashes.Add(text2);
					JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Adding trusted contact {0} to list", text2);
				}
				if (junkEmailRule.TrustedContactsEmailCollection.Count != num2)
				{
					oversized = true;
					JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<int>((long)this.GetHashCode(), "{0} contact entries had to be skipped due to size constraints.", junkEmailRule.TrustedContactsEmailCollection.Count - num2);
				}
			}
			foreach (string text3 in junkEmailRule.TrustedSenderEmailCollection)
			{
				if (addressHashes.Count >= Configuration.MaxSafeSenders)
				{
					break;
				}
				num3++;
				addressHashes.Add(text3);
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Adding safe sender {0} to list", text3);
			}
			if (junkEmailRule.TrustedSenderEmailCollection.Count != num3)
			{
				oversized = true;
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<int>((long)this.GetHashCode(), "{0} safe sender entries had to be skipped due to size constraints.", junkEmailRule.TrustedSenderEmailCollection.Count - num3);
			}
			byte[] bytes = addressHashes.GetBytes();
			if (bytes.Length == 0)
			{
				return null;
			}
			return bytes;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00048F64 File Offset: 0x00047164
		private byte[] GetSafeRecipientsHash(JunkEmailRule junkEmailRule, out bool oversized)
		{
			AddressHashes addressHashes = new AddressHashes();
			int num = 0;
			int num2 = 0;
			oversized = false;
			if (Configuration.IncludeSafeDomains)
			{
				foreach (string text in junkEmailRule.TrustedRecipientDomainCollection)
				{
					if (addressHashes.Count >= Configuration.MaxSafeRecipients)
					{
						break;
					}
					num++;
					addressHashes.Add(text);
					JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Adding safe recipient domain {0} to list", text);
				}
				if (junkEmailRule.TrustedRecipientDomainCollection.Count != num)
				{
					oversized = true;
					JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<int>((long)this.GetHashCode(), "{0} safe recipient domain entries had to be skipped due to size constraints.", junkEmailRule.TrustedRecipientDomainCollection.Count - num);
				}
			}
			foreach (string text2 in junkEmailRule.TrustedRecipientEmailCollection)
			{
				if (addressHashes.Count >= Configuration.MaxSafeRecipients)
				{
					break;
				}
				num2++;
				addressHashes.Add(text2);
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Adding safe recipient {0} to list", text2);
			}
			if (junkEmailRule.TrustedRecipientEmailCollection.Count != num2)
			{
				oversized = true;
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<int>((long)this.GetHashCode(), "{0} safe recipient entries had to be skipped due to size constraints.", junkEmailRule.TrustedRecipientEmailCollection.Count - num2);
			}
			byte[] bytes = addressHashes.GetBytes();
			if (bytes.Length == 0)
			{
				return null;
			}
			return bytes;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000490DC File Offset: 0x000472DC
		private byte[] GetBlockedSendersHash(JunkEmailRule junkEmailRule, out bool oversized)
		{
			AddressHashes addressHashes = new AddressHashes();
			int num = 0;
			int num2 = 0;
			oversized = false;
			foreach (string text in junkEmailRule.BlockedSenderDomainCollection)
			{
				if (addressHashes.Count >= Configuration.MaxBlockedSenders)
				{
					break;
				}
				num++;
				addressHashes.Add(text);
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Adding blocked sender domain {0} to list", text);
			}
			if (junkEmailRule.BlockedSenderDomainCollection.Count != num)
			{
				oversized = true;
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<int>((long)this.GetHashCode(), "{0} blocked sender domain entries had to be skipped due to size constraints.", junkEmailRule.BlockedSenderDomainCollection.Count - num);
			}
			foreach (string text2 in junkEmailRule.BlockedSenderEmailCollection)
			{
				if (addressHashes.Count >= Configuration.MaxBlockedSenders)
				{
					break;
				}
				num2++;
				addressHashes.Add(text2);
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Adding blocked sender {0} to list", text2);
			}
			if (junkEmailRule.BlockedSenderEmailCollection.Count != num2)
			{
				oversized = true;
				JunkEmailOptionsCommiterAssistant.Tracer.TraceDebug<int>((long)this.GetHashCode(), "{0} blocked sender entries had to be skipped due to size constraints.", junkEmailRule.BlockedSenderEmailCollection.Count - num2);
			}
			byte[] bytes = addressHashes.GetBytes();
			if (bytes.Length == 0)
			{
				return null;
			}
			return bytes;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00049271 File Offset: 0x00047471
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00049279 File Offset: 0x00047479
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00049281 File Offset: 0x00047481
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x0400070B RID: 1803
		private const int MaximumMailboxesLoggedPerEvent = 100;

		// Token: 0x0400070C RID: 1804
		private static readonly Trace Tracer = ExTraceGlobals.JEOAssistantTracer;

		// Token: 0x0400070D RID: 1805
		private static readonly ExEventLog EventLogger = new ExEventLog(ExTraceGlobals.JEOAssistantTracer.Category, "MSExchange Assistants");

		// Token: 0x0400070E RID: 1806
		private JunkEmailOptionsCommiterAssistant.SkippedMailboxCollection skippedMailboxes = new JunkEmailOptionsCommiterAssistant.SkippedMailboxCollection();

		// Token: 0x02000115 RID: 277
		private sealed class SkippedMailboxCollection
		{
			// Token: 0x06000B53 RID: 2899 RVA: 0x000492A8 File Offset: 0x000474A8
			public void Add(string displayName, Guid guid)
			{
				lock (this.syncRoot)
				{
					this.mailboxes.AddLast(new JunkEmailOptionsCommiterAssistant.SkippedMailboxCollection.SkippedMailbox(displayName, guid));
				}
			}

			// Token: 0x06000B54 RID: 2900 RVA: 0x000492F8 File Offset: 0x000474F8
			public ICollection<object> RemoveAll()
			{
				ICollection<object> result = null;
				lock (this.syncRoot)
				{
					result = this.mailboxes;
					this.mailboxes = new LinkedList<object>();
				}
				return result;
			}

			// Token: 0x0400070F RID: 1807
			private object syncRoot = new object();

			// Token: 0x04000710 RID: 1808
			private LinkedList<object> mailboxes = new LinkedList<object>();

			// Token: 0x02000116 RID: 278
			private sealed class SkippedMailbox
			{
				// Token: 0x06000B55 RID: 2901 RVA: 0x00049348 File Offset: 0x00047548
				public SkippedMailbox(string displayName, Guid guid)
				{
					this.DisplayName = (displayName ?? "(unknown)");
					this.Guid = guid;
				}

				// Token: 0x06000B56 RID: 2902 RVA: 0x00049368 File Offset: 0x00047568
				public override string ToString()
				{
					return this.DisplayName + this.Guid.ToString("P");
				}

				// Token: 0x04000711 RID: 1809
				public readonly string DisplayName;

				// Token: 0x04000712 RID: 1810
				public readonly Guid Guid;
			}
		}
	}
}
