using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Directory;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Office.Server.Directory;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x0200006A RID: 106
	internal sealed class ExchangeDirectoryAdaptor : BaseAdaptor
	{
		// Token: 0x06000273 RID: 627 RVA: 0x0000C88C File Offset: 0x0000AA8C
		public override void Initialize(NameValueCollection parameters)
		{
			base.Parameters = parameters;
			ExchangeDirectorySchema.Initialize();
			base.AdapterId = ExchangeDirectorySchema.AdaptorId;
			base.ServiceName = "Exchange";
			base.PropertyTypes = ExchangeDirectorySchema.PropertyDefinitions;
			base.ResourceTypes = ExchangeDirectorySchema.ResourceDefinitions;
			base.RelationTypes = ExchangeDirectorySchema.RelationDefinitions;
			BaseAdaptor.Tracer.TraceDebug<ExchangeDirectoryAdaptor>((long)this.GetHashCode(), "ExchangeDirectoryAdaptor.Initialize() called: schema initialized with: {0}", this);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public override void LoadDirectoryObjectData(DirectoryObjectAccessor directoryObjectAccessor, RequestSchema requestSchema, out IDirectoryObjectState state)
		{
			ExchangeDirectoryAdaptor.EnsureObject(directoryObjectAccessor);
			Group group = directoryObjectAccessor.DirectoryObject as Group;
			if (group != null)
			{
				if (this.GetGroupMailbox(directoryObjectAccessor, requestSchema))
				{
					state = new DirectoryObjectState
					{
						Version = 1L,
						IsCommitted = true
					};
					return;
				}
				throw new FederatedDirectoryException(CoreResources.GetGroupMailboxFailed(group.Id.ToString(), "NotFound"));
			}
			else
			{
				User user = directoryObjectAccessor.DirectoryObject as User;
				if (user != null)
				{
					if (this.GetUser(directoryObjectAccessor, requestSchema))
					{
						state = new DirectoryObjectState
						{
							Version = 1L,
							IsCommitted = true
						};
						return;
					}
					throw new FederatedDirectoryException(CoreResources.GetFederatedDirectoryUserFailed(user.Id.ToString(), "NotFound"));
				}
				else
				{
					Tenant tenant = directoryObjectAccessor.DirectoryObject as Tenant;
					if (tenant == null)
					{
						state = null;
						LogWriter.TraceAndLog(new LogWriter.TraceMethod(BaseAdaptor.Tracer.TraceDebug), 0, this.GetHashCode(), "ExchangeDirectoryAdaptor.LoadDirectoryObjectData(): ignoring directory object '{0}' because it is of unknown type: {1}", new object[]
						{
							directoryObjectAccessor.DirectoryObject.Id,
							directoryObjectAccessor.DirectoryObject.DirectoryObjectType
						});
						return;
					}
					if (this.GetTenant(directoryObjectAccessor, requestSchema))
					{
						state = new DirectoryObjectState
						{
							Version = 1L,
							IsCommitted = true
						};
						return;
					}
					throw new FederatedDirectoryException(CoreResources.GetTenantFailed(tenant.Id.ToString(), "NotFound"));
				}
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000CA78 File Offset: 0x0000AC78
		public override void RemoveDirectoryObject(DirectoryObjectAccessor directoryObjectAccessor)
		{
			ExchangeDirectoryAdaptor.EnsureObject(directoryObjectAccessor);
			Group group = directoryObjectAccessor.DirectoryObject as Group;
			if (group != null)
			{
				this.RemoveGroupMailbox(directoryObjectAccessor);
				return;
			}
			LogWriter.TraceAndLog(new LogWriter.TraceMethod(BaseAdaptor.Tracer.TraceDebug), 0, this.GetHashCode(), "ExchangeDirectoryAdaptor.RemoveDirectoryObject(): ignoring directory object '{0}' because it is of unknown type: {1}", new object[]
			{
				directoryObjectAccessor.DirectoryObject.Id,
				directoryObjectAccessor.DirectoryObject.DirectoryObjectType
			});
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000CAF4 File Offset: 0x0000ACF4
		public override void CommitDirectoryObject(DirectoryObjectAccessor directoryObjectAccessor)
		{
			ExchangeDirectoryAdaptor.EnsureObject(directoryObjectAccessor);
			Group group = directoryObjectAccessor.DirectoryObject as Group;
			if (group != null)
			{
				DirectoryObjectState directoryObjectState = (DirectoryObjectState)directoryObjectAccessor.GetState(base.ServiceName);
				if (directoryObjectState != null && directoryObjectState.IsNew)
				{
					IActivityScope activityScope = null;
					bool flag = false;
					try
					{
						activityScope = ActivityContext.GetCurrentActivityScope();
						if (activityScope == null)
						{
							activityScope = ActivityContext.Start(null);
							activityScope.SetProperty(ExtensibleLoggerMetadata.EventId, "PSDirectInvoke_NewGroupMailbox");
							flag = true;
						}
						this.CreateGroupMailbox(directoryObjectAccessor, activityScope.ActivityId);
						goto IL_81;
					}
					finally
					{
						if (flag)
						{
							activityScope.End();
						}
					}
				}
				this.UpdateGroupMailbox(directoryObjectAccessor, ExchangeDirectoryAdaptor.UpdateGroupCommitDirectoryObjectSchema);
				IL_81:
				if (directoryObjectState != null)
				{
					directoryObjectState.IsCommitted = true;
					directoryObjectState.Version += 1L;
					return;
				}
			}
			else
			{
				LogWriter.TraceAndLog(new LogWriter.TraceMethod(BaseAdaptor.Tracer.TraceDebug), 0, this.GetHashCode(), "ExchangeDirectoryAdaptor.CommitDirectoryObject(): ignoring directory object '{0}' because it is of unknown type: {1}", new object[]
				{
					directoryObjectAccessor.DirectoryObject.Id,
					directoryObjectAccessor.DirectoryObject.DirectoryObjectType
				});
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000CC00 File Offset: 0x0000AE00
		public override bool TryRelationExists(DirectorySession directorySession, string relationName, Guid parentObjectId, DirectoryObjectType parentObjectObjectType, Guid targetObjectId, out bool relationExists)
		{
			relationExists = false;
			return true;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000CC08 File Offset: 0x0000AE08
		public override void NotifyChanges(DirectoryObjectAccessor directoryObjectAccessor)
		{
			ExchangeDirectoryAdaptor.EnsureObject(directoryObjectAccessor);
			if (!ExEnvironment.IsTest)
			{
				LogWriter.TraceAndLog(new LogWriter.TraceMethod(BaseAdaptor.Tracer.TraceDebug), 3, this.GetHashCode(), "ExchangeDirectoryAdaptor.NotifyChanges(): Ignoring Object={0}, Type={1}.", new object[]
				{
					directoryObjectAccessor.DirectoryObject.Id,
					directoryObjectAccessor.DirectoryObject.DirectoryObjectType
				});
				return;
			}
			Group group = directoryObjectAccessor.DirectoryObject as Group;
			if (group == null)
			{
				LogWriter.TraceAndLog(new LogWriter.TraceMethod(BaseAdaptor.Tracer.TraceDebug), 0, this.GetHashCode(), "ExchangeDirectoryAdaptor.NotifyChanges(): ignoring directory object '{0}' because it is of unknown type: {1}", new object[]
				{
					directoryObjectAccessor.DirectoryObject.Id,
					directoryObjectAccessor.DirectoryObject.DirectoryObjectType
				});
				return;
			}
			DirectoryObjectState directoryObjectState = (DirectoryObjectState)directoryObjectAccessor.GetState(base.ServiceName);
			if (directoryObjectState != null && directoryObjectState.IsNew)
			{
				this.UpdateGroupMailbox(directoryObjectAccessor, ExchangeDirectoryAdaptor.CreateGroupNotifyChangesSchema);
				return;
			}
			this.UpdateGroupMailbox(directoryObjectAccessor, ExchangeDirectoryAdaptor.UpdateGroupNotifyChangesSchema);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000CD08 File Offset: 0x0000AF08
		private bool GetGroupMailbox(DirectoryObjectAccessor directoryObjectAccessor, RequestSchema requestSchema)
		{
			if (ExchangeDirectoryAdaptor.ContainsRelation(requestSchema, ExchangeDirectoryAdaptor.GetGroupMailboxRelations) || ExchangeDirectoryAdaptor.ContainsResource(requestSchema, ExchangeDirectoryAdaptor.GetGroupMailboxResources))
			{
				Group group = (Group)directoryObjectAccessor.DirectoryObject;
				ExchangeDirectorySessionContext exchangeDirectorySessionContext = (ExchangeDirectorySessionContext)directoryObjectAccessor.DirectoryObject.DirectorySession.SessionContext;
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.FullyConsistent, exchangeDirectorySessionContext.AccessingUser.OrganizationId.ToADSessionSettings(), 417, "GetGroupMailbox", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\FederatedDirectory\\ExchangeDirectoryAdaptor.cs");
				ADUser aduser = tenantOrRootOrgRecipientSession.FindADUserByExternalDirectoryObjectId(group.Id.ToString());
				if (aduser == null)
				{
					throw new FederatedDirectoryException(CoreResources.GetFederatedDirectoryUserFailed(group.Id.ToString(), "NotFound"));
				}
				if (ExchangeDirectoryAdaptor.ContainsResource(requestSchema, ExchangeDirectoryAdaptor.MailboxUrlResources))
				{
					MailboxUrls mailboxUrls = new MailboxUrls(ExchangePrincipal.FromADUser(aduser, null), false);
					directoryObjectAccessor.SetResource(ExchangeDirectorySchema.CalendarUrlResource.Name, mailboxUrls.CalendarUrl, false);
					directoryObjectAccessor.SetResource(ExchangeDirectorySchema.InboxUrlResource.Name, mailboxUrls.InboxUrl, false);
					directoryObjectAccessor.SetResource(ExchangeDirectorySchema.PeopleUrlResource.Name, mailboxUrls.PeopleUrl, false);
					directoryObjectAccessor.SetResource(ExchangeDirectorySchema.GroupPictureUrlResource.Name, mailboxUrls.PhotoUrl, false);
				}
				GetFederatedDirectoryGroupResponse groupMailbox = new GetFederatedDirectoryGroupResponse
				{
					Members = (ExchangeDirectoryAdaptor.ContainsRelation(requestSchema, ExchangeDirectorySchema.MembersRelation.Name) ? ExchangeDirectoryAdaptor.GetMembers(tenantOrRootOrgRecipientSession, aduser) : null),
					Owners = (ExchangeDirectoryAdaptor.ContainsRelation(requestSchema, ExchangeDirectorySchema.OwnersRelation.Name) ? ExchangeDirectoryAdaptor.GetOwners(tenantOrRootOrgRecipientSession, aduser) : null)
				};
				SchemaAdaptor.FromGroupMailboxToDirectoryObject(requestSchema, groupMailbox, directoryObjectAccessor);
			}
			return true;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000CEA4 File Offset: 0x0000B0A4
		private bool GetTenant(DirectoryObjectAccessor directoryObjectAccessor, RequestSchema requestSchema)
		{
			ExchangeDirectorySessionContext exchangeDirectorySessionContext = (ExchangeDirectorySessionContext)directoryObjectAccessor.DirectoryObject.DirectorySession.SessionContext;
			BaseAdaptor.Tracer.TraceDebug((long)this.GetHashCode(), "ExchangeDirectoryAdaptor.GetTenant()");
			if (directoryObjectAccessor.DirectoryObject.Id.Equals(exchangeDirectorySessionContext.TenantContextId))
			{
				directoryObjectAccessor.SetProperty("DefaultDomain", exchangeDirectorySessionContext.AccessingUser.PrimarySmtpAddress.Domain, false);
				return true;
			}
			return false;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000CF1C File Offset: 0x0000B11C
		private void CreateGroupMailbox(DirectoryObjectAccessor directoryObjectAccessor, Guid activityId)
		{
			ExchangeDirectorySessionContext exchangeDirectorySessionContext = (ExchangeDirectorySessionContext)directoryObjectAccessor.DirectoryObject.DirectorySession.SessionContext;
			exchangeDirectorySessionContext.CreationDiagnostics.RecordAADTime();
			exchangeDirectorySessionContext.CreationDiagnostics.CmdletLogCorrelationId = activityId;
			using (PSLocalTask<NewGroupMailbox, GroupMailbox> pslocalTask = CmdletTaskFactory.Instance.CreateNewGroupMailboxTask(exchangeDirectorySessionContext.AccessingPrincipal))
			{
				pslocalTask.CaptureAdditionalIO = true;
				pslocalTask.Task.ExecutingUser = new RecipientIdParameter(exchangeDirectorySessionContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
				RequestSchema requestSchemaFromDirectoryObjectChanges = ExchangeDirectoryAdaptor.GetRequestSchemaFromDirectoryObjectChanges(directoryObjectAccessor, ExchangeDirectoryAdaptor.CreateGroupCommitDirectoryObjectSchema);
				SchemaAdaptor.FromDirectoryObjectToCmdletParameter(requestSchemaFromDirectoryObjectChanges, directoryObjectAccessor, pslocalTask.Task);
				LogWriter.SimpleLog(new ExchangeDirectoryAdaptor.NewGroupMailboxToString(pslocalTask.Task));
				pslocalTask.Task.Execute();
				exchangeDirectorySessionContext.CreationDiagnostics.RecordMailboxTime();
				LogWriter.SimpleLog(new ExchangeDirectoryAdaptor.TaskOutputToString(pslocalTask.AdditionalIO));
				if (pslocalTask.Error != null)
				{
					LogWriter.TraceAndLog(new LogWriter.TraceMethod(BaseAdaptor.Tracer.TraceError), 1, this.GetHashCode(), "ExchangeDirectoryAdaptor.CreateGroupMailbox() failed: {0}", new object[]
					{
						pslocalTask.ErrorMessage
					});
					throw new FederatedDirectoryException(CoreResources.NewGroupMailboxFailed(directoryObjectAccessor.DirectoryObject.Id.ToString(), pslocalTask.ErrorMessage));
				}
				exchangeDirectorySessionContext.CreationDiagnostics.MailboxCreatedSuccessfully = true;
				directoryObjectAccessor.SetProperty(ExchangeDirectorySchema.ExchangeDirectoryObjectIdProperty.Name, pslocalTask.Result.Guid, true);
				directoryObjectAccessor.SetProperty("Mail", pslocalTask.Result.PrimarySmtpAddress.ToString(), true);
				this.EnsureGroupIsInDirectoryCache(exchangeDirectorySessionContext.AccessingUser, pslocalTask.Result);
				LogWriter.TraceAndLog(new LogWriter.TraceMethod(BaseAdaptor.Tracer.TraceDebug), 4, this.GetHashCode(), "ExchangeDirectoryAdaptor.CreateGroupMailbox() completed. Id={0}", new object[]
				{
					pslocalTask.Result.Identity
				});
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000D124 File Offset: 0x0000B324
		private void EnsureGroupIsInDirectoryCache(ADUser accessingUser, GroupMailbox group)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.FullyConsistent, accessingUser.OrganizationId.ToADSessionSettings(), 558, "EnsureGroupIsInDirectoryCache", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\FederatedDirectory\\ExchangeDirectoryAdaptor.cs");
			tenantOrRootOrgRecipientSession.DomainController = group.OriginatingServer;
			ProxyAddress proxyAddress = new SmtpProxyAddress(group.PrimarySmtpAddress.ToString(), true);
			ADUser aduser = tenantOrRootOrgRecipientSession.FindByProxyAddress(proxyAddress) as ADUser;
			OWAMiniRecipient owaminiRecipient = tenantOrRootOrgRecipientSession.FindMiniRecipientByProxyAddress<OWAMiniRecipient>(proxyAddress, OWAMiniRecipientSchema.AdditionalProperties);
			bool flag = aduser != null;
			bool flag2 = owaminiRecipient != null;
			LogWriter.TraceAndLog(new LogWriter.TraceMethod(BaseAdaptor.Tracer.TraceDebug), 4, this.GetHashCode(), "ExchangeDirectoryAdaptor.EnsureGroupIsInDirectoryCache: ProxyAddress={0}, DomainController={1}, ADUser={2}, OWAMiniRecipient={3}", new object[]
			{
				proxyAddress,
				group.OriginatingServer,
				flag ? (aduser.IsCached ? "Cached" : "NotCached") : "NotFound",
				flag2 ? (owaminiRecipient.IsCached ? "Cached" : "NotCached") : "NotFound"
			});
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000D22C File Offset: 0x0000B42C
		private static bool IsRequestSchemaEmpty(RequestSchema requestSchema)
		{
			return !requestSchema.IncludeAllProperties && requestSchema.Properties.Count == 0 && !requestSchema.IncludeAllResources && requestSchema.Resources.Count == 0 && !requestSchema.IncludeAllRelations && requestSchema.Relations.Count == 0;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000D27C File Offset: 0x0000B47C
		private void UpdateGroupMailbox(DirectoryObjectAccessor directoryObjectAccessor, HashSet<string> schema)
		{
			ExchangeDirectorySessionContext exchangeDirectorySessionContext = (ExchangeDirectorySessionContext)directoryObjectAccessor.DirectoryObject.DirectorySession.SessionContext;
			RequestSchema requestSchemaFromDirectoryObjectChanges = ExchangeDirectoryAdaptor.GetRequestSchemaFromDirectoryObjectChanges(directoryObjectAccessor, schema);
			if (!ExchangeDirectoryAdaptor.IsRequestSchemaEmpty(requestSchemaFromDirectoryObjectChanges))
			{
				using (PSLocalTask<SetGroupMailbox, object> pslocalTask = CmdletTaskFactory.Instance.CreateSetGroupMailboxTask(exchangeDirectorySessionContext.AccessingPrincipal))
				{
					pslocalTask.CaptureAdditionalIO = true;
					pslocalTask.Task.ExecutingUser = new RecipientIdParameter(exchangeDirectorySessionContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
					SchemaAdaptor.FromDirectoryObjectToCmdletParameter(requestSchemaFromDirectoryObjectChanges, directoryObjectAccessor, pslocalTask.Task);
					LogWriter.SimpleLog(new ExchangeDirectoryAdaptor.SetGroupMailboxToString(pslocalTask.Task));
					pslocalTask.Task.Execute();
					LogWriter.SimpleLog(new ExchangeDirectoryAdaptor.TaskOutputToString(pslocalTask.AdditionalIO));
					if (pslocalTask.Error != null)
					{
						LogWriter.TraceAndLog(new LogWriter.TraceMethod(BaseAdaptor.Tracer.TraceError), 1, this.GetHashCode(), "ExchangeDirectoryAdaptor.UpdateGroupMailbox() failed: {0}", new object[]
						{
							pslocalTask.ErrorMessage
						});
						throw new FederatedDirectoryException(CoreResources.SetGroupMailboxFailed(directoryObjectAccessor.DirectoryObject.Id.ToString(), pslocalTask.ErrorMessage));
					}
					LogWriter.TraceAndLog(new LogWriter.TraceMethod(BaseAdaptor.Tracer.TraceDebug), 4, this.GetHashCode(), "ExchangeDirectoryAdaptor.UpdateGroupMailbox() completed", new object[0]);
					return;
				}
			}
			BaseAdaptor.Tracer.TraceDebug((long)this.GetHashCode(), "ExchangeDirectoryAdaptor.UpdateGroupMailbox() nothing to do");
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000D404 File Offset: 0x0000B604
		private void RemoveGroupMailbox(DirectoryObjectAccessor directoryObjectAccessor)
		{
			ExchangeDirectorySessionContext exchangeDirectorySessionContext = (ExchangeDirectorySessionContext)directoryObjectAccessor.DirectoryObject.DirectorySession.SessionContext;
			using (PSLocalTask<RemoveGroupMailbox, object> pslocalTask = CmdletTaskFactory.Instance.CreateRemoveGroupMailboxTask(exchangeDirectorySessionContext.AccessingPrincipal))
			{
				pslocalTask.CaptureAdditionalIO = true;
				pslocalTask.Task.ExecutingUser = new RecipientIdParameter(exchangeDirectorySessionContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
				pslocalTask.Task.Identity = new MailboxIdParameter(directoryObjectAccessor.DirectoryObject.Id.ToString());
				LogWriter.SimpleLog(new ExchangeDirectoryAdaptor.RemoveGroupMailboxToString(pslocalTask.Task));
				pslocalTask.Task.Execute();
				LogWriter.SimpleLog(new ExchangeDirectoryAdaptor.TaskOutputToString(pslocalTask.AdditionalIO));
				if (pslocalTask.Error != null)
				{
					LogWriter.TraceAndLog(new LogWriter.TraceMethod(BaseAdaptor.Tracer.TraceError), 1, this.GetHashCode(), "ExchangeDirectoryAdaptor.RemoveGroupMailbox() failed: {0}", new object[]
					{
						pslocalTask.ErrorMessage
					});
					throw new FederatedDirectoryException(CoreResources.RemoveGroupMailboxFailed(directoryObjectAccessor.DirectoryObject.Id.ToString(), pslocalTask.ErrorMessage));
				}
				LogWriter.TraceAndLog(new LogWriter.TraceMethod(BaseAdaptor.Tracer.TraceDebug), 4, this.GetHashCode(), "ExchangeDirectoryAdaptor.RemoveGroupMailbox() succeeded", new object[0]);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		private bool GetUser(DirectoryObjectAccessor directoryObjectAccessor, RequestSchema requestSchema)
		{
			if (ExchangeDirectoryAdaptor.ContainsRelation(requestSchema, ExchangeDirectoryAdaptor.GetUserRelations) || ExchangeDirectoryAdaptor.ContainsResource(requestSchema, ExchangeDirectoryAdaptor.GetUserResources))
			{
				User user = (User)directoryObjectAccessor.DirectoryObject;
				ExchangeDirectorySessionContext exchangeDirectorySessionContext = (ExchangeDirectorySessionContext)directoryObjectAccessor.DirectoryObject.DirectorySession.SessionContext;
				IRecipientSession adSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.FullyConsistent, exchangeDirectorySessionContext.AccessingUser.OrganizationId.ToADSessionSettings(), 707, "GetUser", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\FederatedDirectory\\ExchangeDirectoryAdaptor.cs");
				ADUser adUser = adSession.FindADUserByExternalDirectoryObjectId(user.Id.ToString());
				if (adUser == null)
				{
					throw new FederatedDirectoryException(CoreResources.GetFederatedDirectoryUserFailed(user.Id.ToString(), "NotFound"));
				}
				GetFederatedDirectoryUserResponse user2;
				if (adUser.RecipientType == RecipientType.MailUser)
				{
					user2 = new GetFederatedDirectoryUserResponse
					{
						Groups = new FederatedDirectoryGroupType[0]
					};
				}
				else
				{
					if (adUser.RecipientType != RecipientType.UserMailbox)
					{
						throw new FederatedDirectoryException(CoreResources.GetFederatedDirectoryUserFailed(user.Id.ToString(), "RecipientType"));
					}
					List<GroupMailbox> groupMailboxes = null;
					GroupMailboxAccessLayer.Execute("GetFederatedDirectoryUser", adSession, adUser.ExchangeGuid, adUser.OrganizationId, "Client=MSExchangeRPC;Action=ExchangeDirectoryAdaptor", delegate(GroupMailboxAccessLayer accessLayer)
					{
						UserMailboxLocator user3 = UserMailboxLocator.Instantiate(adSession, adUser);
						groupMailboxes = accessLayer.GetJoinedGroups(user3, false).ToList<GroupMailbox>();
					});
					if (ExchangeDirectoryAdaptor.ContainsResource(requestSchema, ExchangeDirectoryAdaptor.MailboxUrlResources))
					{
						MailboxUrls mailboxUrls = new MailboxUrls(ExchangePrincipal.FromADUser(adUser, null), false);
						directoryObjectAccessor.SetResource(ExchangeDirectorySchema.UserPictureUrlResource.Name, mailboxUrls.PhotoUrl, false);
					}
					user2 = new GetFederatedDirectoryUserResponse
					{
						Groups = ExchangeDirectoryAdaptor.ConvertToFederatedDirectoryGroupTypeList(groupMailboxes)
					};
				}
				SchemaAdaptor.FromUserToDirectoryObject(requestSchema, user2, directoryObjectAccessor);
			}
			return true;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		private static FederatedDirectoryGroupType[] ConvertToFederatedDirectoryGroupTypeList(List<GroupMailbox> groupMailboxes)
		{
			List<FederatedDirectoryGroupType> list = new List<FederatedDirectoryGroupType>(groupMailboxes.Count);
			foreach (GroupMailbox groupMailbox in groupMailboxes)
			{
				list.Add(new FederatedDirectoryGroupType
				{
					ExternalDirectoryObjectId = groupMailbox.Locator.ExternalId,
					JoinDateTime = groupMailbox.JoinDate
				});
			}
			return list.ToArray();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000D84C File Offset: 0x0000BA4C
		private static FederatedDirectoryIdentityDetailsType[] GetOwners(IRecipientSession recipientSession, ADUser adUser)
		{
			ADObjectId[] array = ((MultiValuedProperty<ADObjectId>)adUser[GroupMailboxSchema.Owners]).ToArray();
			Result<ADRawEntry>[] array2 = recipientSession.ReadMultiple(array, ExchangeDirectoryAdaptor.IdentityProperties);
			List<FederatedDirectoryIdentityDetailsType> list = new List<FederatedDirectoryIdentityDetailsType>(array2.Length);
			for (int i = 0; i < array2.Length; i++)
			{
				Result<ADRawEntry> result = array2[i];
				if (result.Error != null || result.Data == null)
				{
					ExTraceGlobals.ModernGroupsTracer.TraceWarning<string, string>(0L, "GetFederatedDirectoryGroup.GetOwners: Unable to resolve \"{0}\":\"{1}\".", array[i].ToString(), (result.Error != null) ? result.Error.ToString() : string.Empty);
				}
				else
				{
					string text = (string)result.Data[ADRecipientSchema.ExternalDirectoryObjectId];
					if (string.IsNullOrEmpty(text))
					{
						ExTraceGlobals.ModernGroupsTracer.TraceWarning<string>(0L, "GetFederatedDirectoryGroup.GetOwners: Missing ExternalDirectoryObjectId for \"{0}\".", array[i].ToString());
					}
					else
					{
						list.Add(new FederatedDirectoryIdentityDetailsType
						{
							ExternalDirectoryObjectId = text
						});
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000DA04 File Offset: 0x0000BC04
		private static FederatedDirectoryIdentityDetailsType[] GetMembers(IRecipientSession recipientSession, ADUser adUser)
		{
			List<FederatedDirectoryIdentityDetailsType> result = new List<FederatedDirectoryIdentityDetailsType>(10);
			GroupMailboxLocator groupLocator = GroupMailboxLocator.Instantiate(recipientSession, adUser);
			GroupMailboxAccessLayer.Execute("GetFederatedDirectoryGroup", recipientSession, adUser.ExchangeGuid, adUser.OrganizationId, "Client=WebServices;Action=GetFederatedDirectoryGroup", delegate(GroupMailboxAccessLayer accessLayer)
			{
				IEnumerable<UserMailbox> members = accessLayer.GetMembers(groupLocator, false, null);
				foreach (UserMailbox userMailbox in members)
				{
					if (string.IsNullOrEmpty(userMailbox.Locator.ExternalId))
					{
						ExTraceGlobals.ModernGroupsTracer.TraceWarning<string>(0L, "GetFederatedDirectoryGroup.GetOwners: Missing ExternalDirectoryObjectId for \"{0}\".", userMailbox.Locator.LegacyDn);
					}
					else
					{
						result.Add(new FederatedDirectoryIdentityDetailsType
						{
							ExternalDirectoryObjectId = userMailbox.Locator.ExternalId
						});
					}
				}
			});
			return result.ToArray();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000DA64 File Offset: 0x0000BC64
		private static void EnsureObject(DirectoryObjectAccessor directoryObjectAccessor)
		{
			ArgumentValidator.ThrowIfNull("directoryObjectAccessor", directoryObjectAccessor);
			ArgumentValidator.ThrowIfNull("directoryObjectAccessor.DirectoryObject", directoryObjectAccessor.DirectoryObject);
			ArgumentValidator.ThrowIfEmpty("directoryObjectAccessor.DirectoryObject.Id", directoryObjectAccessor.DirectoryObject.Id);
			ArgumentValidator.ThrowIfNull("directoryObjectAccessor.DirectoryObject.DirectorySession", directoryObjectAccessor.DirectoryObject.DirectorySession);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000DAB8 File Offset: 0x0000BCB8
		private static RequestSchema GetRequestSchemaFromDirectoryObjectChanges(DirectoryObjectAccessor directoryObjectAccessor, HashSet<string> schema)
		{
			RequestSchema requestSchema = new RequestSchema();
			foreach (Property property in directoryObjectAccessor.GetChanges<Property>())
			{
				string name = property.GetPropertyType().Name;
				if (schema.Contains(name))
				{
					requestSchema.Properties.Add(name);
				}
			}
			foreach (Resource resource in directoryObjectAccessor.GetChanges<Resource>())
			{
				string name2 = resource.GetResourceType().Name;
				if (schema.Contains(name2))
				{
					requestSchema.Resources.Add(name2);
				}
			}
			foreach (RelationSet relationSet in directoryObjectAccessor.GetChanges<RelationSet>())
			{
				string name3 = relationSet.GetRelationType().Name;
				if (schema.Contains(name3))
				{
					requestSchema.Relations.Add(new RelationRequestSchema
					{
						Name = name3
					});
				}
			}
			return requestSchema;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		private static bool ContainsRelation(RequestSchema requestSchema, string relation)
		{
			return requestSchema.Relations.Any((RelationRequestSchema requestRelation) => StringComparer.OrdinalIgnoreCase.Equals(requestRelation.Name, relation));
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000DCB0 File Offset: 0x0000BEB0
		private static bool ContainsRelation(RequestSchema requestSchema, string[] relations)
		{
			return requestSchema.Relations.Any((RelationRequestSchema requestRelation) => relations.Any((string relation) => StringComparer.OrdinalIgnoreCase.Equals(requestRelation.Name, relation)));
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		private static bool ContainsResource(RequestSchema requestSchema, string[] resources)
		{
			return requestSchema.Resources.Any((string requestResource) => resources.Any((string resource) => StringComparer.OrdinalIgnoreCase.Equals(requestResource, resource)));
		}

		// Token: 0x0400056A RID: 1386
		private const string NewGroupMailboxTaskEventId = "PSDirectInvoke_NewGroupMailbox";

		// Token: 0x0400056B RID: 1387
		private static readonly HashSet<string> CreateGroupCommitDirectoryObjectSchema = new HashSet<string>(new string[]
		{
			"Alias",
			"Mail",
			"DisplayName",
			"Description",
			"Members",
			"Owners",
			"AllowAccessTo"
		}, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400056C RID: 1388
		private static readonly HashSet<string> CreateGroupNotifyChangesSchema = new HashSet<string>(new string[]
		{
			"SiteUrl"
		}, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400056D RID: 1389
		private static readonly HashSet<string> UpdateGroupCommitDirectoryObjectSchema = new HashSet<string>(new string[]
		{
			"Members",
			"Owners",
			"Membership"
		}, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400056E RID: 1390
		private static readonly HashSet<string> UpdateGroupNotifyChangesSchema = new HashSet<string>(new string[]
		{
			"Mail",
			"DisplayName",
			"Description",
			"SiteUrl"
		}, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400056F RID: 1391
		private static readonly string[] GetGroupMailboxRelations = new string[]
		{
			"Members",
			"Owners"
		};

		// Token: 0x04000570 RID: 1392
		private static readonly string[] GetGroupMailboxResources = new string[]
		{
			"PictureUrl",
			"InboxUrl",
			"CalendarUrl",
			"PeopleUrl"
		};

		// Token: 0x04000571 RID: 1393
		private static readonly string[] GetUserRelations = new string[]
		{
			"Membership"
		};

		// Token: 0x04000572 RID: 1394
		private static readonly string[] GetUserResources = new string[]
		{
			"PictureUrl"
		};

		// Token: 0x04000573 RID: 1395
		private static readonly string[] MailboxUrlResources = new string[]
		{
			"CalendarUrl",
			"InboxUrl",
			"PeopleUrl",
			"PictureUrl"
		};

		// Token: 0x04000574 RID: 1396
		private static readonly ADPropertyDefinition[] IdentityProperties = new ADPropertyDefinition[]
		{
			ADRecipientSchema.ExternalDirectoryObjectId
		};

		// Token: 0x0200006B RID: 107
		private sealed class NewGroupMailboxToString
		{
			// Token: 0x0600028A RID: 650 RVA: 0x0000DF20 File Offset: 0x0000C120
			public NewGroupMailboxToString(NewGroupMailbox cmdlet)
			{
				this.cmdlet = cmdlet;
			}

			// Token: 0x0600028B RID: 651 RVA: 0x0000DF30 File Offset: 0x0000C130
			public override string ToString()
			{
				ExchangeDirectoryAdaptor.CmdletStringBuilder cmdletStringBuilder = default(ExchangeDirectoryAdaptor.CmdletStringBuilder);
				cmdletStringBuilder.Append("New-GroupMailbox");
				cmdletStringBuilder.Append("ExecutingUser", this.cmdlet.ExecutingUser);
				cmdletStringBuilder.Append("Organization", this.cmdlet.Organization);
				cmdletStringBuilder.Append("Name", this.cmdlet.Name);
				cmdletStringBuilder.Append("ModernGroupType", this.cmdlet.ModernGroupType.ToString());
				cmdletStringBuilder.Append("Description", this.cmdlet.Description);
				cmdletStringBuilder.Append("Owners", this.cmdlet.Owners);
				cmdletStringBuilder.Append("Members", this.cmdlet.Members);
				return cmdletStringBuilder.ToString();
			}

			// Token: 0x04000575 RID: 1397
			private readonly NewGroupMailbox cmdlet;
		}

		// Token: 0x0200006C RID: 108
		private sealed class SetGroupMailboxToString
		{
			// Token: 0x0600028C RID: 652 RVA: 0x0000E009 File Offset: 0x0000C209
			public SetGroupMailboxToString(SetGroupMailbox cmdlet)
			{
				this.cmdlet = cmdlet;
			}

			// Token: 0x0600028D RID: 653 RVA: 0x0000E018 File Offset: 0x0000C218
			public override string ToString()
			{
				ExchangeDirectoryAdaptor.CmdletStringBuilder cmdletStringBuilder = default(ExchangeDirectoryAdaptor.CmdletStringBuilder);
				cmdletStringBuilder.Append("Set-GroupMailbox");
				cmdletStringBuilder.Append("ExecutingUser", this.cmdlet.ExecutingUser);
				cmdletStringBuilder.Append("Identity", this.cmdlet.Identity);
				cmdletStringBuilder.Append("Name", this.cmdlet.Name);
				cmdletStringBuilder.Append("DisplayName", this.cmdlet.DisplayName);
				cmdletStringBuilder.Append("Description", this.cmdlet.Description);
				cmdletStringBuilder.Append("SharePointUrl", this.cmdlet.SharePointUrl);
				cmdletStringBuilder.Append("Owners", this.cmdlet.Owners);
				cmdletStringBuilder.Append("AddOwners", this.cmdlet.AddOwners);
				cmdletStringBuilder.Append("RemoveOwners", this.cmdlet.RemoveOwners);
				cmdletStringBuilder.Append("AddedMembers", this.cmdlet.AddedMembers);
				cmdletStringBuilder.Append("RemovedMembers", this.cmdlet.RemovedMembers);
				if (this.cmdlet.SharePointResources != null)
				{
					StringBuilder stringBuilder = new StringBuilder(400);
					foreach (string value in this.cmdlet.SharePointResources)
					{
						stringBuilder.AppendLine(value);
					}
					cmdletStringBuilder.Append("SharePointResources", stringBuilder.ToString());
				}
				return cmdletStringBuilder.ToString();
			}

			// Token: 0x04000576 RID: 1398
			private readonly SetGroupMailbox cmdlet;
		}

		// Token: 0x0200006D RID: 109
		private sealed class RemoveGroupMailboxToString
		{
			// Token: 0x0600028E RID: 654 RVA: 0x0000E1BC File Offset: 0x0000C3BC
			public RemoveGroupMailboxToString(RemoveGroupMailbox cmdlet)
			{
				this.cmdlet = cmdlet;
			}

			// Token: 0x0600028F RID: 655 RVA: 0x0000E1CC File Offset: 0x0000C3CC
			public override string ToString()
			{
				ExchangeDirectoryAdaptor.CmdletStringBuilder cmdletStringBuilder = default(ExchangeDirectoryAdaptor.CmdletStringBuilder);
				cmdletStringBuilder.Append("Remove-GroupMailbox");
				cmdletStringBuilder.Append("ExecutingUser", this.cmdlet.ExecutingUser);
				cmdletStringBuilder.Append("Identity", this.cmdlet.Identity);
				return cmdletStringBuilder.ToString();
			}

			// Token: 0x04000577 RID: 1399
			private readonly RemoveGroupMailbox cmdlet;
		}

		// Token: 0x0200006E RID: 110
		private struct CmdletStringBuilder
		{
			// Token: 0x06000290 RID: 656 RVA: 0x0000E228 File Offset: 0x0000C428
			public void Append(string value)
			{
				this.InitializeIfNeeded();
				this.stringBuilder.Append(value);
			}

			// Token: 0x06000291 RID: 657 RVA: 0x0000E240 File Offset: 0x0000C440
			public void Append(string parameterName, string parameterValue)
			{
				this.InitializeIfNeeded();
				if (!string.IsNullOrEmpty(parameterValue))
				{
					this.stringBuilder.Append(string.Concat(new string[]
					{
						" -",
						parameterName,
						":'",
						parameterValue,
						"'"
					}));
				}
			}

			// Token: 0x06000292 RID: 658 RVA: 0x0000E294 File Offset: 0x0000C494
			public void Append(string parameterName, ADIdParameter parameterValue)
			{
				this.InitializeIfNeeded();
				if (parameterValue != null && !string.IsNullOrEmpty(parameterValue.RawIdentity))
				{
					this.stringBuilder.Append(string.Concat(new string[]
					{
						" -",
						parameterName,
						":'",
						parameterValue.RawIdentity,
						"'"
					}));
				}
			}

			// Token: 0x06000293 RID: 659 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
			public void Append(string parameterName, Uri parameterValue)
			{
				this.InitializeIfNeeded();
				if (parameterValue != null)
				{
					this.stringBuilder.Append(string.Concat(new string[]
					{
						" -",
						parameterName,
						":'",
						parameterValue.ToString(),
						"'"
					}));
				}
			}

			// Token: 0x06000294 RID: 660 RVA: 0x0000E354 File Offset: 0x0000C554
			public void Append(string parameterName, RecipientIdParameter[] ids)
			{
				this.InitializeIfNeeded();
				if (ids != null && ids.Length > 0)
				{
					this.stringBuilder.Append(" -" + parameterName + ":");
					bool flag = true;
					foreach (RecipientIdParameter recipientIdParameter in ids)
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							this.stringBuilder.Append(",");
						}
						this.stringBuilder.Append("'" + recipientIdParameter.RawIdentity + "'");
					}
				}
			}

			// Token: 0x06000295 RID: 661 RVA: 0x0000E3DB File Offset: 0x0000C5DB
			public override string ToString()
			{
				this.InitializeIfNeeded();
				return this.stringBuilder.ToString();
			}

			// Token: 0x06000296 RID: 662 RVA: 0x0000E3EE File Offset: 0x0000C5EE
			private void InitializeIfNeeded()
			{
				if (this.stringBuilder == null)
				{
					this.stringBuilder = new StringBuilder(256);
				}
			}

			// Token: 0x04000578 RID: 1400
			private StringBuilder stringBuilder;
		}

		// Token: 0x0200006F RID: 111
		private sealed class TaskOutputToString
		{
			// Token: 0x06000297 RID: 663 RVA: 0x0000E408 File Offset: 0x0000C608
			public TaskOutputToString(IList<PSLocalTaskIOData> container)
			{
				this.container = container;
			}

			// Token: 0x06000298 RID: 664 RVA: 0x0000E418 File Offset: 0x0000C618
			public override string ToString()
			{
				if (this.container != null)
				{
					StringBuilder stringBuilder = new StringBuilder(1000);
					stringBuilder.AppendLine("Output:");
					foreach (PSLocalTaskIOData pslocalTaskIOData in this.container)
					{
						stringBuilder.AppendLine(pslocalTaskIOData.ToString());
					}
					return stringBuilder.ToString();
				}
				return "No output";
			}

			// Token: 0x04000579 RID: 1401
			private readonly IList<PSLocalTaskIOData> container;
		}
	}
}
