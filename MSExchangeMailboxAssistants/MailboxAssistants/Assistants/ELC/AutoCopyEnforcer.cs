using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000056 RID: 86
	internal sealed class AutoCopyEnforcer : EnforcerBase
	{
		// Token: 0x060002FE RID: 766 RVA: 0x00012B9A File Offset: 0x00010D9A
		internal AutoCopyEnforcer(MailboxDataForFolders mailboxData, DatabaseInfo databaseInfo, ElcFolderSubAssistant assistant) : base(mailboxData, assistant)
		{
			this.databaseInfo = databaseInfo;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00012BB8 File Offset: 0x00010DB8
		private Participant NdrParticipant
		{
			get
			{
				if (this.ndrParticipant == null)
				{
					AutoCopyEnforcer.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Need to get the Journal Ndr Report address from the AD.", new object[]
					{
						TraceContext.Get()
					});
					ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.MailboxData.MailboxSession.MailboxOwner.MailboxInfo.OrganizationId);
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 127, "NdrParticipant", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\elc\\ManagedFolderAssistant\\AutoCopyEnforcer.cs");
					string distinguishedName = string.Empty;
					if (base.MailboxData.ElcUserInformation.ADUser.OrganizationId != null && base.MailboxData.ElcUserInformation.ADUser.OrganizationId.ConfigurationUnit != null)
					{
						distinguishedName = "CN=Transport Settings," + base.MailboxData.ElcUserInformation.ADUser.OrganizationId.ConfigurationUnit.DistinguishedName;
					}
					else
					{
						distinguishedName = "CN=Transport Settings," + tenantOrTopologyConfigurationSession.GetOrgContainerId().DistinguishedName;
					}
					ADObjectId rootId = new ADObjectId(distinguishedName);
					TransportConfigContainer[] array = tenantOrTopologyConfigurationSession.Find<TransportConfigContainer>(rootId, QueryScope.Base, null, null, 1);
					if (array == null || array.Length != 1)
					{
						AutoCopyEnforcer.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Unable to find the transport config container. No Ndr recipient.", new object[]
						{
							TraceContext.Get()
						});
						return null;
					}
					SmtpAddress journalingReportNdrTo = array[0].JournalingReportNdrTo;
					string text = array[0].JournalingReportNdrTo.ToString();
					if (!string.IsNullOrEmpty(text))
					{
						AutoCopyEnforcer.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: JournalReportNdrTo found. Address is {1}.", TraceContext.Get(), text);
						this.ndrParticipant = new Participant(text, text, "SMTP");
					}
				}
				return this.ndrParticipant;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00012D68 File Offset: 0x00010F68
		private VersionedId SendFolderId
		{
			get
			{
				if (this.sendFolderId == null)
				{
					StoreObjectId defaultFolderId = base.MailboxData.MailboxSession.GetDefaultFolderId(DefaultFolderType.Configuration);
					using (Folder folder = Folder.Create(base.MailboxData.MailboxSession, defaultFolderId, StoreObjectType.Folder, "Temp archive folder", CreateMode.OpenIfExists))
					{
						FolderSaveResult folderSaveResult = folder.Save();
						if (folderSaveResult.OperationResult != OperationResult.Succeeded)
						{
							AutoCopyEnforcer.Tracer.TraceError((long)this.GetHashCode(), "{0}: AutoCopyEnforcer: Failed to temporary archive submission folder in the config folder.", new object[]
							{
								TraceContext.Get()
							});
							throw new TransientMailboxException(Strings.descFailedToCreateTempFolder(base.MailboxData.MailboxSession.MailboxOwner.MailboxInfo.DisplayName, this.databaseInfo.DisplayName));
						}
						folder.Load();
						this.sendFolderId = folder.Id;
					}
				}
				return this.sendFolderId;
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00012E48 File Offset: 0x00011048
		internal override bool IsEnabled(ProvisionedFolder provisionedFolder)
		{
			bool flag = false;
			foreach (ElcPolicySettings elcPolicySettings in provisionedFolder.ElcPolicies)
			{
				if (elcPolicySettings.JournalingEnabled)
				{
					AutoCopyEnforcer.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: search for items in folder '{1}' will include autocopy properties because policy '{2}' has autocopy enabled and applies to this folder.", TraceContext.Get(), provisionedFolder.DisplayName, elcPolicySettings.Name);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				AutoCopyEnforcer.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: search for items in folder '{1}' will not include autocopy properties because no policy with autocopy applies to this folder.", TraceContext.Get(), provisionedFolder.DisplayName);
			}
			return flag;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00012EF0 File Offset: 0x000110F0
		internal override void SetItemQueryFlags(ProvisionedFolder provisionedFolder, ItemFinder itemFinder)
		{
			itemFinder.NeedAutoCopyProps = true;
			if (base.MailboxData.ElcAuditLog.AutocopyLoggingEnabled)
			{
				itemFinder.SetAuditLogFlags(base.MailboxData.ElcAuditLog.SubjectLoggingEnabled);
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00012F24 File Offset: 0x00011124
		internal override void Invoke(ProvisionedFolder provisionedFolder, List<object[]> items, PropertyIndexHolder propertyIndexHolder)
		{
			AutoCopyEnforcer.TracerPfd.TracePfd<int, Folder>((long)this.GetHashCode(), "PFD IWE {0} {1} AutoCopyEnforcer Invoked", 18711, provisionedFolder.Folder);
			using (Folder folder = Folder.Bind(base.MailboxData.MailboxSession, this.SendFolderId))
			{
				foreach (object[] array in items)
				{
					if (array[propertyIndexHolder.IdIndex] is VersionedId)
					{
						VersionedId versionedId = (VersionedId)array[propertyIndexHolder.IdIndex];
						provisionedFolder.CurrentItems = new VersionedId[]
						{
							versionedId
						};
						object obj = array[propertyIndexHolder.AutoCopiedIndex];
						if (obj is byte[])
						{
							AutoCopyEnforcer.Tracer.TraceDebug<object, VersionedId>((long)this.GetHashCode(), "{0}: ignoring item '{1}' because it has been copied already.", TraceContext.Get(), versionedId);
						}
						else
						{
							string text = array[propertyIndexHolder.ItemClassIndex] as string;
							text = ((text == null) ? string.Empty : text.ToLower());
							ContentSetting applyingPolicy = ElcPolicySettings.GetApplyingPolicy(provisionedFolder.ElcPolicies, text, provisionedFolder.ItemClassToPolicyMapping);
							if (applyingPolicy != null && applyingPolicy.JournalingEnabled)
							{
								ItemAuditLogData itemAuditLogData = null;
								if (base.MailboxData.ElcAuditLog.AutocopyLoggingEnabled)
								{
									itemAuditLogData = new ItemAuditLogData(array, propertyIndexHolder, new FolderAuditLogData(provisionedFolder, base.MailboxData, ELCAction.Journaling.ToString(), this.GetOfficialFileParticipant(applyingPolicy).EmailAddress));
								}
								ItemData itemData = new ItemData(versionedId, (array[propertyIndexHolder.ReceivedTimeIndex] is ExDateTime) ? ((DateTime)((ExDateTime)array[propertyIndexHolder.ReceivedTimeIndex])) : DateTime.MinValue, itemAuditLogData, (int)array[propertyIndexHolder.SizeIndex]);
								StoreObjectId storeObjectId;
								bool flag = this.SendItemAsAttachement(applyingPolicy, itemData, out storeObjectId);
								if (flag)
								{
									this.MarkItemAsCopied(itemData, provisionedFolder);
									ELCPerfmon.TotalItemsAutoCopied.Increment();
								}
								else
								{
									Exception ex = null;
									OperationResult operationResult = OperationResult.Succeeded;
									try
									{
										operationResult = folder.DeleteObjects(DeleteItemFlags.HardDelete, new StoreObjectId[]
										{
											storeObjectId
										}).OperationResult;
									}
									catch (Exception ex2)
									{
										operationResult = OperationResult.Failed;
										ex = ex2;
									}
									if (operationResult != OperationResult.Succeeded)
									{
										AutoCopyEnforcer.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: Failed to delete the journal message '{1}'. Exception: '{2}'.", TraceContext.Get(), storeObjectId.ToString(), (ex == null) ? "" : ex.Message);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000131B8 File Offset: 0x000113B8
		private bool SendItemAsAttachement(ContentSetting policy, ItemData itemData, out StoreObjectId messageId)
		{
			Exception ex = null;
			messageId = null;
			using (MessageItem messageItem = MessageItem.Create(base.MailboxData.MailboxSession, this.sendFolderId))
			{
				Participant officialFileParticipant = this.GetOfficialFileParticipant(policy);
				string text = base.MailboxData.MailboxSmtpAddress + ":";
				if (policy.AddressForJournaling != null)
				{
					messageItem[MessageItemSchema.ElcAutoCopyLabel] = text + policy.LabelForJournaling;
				}
				else
				{
					messageItem[MessageItemSchema.ElcAutoCopyLabel] = text;
				}
				messageItem.ClassName = this.GetMessageClass(policy.MessageFormatForJournaling);
				messageItem.Subject = "Autocopy report";
				Participant v = this.NdrParticipant;
				if (v != null)
				{
					messageItem.ReplyTo.Add(this.NdrParticipant);
				}
				messageItem.Recipients.Add(officialFileParticipant, RecipientItemType.To);
				try
				{
					using (Item item = Item.Bind(base.MailboxData.MailboxSession, itemData.Id, ItemBindOption.LoadRequiredPropertiesOnly))
					{
						using (ItemAttachment itemAttachment = messageItem.AttachmentCollection.AddExistingItem(item))
						{
							itemAttachment.Save();
						}
					}
					messageItem.SendWithoutSavingMessage();
					AutoCopyEnforcer.Tracer.TraceDebug<object, string, VersionedId>((long)this.GetHashCode(), "{0}: sent journaling message to '{1}' for item '{2}'.", TraceContext.Get(), officialFileParticipant.EmailAddress, itemData.Id);
					AutoCopyEnforcer.TracerPfd.TracePfd((long)this.GetHashCode(), "PFD IWE {0} {1}: sent journaling message to '{2}' for item '{3}'.", new object[]
					{
						29975,
						TraceContext.Get(),
						officialFileParticipant.EmailAddress,
						itemData.Id
					});
				}
				catch (ObjectNotFoundException ex2)
				{
					ex = ex2;
				}
				catch (VirusMessageDeletedException ex3)
				{
					ex = ex3;
				}
				catch (VirusScanInProgressException ex4)
				{
					ex = ex4;
				}
				catch (CorruptDataException ex5)
				{
					ex = ex5;
				}
				catch (VirusDetectedException ex6)
				{
					ex = ex6;
				}
				catch (MessageSubmissionExceededException ex7)
				{
					ex = ex7;
				}
				if (ex != null)
				{
					messageId = messageItem.StoreObjectId;
					AutoCopyEnforcer.Tracer.TraceError<AutoCopyEnforcer, VersionedId, Exception>((long)this.GetHashCode(), "{0}: Unable to process the message '{1}' due to exception '{2}'. Skipping this item.", this, itemData.Id, ex);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00013484 File Offset: 0x00011684
		private void MarkItemAsCopied(ItemData itemData, ProvisionedFolder provisionedFolder)
		{
			ItemUpdater itemUpdater = new ItemUpdater(base.MailboxData, provisionedFolder, base.Assistant);
			itemUpdater.SetProperty(itemData, ItemSchema.ElcAutoCopyTag, itemData.Id.ChangeKeyAsByteArray());
			AutoCopyEnforcer.Tracer.TraceDebug<object, VersionedId>((long)this.GetHashCode(), "{0}: item '{1}' marked as copied.", TraceContext.Get(), itemData.Id);
			AutoCopyEnforcer.TracerPfd.TracePfd<int, object, VersionedId>((long)this.GetHashCode(), "PFD IWE {0} {1}: item '{2}' marked as copied.", 21783, TraceContext.Get(), itemData.Id);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00013504 File Offset: 0x00011704
		private Participant GetOfficialFileParticipant(ContentSetting policy)
		{
			Participant participant = null;
			if (!this.officialFileParticipantDictionary.TryGetValue(policy, out participant))
			{
				AutoCopyEnforcer.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Getting the archive address for policy {1} from the AD.", TraceContext.Get(), policy.Name);
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, base.MailboxData.MailboxSession.MailboxOwner.MailboxInfo.OrganizationId.ToADSessionSettings(), 590, "GetOfficialFileParticipant", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\elc\\ManagedFolderAssistant\\AutoCopyEnforcer.cs");
				ADRecipient adrecipient = tenantOrRootOrgRecipientSession.Read(policy.AddressForJournaling);
				string arg = adrecipient.PrimarySmtpAddress.ToString();
				participant = new Participant(adrecipient);
				AutoCopyEnforcer.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: Archive address for policy {1} is {2}.", TraceContext.Get(), policy.Name, arg);
				this.officialFileParticipantDictionary[policy] = participant;
			}
			return participant;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000135DC File Offset: 0x000117DC
		private string GetMessageClass(JournalingFormat autoCopyFormat)
		{
			switch (autoCopyFormat)
			{
			case JournalingFormat.UseMsg:
				return "IPM.Note.JournalReport.Msg";
			case JournalingFormat.UseTnef:
				return "IPM.Note.JournalReport.Tnef";
			default:
				return null;
			}
		}

		// Token: 0x0400027A RID: 634
		private const string SytemMailboxElcFolderName = "Temporary ELC Folder";

		// Token: 0x0400027B RID: 635
		private const string JournalReportTnef = "IPM.Note.JournalReport.Tnef";

		// Token: 0x0400027C RID: 636
		private const string JournalReportMsg = "IPM.Note.JournalReport.Msg";

		// Token: 0x0400027D RID: 637
		private const string JournalingReportSubject = "Autocopy report";

		// Token: 0x0400027E RID: 638
		private static readonly Trace Tracer = ExTraceGlobals.AutoCopyEnforcerTracer;

		// Token: 0x0400027F RID: 639
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000280 RID: 640
		private DatabaseInfo databaseInfo;

		// Token: 0x04000281 RID: 641
		private Participant ndrParticipant;

		// Token: 0x04000282 RID: 642
		private VersionedId sendFolderId;

		// Token: 0x04000283 RID: 643
		private Dictionary<ContentSetting, Participant> officialFileParticipantDictionary = new Dictionary<ContentSetting, Participant>();
	}
}
