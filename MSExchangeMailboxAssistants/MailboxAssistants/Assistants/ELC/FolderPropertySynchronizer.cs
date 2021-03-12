using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000075 RID: 117
	internal sealed class FolderPropertySynchronizer : PropertySynchronizerBase
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x0001DCBC File Offset: 0x0001BEBC
		internal FolderPropertySynchronizer(MailboxDataForTags mailboxDataForTags, ElcTagSubAssistant elcAssistant, Dictionary<StoreObjectId, Synchronizer.FolderPropertySet> parentFolderIdToPropertyMap, List<object[]> entireFolderList, int indexInTree) : base(mailboxDataForTags, elcAssistant)
		{
			this.entireFolderList = entireFolderList;
			this.indexInTree = indexInTree;
			base.FolderProperties = entireFolderList[indexInTree];
			this.parentFolderIdToPropertyMap = parentFolderIdToPropertyMap;
			this.originalTagGuid = ElcMailboxHelper.GetGuidFromBytes(base.FolderProperties[2], new Guid?(Guid.Empty), true, base.FolderDisplayName).Value;
			this.originalRetentionFlags = RetentionAndArchiveFlags.None;
			if (base.FolderProperties[3] is int)
			{
				this.originalRetentionFlags = (RetentionAndArchiveFlags)base.FolderProperties[3];
				this.explicitRetention = FlagsMan.IsExplicitSet(this.originalRetentionFlags);
				this.explicitArchive = FlagsMan.IsExplicitArchiveSet(this.originalRetentionFlags);
			}
			this.originalRetentionPeriod = -1;
			if (base.FolderProperties[4] is int)
			{
				this.originalRetentionPeriod = (int)base.FolderProperties[4];
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0001DDA1 File Offset: 0x0001BFA1
		private Folder FolderSession
		{
			get
			{
				if (base.Folder == null)
				{
					base.Folder = Folder.Bind(base.ElcUserTagInformation.MailboxSession, (VersionedId)base.FolderProperties[0], Synchronizer.FolderDataColumns);
				}
				return base.Folder;
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001DDD9 File Offset: 0x0001BFD9
		public override string ToString()
		{
			if (base.DebugString == null)
			{
				base.DebugString = "Mailbox:" + base.ElcUserTagInformation.MailboxSession.MailboxOwner.ToString() + " being processed by StoreFolder. Folder:" + base.FolderDisplayName;
			}
			return base.DebugString;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001DE1C File Offset: 0x0001C01C
		internal void Update()
		{
			try
			{
				if (base.MailboxDataForTags.MailboxSession.MailboxOwner.RecipientType != RecipientType.MailUser)
				{
					this.UpdateFolderProperties();
				}
				ItemPropertySynchronizer itemPropertySynchronizer = new ItemPropertySynchronizer(base.MailboxDataForTags, base.ElcAssistant, this.FolderSession, this.GetArchiveGuid(), base.FolderProperties);
				itemPropertySynchronizer.Update();
				this.FolderIsNoLongerPending();
			}
			finally
			{
				if (base.Folder != null)
				{
					base.Folder.Dispose();
				}
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001DEA0 File Offset: 0x0001C0A0
		private void UpdateFolderProperties()
		{
			if (!(base.FolderProperties[0] is VersionedId))
			{
				PropertySynchronizerBase.Tracer.TraceError<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: We could not get id of this folder.", this);
				throw new SkipException(Strings.descUnableToGetId("Folder:" + base.FolderDisplayName, base.ElcUserTagInformation.MailboxSession.MailboxOwner.ToString()));
			}
			Guid guid = Guid.Empty;
			bool tagChanged = false;
			this.taggedByPersonalArchiveTag = false;
			this.taggedByPersonalExpiryTag = false;
			this.taggedBySystemExpiryTag = false;
			this.taggedByUncertainExpiryTag = false;
			DefaultFolderType? defaultFolderType = ElcMailboxHelper.GetDefaultFolderType(base.ElcUserTagInformation.MailboxSession, ((VersionedId)base.FolderProperties[0]).ObjectId);
			if (defaultFolderType != null)
			{
				guid = this.UpdateSystemFolder(defaultFolderType, out tagChanged);
			}
			else
			{
				guid = this.UpdatePersonalFolder(out tagChanged);
			}
			this.UpdateContentSettings(tagChanged, guid);
			this.UpdateArchiveSettings();
			this.UpdateRetentionFlags(guid);
			this.CommitChangesAlready();
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001DF84 File Offset: 0x0001C184
		private Guid UpdateSystemFolder(DefaultFolderType? systemFolderType, out bool tagChanged)
		{
			PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer, DefaultFolderType?>((long)this.GetHashCode(), "{0}: Processing system folder of type {1}.", this, systemFolderType);
			tagChanged = false;
			Guid guid = this.originalTagGuid;
			if (systemFolderType == DefaultFolderType.Root)
			{
				PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: Skipping default (IPM_Root) folder as we don't stamp that.", this);
				return guid;
			}
			Guid adTagGuidForSystemFolder = this.GetAdTagGuidForSystemFolder(systemFolderType);
			if (this.originalTagGuid.Equals(adTagGuidForSystemFolder))
			{
				PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer, DefaultFolderType?>((long)this.GetHashCode(), "{0}: The AD tag and store tag for folder type '{1}' are the same.", this, systemFolderType);
			}
			else
			{
				PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer, DefaultFolderType?>((long)this.GetHashCode(), "{0}: The AD tag and store tag for folder type '{1}' are not the same.", this, systemFolderType);
				this.SetTagProperties(adTagGuidForSystemFolder, true);
				tagChanged = true;
				guid = adTagGuidForSystemFolder;
				this.taggedBySystemExpiryTag = true;
			}
			this.SetDefaultPolicy(guid);
			return guid;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001E04C File Offset: 0x0001C24C
		private Guid UpdatePersonalFolder(out bool tagChanged)
		{
			PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: Processing personal folder.", this);
			tagChanged = false;
			Guid guid = this.originalTagGuid;
			Guid value = ElcMailboxHelper.GetGuidFromBytes(base.FolderProperties[9], new Guid?(Guid.Empty), false, base.FolderDisplayName).Value;
			if (!value.Equals(Guid.Empty) && base.ElcUserTagInformation.ContainsTag(value))
			{
				tagChanged = true;
				base.PropertiesToBeDeleted.Add(StoreObjectSchema.ExplicitPolicyTag);
				guid = value;
				this.SetTagProperties(guid, true);
				base.AddAdriftTagToFai(value);
				this.taggedByPersonalExpiryTag = true;
			}
			else
			{
				Guid parentTag = this.GetParentTag();
				if ((!FlagsMan.IsExplicitSet(this.originalRetentionFlags) || (FlagsMan.IsExplicitSet(this.originalRetentionFlags) && !base.ElcUserTagInformation.ContainsTag(this.originalTagGuid))) && !parentTag.Equals(this.originalTagGuid))
				{
					if (FlagsMan.IsExplicitSet(this.originalRetentionFlags))
					{
						base.PropertiesToBeUpdated.Add(StoreObjectSchema.ExplicitPolicyTag, this.originalTagGuid.ToByteArray());
						base.AddDeletedTag(this.originalTagGuid);
					}
					this.SetTagProperties(parentTag, false);
					tagChanged = true;
					guid = parentTag;
					PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: The AD tag and store tag for this personal folder are not the same.", this);
					if (parentTag.Equals(Guid.Empty))
					{
						if (FlagsMan.IsExplicitSet(this.originalRetentionFlags))
						{
							this.taggedByPersonalExpiryTag = true;
						}
						else if (base.ElcUserTagInformation.ContainsTag(this.originalTagGuid))
						{
							if (base.ElcUserTagInformation.GetTag(this.originalTagGuid).Tag.Type == ElcFolderType.Personal)
							{
								this.taggedByPersonalExpiryTag = true;
							}
							else
							{
								this.taggedBySystemExpiryTag = true;
							}
						}
						else
						{
							PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: The parent tag is deleted in AD. Its type is unknown.", this);
							this.taggedByUncertainExpiryTag = true;
						}
					}
					else if (base.ElcUserTagInformation.ContainsTag(parentTag))
					{
						if (base.ElcUserTagInformation.GetTag(parentTag).Tag.Type == ElcFolderType.Personal)
						{
							this.taggedByPersonalExpiryTag = true;
						}
						else
						{
							this.taggedBySystemExpiryTag = true;
						}
					}
					else
					{
						PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: The parent tag is not in AD. Its type is unknown.", this);
						this.taggedByUncertainExpiryTag = true;
					}
				}
				else if (FlagsMan.IsExplicitSet(this.originalRetentionFlags))
				{
					base.AddAdriftTagToFai(this.originalTagGuid);
				}
			}
			this.SetDefaultPolicy(guid);
			return guid;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001E2BC File Offset: 0x0001C4BC
		private void SetTagProperties(Guid tagToUpdateTo, bool explicitBit)
		{
			if (tagToUpdateTo.Equals(Guid.Empty))
			{
				if (!this.originalTagGuid.Equals(Guid.Empty))
				{
					PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: The folder tag needs to be deleted.", this);
					base.PropertiesToBeDeleted.Add(StoreObjectSchema.PolicyTag);
					base.FolderProperties[2] = null;
				}
				return;
			}
			PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: The folder tag needs to be updated.", this);
			base.PropertiesToBeUpdated.Add(StoreObjectSchema.PolicyTag, tagToUpdateTo.ToByteArray());
			base.FolderProperties[2] = tagToUpdateTo.ToByteArray();
			this.explicitRetention = explicitBit;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001E360 File Offset: 0x0001C560
		private void UpdateArchiveSettings()
		{
			PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: Processing archive tag on a folderfolder.", this);
			Guid value = ElcMailboxHelper.GetGuidFromBytes(base.FolderProperties[7], new Guid?(Guid.Empty), false, base.FolderDisplayName).Value;
			Guid value2 = ElcMailboxHelper.GetGuidFromBytes(base.FolderProperties[10], new Guid?(Guid.Empty), false, base.FolderDisplayName).Value;
			ContentSetting contentSetting = null;
			this.effectiveArchiveGuid = null;
			if (value2 != Guid.Empty)
			{
				if (base.ElcUserTagInformation.ContainsTag(value2))
				{
					contentSetting = base.ElcUserTagInformation.GetRetentionEnabledSettingForTag(value2);
					base.PropertiesToBeDeleted.Add(StoreObjectSchema.ExplicitArchiveTag);
					this.effectiveArchiveGuid = new Guid?(value2);
					base.PropertiesToBeUpdated.Add(StoreObjectSchema.ArchiveTag, this.effectiveArchiveGuid.Value.ToByteArray());
					base.AddAdriftTagToFai(value2);
					this.taggedByPersonalArchiveTag = true;
				}
			}
			else if (value != Guid.Empty)
			{
				if (base.ElcUserTagInformation.ContainsTag(value))
				{
					contentSetting = base.ElcUserTagInformation.GetRetentionEnabledSettingForTag(value);
					this.effectiveArchiveGuid = new Guid?(value);
					base.AddAdriftTagToFai(value);
				}
				else
				{
					base.PropertiesToBeUpdated.Add(StoreObjectSchema.ExplicitArchiveTag, value.ToByteArray());
					base.PropertiesToBeDeleted.Add(StoreObjectSchema.ArchiveTag);
					base.AddDeletedTag(value);
					this.taggedByPersonalArchiveTag = true;
				}
			}
			this.explicitArchive = FlagsMan.IsExplicitArchiveSet(this.originalRetentionFlags);
			if (contentSetting != null && contentSetting.AgeLimitForRetention != null)
			{
				int num = (int)contentSetting.AgeLimitForRetention.Value.TotalDays;
				if (base.FolderProperties[8] is int)
				{
					int num2 = (int)base.FolderProperties[8];
					if (num != num2)
					{
						base.PropertiesToBeUpdated.Add(StoreObjectSchema.ArchivePeriod, num);
						return;
					}
				}
				else
				{
					base.PropertiesToBeUpdated.Add(StoreObjectSchema.ArchivePeriod, num);
				}
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001E574 File Offset: 0x0001C774
		private void UpdateContentSettings(bool tagChanged, Guid updatedTagGuid)
		{
			if (!tagChanged && updatedTagGuid.Equals(Guid.Empty))
			{
				PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: No need to update ContentSettings since tag is null and there was no change.", this);
				return;
			}
			if (tagChanged && updatedTagGuid.Equals(Guid.Empty))
			{
				PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: Tag is null, so we need to delete the retention period property.", this);
				if (this.originalRetentionPeriod != -1)
				{
					base.PropertiesToBeDeleted.Add(StoreObjectSchema.RetentionPeriod);
					base.FolderProperties[4] = null;
				}
				return;
			}
			ContentSetting retentionEnabledSettingForTag = base.ElcUserTagInformation.GetRetentionEnabledSettingForTag(updatedTagGuid);
			int num;
			if (retentionEnabledSettingForTag == null || retentionEnabledSettingForTag.AgeLimitForRetention == null)
			{
				num = 0;
			}
			else
			{
				num = (int)retentionEnabledSettingForTag.AgeLimitForRetention.Value.TotalDays;
			}
			if (num != this.originalRetentionPeriod)
			{
				PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: Retention period needs to be updated.", this);
				base.PropertiesToBeUpdated.Add(StoreObjectSchema.RetentionPeriod, num);
				base.FolderProperties[4] = num;
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001E678 File Offset: 0x0001C878
		private void UpdateRetentionFlags(Guid retentionTagToUpdateTo)
		{
			RetentionAndArchiveFlags retentionAndArchiveFlags = TagAssistantHelper.UpdatePersonalTagBit((!base.ElcUserTagInformation.ContainsTag(retentionTagToUpdateTo)) ? null : base.ElcUserTagInformation.GetTag(retentionTagToUpdateTo), this.originalRetentionFlags);
			if (retentionTagToUpdateTo.Equals(Guid.Empty))
			{
				retentionAndArchiveFlags = FlagsMan.ClearAllRetentionFlags(retentionAndArchiveFlags);
			}
			else if (this.explicitRetention)
			{
				retentionAndArchiveFlags = (RetentionAndArchiveFlags)FlagsMan.SetExplicit((int)retentionAndArchiveFlags);
			}
			else
			{
				retentionAndArchiveFlags = (RetentionAndArchiveFlags)FlagsMan.ClearExplicit((int)retentionAndArchiveFlags);
			}
			if (this.effectiveArchiveGuid == null || this.effectiveArchiveGuid.Equals(Guid.Empty))
			{
				retentionAndArchiveFlags = FlagsMan.ClearAllArchiveFlags(retentionAndArchiveFlags);
			}
			else if (this.explicitArchive)
			{
				retentionAndArchiveFlags = FlagsMan.SetExplicitArchiveFlag(retentionAndArchiveFlags);
			}
			else
			{
				retentionAndArchiveFlags = FlagsMan.ClearExplicitArchiveFlag(retentionAndArchiveFlags);
			}
			retentionAndArchiveFlags = FlagsMan.SetPendingRescan(retentionAndArchiveFlags);
			retentionAndArchiveFlags = FlagsMan.ClearNeedRescan(retentionAndArchiveFlags);
			if (retentionAndArchiveFlags != this.originalRetentionFlags)
			{
				if (retentionAndArchiveFlags == RetentionAndArchiveFlags.None)
				{
					PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: The flags bit on the folder will be removed.", this);
					base.PropertiesToBeDeleted.Add(StoreObjectSchema.RetentionFlags);
					base.FolderProperties[3] = null;
					return;
				}
				PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer, RetentionAndArchiveFlags, RetentionAndArchiveFlags>((long)this.GetHashCode(), "{0}: The flags bit on the folder will be updated. ExpectedFlags: {1}. OriginalFlags: {2}. ", this, retentionAndArchiveFlags, this.originalRetentionFlags);
				base.PropertiesToBeUpdated.Add(StoreObjectSchema.RetentionFlags, (int)retentionAndArchiveFlags);
				base.FolderProperties[3] = (int)retentionAndArchiveFlags;
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001E7B8 File Offset: 0x0001C9B8
		private Guid GetAdTagGuidForSystemFolder(DefaultFolderType? systemFolderType)
		{
			ElcFolderType elcFolderType = ElcFolderType.All;
			for (int i = 0; i < ElcMailboxHelper.DefaultFolderTypeList.Length; i++)
			{
				if (ElcMailboxHelper.DefaultFolderTypeList[i] == systemFolderType)
				{
					elcFolderType = ElcMailboxHelper.ElcFolderTypeList[i];
				}
			}
			foreach (AdTagData adTagData in base.ElcUserTagInformation.GetPolicyTagsList())
			{
				if (adTagData.Tag.Type == elcFolderType)
				{
					return adTagData.Tag.RetentionId;
				}
			}
			return Guid.Empty;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001EB0C File Offset: 0x0001CD0C
		private void CommitChangesAlready()
		{
			base.ElcAssistant.ThrowIfShuttingDown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
			if (base.PropertiesToBeDeleted.Count <= 0 && base.PropertiesToBeUpdated.PropertyDefinitions.Count <= 0)
			{
				return;
			}
			ILUtil.DoTryFilterCatch(new TryDelegate(this, (UIntPtr)ldftn(<CommitChangesAlready>b__0)), new FilterDelegate(null, (UIntPtr)ldftn(ExceptionFilter)), new CatchDelegate(this, (UIntPtr)ldftn(<CommitChangesAlready>b__1)));
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001EC9E File Offset: 0x0001CE9E
		private void FolderIsNoLongerPending()
		{
			ILUtil.DoTryFilterCatch(new TryDelegate(this, (UIntPtr)ldftn(<FolderIsNoLongerPending>b__2)), new FilterDelegate(null, (UIntPtr)ldftn(ExceptionFilter)), new CatchDelegate(this, (UIntPtr)ldftn(<FolderIsNoLongerPending>b__3)));
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001ECCC File Offset: 0x0001CECC
		private Guid GetParentTag()
		{
			PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer, string>((long)this.GetHashCode(), "{0}: Start searching parent retention tag for folder: {1}", this, base.FolderDisplayName);
			Guid result = Guid.Empty;
			for (int i = this.indexInTree - 1; i >= 0; i--)
			{
				if (((VersionedId)this.entireFolderList[i][0]).ObjectId.Equals((StoreObjectId)this.entireFolderList[this.indexInTree][5]))
				{
					result = ElcMailboxHelper.GetGuidFromBytes(this.entireFolderList[i][2], new Guid?(Guid.Empty), false, base.FolderDisplayName).Value;
					PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer, object>((long)this.GetHashCode(), "{0}: Found parent tag from parent folder: {1}", this, this.entireFolderList[i][1]);
					break;
				}
			}
			if (this.parentFolderIdToPropertyMap != null && result.Equals(Guid.Empty))
			{
				PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: Cannot find parent in the current folder list. Try to find parent in parentFolderIdToPropertyMap", this);
				Synchronizer.FolderPropertySet folderPropertySet;
				if (this.parentFolderIdToPropertyMap.TryGetValue((StoreObjectId)this.entireFolderList[this.indexInTree][5], out folderPropertySet))
				{
					result = ElcMailboxHelper.GetGuidFromBytes(folderPropertySet.PolicyTagProperty, new Guid?(Guid.Empty), false, base.FolderDisplayName).Value;
					PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: Found parent in parentFolderIdToPropertyMap", this);
				}
			}
			return result;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001EE38 File Offset: 0x0001D038
		private void SetDefaultPolicy(Guid latestTagGuid)
		{
			byte[] compactDefaultPolicy = base.ElcUserTagInformation.GetCompactDefaultPolicy();
			byte[] array = base.FolderProperties[6] as byte[];
			if (compactDefaultPolicy == null && array != null)
			{
				PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: CompactDefaultPolicy does not exist on current folder.", this);
				base.PropertiesToBeDeleted.Add(FolderSchema.RetentionTagEntryId);
				return;
			}
			if (compactDefaultPolicy != null)
			{
				PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: CompactDefaultPolicy exists on current folder.", this);
				if (!ArrayComparer<byte>.Comparer.Equals(compactDefaultPolicy, array))
				{
					PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: CompactDefaultPolicy on current folder does not match the desired default policy. Updating.", this);
					base.PropertiesToBeUpdated.Add(FolderSchema.RetentionTagEntryId, compactDefaultPolicy);
				}
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001EEE0 File Offset: 0x0001D0E0
		private Guid? GetArchiveGuid()
		{
			PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer, string>((long)this.GetHashCode(), "{0}: Start searching parent archive tag for folder: {1}", this, base.FolderDisplayName);
			if (this.effectiveArchiveGuid != null)
			{
				return this.effectiveArchiveGuid;
			}
			Guid guid = Guid.Empty;
			int i = this.indexInTree - 1;
			int index = this.indexInTree;
			while (i >= 0)
			{
				if (((VersionedId)this.entireFolderList[i][0]).ObjectId.Equals((StoreObjectId)this.entireFolderList[index][5]))
				{
					guid = ElcMailboxHelper.GetGuidFromBytes(this.entireFolderList[i][7], new Guid?(Guid.Empty), false, base.FolderDisplayName).Value;
					if (!(guid == Guid.Empty) && base.ElcUserTagInformation.ContainsTag(guid))
					{
						this.effectiveArchiveGuid = new Guid?(guid);
						break;
					}
					index = i;
				}
				i--;
			}
			if (this.parentFolderIdToPropertyMap != null && (this.effectiveArchiveGuid == null || this.effectiveArchiveGuid.Value.Equals(Guid.Empty) || !base.ElcUserTagInformation.ContainsTag(this.effectiveArchiveGuid.Value)))
			{
				PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: Cannot find parent in the current folder list. Try to find parent in parentFolderIdToPropertyMap", this);
				StoreObjectId key = (StoreObjectId)this.entireFolderList[index][5];
				Synchronizer.FolderPropertySet folderPropertySet;
				while (this.parentFolderIdToPropertyMap.TryGetValue(key, out folderPropertySet))
				{
					guid = ElcMailboxHelper.GetGuidFromBytes(folderPropertySet.ArchiveTagProperty, new Guid?(Guid.Empty), false, base.FolderDisplayName).Value;
					if (!(guid == Guid.Empty) && base.ElcUserTagInformation.ContainsTag(guid))
					{
						PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: Find parent in parentFolderIdToPropertyMap. And it has intelligable archive GUID. Stop...", this);
						this.effectiveArchiveGuid = new Guid?(guid);
						break;
					}
					key = folderPropertySet.ParentFolderId;
					PropertySynchronizerBase.Tracer.TraceDebug<FolderPropertySynchronizer>((long)this.GetHashCode(), "{0}: Find parent in parentFolderIdToPropertyMap. But it has no intelligable archive GUID. Continue...", this);
				}
			}
			return this.effectiveArchiveGuid;
		}

		// Token: 0x0400035F RID: 863
		private List<object[]> entireFolderList;

		// Token: 0x04000360 RID: 864
		private int indexInTree;

		// Token: 0x04000361 RID: 865
		private Guid originalTagGuid;

		// Token: 0x04000362 RID: 866
		private RetentionAndArchiveFlags originalRetentionFlags;

		// Token: 0x04000363 RID: 867
		private int originalRetentionPeriod;

		// Token: 0x04000364 RID: 868
		private bool explicitRetention;

		// Token: 0x04000365 RID: 869
		private bool explicitArchive;

		// Token: 0x04000366 RID: 870
		private Guid? effectiveArchiveGuid;

		// Token: 0x04000367 RID: 871
		private Dictionary<StoreObjectId, Synchronizer.FolderPropertySet> parentFolderIdToPropertyMap;
	}
}
