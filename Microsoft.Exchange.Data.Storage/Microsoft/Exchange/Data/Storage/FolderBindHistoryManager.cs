using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200063D RID: 1597
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FolderBindHistoryManager
	{
		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x060041C5 RID: 16837 RVA: 0x0011786E File Offset: 0x00115A6E
		private static TimeSpan HistoryRefreshTime
		{
			get
			{
				if (!AuditFeatureManager.IsFolderBindExtendedThrottlingEnabled())
				{
					return TimeSpan.FromHours(3.0);
				}
				return TimeSpan.FromHours(24.0);
			}
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x00117894 File Offset: 0x00115A94
		public FolderBindHistoryManager(CoreFolder folder)
		{
			Util.ThrowOnNullArgument(folder, "folder");
			this.currentFolder = folder;
			this.ShouldSkipAudit = false;
			this.bindingHistory.Add(this.GenerateBindingEntry());
			string[] array = this.ReadRecentHistory();
			if (array == null)
			{
				return;
			}
			string bindingEntryIdentity = this.GetBindingEntryIdentity();
			long num = (long)(DateTime.UtcNow - FolderBindHistoryManager.Epoch).TotalMinutes;
			foreach (string text in array)
			{
				if (text != null)
				{
					string[] array3 = text.Split(new char[0]);
					long num2;
					if (array3.Length == 2 && long.TryParse(array3[0], out num2))
					{
						if (bindingEntryIdentity.Equals(array3[1], StringComparison.OrdinalIgnoreCase))
						{
							if ((double)(num - num2) < FolderBindHistoryManager.HistoryRefreshTime.TotalMinutes)
							{
								this.ShouldSkipAudit = true;
							}
						}
						else if ((double)(num - num2) < FolderBindHistoryManager.HistoryRefreshTime.TotalMinutes)
						{
							this.bindingHistory.Add(text);
						}
					}
				}
			}
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x00117998 File Offset: 0x00115B98
		public void UpdateHistory(CallbackContext callbackContext)
		{
			if (this.ShouldSkipAudit)
			{
				return;
			}
			MailboxSession mailboxSession = (MailboxSession)this.currentFolder.Session;
			CoreFolder coreFolder = null;
			StoreObjectId objectId = this.currentFolder.Id.ObjectId;
			if (mailboxSession.LogonType == LogonType.Delegated || mailboxSession.LogonType == LogonType.DelegatedAdmin)
			{
				Exception ex = null;
				try
				{
					coreFolder = CoreFolder.Bind(callbackContext.SessionWithBestAccess, objectId);
					this.currentFolder = coreFolder;
				}
				catch (StoragePermanentException ex2)
				{
					ex = ex2;
				}
				catch (StorageTransientException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					ExTraceGlobals.SessionTracer.TraceWarning<StoreObjectId, Exception>((long)this.currentFolder.Session.GetHashCode(), "Failed to rebind folder {0} with Admin logon. The cached RecentBindingHistory data will not be updated. Error: {1}", objectId, ex);
					ProcessInfoEventLogger.Log(StorageEventLogConstants.Tuple_ErrorBindingFolderForFolderBindHistory, objectId.ToString(), new object[]
					{
						objectId,
						mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
						mailboxSession.MailboxGuid,
						mailboxSession.LogonType,
						IdentityHelper.SidFromLogonIdentity(mailboxSession.Identity),
						COWTriggerAction.FolderBind,
						ex
					});
					this.currentFolder = null;
				}
			}
			if (this.currentFolder != null)
			{
				try
				{
					this.currentFolder.PropertyBag.SetProperty(FolderSchema.RecentBindingHistory, this.bindingHistory.ToArray());
					FolderSaveResult folderSaveResult = this.currentFolder.Save(SaveMode.NoConflictResolutionForceSave);
					if (coreFolder == null)
					{
						this.currentFolder.PropertyBag.Load(null);
					}
					if (folderSaveResult.OperationResult != OperationResult.Succeeded)
					{
						ExTraceGlobals.SessionTracer.TraceWarning<StoreObjectId, LocalizedException>((long)this.currentFolder.Session.GetHashCode(), "Failed to save RecentBindingHistory on folder {0}. Error: {1}.", objectId, folderSaveResult.Exception);
						ProcessInfoEventLogger.Log(StorageEventLogConstants.Tuple_ErrorSavingFolderBindHistory, objectId.ToString(), new object[]
						{
							objectId,
							mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
							mailboxSession.MailboxGuid,
							mailboxSession.LogonType,
							IdentityHelper.SidFromLogonIdentity(mailboxSession.Identity),
							COWTriggerAction.FolderBind,
							folderSaveResult.Exception
						});
					}
				}
				finally
				{
					if (coreFolder != null)
					{
						coreFolder.Dispose();
					}
				}
			}
		}

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x060041C8 RID: 16840 RVA: 0x00117BF8 File Offset: 0x00115DF8
		// (set) Token: 0x060041C9 RID: 16841 RVA: 0x00117C00 File Offset: 0x00115E00
		public bool ShouldSkipAudit { get; private set; }

		// Token: 0x060041CA RID: 16842 RVA: 0x00117C0C File Offset: 0x00115E0C
		private string[] ReadRecentHistory()
		{
			string[] result = null;
			try
			{
				result = (this.currentFolder.PropertyBag.TryGetProperty(CoreFolderSchema.RecentBindingHistory) as string[]);
			}
			catch (NotInBagPropertyErrorException)
			{
			}
			return result;
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x00117C4C File Offset: 0x00115E4C
		private string GetBindingEntryIdentity()
		{
			SecurityIdentifier effectiveLogonSid = IdentityHelper.GetEffectiveLogonSid((MailboxSession)this.currentFolder.Session);
			return effectiveLogonSid.Value;
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x00117C78 File Offset: 0x00115E78
		private string GenerateBindingEntry()
		{
			string bindingEntryIdentity = this.GetBindingEntryIdentity();
			return string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				(long)(DateTime.UtcNow - FolderBindHistoryManager.Epoch).TotalMinutes,
				bindingEntryIdentity
			});
		}

		// Token: 0x0400243B RID: 9275
		private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x0400243C RID: 9276
		private CoreFolder currentFolder;

		// Token: 0x0400243D RID: 9277
		private List<string> bindingHistory = new List<string>();
	}
}
