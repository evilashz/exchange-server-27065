using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000071 RID: 113
	internal sealed class ElcUserTagInformation : ElcUserInformation, IDisposeTrackable, IDisposable
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x0001C874 File Offset: 0x0001AA74
		internal ElcUserTagInformation(MailboxSession session, Dictionary<Guid, AdTagData> allAdTags) : base(session)
		{
			this.allAdTags = allAdTags;
			this.disposeTracker = this.GetDisposeTracker();
			this.SetDependentProperties();
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001C8AC File Offset: 0x0001AAAC
		internal ElcUserTagInformation(MailboxSession session) : this(session, null)
		{
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0001C8B6 File Offset: 0x0001AAB6
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0001C8BE File Offset: 0x0001AABE
		internal bool FullCrawlRequired
		{
			get
			{
				return this.fullCrawlRequired;
			}
			set
			{
				this.fullCrawlRequired = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0001C8C7 File Offset: 0x0001AAC7
		internal bool IsArchiveMailUser
		{
			get
			{
				return base.MailboxSession.MailboxOwner.RecipientType == RecipientType.MailUser;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0001C8DC File Offset: 0x0001AADC
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0001C8E4 File Offset: 0x0001AAE4
		internal Dictionary<Guid, Guid> EffectiveGuidMapping
		{
			get
			{
				return this.effectiveGuidMapping;
			}
			set
			{
				this.effectiveGuidMapping = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0001C8ED File Offset: 0x0001AAED
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0001C8F5 File Offset: 0x0001AAF5
		internal Dictionary<Guid, AdTagData> AllAdTags
		{
			get
			{
				return this.allAdTags;
			}
			set
			{
				this.allAdTags = value;
				this.SetDependentProperties();
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001C904 File Offset: 0x0001AB04
		internal bool ContainsTag(Guid retentionId)
		{
			if (this.IsArchiveMailUser)
			{
				return this.storeTagDictionary.ContainsKey(retentionId);
			}
			return this.allAdTags.ContainsKey(retentionId);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001C927 File Offset: 0x0001AB27
		internal AdTagData GetTag(Guid retentionId)
		{
			if (this.IsArchiveMailUser)
			{
				return this.storeTagDictionary[retentionId];
			}
			return this.allAdTags[retentionId];
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001C94C File Offset: 0x0001AB4C
		internal Dictionary<Guid, AdTagData> GetAllTags()
		{
			if (this.IsArchiveMailUser)
			{
				Dictionary<Guid, AdTagData> dictionary = new Dictionary<Guid, AdTagData>();
				foreach (AdTagData adTagData in this.storeTagDictionary.Values)
				{
					dictionary.Add(adTagData.Tag.RetentionId, adTagData);
				}
				return dictionary;
			}
			return this.allAdTags;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001C9C8 File Offset: 0x0001ABC8
		internal List<AdTagData> GetPolicyTagsList()
		{
			if (this.IsArchiveMailUser)
			{
				List<AdTagData> list = new List<AdTagData>();
				foreach (AdTagData item in this.storeTagDictionary.Values)
				{
					list.Add(item);
				}
				return list;
			}
			if (this.tagsInUserPolicy != null)
			{
				return new List<AdTagData>(this.tagsInUserPolicy.Values);
			}
			return new List<AdTagData>();
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001CA50 File Offset: 0x0001AC50
		internal bool TryGetTag(Guid retentionId, out AdTagData adTag)
		{
			adTag = null;
			if (this.IsArchiveMailUser)
			{
				if (this.storeTagDictionary != null && this.storeTagDictionary.ContainsKey(retentionId))
				{
					adTag = this.storeTagDictionary[retentionId];
					return true;
				}
			}
			else if (this.allAdTags.ContainsKey(retentionId))
			{
				adTag = this.allAdTags[retentionId];
				return true;
			}
			return false;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001CAB0 File Offset: 0x0001ACB0
		internal void SetDependentProperties()
		{
			this.effectiveGuidMapping = new Dictionary<Guid, Guid>();
			if (this.allAdTags != null)
			{
				foreach (Guid guid in this.allAdTags.Keys)
				{
					AdTagData adTagData = this.allAdTags[guid];
					this.effectiveGuidMapping[adTagData.Tag.Guid] = guid;
				}
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0001CB38 File Offset: 0x0001AD38
		internal Dictionary<Guid, AdTagData> TagsInUserPolicy
		{
			get
			{
				return this.tagsInUserPolicy;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0001CB40 File Offset: 0x0001AD40
		internal Dictionary<Guid, StoreTagData> StoreTagDictionary
		{
			get
			{
				return this.storeTagDictionary;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0001CB48 File Offset: 0x0001AD48
		internal Guid DefaultAdTag
		{
			get
			{
				return this.defaultAdTag;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0001CB50 File Offset: 0x0001AD50
		internal Guid DefaultVmAdTag
		{
			get
			{
				return this.defaultVmAdTag;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0001CB58 File Offset: 0x0001AD58
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0001CB60 File Offset: 0x0001AD60
		internal List<Guid> DeletedTags
		{
			get
			{
				return this.deletedTags;
			}
			set
			{
				this.deletedTags = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0001CB69 File Offset: 0x0001AD69
		internal Guid DefaultArchiveAdTag
		{
			get
			{
				return this.defaultArchiveAdTag;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0001CB71 File Offset: 0x0001AD71
		internal bool ArchivingEnabled
		{
			get
			{
				return this.archivingEnabled;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0001CB7C File Offset: 0x0001AD7C
		internal EnhancedTimeSpan? MinDefaultMovePeriod
		{
			get
			{
				if (this.minDefaultMovePeriod == null && !this.defaultArchiveTagObjectGuid.Equals(Guid.Empty))
				{
					AdTagData adTagData = null;
					if (this.tagsInUserPolicy.TryGetValue(this.defaultArchiveTagObjectGuid, out adTagData) && adTagData != null && adTagData.ContentSettings != null)
					{
						foreach (ContentSetting contentSetting in adTagData.ContentSettings.Values)
						{
							if (this.minDefaultMovePeriod == null || this.minDefaultMovePeriod.Value > contentSetting.AgeLimitForRetention.Value)
							{
								this.minDefaultMovePeriod = new EnhancedTimeSpan?(contentSetting.AgeLimitForRetention.Value);
							}
						}
					}
				}
				return this.minDefaultMovePeriod;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0001CC68 File Offset: 0x0001AE68
		internal bool PolicyExists
		{
			get
			{
				return this.policyExists;
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001CC70 File Offset: 0x0001AE70
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Mailbox:" + base.MailboxSession.MailboxOwner.ToString() + " being processed by ElcUserTagInformation";
			}
			return this.toString;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001CCA5 File Offset: 0x0001AEA5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001CCB4 File Offset: 0x0001AEB4
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ElcUserTagInformation>(this);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001CCBC File Offset: 0x0001AEBC
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001CCD4 File Offset: 0x0001AED4
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (this.configItem != null)
					{
						this.configItem.Dispose();
						this.configItem = null;
					}
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001CD20 File Offset: 0x0001AF20
		internal bool Build()
		{
			bool result;
			try
			{
				ElcUserInformation.Tracer.TraceDebug<ElcUserTagInformation>((long)this.GetHashCode(), "{0}: Looking up ELC tag information from AD and FAI message for this mailbox.", this);
				bool flag = false;
				if (base.MailboxSession.MailboxOwner.RecipientType != RecipientType.MailUser)
				{
					flag = this.GetAdData();
					if (!flag)
					{
						if (base.LitigationHoldEnabled || base.SuspendExpiration)
						{
							this.GetStoreData();
						}
						return false;
					}
				}
				bool storeData = this.GetStoreData();
				if (!storeData)
				{
					result = false;
				}
				else
				{
					if (base.MailboxSession.MailboxOwner.RecipientType != RecipientType.MailUser)
					{
						this.GetDefaultTagInAD();
					}
					this.policyExists = ((flag && storeData) ^ (base.MailboxSession.MailboxOwner.RecipientType == RecipientType.MailUser && this.storeTagDictionary.Count > 0));
					result = this.policyExists;
				}
			}
			catch (DataValidationException ex)
			{
				ElcUserInformation.Tracer.TraceError<ElcUserTagInformation, DataValidationException>((long)this.GetHashCode(), "{0}: Invalid data found in AD. Skipping this mailbox. Exception: {1}", this, ex);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_InvalidTagDataInAD, null, new object[]
				{
					base.MailboxSession.MailboxOwner,
					ex.ToString()
				});
				throw new SkipException(ex);
			}
			return result;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001CE4C File Offset: 0x0001B04C
		internal bool SaveConfigItem(IArchiveProcessor archiveProcessor)
		{
			bool result = false;
			Exception arg = null;
			try
			{
				MrmFaiFormatter.Serialize(this.storeTagDictionary, this.storeDefaultArchiveTagDictionary, this.deletedTags, new RetentionHoldData(base.SuspendExpiration, base.ADUser.RetentionComment, base.ADUser.RetentionUrl), this.configItem, false, base.MailboxSession.MailboxOwner);
				this.configItem.Save();
				IMailboxInfo archiveMailbox = base.MailboxSession.MailboxOwner.GetArchiveMailbox();
				if (!base.MailboxSession.MailboxOwner.MailboxInfo.IsArchive && archiveProcessor != null && archiveMailbox != null && archiveMailbox.IsRemote)
				{
					CloudArchiveProcessor cloudArchiveProcessor = archiveProcessor as CloudArchiveProcessor;
					if (cloudArchiveProcessor != null && cloudArchiveProcessor.ArchiveEwsClient != null)
					{
						byte[] xmlData = MrmFaiFormatter.Serialize(this.storeTagDictionary, this.storeDefaultArchiveTagDictionary, null, new RetentionHoldData(base.SuspendExpiration, base.ADUser.RetentionComment, base.ADUser.RetentionUrl), this.fullCrawlRequired, base.MailboxSession.MailboxOwner);
						if (!cloudArchiveProcessor.SaveConfigItemInArchive(xmlData))
						{
							ElcUserInformation.Tracer.TraceError((long)this.GetHashCode(), "The MRM FAI message could not be saved to the cross-premise archive");
							return false;
						}
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				if (ex is ObjectNotFoundException || ex is ObjectExistedException || ex is SaveConflictException || ex is StorageTransientException || ex is StoragePermanentException)
				{
					result = false;
					ElcUserInformation.Tracer.TraceDebug<IExchangePrincipal, MailboxSession, Exception>((long)this.GetHashCode(), "The MRM FAI message could not be saved. Mailbox Owner {0}, Mailbox Session {1}. Exception: {2}", base.MailboxSession.MailboxOwner, base.MailboxSession, arg);
					throw new TransientMailboxException(ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001CFF8 File Offset: 0x0001B1F8
		internal void DeleteConfigMessage(IArchiveProcessor archiveProcessor)
		{
			StoreId defaultFolderId = base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			base.MailboxSession.UserConfigurationManager.DeleteFolderConfigurations(defaultFolderId, new string[]
			{
				"MRM"
			});
			IMailboxInfo archiveMailbox = base.MailboxSession.MailboxOwner.GetArchiveMailbox();
			if (!base.MailboxSession.MailboxOwner.MailboxInfo.IsArchive && archiveProcessor != null && archiveMailbox != null && archiveMailbox.IsRemote)
			{
				CloudArchiveProcessor cloudArchiveProcessor = archiveProcessor as CloudArchiveProcessor;
				if (cloudArchiveProcessor != null && cloudArchiveProcessor.ArchiveEwsClient != null)
				{
					cloudArchiveProcessor.DeleteConfigItemInArchive();
				}
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001D084 File Offset: 0x0001B284
		internal void GetDefaultTagInAD()
		{
			this.defaultAdTag = Guid.Empty;
			this.defaultVmAdTag = Guid.Empty;
			Guid key = Guid.Empty;
			Guid key2 = Guid.Empty;
			foreach (AdTagData adTagData in this.tagsInUserPolicy.Values)
			{
				if (adTagData.Tag.Type == ElcFolderType.All)
				{
					if (ElcMailboxHelper.IsArchiveTag(adTagData, true))
					{
						ElcUserInformation.Tracer.TraceDebug<ElcUserTagInformation, string>((long)this.GetHashCode(), "{0}: Default move to archive tag found: {1}", this, adTagData.Tag.Name);
						this.defaultArchiveAdTag = adTagData.Tag.RetentionId;
						this.defaultArchiveTagObjectGuid = adTagData.Tag.Guid;
						this.archivingEnabled = true;
						continue;
					}
					if (ElcMailboxHelper.IsArchiveTag(adTagData, false))
					{
						continue;
					}
					ElcUserInformation.Tracer.TraceDebug<ElcUserTagInformation, string>((long)this.GetHashCode(), "{0}: Default tag found: {1}", this, adTagData.Tag.Name);
					using (SortedDictionary<Guid, ContentSetting>.ValueCollection.Enumerator enumerator2 = adTagData.ContentSettings.Values.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ContentSetting contentSetting = enumerator2.Current;
							if (contentSetting.MessageClass.Equals(ElcMessageClass.AllMailboxContent, StringComparison.CurrentCultureIgnoreCase))
							{
								this.defaultAdTag = adTagData.Tag.RetentionId;
								key = adTagData.Tag.Guid;
								this.compactDefaultPolicy = null;
								break;
							}
							if (contentSetting.MessageClass.Equals(ElcMessageClass.VoiceMail, StringComparison.CurrentCultureIgnoreCase))
							{
								this.defaultVmAdTag = adTagData.Tag.RetentionId;
								key2 = adTagData.Tag.Guid;
								this.compactDefaultPolicy = null;
								break;
							}
						}
						continue;
					}
				}
				if (adTagData.Tag.Type != ElcFolderType.RecoverableItems && ElcMailboxHelper.IsArchiveTag(adTagData, true))
				{
					ElcUserInformation.Tracer.TraceDebug<ElcUserTagInformation, string>((long)this.GetHashCode(), "{0}: Enabled Archive tag found: {1}", this, adTagData.Tag.Name);
					this.archivingEnabled = true;
				}
			}
			this.defaultContentSettingList = new List<ElcPolicySettings>();
			if (!this.defaultAdTag.Equals(Guid.Empty))
			{
				foreach (ContentSetting elcContentSetting in this.tagsInUserPolicy[key].ContentSettings.Values)
				{
					ElcPolicySettings.ParseContentSettings(this.defaultContentSettingList, elcContentSetting);
				}
			}
			if (!this.defaultVmAdTag.Equals(Guid.Empty))
			{
				foreach (ContentSetting elcContentSetting2 in this.tagsInUserPolicy[key2].ContentSettings.Values)
				{
					ElcPolicySettings.ParseContentSettings(this.defaultContentSettingList, elcContentSetting2);
				}
			}
			if (!this.defaultArchiveAdTag.Equals(Guid.Empty))
			{
				this.storeDefaultArchiveTagDictionary = new Dictionary<Guid, StoreTagData>();
				this.storeDefaultArchiveTagDictionary[this.defaultArchiveAdTag] = new StoreTagData(this.tagsInUserPolicy[this.defaultArchiveTagObjectGuid]);
				this.defaultArchiveContentSettingList = new List<ElcPolicySettings>();
				foreach (ContentSetting elcContentSetting3 in this.tagsInUserPolicy[this.defaultArchiveTagObjectGuid].ContentSettings.Values)
				{
					ElcPolicySettings.ParseContentSettings(this.defaultArchiveContentSettingList, elcContentSetting3);
				}
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001D460 File Offset: 0x0001B660
		internal ContentSetting GetRetentionEnabledSettingForTag(Guid updatedTagGuid)
		{
			if (!this.ContainsTag(updatedTagGuid))
			{
				ElcUserInformation.Tracer.TraceError<ElcUserTagInformation, Guid>((long)this.GetHashCode(), "{0}: Tag guid {1} does not exist in the allAdTags list. The tag must have been corrupted. Return null for content setting so the item never expires.", this, updatedTagGuid);
				return null;
			}
			AdTagData tag = this.GetTag(updatedTagGuid);
			foreach (KeyValuePair<Guid, ContentSetting> keyValuePair in tag.ContentSettings)
			{
				if (keyValuePair.Value.RetentionEnabled)
				{
					return keyValuePair.Value;
				}
			}
			ElcUserInformation.Tracer.TraceDebug<ElcUserTagInformation, Guid>((long)this.GetHashCode(), "{0}: Did not find any content setting for Tag guid {1}.", this, updatedTagGuid);
			return null;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001D50C File Offset: 0x0001B70C
		internal ContentSetting GetRetentionEnabledSettingForTag(Guid updatedTagGuid, string messageClass)
		{
			if (this.tagAndClassToPolicyMapping.ContainsKey(updatedTagGuid.ToString() + messageClass))
			{
				return this.tagAndClassToPolicyMapping[updatedTagGuid.ToString() + messageClass];
			}
			ContentSetting contentSetting = null;
			if (this.ContainsTag(updatedTagGuid))
			{
				AdTagData tag = this.GetTag(updatedTagGuid);
				if (tag.Tag.Type != ElcFolderType.All)
				{
					contentSetting = this.GetRetentionEnabledSettingForTag(updatedTagGuid);
					messageClass = "*";
				}
				else
				{
					contentSetting = this.GetDefaultContentSettingForMsgClass(updatedTagGuid, messageClass);
				}
			}
			this.tagAndClassToPolicyMapping[updatedTagGuid.ToString() + messageClass] = contentSetting;
			return contentSetting;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001D5B8 File Offset: 0x0001B7B8
		internal ContentSetting GetDefaultTagContentSettingForMsgClass(Guid updatedTagGuid, string messageClass)
		{
			return this.GetContentSettingForMsgClassBasedOnTag(updatedTagGuid, messageClass);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001D5D4 File Offset: 0x0001B7D4
		internal ContentSetting GetDefaultContentSettingForMsgClass(string messageClass)
		{
			if (messageClass.StartsWith(ElcMessageClass.VoiceMail.TrimEnd(new char[]
			{
				'*'
			}), StringComparison.OrdinalIgnoreCase) && !this.defaultVmAdTag.Equals(Guid.Empty))
			{
				return this.GetDefaultContentSettingForMsgClass(this.defaultVmAdTag, messageClass);
			}
			return this.GetDefaultContentSettingForMsgClass(this.defaultAdTag, messageClass);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001D62E File Offset: 0x0001B82E
		internal byte[] GetCompactDefaultPolicy()
		{
			if (this.compactDefaultPolicy == null)
			{
				this.compactDefaultPolicy = MrmFaiFormatter.SerializeDefaultPolicy(this.storeTagDictionary, base.MailboxSession.MailboxOwner);
			}
			return this.compactDefaultPolicy;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001D65A File Offset: 0x0001B85A
		internal void EnsureUserConfigurationIsValid()
		{
			if (this.configItem == null)
			{
				this.configItem = ElcMailboxHelper.OpenFaiMessage(base.MailboxSession, "MRM", true);
				if (this.configItem == null)
				{
					throw new TransientMailboxException(Strings.descFAIAvailabilityCannotBeDetermined);
				}
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001D690 File Offset: 0x0001B890
		private ContentSetting GetDefaultContentSettingForMsgClass(Guid updatedTagGuid, string messageClass)
		{
			ContentSetting contentSettingForMsgClassBasedOnTag = this.GetContentSettingForMsgClassBasedOnTag(updatedTagGuid, messageClass);
			if (contentSettingForMsgClassBasedOnTag != null && contentSettingForMsgClassBasedOnTag.RetentionEnabled)
			{
				return contentSettingForMsgClassBasedOnTag;
			}
			return null;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001D6B8 File Offset: 0x0001B8B8
		private ContentSetting GetContentSettingForMsgClassBasedOnTag(Guid updatedTagGuid, string messageClass)
		{
			ContentSetting result = null;
			if (updatedTagGuid.Equals(this.defaultAdTag))
			{
				result = ElcPolicySettings.GetApplyingPolicy(this.defaultContentSettingList, messageClass, this.tagAndClassToPolicyMapping, this.defaultAdTag.ToString() + messageClass);
			}
			else if (updatedTagGuid.Equals(this.defaultVmAdTag))
			{
				result = ElcPolicySettings.GetApplyingPolicy(this.defaultContentSettingList, messageClass, this.tagAndClassToPolicyMapping, this.defaultVmAdTag.ToString() + messageClass);
			}
			else if (updatedTagGuid.Equals(this.defaultArchiveAdTag))
			{
				result = ElcPolicySettings.GetApplyingPolicy(this.defaultArchiveContentSettingList, messageClass, this.tagAndClassToPolicyMapping, this.defaultArchiveAdTag.ToString() + messageClass);
			}
			return result;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001D778 File Offset: 0x0001B978
		private bool GetAdData()
		{
			this.tagsInUserPolicy = AdTagReader.GetTagsInPolicy(base.MailboxSession, base.ADUser, this.allAdTags);
			if (this.tagsInUserPolicy == null)
			{
				ElcUserInformation.Tracer.TraceDebug<ElcUserTagInformation>((long)this.GetHashCode(), "{0}: User has no associated elc policy in the AD.", this);
				return false;
			}
			if (this.tagsInUserPolicy != null && this.tagsInUserPolicy.Count > 0)
			{
				ElcUserInformation.Tracer.TraceDebug<ElcUserTagInformation, int>((long)this.GetHashCode(), "{0}: User has {1} elc tags in the AD.", this, this.tagsInUserPolicy.Count);
			}
			else
			{
				ElcUserInformation.Tracer.TraceDebug<ElcUserTagInformation>((long)this.GetHashCode(), "{0}: User has no elc tags.", this);
			}
			return true;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001D818 File Offset: 0x0001BA18
		private bool GetStoreData()
		{
			if (this.configItem == null)
			{
				this.EnsureUserConfigurationIsValid();
			}
			try
			{
				this.storeTagDictionary = MrmFaiFormatter.Deserialize(this.configItem, base.MailboxSession.MailboxOwner, out this.deletedTags, out this.fullCrawlRequired);
				if (this.IsArchiveMailUser)
				{
					foreach (StoreTagData storeTagData in this.storeTagDictionary.Values)
					{
						bool flag = ElcMailboxHelper.IsArchiveTag(storeTagData, false);
						if (storeTagData.Tag.Type == ElcFolderType.All && !flag)
						{
							foreach (ContentSetting contentSetting in storeTagData.ContentSettings.Values)
							{
								if (contentSetting.MessageClass.Equals(ElcMessageClass.AllMailboxContent, StringComparison.CurrentCultureIgnoreCase))
								{
									this.defaultAdTag = storeTagData.Tag.RetentionId;
									break;
								}
								if (contentSetting.MessageClass.Equals(ElcMessageClass.VoiceMail, StringComparison.CurrentCultureIgnoreCase))
								{
									this.defaultVmAdTag = storeTagData.Tag.RetentionId;
									break;
								}
							}
						}
					}
					this.defaultContentSettingList = new List<ElcPolicySettings>();
					if (!this.defaultAdTag.Equals(Guid.Empty))
					{
						foreach (ContentSetting elcContentSetting in this.storeTagDictionary[this.defaultAdTag].ContentSettings.Values)
						{
							ElcPolicySettings.ParseContentSettings(this.defaultContentSettingList, elcContentSetting);
						}
					}
					if (!this.defaultVmAdTag.Equals(Guid.Empty))
					{
						foreach (ContentSetting elcContentSetting2 in this.storeTagDictionary[this.defaultVmAdTag].ContentSettings.Values)
						{
							ElcPolicySettings.ParseContentSettings(this.defaultContentSettingList, elcContentSetting2);
						}
					}
				}
			}
			catch (ObjectNotFoundException arg)
			{
				ElcUserInformation.Tracer.TraceDebug<ObjectNotFoundException>((long)this.GetHashCode(), "Deserialize of MRM FAI message failed because it could not be found. Exception: {0}", arg);
				return false;
			}
			return true;
		}

		// Token: 0x04000332 RID: 818
		private Dictionary<Guid, Guid> effectiveGuidMapping;

		// Token: 0x04000333 RID: 819
		private Dictionary<Guid, AdTagData> allAdTags;

		// Token: 0x04000334 RID: 820
		private Dictionary<Guid, AdTagData> tagsInUserPolicy;

		// Token: 0x04000335 RID: 821
		private Dictionary<Guid, StoreTagData> storeTagDictionary;

		// Token: 0x04000336 RID: 822
		private Guid defaultAdTag;

		// Token: 0x04000337 RID: 823
		private Guid defaultVmAdTag;

		// Token: 0x04000338 RID: 824
		private Guid defaultArchiveAdTag;

		// Token: 0x04000339 RID: 825
		private Guid defaultArchiveTagObjectGuid;

		// Token: 0x0400033A RID: 826
		private List<Guid> deletedTags = new List<Guid>();

		// Token: 0x0400033B RID: 827
		private Dictionary<Guid, StoreTagData> storeDefaultArchiveTagDictionary;

		// Token: 0x0400033C RID: 828
		private Dictionary<string, ContentSetting> tagAndClassToPolicyMapping = new Dictionary<string, ContentSetting>();

		// Token: 0x0400033D RID: 829
		private List<ElcPolicySettings> defaultContentSettingList;

		// Token: 0x0400033E RID: 830
		private List<ElcPolicySettings> defaultArchiveContentSettingList;

		// Token: 0x0400033F RID: 831
		private UserConfiguration configItem;

		// Token: 0x04000340 RID: 832
		private string toString;

		// Token: 0x04000341 RID: 833
		private byte[] compactDefaultPolicy;

		// Token: 0x04000342 RID: 834
		private bool archivingEnabled;

		// Token: 0x04000343 RID: 835
		private bool fullCrawlRequired;

		// Token: 0x04000344 RID: 836
		private EnhancedTimeSpan? minDefaultMovePeriod;

		// Token: 0x04000345 RID: 837
		private bool policyExists;

		// Token: 0x04000346 RID: 838
		private bool disposed;

		// Token: 0x04000347 RID: 839
		private readonly DisposeTracker disposeTracker;
	}
}
