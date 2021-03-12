using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Mapi;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000081 RID: 129
	internal class ElcEventProcessor
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x000226C3 File Offset: 0x000208C3
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x000226ED File Offset: 0x000208ED
		public int DepthLimit
		{
			get
			{
				if (this.m_depthLimit == null)
				{
					this.m_depthLimit = new int?(ElcEventProcessor.GetElcAssistantInheritedTagDepth());
				}
				return this.m_depthLimit.Value;
			}
			set
			{
				this.m_depthLimit = new int?(value);
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0002271A File Offset: 0x0002091A
		public override string ToString()
		{
			return "ElcEventProcessor";
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00022721 File Offset: 0x00020921
		internal static ElcEventProcessor GetElcEventProcessor()
		{
			return ElcEventProcessor.elcEventProcessor;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000227AC File Offset: 0x000209AC
		internal static bool ItemSave(MailboxSession mailboxSession, Item itemToSave)
		{
			ElcEventProcessor.<>c__DisplayClass2 CS$<>8__locals1 = new ElcEventProcessor.<>c__DisplayClass2();
			CS$<>8__locals1.mailboxSession = mailboxSession;
			CS$<>8__locals1.itemToSave = itemToSave;
			CS$<>8__locals1.result = null;
			CS$<>8__locals1.success = true;
			ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ItemSave>b__0)), new FilterDelegate(null, (UIntPtr)ldftn(ExceptionFilter)), new CatchDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ItemSave>b__1)));
			return CS$<>8__locals1.result != null && CS$<>8__locals1.result.SaveStatus != SaveResult.IrresolvableConflict && CS$<>8__locals1.success;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00022824 File Offset: 0x00020A24
		internal void ValidateStoreObject(UserRetentionPolicyCache mailboxState, MailboxSession mailboxSession, StoreObjectId parentId, StoreObjectId itemId, StoreObject item, MapiEvent mapiEvent)
		{
			if (item == null)
			{
				ElcEventProcessor.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: ValidateStoreObject called on null item");
				return;
			}
			ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, StoreObjectId>((long)this.GetHashCode(), "{0}: ValidateStoreObject called on itemId: {1}", this, item.StoreObjectId ?? StoreObjectId.DummyId);
			if (item is Folder && !(item is OutlookSearchFolder))
			{
				this.ValidateFolderTag(mailboxState, mailboxSession, parentId, item as Folder, mapiEvent);
				return;
			}
			if (item is Item && TagAssistantHelper.IsRetainableItem(mailboxState, mailboxSession, parentId, item))
			{
				this.ValidateItemTag(mailboxState, mailboxSession, parentId, item as Item, mapiEvent);
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000228C0 File Offset: 0x00020AC0
		private void ValidateFolderTag(UserRetentionPolicyCache mailboxState, MailboxSession mailboxSession, StoreObjectId parentId, Folder folder, MapiEvent mapiEvent)
		{
			mailboxState.PurgeTag(folder.Id.ObjectId);
			DefaultFolderType defaultFolderType = mailboxSession.IsDefaultFolderType(folder.Id);
			if (defaultFolderType != DefaultFolderType.None)
			{
				this.ValidateDefaultFolder(mailboxState, mailboxSession, parentId, folder, defaultFolderType, mapiEvent);
				return;
			}
			this.ValidateRegularFolder(mailboxState, mailboxSession, parentId, folder, mapiEvent);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000229F0 File Offset: 0x00020BF0
		private void QuickUpdateFolderContents(UserRetentionPolicyCache mailboxState, MailboxSession mailboxSession, Folder folder)
		{
			ElcEventProcessor.<>c__DisplayClass6 CS$<>8__locals1 = new ElcEventProcessor.<>c__DisplayClass6();
			CS$<>8__locals1.mailboxState = mailboxState;
			CS$<>8__locals1.mailboxSession = mailboxSession;
			CS$<>8__locals1.<>4__this = this;
			try
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, new PropertyDefinition[]
				{
					FolderSchema.Id,
					StoreObjectSchema.ParentItemId
				}))
				{
					for (;;)
					{
						object[][] rows = queryResult.GetRows(100);
						if (rows.Length <= 0)
						{
							break;
						}
						foreach (object[] array2 in rows)
						{
							ElcEventProcessor.<>c__DisplayClass8 CS$<>8__locals2 = new ElcEventProcessor.<>c__DisplayClass8();
							CS$<>8__locals2.CS$<>8__locals7 = CS$<>8__locals1;
							if (array2[0] is VersionedId)
							{
								CS$<>8__locals2.folderId = (VersionedId)array2[0];
								CS$<>8__locals2.parentId = (StoreObjectId)array2[1];
								ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals2, (UIntPtr)ldftn(<QuickUpdateFolderContents>b__4)), new FilterDelegate(null, (UIntPtr)ldftn(ExceptionFilter)), new CatchDelegate(CS$<>8__locals2, (UIntPtr)ldftn(<QuickUpdateFolderContents>b__5)));
							}
						}
					}
				}
			}
			catch (ObjectNotFoundException arg)
			{
				ElcEventProcessor.Tracer.TraceDebug<MailboxSession, string, ObjectNotFoundException>((long)CS$<>8__locals1.mailboxSession.GetHashCode(), "{0}: Problems querying folder {1}. It will not be processed. Exception: {2}", CS$<>8__locals1.mailboxSession, folder.Id.ToString(), arg);
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00022B30 File Offset: 0x00020D30
		private void ValidateDefaultFolder(UserRetentionPolicyCache mailboxState, MailboxSession mailboxSession, StoreObjectId parentId, Folder folder, DefaultFolderType dft, MapiEvent mapiEvent)
		{
			ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, string>((long)this.GetHashCode(), "{0}: ValidateDefaultFolder called on folder {1}.", this, folder.DisplayName ?? string.Empty);
			ElcFolderType? elcFolderType = ElcMailboxHelper.GetElcFolderType(dft);
			if (elcFolderType == null)
			{
				return;
			}
			if (dft == DefaultFolderType.Root || dft == DefaultFolderType.Configuration)
			{
				return;
			}
			if (ArrayComparer<byte>.Comparer.Equals(folder.Id.ObjectId.ProviderLevelItemId, folder.ParentId.ProviderLevelItemId))
			{
				return;
			}
			bool flag = false;
			foreach (StoreTagData storeTagData in mailboxState.StoreTagDictionary.Values)
			{
				if (storeTagData.Tag.Type == elcFolderType)
				{
					flag = true;
					ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, ElcFolderType>((long)this.GetHashCode(), "{0}: Applying tag to system folder of type {1}", this, storeTagData.Tag.Type);
					Guid? existingTagGuid = null;
					int? existingRetentionPeriod = null;
					RetentionAndArchiveFlags? retentionAndArchiveFlags = null;
					existingTagGuid = ElcMailboxHelper.GetGuidFromBytes(folder.GetValueOrDefault<object>(StoreObjectSchema.PolicyTag), null, true, folder.Id);
					existingRetentionPeriod = folder.GetValueAsNullable<int>(StoreObjectSchema.RetentionPeriod);
					object valueOrDefault = folder.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
					if (valueOrDefault != null && valueOrDefault is int)
					{
						retentionAndArchiveFlags = new RetentionAndArchiveFlags?((RetentionAndArchiveFlags)((int)valueOrDefault));
					}
					mailboxState.FolderTagDictionary[folder.Id.ObjectId] = storeTagData.Tag.RetentionId;
					Guid retentionId = storeTagData.Tag.RetentionId;
					int retentionPeriod = this.GetRetentionPeriod(mailboxState, storeTagData, folder.ClassName);
					RetentionAndArchiveFlags retentionAndArchiveFlags2 = RetentionAndArchiveFlags.ExplicitTag;
					if (retentionAndArchiveFlags != null)
					{
						retentionAndArchiveFlags2 = (retentionAndArchiveFlags2 | (retentionAndArchiveFlags & ~(RetentionAndArchiveFlags.ExplicitTag | RetentionAndArchiveFlags.UserOverride | RetentionAndArchiveFlags.Autotag | RetentionAndArchiveFlags.PersonalTag))).Value;
					}
					this.FolderSave(mailboxState, mailboxSession, folder, existingTagGuid, existingRetentionPeriod, retentionAndArchiveFlags, new Guid?(retentionId), new int?(retentionPeriod), new RetentionAndArchiveFlags?(retentionAndArchiveFlags2));
				}
			}
			if (!flag)
			{
				ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, string, ElcFolderType?>((long)this.GetHashCode(), "{0}: calling ValidateRegularFolder from ValidateDefaultFolder on folder {1} because folderType is unmatched by mailboxState.StoreTagDictionary. folderType=={2}", this, folder.DisplayName ?? string.Empty, elcFolderType);
				this.ValidateRegularFolder(mailboxState, mailboxSession, parentId, folder, mapiEvent);
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00022DCC File Offset: 0x00020FCC
		private void ValidateRegularFolder(UserRetentionPolicyCache mailboxState, MailboxSession mailboxSession, StoreObjectId parentId, Folder folder, MapiEvent mapiEvent)
		{
			bool flag = true;
			ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, string>((long)this.GetHashCode(), "{0}: ValidateRegularFolder called on folder {1}.", this, folder.DisplayName ?? string.Empty);
			Guid? guid = null;
			int? existingRetentionPeriod = null;
			RetentionAndArchiveFlags? retentionAndArchiveFlags = null;
			guid = ElcMailboxHelper.GetGuidFromBytes(folder.GetValueOrDefault<object>(StoreObjectSchema.PolicyTag), null, true, folder.Id);
			existingRetentionPeriod = folder.GetValueAsNullable<int>(StoreObjectSchema.RetentionPeriod);
			object valueOrDefault = folder.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
			if (valueOrDefault != null && valueOrDefault is int)
			{
				retentionAndArchiveFlags = new RetentionAndArchiveFlags?((RetentionAndArchiveFlags)((int)valueOrDefault));
			}
			Guid? guid2 = new Guid?(Guid.Empty);
			Guid empty = Guid.Empty;
			int? expectedRetentionPeriod = new int?(0);
			RetentionAndArchiveFlags? retentionAndArchiveFlags2 = new RetentionAndArchiveFlags?(RetentionAndArchiveFlags.None);
			StoreTagData defaultTag = mailboxState.GetDefaultTag();
			bool flag2 = guid != null && !guid.Value.Equals(ElcMailboxHelper.BadGuid);
			bool flag3 = retentionAndArchiveFlags == null || !FlagsMan.IsExplicitSet(retentionAndArchiveFlags) || !flag2;
			if (flag3)
			{
				Item item = null;
				ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, string>((long)this.GetHashCode(), "{0}: The folder {1} will inherit a tag.", this, folder.DisplayName ?? string.Empty);
				bool flag4 = false;
				Guid value;
				bool flag5 = this.TryFindInheritedTag(mailboxState, mailboxSession, folder.ParentId, TagType.PolicyTag, item, out value, out empty, out flag4);
				if (flag4)
				{
					ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor>((long)this.GetHashCode(), "{0}: When trying to find inherited policy tag, limit reached.", this);
				}
				if (flag5)
				{
					guid2 = new Guid?(value);
				}
				if (defaultTag != null && flag5 && defaultTag.Tag.RetentionId == guid2)
				{
					bool flag6 = mailboxSession.MailboxOwner != null && mailboxSession.MailboxOwner.MailboxInfo.IsArchive;
					if (flag6 && flag2 && !this.IsOldParentFolderDeletedItems(mapiEvent, mailboxSession))
					{
						flag3 = false;
						guid2 = guid;
					}
					else
					{
						guid2 = null;
						expectedRetentionPeriod = null;
						retentionAndArchiveFlags2 = null;
					}
				}
				else
				{
					StoreTagData storeTagData = null;
					if (flag5)
					{
						mailboxState.StoreTagDictionary.TryGetValue(guid2.Value, out storeTagData);
					}
					if (storeTagData != null)
					{
						expectedRetentionPeriod = new int?(this.GetRetentionPeriod(mailboxState, storeTagData, folder.ClassName));
						retentionAndArchiveFlags2 = new RetentionAndArchiveFlags?(RetentionAndArchiveFlags.ExplicitTag);
						if (retentionAndArchiveFlags == null)
						{
							retentionAndArchiveFlags2 = new RetentionAndArchiveFlags?(RetentionAndArchiveFlags.None);
						}
						else
						{
							retentionAndArchiveFlags2 = new RetentionAndArchiveFlags?((RetentionAndArchiveFlags)FlagsMan.ClearExplicit((int)retentionAndArchiveFlags2.Value));
						}
						retentionAndArchiveFlags2 = new RetentionAndArchiveFlags?(TagAssistantHelper.UpdatePersonalTagBit(storeTagData, retentionAndArchiveFlags2.Value));
					}
					else
					{
						guid2 = null;
						expectedRetentionPeriod = null;
						retentionAndArchiveFlags2 = null;
					}
				}
			}
			if (!flag3)
			{
				ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, string>((long)this.GetHashCode(), "{0}: The folder {1} has an explicit tag.", this, folder.DisplayName ?? string.Empty);
				StoreTagData storeTagData2 = null;
				mailboxState.StoreTagDictionary.TryGetValue(guid.Value, out storeTagData2);
				if (storeTagData2 != null)
				{
					guid2 = new Guid?(guid.Value);
					expectedRetentionPeriod = new int?(this.GetRetentionPeriod(mailboxState, storeTagData2, folder.ClassName));
					retentionAndArchiveFlags2 = new RetentionAndArchiveFlags?(retentionAndArchiveFlags ?? RetentionAndArchiveFlags.ExplicitTag);
					retentionAndArchiveFlags2 = new RetentionAndArchiveFlags?(TagAssistantHelper.UpdatePersonalTagBit(storeTagData2, retentionAndArchiveFlags2.Value));
					retentionAndArchiveFlags2 = new RetentionAndArchiveFlags?((RetentionAndArchiveFlags)FlagsMan.SetExplicit((int)retentionAndArchiveFlags2.Value));
				}
				else
				{
					flag = false;
				}
			}
			if (retentionAndArchiveFlags != null)
			{
				retentionAndArchiveFlags2 = ((retentionAndArchiveFlags2 ?? RetentionAndArchiveFlags.None) | (retentionAndArchiveFlags & ~(RetentionAndArchiveFlags.ExplicitTag | RetentionAndArchiveFlags.UserOverride | RetentionAndArchiveFlags.Autotag | RetentionAndArchiveFlags.PersonalTag)));
			}
			if (guid != null != (guid2 != null) || guid != guid2)
			{
				if (retentionAndArchiveFlags2 != null)
				{
					retentionAndArchiveFlags2 |= RetentionAndArchiveFlags.NeedsRescan;
				}
				else
				{
					retentionAndArchiveFlags2 = new RetentionAndArchiveFlags?(RetentionAndArchiveFlags.NeedsRescan);
				}
			}
			if (flag)
			{
				this.FolderSave(mailboxState, mailboxSession, folder, guid, existingRetentionPeriod, retentionAndArchiveFlags, guid2, expectedRetentionPeriod, retentionAndArchiveFlags2);
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00023254 File Offset: 0x00021454
		private void ValidateItemTag(UserRetentionPolicyCache mailboxState, MailboxSession mailboxSession, StoreObjectId parentId, Item item, MapiEvent mapiEvent)
		{
			ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, Item>((long)this.GetHashCode(), "{0}: ValidateItemTag called on item {1}", this, item);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			bool flag4 = true;
			Guid? guid = null;
			Guid? guid2 = null;
			int? num = null;
			int? num2 = null;
			DateTime? b = null;
			DateTime? b2 = null;
			CompositeProperty compositeProperty = null;
			int? valueAsNullable = new int?(0);
			guid = ElcMailboxHelper.GetGuidFromBytes(item.GetValueOrDefault<object>(StoreObjectSchema.PolicyTag), null, true, item.Id);
			num = item.GetValueAsNullable<int>(StoreObjectSchema.RetentionPeriod);
			b = (DateTime?)item.GetValueAsNullable<ExDateTime>(ItemSchema.RetentionDate);
			guid2 = ElcMailboxHelper.GetGuidFromBytes(item.GetValueOrDefault<object>(StoreObjectSchema.ArchiveTag), null, true, item.Id);
			b2 = (DateTime?)item.GetValueAsNullable<ExDateTime>(ItemSchema.ArchiveDate);
			num2 = item.GetValueAsNullable<int>(StoreObjectSchema.ArchivePeriod);
			byte[] valueOrDefault = item.GetValueOrDefault<byte[]>(ItemSchema.StartDateEtc);
			if (valueOrDefault != null)
			{
				try
				{
					compositeProperty = CompositeProperty.Parse(valueOrDefault, true);
				}
				catch (ArgumentException ex)
				{
					ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, Item, string>((long)this.GetHashCode(), "{0}: The item {1} StartDate is corrupt. {2}", this, item, ex.Message);
				}
			}
			valueAsNullable = item.GetValueAsNullable<int>(StoreObjectSchema.RetentionFlags);
			if (!TagAssistantHelper.IsTagNull(guid) && guid.Value.Equals(PolicyTagHelper.SystemCleanupTagGuid))
			{
				ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, string, Item>((long)this.GetHashCode(), "{0}: The item has System Cleanup tag. Skipping it. Item subject: {1}. {2}", this, (item is MessageItem) ? ((MessageItem)item).Subject : item.ClassName, item);
				ELCPerfmon.TotalItemsWithSystemCleanupTag.Increment();
				return;
			}
			Guid guid3 = Guid.Empty;
			Guid empty = Guid.Empty;
			Guid guid4 = Guid.Empty;
			int? num3 = null;
			int? num4 = num2;
			DateTime dateTime = DateTime.MaxValue;
			DateTime d = DateTime.MaxValue;
			CompositeProperty compositeProperty2 = new CompositeProperty();
			int num5 = valueAsNullable ?? 0;
			TagSource tagSource = TagSource.None;
			object[] itemProperties = null;
			PropertyIndexHolder propertyIndexHolder = new PropertyIndexHolder(item, ref itemProperties);
			DefaultFolderType parentDefaultFolderType = TagAssistantHelper.GetParentDefaultFolderType(mailboxState, mailboxSession, parentId);
			ItemStartDateCalculator itemStartDateCalculator = new ItemStartDateCalculator(propertyIndexHolder, "EventBased", parentDefaultFolderType, mailboxSession, ElcEventProcessor.Tracer);
			MapiEventTypeFlags mapiEventTypeFlags = MapiEventTypeFlags.ObjectCreated | MapiEventTypeFlags.ObjectMoved | MapiEventTypeFlags.ObjectCopied;
			bool flag5 = mapiEvent == null || (mapiEvent.EventMask & mapiEventTypeFlags) != (MapiEventTypeFlags)0;
			DateTime? existingStartDate = (compositeProperty != null) ? compositeProperty.Date : null;
			compositeProperty2.Date = new DateTime?(itemStartDateCalculator.GetStartDateForTag(item.Id, item.ClassName, itemProperties, existingStartDate, flag5));
			bool flag6 = true;
			bool flag7 = true;
			if (TagAssistantHelper.IsTagImplicit(num, valueAsNullable) || TagAssistantHelper.IsTagNull(guid))
			{
				ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, string, Item>((long)this.GetHashCode(), "{0}: The item has implicit or no tag. Trying to find inherited tag. Item subject: {1}. {2}", this, (item is MessageItem) ? ((MessageItem)item).Subject : item.ClassName, item);
				bool flag8 = false;
				bool flag9 = this.TryFindInheritedTag(mailboxState, mailboxSession, parentId, TagType.PolicyTag, item, out guid3, out empty, out flag8);
				if (flag8 && flag5)
				{
					ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor>((long)this.GetHashCode(), "{0}: When trying to find inherited policy tag, limit reached so clear delete policy settings on item.", this);
					flag = true;
				}
				Guid key = Guid.Empty;
				if (flag9)
				{
					key = guid3;
				}
				this.AdjustExpectedTagIfItemAutotagged(valueAsNullable, guid, ref guid3, mailboxState, mailboxSession);
				if (empty != Guid.Empty)
				{
					guid3 = empty;
				}
				if (!guid3.Equals(Guid.Empty))
				{
					int retentionPeriod = this.GetRetentionPeriod(mailboxState, guid3, item.ClassName);
					compositeProperty2.Integer = this.GetDefaultRetentionPeriod(mailboxState, item.ClassName);
					if (compositeProperty2.Date != DateTime.MinValue && retentionPeriod != 0)
					{
						try
						{
							dateTime = compositeProperty2.Date.Value.AddDays((double)retentionPeriod);
							goto IL_3F5;
						}
						catch (ArgumentOutOfRangeException)
						{
							ElcEventProcessor.Tracer.TraceDebug<MailboxSession, DateTime, int>((long)this.GetHashCode(), "{0}: Corrupted Data. Could not AddDays {2} to {1}", mailboxSession, compositeProperty2.Date.Value, retentionPeriod);
							dateTime = DateTime.MaxValue;
							goto IL_3F5;
						}
					}
					dateTime = DateTime.MaxValue;
					IL_3F5:
					if (tagSource == TagSource.PredictedTag || FlagsMan.IsAutoTagSet(valueAsNullable))
					{
						num5 = FlagsMan.SetAutoTag(valueAsNullable);
						num3 = new int?(retentionPeriod);
						ElcEventProcessor.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Tag is predicted or autotagged. tagSource is {1}. Setting autotag=1 and retention period to {2}. Item: {3}", new object[]
						{
							mailboxSession,
							tagSource,
							num3,
							(item is MessageItem) ? ((MessageItem)item).Subject : item.ClassName
						});
					}
					if (mailboxState.StoreTagDictionary.ContainsKey(key) && mailboxState.StoreTagDictionary[key].Tag.Type == ElcFolderType.Personal)
					{
						if (FlagsMan.IsAutoTagSet(valueAsNullable))
						{
							num5 = FlagsMan.ClearAutoTag(valueAsNullable.Value);
							num3 = null;
							ElcEventProcessor.Tracer.TraceDebug<MailboxSession, string>((long)this.GetHashCode(), "{0}: Parent tag is personal. Clearing the autotag bit from item and deleting retention period. Item: {1}", mailboxSession, (item is MessageItem) ? ((MessageItem)item).Subject : item.ClassName);
						}
						flag6 = false;
					}
				}
				else if (!flag8)
				{
					flag = true;
				}
			}
			else
			{
				ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, string, Item>((long)this.GetHashCode(), "{0}: The item has an explicit tag. Item subject: {1}. {2}", this, (item is MessageItem) ? ((MessageItem)item).Subject : item.ClassName, item);
				StoreTagData storeTagData = null;
				mailboxState.StoreTagDictionary.TryGetValue(guid.Value, out storeTagData);
				if (storeTagData == null)
				{
					storeTagData = mailboxState.GetDefaultTag();
				}
				if (storeTagData != null)
				{
					guid3 = storeTagData.Tag.RetentionId;
					num3 = new int?(this.GetRetentionPeriod(mailboxState, guid3, item.ClassName));
					compositeProperty2.Integer = this.GetDefaultRetentionPeriod(mailboxState, item.ClassName);
					if (compositeProperty2.Date != DateTime.MinValue && num3 != 0)
					{
						try
						{
							dateTime = compositeProperty2.Date.Value.AddDays((double)num3.Value);
							goto IL_65E;
						}
						catch (ArgumentOutOfRangeException)
						{
							ElcEventProcessor.Tracer.TraceDebug<MailboxSession, DateTime, int>((long)this.GetHashCode(), "{0}: Corrupted Data. Could not AddDays {2} to {1}", mailboxSession, compositeProperty2.Date.Value, num3.Value);
							dateTime = DateTime.MaxValue;
							goto IL_65E;
						}
					}
					dateTime = DateTime.MaxValue;
					IL_65E:
					flag6 = false;
				}
				else
				{
					flag3 = false;
				}
			}
			StoreTagData storeTagData2 = null;
			if (!TagAssistantHelper.IsTagNull(guid2))
			{
				mailboxState.StoreTagDictionary.TryGetValue(guid2.Value, out storeTagData2);
				if (storeTagData2 == null && b2 != null)
				{
					flag4 = false;
				}
				else
				{
					guid4 = guid2.Value;
				}
			}
			bool flag10 = true;
			if (TagAssistantHelper.IsTagNull(new Guid?(guid4)) || guid4.Equals(Guid.Empty) || num2 == null)
			{
				bool flag11 = false;
				bool flag12 = this.TryFindInheritedTag(mailboxState, mailboxSession, parentId, TagType.ArchiveTag, item, out guid4, out empty, out flag11);
				if (flag11 && flag5)
				{
					ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor>((long)this.GetHashCode(), "{0}: When trying to find inherited archive tag, limit reached so clear archive policy settings on item.", this);
					flag2 = true;
				}
				if (flag12)
				{
					mailboxState.StoreTagDictionary.TryGetValue(guid4, out storeTagData2);
					if (storeTagData2 == null)
					{
						if (b2 != null)
						{
							flag4 = false;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag10 = false;
					}
				}
				else if (!flag11)
				{
					flag2 = true;
				}
			}
			if (storeTagData2 != null)
			{
				int archivePeriod = this.GetArchivePeriod(storeTagData2);
				guid4 = storeTagData2.Tag.RetentionId;
				if (!flag2 && mailboxState.StoreTagDictionary.ContainsKey(guid4) && mailboxState.StoreTagDictionary[guid4].Tag.Type == ElcFolderType.Personal)
				{
					flag7 = false;
				}
				if (flag10)
				{
					num4 = new int?(archivePeriod);
				}
				else
				{
					num4 = null;
				}
				if (archivePeriod != 0 && archivePeriod != 2147483647)
				{
					try
					{
						d = compositeProperty2.Date.Value.AddDays((double)archivePeriod);
						goto IL_801;
					}
					catch (ArgumentOutOfRangeException)
					{
						ElcEventProcessor.Tracer.TraceDebug<MailboxSession, DateTime, int>((long)this.GetHashCode(), "{0}: Corrupted Data. Could not AddDays {2} to {1}", mailboxSession, compositeProperty2.Date.Value, archivePeriod);
						d = DateTime.MaxValue;
						goto IL_801;
					}
				}
				d = DateTime.MaxValue;
			}
			IL_801:
			bool flag13 = false;
			if (flag2)
			{
				if (guid2 != null)
				{
					item.DeleteProperties(new PropertyDefinition[]
					{
						StoreObjectSchema.ArchiveTag
					});
					flag13 = true;
				}
				if (b2 != null)
				{
					item.DeleteProperties(new PropertyDefinition[]
					{
						ItemSchema.ArchiveDate
					});
					flag13 = true;
				}
				if (num2 != null)
				{
					item.DeleteProperties(new PropertyDefinition[]
					{
						StoreObjectSchema.ArchivePeriod
					});
					flag13 = true;
				}
				flag4 = false;
			}
			if (flag)
			{
				ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, string, Item>((long)this.GetHashCode(), "{0}: All retention props will be deleted. Item subject: {1}. {2}", this, (item is MessageItem) ? ((MessageItem)item).Subject : item.ClassName, item);
				if (guid != null)
				{
					item.DeleteProperties(new PropertyDefinition[]
					{
						StoreObjectSchema.PolicyTag
					});
					flag13 = true;
				}
				if (num != null)
				{
					item.DeleteProperties(new PropertyDefinition[]
					{
						StoreObjectSchema.RetentionPeriod
					});
					flag13 = true;
				}
				if (b != null)
				{
					item.DeleteProperties(new PropertyDefinition[]
					{
						ItemSchema.RetentionDate
					});
					flag13 = true;
				}
				flag3 = false;
			}
			if (flag2 && flag)
			{
				if (compositeProperty != null || valueOrDefault != null)
				{
					item.DeleteProperties(new PropertyDefinition[]
					{
						ItemSchema.StartDateEtc
					});
					flag13 = true;
				}
				if (valueAsNullable != null)
				{
					item.DeleteProperties(new PropertyDefinition[]
					{
						StoreObjectSchema.RetentionFlags
					});
					flag13 = true;
				}
			}
			bool flag14 = false;
			if (flag4)
			{
				if (guid2 == null || guid2 != guid4)
				{
					item[StoreObjectSchema.ArchiveTag] = guid4.ToByteArray();
					flag14 = true;
					flag13 = true;
				}
				if (num4 != null)
				{
					if (num2 == null || num2 != num4)
					{
						item[StoreObjectSchema.ArchivePeriod] = num4;
						flag13 = true;
					}
				}
				else if (num2 != null)
				{
					item.DeleteProperties(new PropertyDefinition[]
					{
						StoreObjectSchema.ArchivePeriod
					});
					flag13 = true;
				}
				if (d == DateTime.MaxValue && b2 != null)
				{
					item.DeleteProperties(new PropertyDefinition[]
					{
						ItemSchema.ArchiveDate
					});
					flag13 = true;
				}
				else if (d != DateTime.MaxValue)
				{
					long fileTime = 0L;
					try
					{
						fileTime = d.ToFileTimeUtc();
					}
					catch (ArgumentOutOfRangeException)
					{
						fileTime = 0L;
						ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, Item, DateTime>((long)this.GetHashCode(), "{0}: The item {1} archive date {2} is out of the rainge of MAPI dates. ", this, item, dateTime);
					}
					DateTime dateTime2 = DateTime.FromFileTimeUtc(fileTime);
					if (b2 == null)
					{
						item[ItemSchema.ArchiveDate] = dateTime2;
						flag13 = true;
					}
					else if (!TagAssistantHelper.DateSlushyEquals(new DateTime?(dateTime2), b2))
					{
						item[ItemSchema.ArchiveDate] = dateTime2;
						flag13 = true;
					}
				}
			}
			bool flag15 = false;
			if (flag3)
			{
				if (guid == null || guid != guid3)
				{
					item[StoreObjectSchema.PolicyTag] = guid3.ToByteArray();
					flag15 = true;
					flag13 = true;
				}
				if (num3 == null && num != null)
				{
					item.DeleteProperties(new PropertyDefinition[]
					{
						StoreObjectSchema.RetentionPeriod
					});
					flag13 = true;
				}
				else if (num3 != null && (num == null || num3 != num))
				{
					item[StoreObjectSchema.RetentionPeriod] = num3;
					flag13 = true;
				}
				if (dateTime == DateTime.MaxValue && b != null)
				{
					item.DeleteProperties(new PropertyDefinition[]
					{
						ItemSchema.RetentionDate
					});
					flag13 = true;
				}
				else if (dateTime != DateTime.MaxValue)
				{
					long fileTime2 = 0L;
					try
					{
						fileTime2 = dateTime.ToFileTimeUtc();
					}
					catch (ArgumentOutOfRangeException)
					{
						fileTime2 = 0L;
						ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, Item, DateTime>((long)this.GetHashCode(), "{0}: The item {1} retention date {2} is out of the rainge of MAPI dates. ", this, item, dateTime);
					}
					DateTime dateTime3 = DateTime.FromFileTimeUtc(fileTime2);
					if (b == null)
					{
						item[ItemSchema.RetentionDate] = dateTime3;
						flag13 = true;
					}
					else if (!TagAssistantHelper.DateSlushyEquals(new DateTime?(dateTime3), b))
					{
						item[ItemSchema.RetentionDate] = dateTime3;
						flag13 = true;
					}
				}
			}
			if (flag4 || flag3)
			{
				if (compositeProperty == null || compositeProperty2.Integer != compositeProperty.Integer || !TagAssistantHelper.DateSlushyEquals(compositeProperty2.Date, compositeProperty.Date))
				{
					item[ItemSchema.StartDateEtc] = compositeProperty2.GetBytes(true);
					flag13 = true;
				}
				if ((valueAsNullable ?? 0) != num5)
				{
					item[StoreObjectSchema.RetentionFlags] = num5;
					flag13 = true;
				}
				if (!flag6 || !flag7)
				{
					ELCPerfmon.TotalItemsWithPersonalTag.Increment();
				}
				else
				{
					ELCPerfmon.TotalItemsWithDefaultTag.Increment();
				}
			}
			if (flag13)
			{
				if (flag15 || flag14)
				{
					this.RemoveEhaMigrationDate(item);
				}
				ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, string, Item>((long)this.GetHashCode(), "{0}: One or more retention props will be saved. Item subject: {1}. {2}", this, (item is MessageItem) ? ((MessageItem)item).Subject : item.ClassName, item);
				ElcEventProcessor.ItemSave(mailboxSession, item);
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00024058 File Offset: 0x00022258
		private void RemoveEhaMigrationDate(Item item)
		{
			object obj = item.TryGetProperty(ItemSchema.EHAMigrationExpiryDate);
			if (obj != null && obj is ExDateTime)
			{
				item.DeleteProperties(new PropertyDefinition[]
				{
					ItemSchema.EHAMigrationExpiryDate
				});
				ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, string, Item>((long)this.GetHashCode(), "{0}: This is an eha migration message, policy changed on this item , hence deleting the ehamigrationdate property. {1}. {2}", this, (item is MessageItem) ? ((MessageItem)item).Subject : item.ClassName, item);
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000240C8 File Offset: 0x000222C8
		private bool TryFindInheritedTag(UserRetentionPolicyCache mailboxState, MailboxSession mailboxSession, StoreObjectId parentId, TagType findTag, Item item, out Guid tagGuid, out Guid theVmTagGuid, out bool limitReached)
		{
			bool flag = false;
			StoreObjectId storeObjectId = parentId;
			tagGuid = Guid.Empty;
			theVmTagGuid = Guid.Empty;
			Folder folder = null;
			limitReached = false;
			try
			{
				ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor>((long)this.GetHashCode(), "{0}: Start: TryFindInheritedTag for finding implict tag for a given item or folder.", this);
				int num = this.DepthLimit;
				ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, int>((long)this.GetHashCode(), "{0}: Looking for inherited tag with a depth limit of {1}.", this, num);
				string text = string.Empty;
				if (item != null)
				{
					text = ElcPolicySettings.GetEffectiveItemClass(item.ClassName);
				}
				while (!flag)
				{
					bool flag2 = false;
					if (findTag.Equals(TagType.PolicyTag))
					{
						if ((mailboxState.DefaultVmTag != null && !text.StartsWith(ElcMessageClass.VoiceMail.TrimEnd(new char[]
						{
							'*'
						}), StringComparison.OrdinalIgnoreCase)) || mailboxState.DefaultVmTag == null)
						{
							flag2 = mailboxState.FolderTagDictionary.TryGetValue(storeObjectId, out tagGuid);
						}
					}
					else
					{
						flag2 = mailboxState.FolderArchiveTagDictionary.TryGetValue(storeObjectId, out tagGuid);
					}
					if (!flag2)
					{
						Exception ex = null;
						try
						{
							ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, StoreObjectId>((long)this.GetHashCode(), "{0}: Start: Folder binding to bind the folder for Policy and Archive tag: FolderId: {1}", this, storeObjectId);
							folder = Folder.Bind(mailboxSession, storeObjectId, new PropertyDefinition[]
							{
								StoreObjectSchema.PolicyTag,
								StoreObjectSchema.ArchiveTag
							});
							ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, StoreObjectId>((long)this.GetHashCode(), "{0}: Stop: Folder binding to bind the folder for Policy and Archive tag: FolderId : {1}", this, storeObjectId);
						}
						catch (ObjectNotFoundException ex2)
						{
							ex = ex2;
						}
						catch (ConversionFailedException ex3)
						{
							ex = ex3;
						}
						catch (VirusMessageDeletedException ex4)
						{
							ex = ex4;
						}
						if (ex != null)
						{
							ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, Exception>((long)this.GetHashCode(), "{0}: Problems loading a folder. It will not be processed. Exception: {1}", this, ex);
						}
						if (folder == null)
						{
							break;
						}
						ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, StoreObjectId>((long)this.GetHashCode(), "{0}: start: Generating archive guid for folderId for caching. FolderId : {1}.", this, storeObjectId);
						Guid value = ElcMailboxHelper.GetGuidFromBytes(folder.GetValueOrDefault<object>(StoreObjectSchema.ArchiveTag), new Guid?(Guid.Empty), false, storeObjectId).Value;
						ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, StoreObjectId>((long)this.GetHashCode(), "{0}: stop: Generating archive guid for folderId for caching. FolderId : {1}.", this, storeObjectId);
						if (value != Guid.Empty && !mailboxState.FolderArchiveTagDictionary.ContainsKey(storeObjectId))
						{
							ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor>((long)this.GetHashCode(), "{0}: Adding folder archive guid to ArchiveTagDictionary", this);
							mailboxState.FolderArchiveTagDictionary.Add(storeObjectId, value);
							if (findTag.Equals(TagType.ArchiveTag))
							{
								flag = true;
								tagGuid = value;
							}
						}
						ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, StoreObjectId>((long)this.GetHashCode(), "{0}: start: Generating tag guid for folderId for caching. FolderId : {1}.", this, storeObjectId);
						Guid value2 = ElcMailboxHelper.GetGuidFromBytes(folder.GetValueOrDefault<object>(StoreObjectSchema.PolicyTag), new Guid?(Guid.Empty), false, storeObjectId).Value;
						ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, StoreObjectId, Guid>((long)this.GetHashCode(), "{0}: stop: Generating tag guid for folderId for caching. FolderId : {1}, tag : {2}.", this, storeObjectId, tagGuid);
						if (value2 != Guid.Empty && !mailboxState.FolderTagDictionary.ContainsKey(storeObjectId))
						{
							ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor>((long)this.GetHashCode(), "{0}: Adding folder policy guid to TagDictionary", this);
							mailboxState.FolderTagDictionary.Add(storeObjectId, value2);
							if (findTag.Equals(TagType.PolicyTag))
							{
								flag = true;
								tagGuid = value2;
							}
						}
						if (item != null)
						{
							ElcEventProcessor.Tracer.TraceDebug<VersionedId>((long)this.GetHashCode(), "Trying to apply default vm tag guid to this item with ID of: {0}.", item.Id);
						}
						if (mailboxState.DefaultVmTag != null && text.StartsWith(ElcMessageClass.VoiceMail.TrimEnd(new char[]
						{
							'*'
						}), StringComparison.OrdinalIgnoreCase))
						{
							ElcEventProcessor.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "Default vm tag exist and item is vm msg, vm tag guid:{0}", mailboxState.DefaultVmTag.Tag.Guid);
							if (findTag.Equals(TagType.PolicyTag))
							{
								flag = true;
								tagGuid = mailboxState.DefaultVmTag.Tag.Guid;
							}
						}
						if (flag || ArrayComparer<byte>.Comparer.Equals(storeObjectId.ProviderLevelItemId, folder.ParentId.ProviderLevelItemId) || ArrayComparer<byte>.Comparer.Equals(storeObjectId.ProviderLevelItemId, mailboxState.RootFolderId))
						{
							break;
						}
						if (num <= 0)
						{
							ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor>((long)this.GetHashCode(), "{0}: Reached max search depth and tag not found.", this);
							limitReached = true;
							tagGuid = Guid.Empty;
							return false;
						}
						num--;
						storeObjectId = folder.ParentId;
						folder.Dispose();
						folder = null;
					}
					else
					{
						flag = true;
					}
				}
				if (findTag.Equals(TagType.PolicyTag) && !flag)
				{
					StoreTagData defaultTagAndDefaultVmTag = mailboxState.GetDefaultTagAndDefaultVmTag();
					if (mailboxState.DefaultVmTag != null && text.StartsWith(ElcMessageClass.VoiceMail.TrimEnd(new char[]
					{
						'*'
					}), StringComparison.OrdinalIgnoreCase))
					{
						ElcEventProcessor.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "Default vm tag exist and item is vm msg, vm tag guid:{0}", mailboxState.DefaultVmTag.Tag.Guid);
						if (findTag.Equals(TagType.PolicyTag))
						{
							flag = true;
							theVmTagGuid = mailboxState.DefaultVmTag.Tag.Guid;
						}
					}
					if (defaultTagAndDefaultVmTag != null)
					{
						tagGuid = defaultTagAndDefaultVmTag.Tag.RetentionId;
						flag = true;
						ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, Guid, StoreObjectId>((long)this.GetHashCode(), "{0}: Set tagGuid to the default tag: {1}. Parent: {2}", this, tagGuid, parentId);
					}
					else
					{
						ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, StoreObjectId>((long)this.GetHashCode(), "{0}: No default tag found. Parent: {1}", this, parentId);
					}
				}
				if (flag && storeObjectId != null && !ArrayComparer<byte>.Comparer.Equals(storeObjectId.ProviderLevelItemId, parentId.ProviderLevelItemId))
				{
					if (tagGuid == this.nullGuid)
					{
						throw new ArgumentOutOfRangeException();
					}
					if (findTag.Equals(TagType.ArchiveTag) && !mailboxState.FolderArchiveTagDictionary.ContainsKey(parentId))
					{
						ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, StoreObjectId>((long)this.GetHashCode(), "{0}: Caching the expected archive tag for parent folder: {1}", this, parentId);
						mailboxState.FolderArchiveTagDictionary.Add(parentId, tagGuid);
					}
					if (findTag.Equals(TagType.PolicyTag) && !mailboxState.FolderTagDictionary.ContainsKey(parentId))
					{
						ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, StoreObjectId>((long)this.GetHashCode(), "{0}: Caching the default tag for parent folder: {1}", this, parentId);
						mailboxState.FolderTagDictionary.Add(parentId, tagGuid);
					}
				}
			}
			finally
			{
				if (folder != null)
				{
					ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, Folder>((long)this.GetHashCode(), "{0}: Disposing folder before returing: folder : {1}.", this, folder);
					folder.Dispose();
				}
			}
			ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor, bool, Guid>((long)this.GetHashCode(), "{0}: Inherited tag found: {1}. Inherited tag Guid: {2}.", this, flag, tagGuid);
			ElcEventProcessor.Tracer.TraceDebug<ElcEventProcessor>((long)this.GetHashCode(), "{0}: Stop: TryFindInheritedTag", this);
			return flag;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0002478C File Offset: 0x0002298C
		private static int GetElcAssistantInheritedTagDepth()
		{
			int result = 20;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
			{
				if (registryKey != null && registryKey.GetValue("ELCAssistantInheritedTagDepthLimit") != null)
				{
					object value = registryKey.GetValue("ELCAssistantInheritedTagDepthLimit");
					if (value is int && (int)value > 0)
					{
						result = (int)value;
					}
				}
			}
			return result;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00024800 File Offset: 0x00022A00
		private int GetDefaultRetentionPeriod(UserRetentionPolicyCache mailboxState, string className)
		{
			ContentSetting applyingPolicy = ElcPolicySettings.GetApplyingPolicy(mailboxState.DefaultContentSettingList, className, mailboxState.ItemClassToPolicyMapping);
			if (applyingPolicy != null)
			{
				return (int)applyingPolicy.AgeLimitForRetention.Value.TotalDays;
			}
			return 0;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0002483C File Offset: 0x00022A3C
		private int GetRetentionPeriod(UserRetentionPolicyCache mailboxState, Guid tag, string className)
		{
			int result = int.MaxValue;
			StoreTagData storeTagData = null;
			if (mailboxState.StoreTagDictionary.TryGetValue(tag, out storeTagData) && storeTagData != null)
			{
				result = this.GetRetentionPeriod(mailboxState, storeTagData, className);
			}
			return result;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00024870 File Offset: 0x00022A70
		private int GetRetentionPeriod(UserRetentionPolicyCache mailboxState, StoreTagData storeTag, string className)
		{
			int result = 0;
			if (storeTag.Tag.Type != ElcFolderType.All)
			{
				using (SortedDictionary<Guid, ContentSetting>.ValueCollection.Enumerator enumerator = storeTag.ContentSettings.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ContentSetting contentSetting = enumerator.Current;
						if (contentSetting.RetentionEnabled)
						{
							result = (int)contentSetting.AgeLimitForRetention.Value.TotalDays;
							break;
						}
					}
					return result;
				}
			}
			result = this.GetDefaultRetentionPeriod(mailboxState, className);
			return result;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00024900 File Offset: 0x00022B00
		private int GetArchivePeriod(StoreTagData storeTag)
		{
			int result = int.MaxValue;
			foreach (ContentSetting contentSetting in storeTag.ContentSettings.Values)
			{
				if (contentSetting.RetentionEnabled && contentSetting.RetentionAction == RetentionActionType.MoveToArchive)
				{
					result = (int)contentSetting.AgeLimitForRetention.Value.TotalDays;
					break;
				}
			}
			return result;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00024A54 File Offset: 0x00022C54
		private void FolderSave(UserRetentionPolicyCache mailboxState, MailboxSession mailboxSession, Folder folderToSave, Guid? existingTagGuid, int? existingRetentionPeriod, RetentionAndArchiveFlags? existingRetentionFlags, Guid? expectedTagGuid, int? expectedRetentionPeriod, RetentionAndArchiveFlags? expectedRetentionFlags)
		{
			ElcEventProcessor.<>c__DisplayClassc CS$<>8__locals1 = new ElcEventProcessor.<>c__DisplayClassc();
			CS$<>8__locals1.mailboxSession = mailboxSession;
			CS$<>8__locals1.folderToSave = folderToSave;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.mustSave = false;
			if (expectedTagGuid != null)
			{
				if (existingTagGuid == null || existingTagGuid != expectedTagGuid)
				{
					CS$<>8__locals1.folderToSave[StoreObjectSchema.PolicyTag] = expectedTagGuid.Value.ToByteArray();
					mailboxState.FolderTagDictionary[CS$<>8__locals1.folderToSave.Id.ObjectId] = expectedTagGuid.Value;
					CS$<>8__locals1.mustSave = true;
				}
			}
			else if (existingTagGuid != null)
			{
				CS$<>8__locals1.folderToSave.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.PolicyTag
				});
				mailboxState.FolderTagDictionary.Remove(CS$<>8__locals1.folderToSave.Id.ObjectId);
				CS$<>8__locals1.mustSave = true;
			}
			if (expectedRetentionPeriod != null)
			{
				if (existingRetentionPeriod == null || existingRetentionPeriod.Value != expectedRetentionPeriod.Value)
				{
					CS$<>8__locals1.folderToSave[StoreObjectSchema.RetentionPeriod] = expectedRetentionPeriod;
					CS$<>8__locals1.mustSave = true;
				}
			}
			else if (existingRetentionPeriod != null)
			{
				CS$<>8__locals1.folderToSave.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.RetentionPeriod
				});
				CS$<>8__locals1.mustSave = true;
			}
			if (expectedRetentionFlags != null)
			{
				if (existingRetentionFlags == null || existingRetentionFlags.Value != expectedRetentionFlags.Value)
				{
					CS$<>8__locals1.folderToSave[StoreObjectSchema.RetentionFlags] = (int)expectedRetentionFlags.Value;
					CS$<>8__locals1.mustSave = true;
				}
			}
			else if (existingRetentionFlags != null)
			{
				CS$<>8__locals1.folderToSave.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.RetentionFlags
				});
				CS$<>8__locals1.mustSave = true;
			}
			CS$<>8__locals1.folderName = CS$<>8__locals1.folderToSave.DisplayName;
			ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<FolderSave>b__a)), new FilterDelegate(null, (UIntPtr)ldftn(ExceptionFilter)), new CatchDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<FolderSave>b__b)));
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00024C88 File Offset: 0x00022E88
		private void AdjustExpectedTagIfItemAutotagged(int? existingRetentionFlags, Guid? existingTagGuid, ref Guid expectedTagGuid, UserRetentionPolicyCache mailboxState, MailboxSession mailboxSession)
		{
			if (FlagsMan.IsAutoTagSet(existingRetentionFlags) && (expectedTagGuid.Equals(Guid.Empty) || (mailboxState.StoreTagDictionary.ContainsKey(expectedTagGuid) && mailboxState.StoreTagDictionary[expectedTagGuid].Tag.Type != ElcFolderType.Personal)))
			{
				if (TagAssistantHelper.IsTagNull(existingTagGuid) || existingTagGuid.Value.Equals(Guid.Empty))
				{
					ElcEventProcessor.Tracer.TraceDebug<MailboxSession, Guid>((long)this.GetHashCode(), "{0}: AdjustExpectedTagIfItemAutotagged: existingTagGuid is missing. The user must have cleared it. The expectedTagGuid {1} stays unchanged.", mailboxSession, expectedTagGuid);
					return;
				}
				expectedTagGuid = existingTagGuid.Value;
				ElcEventProcessor.Tracer.TraceDebug<MailboxSession, Guid>((long)this.GetHashCode(), "{0}: AdjustExpectedTagIfItemAutotagged: existingTagGuid exists. Setting the expectedTagGuid to it - {1}.", mailboxSession, expectedTagGuid);
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00024D54 File Offset: 0x00022F54
		private bool IsOldParentFolderDeletedItems(MapiEvent mapiEvent, IMailboxSession session)
		{
			StoreObjectId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.DeletedItems);
			return mapiEvent.OldParentEntryId != null && object.Equals(defaultFolderId, StoreObjectId.FromProviderSpecificId(mapiEvent.OldParentEntryId));
		}

		// Token: 0x040003A6 RID: 934
		private static readonly Trace Tracer = ExTraceGlobals.EventBasedAssistantTracer;

		// Token: 0x040003A7 RID: 935
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x040003A8 RID: 936
		private static ElcEventProcessor elcEventProcessor = new ElcEventProcessor();

		// Token: 0x040003A9 RID: 937
		private Guid nullGuid = Guid.Empty;

		// Token: 0x040003AA RID: 938
		private int? m_depthLimit = null;
	}
}
