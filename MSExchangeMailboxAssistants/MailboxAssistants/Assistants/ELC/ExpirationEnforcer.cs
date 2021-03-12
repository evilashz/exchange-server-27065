using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000062 RID: 98
	internal class ExpirationEnforcer : EnforcerBase
	{
		// Token: 0x06000379 RID: 889 RVA: 0x00017DF8 File Offset: 0x00015FF8
		internal ExpirationEnforcer(MailboxDataForFolders mailboxData, ElcFolderSubAssistant assistant) : base(mailboxData, assistant)
		{
			this.allPolicyTags = this.GetPolicyTags(mailboxData);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00017E10 File Offset: 0x00016010
		internal override bool IsEnabled(ProvisionedFolder provisionedFolder)
		{
			if (base.MailboxData.SuspendExpiration)
			{
				ExpirationEnforcer.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Expiration for this user is currently suspended. This user will be skipped.", new object[]
				{
					TraceContext.Get()
				});
				return false;
			}
			ExpirationEnforcer.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Expiration for this user is not suspended. This user will be processed.", new object[]
			{
				TraceContext.Get()
			});
			foreach (ElcPolicySettings elcPolicySettings in provisionedFolder.ElcPolicies)
			{
				if (elcPolicySettings.RetentionEnabled)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00017EC8 File Offset: 0x000160C8
		internal override void SetItemQueryFlags(ProvisionedFolder provisionedFolder, ItemFinder itemFinder)
		{
			int num = -1;
			bool flag = false;
			foreach (ElcPolicySettings elcPolicySettings in provisionedFolder.ElcPolicies)
			{
				if (elcPolicySettings.RetentionEnabled)
				{
					EnhancedTimeSpan? ageLimitForRetention = elcPolicySettings.AgeLimitForRetention;
					TimeSpan? timeSpan2;
					TimeSpan? timeSpan = timeSpan2 = ((ageLimitForRetention != null) ? new TimeSpan?(ageLimitForRetention.GetValueOrDefault()) : null);
					double totalDays;
					if (timeSpan2 != null && (totalDays = timeSpan.Value.TotalDays) > 0.0)
					{
						ExpirationEnforcer.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: Reading policy. MessageClass {1} in Policy '{2}' has expiration enabled.", TraceContext.Get(), elcPolicySettings.MessageClass, elcPolicySettings.Name);
						flag = true;
						if (!itemFinder.NeedMoveDate && (num == -1 || (double)num > totalDays))
						{
							num = (int)totalDays;
						}
						if (elcPolicySettings.RetentionAction == RetentionActionType.MarkAsPastRetentionLimit)
						{
							itemFinder.NeedExpiryTime = true;
							ExpirationEnforcer.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: Reading policy. MessageClass {1} in Policy '{2}' has expiration action of tag expiry time.", TraceContext.Get(), elcPolicySettings.MessageClass, elcPolicySettings.Name);
						}
						if (elcPolicySettings.TriggerForRetention == RetentionDateType.WhenMoved)
						{
							itemFinder.NeedMoveDate = true;
							num = 0;
							ExpirationEnforcer.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: Reading policy. MessageClass {1} in Policy '{2}' expires based on move date.", TraceContext.Get(), elcPolicySettings.MessageClass, elcPolicySettings.Name);
						}
					}
				}
			}
			itemFinder.SmallestAgeLimit = num;
			if (provisionedFolder.ContainerClass != null && ObjectClass.IsCalendarFolder(provisionedFolder.ContainerClass) && flag)
			{
				itemFinder.NeedCalendarProps = true;
			}
			if (provisionedFolder.ContainerClass != null && ObjectClass.IsTaskFolder(provisionedFolder.ContainerClass) && flag)
			{
				itemFinder.NeedTaskProps = true;
			}
			if (base.MailboxData.ElcAuditLog.ExpirationLoggingEnabled)
			{
				itemFinder.SetAuditLogFlags(base.MailboxData.ElcAuditLog.SubjectLoggingEnabled);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000180B4 File Offset: 0x000162B4
		internal override void Invoke(ProvisionedFolder provisionedFolder, List<object[]> items, PropertyIndexHolder propertyIndexHolder)
		{
			FolderExpirationExecutor folderExpirationExecutor = new FolderExpirationExecutor(provisionedFolder, base.MailboxData, base.Assistant);
			ExpirationEnforcer.Tracer.TraceDebug<object, string, int>((long)this.GetHashCode(), "{0}: Number of items found in folder '{1}' is {2}.", TraceContext.Get(), provisionedFolder.DisplayName, items.Count);
			DefaultFolderType folderType = DefaultFolderType.None;
			if (provisionedFolder.FolderId.Equals(base.MailboxData.MailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems)))
			{
				folderType = DefaultFolderType.DeletedItems;
			}
			else if (provisionedFolder.ContainerClass != null && ObjectClass.IsCalendarFolder(provisionedFolder.ContainerClass))
			{
				folderType = DefaultFolderType.Calendar;
			}
			else if (provisionedFolder.ContainerClass != null && ObjectClass.IsTaskFolder(provisionedFolder.ContainerClass))
			{
				folderType = DefaultFolderType.Tasks;
			}
			ItemStartDateCalculator itemStartDateCalculator = new ItemStartDateCalculator(propertyIndexHolder, provisionedFolder.Folder.DisplayName, folderType, base.MailboxData.MailboxSession, ExpirationEnforcer.Tracer);
			FolderAuditLogData folderAuditLogData = null;
			if (base.MailboxData.ElcAuditLog.ExpirationLoggingEnabled)
			{
				folderAuditLogData = new FolderAuditLogData(provisionedFolder, base.MailboxData, ELCAction.Retention.ToString());
			}
			foreach (object[] array in items)
			{
				VersionedId versionedId = array[propertyIndexHolder.IdIndex] as VersionedId;
				string text = array[propertyIndexHolder.ItemClassIndex] as string;
				if (versionedId == null)
				{
					ExpirationEnforcer.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Current item in folder {1} is null. Skipping it.", TraceContext.Get(), provisionedFolder.DisplayName);
				}
				else
				{
					provisionedFolder.CurrentItems = new VersionedId[]
					{
						versionedId
					};
					text = ((text == null) ? string.Empty : text.ToLower());
					ContentSetting contentSetting = null;
					contentSetting = ElcPolicySettings.GetApplyingPolicy(provisionedFolder.ElcPolicies, text, provisionedFolder.ItemClassToPolicyMapping);
					if (contentSetting == null)
					{
						ExpirationEnforcer.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: Policy for item class {1} in folder {2} is null. Skipping item.", TraceContext.Get(), text, provisionedFolder.DisplayName);
					}
					else if (!base.MailboxData.FolderProcessor.IsPolicyValid(provisionedFolder, contentSetting, text, base.MailboxData))
					{
						ExpirationEnforcer.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: Removing policy {1} from the list in folder {2} because it is invalid.", TraceContext.Get(), contentSetting.Name, provisionedFolder.DisplayName);
						provisionedFolder.RemovePolicy(contentSetting);
					}
					else
					{
						double totalDays = contentSetting.AgeLimitForRetention.Value.TotalDays;
						int num;
						if (contentSetting.TriggerForRetention == RetentionDateType.WhenMoved)
						{
							ExpirationEnforcer.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Applying policy. Policy '{1}' expires based on move date.", TraceContext.Get(), contentSetting.Name);
							CompositeProperty compositeProperty = null;
							if (ElcMailboxHelper.Exists(array[propertyIndexHolder.MoveDateIndex]))
							{
								try
								{
									compositeProperty = CompositeProperty.Parse((byte[])array[propertyIndexHolder.MoveDateIndex]);
								}
								catch (ArgumentException ex)
								{
									ExpirationEnforcer.Tracer.TraceError((long)this.GetHashCode(), "{0}: Could not parse move date property of item. Folder: {1} ItemClass: {2} Exception: {3}.", new object[]
									{
										TraceContext.Get(),
										provisionedFolder.DisplayName,
										text,
										ex
									});
									base.MailboxData.ThrowIfErrorsOverLimit();
								}
							}
							if (compositeProperty == null)
							{
								string arg = "Stamped Move date is null.";
								ExpirationEnforcer.Tracer.TraceDebug<object, VersionedId, string>((long)this.GetHashCode(), "{0}: Move date needs to be stamped on item {1}. {2}", TraceContext.Get(), versionedId, arg);
								folderExpirationExecutor.AddToMoveDateStampingList(new ItemData(versionedId, (int)array[propertyIndexHolder.SizeIndex]));
								continue;
							}
							ExpirationEnforcer.Tracer.TraceDebug<object, VersionedId>((long)this.GetHashCode(), "{0}: Calculating age of item {1} based on move date", TraceContext.Get(), versionedId);
							num = (int)base.MailboxData.Now.Subtract(compositeProperty.Date.Value).TotalDays;
						}
						else
						{
							DateTime startDate = itemStartDateCalculator.GetStartDate(versionedId, text, array);
							if (startDate == DateTime.MinValue)
							{
								num = 0;
							}
							else
							{
								num = (int)base.MailboxData.UtcNow.Subtract(startDate).TotalDays;
							}
						}
						try
						{
							if ((double)num >= totalDays)
							{
								if (contentSetting.RetentionAction == RetentionActionType.MarkAsPastRetentionLimit && array[propertyIndexHolder.ExpiryTimeIndex] != null && !(array[propertyIndexHolder.ExpiryTimeIndex] is PropertyError))
								{
									ExpirationEnforcer.Tracer.TraceDebug<object, VersionedId>((long)this.GetHashCode(), "{0}: Item {1} is already tagged with expiry time, hence will not tag again.", TraceContext.Get(), versionedId);
								}
								else
								{
									ExpirationEnforcer.Tracer.TraceDebug<object, VersionedId>((long)this.GetHashCode(), "{0}: Adding item {1} to list to be expired.", TraceContext.Get(), versionedId);
									ItemAuditLogData itemAuditLogData = null;
									if (base.MailboxData.ElcAuditLog.ExpirationLoggingEnabled)
									{
										folderAuditLogData.ExpirationAction = contentSetting.RetentionAction.ToString();
										itemAuditLogData = new ItemAuditLogData(array, propertyIndexHolder, folderAuditLogData);
									}
									ItemData itemData = new ItemData(versionedId, (array[propertyIndexHolder.ReceivedTimeIndex] is ExDateTime) ? ((DateTime)((ExDateTime)array[propertyIndexHolder.ReceivedTimeIndex])) : DateTime.MinValue, itemAuditLogData, (int)array[propertyIndexHolder.SizeIndex]);
									folderExpirationExecutor.AddToReportAndDoomedList(array, propertyIndexHolder, contentSetting, itemData, text, this.allPolicyTags);
								}
							}
						}
						catch (InvalidExpiryDestinationException ex2)
						{
							ExpirationEnforcer.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Removing policy '{1}', that applies to folder '{2}' from list of policies to process. Exception: {3}", new object[]
							{
								TraceContext.Get(),
								contentSetting.Name,
								contentSetting.ManagedFolderName,
								ex2
							});
							provisionedFolder.RemovePolicy(contentSetting);
						}
						catch (SkipFolderException ex3)
						{
							ExpirationEnforcer.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Policy '{1}', that applies to folder '{2}' will be skipped for the current folder. Exception: {3}", new object[]
							{
								TraceContext.Get(),
								contentSetting.Name,
								contentSetting.ManagedFolderName,
								ex3
							});
							return;
						}
					}
				}
			}
			ExpirationEnforcer.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Done identifying items for expiration. Proceed to expire.", new object[]
			{
				TraceContext.Get()
			});
			ExpirationEnforcer.TracerPfd.TracePfd<int, object>((long)this.GetHashCode(), "PFD IWE {0} {1}: Done identifying items for expiration. Calling ExpirationExecutor to expire.", 26903, TraceContext.Get());
			folderExpirationExecutor.ExecuteTheDoomed();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000186D0 File Offset: 0x000168D0
		private Dictionary<Guid, string> GetPolicyTags(MailboxDataForFolders mailboxData)
		{
			Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
			this.AddToPolicyTagCollection(mailboxData, dictionary, RetentionActionType.PermanentlyDelete);
			this.AddToPolicyTagCollection(mailboxData, dictionary, RetentionActionType.MoveToArchive);
			this.AddToPolicyTagCollection(mailboxData, dictionary, RetentionActionType.DeleteAndAllowRecovery);
			return dictionary;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00018700 File Offset: 0x00016900
		private void AddToPolicyTagCollection(MailboxDataForFolders mailboxData, Dictionary<Guid, string> allPolicyTagsRet, RetentionActionType retention)
		{
			Dictionary<Guid, PolicyTag> policyTagList = mailboxData.MailboxSession.GetPolicyTagList(retention);
			if (policyTagList != null)
			{
				foreach (KeyValuePair<Guid, PolicyTag> keyValuePair in policyTagList)
				{
					if (!allPolicyTagsRet.ContainsKey(keyValuePair.Key))
					{
						allPolicyTagsRet.Add(keyValuePair.Key, keyValuePair.Value.Name);
					}
				}
				policyTagList.Clear();
			}
		}

		// Token: 0x040002E5 RID: 741
		private static readonly Trace Tracer = ExTraceGlobals.ExpirationEnforcerTracer;

		// Token: 0x040002E6 RID: 742
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x040002E7 RID: 743
		private Dictionary<Guid, string> allPolicyTags;
	}
}
