using System;
using System.Globalization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Net.AAD;
using Microsoft.Exchange.UnifiedGroups;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CreateUnifiedGroupTask : UnifiedGroupsTask
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000028E8 File Offset: 0x00000AE8
		public CreateUnifiedGroupTask(ADUser accessingUser, ExchangePrincipal accessingPrincipal, IRecipientSession adSession) : base(accessingUser, adSession)
		{
			this.accessingPrincipal = accessingPrincipal;
			this.CreationDiagnostics = new CreationDiagnostics();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003B RID: 59 RVA: 0x00002904 File Offset: 0x00000B04
		// (remove) Token: 0x0600003C RID: 60 RVA: 0x0000293C File Offset: 0x00000B3C
		public event Action AADComplete;

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002971 File Offset: 0x00000B71
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002979 File Offset: 0x00000B79
		public CreationDiagnostics CreationDiagnostics { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002982 File Offset: 0x00000B82
		// (set) Token: 0x06000040 RID: 64 RVA: 0x0000298A File Offset: 0x00000B8A
		public string Name { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002993 File Offset: 0x00000B93
		// (set) Token: 0x06000042 RID: 66 RVA: 0x0000299B File Offset: 0x00000B9B
		public string Alias { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000029A4 File Offset: 0x00000BA4
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000029AC File Offset: 0x00000BAC
		public string Description { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000029B5 File Offset: 0x00000BB5
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000029BD File Offset: 0x00000BBD
		public ModernGroupTypeInfo Type { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000029C6 File Offset: 0x00000BC6
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000029CE File Offset: 0x00000BCE
		public string ExternalDirectoryObjectId { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000029D7 File Offset: 0x00000BD7
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000029DF File Offset: 0x00000BDF
		public Guid ADObjectGuid { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000029E8 File Offset: 0x00000BE8
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000029F0 File Offset: 0x00000BF0
		public string SmtpAddress { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000029F9 File Offset: 0x00000BF9
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002A01 File Offset: 0x00000C01
		public bool? AutoSubscribeNewGroupMembers { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002A0A File Offset: 0x00000C0A
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002A12 File Offset: 0x00000C12
		public CultureInfo Language { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002A1B File Offset: 0x00000C1B
		protected override string TaskName
		{
			get
			{
				return "CreateUnifiedGroupTask";
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A24 File Offset: 0x00000C24
		protected override void RunInternal()
		{
			this.CreationDiagnostics.Start();
			UnifiedGroupsTask.Tracer.TraceDebug((long)this.GetHashCode(), "ActivityId={0}. UpdateUnifiedGroupTask.Run: User {1} is creating a group. Name: {2}, Alias: {3}", new object[]
			{
				base.ActivityId,
				this.accessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString(),
				this.Name,
				this.Alias
			});
			this.CreationDiagnostics.CmdletLogCorrelationId = base.ActivityId;
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.AADCreate;
			UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: Creating group in AAD", base.ActivityId);
			this.ExternalDirectoryObjectId = this.CreateAAD();
			UnifiedGroupsTask.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: Finished creating group in AAD. ExternalDirectoryObjectId: {1}", base.ActivityId, this.ExternalDirectoryObjectId);
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.AADAddOwnerAsMember;
			UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: Adding owner as member in AAD", base.ActivityId);
			try
			{
				this.AddOwnerAsMember(this.ExternalDirectoryObjectId, base.AccessingUser.ExternalDirectoryObjectId);
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: Finished adding owner as member in AAD", base.ActivityId);
			}
			catch (AADException ex)
			{
				UnifiedGroupsTask.Tracer.TraceError<Guid, AADException>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: Adding owner as member in AAD failed: {1}", base.ActivityId, ex);
				FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.ExceptionTag>
				{
					{
						FederatedDirectoryLogSchema.ExceptionTag.TaskName,
						this.TaskName
					},
					{
						FederatedDirectoryLogSchema.ExceptionTag.ActivityId,
						base.ActivityId
					},
					{
						FederatedDirectoryLogSchema.ExceptionTag.ExceptionType,
						ex.GetType()
					},
					{
						FederatedDirectoryLogSchema.ExceptionTag.ExceptionDetail,
						ex
					},
					{
						FederatedDirectoryLogSchema.ExceptionTag.CurrentAction,
						base.CurrentAction
					},
					{
						FederatedDirectoryLogSchema.ExceptionTag.Message,
						"Adding owner as member in AAD failed"
					}
				});
			}
			this.CreationDiagnostics.RecordAADTime();
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.AADCompleteCallback;
			if (this.AADComplete != null)
			{
				try
				{
					UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: Calling AADComplete", base.ActivityId);
					this.AADComplete();
					UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: Finished calling AADComplete", base.ActivityId);
				}
				catch (LocalizedException ex2)
				{
					UnifiedGroupsTask.Tracer.TraceError<Guid, LocalizedException>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: AADComplete event failed: {1}", base.ActivityId, ex2);
					FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.ExceptionTag>
					{
						{
							FederatedDirectoryLogSchema.ExceptionTag.TaskName,
							this.TaskName
						},
						{
							FederatedDirectoryLogSchema.ExceptionTag.ActivityId,
							base.ActivityId
						},
						{
							FederatedDirectoryLogSchema.ExceptionTag.ExceptionType,
							ex2.GetType()
						},
						{
							FederatedDirectoryLogSchema.ExceptionTag.ExceptionDetail,
							ex2
						},
						{
							FederatedDirectoryLogSchema.ExceptionTag.CurrentAction,
							base.CurrentAction
						},
						{
							FederatedDirectoryLogSchema.ExceptionTag.Message,
							"AADComplete event failed"
						}
					});
				}
			}
			this.CreationDiagnostics.RecordAADCompleteCallbackTime();
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.SharePointCreate;
			if (base.IsSharePointEnabled)
			{
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: Queuing job to notify SharePoint about group creation", base.ActivityId);
				CreateSiteCollectionTask task = new CreateSiteCollectionTask(base.AccessingUser, base.ADSession, base.ActivityId)
				{
					Name = this.Name,
					Alias = this.Alias,
					Description = this.Description,
					Type = this.Type,
					ExternalDirectoryObjectId = this.ExternalDirectoryObjectId
				};
				bool flag = UnifiedGroupsTask.QueueTask(task);
				UnifiedGroupsTask.Tracer.TraceDebug<Guid, bool>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: Finished queuing job to notify SharePoint about group creation. queued: {1}", base.ActivityId, flag);
				if (!flag)
				{
					UnifiedGroupsTask.Tracer.TraceError<Guid>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: Failed to queue job to notify SharePoint about group creation", base.ActivityId);
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
							"Failed to queue job to notify SharePoint about group creation. ExternalDirectoryObjectId: " + this.ExternalDirectoryObjectId
						}
					});
				}
			}
			else
			{
				UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: SharePoint is not enabled, skipping notification about group creation", base.ActivityId);
			}
			this.CreationDiagnostics.RecordSharePointNotificationTime();
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.ExchangeCreate;
			UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: Creating group in Exchange", base.ActivityId);
			GroupMailbox groupMailbox = this.CreateGroupMailbox(this.ExternalDirectoryObjectId);
			this.ADObjectGuid = groupMailbox.Guid;
			this.SmtpAddress = groupMailbox.PrimarySmtpAddress.ToString();
			UnifiedGroupsTask.Tracer.TraceDebug<Guid, ObjectId>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.Run: Finished creating group in Exchange. Identity: {1}", base.ActivityId, groupMailbox.Identity);
			this.CreationDiagnostics.RecordMailboxTime();
			this.CreationDiagnostics.MailboxCreatedSuccessfully = true;
			string text = this.EnsureGroupIsInDirectoryCache(groupMailbox);
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
					string.Format("Created group. Name: {0}, Alias: {1}, Type: {2}, ExternalDirectoryObjectId: {3}, By: {4}. EnsureCached={5}", new object[]
					{
						this.Name,
						this.Alias,
						this.Type,
						this.ExternalDirectoryObjectId,
						this.accessingPrincipal.MailboxInfo.PrimarySmtpAddress,
						text
					})
				}
			});
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.Completed;
			this.CreationDiagnostics.Stop();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F98 File Offset: 0x00001198
		private string CreateAAD()
		{
			if (!base.IsAADEnabled)
			{
				return Guid.NewGuid().ToString();
			}
			return base.AADClient.CreateGroup(this.Name, this.Alias, this.Description, this.Type == ModernGroupTypeInfo.Public);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002FE8 File Offset: 0x000011E8
		private void AddOwnerAsMember(string groupObjectId, string ownerObjectId)
		{
			if (!base.IsAADEnabled)
			{
				return;
			}
			base.AADClient.AddMembers(groupObjectId, new string[]
			{
				ownerObjectId
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003018 File Offset: 0x00001218
		private GroupMailbox CreateGroupMailbox(string groupObjectId)
		{
			GroupMailbox result;
			using (PSLocalTask<NewGroupMailbox, GroupMailbox> pslocalTask = CmdletTaskFactory.Instance.CreateNewGroupMailboxTask(this.accessingPrincipal))
			{
				pslocalTask.CaptureAdditionalIO = true;
				pslocalTask.Task.ExecutingUser = new RecipientIdParameter(this.accessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
				pslocalTask.Task.Alias = this.Alias;
				pslocalTask.Task.Name = this.Alias;
				pslocalTask.Task.DisplayName = this.Name;
				pslocalTask.Task.ModernGroupType = this.Type;
				pslocalTask.Task.AutoSubscribeNewGroupMembers = (this.AutoSubscribeNewGroupMembers != null && this.AutoSubscribeNewGroupMembers.Value);
				if (this.Language != null)
				{
					pslocalTask.Task.Language = this.Language;
				}
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
				{
					pslocalTask.Task.ExternalDirectoryObjectId = groupObjectId;
				}
				if (!string.IsNullOrEmpty(this.Description))
				{
					pslocalTask.Task.Description = this.Description;
				}
				pslocalTask.Task.Members = new RecipientIdParameter[]
				{
					new RecipientIdParameter(this.accessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString())
				};
				pslocalTask.Task.Owners = new RecipientIdParameter[]
				{
					new RecipientIdParameter(this.accessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString())
				};
				UnifiedGroupsTask.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "ActivityId={0}. {1}", base.ActivityId, new PSLocalTaskLogging.NewGroupMailboxToString(pslocalTask.Task).ToString());
				pslocalTask.Task.Execute();
				UnifiedGroupsTask.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "ActivityId={0}. {1}", base.ActivityId, new PSLocalTaskLogging.TaskOutputToString(pslocalTask.AdditionalIO).ToString());
				if (pslocalTask.Error != null)
				{
					UnifiedGroupsTask.Tracer.TraceError<Guid, string>((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.CreateGroupMailbox: New-GroupMailbox failed: {1}", base.ActivityId, pslocalTask.ErrorMessage);
					throw new ExchangeAdaptorException(Strings.GroupMailboxFailedCreate(this.Name, pslocalTask.ErrorMessage));
				}
				result = pslocalTask.Result;
			}
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003294 File Offset: 0x00001494
		private string EnsureGroupIsInDirectoryCache(GroupMailbox group)
		{
			IRecipientSession tenantOrRootRecipientReadOnlySession = DirectorySessionFactory.Default.GetTenantOrRootRecipientReadOnlySession(base.ADSession, group.OriginatingServer, 350, "EnsureGroupIsInDirectoryCache", "f:\\15.00.1497\\sources\\dev\\Management\\src\\FederatedDirectory\\CreateUnifiedGroupTask.cs");
			ProxyAddress proxyAddress = new SmtpProxyAddress(group.PrimarySmtpAddress.ToString(), true);
			OWAMiniRecipient owaminiRecipient = tenantOrRootRecipientReadOnlySession.FindMiniRecipientByProxyAddress<OWAMiniRecipient>(proxyAddress, OWAMiniRecipientSchema.AdditionalProperties);
			string text = (owaminiRecipient != null) ? (owaminiRecipient.IsCached ? "ReadFromCache" : "ReadFromDc") : "NotFound";
			UnifiedGroupsTask.Tracer.TraceDebug((long)this.GetHashCode(), "ActivityId={0}. CreateUnifiedGroupTask.EnsureGroupIsInDirectoryCache: ProxyAddress={1}, DomainController={2}, Result={3}", new object[]
			{
				base.ActivityId,
				proxyAddress,
				group.OriginatingServer,
				text
			});
			return text;
		}

		// Token: 0x04000031 RID: 49
		private readonly ExchangePrincipal accessingPrincipal;
	}
}
