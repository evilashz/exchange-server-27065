using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200006A RID: 106
	internal class ElcTagSubAssistant : ElcSubAssistant
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x0001A60B File Offset: 0x0001880B
		public ElcTagSubAssistant(DatabaseInfo databaseInfo, ELCAssistantType elcAssistantType, ELCHealthMonitor healthMonitor) : base(databaseInfo, healthMonitor)
		{
			base.ElcAssistantType = elcAssistantType;
			this.tagProvisioner = new TagProvisioner(this);
			this.tagEnforcerManager = new TagEnforcerManager(this);
			this.ReadCalendarTaskMRMRegKeys();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001A649 File Offset: 0x00018849
		public override void OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001A651 File Offset: 0x00018851
		public override string ToString()
		{
			if (base.DebugString == null)
			{
				base.DebugString = "ELC Tag assistant for " + base.DatabaseInfo.ToString();
			}
			return base.DebugString;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001A67C File Offset: 0x0001887C
		internal override void OnWindowBegin()
		{
			ElcSubAssistant.Tracer.TraceDebug<ElcTagSubAssistant>((long)this.GetHashCode(), "{0}: OnWindowBegin entered.", this);
			ElcSubAssistant.Tracer.TraceDebug<ElcTagSubAssistant>((long)this.GetHashCode(), "{0}: OnWindowBegin exited.", this);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001A6AC File Offset: 0x000188AC
		internal void Invoke(MailboxSession mailboxSession, MailboxDataForTags mailboxDataForTags)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				this.InvokeInternal(mailboxSession, mailboxDataForTags);
			}
			finally
			{
				mailboxDataForTags.StatisticsLogEntry.TagSubAssistantProcessingTime = stopwatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001A6EC File Offset: 0x000188EC
		private void InvokeInternal(MailboxSession mailboxSession, MailboxDataForTags mailboxDataForTags)
		{
			if (mailboxSession.MailboxOwner.RecipientType == RecipientType.MailUser && !this.FaiExists(mailboxSession))
			{
				ElcSubAssistant.Tracer.TraceDebug<ElcTagSubAssistant, string>((long)this.GetHashCode(), "{0}: FAI doesn't exist for archive mailbox: {1}. Skip it.", this, mailboxSession.MailboxOwner.MailboxInfo.DisplayName);
				return;
			}
			if (!this.CheckForPolicy(mailboxDataForTags))
			{
				if (mailboxDataForTags.LitigationHoldEnabled || mailboxDataForTags.SuspendExpiration)
				{
					ElcUserTagInformation elcUserTagInformation = (ElcUserTagInformation)mailboxDataForTags.ElcUserInformation;
					elcUserTagInformation.EnsureUserConfigurationIsValid();
					elcUserTagInformation.SaveConfigItem(mailboxDataForTags.ArchiveProcessor);
				}
				return;
			}
			bool flag = this.tagProvisioner.Invoke(mailboxDataForTags);
			if (flag)
			{
				this.MapPolicyForArchive(mailboxDataForTags);
				this.tagEnforcerManager.Invoke(mailboxDataForTags);
			}
			this.TrainForAutoTagging(mailboxDataForTags);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001A79B File Offset: 0x0001899B
		internal override void OnWindowEnd()
		{
			ElcSubAssistant.Tracer.TraceDebug<ElcTagSubAssistant>((long)this.GetHashCode(), "{0}: OnWindowEnd entered.", this);
			ElcSubAssistant.Tracer.TraceDebug<ElcTagSubAssistant>((long)this.GetHashCode(), "{0}: OnWindowEnd exited.", this);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0001A7CB File Offset: 0x000189CB
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0001A7D3 File Offset: 0x000189D3
		internal int ELCAssistantCalendarTaskRetentionProcessingTimeInMinutes
		{
			get
			{
				return this.elcAssistantCalendarTaskRetentionProcessingTimeInMinutes;
			}
			set
			{
				this.elcAssistantCalendarTaskRetentionProcessingTimeInMinutes = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001A7DC File Offset: 0x000189DC
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0001A7E4 File Offset: 0x000189E4
		internal bool ELCAssistantCalendarTaskRetentionEnabled
		{
			get
			{
				return this.elcAssistantCalendarTaskRetentionEnabled;
			}
			set
			{
				this.elcAssistantCalendarTaskRetentionEnabled = value;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001A7F0 File Offset: 0x000189F0
		private bool FaiExists(MailboxSession mailboxSession)
		{
			UserConfiguration userConfiguration = ElcMailboxHelper.OpenFaiMessage(mailboxSession, "MRM", true);
			if (userConfiguration == null)
			{
				ElcSubAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "FAI cannot be found or created.");
				return false;
			}
			Dictionary<Guid, StoreTagData> dictionary = null;
			try
			{
				dictionary = MrmFaiFormatter.Deserialize(userConfiguration, mailboxSession.MailboxOwner);
			}
			catch (ObjectNotFoundException arg)
			{
				ElcSubAssistant.Tracer.TraceDebug<ObjectNotFoundException>((long)this.GetHashCode(), "Deserialize of MRM FAI message failed because it could not be found. Exception: {0}", arg);
				return false;
			}
			finally
			{
				if (userConfiguration != null)
				{
					userConfiguration.Dispose();
				}
			}
			return dictionary != null && dictionary.Count > 0;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001A88C File Offset: 0x00018A8C
		private bool CheckForPolicy(MailboxDataForTags mailboxDataForTags)
		{
			bool policyExists = mailboxDataForTags.ElcUserTagInformation.PolicyExists;
			if (!policyExists)
			{
				ElcSubAssistant.Tracer.TraceDebug<ElcTagSubAssistant, IExchangePrincipal>((long)this.GetHashCode(), "{0}: User '{1}' is not linked to any MRM policy.", this, mailboxDataForTags.MailboxSession.MailboxOwner);
				mailboxDataForTags.ElcUserTagInformation.DeleteConfigMessage(mailboxDataForTags.ArchiveProcessor);
			}
			ElcSubAssistant.Tracer.TraceDebug<ElcTagSubAssistant, IExchangePrincipal, bool>((long)this.GetHashCode(), "{0}: User: '{1}'. Policy exists: {2}.", this, mailboxDataForTags.MailboxSession.MailboxOwner, policyExists);
			return policyExists;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001A900 File Offset: 0x00018B00
		private void TrainForAutoTagging(MailboxDataForTags mailboxDataForTags)
		{
			try
			{
				if (mailboxDataForTags.PersonalTagDeleted)
				{
					ElcSubAssistant.Tracer.TraceDebug<ElcTagSubAssistant, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Will force a batch train for User '{1}' since a personal tag has been deleted.", this, mailboxDataForTags.MailboxSession.MailboxOwner);
				}
				else
				{
					ElcSubAssistant.Tracer.TraceDebug<ElcTagSubAssistant, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Will batch train if necessary for User '{1}'.", this, mailboxDataForTags.MailboxSession.MailboxOwner);
				}
			}
			catch (IWShutdownException)
			{
				base.ThrowIfShuttingDown(mailboxDataForTags.MailboxSession.MailboxOwner);
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001A984 File Offset: 0x00018B84
		private void MapPolicyForArchive(MailboxDataForTags mailboxDataForTags)
		{
			if (mailboxDataForTags.FolderArchiver != null && mailboxDataForTags.MailboxSession.MailboxOwner != null && !mailboxDataForTags.MailboxSession.MailboxOwner.MailboxInfo.IsArchive && mailboxDataForTags.MailboxSession.MailboxOwner.GetArchiveMailbox() != null)
			{
				try
				{
					mailboxDataForTags.FolderArchiver.SyncHierarchies();
				}
				catch (DataSourceOperationException arg)
				{
					ElcSubAssistant.Tracer.TraceError<IExchangePrincipal, MailboxSession, DataSourceOperationException>((long)mailboxDataForTags.MailboxSession.GetHashCode(), "{0}: Failed to connect to the the archive mailbox : {1}.\nError:\n{2}", mailboxDataForTags.MailboxSession.MailboxOwner, mailboxDataForTags.MailboxSession, arg);
				}
				catch (ObjectNotFoundException arg2)
				{
					ElcSubAssistant.Tracer.TraceDebug<IExchangePrincipal, ObjectNotFoundException>((long)mailboxDataForTags.MailboxSession.GetHashCode(), "{0}: Problems opening the archive.{1}", mailboxDataForTags.MailboxSession.MailboxOwner, arg2);
				}
				catch (StorageTransientException arg3)
				{
					ElcSubAssistant.Tracer.TraceWarning<IExchangePrincipal, MailboxSession, StorageTransientException>((long)mailboxDataForTags.MailboxSession.GetHashCode(), "{0}: Failed to connect to the the archive mailbox : {1}.\nError:\n{2}", mailboxDataForTags.MailboxSession.MailboxOwner, mailboxDataForTags.MailboxSession, arg3);
				}
				catch (StoragePermanentException arg4)
				{
					ElcSubAssistant.Tracer.TraceError<IExchangePrincipal, MailboxSession, StoragePermanentException>((long)mailboxDataForTags.MailboxSession.GetHashCode(), "{0}: Failed to connect to the the archive mailbox : {1}.\nError:\n{2}", mailboxDataForTags.MailboxSession.MailboxOwner, mailboxDataForTags.MailboxSession, arg4);
				}
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001AADC File Offset: 0x00018CDC
		private void ReadCalendarTaskMRMRegKeys()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
			{
				if (registryKey != null && registryKey.GetValue("ELCAssistantCalendarTaskRetentionEnabled") != null)
				{
					object value = registryKey.GetValue("ELCAssistantCalendarTaskRetentionEnabled");
					if (value is int && (int)value == 0)
					{
						this.elcAssistantCalendarTaskRetentionEnabled = false;
					}
				}
				if (registryKey != null && registryKey.GetValue("ELCAssistantCalendarTaskRetentionProcessingTimeInMinutes") != null)
				{
					object value2 = registryKey.GetValue("ELCAssistantCalendarTaskRetentionProcessingTimeInMinutes");
					if (value2 is int && (int)value2 >= 30 && (int)value2 <= 10080)
					{
						this.elcAssistantCalendarTaskRetentionProcessingTimeInMinutes = (int)value2;
					}
				}
			}
		}

		// Token: 0x040002FB RID: 763
		private TagProvisioner tagProvisioner;

		// Token: 0x040002FC RID: 764
		private TagEnforcerManager tagEnforcerManager;

		// Token: 0x040002FD RID: 765
		private int elcAssistantCalendarTaskRetentionProcessingTimeInMinutes = 120;

		// Token: 0x040002FE RID: 766
		private bool elcAssistantCalendarTaskRetentionEnabled = true;
	}
}
