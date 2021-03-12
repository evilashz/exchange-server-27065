using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.RecipientDLExpansion;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.RecipientDLExpansion
{
	// Token: 0x0200025D RID: 605
	internal class RecipientDLExpansionEventBasedAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x06001676 RID: 5750 RVA: 0x0007EC6C File Offset: 0x0007CE6C
		public RecipientDLExpansionEventBasedAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0007EC77 File Offset: 0x0007CE77
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			RecipientDLExpansionEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "MapiEvent is dispatched: {0}", mapiEvent);
			return this.InternalIsEventInteresting(mapiEvent);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0007EC98 File Offset: 0x0007CE98
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			if (this.InternalIsEventInteresting(mapiEvent) && itemStore != null && item != null)
			{
				MessageItem messageItem = item as MessageItem;
				if (messageItem == null)
				{
					RecipientDLExpansionEventBasedAssistant.Tracer.TraceWarning((long)this.GetHashCode(), "The item being processed is not a message item.");
					return;
				}
				RecipientDLExpansionEventBasedAssistant.Tracer.TraceDebug<Guid, StoreObjectId, string>((long)this.GetHashCode(), "Processing mailbox guid: {0} and message id: {1} with subject: '{2}'", itemStore.MailboxGuid, messageItem.StoreObjectId, messageItem.Subject);
				StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_RecipientDLExpansionIsProcessing, null, new object[]
				{
					messageItem.StoreObjectId,
					messageItem.Subject,
					itemStore.MailboxGuid
				});
				bool flag = false;
				bool flag2 = true;
				try
				{
					ADUser adUser = DirectoryHelper.ReadADRecipient(itemStore.MailboxOwner.MailboxInfo.MailboxGuid, itemStore.MailboxOwner.MailboxInfo.IsArchive, itemStore.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid)) as ADUser;
					bool flag3 = this.IsFlightingFeatureEnabled(adUser) || RecipientDLExpansionEventBasedAssistantHelper.IsRecipientDLExpansionTestHookEnabled();
					RecipientDLExpansionEventBasedAssistantHelper.GetComplianceMaxExpansionDGRecipientsAndNestedDGs(itemStore.OrganizationId, out this.maxDGExpansionRecipients, out this.maxExpansionNestedDGs);
					if (flag3 && this.maxDGExpansionRecipients != 0U)
					{
						flag2 = false;
						this.PerformDLExpansionOnItemRecipients(itemStore, messageItem, ref flag2);
					}
					else
					{
						RecipientDLExpansionEventBasedAssistant.Tracer.TraceDebug<StoreObjectId, Guid, OrganizationId>((long)this.GetHashCode(), "Per tenant configuration or flighting framework, DL expansion is disabled, skip processing message id: {0}, mailbox guid: {1}, tenant: {2}", messageItem.StoreObjectId, itemStore.MailboxGuid, itemStore.OrganizationId);
						StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_RecipientDLExpansionSkipped, null, new object[]
						{
							messageItem.StoreObjectId,
							itemStore.MailboxGuid,
							itemStore.OrganizationId
						});
					}
				}
				catch (Exception ex)
				{
					RecipientDLExpansionEventBasedAssistant.Tracer.TraceError((long)this.GetHashCode(), "Exception when updating the item with id: {0}, mailbox guid: {1}, tenant: {2}.\n\nThe exception is : {3}", new object[]
					{
						messageItem.StoreObjectId,
						itemStore.MailboxGuid,
						itemStore.OrganizationId,
						ex
					});
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_RecipientDLExpansionFailed, null, new object[]
					{
						messageItem.StoreObjectId,
						itemStore.MailboxGuid,
						itemStore.OrganizationId,
						ex
					});
					this.PublishMonitoringResults(itemStore, ex);
					flag = true;
					if (!(ex is StorageTransientException) && !(ex is StoragePermanentException))
					{
						throw;
					}
				}
				if (!flag && !flag2)
				{
					this.PublishMonitoringResults(itemStore, null);
				}
			}
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0007EEF8 File Offset: 0x0007D0F8
		private bool InternalIsEventInteresting(MapiEvent mapiEvent)
		{
			return (mapiEvent.ExtendedEventFlags & MapiExtendedEventFlags.NeedGroupExpansion) != MapiExtendedEventFlags.None && ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectCreated) != (MapiEventTypeFlags)0 || (mapiEvent.EventMask & MapiEventTypeFlags.ObjectMoved) != (MapiEventTypeFlags)0 || (mapiEvent.EventMask & MapiEventTypeFlags.ObjectCopied) != (MapiEventTypeFlags)0);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0007EF34 File Offset: 0x0007D134
		private void PerformDLExpansionOnItemRecipients(MailboxSession itemStore, MessageItem messageItem, ref bool isExpectedException)
		{
			Exception ex = null;
			try
			{
				GroupExpansionRecipients groupExpansionRecipients = null;
				if (this.MessageAlreadyHasRecipientsExpanded(messageItem, MessageItemSchema.GroupExpansionRecipients, out groupExpansionRecipients))
				{
					RecipientDLExpansionEventBasedAssistant.Tracer.TraceDebug<StoreObjectId, string>((long)this.GetHashCode(), "Message with id: {0} and subject: '{1}' already have group recipients expanded.", messageItem.StoreObjectId, messageItem.Subject);
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_RecipientDLExpansionMessageAlreadyProcessed, null, new object[]
					{
						messageItem.StoreObjectId,
						groupExpansionRecipients
					});
					return;
				}
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, itemStore.GetADSessionSettings(), 314, "PerformDLExpansionOnItemRecipients", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\Compliance\\RecipientDLExpansionEventBasedAssistant.cs");
				List<ADRecipient> list = new List<ADRecipient>();
				List<ADRecipient> list2 = new List<ADRecipient>();
				List<ADRecipient> list3 = new List<ADRecipient>();
				foreach (Recipient recipient in messageItem.Recipients)
				{
					bool? flag = recipient.IsDistributionList();
					if (flag != null && flag.Value)
					{
						if (recipient.RecipientItemType == RecipientItemType.To)
						{
							ADRecipient adrecipient = null;
							if (recipient.Participant.TryGetADRecipient(tenantOrRootOrgRecipientSession, out adrecipient) && adrecipient != null && !list.Contains(adrecipient))
							{
								list.Add(adrecipient);
							}
						}
						else if (recipient.RecipientItemType == RecipientItemType.Cc)
						{
							ADRecipient adrecipient2 = null;
							if (recipient.Participant.TryGetADRecipient(tenantOrRootOrgRecipientSession, out adrecipient2) && adrecipient2 != null && !list2.Contains(adrecipient2))
							{
								list2.Add(adrecipient2);
							}
						}
						else if (recipient.RecipientItemType == RecipientItemType.Bcc)
						{
							ADRecipient adrecipient3 = null;
							if (recipient.Participant.TryGetADRecipient(tenantOrRootOrgRecipientSession, out adrecipient3) && adrecipient3 != null && !list3.Contains(adrecipient3))
							{
								list3.Add(adrecipient3);
							}
						}
					}
				}
				if (list.Count > 0 || list2.Count > 0 || list3.Count > 0)
				{
					RecipientDLExpansionPerfmon.TotalDLExpansionMessages.Increment();
					RecipientDLExpansionPerfmon.TotalRecipientDLsInMessage.IncrementBy((long)(list.Count + list2.Count + list3.Count));
					using (AverageTimeCounter averageTimeCounter = new AverageTimeCounter(RecipientDLExpansionPerfmon.AverageMessageDLExpansionProcessing, RecipientDLExpansionPerfmon.AverageMessageDLExpansionProcessingBase, true))
					{
						try
						{
							GroupExpansionRecipients groupExpansionRecipients2 = new GroupExpansionRecipients();
							DistributionGroupExpansionError distributionGroupExpansionError = DistributionGroupExpansionError.NoError;
							distributionGroupExpansionError |= this.ExpandGroupMemberRecipients(messageItem, list, RecipientItemType.To, groupExpansionRecipients2);
							distributionGroupExpansionError |= this.ExpandGroupMemberRecipients(messageItem, list2, RecipientItemType.Cc, groupExpansionRecipients2);
							distributionGroupExpansionError |= this.ExpandGroupMemberRecipients(messageItem, list3, RecipientItemType.Bcc, groupExpansionRecipients2);
							int num = 0;
							while (num++ <= 1)
							{
								try
								{
									groupExpansionRecipients2.SaveToStore(messageItem, MessageItemSchema.GroupExpansionRecipients);
									if (distributionGroupExpansionError != DistributionGroupExpansionError.NoError)
									{
										messageItem[MessageItemSchema.GroupExpansionError] = distributionGroupExpansionError;
									}
									SaveMode saveMode = SaveMode.NoConflictResolution;
									messageItem.Save(saveMode);
									break;
								}
								catch (TransientException ex2)
								{
									RecipientDLExpansionEventBasedAssistant.Tracer.TraceError<TransientException>((long)this.GetHashCode(), "Got transient exception when trying to update the message: \r\n{0}", ex2);
									if (num > 1)
									{
										RecipientDLExpansionEventBasedAssistant.Tracer.TraceDebug<int>((long)this.GetHashCode(), "It still failed after retry for {0} times, so give up.", 1);
										throw;
									}
									if (ex2 is SaveConflictException)
									{
										RecipientDLExpansionEventBasedAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "Got SaveConflictException, will reload the item and retry immediately.");
										StoreId id = messageItem.Id;
										MessageItem messageItem2 = Item.BindAsMessage(itemStore, id, RecipientDLExpansionEventBasedAssistant.ItemProperties);
										messageItem.Dispose();
										messageItem = messageItem2;
									}
									else
									{
										RecipientDLExpansionEventBasedAssistant.Tracer.TraceDebug<int>((long)this.GetHashCode(), "Wait for {0} milliseconds before retry again.", 30000);
										Thread.Sleep(30000);
									}
								}
							}
						}
						finally
						{
							averageTimeCounter.Stop();
						}
						goto IL_382;
					}
				}
				RecipientDLExpansionEventBasedAssistant.Tracer.TraceWarning<StoreObjectId, string>((long)this.GetHashCode(), "Message with id: {0} and subject: '{1}' does not have any DG recipients.", messageItem.StoreObjectId, messageItem.Subject);
				StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_RecipientDLExpansionMessageNoDLRecipients, null, new object[]
				{
					messageItem.StoreObjectId
				});
				IL_382:;
			}
			catch (AccessDeniedException ex3)
			{
				ex = ex3;
				isExpectedException = true;
			}
			catch (ObjectNotFoundException ex4)
			{
				ex = ex4;
				isExpectedException = true;
			}
			catch (RecoverableItemsAccessDeniedException ex5)
			{
				isExpectedException = true;
				RecipientDLExpansionEventBasedAssistant.Tracer.TraceWarning((long)this.GetHashCode(), "Can't save DL expansion list to message with id: {0} and subject: '{1}' in mailbox: {2}, tenant: {3} because update item in Dumpster is not allowed. Exception: \r\n{4}", new object[]
				{
					messageItem.StoreObjectId,
					messageItem.Subject,
					itemStore.MailboxGuid,
					itemStore.OrganizationId,
					ex5
				});
				StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_RecipientDLExpansionUpdateItemInDumpster, null, new object[]
				{
					messageItem.StoreObjectId,
					messageItem.Subject,
					itemStore.MailboxGuid,
					itemStore.OrganizationId,
					ex5
				});
			}
			if (ex != null)
			{
				RecipientDLExpansionEventBasedAssistant.Tracer.TraceWarning((long)this.GetHashCode(), "Can't process message with id: {0} and subject: '{1}' in mailbox: {2}, tenant: {3} because it may no longer exist. Exception: \r\n{4}", new object[]
				{
					messageItem.StoreObjectId,
					messageItem.Subject,
					itemStore.MailboxGuid,
					itemStore.OrganizationId,
					ex
				});
				StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_RecipientDLExpansionMessageNoLongerExist, null, new object[]
				{
					messageItem.StoreObjectId,
					itemStore.MailboxGuid,
					itemStore.OrganizationId,
					ex
				});
			}
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0007F4D4 File Offset: 0x0007D6D4
		private void PublishMonitoringResults(MailboxSession itemStore, Exception ex)
		{
			ResultSeverityLevel severity = (ex != null) ? ResultSeverityLevel.Error : ResultSeverityLevel.Informational;
			new EventNotificationItem(ExchangeComponent.DLExpansion.Name, "DLExpansionComponent", string.Empty, severity)
			{
				StateAttribute1 = itemStore.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
				StateAttribute2 = itemStore.MailboxOwner.MailboxInfo.OrganizationId.ToString(),
				StateAttribute3 = itemStore.MailboxOwner.MailboxInfo.Location.ServerLegacyDn,
				StateAttribute4 = itemStore.MailboxOwner.MailboxInfo.GetDatabaseGuid().ToString(),
				StateAttribute5 = ((ex != null) ? ex.ToString() : "No Exception")
			}.Publish(false);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0007F59F File Offset: 0x0007D79F
		private bool MessageAlreadyHasRecipientsExpanded(MessageItem messageItem, StorePropertyDefinition property, out GroupExpansionRecipients ger)
		{
			ger = GroupExpansionRecipients.RetrieveFromStore(messageItem, property);
			return ger != null && ger.Recipients.Count > 0;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0007F5C0 File Offset: 0x0007D7C0
		private DistributionGroupExpansionError ExpandGroupMemberRecipients(MessageItem messageItem, List<ADRecipient> groupRecipients, RecipientItemType recipientType, GroupExpansionRecipients groupExpansionRecipients)
		{
			DistributionGroupExpansionError result = DistributionGroupExpansionError.NoError;
			if (groupRecipients.Count > 0)
			{
				List<RecipientToIndex> collection = this.ExpandGroupMemberRecipients(messageItem, groupRecipients, recipientType, out result);
				groupExpansionRecipients.Recipients.AddRange(collection);
			}
			return result;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0007F8A0 File Offset: 0x0007DAA0
		private List<RecipientToIndex> ExpandGroupMemberRecipients(MessageItem messageItem, List<ADRecipient> groupRecipients, RecipientItemType recipientType, out DistributionGroupExpansionError errorCode)
		{
			errorCode = DistributionGroupExpansionError.NoError;
			DistributionGroupExpansionError tempErrorCode = errorCode;
			List<RecipientToIndex> finalExpansionList = new List<RecipientToIndex>();
			List<ADRecipient> list = new List<ADRecipient>(groupRecipients.Count);
			foreach (ADRecipient adrecipient in groupRecipients)
			{
				OrganizationId organizationId = messageItem.Session.MailboxOwner.MailboxInfo.OrganizationId;
				string uniqueLookupKey = RecipientDLExpansionCache.GetUniqueLookupKey(organizationId, adrecipient);
				RecipientDLExpansionEventBasedAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Get recipients list from the cache for lookup key: {0}", uniqueLookupKey);
				List<RecipientToIndex> perRecipientExpansionList = RecipientDLExpansionCache.Instance.Get(uniqueLookupKey);
				if (perRecipientExpansionList != null)
				{
					RecipientDLExpansionEventBasedAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "The recipients list exists in the cache for lookup key: {0}", uniqueLookupKey);
					if (!this.AddPerRecipientListToFinalExpansionList(messageItem, perRecipientExpansionList, finalExpansionList, recipientType, out tempErrorCode))
					{
						break;
					}
					list.Add(adrecipient);
				}
				else
				{
					RecipientDLExpansionEventBasedAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "Message with id: {0} and subject: '{1}' has recipient '{2}' which is not in the cache (with lookup key: {3}), hence need to make AD call.", new object[]
					{
						messageItem.StoreObjectId,
						messageItem.Subject,
						adrecipient.DisplayName,
						uniqueLookupKey
					});
					perRecipientExpansionList = new List<RecipientToIndex>();
					int numOfGroups = 0;
					ADRecipientExpansion adrecipientExpansion = new ADRecipientExpansion(RecipientDLExpansionEventBasedAssistant.RecipientProperties, organizationId);
					adrecipientExpansion.Expand(adrecipient, delegate(ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentType)
					{
						if (recipientExpansionType == ExpansionType.GroupMembership)
						{
							if ((long)numOfGroups++ >= (long)((ulong)this.maxExpansionNestedDGs))
							{
								if (recipientType == RecipientItemType.Cc)
								{
									tempErrorCode = DistributionGroupExpansionError.CcGroupExpansionHitDepthsLimit;
								}
								else if (recipientType == RecipientItemType.Bcc)
								{
									tempErrorCode = DistributionGroupExpansionError.BccGroupExpansionHitDepthsLimit;
								}
								else
								{
									tempErrorCode = DistributionGroupExpansionError.ToGroupExpansionHitDepthsLimit;
								}
								RecipientDLExpansionEventBasedAssistant.Tracer.TraceWarning((long)this.GetHashCode(), "DL expansion on message with id: {0}, mailbox guid: {1}, tenant: {2} is terminated because number of nested DLs ({3}) is greater than the limit ({4})", new object[]
								{
									messageItem.StoreObjectId,
									messageItem.Session.MailboxGuid,
									organizationId,
									numOfGroups,
									this.maxExpansionNestedDGs
								});
								StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_RecipientDLExpansionMaxNestedDLsLimit, null, new object[]
								{
									this.maxExpansionNestedDGs,
									messageItem.StoreObjectId,
									messageItem.Session.MailboxGuid,
									organizationId
								});
								DistributionGroupExpansionError distributionGroupExpansionError = DistributionGroupExpansionError.NoError;
								this.AddRecipientToExpansionList(messageItem, recipient, perRecipientExpansionList, finalExpansionList, recipientType, out distributionGroupExpansionError);
								if (distributionGroupExpansionError != DistributionGroupExpansionError.NoError)
								{
									tempErrorCode |= distributionGroupExpansionError;
								}
								return ExpansionControl.Terminate;
							}
							RecipientDLExpansionPerfmon.TotalExpandedNestedDLs.Increment();
							if (this.AddRecipientToExpansionList(messageItem, recipient, perRecipientExpansionList, finalExpansionList, recipientType, out tempErrorCode))
							{
								return ExpansionControl.Continue;
							}
							return ExpansionControl.Terminate;
						}
						else
						{
							if (!this.AddRecipientToExpansionList(messageItem, recipient, perRecipientExpansionList, finalExpansionList, recipientType, out tempErrorCode))
							{
								return ExpansionControl.Terminate;
							}
							return ExpansionControl.Skip;
						}
					}, delegate(ExpansionFailure expansionFailure, ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType)
					{
						if (recipientType == RecipientItemType.Cc)
						{
							tempErrorCode = DistributionGroupExpansionError.CcGroupExpansionFailed;
						}
						else if (recipientType == RecipientItemType.Bcc)
						{
							tempErrorCode = DistributionGroupExpansionError.BccGroupExpansionFailed;
						}
						else
						{
							tempErrorCode = DistributionGroupExpansionError.ToGroupExpansionFailed;
						}
						return ExpansionControl.Skip;
					});
					errorCode = tempErrorCode;
					if (tempErrorCode != DistributionGroupExpansionError.NoError)
					{
						RecipientDLExpansionEventBasedAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Failed or partially success when performing the DG expansion: {0}", tempErrorCode.ToString());
						break;
					}
					RecipientDLExpansionEventBasedAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Add the recipients list to the cache for key: {0}", uniqueLookupKey);
					RecipientDLExpansionCache.Instance.Add(uniqueLookupKey, perRecipientExpansionList);
					list.Add(adrecipient);
				}
			}
			if (list.Count != groupRecipients.Count)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("There are total of {0} group recipients, which only {1} of them successfully expanded.\r\n", groupRecipients.Count, list.Count);
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder.AppendLine("List of successful expanded group recipients:");
				foreach (ADRecipient adrecipient2 in groupRecipients)
				{
					if (list.Contains(adrecipient2))
					{
						stringBuilder.AppendLine(adrecipient2.DisplayName);
					}
					else
					{
						stringBuilder2.AppendLine(adrecipient2.DisplayName);
					}
				}
				stringBuilder.AppendLine("List of not successful expanded group recipients:");
				stringBuilder.AppendLine(stringBuilder2.ToString());
				RecipientDLExpansionEventBasedAssistant.Tracer.TraceWarning((long)this.GetHashCode(), stringBuilder.ToString());
				if (!this.IsDLExpansionLimitError(errorCode))
				{
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_RecipientDLExpansionMismatchResults, null, new object[]
					{
						messageItem.StoreObjectId,
						messageItem.Session.MailboxGuid,
						messageItem.Session.MailboxOwner.MailboxInfo.OrganizationId,
						stringBuilder.ToString()
					});
				}
			}
			return finalExpansionList;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0007FC88 File Offset: 0x0007DE88
		private bool AddRecipientToExpansionList(MessageItem messageItem, ADRawEntry recipient, List<RecipientToIndex> perRecipientExpansionList, List<RecipientToIndex> finalExpansionList, RecipientItemType recipientType, out DistributionGroupExpansionError errorCode)
		{
			errorCode = DistributionGroupExpansionError.NoError;
			SmtpAddress smtpAddress = (SmtpAddress)recipient[ADRecipientSchema.PrimarySmtpAddress];
			string displayName = recipient[ADRecipientSchema.DisplayName] as string;
			RecipientToIndex item = new RecipientToIndex(recipientType, displayName, smtpAddress.ToString());
			if (!perRecipientExpansionList.Contains(item))
			{
				perRecipientExpansionList.Add(item);
			}
			if (finalExpansionList.Contains(item))
			{
				return true;
			}
			if ((long)finalExpansionList.Count < (long)((ulong)this.maxDGExpansionRecipients))
			{
				finalExpansionList.Add(item);
				return true;
			}
			if (recipientType == RecipientItemType.Cc)
			{
				errorCode = DistributionGroupExpansionError.CcGroupExpansionHitRecipientsLimit;
			}
			else if (recipientType == RecipientItemType.Bcc)
			{
				errorCode = DistributionGroupExpansionError.BccGroupExpansionHitRecipientsLimit;
			}
			else
			{
				errorCode = DistributionGroupExpansionError.ToGroupExpansionHitRecipientsLimit;
			}
			RecipientDLExpansionEventBasedAssistant.Tracer.TraceWarning((long)this.GetHashCode(), "DL expansion on message with id: {0}, mailbox guid: {1}, tenant: {2} is terminated because number of total member recipients ({3}) is greater than the limit ({4})", new object[]
			{
				messageItem.StoreObjectId,
				messageItem.Session.MailboxGuid,
				messageItem.Session.MailboxOwner.MailboxInfo.OrganizationId,
				finalExpansionList.Count,
				this.maxDGExpansionRecipients
			});
			StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_RecipientDLExpansionMaxRecipientsLimit, null, new object[]
			{
				this.maxDGExpansionRecipients,
				messageItem.StoreObjectId,
				messageItem.Session.MailboxGuid,
				messageItem.Session.MailboxOwner.MailboxInfo.OrganizationId
			});
			return false;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0007FDF8 File Offset: 0x0007DFF8
		private bool AddPerRecipientListToFinalExpansionList(MessageItem messageItem, List<RecipientToIndex> perRecipientExpansionList, List<RecipientToIndex> finalExpansionList, RecipientItemType recipientType, out DistributionGroupExpansionError errorCode)
		{
			errorCode = DistributionGroupExpansionError.NoError;
			foreach (RecipientToIndex recipientToIndex in perRecipientExpansionList)
			{
				if (!finalExpansionList.Contains(recipientToIndex))
				{
					if ((long)finalExpansionList.Count >= (long)((ulong)this.maxDGExpansionRecipients))
					{
						if (recipientType == RecipientItemType.Cc)
						{
							errorCode = DistributionGroupExpansionError.CcGroupExpansionHitRecipientsLimit;
						}
						else if (recipientType == RecipientItemType.Bcc)
						{
							errorCode = DistributionGroupExpansionError.BccGroupExpansionHitRecipientsLimit;
						}
						else
						{
							errorCode = DistributionGroupExpansionError.ToGroupExpansionHitRecipientsLimit;
						}
						RecipientDLExpansionEventBasedAssistant.Tracer.TraceWarning((long)this.GetHashCode(), "DL expansion on message with id: {0}, mailbox guid: {1}, tenant: {2}, is terminated because number of total member recipients ({3}) is greater than the limit ({4})", new object[]
						{
							messageItem.StoreObjectId,
							messageItem.Session.MailboxGuid,
							messageItem.Session.MailboxOwner.MailboxInfo.OrganizationId,
							finalExpansionList.Count,
							this.maxDGExpansionRecipients
						});
						StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_RecipientDLExpansionMaxRecipientsLimit, null, new object[]
						{
							this.maxDGExpansionRecipients,
							messageItem.StoreObjectId,
							messageItem.Session.MailboxGuid,
							messageItem.Session.MailboxOwner.MailboxInfo.OrganizationId
						});
						return false;
					}
					if (recipientToIndex.RecipientType != recipientType)
					{
						finalExpansionList.Add(new RecipientToIndex(recipientType, recipientToIndex.DisplayName, recipientToIndex.EmailAddress));
					}
					else
					{
						finalExpansionList.Add(recipientToIndex);
					}
				}
			}
			return true;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0007FF98 File Offset: 0x0007E198
		private bool IsFlightingFeatureEnabled(ADUser adUser)
		{
			if (adUser != null)
			{
				VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(adUser.GetContext(null), null, null);
				return snapshot.MailboxAssistants.PerformRecipientDLExpansion.Enabled;
			}
			RecipientDLExpansionEventBasedAssistant.Tracer.TraceWarning((long)this.GetHashCode(), "Feature is disabled because AD user is null, hence cannot get per user variant snapshot");
			return false;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0007FFE2 File Offset: 0x0007E1E2
		private bool IsDLExpansionLimitError(DistributionGroupExpansionError expansionError)
		{
			return (expansionError & DistributionGroupExpansionError.ToGroupExpansionHitDepthsLimit) == DistributionGroupExpansionError.ToGroupExpansionHitDepthsLimit || (expansionError & DistributionGroupExpansionError.ToGroupExpansionHitRecipientsLimit) == DistributionGroupExpansionError.ToGroupExpansionHitRecipientsLimit || (expansionError & DistributionGroupExpansionError.CcGroupExpansionHitDepthsLimit) == DistributionGroupExpansionError.CcGroupExpansionHitDepthsLimit || (expansionError & DistributionGroupExpansionError.CcGroupExpansionHitRecipientsLimit) == DistributionGroupExpansionError.CcGroupExpansionHitRecipientsLimit || (expansionError & DistributionGroupExpansionError.BccGroupExpansionHitDepthsLimit) == DistributionGroupExpansionError.BccGroupExpansionHitDepthsLimit || (expansionError & DistributionGroupExpansionError.BccGroupExpansionHitRecipientsLimit) == DistributionGroupExpansionError.BccGroupExpansionHitRecipientsLimit;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00080087 File Offset: 0x0007E287
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0008008F File Offset: 0x0007E28F
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00080097 File Offset: 0x0007E297
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000D32 RID: 3378
		private const int MaxRetries = 1;

		// Token: 0x04000D33 RID: 3379
		private const int RetryWaitInterval = 30000;

		// Token: 0x04000D34 RID: 3380
		private uint maxDGExpansionRecipients;

		// Token: 0x04000D35 RID: 3381
		private uint maxExpansionNestedDGs;

		// Token: 0x04000D36 RID: 3382
		private static readonly PropertyDefinition[] ItemProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			MessageItemSchema.GroupExpansionRecipients,
			MessageItemSchema.GroupExpansionError
		};

		// Token: 0x04000D37 RID: 3383
		private static readonly PropertyDefinition[] RecipientProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.DisplayName
		};

		// Token: 0x04000D38 RID: 3384
		private static readonly Trace Tracer = ExTraceGlobals.RecipientDLExpansionEventBasedAssistantTracer;

		// Token: 0x04000D39 RID: 3385
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
