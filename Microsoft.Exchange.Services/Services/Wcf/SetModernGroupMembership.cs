using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.GroupMailbox.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.FederatedDirectory;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Office.Server.Directory;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000937 RID: 2359
	internal class SetModernGroupMembership : ServiceCommand<SetModernGroupMembershipResponse>
	{
		// Token: 0x0600445C RID: 17500 RVA: 0x000EA5BC File Offset: 0x000E87BC
		public SetModernGroupMembership(CallContext context, SetModernGroupMembershipJsonRequest request) : base(context)
		{
			this.request = request;
			this.request.Validate();
		}

		// Token: 0x0600445D RID: 17501 RVA: 0x000EA5D8 File Offset: 0x000E87D8
		protected override SetModernGroupMembershipResponse InternalExecute()
		{
			ExTraceGlobals.ModernGroupsTracer.TraceDebug<string, ModernGroupMembershipOperationType, SmtpAddress>((long)this.GetHashCode(), "SetModernGroupMembership.InternalExecute: Group:{0}, State:{1}, User:{2}.", this.request.GroupSmtpAddress, this.request.OperationType, base.CallContext.AccessingADUser.PrimarySmtpAddress);
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			GroupMailboxLocator groupMailboxLocator = GroupMailboxLocator.Instantiate(adrecipientSession, this.request.GroupProxyAddress);
			switch (this.request.OperationType)
			{
			case ModernGroupMembershipOperationType.Join:
			case ModernGroupMembershipOperationType.Leave:
				return this.SetUserMembership(adrecipientSession, groupMailboxLocator);
			case ModernGroupMembershipOperationType.Escalate:
			case ModernGroupMembershipOperationType.DeEscalate:
				return this.SetModernGroupEscalate(adrecipientSession, groupMailboxLocator);
			case ModernGroupMembershipOperationType.RequestJoin:
				return this.RequestJoin(adrecipientSession, groupMailboxLocator);
			default:
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException((CoreResources.IDs)3784063568U), FaultParty.Sender);
			}
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x000EA6A0 File Offset: 0x000E88A0
		private SetModernGroupMembershipResponse SetUserMembership(IRecipientSession adSession, GroupMailboxLocator groupMailboxLocator)
		{
			string text = base.CallContext.AccessingADUser.PrimarySmtpAddress.ToString();
			if (base.CallContext.FeaturesManager != null && base.CallContext.FeaturesManager.IsFeatureSupported("ModernGroupsNewArchitecture"))
			{
				ADUser aduser = groupMailboxLocator.FindAdUser();
				UpdateUnifiedGroupTask updateUnifiedGroupTask = new UpdateUnifiedGroupTask(base.CallContext.AccessingADUser, base.CallContext.AccessingPrincipal, base.CallContext.ADRecipientSessionContext.GetADRecipientSession());
				updateUnifiedGroupTask.ExternalDirectoryObjectId = aduser.ExternalDirectoryObjectId;
				if (this.request.OperationType == ModernGroupMembershipOperationType.Join)
				{
					if (aduser.ModernGroupType == ModernGroupObjectType.Private)
					{
						throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
					}
					updateUnifiedGroupTask.AddedMembers = new string[]
					{
						text
					};
				}
				else
				{
					updateUnifiedGroupTask.RemovedMembers = new string[]
					{
						text
					};
					if (aduser.Owners.Contains(base.CallContext.AccessingADUser.ObjectId, ADObjectIdEqualityComparer.Instance))
					{
						updateUnifiedGroupTask.RemovedOwners = updateUnifiedGroupTask.RemovedMembers;
					}
				}
				if (!updateUnifiedGroupTask.Run())
				{
					ExTraceGlobals.ModernGroupsTracer.TraceError<UnifiedGroupsTask.UnifiedGroupsAction, Exception>((long)this.GetHashCode(), "SetModernGroupMembership.InternalExecute: UpdateUnifiedGroupTask.Run failed. ErrorAction: {0}, ErrorException: {1}", updateUnifiedGroupTask.ErrorAction, updateUnifiedGroupTask.ErrorException);
					if (updateUnifiedGroupTask.ErrorAction == UnifiedGroupsTask.UnifiedGroupsAction.AADUpdate)
					{
						throw new InternalServerErrorException(updateUnifiedGroupTask.ErrorException);
					}
					if (updateUnifiedGroupTask.ErrorAction == UnifiedGroupsTask.UnifiedGroupsAction.ExchangeUpdate)
					{
						base.CallContext.ProtocolLog.Set(ServiceCommonMetadata.GenericErrors, updateUnifiedGroupTask.ErrorException);
						return new SetModernGroupMembershipResponse
						{
							ErrorState = UnifiedGroupResponseErrorState.FailedMailbox,
							Error = updateUnifiedGroupTask.ErrorException.ToString()
						};
					}
				}
			}
			else
			{
				using (new CorrelationContext())
				{
					IdentityMapping identityMapping = new IdentityMapping(adSession);
					identityMapping.Prefetch(new string[]
					{
						this.request.GroupSmtpAddress
					});
					identityMapping.Prefetch(new string[]
					{
						text
					});
					DirectorySession directorySession = FederatedDirectorySessionFactory.Create(base.CallContext.AccessingADUser, base.CallContext.AccessingPrincipal);
					RequestSchema requestSchema = new RequestSchema();
					Guid identityFromSmtpAddress = identityMapping.GetIdentityFromSmtpAddress(this.request.GroupSmtpAddress);
					if (identityFromSmtpAddress == Guid.Empty)
					{
						ExTraceGlobals.ModernGroupsTracer.TraceError<string>((long)this.GetHashCode(), "SetModernGroupMembership.InternalExecute: no group found with SMTP address: {0}.", this.request.GroupSmtpAddress);
						throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
					}
					Group group = directorySession.GetGroup(identityFromSmtpAddress, requestSchema);
					if (group == null)
					{
						ExTraceGlobals.ModernGroupsTracer.TraceError<Guid>((long)this.GetHashCode(), "SetModernGroupCommand.InternalExecute: no group found with ExternalDirectoryObjectId {0}", identityFromSmtpAddress);
						throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
					}
					if (this.request.OperationType == ModernGroupMembershipOperationType.Join)
					{
						if (!group.IsPublic)
						{
							throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
						}
						identityMapping.AddToRelation(new string[]
						{
							text
						}, group.Members);
					}
					else
					{
						identityMapping.RemoveFromRelation(new string[]
						{
							text
						}, group.Members);
						bool flag = groupMailboxLocator.FindAdUser().Owners.Contains(base.CallContext.AccessingADUser.ObjectId, ADObjectIdEqualityComparer.Instance);
						if (flag)
						{
							identityMapping.RemoveFromRelation(new string[]
							{
								text
							}, group.Owners);
						}
					}
					group.Commit();
				}
			}
			if (this.request.OperationType == ModernGroupMembershipOperationType.Join)
			{
				UserMailboxLocator userMailboxLocator = UserMailboxLocator.Instantiate(adSession, base.CallContext.AccessingADUser);
				JoinResponse joinInfo = new JoinResponse
				{
					IsSubscribed = this.IsJoiningMemberSubscribed(adSession, base.MailboxIdentityMailboxSession, userMailboxLocator, groupMailboxLocator)
				};
				return new SetModernGroupMembershipResponse
				{
					JoinInfo = joinInfo
				};
			}
			return new SetModernGroupMembershipResponse();
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x000EAAB4 File Offset: 0x000E8CB4
		private SetModernGroupMembershipResponse SetModernGroupEscalate(IRecipientSession adSession, GroupMailboxLocator groupMailboxLocator)
		{
			UserMailboxLocator userMailboxLocator = UserMailboxLocator.Instantiate(adSession, base.CallContext.AccessingADUser);
			SetModernGroupMembershipResponse result;
			try
			{
				GroupMailboxAccessLayer.Execute("SetModernGroupEscalate", adSession, base.MailboxIdentityMailboxSession, delegate(GroupMailboxAccessLayer accessLayer)
				{
					accessLayer.SetEscalate(userMailboxLocator, groupMailboxLocator, this.request.OperationType == ModernGroupMembershipOperationType.Escalate, this.CallContext.AccessingADUser.PrimarySmtpAddress, 400);
				});
				result = new SetModernGroupMembershipResponse();
			}
			catch (ExceededMaxSubscribersException)
			{
				result = new SetModernGroupMembershipResponse(ModernGroupActionError.MaxSubscriptionsForGroupReached);
			}
			return result;
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x000EAB34 File Offset: 0x000E8D34
		private SetModernGroupMembershipResponse RequestJoin(IRecipientSession adSession, GroupMailboxLocator groupMailboxLocator)
		{
			ADUser aduser = groupMailboxLocator.FindAdUser();
			if (aduser.ModernGroupType != ModernGroupObjectType.Private)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException((CoreResources.IDs)3784063568U), FaultParty.Sender);
			}
			GroupJoinRequestMessage.SendMessage(base.MailboxIdentityMailboxSession, aduser, this.request.AttachedMessage);
			return new SetModernGroupMembershipResponse();
		}

		// Token: 0x06004461 RID: 17505 RVA: 0x000EABA8 File Offset: 0x000E8DA8
		private bool IsJoiningMemberSubscribed(IRecipientSession adSession, MailboxSession groupMailboxSession, UserMailboxLocator userMailboxLocator, GroupMailboxLocator groupMailboxLocator)
		{
			UserMailbox userMailbox = null;
			if (groupMailboxLocator.FindAdUser().AutoSubscribeNewGroupMembers)
			{
				GroupMailboxAccessLayer.Execute("IsJoiningMemberSubscribed", adSession, groupMailboxSession, delegate(GroupMailboxAccessLayer accessLayer)
				{
					userMailbox = accessLayer.GetMember(groupMailboxLocator, userMailboxLocator, false);
				});
			}
			return userMailbox != null && userMailbox.ShouldEscalate;
		}

		// Token: 0x040027D6 RID: 10198
		private readonly SetModernGroupMembershipJsonRequest request;
	}
}
