using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200007D RID: 125
	internal sealed class ItemPropertySynchronizer : PropertySynchronizerBase
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x000203F8 File Offset: 0x0001E5F8
		internal ItemPropertySynchronizer(MailboxDataForTags mailboxDataForTags, ElcTagSubAssistant elcAssistant, Folder folder, Guid? inheritedArchiveGuid, object[] folderProperties) : base(mailboxDataForTags, elcAssistant)
		{
			base.Folder = folder;
			base.FolderProperties = folderProperties;
			this.inheritedArchiveGuid = inheritedArchiveGuid;
			if (folder.Id.ObjectId.Equals(base.ElcUserTagInformation.MailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems)))
			{
				this.folderType = DefaultFolderType.DeletedItems;
				return;
			}
			if (ObjectClass.IsCalendarFolder(folder.ClassName))
			{
				this.folderType = DefaultFolderType.Calendar;
				return;
			}
			if (ObjectClass.IsTaskFolder(folder.ClassName))
			{
				this.folderType = DefaultFolderType.Tasks;
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00020479 File Offset: 0x0001E679
		public override string ToString()
		{
			if (base.DebugString == null)
			{
				base.DebugString = "Mailbox:" + base.ElcUserTagInformation.MailboxSession.MailboxOwner.ToString() + " being processed by ItemPropertySynchronizer. Folder:" + base.FolderDisplayName;
			}
			return base.DebugString;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000204BC File Offset: 0x0001E6BC
		public void UpdateArchiveContentSettingProperties(bool tagChanged, object[] itemProperties, string itemClass, Guid effectiveArchiveGuid, DateTime updatedStartDate)
		{
			if (!tagChanged && effectiveArchiveGuid.Equals(Guid.Empty))
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: No need to update Archive ContentSettings since tag is null and there was no change.", this);
				return;
			}
			int num = -1;
			if (itemProperties[this.propertyIndexHolder.ArchivePeriodIndex] is int)
			{
				num = (int)itemProperties[this.propertyIndexHolder.ArchivePeriodIndex];
			}
			DateTime dateTime = DateTime.MinValue;
			if (itemProperties[this.propertyIndexHolder.ArchiveDateIndex] is ExDateTime)
			{
				dateTime = (DateTime)((ExDateTime)itemProperties[this.propertyIndexHolder.ArchiveDateIndex]).ToUtc();
			}
			if (effectiveArchiveGuid.Equals(Guid.Empty))
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Tag is null, so we need to delete the Archive Date & Period.", this);
				if (dateTime != DateTime.MinValue)
				{
					base.PropertiesToBeDeleted.Add(ItemSchema.ArchiveDate);
				}
				if (num != -1)
				{
					base.PropertiesToBeDeleted.Add(StoreObjectSchema.ArchivePeriod);
				}
				return;
			}
			int retentionPeriod = this.GetRetentionPeriod(effectiveArchiveGuid, itemClass);
			DateTime retentionDate = this.GetRetentionDate(updatedStartDate, retentionPeriod);
			if (itemProperties[this.propertyIndexHolder.ArchivePeriodIndex] is int && num != retentionPeriod)
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Archive period needs to be updated.", this);
				base.PropertiesToBeUpdated.Add(StoreObjectSchema.ArchivePeriod, retentionPeriod);
			}
			if (!TagAssistantHelper.DateSlushyEquals(new DateTime?(retentionDate), new DateTime?(dateTime)))
			{
				if (retentionDate == DateTime.MinValue)
				{
					PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: archive date needs to be deleted.", this);
					base.PropertiesToBeDeleted.Add(ItemSchema.ArchiveDate);
					return;
				}
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: archive date needs to be updated.", this);
				base.PropertiesToBeUpdated.Add(ItemSchema.ArchiveDate, retentionDate);
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0002067C File Offset: 0x0001E87C
		internal void Update()
		{
			bool processEhaMigratedMessages = base.MailboxDataForTags.ElcUserInformation.ProcessEhaMigratedMessages;
			using (IEnumerator<List<object[]>> allItemsIterator = this.GetAllItemsIterator())
			{
				while (allItemsIterator != null && allItemsIterator.MoveNext())
				{
					List<object[]> list = allItemsIterator.Current;
					VersionedId versionedId = null;
					Guid empty = Guid.Empty;
					Guid empty2 = Guid.Empty;
					DateTime updatedStartDate = DateTime.MaxValue;
					Guid updatedTagGuid = Guid.Empty;
					foreach (object[] array in list)
					{
						versionedId = (array[this.propertyIndexHolder.IdIndex] as VersionedId);
						if (versionedId == null)
						{
							PropertySynchronizerBase.Tracer.TraceError<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: We could not get id of this item.", this);
						}
						else
						{
							if (processEhaMigratedMessages)
							{
								object obj = array[this.propertyIndexHolder.EHAMigrationExpiryDateIndex];
								if (obj != null && obj is ExDateTime)
								{
									PropertySynchronizerBase.Tracer.TraceError<ItemPropertySynchronizer, object>((long)this.GetHashCode(), "{0}: We dont tag eha migration messages, this message is stamped with migration expiration date from eha. Expiration date is {1}", this, obj);
									continue;
								}
							}
							base.PropertiesToBeDeleted.Clear();
							base.PropertiesToBeUpdated.Clear();
							bool tagChanged = false;
							this.taggedByDefaultExpiryTag = false;
							this.taggedByPersonalArchiveTag = false;
							this.taggedByPersonalExpiryTag = false;
							this.taggedBySystemExpiryTag = false;
							this.taggedByUncertainExpiryTag = false;
							string text = array[this.propertyIndexHolder.ItemClassIndex] as string;
							text = ((text == null) ? string.Empty : ElcPolicySettings.GetEffectiveItemClass(text).ToLower());
							if (TagAssistantHelper.IsRetainableItem(text) && !this.ShouldSkipItem(array) && !this.IsRuleMessageItem(text))
							{
								try
								{
									empty = Guid.Empty;
									empty2 = Guid.Empty;
									updatedStartDate = DateTime.MaxValue;
									updatedTagGuid = this.UpdateTagProperties(out tagChanged, out empty, text, array);
									bool tagChanged2 = this.UpdateArchiveTag(array, out empty2);
									if (!updatedTagGuid.Equals(Guid.Empty) || !empty2.Equals(Guid.Empty))
									{
										PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Need to set StartDate since no tag is present.", this);
										updatedStartDate = this.UpdateStartDateEtc(versionedId, text, array);
									}
									this.UpdateContentSettingProperties(tagChanged, updatedTagGuid, updatedStartDate, text, array);
									this.UpdateRetentionFlags(empty, tagChanged, updatedTagGuid, empty2, array);
									this.UpdateArchiveContentSettingProperties(tagChanged2, array, text, empty2, updatedStartDate);
									this.CommitChangesAlready(versionedId);
								}
								catch (ArgumentOutOfRangeException arg)
								{
									PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer, string, ArgumentOutOfRangeException>((long)this.GetHashCode(), "{0} Corrupted Data. Skip current item {1}. Exception: {2}", this, versionedId.ObjectId.ToHexEntryId(), arg);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00020938 File Offset: 0x0001EB38
		private bool ShouldSkipItem(object[] itemProperties)
		{
			string itemClass = itemProperties[this.propertyIndexHolder.ItemClassIndex] as string;
			if (this.IsConflictableItem(itemClass))
			{
				if (!base.ElcAssistant.ELCAssistantCalendarTaskRetentionEnabled)
				{
					return true;
				}
				ExDateTime? exDateTime = itemProperties[this.propertyIndexHolder.LastModifiedTime] as ExDateTime?;
				if (exDateTime == null)
				{
					exDateTime = (itemProperties[this.propertyIndexHolder.CreationTimeIndex] as ExDateTime?);
				}
				if (exDateTime == null)
				{
					PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Item does not have PR_CREATION_TIME or PR_LAST_MODIFICATION_TIME.  Skipping.", this);
					return true;
				}
				if (ExDateTime.Compare(ExDateTime.GetNow(null), exDateTime.Value, TimeSpan.FromMinutes((double)base.ElcAssistant.ELCAssistantCalendarTaskRetentionProcessingTimeInMinutes)) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000209F8 File Offset: 0x0001EBF8
		private IEnumerator<List<object[]>> GetAllItemsIterator()
		{
			this.itemFinder = new ItemFinder(base.ElcUserTagInformation.MailboxSession, base.Folder.DisplayName, base.ElcUserTagInformation.UtcNow);
			this.SetItemQueryFlags();
			IEnumerator<List<object[]>> resultView = this.itemFinder.GetResultView(base.Folder);
			this.propertyIndexHolder = new PropertyIndexHolder(this.itemFinder.DataColumns);
			this.itemStartDateCalculator = new ItemStartDateCalculator(this.propertyIndexHolder, base.Folder.DisplayName, this.folderType, base.ElcUserTagInformation.MailboxSession, PropertySynchronizerBase.Tracer);
			return resultView;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00020A94 File Offset: 0x0001EC94
		private void SetItemQueryFlags()
		{
			this.itemFinder.NeedRetentionTagProps = true;
			if (this.folderType == DefaultFolderType.Calendar)
			{
				this.itemFinder.NeedCalendarProps = true;
			}
			else if (this.folderType == DefaultFolderType.Tasks)
			{
				this.itemFinder.NeedTaskProps = true;
			}
			if (base.MailboxDataForTags.ElcUserInformation.ProcessEhaMigratedMessages)
			{
				this.itemFinder.NeedMigrationExpiryTime = true;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00020AF8 File Offset: 0x0001ECF8
		private Guid UpdateTagProperties(out bool tagChanged, out Guid effectiveParentTagGuid, string itemClass, object[] itemProperties)
		{
			PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Start to update tag properties", this);
			Guid value = ElcMailboxHelper.GetGuidFromBytes(itemProperties[this.propertyIndexHolder.PolicyTagIndex], new Guid?(Guid.Empty), true, (VersionedId)itemProperties[this.propertyIndexHolder.IdIndex]).Value;
			Guid value2 = ElcMailboxHelper.GetGuidFromBytes(itemProperties[this.propertyIndexHolder.ExplicitPolicyTagIndex], new Guid?(Guid.Empty), true, (VersionedId)itemProperties[this.propertyIndexHolder.IdIndex]).Value;
			int? retentionPeriod = (itemProperties[this.propertyIndexHolder.RetentionPeriodIndex] is int) ? ((int?)itemProperties[this.propertyIndexHolder.RetentionPeriodIndex]) : null;
			int? retentionFlags = (itemProperties[this.propertyIndexHolder.RetentionFlagsIndex] is int) ? ((int?)itemProperties[this.propertyIndexHolder.RetentionFlagsIndex]) : null;
			tagChanged = false;
			Guid guid = value;
			effectiveParentTagGuid = this.GetEffectiveParentTag(itemClass);
			bool flag = TagAssistantHelper.IsTagImplicit(retentionPeriod, retentionFlags);
			bool flag2 = !base.ElcUserTagInformation.ContainsTag(value);
			if (!value2.Equals(Guid.Empty) && base.ElcUserTagInformation.ContainsTag(value2))
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Found explicit personal tag", this);
				tagChanged = true;
				guid = value2;
				base.AddAdriftTagToFai(value2);
				this.SetTagProperties(guid, value, true);
				base.PropertiesToBeDeleted.Add(StoreObjectSchema.ExplicitPolicyTag);
				itemProperties[this.propertyIndexHolder.RetentionPeriodIndex] = 1;
				ELCPerfmon.TotalItemsWithPersonalTag.Increment();
				this.taggedByPersonalExpiryTag = true;
			}
			else if (flag || (itemProperties[this.propertyIndexHolder.RetentionPeriodIndex] is int && flag2))
			{
				if (!effectiveParentTagGuid.Equals(value))
				{
					PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: The original tag doesn't match the parent tag", this);
					if (!flag)
					{
						base.PropertiesToBeUpdated.Add(StoreObjectSchema.ExplicitPolicyTag, value.ToByteArray());
						base.AddDeletedTag(value);
					}
					if (!this.ShouldSkipImplicitItem(itemProperties, effectiveParentTagGuid, value, itemClass))
					{
						tagChanged = true;
						guid = effectiveParentTagGuid;
						this.SetTagProperties(effectiveParentTagGuid, value, false);
						itemProperties[this.propertyIndexHolder.RetentionPeriodIndex] = null;
						if (effectiveParentTagGuid != Guid.Empty)
						{
							if (base.ElcUserTagInformation.AllAdTags.ContainsKey(effectiveParentTagGuid))
							{
								if (base.ElcUserTagInformation.GetTag(effectiveParentTagGuid).Tag.Type == ElcFolderType.Personal)
								{
									PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Inherited explicit personal tag", this);
									ELCPerfmon.TotalItemsWithPersonalTag.Increment();
									this.taggedByPersonalExpiryTag = true;
								}
								else if (base.ElcUserTagInformation.GetTag(effectiveParentTagGuid).Tag.Type == ElcFolderType.All)
								{
									PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Inherited default tag", this);
									ELCPerfmon.TotalItemsWithDefaultTag.Increment();
									this.taggedByDefaultExpiryTag = true;
								}
								else
								{
									PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Inherited system tag", this);
									ELCPerfmon.TotalItemsWithDefaultTag.Increment();
									this.taggedBySystemExpiryTag = true;
								}
							}
							else
							{
								PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: The parent tag is not in AD. Its type is unknown.", this);
								this.taggedByUncertainExpiryTag = true;
							}
						}
						else if (!flag)
						{
							PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: The tag is explicit. Removed personal tag", this);
							this.taggedByPersonalExpiryTag = true;
						}
						else if (!flag2)
						{
							if (base.ElcUserTagInformation.GetTag(value).Tag.Type == ElcFolderType.Personal)
							{
								PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Removed personal tag", this);
								this.taggedByPersonalExpiryTag = true;
							}
							else if (base.ElcUserTagInformation.GetTag(value).Tag.Type == ElcFolderType.All)
							{
								PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Removed default tag", this);
								this.taggedByDefaultExpiryTag = true;
							}
							else
							{
								PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Removed system tag", this);
								this.taggedBySystemExpiryTag = true;
							}
						}
						else
						{
							PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: The removed tag is deleted in AD. Its type is unknown.", this);
							this.taggedByUncertainExpiryTag = true;
						}
					}
					else
					{
						PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: The item is skipped", this);
					}
				}
				else
				{
					PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: The original tag matches the parent tag", this);
					if (FlagsMan.IsAutoTagSet(itemProperties[this.propertyIndexHolder.RetentionFlagsIndex]))
					{
						base.PropertiesToBeDeleted.Add(StoreObjectSchema.RetentionPeriod);
						itemProperties[this.propertyIndexHolder.RetentionPeriodIndex] = null;
					}
				}
			}
			else if (itemProperties[this.propertyIndexHolder.RetentionPeriodIndex] is int)
			{
				base.AddAdriftTagToFai(value);
			}
			return guid;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00020FE0 File Offset: 0x0001F1E0
		private void SetTagProperties(Guid tagToUpdateTo, Guid originalItemTagGuid, bool isExplicit)
		{
			if (tagToUpdateTo.Equals(Guid.Empty))
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: The item tag needs to be deleted.", this);
				base.PropertiesToBeDeleted.Add(StoreObjectSchema.PolicyTag);
			}
			else
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: The item tag needs to be updated.", this);
				base.PropertiesToBeUpdated.Add(StoreObjectSchema.PolicyTag, tagToUpdateTo.ToByteArray());
			}
			if (!isExplicit)
			{
				base.PropertiesToBeDeleted.Add(StoreObjectSchema.RetentionPeriod);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00021068 File Offset: 0x0001F268
		private Guid GetEffectiveParentTag(string messageClass)
		{
			Guid guid = ElcMailboxHelper.GetGuidFromBytes(base.FolderProperties[2], new Guid?(Guid.Empty), false, base.FolderDisplayName).Value;
			if (guid == Guid.Empty)
			{
				if (messageClass.StartsWith(ElcMessageClass.VoiceMail.TrimEnd(new char[]
				{
					'*'
				}), StringComparison.OrdinalIgnoreCase) && !base.ElcUserTagInformation.DefaultVmAdTag.Equals(Guid.Empty))
				{
					guid = base.ElcUserTagInformation.DefaultVmAdTag;
				}
				else
				{
					guid = base.ElcUserTagInformation.DefaultAdTag;
				}
			}
			return guid;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00021100 File Offset: 0x0001F300
		private DateTime UpdateStartDateEtc(VersionedId itemId, string itemClass, object[] itemProperties)
		{
			CompositeProperty compositeProperty = new CompositeProperty();
			try
			{
				if (itemProperties[this.propertyIndexHolder.StartDateEtcIndex] is byte[])
				{
					compositeProperty = CompositeProperty.Parse((byte[])itemProperties[this.propertyIndexHolder.StartDateEtcIndex], true);
				}
			}
			catch (ArgumentException arg)
			{
				PropertySynchronizerBase.Tracer.TraceError<ItemPropertySynchronizer, string, ArgumentException>((long)this.GetHashCode(), "{0}: Could not parse StartDateEtc property of item. ItemClass: {1} Exception: {2}.", this, itemClass, arg);
			}
			bool flag = this.UpdateStartDate(compositeProperty, itemId, itemClass, itemProperties);
			bool flag2 = this.UpdateDefaultRetentionPeriod(compositeProperty, itemClass);
			if (flag || flag2)
			{
				base.PropertiesToBeUpdated.Add(ItemSchema.StartDateEtc, compositeProperty.GetBytes(true));
			}
			return compositeProperty.Date.Value;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000211B0 File Offset: 0x0001F3B0
		private bool UpdateStartDate(CompositeProperty compositeProperty, VersionedId itemId, string itemClass, object[] itemProperties)
		{
			if (compositeProperty.Date == null)
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Updating Start Date on item because it is null.", this);
				compositeProperty.Date = new DateTime?(this.itemStartDateCalculator.GetStartDateForTag(itemId, itemClass, itemProperties, null, false));
				return true;
			}
			if (this.IsConflictableItem(itemClass))
			{
				DateTime startDateForTag = this.itemStartDateCalculator.GetStartDateForTag(itemId, itemClass, itemProperties, null, false);
				bool flag = !startDateForTag.Equals(compositeProperty.Date.Value);
				if (flag)
				{
					compositeProperty.Date = new DateTime?(startDateForTag);
				}
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer, bool>((long)this.GetHashCode(), "{0}: Recalculating Start Date on item because it is ConflictableItem.  Has changed = {1}", this, flag);
				return flag;
			}
			return false;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00021274 File Offset: 0x0001F474
		private bool UpdateDefaultRetentionPeriod(CompositeProperty compositeProperty, string itemClass)
		{
			int integer = compositeProperty.Integer;
			ContentSetting defaultContentSettingForMsgClass = base.ElcUserTagInformation.GetDefaultContentSettingForMsgClass(itemClass);
			int num = 0;
			if (defaultContentSettingForMsgClass != null && defaultContentSettingForMsgClass.RetentionEnabled)
			{
				num = (int)defaultContentSettingForMsgClass.AgeLimitForRetention.Value.TotalDays;
			}
			if (integer != num)
			{
				compositeProperty.Integer = num;
				return true;
			}
			return false;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000212CC File Offset: 0x0001F4CC
		private void UpdateContentSettingProperties(bool tagChanged, Guid updatedTagGuid, DateTime updatedStartDate, string itemClass, object[] itemProperties)
		{
			if (!tagChanged && updatedTagGuid.Equals(Guid.Empty))
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: No need to update ContentSettings since tag is null and there was no change.", this);
				return;
			}
			int num = -1;
			if (itemProperties[this.propertyIndexHolder.RetentionPeriodIndex] is int)
			{
				num = (int)itemProperties[this.propertyIndexHolder.RetentionPeriodIndex];
			}
			DateTime dateTime = DateTime.MinValue;
			if (itemProperties[this.propertyIndexHolder.RetentionDateIndex] is ExDateTime)
			{
				dateTime = (DateTime)((ExDateTime)itemProperties[this.propertyIndexHolder.RetentionDateIndex]).ToUtc();
			}
			if (tagChanged && updatedTagGuid.Equals(Guid.Empty))
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Tag is null, so we need to delete the Retention Date & Retention Period.", this);
				if (dateTime != DateTime.MinValue)
				{
					base.PropertiesToBeDeleted.Add(ItemSchema.RetentionDate);
				}
				if (num != -1)
				{
					base.PropertiesToBeDeleted.Add(StoreObjectSchema.RetentionPeriod);
				}
				return;
			}
			int retentionPeriod = this.GetRetentionPeriod(updatedTagGuid, itemClass);
			DateTime retentionDate = this.GetRetentionDate(updatedStartDate, retentionPeriod);
			if (itemProperties[this.propertyIndexHolder.RetentionPeriodIndex] is int && num != retentionPeriod)
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Retention period needs to be updated.", this);
				base.PropertiesToBeUpdated.Add(StoreObjectSchema.RetentionPeriod, retentionPeriod);
			}
			if (!TagAssistantHelper.DateSlushyEquals(new DateTime?(retentionDate), new DateTime?(dateTime)))
			{
				if (retentionDate == DateTime.MinValue)
				{
					PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Retention date needs to be deleted.", this);
					base.PropertiesToBeDeleted.Add(ItemSchema.RetentionDate);
					return;
				}
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Retention date needs to be updated.", this);
				base.PropertiesToBeUpdated.Add(ItemSchema.RetentionDate, retentionDate);
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00021494 File Offset: 0x0001F694
		private bool UpdateArchiveTag(object[] itemProperties, out Guid effectiveGuid)
		{
			bool result = false;
			Guid value = ElcMailboxHelper.GetGuidFromBytes(itemProperties[this.propertyIndexHolder.ArchiveTagIndex], new Guid?(Guid.Empty), false, base.FolderDisplayName).Value;
			Guid value2 = ElcMailboxHelper.GetGuidFromBytes(itemProperties[this.propertyIndexHolder.ExplicitArchiveTagIndex], new Guid?(Guid.Empty), true, (VersionedId)itemProperties[this.propertyIndexHolder.IdIndex]).Value;
			effectiveGuid = Guid.Empty;
			bool flag = !Guid.Empty.Equals(value);
			bool flag2 = itemProperties[this.propertyIndexHolder.ArchivePeriodIndex] is int;
			bool flag3 = flag && base.ElcUserTagInformation.ContainsTag(value);
			bool flag4 = this.inheritedArchiveGuid != null && !Guid.Empty.Equals(this.inheritedArchiveGuid.Value);
			if (!value2.Equals(Guid.Empty) && base.ElcUserTagInformation.ContainsTag(value2))
			{
				result = true;
				effectiveGuid = value2;
				base.AddAdriftTagToFai(value2);
				base.PropertiesToBeDeleted.Add(StoreObjectSchema.ExplicitArchiveTag);
				base.PropertiesToBeUpdated.Add(StoreObjectSchema.ArchiveTag, effectiveGuid.ToByteArray());
				itemProperties[this.propertyIndexHolder.ArchivePeriodIndex] = 1;
				ELCPerfmon.TotalItemsWithPersonalTag.Increment();
				this.taggedByPersonalArchiveTag = true;
			}
			else if (flag && flag3 && flag2)
			{
				effectiveGuid = value;
				base.AddAdriftTagToFai(value);
			}
			else if (flag4 && (!flag || !flag2 || (flag && !flag3)))
			{
				if (flag && flag2 && !flag3)
				{
					base.PropertiesToBeUpdated.Add(StoreObjectSchema.ExplicitArchiveTag, value.ToByteArray());
					base.AddDeletedTag(value);
				}
				if (!this.inheritedArchiveGuid.Equals(value))
				{
					result = true;
					base.PropertiesToBeUpdated.Add(StoreObjectSchema.ArchiveTag, this.inheritedArchiveGuid.Value.ToByteArray());
					if (base.ElcUserTagInformation.GetTag(this.inheritedArchiveGuid.Value).Tag.Type == ElcFolderType.Personal)
					{
						ELCPerfmon.TotalItemsWithPersonalTag.Increment();
						this.taggedByPersonalArchiveTag = true;
					}
					else
					{
						ELCPerfmon.TotalItemsWithDefaultTag.Increment();
						PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Found item with default archive tag", this);
					}
				}
				effectiveGuid = this.inheritedArchiveGuid.Value;
			}
			else if (flag)
			{
				result = true;
				base.PropertiesToBeDeleted.Add(StoreObjectSchema.ArchiveTag);
				this.taggedByPersonalArchiveTag = true;
			}
			return result;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0002172C File Offset: 0x0001F92C
		private void UpdateRetentionFlags(Guid parentTagGuid, bool tagChanged, Guid updatedTagGuid, Guid updatedArchiveGuid, object[] itemProperties)
		{
			int num = -1;
			if (itemProperties[this.propertyIndexHolder.RetentionFlagsIndex] is int)
			{
				num = (int)itemProperties[this.propertyIndexHolder.RetentionFlagsIndex];
			}
			if (tagChanged && updatedTagGuid.Equals(Guid.Empty) && num != -1)
			{
				if (!updatedArchiveGuid.Equals(Guid.Empty))
				{
					base.PropertiesToBeDeleted.Add(StoreObjectSchema.RetentionFlags);
					return;
				}
				base.PropertiesToBeUpdated.Add(StoreObjectSchema.RetentionFlags, 0);
			}
			if (base.ElcUserTagInformation.ContainsTag(parentTagGuid) && base.ElcUserTagInformation.GetTag(parentTagGuid).Tag.Type == ElcFolderType.Personal && FlagsMan.IsAutoTagSet(itemProperties[this.propertyIndexHolder.RetentionFlagsIndex]))
			{
				base.PropertiesToBeUpdated.Add(StoreObjectSchema.RetentionFlags, FlagsMan.ClearAutoTag((int)itemProperties[this.propertyIndexHolder.RetentionFlagsIndex]));
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0002181C File Offset: 0x0001FA1C
		private int GetRetentionPeriod(Guid updatedTagGuid, string messageClass)
		{
			int result = 0;
			AdTagData adTagData = null;
			ContentSetting contentSetting;
			if (base.ElcUserTagInformation.TryGetTag(updatedTagGuid, out adTagData) && adTagData.Tag.Type != ElcFolderType.All)
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Non-default tag. Fetch its retention date.", this);
				contentSetting = base.ElcUserTagInformation.GetRetentionEnabledSettingForTag(updatedTagGuid);
			}
			else
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: Default tag. Need to get the content settings that applies to this message class.", this);
				contentSetting = base.ElcUserTagInformation.GetDefaultContentSettingForMsgClass(messageClass);
			}
			if (contentSetting != null && contentSetting.RetentionEnabled)
			{
				result = (int)contentSetting.AgeLimitForRetention.Value.TotalDays;
			}
			return result;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000218C0 File Offset: 0x0001FAC0
		private DateTime GetRetentionDate(DateTime updatedStartDate, int retentionPeriod)
		{
			DateTime dateTime = DateTime.MinValue;
			if (retentionPeriod != 0 && updatedStartDate != DateTime.MinValue)
			{
				try
				{
					dateTime = updatedStartDate.AddDays((double)retentionPeriod);
				}
				catch (ArgumentOutOfRangeException)
				{
					PropertySynchronizerBase.Tracer.TraceDebug<DateTime, int>((long)this.GetHashCode(), "Corrupted Data. Could not AddDays {1} to {0}", updatedStartDate, retentionPeriod);
					dateTime = DateTime.MinValue;
				}
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer, DateTime>((long)this.GetHashCode(), "{0}: We have a valid Retention Date of {1}", this, dateTime);
			}
			return dateTime;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00021C4C File Offset: 0x0001FE4C
		private void CommitChangesAlready(VersionedId itemId)
		{
			ItemPropertySynchronizer.<>c__DisplayClass2 CS$<>8__locals1 = new ItemPropertySynchronizer.<>c__DisplayClass2();
			CS$<>8__locals1.itemId = itemId;
			CS$<>8__locals1.<>4__this = this;
			base.ElcAssistant.ThrowIfShuttingDown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
			if (base.PropertiesToBeDeleted.Count <= 0 && base.PropertiesToBeUpdated.PropertyDefinitions.Count <= 0)
			{
				return;
			}
			ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<CommitChangesAlready>b__0)), new FilterDelegate(null, (UIntPtr)ldftn(ExceptionFilter)), new CatchDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<CommitChangesAlready>b__1)));
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00021CD4 File Offset: 0x0001FED4
		private bool ShouldSkipImplicitItem(object[] itemProperties, Guid effectiveParentTagGuid, Guid originalItemTagGuid, string itemClass)
		{
			ContentSetting defaultTagContentSettingForMsgClass = base.ElcUserTagInformation.GetDefaultTagContentSettingForMsgClass(effectiveParentTagGuid, itemClass);
			if ((effectiveParentTagGuid.Equals(Guid.Empty) || (base.ElcUserTagInformation.ContainsTag(effectiveParentTagGuid) && base.ElcUserTagInformation.GetTag(effectiveParentTagGuid).Tag.Type != ElcFolderType.Personal)) && FlagsMan.IsAutoTagSet(itemProperties[this.propertyIndexHolder.RetentionFlagsIndex]) && base.ElcUserTagInformation.ContainsTag(originalItemTagGuid))
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer>((long)this.GetHashCode(), "{0}: The current item is auto tagged. It will not inherit the parent tag.", this);
				return true;
			}
			if (originalItemTagGuid.Equals(Guid.Empty) && base.ElcUserTagInformation.ContainsTag(effectiveParentTagGuid) && base.ElcUserTagInformation.GetTag(effectiveParentTagGuid).Tag.Type == ElcFolderType.All && defaultTagContentSettingForMsgClass == null)
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer, string, Guid>((long)this.GetHashCode(), "{0}: The current item class ({1}) is not supported by the default tag ({2}). It will not inherit the default tag.", this, itemClass, effectiveParentTagGuid);
				return true;
			}
			if (!originalItemTagGuid.Equals(effectiveParentTagGuid) && base.ElcUserTagInformation.ContainsTag(effectiveParentTagGuid) && base.ElcUserTagInformation.GetTag(effectiveParentTagGuid).Tag.Type == ElcFolderType.All && defaultTagContentSettingForMsgClass == null)
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer, string, Guid>((long)this.GetHashCode(), "{0}: The current item class ({1}) is not supported by the default tag ({2}). It will not inherit the default tag.", this, itemClass, effectiveParentTagGuid);
				return true;
			}
			return false;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00021E0A File Offset: 0x0002000A
		private bool IsConflictableItem(string itemClass)
		{
			if (TagAssistantHelper.IsConflictableItemClass(itemClass) && this.folderType != DefaultFolderType.DeletedItems)
			{
				PropertySynchronizerBase.Tracer.TraceDebug<ItemPropertySynchronizer, DefaultFolderType>((long)this.GetHashCode(), "{0}: Item is conflictable and not in deleted items folder. Folder: {1}. Skipping it.", this, this.folderType);
				return true;
			}
			return false;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00021E3D File Offset: 0x0002003D
		private bool IsRuleMessageItem(string itemClass)
		{
			return string.Equals(itemClass, "IPM.Rule.Message", StringComparison.InvariantCultureIgnoreCase) || string.Equals(itemClass, "IPM.Rule.Version2.Message", StringComparison.InvariantCultureIgnoreCase) || string.Equals(itemClass, "IPM.ExtendedRule.Message", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x04000395 RID: 917
		private ItemFinder itemFinder;

		// Token: 0x04000396 RID: 918
		private PropertyIndexHolder propertyIndexHolder;

		// Token: 0x04000397 RID: 919
		private ItemStartDateCalculator itemStartDateCalculator;

		// Token: 0x04000398 RID: 920
		private DefaultFolderType folderType;

		// Token: 0x04000399 RID: 921
		private Guid? inheritedArchiveGuid;
	}
}
