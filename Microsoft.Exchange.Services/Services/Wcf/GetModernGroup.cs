using System;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.FederatedDirectory;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000920 RID: 2336
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GetModernGroup : ServiceCommand<GetModernGroupResponse>
	{
		// Token: 0x060043B2 RID: 17330 RVA: 0x000E5B0F File Offset: 0x000E3D0F
		public GetModernGroup(CallContext context, GetModernGroupRequest request) : base(context)
		{
			this.request = request;
			OwsLogRegistry.Register(GetModernGroup.GetModernGroupActionName, typeof(GetModernGroupMetadata), new Type[0]);
			request.ValidateRequest();
			WarmupGroupManagementDependency.WarmUpAsyncIfRequired(base.CallContext.AccessingPrincipal);
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x060043B3 RID: 17331 RVA: 0x000E5B4F File Offset: 0x000E3D4F
		private IRecipientSession ADSession
		{
			get
			{
				return base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x060043B4 RID: 17332 RVA: 0x000E5B61 File Offset: 0x000E3D61
		private UserMailboxLocator UserMailboxLocator
		{
			get
			{
				if (this.userMailboxLocator == null)
				{
					this.userMailboxLocator = UserMailboxLocator.Instantiate(this.ADSession, base.CallContext.AccessingADUser);
				}
				return this.userMailboxLocator;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x060043B5 RID: 17333 RVA: 0x000E5B8D File Offset: 0x000E3D8D
		private GroupMailboxLocator GroupMailboxLocator
		{
			get
			{
				if (this.groupMailboxLocator == null)
				{
					this.groupMailboxLocator = GroupMailboxLocator.Instantiate(this.ADSession, this.request.ProxyAddress);
				}
				return this.groupMailboxLocator;
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x060043B6 RID: 17334 RVA: 0x000E5BB9 File Offset: 0x000E3DB9
		private GroupMailbox GroupMailbox
		{
			get
			{
				if (this.groupMailbox == null)
				{
					this.groupMailbox = this.GetGroup();
				}
				return this.groupMailbox;
			}
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x000E5BD8 File Offset: 0x000E3DD8
		protected override GetModernGroupResponse InternalExecute()
		{
			GetModernGroupResponse getModernGroupResponse = new GetModernGroupResponse();
			if (this.request.IsGeneralInfoRequested)
			{
				base.CallContext.ProtocolLog.Set(GetModernGroupMetadata.GeneralInfo, ExtensibleLogger.FormatPIIValue(this.request.SmtpAddress));
				getModernGroupResponse.GeneralInfo = this.GetGeneralInfo();
			}
			if (this.request.IsMemberRequested || this.request.IsOwnerListRequested)
			{
				GetGroupMembers getGroupMembers = new GetGroupMembers(this.ADSession, base.CallContext.AccessingADUser.OrganizationId, this.GroupMailboxLocator, StoreSessionCacheBase.BuildMapiApplicationId(base.CallContext, null), this.request.MemberSortOrder, (this.request.MembersPageRequest != null) ? this.request.MembersPageRequest.MaxRows : 100, this.request.SerializedPeopleIKnowGraph, base.CallContext.ProtocolLog);
				if (this.request.IsOwnerListRequested)
				{
					base.CallContext.ProtocolLog.Set(GetModernGroupMetadata.OwnerList, ExtensibleLogger.FormatPIIValue(this.request.SmtpAddress));
					getModernGroupResponse.OwnerList = getGroupMembers.GetOwners();
				}
				if (this.request.IsMemberRequested)
				{
					getModernGroupResponse.MembersInfo = getGroupMembers.GetMembers();
				}
			}
			if (this.request.IsExternalResourcesRequested)
			{
				getModernGroupResponse.ExternalResources = this.GetExternalResources();
			}
			if (this.request.IsMailboxInfoRequested)
			{
				getModernGroupResponse.MailboxProperties = this.GetGroupProperties();
			}
			return getModernGroupResponse;
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x000E5D44 File Offset: 0x000E3F44
		private ModernGroupExternalResources GetExternalResources()
		{
			SharePointUrlResolver sharePointUrlResolver = new SharePointUrlResolver(this.GroupMailboxLocator.FindAdUser());
			return new ModernGroupExternalResources
			{
				SharePointUrl = sharePointUrlResolver.GetSiteUrl(),
				DocumentsUrl = sharePointUrlResolver.GetDocumentsUrl()
			};
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x000E5DB0 File Offset: 0x000E3FB0
		private GroupMailboxProperties GetGroupProperties()
		{
			int subscribersCount = 100;
			this.ExecuteGroupMailboxAction("GetEscalatedMembers", delegate(GroupMailboxAccessLayer accessLayer)
			{
				subscribersCount = accessLayer.GetEscalatedMembers(this.GroupMailboxLocator, false).ToArray<UserMailbox>().Count<UserMailbox>();
			});
			return new GroupMailboxProperties
			{
				SubscribersCount = subscribersCount,
				CanUpdateAutoSubscribeFlag = (subscribersCount < 100),
				LanguageLCID = ((this.GroupMailbox.Language != null) ? this.GroupMailbox.Language.LCID : -1)
			};
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x000E5E34 File Offset: 0x000E4034
		private ModernGroupGeneralInfoResponse GetGeneralInfo()
		{
			string description = this.GroupMailbox.Description;
			UserMailbox member = this.GetMember();
			bool flag = member != null && member.IsMember;
			return new ModernGroupGeneralInfoResponse
			{
				Description = description,
				IsMember = flag,
				IsOwner = (flag && member.IsOwner),
				ModernGroupType = this.GroupMailbox.Type,
				Name = this.GroupMailbox.DisplayName,
				OwnersCount = this.GroupMailbox.Owners.Count,
				ShouldEscalate = (flag && member.ShouldEscalate),
				SmtpAddress = this.GroupMailbox.SmtpAddress.ToString(),
				RequireSenderAuthenticationEnabled = this.GroupMailbox.RequireSenderAuthenticationEnabled,
				AutoSubscribeNewGroupMembers = this.GroupMailbox.AutoSubscribeNewGroupMembers
			};
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x000E5F44 File Offset: 0x000E4144
		private UserMailbox GetMember()
		{
			UserMailbox user = null;
			this.ExecuteGroupMailboxAction("GetModernGroupMember", delegate(GroupMailboxAccessLayer accessLayer)
			{
				user = accessLayer.GetMember(this.GroupMailboxLocator, this.UserMailboxLocator, true);
			});
			return user;
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x000E5FB0 File Offset: 0x000E41B0
		private GroupMailbox GetGroup()
		{
			GroupMailbox group = null;
			this.ExecuteGroupMailboxAction("GetModernGroupDetails", delegate(GroupMailboxAccessLayer accessLayer)
			{
				group = accessLayer.GetGroupMailbox(this.GroupMailboxLocator, this.UserMailboxLocator, true);
			});
			return group;
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x000E5FEE File Offset: 0x000E41EE
		private void ExecuteGroupMailboxAction(string operationDescription, Action<GroupMailboxAccessLayer> action)
		{
			GroupMailboxAccessLayer.Execute(operationDescription, this.ADSession, this.GroupMailboxLocator.MailboxGuid, base.CallContext.AccessingADUser.OrganizationId, StoreSessionCacheBase.BuildMapiApplicationId(base.CallContext, null), action);
		}

		// Token: 0x04002781 RID: 10113
		private const int MaxMembers = 100;

		// Token: 0x04002782 RID: 10114
		private static readonly string GetModernGroupActionName = typeof(GetModernGroup).Name;

		// Token: 0x04002783 RID: 10115
		private readonly GetModernGroupRequest request;

		// Token: 0x04002784 RID: 10116
		private GroupMailbox groupMailbox;

		// Token: 0x04002785 RID: 10117
		private UserMailboxLocator userMailboxLocator;

		// Token: 0x04002786 RID: 10118
		private GroupMailboxLocator groupMailboxLocator;
	}
}
