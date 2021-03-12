using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000067 RID: 103
	internal sealed class ADChangeDetector
	{
		// Token: 0x0600039F RID: 927 RVA: 0x000197AC File Offset: 0x000179AC
		public ADChangeDetector(MailboxDataForTags mailboxDataForTags)
		{
			this.mailboxDataForTags = mailboxDataForTags;
			this.elcUserInfo = (ElcUserTagInformation)this.mailboxDataForTags.ElcUserInformation;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000197D1 File Offset: 0x000179D1
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Mailbox:" + this.elcUserInfo.MailboxSession.MailboxOwner.ToString() + " being processed by ADChangeDetector.";
			}
			return this.toString;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001980C File Offset: 0x00017A0C
		internal TagChange Detect()
		{
			TagChange tagChange = new TagChange();
			tagChange.ChangeType = ChangeType.None;
			this.UpdateStoreTags(tagChange);
			this.LookForNewTagsInAD(tagChange);
			this.CheckForRecreatedTags(tagChange);
			return tagChange;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001983C File Offset: 0x00017A3C
		private void UpdateStoreTags(TagChange tagChange)
		{
			Guid[] array = new Guid[this.elcUserInfo.StoreTagDictionary.Keys.Count];
			this.elcUserInfo.StoreTagDictionary.Keys.CopyTo(array, 0);
			foreach (Guid guid in array)
			{
				if (!this.elcUserInfo.AllAdTags.ContainsKey(guid))
				{
					ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreTags: tag '{1}' has been deleted from AD.", this, this.elcUserInfo.StoreTagDictionary[guid].Tag.Name);
					this.DeleteTag(tagChange, guid);
				}
				else
				{
					ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreTags: tag '{1}' exists in AD.", this, this.elcUserInfo.StoreTagDictionary[guid].Tag.Name);
					Guid key = Guid.Empty;
					if (this.elcUserInfo.AllAdTags.ContainsKey(guid))
					{
						key = this.elcUserInfo.AllAdTags[guid].Tag.Guid;
					}
					if (key.Equals(Guid.Empty) || !this.elcUserInfo.TagsInUserPolicy.ContainsKey(key))
					{
						if (this.elcUserInfo.StoreTagDictionary[guid].Tag.Type != ElcFolderType.Personal)
						{
							ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreTags: system tag '{1}' is no longer in policy.", this, this.elcUserInfo.StoreTagDictionary[guid].Tag.Name);
							this.DeleteTag(tagChange, guid);
							goto IL_203;
						}
						if (!this.elcUserInfo.StoreTagDictionary[guid].OptedInto)
						{
							ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreTags: personal tag '{1}' is no longer in policy.", this, this.elcUserInfo.StoreTagDictionary[guid].Tag.Name);
							this.elcUserInfo.StoreTagDictionary[guid].IsVisible = false;
							this.mailboxDataForTags.PersonalTagDeleted = true;
						}
					}
					this.UpdateTagMetadata(guid);
					this.UpdateContentSettings(guid, tagChange);
				}
				IL_203:;
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00019A5C File Offset: 0x00017C5C
		private void LookForNewTagsInAD(TagChange tagChange)
		{
			foreach (Guid key in this.elcUserInfo.TagsInUserPolicy.Keys)
			{
				bool flag = ElcMailboxHelper.IsArchiveTag(this.elcUserInfo.TagsInUserPolicy[key], false);
				bool flag2 = this.elcUserInfo.MailboxSession.MailboxOwner.MailboxInfo.IsArchive && flag;
				if (!this.elcUserInfo.StoreTagDictionary.ContainsKey(this.elcUserInfo.EffectiveGuidMapping[key]))
				{
					if (this.elcUserInfo.TagsInUserPolicy[key].Tag.Type == ElcFolderType.All && flag)
					{
						ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: LookForNewTagsInAD: Default MTA tag: {1}. Skip checks for this tag.", this, this.elcUserInfo.TagsInUserPolicy[key].Tag.Name);
					}
					else
					{
						ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string, ElcFolderType>((long)this.GetHashCode(), "{0}: LookForNewTagsInAD: A new tag '{1}' of type {2} was found in AD.", this, this.elcUserInfo.TagsInUserPolicy[key].Tag.Name, this.elcUserInfo.TagsInUserPolicy[key].Tag.Type);
						if (this.elcUserInfo.TagsInUserPolicy[key].Tag.Type != ElcFolderType.Personal)
						{
							tagChange.ChangeType |= ChangeType.Other;
						}
						this.elcUserInfo.StoreTagDictionary[this.elcUserInfo.EffectiveGuidMapping[key]] = new StoreTagData(this.elcUserInfo.TagsInUserPolicy[key]);
						if (flag2)
						{
							ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: LookForNewTagsInAD: MTA tag: {1} in the archive mbx. Set IsVisible to false.", this, this.elcUserInfo.TagsInUserPolicy[key].Tag.Name);
							this.elcUserInfo.StoreTagDictionary[this.elcUserInfo.EffectiveGuidMapping[key]].IsVisible = false;
						}
						this.UpdateStoreContentSettings(this.elcUserInfo.EffectiveGuidMapping[key]);
					}
				}
				else
				{
					ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string, ElcFolderType>((long)this.GetHashCode(), "{0}: LookForNewTagsInAD: Tag '{1}' of type {2} exists in FAI.", this, this.elcUserInfo.TagsInUserPolicy[key].Tag.Name, this.elcUserInfo.TagsInUserPolicy[key].Tag.Type);
					if (this.elcUserInfo.TagsInUserPolicy[key].Tag.Type == ElcFolderType.Personal && !flag2)
					{
						this.elcUserInfo.StoreTagDictionary[this.elcUserInfo.EffectiveGuidMapping[key]].IsVisible = true;
					}
				}
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00019D3C File Offset: 0x00017F3C
		private void CheckForRecreatedTags(TagChange tagChange)
		{
			List<Guid> list = new List<Guid>();
			foreach (Guid guid in this.elcUserInfo.DeletedTags)
			{
				if (this.elcUserInfo.AllAdTags.ContainsKey(guid))
				{
					tagChange.ChangeType = ChangeType.Other;
				}
				else
				{
					list.Add(guid);
				}
			}
			this.elcUserInfo.DeletedTags = list;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00019DC4 File Offset: 0x00017FC4
		private void UpdateTagMetadata(Guid storeTagGuid)
		{
			this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.Guid = this.elcUserInfo.AllAdTags[storeTagGuid].Tag.Guid;
			this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.Name = this.elcUserInfo.AllAdTags[storeTagGuid].Tag.Name;
			this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.LocalizedRetentionPolicyTagName = this.elcUserInfo.AllAdTags[storeTagGuid].Tag.LocalizedRetentionPolicyTagName;
			this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.Comment = this.elcUserInfo.AllAdTags[storeTagGuid].Tag.Comment;
			this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.LocalizedComment = this.elcUserInfo.AllAdTags[storeTagGuid].Tag.LocalizedComment;
			this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.MustDisplayCommentEnabled = this.elcUserInfo.AllAdTags[storeTagGuid].Tag.MustDisplayCommentEnabled;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00019F18 File Offset: 0x00018118
		private void UpdateContentSettings(Guid storeTagGuid, TagChange tagChange)
		{
			bool flag = this.UpdateStoreContentSettings(storeTagGuid);
			flag |= this.LookForNewContentSettingsInAd(storeTagGuid);
			if (flag)
			{
				ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateContentSettings: Content Settings for Tag '{1}' changed.", this, this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.Name);
				tagChange.ChangeType |= ChangeType.ContentSettings;
				if (!tagChange.TagsWithContentSettingsChange.Contains(storeTagGuid))
				{
					tagChange.TagsWithContentSettingsChange.Add(storeTagGuid);
				}
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00019F98 File Offset: 0x00018198
		private bool LookForNewContentSettingsInAd(Guid storeTagGuid)
		{
			bool result = false;
			AdTagData adTagData = this.elcUserInfo.AllAdTags[storeTagGuid];
			IEnumerable<KeyValuePair<Guid, ContentSetting>> enumerable = null;
			if (adTagData.Tag.Type == ElcFolderType.All)
			{
				if (this.elcUserInfo.TagsInUserPolicy.ContainsKey(storeTagGuid))
				{
					enumerable = this.elcUserInfo.TagsInUserPolicy[storeTagGuid].ContentSettings;
				}
			}
			else
			{
				enumerable = adTagData.ContentSettings;
			}
			if (enumerable != null)
			{
				foreach (KeyValuePair<Guid, ContentSetting> keyValuePair in enumerable)
				{
					ContentSetting contentSetting = null;
					if (this.elcUserInfo.StoreTagDictionary[storeTagGuid].ContentSettings.ContainsKey(keyValuePair.Key))
					{
						contentSetting = this.elcUserInfo.StoreTagDictionary[storeTagGuid].ContentSettings[keyValuePair.Key];
					}
					if (contentSetting == null)
					{
						ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: LookForNewContentSettingsInAd: Content Settings for Tag '{1}' is null in FAI but not null in AD.", this, this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.Name);
						this.elcUserInfo.StoreTagDictionary[storeTagGuid].ContentSettings[keyValuePair.Key] = keyValuePair.Value;
						if (keyValuePair.Value.RetentionEnabled)
						{
							result = true;
						}
						else
						{
							ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: LookForNewContentSettingsInAd: Tag '{1}' is not retention enabled, recrawl skipped.", this, this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.Name);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001A140 File Offset: 0x00018340
		private bool UpdateStoreContentSettings(Guid storeTagGuid)
		{
			bool result = false;
			Guid[] array = new Guid[this.elcUserInfo.StoreTagDictionary[storeTagGuid].ContentSettings.Keys.Count];
			this.elcUserInfo.StoreTagDictionary[storeTagGuid].ContentSettings.Keys.CopyTo(array, 0);
			ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, int>((long)this.GetHashCode(), "{0}: UpdateStoreContentSettings: Length of storeCSGuids: {1}", this, array.Length);
			foreach (Guid key in array)
			{
				ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreContentSettings: storeCSGuid being updated: {1}", this, key.ToString());
				ContentSetting contentSetting = this.elcUserInfo.StoreTagDictionary[storeTagGuid].ContentSettings[key];
				ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreContentSettings: storeContentSetting.Name: {1}", this, contentSetting.Name);
				ContentSetting contentSetting2 = null;
				AdTagData adTagData = this.elcUserInfo.AllAdTags[storeTagGuid];
				ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string, ElcFolderType>((long)this.GetHashCode(), "{0}: UpdateStoreContentSettings: Retrieved tagData {1} from AllAdTags cache. Type: {2}", this, adTagData.Tag.Name, adTagData.Tag.Type);
				if (adTagData.Tag.Type == ElcFolderType.All)
				{
					if (this.elcUserInfo.TagsInUserPolicy.ContainsKey(storeTagGuid) && this.elcUserInfo.TagsInUserPolicy[storeTagGuid].ContentSettings.ContainsKey(key))
					{
						contentSetting2 = this.elcUserInfo.TagsInUserPolicy[storeTagGuid].ContentSettings[key];
						ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreContentSettings: Retrieved composite adContentSettings {1} for default tag from TagsInUserPolicy.", this, contentSetting2.Name);
					}
				}
				else if (adTagData.ContentSettings.ContainsKey(key))
				{
					contentSetting2 = adTagData.ContentSettings[key];
					ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreContentSettings: Retrieved adContentSettings {1} from AllAdTags cache.", this, contentSetting2.Name);
				}
				if (contentSetting2 == null)
				{
					ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreContentSettings: Content Settings for Tag '{1}' is null in the AD.", this, this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.Name);
					this.elcUserInfo.StoreTagDictionary[storeTagGuid].ContentSettings.Remove(key);
					result = true;
				}
				else
				{
					ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreContentSettings: Content Settings for Tag '{1}' exists in AD.", this, this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.Name);
					if (string.CompareOrdinal(contentSetting.MessageClass, contentSetting2.MessageClass) != 0)
					{
						ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreContentSettings: Message class for Content Settings for Tag '{1}' is different in the AD.", this, this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.Name);
						contentSetting.MessageClass = contentSetting2.MessageClass;
						result = true;
					}
					if (contentSetting.RetentionEnabled != contentSetting2.RetentionEnabled)
					{
						ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreContentSettings: Retention is not enabled in AD for Content Settings for Tag '{1}'", this, this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.Name);
						contentSetting.RetentionEnabled = contentSetting2.RetentionEnabled;
						result = true;
					}
					if (contentSetting2.RetentionEnabled && !contentSetting.AgeLimitForRetention.Equals(contentSetting2.AgeLimitForRetention))
					{
						ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreContentSettings: Age limit for Content Settings for Tag '{1}' is different in the AD.", this, this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.Name);
						contentSetting.AgeLimitForRetention = contentSetting2.AgeLimitForRetention;
						result = true;
					}
					if (contentSetting.RetentionAction != contentSetting2.RetentionAction)
					{
						ADChangeDetector.Tracer.TraceDebug<ADChangeDetector, string>((long)this.GetHashCode(), "{0}: UpdateStoreContentSettings: RetentionAction for Content Settings for Tag '{1}' is different in the AD.", this, this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.Name);
						contentSetting.RetentionAction = contentSetting2.RetentionAction;
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001A52C File Offset: 0x0001872C
		private void DeleteTag(TagChange tagChange, Guid storeTagGuid)
		{
			tagChange.ChangeType |= ChangeType.Other;
			tagChange.DeletedTags.Add(storeTagGuid);
			if (this.elcUserInfo.StoreTagDictionary[storeTagGuid].Tag.Type == ElcFolderType.Personal)
			{
				this.mailboxDataForTags.PersonalTagDeleted = true;
			}
			this.elcUserInfo.StoreTagDictionary.Remove(storeTagGuid);
		}

		// Token: 0x040002F2 RID: 754
		private static readonly Trace Tracer = ExTraceGlobals.TagProvisionerTracer;

		// Token: 0x040002F3 RID: 755
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x040002F4 RID: 756
		private string toString;

		// Token: 0x040002F5 RID: 757
		private ElcUserTagInformation elcUserInfo;

		// Token: 0x040002F6 RID: 758
		private MailboxDataForTags mailboxDataForTags;
	}
}
