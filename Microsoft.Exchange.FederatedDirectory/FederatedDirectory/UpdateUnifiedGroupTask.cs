using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Net.AAD;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UpdateUnifiedGroupTask : UnifiedGroupsTask
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000048DC File Offset: 0x00002ADC
		public UpdateUnifiedGroupTask(ADUser accessingUser, ExchangePrincipal accessingPrincipal, IRecipientSession adSession) : base(accessingUser, adSession)
		{
			this.accessingPrincipal = accessingPrincipal;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000048ED File Offset: 0x00002AED
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000048F5 File Offset: 0x00002AF5
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000048FE File Offset: 0x00002AFE
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00004906 File Offset: 0x00002B06
		public string SmtpAddress { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000490F File Offset: 0x00002B0F
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00004917 File Offset: 0x00002B17
		public string Description { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004920 File Offset: 0x00002B20
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004928 File Offset: 0x00002B28
		public string DisplayName { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004931 File Offset: 0x00002B31
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00004939 File Offset: 0x00002B39
		public string[] AddedOwners { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004942 File Offset: 0x00002B42
		// (set) Token: 0x060000BD RID: 189 RVA: 0x0000494A File Offset: 0x00002B4A
		public string[] RemovedOwners { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004953 File Offset: 0x00002B53
		// (set) Token: 0x060000BF RID: 191 RVA: 0x0000495B File Offset: 0x00002B5B
		public string[] AddedMembers { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004964 File Offset: 0x00002B64
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000496C File Offset: 0x00002B6C
		public string[] RemovedMembers { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004975 File Offset: 0x00002B75
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000497D File Offset: 0x00002B7D
		public string[] AddedPendingMembers { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004986 File Offset: 0x00002B86
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x0000498E File Offset: 0x00002B8E
		public string[] RemovedPendingMembers { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004997 File Offset: 0x00002B97
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000499F File Offset: 0x00002B9F
		public bool? RequireSenderAuthenticationEnabled { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000049A8 File Offset: 0x00002BA8
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000049B0 File Offset: 0x00002BB0
		public bool? AutoSubscribeNewGroupMembers { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000049B9 File Offset: 0x00002BB9
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000049C1 File Offset: 0x00002BC1
		public CultureInfo Language { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000049CA File Offset: 0x00002BCA
		protected override string TaskName
		{
			get
			{
				return "UpdateUnifiedGroupTask";
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000049D4 File Offset: 0x00002BD4
		protected override void RunInternal()
		{
			UnifiedGroupsTask.Tracer.TraceDebug<Guid, string, string>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.Run: User {1} is updating group {2}", base.ActivityId, this.accessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), this.ExternalDirectoryObjectId ?? this.SmtpAddress);
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.ResolveExternalIdentities;
			if (base.IsAADEnabled || base.IsSharePointEnabled)
			{
				this.GetIdentitiesForParameters();
			}
			UpdateUnifiedGroupTask.UpdateAADLinkResults updateAADLinkResults = null;
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.AADUpdate;
			if (base.IsAADEnabled)
			{
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.Run: Updating group in AAD", base.ActivityId);
				updateAADLinkResults = this.UpdateAAD();
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.Run: Finished updating group in AAD", base.ActivityId);
				base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.SharePointUpdate;
				if (base.IsSharePointEnabled)
				{
					UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.Run: Enqueueing job to notify SharePoint about group update", base.ActivityId);
					UpdateSiteCollectionTask task = new UpdateSiteCollectionTask(base.AccessingUser, base.ADSession, base.ActivityId)
					{
						Description = this.Description,
						DisplayName = this.DisplayName,
						AddedOwners = this.GetSucceededLinkExternalIds(this.addedOwnersIdentities, updateAADLinkResults.FailedAddedOwners),
						RemovedOwners = this.GetSucceededLinkExternalIds(this.removedOwnersIdentities, updateAADLinkResults.FailedRemovedOwners),
						AddedMembers = this.GetSucceededLinkExternalIds(this.addedMembersIdentities, updateAADLinkResults.FailedAddedMembers),
						RemovedMembers = this.GetSucceededLinkExternalIds(this.removedMembersIdentities, updateAADLinkResults.FailedRemovedMembers),
						ExternalDirectoryObjectId = this.ExternalDirectoryObjectId
					};
					bool flag = UnifiedGroupsTask.QueueTask(task);
					UnifiedGroupsTask.Tracer.TraceDebug<Guid, bool>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.Run: Finished enqueueing job to notify SharePoint about group update. queued: {1}", base.ActivityId, flag);
					if (!flag)
					{
						UnifiedGroupsTask.Tracer.TraceError<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.Run: Failed to queue job to notify SharePoint about group update", base.ActivityId);
						FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.TraceTag>
						{
							{
								FederatedDirectoryLogSchema.TraceTag.TaskName,
								this.TaskName
							},
							{
								FederatedDirectoryLogSchema.TraceTag.ActivityId,
								base.ActivityId
							},
							{
								FederatedDirectoryLogSchema.TraceTag.CurrentAction,
								base.CurrentAction
							},
							{
								FederatedDirectoryLogSchema.TraceTag.Message,
								"Failed to queue job to notify SharePoint about group update. ExternalDirectoryObjectId: " + this.ExternalDirectoryObjectId
							}
						});
					}
				}
				else
				{
					UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.Run: SharePoint is not enabled, skipping notification about group creation", base.ActivityId);
				}
			}
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.ExchangeUpdate;
			UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.Run: Updating group in Exchange", base.ActivityId);
			try
			{
				this.UpdateGroupMailbox(updateAADLinkResults);
			}
			catch (ExchangeAdaptorException arg)
			{
				if (updateAADLinkResults == null || !updateAADLinkResults.ContainsFailure())
				{
					throw;
				}
				FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.TraceTag>
				{
					{
						FederatedDirectoryLogSchema.TraceTag.TaskName,
						this.TaskName
					},
					{
						FederatedDirectoryLogSchema.TraceTag.ActivityId,
						base.ActivityId
					},
					{
						FederatedDirectoryLogSchema.TraceTag.CurrentAction,
						base.CurrentAction
					},
					{
						FederatedDirectoryLogSchema.TraceTag.Message,
						string.Format("AAD partially failed and Exchange threw an exception. ExternalDirectoryObjectId: {0}, {1}", this.ExternalDirectoryObjectId ?? this.SmtpAddress, arg)
					}
				});
			}
			UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.Run: Finished updating group in Exchange", base.ActivityId);
			FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.TraceTag>
			{
				{
					FederatedDirectoryLogSchema.TraceTag.TaskName,
					this.TaskName
				},
				{
					FederatedDirectoryLogSchema.TraceTag.ActivityId,
					base.ActivityId
				},
				{
					FederatedDirectoryLogSchema.TraceTag.CurrentAction,
					base.CurrentAction
				},
				{
					FederatedDirectoryLogSchema.TraceTag.Message,
					string.Format("Updated group. ExternalDirectoryObjectId: {0}, By: {1}", this.ExternalDirectoryObjectId ?? this.SmtpAddress, this.accessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString())
				}
			});
			this.ThrowIfPartialSuccess(updateAADLinkResults);
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.Completed;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004D8C File Offset: 0x00002F8C
		private void GetIdentitiesForParameters()
		{
			this.identityMapping = new ObjectIdMapping(base.ADSession);
			this.identityMapping.Prefetch(new string[]
			{
				this.SmtpAddress
			});
			this.identityMapping.Prefetch(this.AddedOwners);
			this.identityMapping.Prefetch(this.RemovedOwners);
			this.identityMapping.Prefetch(this.AddedMembers);
			this.identityMapping.Prefetch(this.RemovedMembers);
			this.identityMapping.Prefetch(this.AddedPendingMembers);
			this.identityMapping.Prefetch(this.RemovedPendingMembers);
			if (string.IsNullOrEmpty(this.ExternalDirectoryObjectId))
			{
				this.ExternalDirectoryObjectId = this.identityMapping.GetIdentityFromSmtpAddress(this.SmtpAddress);
			}
			this.addedMembersIdentities = this.identityMapping.GetIdentitiesFromSmtpAddresses(this.AddedMembers);
			this.removedMembersIdentities = this.identityMapping.GetIdentitiesFromSmtpAddresses(this.RemovedMembers);
			this.addedOwnersIdentities = this.identityMapping.GetIdentitiesFromSmtpAddresses(this.AddedOwners);
			this.removedOwnersIdentities = this.identityMapping.GetIdentitiesFromSmtpAddresses(this.RemovedOwners);
			this.addedPendingMembersIdentities = this.identityMapping.GetIdentitiesFromSmtpAddresses(this.AddedPendingMembers);
			this.removedPendingMembersIdentities = this.identityMapping.GetIdentitiesFromSmtpAddresses(this.RemovedPendingMembers);
			if (this.identityMapping.InvalidSmtpAddresses.Count > 0)
			{
				FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.TraceTag>
				{
					{
						FederatedDirectoryLogSchema.TraceTag.TaskName,
						this.TaskName
					},
					{
						FederatedDirectoryLogSchema.TraceTag.ActivityId,
						base.ActivityId
					},
					{
						FederatedDirectoryLogSchema.TraceTag.CurrentAction,
						base.CurrentAction
					},
					{
						FederatedDirectoryLogSchema.TraceTag.Message,
						string.Format("Unable to parse identities as SMTP Addresses: {0}", string.Join(",", this.identityMapping.InvalidSmtpAddresses))
					}
				});
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004F50 File Offset: 0x00003150
		private UpdateUnifiedGroupTask.UpdateAADLinkResults UpdateAAD()
		{
			UpdateUnifiedGroupTask.UpdateAADLinkResults updateAADLinkResults = new UpdateUnifiedGroupTask.UpdateAADLinkResults();
			if (this.Description != null || this.DisplayName != null)
			{
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "Activityid={0}. UpdateUnifiedGroupTask.UpdateAAD: Calling UpdateGroup", base.ActivityId);
				base.AADClient.UpdateGroup(this.ExternalDirectoryObjectId, this.Description, null, this.DisplayName, null);
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "Activityid={0}. UpdateUnifiedGroupTask.UpdateAAD: Finished calling UpdateGroup", base.ActivityId);
			}
			if (this.addedMembersIdentities != null && this.addedMembersIdentities.Length != 0)
			{
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.UpdateAAD: Adding members", base.ActivityId);
				updateAADLinkResults.FailedAddedMembers = base.AADClient.AddMembers(this.ExternalDirectoryObjectId, this.addedMembersIdentities);
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.UpdateAAD: Finished adding members", base.ActivityId);
			}
			if (this.removedMembersIdentities != null && this.removedMembersIdentities.Length != 0)
			{
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.UpdateAAD: Removing members", base.ActivityId);
				updateAADLinkResults.FailedRemovedMembers = base.AADClient.RemoveMembers(this.ExternalDirectoryObjectId, this.removedMembersIdentities);
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.UpdateAAD: Finished removing members", base.ActivityId);
			}
			if (this.addedOwnersIdentities != null && this.addedOwnersIdentities.Length != 0)
			{
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.UpdateAAD: Adding owners", base.ActivityId);
				updateAADLinkResults.FailedAddedOwners = base.AADClient.AddOwners(this.ExternalDirectoryObjectId, this.addedOwnersIdentities);
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.UpdateAAD: Finished adding owners", base.ActivityId);
			}
			if (this.removedOwnersIdentities != null && this.removedOwnersIdentities.Length != 0)
			{
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.UpdateAAD: Removing owners", base.ActivityId);
				updateAADLinkResults.FailedRemovedOwners = base.AADClient.RemoveOwners(this.ExternalDirectoryObjectId, this.removedOwnersIdentities);
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.UpdateAAD: Finished removing owners", base.ActivityId);
			}
			if (this.addedPendingMembersIdentities != null && this.addedPendingMembersIdentities.Length != 0)
			{
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.UpdateAAD: Adding pending members", base.ActivityId);
				updateAADLinkResults.FailedAddedPendingMembers = base.AADClient.AddPendingMembers(this.ExternalDirectoryObjectId, this.addedPendingMembersIdentities);
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.UpdateAAD: Finished adding pending members", base.ActivityId);
			}
			if (this.removedPendingMembersIdentities != null && this.removedPendingMembersIdentities.Length != 0)
			{
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.UpdateAAD: Removing pending members", base.ActivityId);
				updateAADLinkResults.FailedRemovedPendingMembers = base.AADClient.RemovePendingMembers(this.ExternalDirectoryObjectId, this.removedPendingMembersIdentities);
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.UpdateAAD: Finished removing pending members", base.ActivityId);
			}
			return updateAADLinkResults;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005260 File Offset: 0x00003460
		private void UpdateGroupMailbox(UpdateUnifiedGroupTask.UpdateAADLinkResults results)
		{
			using (PSLocalTask<SetGroupMailbox, object> pslocalTask = CmdletTaskFactory.Instance.CreateSetGroupMailboxTask(this.accessingPrincipal))
			{
				pslocalTask.CaptureAdditionalIO = true;
				pslocalTask.Task.ExecutingUser = new RecipientIdParameter(this.accessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
				pslocalTask.Task.Identity = new RecipientIdParameter(this.ExternalDirectoryObjectId ?? this.SmtpAddress);
				if (this.Description != null)
				{
					pslocalTask.Task.Description = this.Description;
				}
				if (!string.IsNullOrEmpty(this.DisplayName))
				{
					pslocalTask.Task.DisplayName = this.DisplayName;
				}
				string[] array = (results != null) ? this.GetSucceededLinkSmtpAddresses(this.addedMembersIdentities, results.FailedAddedMembers) : this.AddedMembers;
				if (array != null && array.Length != 0)
				{
					pslocalTask.Task.AddedMembers = (from o in array
					select new RecipientIdParameter(o)).ToArray<RecipientIdParameter>();
				}
				string[] array2 = (results != null) ? this.GetSucceededLinkSmtpAddresses(this.removedMembersIdentities, results.FailedRemovedMembers) : this.RemovedMembers;
				if (array2 != null && array2.Length != 0)
				{
					pslocalTask.Task.RemovedMembers = (from o in array2
					select new RecipientIdParameter(o)).ToArray<RecipientIdParameter>();
				}
				string[] array3 = (results != null) ? this.GetSucceededLinkSmtpAddresses(this.addedOwnersIdentities, results.FailedAddedOwners) : this.AddedOwners;
				if (array3 != null && array3.Length != 0)
				{
					pslocalTask.Task.AddOwners = (from o in array3
					select new RecipientIdParameter(o)).ToArray<RecipientIdParameter>();
				}
				string[] array4 = (results != null) ? this.GetSucceededLinkSmtpAddresses(this.removedOwnersIdentities, results.FailedRemovedOwners) : this.RemovedOwners;
				if (array4 != null && array4.Length != 0)
				{
					pslocalTask.Task.RemoveOwners = (from o in array4
					select new RecipientIdParameter(o)).ToArray<RecipientIdParameter>();
				}
				if (this.RequireSenderAuthenticationEnabled != null)
				{
					pslocalTask.Task.RequireSenderAuthenticationEnabled = this.RequireSenderAuthenticationEnabled.Value;
				}
				if (this.AutoSubscribeNewGroupMembers != null)
				{
					pslocalTask.Task.AutoSubscribeNewGroupMembers = this.AutoSubscribeNewGroupMembers.Value;
				}
				if (this.Language != null)
				{
					pslocalTask.Task.Language = this.Language;
				}
				UnifiedGroupsTask.Tracer.TraceDebug<Guid, PSLocalTaskLogging.SetGroupMailboxToString>((long)this.GetHashCode(), "ActivityId={0}. {1}", base.ActivityId, new PSLocalTaskLogging.SetGroupMailboxToString(pslocalTask.Task));
				pslocalTask.Task.Execute();
				UnifiedGroupsTask.Tracer.TraceDebug<Guid, PSLocalTaskLogging.TaskOutputToString>((long)this.GetHashCode(), "ActivityId={0}. {1}", base.ActivityId, new PSLocalTaskLogging.TaskOutputToString(pslocalTask.AdditionalIO));
				if (pslocalTask.Error != null)
				{
					UnifiedGroupsTask.Tracer.TraceError<Guid, string>((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.UpdateGroupMailbox() failed: {1}", base.ActivityId, pslocalTask.ErrorMessage);
					throw new ExchangeAdaptorException(Strings.GroupMailboxFailedUpdate(this.ExternalDirectoryObjectId ?? this.SmtpAddress, pslocalTask.ErrorMessage));
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000055B4 File Offset: 0x000037B4
		private void ThrowIfPartialSuccess(UpdateUnifiedGroupTask.UpdateAADLinkResults results)
		{
			if (results != null && results.ContainsFailure())
			{
				base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.AADUpdate;
				AADPartialFailureException ex = new AADPartialFailureException(string.Format("Partially failed to update group: {0}", this.ExternalDirectoryObjectId ?? this.SmtpAddress))
				{
					FailedAddedMembers = this.GetOriginalFailedLinks(results.FailedAddedMembers),
					FailedRemovedMembers = this.GetOriginalFailedLinks(results.FailedRemovedMembers),
					FailedAddedOwners = this.GetOriginalFailedLinks(results.FailedAddedOwners),
					FailedRemovedOwners = this.GetOriginalFailedLinks(results.FailedRemovedOwners),
					FailedAddedPendingMembers = this.GetOriginalFailedLinks(results.FailedAddedPendingMembers),
					FailedRemovedPendingMembers = this.GetOriginalFailedLinks(results.FailedRemovedPendingMembers)
				};
				throw ex;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005674 File Offset: 0x00003874
		private string[] GetSucceededLinkExternalIds(string[] allLinks, AADClient.LinkResult[] failedLinkResults)
		{
			if (allLinks == null)
			{
				return null;
			}
			if (failedLinkResults == null)
			{
				return allLinks;
			}
			string[] second = (from linkResult in failedLinkResults
			select linkResult.FailedLink).ToArray<string>();
			string[] array = allLinks.Except(second).ToArray<string>();
			if (array.Length <= 0)
			{
				return null;
			}
			return array;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000056D8 File Offset: 0x000038D8
		private string[] GetSucceededLinkSmtpAddresses(string[] allLinks, AADClient.LinkResult[] failedLinkResults)
		{
			string[] succeededLinkExternalIds = this.GetSucceededLinkExternalIds(allLinks, failedLinkResults);
			if (succeededLinkExternalIds == null)
			{
				return null;
			}
			return (from link in succeededLinkExternalIds
			select this.identityMapping.GetSmtpAddressFromIdentity(link)).ToArray<string>();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000570C File Offset: 0x0000390C
		private AADPartialFailureException.FailedLink[] GetOriginalFailedLinks(AADClient.LinkResult[] failedLinkResults)
		{
			if (failedLinkResults == null)
			{
				return null;
			}
			List<AADPartialFailureException.FailedLink> list = new List<AADPartialFailureException.FailedLink>(failedLinkResults.Length);
			foreach (AADClient.LinkResult linkResult in failedLinkResults)
			{
				list.Add(new AADPartialFailureException.FailedLink
				{
					Link = this.identityMapping.GetSmtpAddressFromIdentity(linkResult.FailedLink),
					Exception = linkResult.Exception
				});
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x04000061 RID: 97
		private string[] addedOwnersIdentities;

		// Token: 0x04000062 RID: 98
		private string[] removedOwnersIdentities;

		// Token: 0x04000063 RID: 99
		private string[] addedMembersIdentities;

		// Token: 0x04000064 RID: 100
		private string[] removedMembersIdentities;

		// Token: 0x04000065 RID: 101
		private string[] addedPendingMembersIdentities;

		// Token: 0x04000066 RID: 102
		private string[] removedPendingMembersIdentities;

		// Token: 0x04000067 RID: 103
		private ExchangePrincipal accessingPrincipal;

		// Token: 0x04000068 RID: 104
		private ObjectIdMapping identityMapping;

		// Token: 0x02000016 RID: 22
		private class UpdateAADLinkResults
		{
			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060000DB RID: 219 RVA: 0x00005785 File Offset: 0x00003985
			// (set) Token: 0x060000DC RID: 220 RVA: 0x0000578D File Offset: 0x0000398D
			public AADClient.LinkResult[] FailedAddedMembers { get; set; }

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060000DD RID: 221 RVA: 0x00005796 File Offset: 0x00003996
			// (set) Token: 0x060000DE RID: 222 RVA: 0x0000579E File Offset: 0x0000399E
			public AADClient.LinkResult[] FailedRemovedMembers { get; set; }

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060000DF RID: 223 RVA: 0x000057A7 File Offset: 0x000039A7
			// (set) Token: 0x060000E0 RID: 224 RVA: 0x000057AF File Offset: 0x000039AF
			public AADClient.LinkResult[] FailedAddedOwners { get; set; }

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x060000E1 RID: 225 RVA: 0x000057B8 File Offset: 0x000039B8
			// (set) Token: 0x060000E2 RID: 226 RVA: 0x000057C0 File Offset: 0x000039C0
			public AADClient.LinkResult[] FailedRemovedOwners { get; set; }

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060000E3 RID: 227 RVA: 0x000057C9 File Offset: 0x000039C9
			// (set) Token: 0x060000E4 RID: 228 RVA: 0x000057D1 File Offset: 0x000039D1
			public AADClient.LinkResult[] FailedAddedPendingMembers { get; set; }

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060000E5 RID: 229 RVA: 0x000057DA File Offset: 0x000039DA
			// (set) Token: 0x060000E6 RID: 230 RVA: 0x000057E2 File Offset: 0x000039E2
			public AADClient.LinkResult[] FailedRemovedPendingMembers { get; set; }

			// Token: 0x060000E7 RID: 231 RVA: 0x000057EB File Offset: 0x000039EB
			public bool ContainsFailure()
			{
				return this.FailedAddedMembers != null || this.FailedRemovedMembers != null || this.FailedAddedOwners != null || this.FailedRemovedOwners != null || this.FailedAddedPendingMembers != null || this.FailedRemovedPendingMembers != null;
			}
		}
	}
}
