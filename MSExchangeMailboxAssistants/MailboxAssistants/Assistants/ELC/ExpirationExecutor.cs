using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000037 RID: 55
	internal class ExpirationExecutor
	{
		// Token: 0x060001AA RID: 426 RVA: 0x0000C088 File Offset: 0x0000A288
		internal ExpirationExecutor(MailboxData mailboxData, ElcSubAssistant elcAssistant, Trace tracer)
		{
			this.MailboxData = mailboxData;
			this.ElcAssistant = elcAssistant;
			this.tracer = tracer;
			this.isReportEnabled = false;
			this.SoftDeleteList = new List<ItemData>();
			this.HardDeleteList = new List<ItemData>();
			this.HardDeleteNoCalLoggingList = new List<ItemData>();
			this.MoveToArchiveList = new List<ItemData>();
			this.MoveToArchiveDumpsterList = new Dictionary<DefaultFolderType, List<ItemData>>();
			this.MoveToPurgesList = new List<ItemData>();
			this.MoveToPurgesNoCalLoggingList = new List<ItemData>();
			this.MoveToDiscoveryHoldsList = new List<ItemData>();
			this.MoveToDiscoveryHoldsNoCalLoggingList = new List<ItemData>();
			this.MoveToMigratedMessagesList = new List<ItemData>();
			this.ELCReport = new Dictionary<RetentionActionType, List<List<object>>>();
			this.elcReportOverflow = false;
			this.separationIndices = new Dictionary<RetentionActionType, int>();
			this.separationIndices.Add(RetentionActionType.DeleteAndAllowRecovery, 0);
			this.separationIndices.Add(RetentionActionType.PermanentlyDelete, 0);
			this.separationIndices.Add(RetentionActionType.MoveToArchive, 0);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000C169 File Offset: 0x0000A369
		// (set) Token: 0x060001AC RID: 428 RVA: 0x0000C171 File Offset: 0x0000A371
		protected MailboxData MailboxData { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000C17A File Offset: 0x0000A37A
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000C182 File Offset: 0x0000A382
		protected List<ItemData> MoveToPurgesList { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000C18B File Offset: 0x0000A38B
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000C193 File Offset: 0x0000A393
		protected List<ItemData> MoveToPurgesNoCalLoggingList { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000C19C File Offset: 0x0000A39C
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
		protected List<ItemData> MoveToDiscoveryHoldsList { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000C1B5 File Offset: 0x0000A3B5
		protected List<ItemData> MoveToMigratedMessagesList { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000C1BE File Offset: 0x0000A3BE
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000C1C6 File Offset: 0x0000A3C6
		protected List<ItemData> MoveToDiscoveryHoldsNoCalLoggingList { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000C1CF File Offset: 0x0000A3CF
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000C1D7 File Offset: 0x0000A3D7
		protected List<ItemData> SoftDeleteList { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
		protected List<ItemData> HardDeleteList { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000C1F1 File Offset: 0x0000A3F1
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000C1F9 File Offset: 0x0000A3F9
		protected List<ItemData> HardDeleteNoCalLoggingList { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000C202 File Offset: 0x0000A402
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000C20A File Offset: 0x0000A40A
		protected List<ItemData> MoveToArchiveList { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000C213 File Offset: 0x0000A413
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000C21B File Offset: 0x0000A41B
		protected Dictionary<DefaultFolderType, List<ItemData>> MoveToArchiveDumpsterList { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000C224 File Offset: 0x0000A424
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000C22C File Offset: 0x0000A42C
		internal ElcSubAssistant ElcAssistant { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000C235 File Offset: 0x0000A435
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000C23D File Offset: 0x0000A43D
		internal Dictionary<RetentionActionType, List<List<object>>> ELCReport { get; set; }

		// Token: 0x060001C5 RID: 453 RVA: 0x0000C246 File Offset: 0x0000A446
		internal void CheckReportingStatus()
		{
			this.isReportEnabled = this.MailboxData.ElcReporter.CheckReportingStatus();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C260 File Offset: 0x0000A460
		internal void AddToReportAndDoomedList(object[] itemProperties, PropertyIndexHolder propertyIndexHolder, ContentSetting settings, Dictionary<Guid, string> allPolicyTags, ItemData.EnforcerType enforcerType, bool disableCalendarLogging)
		{
			this.AddToReport(settings.RetentionAction, itemProperties, propertyIndexHolder, allPolicyTags);
			this.AddToDoomedList(new ItemData((VersionedId)itemProperties[propertyIndexHolder.IdIndex], (StoreObjectId)itemProperties[propertyIndexHolder.ParentItemIdIndex], enforcerType, (int)itemProperties[propertyIndexHolder.SizeIndex]), settings, disableCalendarLogging);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000C2B4 File Offset: 0x0000A4B4
		protected void AddToReport(RetentionActionType policy, object[] itemProperties, PropertyIndexHolder propertyIndexHolder, Dictionary<Guid, string> allPolicyTags)
		{
			try
			{
				if (this.isReportEnabled)
				{
					object propertyObject = this.GetPropertyObject(itemProperties[propertyIndexHolder.ConversationTopic]);
					object propertyObject2 = this.GetPropertyObject(itemProperties[propertyIndexHolder.MessageSenderDisplayName]);
					object propertyObject3 = this.GetPropertyObject(itemProperties[propertyIndexHolder.ParentDisplayName]);
					object propertyObject4 = this.GetPropertyObject(itemProperties[propertyIndexHolder.ReceivedTimeIndex]);
					object propertyObject5 = this.GetPropertyObject(itemProperties[propertyIndexHolder.LastModifiedTime]);
					Guid key = (itemProperties[propertyIndexHolder.PolicyTagIndex] is PropertyError) ? Guid.Empty : new Guid((byte[])itemProperties[propertyIndexHolder.PolicyTagIndex]);
					Guid key2 = (itemProperties[propertyIndexHolder.ArchiveTagIndex] is PropertyError) ? Guid.Empty : new Guid((byte[])itemProperties[propertyIndexHolder.ArchiveTagIndex]);
					bool flag = (!(itemProperties[propertyIndexHolder.MessageToMe] is PropertyError) && (bool)itemProperties[propertyIndexHolder.MessageToMe]) || (!(itemProperties[propertyIndexHolder.MessageCcMe] is PropertyError) && (bool)itemProperties[propertyIndexHolder.MessageCcMe]);
					object obj = key.Equals(Guid.Empty) ? Strings.ElcEmailDefaultTag : (allPolicyTags.ContainsKey(key) ? allPolicyTags[key] : Strings.ElcEmailUnknownTag);
					object obj2 = key2.Equals(Guid.Empty) ? Strings.ElcEmailDefaultTag : (allPolicyTags.ContainsKey(key2) ? allPolicyTags[key2] : Strings.ElcEmailUnknownTag);
					object item = null;
					object item2 = null;
					if (policy.Equals(RetentionActionType.MoveToArchive))
					{
						item = obj2;
						item2 = obj;
					}
					else
					{
						item = obj;
						item2 = obj2;
					}
					if (!this.ELCReport.ContainsKey(policy))
					{
						this.ELCReport.Add(policy, new List<List<object>>());
					}
					if (this.ELCReport[policy].Count < ElcGlobals.ReportItemLimit)
					{
						foreach (List<object> list in this.ELCReport[policy])
						{
							if (list[0].Equals(itemProperties[propertyIndexHolder.ConversationId]))
							{
								if (((ExDateTime)propertyObject4).CompareTo(list[4]) < 0)
								{
									list[2] = propertyObject2;
									list[3] = propertyObject3;
									list[6] = propertyObject4;
									list[7] = propertyObject5;
									list[8] = (int)list[8] + 1;
								}
								return;
							}
						}
						List<object> list2 = new List<object>(9);
						list2.Add(itemProperties[propertyIndexHolder.ConversationId]);
						list2.Add(propertyObject);
						list2.Add(propertyObject2);
						list2.Add(propertyObject3);
						list2.Add(item);
						list2.Add(item2);
						list2.Add(propertyObject4);
						list2.Add(propertyObject5);
						list2.Add(1);
						if (flag)
						{
							this.ELCReport[policy].Insert(0, list2);
							this.separationIndices[policy] = this.separationIndices[policy] + 1;
						}
						else
						{
							this.ELCReport[policy].Add(list2);
						}
					}
					else
					{
						foreach (List<object> list3 in this.ELCReport[policy])
						{
							if (list3[0].Equals(itemProperties[propertyIndexHolder.ConversationId]))
							{
								if (((ExDateTime)itemProperties[propertyIndexHolder.ReceivedTimeIndex]).CompareTo(list3[6]) < 0)
								{
									list3[2] = propertyObject2;
									list3[3] = propertyObject3;
									list3[6] = propertyObject4;
									list3[7] = propertyObject5;
									list3[8] = (int)list3[8] + 1;
								}
								return;
							}
						}
						if (this.separationIndices[policy] < ElcGlobals.ReportItemLimit)
						{
							if (flag)
							{
								List<object> list4 = new List<object>(9);
								list4.Add(itemProperties[propertyIndexHolder.ConversationId]);
								list4.Add(propertyObject);
								list4.Add(propertyObject2);
								list4.Add(propertyObject3);
								list4.Add(item);
								list4.Add(item2);
								list4.Add(propertyObject4);
								list4.Add(propertyObject5);
								list4.Add(1);
								this.ELCReport[policy][this.separationIndices[policy]] = list4;
								this.separationIndices[policy] = this.separationIndices[policy] + 1;
								this.elcReportOverflow = true;
							}
							else
							{
								this.elcReportOverflow = true;
							}
						}
						else
						{
							this.elcReportOverflow = true;
						}
					}
				}
			}
			catch (IndexOutOfRangeException ex)
			{
				this.tracer.TraceError<ExpirationExecutor>((long)this.GetHashCode(), "{0}: Didn't receive a parameter for an item that's being reported: " + ex.Message, this);
				StatisticsLogEntry statisticsLogEntry = this.MailboxData.StatisticsLogEntry;
				statisticsLogEntry.ExceptionType += ex.GetType();
				this.MailboxData.StatisticsLogEntry.AddExceptionToLog(ex);
			}
			finally
			{
				if (this.ELCReport.ContainsKey(policy) && this.ELCReport[policy].Count == 0)
				{
					this.ELCReport.Remove(policy);
				}
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000C86C File Offset: 0x0000AA6C
		protected void AddToDoomedList(ItemData itemData, ContentSetting elcPolicy)
		{
			this.AddToDoomedList(itemData, elcPolicy, false);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000C878 File Offset: 0x0000AA78
		protected void AddToDoomedList(ItemData itemData, ContentSetting elcPolicy, bool disableCalendarLogging)
		{
			switch (elcPolicy.RetentionAction)
			{
			case RetentionActionType.DeleteAndAllowRecovery:
				this.AddToDoomedSoftDeleteList(itemData);
				return;
			case RetentionActionType.PermanentlyDelete:
				this.AddToDoomedHardDeleteList(itemData, disableCalendarLogging);
				return;
			case RetentionActionType.MoveToArchive:
				this.AddToDoomedMoveToArchiveList(itemData);
				return;
			}
			this.tracer.TraceDebug<ExpirationExecutor, RetentionActionType, VersionedId>((long)this.GetHashCode(), "{0}: Abnormal retention action {1} found for item {2}. Skip the item.", this, elcPolicy.RetentionAction, itemData.Id);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		internal void AddToDoomedMoveToArchiveDumpsterList(ItemData itemData, DefaultFolderType folderType)
		{
			if (!this.MoveToArchiveDumpsterList.ContainsKey(folderType))
			{
				this.MoveToArchiveDumpsterList[folderType] = new List<ItemData>();
			}
			this.MoveToArchiveDumpsterList[folderType].Add(itemData);
			this.CheckAndProcessItemsOnBatchSizeReached(this.MoveToArchiveDumpsterList[folderType]);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000C934 File Offset: 0x0000AB34
		internal void AddToDoomedMoveToPurgesList(ItemData itemData, bool disableCalendarLogging)
		{
			if (disableCalendarLogging)
			{
				this.MoveToPurgesNoCalLoggingList.Add(itemData);
				this.CheckAndProcessItemsOnBatchSizeReached(this.MoveToPurgesNoCalLoggingList);
				return;
			}
			this.MoveToPurgesList.Add(itemData);
			this.CheckAndProcessItemsOnBatchSizeReached(this.MoveToPurgesList);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000C96A File Offset: 0x0000AB6A
		internal void AddToDoomedMoveToDiscoveryHoldsList(ItemData itemData, bool disableCalendarLogging)
		{
			if (disableCalendarLogging)
			{
				this.MoveToDiscoveryHoldsNoCalLoggingList.Add(itemData);
				this.CheckAndProcessItemsOnBatchSizeReached(this.MoveToDiscoveryHoldsNoCalLoggingList);
				return;
			}
			this.MoveToDiscoveryHoldsList.Add(itemData);
			this.CheckAndProcessItemsOnBatchSizeReached(this.MoveToDiscoveryHoldsList);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		internal void AddToDoomedMoveToMigratedMessagesList(ItemData itemData)
		{
			this.MoveToMigratedMessagesList.Add(itemData);
			this.CheckAndProcessItemsOnBatchSizeReached(this.MoveToMigratedMessagesList);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000C9BA File Offset: 0x0000ABBA
		internal void AddToDoomedSoftDeleteList(ItemData itemData)
		{
			this.SoftDeleteList.Add(itemData);
			this.CheckAndProcessItemsOnBatchSizeReached(this.SoftDeleteList);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		internal void AddToDoomedHardDeleteList(ItemData itemData, bool disableCalendarLogging)
		{
			if (disableCalendarLogging)
			{
				this.HardDeleteNoCalLoggingList.Add(itemData);
				this.CheckAndProcessItemsOnBatchSizeReached(this.HardDeleteNoCalLoggingList);
				return;
			}
			this.HardDeleteList.Add(itemData);
			this.CheckAndProcessItemsOnBatchSizeReached(this.HardDeleteList);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000CA0A File Offset: 0x0000AC0A
		internal void AddToDoomedMoveToArchiveList(ItemData itemData)
		{
			this.MoveToArchiveList.Add(itemData);
			this.CheckAndProcessItemsOnBatchSizeReached(this.MoveToArchiveList);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000CA24 File Offset: 0x0000AC24
		internal virtual void ExecuteTheDoomed()
		{
			if (this.MoveToArchiveList.Count > 0)
			{
				this.PrepareAndExpireInBatches(this.MoveToArchiveList, ExpirationExecutor.Action.MoveToArchive);
				this.tracer.TraceDebug<ExpirationExecutor, int>((long)this.GetHashCode(), "{0}: {1} items moved to archive.", this, this.MoveToArchiveList.Count);
				ExpirationExecutor.TracerPfd.TracePfd<int, ExpirationExecutor, int>((long)this.GetHashCode(), "PFD IWE {0} {1}: {2} items moved to archive.", 16663, this, this.MoveToArchiveList.Count);
			}
			if (this.MoveToArchiveDumpsterList.Count > 0)
			{
				this.PrepareAndExpireInBatches(this.MoveToArchiveDumpsterList, ExpirationExecutor.Action.MoveToArchiveDumpster);
				this.tracer.TraceDebug<ExpirationExecutor, int>((long)this.GetHashCode(), "{0}: {1} items moved to archive dumpster.", this, this.MoveToArchiveDumpsterList.Count);
				ExpirationExecutor.TracerPfd.TracePfd<int, ExpirationExecutor, int>((long)this.GetHashCode(), "PFD IWE {0} {1}: {2} items moved to archive dumpster.", 16663, this, this.MoveToArchiveDumpsterList.Count);
			}
			if (this.SoftDeleteList.Count > 0)
			{
				this.ExpireInBatches(this.SoftDeleteList, ExpirationExecutor.Action.SoftDelete);
				this.tracer.TraceDebug<ExpirationExecutor, int>((long)this.GetHashCode(), "{0}: {1} items soft deleted.", this, this.SoftDeleteList.Count);
				ExpirationExecutor.TracerPfd.TracePfd<int, ExpirationExecutor, int>((long)this.GetHashCode(), "PFD IWE {0} {1}: {2} items soft deleted.", 16663, this, this.SoftDeleteList.Count);
			}
			if (this.HardDeleteList.Count > 0)
			{
				this.ExpireInBatches(this.HardDeleteList, ExpirationExecutor.Action.PermanentlyDelete);
				this.tracer.TraceDebug<ExpirationExecutor, int>((long)this.GetHashCode(), "{0}: {1} items hard deleted.", this, this.HardDeleteList.Count);
				ExpirationExecutor.TracerPfd.TracePfd<int, ExpirationExecutor, int>((long)this.GetHashCode(), "PFD IWE {0} {1}: {2} items hard deleted.", 24855, this, this.HardDeleteList.Count);
			}
			if (this.MoveToPurgesList.Count > 0)
			{
				this.PrepareAndExpireInBatches(this.MoveToPurgesList, ExpirationExecutor.Action.MoveToPurges);
				this.tracer.TraceDebug<ExpirationExecutor, int>((long)this.GetHashCode(), "{0}: {1} items are moved to purges folder.", this, this.MoveToPurgesList.Count);
				ExpirationExecutor.TracerPfd.TracePfd<int, ExpirationExecutor, int>((long)this.GetHashCode(), "PFD IWE {0} {1}: {2} items are moved to purges folder.", 24855, this, this.MoveToPurgesList.Count);
			}
			if (this.MoveToPurgesNoCalLoggingList.Count > 0)
			{
				bool temporaryDisableCalendarLogging = this.MailboxData.MailboxSession.COWSettings.TemporaryDisableCalendarLogging;
				try
				{
					this.MailboxData.MailboxSession.COWSettings.TemporaryDisableCalendarLogging = true;
					this.PrepareAndExpireInBatches(this.MoveToPurgesNoCalLoggingList, ExpirationExecutor.Action.MoveToPurges);
					this.tracer.TraceDebug<ExpirationExecutor, int>((long)this.GetHashCode(), "{0}: {1} items are moved to purges folder with no cal logging.", this, this.MoveToPurgesNoCalLoggingList.Count);
					ExpirationExecutor.TracerPfd.TracePfd<int, ExpirationExecutor, int>((long)this.GetHashCode(), "PFD IWE {0} {1}: {2} items are moved to purges folder with no cal logging.", 24855, this, this.MoveToPurgesNoCalLoggingList.Count);
				}
				finally
				{
					this.MailboxData.MailboxSession.COWSettings.TemporaryDisableCalendarLogging = temporaryDisableCalendarLogging;
				}
			}
			if (this.MoveToDiscoveryHoldsList.Count > 0)
			{
				this.PrepareAndExpireInBatches(this.MoveToDiscoveryHoldsList, ExpirationExecutor.Action.MoveToDiscoveryHolds);
				this.tracer.TraceDebug<ExpirationExecutor, int>((long)this.GetHashCode(), "{0}: {1} items are moved to discovery holds folder.", this, this.MoveToDiscoveryHoldsList.Count);
				ExpirationExecutor.TracerPfd.TracePfd<int, ExpirationExecutor, int>((long)this.GetHashCode(), "PFD IWE {0} {1}: {2} items are moved to discovery holds folder.", 24855, this, this.MoveToDiscoveryHoldsList.Count);
			}
			if (this.MoveToDiscoveryHoldsNoCalLoggingList.Count > 0)
			{
				bool temporaryDisableCalendarLogging2 = this.MailboxData.MailboxSession.COWSettings.TemporaryDisableCalendarLogging;
				try
				{
					this.MailboxData.MailboxSession.COWSettings.TemporaryDisableCalendarLogging = true;
					this.PrepareAndExpireInBatches(this.MoveToDiscoveryHoldsNoCalLoggingList, ExpirationExecutor.Action.MoveToDiscoveryHolds);
					this.tracer.TraceDebug<ExpirationExecutor, int>((long)this.GetHashCode(), "{0}: {1} items are moved to discovery holds folder with no cal logging.", this, this.MoveToDiscoveryHoldsNoCalLoggingList.Count);
					ExpirationExecutor.TracerPfd.TracePfd<int, ExpirationExecutor, int>((long)this.GetHashCode(), "PFD IWE {0} {1}: {2} items are moved to discovery holds folder with no cal logging.", 24855, this, this.MoveToDiscoveryHoldsNoCalLoggingList.Count);
				}
				finally
				{
					this.MailboxData.MailboxSession.COWSettings.TemporaryDisableCalendarLogging = temporaryDisableCalendarLogging2;
				}
			}
			if (this.MoveToMigratedMessagesList.Count > 0)
			{
				bool temporaryDisableCalendarLogging3 = this.MailboxData.MailboxSession.COWSettings.TemporaryDisableCalendarLogging;
				try
				{
					this.MailboxData.MailboxSession.COWSettings.TemporaryDisableCalendarLogging = true;
					this.PrepareAndExpireInBatches(this.MoveToMigratedMessagesList, ExpirationExecutor.Action.MoveToMigratedMessages);
					this.tracer.TraceDebug<ExpirationExecutor, int>((long)this.GetHashCode(), "{0}: {1} items are moved to migrated messages folder with no cal logging.", this, this.MoveToMigratedMessagesList.Count);
					ExpirationExecutor.TracerPfd.TracePfd<int, ExpirationExecutor, int>((long)this.GetHashCode(), "PFD IWE {0} {1}: {2} items are moved to migrated messages folder with no cal logging.", 24855, this, this.MoveToMigratedMessagesList.Count);
				}
				finally
				{
					this.MailboxData.MailboxSession.COWSettings.TemporaryDisableCalendarLogging = temporaryDisableCalendarLogging3;
				}
			}
			if (this.HardDeleteNoCalLoggingList.Count > 0)
			{
				bool temporaryDisableCalendarLogging4 = this.MailboxData.MailboxSession.COWSettings.TemporaryDisableCalendarLogging;
				try
				{
					this.MailboxData.MailboxSession.COWSettings.TemporaryDisableCalendarLogging = true;
					this.ExpireInBatches(this.HardDeleteNoCalLoggingList, ExpirationExecutor.Action.PermanentlyDelete);
					this.tracer.TraceDebug<ExpirationExecutor, int>((long)this.GetHashCode(), "{0}: {1} items hard deleted with calendar logging disabled.", this, this.HardDeleteNoCalLoggingList.Count);
					ExpirationExecutor.TracerPfd.TracePfd<int, ExpirationExecutor, int>((long)this.GetHashCode(), "PFD IWE {0} {1}: {2} items hard deleted with calendar logging disabled.", 24855, this, this.HardDeleteNoCalLoggingList.Count);
				}
				finally
				{
					this.MailboxData.MailboxSession.COWSettings.TemporaryDisableCalendarLogging = temporaryDisableCalendarLogging4;
				}
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000CF78 File Offset: 0x0000B178
		protected virtual void ExpireInBatches(List<ItemData> listToSend, ExpirationExecutor.Action retentionActionType, params StoreObjectId[] destinationFolderIds)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000CF7A File Offset: 0x0000B17A
		protected virtual void ExpireInBatches(List<ItemData> listToSend, ExpirationExecutor.Action retentionActionType)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000CF7C File Offset: 0x0000B17C
		protected virtual void PrepareAndExpireInBatches(List<ItemData> listToSend, ExpirationExecutor.Action retentionActionType)
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000CF7E File Offset: 0x0000B17E
		protected virtual void PrepareAndExpireInBatches(Dictionary<DefaultFolderType, List<ItemData>> listToSend, ExpirationExecutor.Action retentionActionType)
		{
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000CF80 File Offset: 0x0000B180
		protected int CopyIdsToTmpArray(ItemData[] sourceArray, int srcIndex, VersionedId[] destinationArray, int sizeToCopy)
		{
			int num = 0;
			for (int i = srcIndex; i < srcIndex + sizeToCopy; i++)
			{
				destinationArray[i - srcIndex] = sourceArray[i].Id;
				num += sourceArray[i].MessageSize;
			}
			return num;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000CFB7 File Offset: 0x0000B1B7
		protected virtual void CheckAndProcessItemsOnBatchSizeReached(List<ItemData> list)
		{
			if (list.Count >= 2000)
			{
				this.ExecuteTheDoomed();
				this.ClearProcessedItemList();
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		protected virtual void ClearProcessedItemList()
		{
			this.SoftDeleteList.Clear();
			this.SoftDeleteList.TrimExcess();
			this.HardDeleteList.Clear();
			this.HardDeleteList.TrimExcess();
			this.HardDeleteNoCalLoggingList.Clear();
			this.HardDeleteNoCalLoggingList.TrimExcess();
			this.MoveToArchiveList.Clear();
			this.MoveToArchiveList.TrimExcess();
			this.MoveToPurgesList.Clear();
			this.MoveToPurgesList.TrimExcess();
			this.MoveToPurgesNoCalLoggingList.Clear();
			this.MoveToPurgesNoCalLoggingList.TrimExcess();
			this.MoveToDiscoveryHoldsList.Clear();
			this.MoveToDiscoveryHoldsList.TrimExcess();
			this.MoveToDiscoveryHoldsNoCalLoggingList.Clear();
			this.MoveToDiscoveryHoldsNoCalLoggingList.TrimExcess();
			foreach (List<ItemData> list in this.MoveToArchiveDumpsterList.Values)
			{
				list.Clear();
				list.TrimExcess();
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000D0E4 File Offset: 0x0000B2E4
		private object GetPropertyObject(object objProperty)
		{
			if (objProperty != null)
			{
				return objProperty;
			}
			return Strings.ElcEmailNA;
		}

		// Token: 0x0400017B RID: 379
		protected const int SendBatchSize = 100;

		// Token: 0x0400017C RID: 380
		protected static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x0400017D RID: 381
		private Trace tracer;

		// Token: 0x0400017E RID: 382
		public bool isReportEnabled;

		// Token: 0x0400017F RID: 383
		private Dictionary<RetentionActionType, int> separationIndices;

		// Token: 0x04000180 RID: 384
		internal bool elcReportOverflow;

		// Token: 0x02000038 RID: 56
		internal enum Action
		{
			// Token: 0x0400018F RID: 399
			MoveToFolder,
			// Token: 0x04000190 RID: 400
			MoveToFolderAndSet,
			// Token: 0x04000191 RID: 401
			SoftDelete,
			// Token: 0x04000192 RID: 402
			PermanentlyDelete,
			// Token: 0x04000193 RID: 403
			MoveToArchive,
			// Token: 0x04000194 RID: 404
			MoveToArchiveDumpster,
			// Token: 0x04000195 RID: 405
			MoveToDiscoveryHolds,
			// Token: 0x04000196 RID: 406
			MoveToMigratedMessages,
			// Token: 0x04000197 RID: 407
			MoveToPurges
		}
	}
}
