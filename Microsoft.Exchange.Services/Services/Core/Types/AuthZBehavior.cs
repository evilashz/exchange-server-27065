using System;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006C6 RID: 1734
	internal class AuthZBehavior
	{
		// Token: 0x0600357B RID: 13691 RVA: 0x000BFE3A File Offset: 0x000BE03A
		public virtual bool IsAllowedToCallWebMethod(WebMethodEntry webMethodEntry)
		{
			return !webMethodEntry.IsPartnerAppOnly;
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000BFE45 File Offset: 0x000BE045
		public virtual bool IsAllowedToOpenMailbox(ExchangePrincipal mailboxToAccess)
		{
			return true;
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000BFE48 File Offset: 0x000BE048
		public virtual bool IsAllowedToPrivilegedOpenMailbox(ExchangePrincipal mailboxToAccess)
		{
			return false;
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000BFE4B File Offset: 0x000BE04B
		public virtual bool IsAllowedToOptimizeMoveCopyCommand()
		{
			return true;
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000BFE4E File Offset: 0x000BE04E
		public virtual RequestedLogonType GetMailboxLogonType()
		{
			return RequestedLogonType.Default;
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000BFE55 File Offset: 0x000BE055
		public virtual void OnCreateMessageItem(bool isAssociated)
		{
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000BFE57 File Offset: 0x000BE057
		public virtual void OnUpdateMessageItem(MessageItem messageItem)
		{
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000BFE59 File Offset: 0x000BE059
		public virtual void OnGetAttachment(StoreObjectId itemId)
		{
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000BFE5B File Offset: 0x000BE05B
		public virtual void OnGetItem(StoreObjectId itemId)
		{
		}

		// Token: 0x04001DF5 RID: 7669
		public static AuthZBehavior DefaultBehavior = new AuthZBehavior();

		// Token: 0x020006C7 RID: 1735
		internal sealed class PrivilegedSessionAuthZBehavior : AuthZBehavior
		{
			// Token: 0x06003586 RID: 13702 RVA: 0x000BFE71 File Offset: 0x000BE071
			public PrivilegedSessionAuthZBehavior(RequestedLogonType logonType)
			{
				this.requestedLogonType = logonType;
			}

			// Token: 0x06003587 RID: 13703 RVA: 0x000BFE80 File Offset: 0x000BE080
			public override bool IsAllowedToPrivilegedOpenMailbox(ExchangePrincipal mailboxToAccess)
			{
				return true;
			}

			// Token: 0x06003588 RID: 13704 RVA: 0x000BFE83 File Offset: 0x000BE083
			public override RequestedLogonType GetMailboxLogonType()
			{
				return this.requestedLogonType;
			}

			// Token: 0x04001DF6 RID: 7670
			private readonly RequestedLogonType requestedLogonType;
		}

		// Token: 0x020006C8 RID: 1736
		internal class AdminRoleUserAuthZBehavior : AuthZBehavior
		{
			// Token: 0x06003589 RID: 13705 RVA: 0x000BFE8C File Offset: 0x000BE08C
			public AdminRoleUserAuthZBehavior(AuthZClientInfo authZClientInfo)
			{
				SecurityIdentifier userSid = authZClientInfo.ClientSecurityContext.UserSid;
				SidWithGroupsIdentity sidIdentity = new SidWithGroupsIdentity(userSid.ToString(), string.Empty, authZClientInfo.ClientSecurityContext);
				this.runspaceConfig = AuthZBehavior.WebServiceRunspaceConfigurationCache.Singleton.Get(sidIdentity);
				if (this.runspaceConfig == null)
				{
					throw new ServiceAccessDeniedException(CoreResources.IDs.MessageUnableToLoadRBACSettings);
				}
				this.userRoleTypes = authZClientInfo.UserRoleTypes;
				foreach (RoleType roleType in this.userRoleTypes)
				{
					if (!this.runspaceConfig.HasRoleOfType(roleType))
					{
						ExTraceGlobals.AuthorizationTracer.TraceDebug<SecurityIdentifier, RoleType>(0L, "AdminRoleUserAuthZBehavior.ctor: user with sid {0} has no role {1} assigned.", userSid, roleType);
						throw new ServiceAccessDeniedException(CoreResources.IDs.MessageCallerHasNoAdminRoleGranted);
					}
				}
			}

			// Token: 0x0600358A RID: 13706 RVA: 0x000BFF68 File Offset: 0x000BE168
			public override bool IsAllowedToCallWebMethod(WebMethodEntry webMethodEntry)
			{
				string methodName = webMethodEntry.Name;
				return this.userRoleTypes.Any((RoleType roleType) => this.runspaceConfig.IsWebMethodInRole(methodName, roleType));
			}

			// Token: 0x0600358B RID: 13707 RVA: 0x000BFFA8 File Offset: 0x000BE1A8
			public override bool IsAllowedToPrivilegedOpenMailbox(ExchangePrincipal mailboxToAccess)
			{
				ADSessionSettings adsessionSettings = mailboxToAccess.MailboxInfo.OrganizationId.ToADSessionSettings();
				adsessionSettings.IncludeInactiveMailbox = true;
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, adsessionSettings, 133, "IsAllowedToPrivilegedOpenMailbox", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\Server\\Types\\AuthZBehavior.cs");
				foreach (RoleType roleType in this.userRoleTypes)
				{
					if (ExchangeRole.IsAdminRole(roleType) && this.runspaceConfig.IsTargetObjectInRoleScope(roleType, DirectoryHelper.ReadADRecipient(mailboxToAccess.MailboxInfo.MailboxGuid, mailboxToAccess.MailboxInfo.IsArchive, tenantOrRootOrgRecipientSession)))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600358C RID: 13708 RVA: 0x000C0048 File Offset: 0x000BE248
			public override RequestedLogonType GetMailboxLogonType()
			{
				foreach (RoleType roleToCheck in this.userRoleTypes)
				{
					if (ExchangeRole.IsAdminRole(roleToCheck))
					{
						return RequestedLogonType.AdminFromManagementRoleHeader;
					}
				}
				return base.GetMailboxLogonType();
			}

			// Token: 0x17000C63 RID: 3171
			// (get) Token: 0x0600358D RID: 13709 RVA: 0x000C0086 File Offset: 0x000BE286
			public RoleType[] UserRoleTypes
			{
				get
				{
					return this.userRoleTypes;
				}
			}

			// Token: 0x04001DF7 RID: 7671
			private readonly RoleType[] userRoleTypes;

			// Token: 0x04001DF8 RID: 7672
			private readonly WebServiceRunspaceConfiguration runspaceConfig;
		}

		// Token: 0x020006C9 RID: 1737
		internal class OfficeExtensionRoleAuthZBehavior : AuthZBehavior
		{
			// Token: 0x0600358E RID: 13710 RVA: 0x000C008E File Offset: 0x000BE28E
			public OfficeExtensionRoleAuthZBehavior(OAuthIdentity identity)
			{
				this.identity = identity;
			}

			// Token: 0x0600358F RID: 13711 RVA: 0x000C009D File Offset: 0x000BE29D
			public override bool IsAllowedToCallWebMethod(WebMethodEntry webMethodEntry)
			{
				return PartnerApplicationRunspaceConfiguration.IsWebMethodInOfficeExtensionRole(webMethodEntry.Name);
			}

			// Token: 0x06003590 RID: 13712 RVA: 0x000C00AC File Offset: 0x000BE2AC
			public override bool IsAllowedToOpenMailbox(ExchangePrincipal mailboxToAccess)
			{
				ExTraceGlobals.AuthorizationTracer.TraceDebug<SecurityIdentifier, SecurityIdentifier>(0L, "OfficeExtensionRoleAuthZBehavior.IsAllowedToOpenMailbox: act-as user sid {0}, mailbox-to-open sid {1}", this.identity.ActAsUser.Sid, mailboxToAccess.Sid);
				return this.identity.ActAsUser.Sid.Equals(mailboxToAccess.Sid);
			}

			// Token: 0x06003591 RID: 13713 RVA: 0x000C00FB File Offset: 0x000BE2FB
			public override void OnCreateMessageItem(bool isAssociated)
			{
				if (isAssociated)
				{
					ExTraceGlobals.AuthorizationTracer.TraceDebug(0L, "OfficeExtensionRoleAuthZBehavior.OnCreateMessageItem: trying to create FAI, throw");
					throw new ServiceAccessDeniedException(CoreResources.IDs.MessageExtensionNotAllowedToCreateFAI);
				}
			}

			// Token: 0x06003592 RID: 13714 RVA: 0x000C0124 File Offset: 0x000BE324
			public override void OnUpdateMessageItem(MessageItem messageItem)
			{
				MessageFlags messageFlags = (MessageFlags)messageItem[MessageItemSchema.Flags];
				if ((messageFlags & MessageFlags.IsAssociated) == MessageFlags.IsAssociated)
				{
					ExTraceGlobals.AuthorizationTracer.TraceDebug(0L, "OfficeExtensionRoleAuthZBehavior.OnUpdateMessageItem: trying to update FAI, throw");
					throw new ServiceAccessDeniedException(CoreResources.IDs.MessageExtensionNotAllowedToUpdateFAI);
				}
			}

			// Token: 0x06003593 RID: 13715 RVA: 0x000C016B File Offset: 0x000BE36B
			public override bool IsAllowedToOptimizeMoveCopyCommand()
			{
				return false;
			}

			// Token: 0x04001DF9 RID: 7673
			protected readonly OAuthIdentity identity;
		}

		// Token: 0x020006CA RID: 1738
		internal class ScopedOfficeExtensionRoleAuthZbehavior : AuthZBehavior.OfficeExtensionRoleAuthZBehavior
		{
			// Token: 0x06003594 RID: 13716 RVA: 0x000C016E File Offset: 0x000BE36E
			public ScopedOfficeExtensionRoleAuthZbehavior(OAuthIdentity identity) : base(identity)
			{
				if (!identity.IsOfficeExtension || !identity.OfficeExtension.IsScopedToken)
				{
					throw new ServiceAccessDeniedException((CoreResources.IDs)3579904699U);
				}
				this.scope = identity.OfficeExtension.Scope;
			}

			// Token: 0x06003595 RID: 13717 RVA: 0x000C01B0 File Offset: 0x000BE3B0
			public override bool IsAllowedToCallWebMethod(WebMethodEntry webMethodEntry)
			{
				bool flag = "GetAttachment".Equals(webMethodEntry.Name, StringComparison.OrdinalIgnoreCase) || "GetItem".Equals(webMethodEntry.Name, StringComparison.OrdinalIgnoreCase);
				if (!flag)
				{
					MSDiagnosticsHeader.AppendToResponse(OAuthErrorCategory.InvalidGrant, "The web method in the request is not allowed with this token.", null);
				}
				return flag;
			}

			// Token: 0x06003596 RID: 13718 RVA: 0x000C01F9 File Offset: 0x000BE3F9
			public override void OnGetAttachment(StoreObjectId itemId)
			{
				this.CheckItemIdMatch(itemId, "The specified attachment id does not correspond to an attachment on the item that the token is scoped for.");
			}

			// Token: 0x06003597 RID: 13719 RVA: 0x000C0207 File Offset: 0x000BE407
			public override void OnGetItem(StoreObjectId itemId)
			{
				this.CheckItemIdMatch(itemId, "The specified item id does not correspond to an item that the token is scoped for.");
			}

			// Token: 0x06003598 RID: 13720 RVA: 0x000C0218 File Offset: 0x000BE418
			private void CheckItemIdMatch(StoreObjectId itemId, string errorMessage)
			{
				try
				{
					if (this.scope.IndexOf("ParentItemId:", StringComparison.OrdinalIgnoreCase) < 0 || !IdConverter.EwsIdToMessageStoreObjectId(this.scope.Remove(0, "ParentItemId:".Length)).Equals(itemId))
					{
						MSDiagnosticsHeader.AppendToResponse(OAuthErrorCategory.InvalidGrant, errorMessage, null);
						throw new ServiceAccessDeniedException((CoreResources.IDs)3579904699U);
					}
				}
				catch (InvalidStoreIdException innerException)
				{
					MSDiagnosticsHeader.AppendToResponse(OAuthErrorCategory.InvalidGrant, errorMessage, null);
					throw new ServiceAccessDeniedException((CoreResources.IDs)3579904699U, innerException);
				}
			}

			// Token: 0x04001DFA RID: 7674
			private const string GetAttachmentWebMethodName = "GetAttachment";

			// Token: 0x04001DFB RID: 7675
			private const string GetItemWebMethodName = "GetItem";

			// Token: 0x04001DFC RID: 7676
			private const string ParentItemIdScopeName = "ParentItemId:";

			// Token: 0x04001DFD RID: 7677
			private readonly string scope;
		}

		// Token: 0x020006CB RID: 1739
		internal abstract class ApplicationAuthZBehaviorBase : AuthZBehavior
		{
			// Token: 0x06003599 RID: 13721 RVA: 0x000C02A8 File Offset: 0x000BE4A8
			public ApplicationAuthZBehaviorBase(OAuthIdentity identity, AuthZBehavior userAuthZBehavior, AuthZBehavior.ActAsUserRequirement actAsUserRequirement, RoleType applicationRoleType)
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest<AuthZBehavior.ActAsUserRequirement>(2328243517U, ref actAsUserRequirement);
				switch (actAsUserRequirement)
				{
				case AuthZBehavior.ActAsUserRequirement.MustPresent:
					if (identity.IsAppOnly)
					{
						throw new ServiceAccessDeniedException(CoreResources.IDs.MessageActAsUserRequiredForSuchApplicationRole);
					}
					break;
				case AuthZBehavior.ActAsUserRequirement.MustNotPresent:
					if (!identity.IsAppOnly)
					{
						throw new ServiceAccessDeniedException(CoreResources.IDs.MessageApplicationTokenOnly);
					}
					break;
				}
				this.oAuthIdentity = identity;
				this.applicationRoleType = applicationRoleType;
				this.userAuthZBehavior = userAuthZBehavior;
				this.applicationRunspaceConfig = AuthZBehavior.WebServiceRunspaceConfigurationCache.Singleton.Get(identity.OAuthApplication.PartnerApplication);
				if (this.applicationRunspaceConfig == null)
				{
					throw new ServiceAccessDeniedException(CoreResources.IDs.MessageUnableToLoadRBACSettings);
				}
				if (!this.applicationRunspaceConfig.HasRoleOfType(applicationRoleType))
				{
					throw new ServiceAccessDeniedException((applicationRoleType == RoleType.UserApplication) ? CoreResources.IDs.MessageApplicationHasNoUserApplicationRoleAssigned : ((CoreResources.IDs)3901728717U));
				}
				if (!this.IsAppAllowedToActAsUser())
				{
					throw new ServiceAccessDeniedException(CoreResources.IDs.MessageApplicationUnableActAsUser);
				}
				AuthZBehavior.AdminRoleUserAuthZBehavior adminRoleUserAuthZBehavior = userAuthZBehavior as AuthZBehavior.AdminRoleUserAuthZBehavior;
				if (adminRoleUserAuthZBehavior != null)
				{
					foreach (RoleType roleType in adminRoleUserAuthZBehavior.UserRoleTypes)
					{
						if (!this.applicationRunspaceConfig.HasRoleOfType(roleType))
						{
							throw new ServiceAccessDeniedException((CoreResources.IDs)3607262778U);
						}
					}
				}
			}

			// Token: 0x0600359A RID: 13722 RVA: 0x000C03E1 File Offset: 0x000BE5E1
			public override bool IsAllowedToCallWebMethod(WebMethodEntry webMethodEntry)
			{
				return this.applicationRunspaceConfig.IsWebMethodInRole(webMethodEntry.Name, this.applicationRoleType);
			}

			// Token: 0x0600359B RID: 13723
			public abstract bool IsAppAllowedToActAsUser();

			// Token: 0x04001DFE RID: 7678
			protected PartnerApplicationRunspaceConfiguration applicationRunspaceConfig;

			// Token: 0x04001DFF RID: 7679
			protected OAuthIdentity oAuthIdentity;

			// Token: 0x04001E00 RID: 7680
			protected RoleType applicationRoleType;

			// Token: 0x04001E01 RID: 7681
			protected AuthZBehavior userAuthZBehavior;
		}

		// Token: 0x020006CC RID: 1740
		internal sealed class UserApplicationRoleAuthZBehavior : AuthZBehavior.ApplicationAuthZBehaviorBase
		{
			// Token: 0x0600359C RID: 13724 RVA: 0x000C03FA File Offset: 0x000BE5FA
			public UserApplicationRoleAuthZBehavior(OAuthIdentity identity, AuthZBehavior userAuthZBehavior) : base(identity, userAuthZBehavior, AuthZBehavior.ActAsUserRequirement.MustPresent, RoleType.UserApplication)
			{
			}

			// Token: 0x0600359D RID: 13725 RVA: 0x000C0408 File Offset: 0x000BE608
			public override bool IsAppAllowedToActAsUser()
			{
				if (this.oAuthIdentity.IsAppOnly)
				{
					return true;
				}
				if (this.oAuthIdentity.ActAsUser.Sid != null)
				{
					return this.applicationRunspaceConfig.IsTargetObjectInRoleScope(this.applicationRoleType, this.oAuthIdentity.ADRecipient);
				}
				ExTraceGlobals.AuthorizationTracer.TraceDebug(0L, "UserApplicationRoleAuthZBehavior.IsAppAllowedToActAsUser: act-as user has no SID");
				return this.oAuthIdentity.IsKnownFromSameOrgExchange;
			}
		}

		// Token: 0x020006CD RID: 1741
		internal abstract class PrivilegedApplicationAuthZBehaviorBase : AuthZBehavior.ApplicationAuthZBehaviorBase
		{
			// Token: 0x0600359E RID: 13726 RVA: 0x000C0475 File Offset: 0x000BE675
			public PrivilegedApplicationAuthZBehaviorBase(OAuthIdentity identity, AuthZBehavior userAuthZBehavior, AuthZBehavior.ActAsUserRequirement actAsUserRequirement, RoleType applicationRoleType, RequestedLogonType requestedLogonType) : base(identity, userAuthZBehavior, actAsUserRequirement, applicationRoleType)
			{
				this.requestedLogonType = requestedLogonType;
			}

			// Token: 0x0600359F RID: 13727 RVA: 0x000C048A File Offset: 0x000BE68A
			public override RequestedLogonType GetMailboxLogonType()
			{
				return this.requestedLogonType;
			}

			// Token: 0x060035A0 RID: 13728 RVA: 0x000C0492 File Offset: 0x000BE692
			public override bool IsAppAllowedToActAsUser()
			{
				return true;
			}

			// Token: 0x060035A1 RID: 13729 RVA: 0x000C0498 File Offset: 0x000BE698
			public override bool IsAllowedToPrivilegedOpenMailbox(ExchangePrincipal mailboxToAccess)
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, mailboxToAccess.MailboxInfo.OrganizationId.ToADSessionSettings(), 552, "IsAllowedToPrivilegedOpenMailbox", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\Server\\Types\\AuthZBehavior.cs");
				return this.applicationRunspaceConfig.IsTargetObjectInRoleScope(this.applicationRoleType, DirectoryHelper.ReadADRecipient(mailboxToAccess.MailboxInfo.MailboxGuid, mailboxToAccess.MailboxInfo.IsArchive, tenantOrRootOrgRecipientSession));
			}

			// Token: 0x04001E02 RID: 7682
			protected RequestedLogonType requestedLogonType;
		}

		// Token: 0x020006CE RID: 1742
		internal sealed class LegalHoldApplicationRoleAuthZBehavior : AuthZBehavior.PrivilegedApplicationAuthZBehaviorBase
		{
			// Token: 0x060035A2 RID: 13730 RVA: 0x000C0500 File Offset: 0x000BE700
			public LegalHoldApplicationRoleAuthZBehavior(OAuthIdentity identity, AuthZBehavior userAuthZBehavior) : base(identity, userAuthZBehavior, AuthZBehavior.ActAsUserRequirement.MustNotPresent, RoleType.LegalHoldApplication, RequestedLogonType.AdminFromManagementRoleHeader)
			{
			}
		}

		// Token: 0x020006CF RID: 1743
		internal sealed class MailboxSearchApplicationRoleAuthZBehavior : AuthZBehavior.PrivilegedApplicationAuthZBehaviorBase
		{
			// Token: 0x060035A3 RID: 13731 RVA: 0x000C0512 File Offset: 0x000BE712
			public MailboxSearchApplicationRoleAuthZBehavior(OAuthIdentity identity, AuthZBehavior userAuthZBehavior) : base(identity, userAuthZBehavior, AuthZBehavior.ActAsUserRequirement.MustNotPresent, RoleType.MailboxSearchApplication, RequestedLogonType.AdminFromManagementRoleHeader)
			{
			}
		}

		// Token: 0x020006D0 RID: 1744
		internal sealed class ArchiveApplicationRoleAuthZBehavior : AuthZBehavior.PrivilegedApplicationAuthZBehaviorBase
		{
			// Token: 0x060035A4 RID: 13732 RVA: 0x000C0524 File Offset: 0x000BE724
			public ArchiveApplicationRoleAuthZBehavior(OAuthIdentity identity, AuthZBehavior userAuthZBehavior) : base(identity, userAuthZBehavior, AuthZBehavior.ActAsUserRequirement.MustPresent, RoleType.ArchiveApplication, RequestedLogonType.SystemServiceFromManagementRoleHeader)
			{
			}
		}

		// Token: 0x020006D1 RID: 1745
		internal sealed class MailboxSearchRoleAuthZBehavior : AuthZBehavior.PrivilegedApplicationAuthZBehaviorBase
		{
			// Token: 0x060035A5 RID: 13733 RVA: 0x000C0536 File Offset: 0x000BE736
			public MailboxSearchRoleAuthZBehavior(OAuthIdentity identity, AuthZBehavior userAuthZBehavior) : base(identity, userAuthZBehavior, AuthZBehavior.ActAsUserRequirement.MustPresent, RoleType.MailboxSearch, RequestedLogonType.AdminFromManagementRoleHeader)
			{
				this.adminRoleUserAuthZBehavior = (userAuthZBehavior as AuthZBehavior.AdminRoleUserAuthZBehavior);
			}

			// Token: 0x060035A6 RID: 13734 RVA: 0x000C0554 File Offset: 0x000BE754
			public override bool IsAllowedToPrivilegedOpenMailbox(ExchangePrincipal mailboxToAccess)
			{
				return base.IsAllowedToPrivilegedOpenMailbox(mailboxToAccess) && this.adminRoleUserAuthZBehavior.IsAllowedToPrivilegedOpenMailbox(mailboxToAccess);
			}

			// Token: 0x04001E03 RID: 7683
			private AuthZBehavior.AdminRoleUserAuthZBehavior adminRoleUserAuthZBehavior;
		}

		// Token: 0x020006D2 RID: 1746
		internal sealed class LegalHoldRoleAuthZBehavior : AuthZBehavior.PrivilegedApplicationAuthZBehaviorBase
		{
			// Token: 0x060035A7 RID: 13735 RVA: 0x000C056D File Offset: 0x000BE76D
			public LegalHoldRoleAuthZBehavior(OAuthIdentity identity, AuthZBehavior userAuthZBehavior) : base(identity, userAuthZBehavior, AuthZBehavior.ActAsUserRequirement.MustPresent, RoleType.LegalHold, RequestedLogonType.AdminFromManagementRoleHeader)
			{
				this.adminRoleUserAuthZBehavior = (userAuthZBehavior as AuthZBehavior.AdminRoleUserAuthZBehavior);
			}

			// Token: 0x060035A8 RID: 13736 RVA: 0x000C058B File Offset: 0x000BE78B
			public override bool IsAllowedToPrivilegedOpenMailbox(ExchangePrincipal mailboxToAccess)
			{
				return base.IsAllowedToPrivilegedOpenMailbox(mailboxToAccess) && this.adminRoleUserAuthZBehavior.IsAllowedToPrivilegedOpenMailbox(mailboxToAccess);
			}

			// Token: 0x04001E04 RID: 7684
			private AuthZBehavior.AdminRoleUserAuthZBehavior adminRoleUserAuthZBehavior;
		}

		// Token: 0x020006D3 RID: 1747
		internal sealed class TeamMailboxLifecycleApplicationRoleAuthZBehavior : AuthZBehavior.PrivilegedApplicationAuthZBehaviorBase
		{
			// Token: 0x060035A9 RID: 13737 RVA: 0x000C05A4 File Offset: 0x000BE7A4
			public TeamMailboxLifecycleApplicationRoleAuthZBehavior(OAuthIdentity identity, AuthZBehavior userAuthZBehavior) : base(identity, userAuthZBehavior, AuthZBehavior.ActAsUserRequirement.MustNotPresent, RoleType.TeamMailboxLifecycleApplication, RequestedLogonType.Default)
			{
			}
		}

		// Token: 0x020006D4 RID: 1748
		internal sealed class ExchangeCrossServiceIntegrationRoleAuthZBehavior : AuthZBehavior.PrivilegedApplicationAuthZBehaviorBase
		{
			// Token: 0x060035AA RID: 13738 RVA: 0x000C05B6 File Offset: 0x000BE7B6
			public ExchangeCrossServiceIntegrationRoleAuthZBehavior(OAuthIdentity identity, AuthZBehavior userAuthZBehavior) : base(identity, userAuthZBehavior, AuthZBehavior.ActAsUserRequirement.Either, RoleType.ExchangeCrossServiceIntegration, RequestedLogonType.AdminFromManagementRoleHeader)
			{
			}
		}

		// Token: 0x020006D5 RID: 1749
		internal sealed class OrganizationConfigurationRoleAuthZBehavior : AuthZBehavior.PrivilegedApplicationAuthZBehaviorBase
		{
			// Token: 0x060035AB RID: 13739 RVA: 0x000C05C8 File Offset: 0x000BE7C8
			public OrganizationConfigurationRoleAuthZBehavior(OAuthIdentity identity, AuthZBehavior userAuthZBehavior) : base(identity, userAuthZBehavior, AuthZBehavior.ActAsUserRequirement.MustPresent, RoleType.OrganizationConfiguration, RequestedLogonType.AdminFromManagementRoleHeader)
			{
				this.adminRoleUserAuthZBehavior = (userAuthZBehavior as AuthZBehavior.AdminRoleUserAuthZBehavior);
			}

			// Token: 0x060035AC RID: 13740 RVA: 0x000C05E6 File Offset: 0x000BE7E6
			public override bool IsAllowedToPrivilegedOpenMailbox(ExchangePrincipal mailboxToAccess)
			{
				return base.IsAllowedToPrivilegedOpenMailbox(mailboxToAccess) && this.adminRoleUserAuthZBehavior.IsAllowedToPrivilegedOpenMailbox(mailboxToAccess);
			}

			// Token: 0x04001E05 RID: 7685
			private AuthZBehavior.AdminRoleUserAuthZBehavior adminRoleUserAuthZBehavior;
		}

		// Token: 0x020006D6 RID: 1750
		internal sealed class WebServiceRunspaceConfigurationCache : LazyLookupTimeoutCache<AuthZBehavior.WebServiceRunspaceConfigurationCache.CacheKey, WebServiceRunspaceConfiguration>
		{
			// Token: 0x060035AD RID: 13741 RVA: 0x000C05FF File Offset: 0x000BE7FF
			private WebServiceRunspaceConfigurationCache() : base(1, AuthZBehavior.WebServiceRunspaceConfigurationCache.cacheSize.Value, false, AuthZBehavior.WebServiceRunspaceConfigurationCache.cacheTimeToLive.Value)
			{
			}

			// Token: 0x17000C64 RID: 3172
			// (get) Token: 0x060035AE RID: 13742 RVA: 0x000C0620 File Offset: 0x000BE820
			public static AuthZBehavior.WebServiceRunspaceConfigurationCache Singleton
			{
				get
				{
					if (AuthZBehavior.WebServiceRunspaceConfigurationCache.singleton == null)
					{
						lock (AuthZBehavior.WebServiceRunspaceConfigurationCache.lockObj)
						{
							if (AuthZBehavior.WebServiceRunspaceConfigurationCache.singleton == null)
							{
								AuthZBehavior.WebServiceRunspaceConfigurationCache.singleton = new AuthZBehavior.WebServiceRunspaceConfigurationCache();
							}
						}
					}
					return AuthZBehavior.WebServiceRunspaceConfigurationCache.singleton;
				}
			}

			// Token: 0x060035AF RID: 13743 RVA: 0x000C0678 File Offset: 0x000BE878
			public PartnerApplicationRunspaceConfiguration Get(PartnerApplication partnerApplication)
			{
				return base.Get(new AuthZBehavior.WebServiceRunspaceConfigurationCache.PartnerApplicationRunspaceConfigurationCacheKey(partnerApplication)) as PartnerApplicationRunspaceConfiguration;
			}

			// Token: 0x060035B0 RID: 13744 RVA: 0x000C068B File Offset: 0x000BE88B
			public WebServiceRunspaceConfiguration Get(SidWithGroupsIdentity sidIdentity)
			{
				return base.Get(new AuthZBehavior.WebServiceRunspaceConfigurationCache.WebServiceRunspaceConfigurationCacheKey(sidIdentity));
			}

			// Token: 0x060035B1 RID: 13745 RVA: 0x000C069C File Offset: 0x000BE89C
			protected override WebServiceRunspaceConfiguration CreateOnCacheMiss(AuthZBehavior.WebServiceRunspaceConfigurationCache.CacheKey key, ref bool shouldAdd)
			{
				try
				{
					shouldAdd = true;
					return key.Create();
				}
				catch (CmdletAccessDeniedException ex)
				{
					ExTraceGlobals.AuthorizationTracer.TraceWarning<AuthZBehavior.WebServiceRunspaceConfigurationCache.CacheKey, CmdletAccessDeniedException>(0L, "WebServiceRunspaceConfigurationCache.CreateOnCacheMiss: hit CmdletAccessDeniedException for key {0}, exception detail: {1} ", key, ex);
					RequestDetailsLogger.Current.AppendGenericInfo("WsrcKey", key);
					RequestDetailsLogger.Current.AppendGenericInfo("WsrcException", ex);
				}
				shouldAdd = false;
				return null;
			}

			// Token: 0x04001E06 RID: 7686
			private static readonly object lockObj = new object();

			// Token: 0x04001E07 RID: 7687
			private static AuthZBehavior.WebServiceRunspaceConfigurationCache singleton = null;

			// Token: 0x04001E08 RID: 7688
			private static TimeSpanAppSettingsEntry cacheTimeToLive = new TimeSpanAppSettingsEntry("WebServiceRunspaceConfigurationCacheTimeToLive", TimeSpanUnit.Seconds, TimeSpan.FromMinutes(30.0), ExTraceGlobals.AuthorizationTracer);

			// Token: 0x04001E09 RID: 7689
			private static IntAppSettingsEntry cacheSize = new IntAppSettingsEntry("WebServiceRunspaceConfigurationCacheMaxItems", 50, ExTraceGlobals.AuthorizationTracer);

			// Token: 0x020006D7 RID: 1751
			internal abstract class CacheKey
			{
				// Token: 0x060035B3 RID: 13747
				public abstract WebServiceRunspaceConfiguration Create();
			}

			// Token: 0x020006D8 RID: 1752
			internal sealed class WebServiceRunspaceConfigurationCacheKey : AuthZBehavior.WebServiceRunspaceConfigurationCache.CacheKey
			{
				// Token: 0x060035B5 RID: 13749 RVA: 0x000C0762 File Offset: 0x000BE962
				public WebServiceRunspaceConfigurationCacheKey(SidWithGroupsIdentity sidIdentity)
				{
					this.identity = sidIdentity;
					this.keyString = AuthZBehavior.WebServiceRunspaceConfigurationCache.WebServiceRunspaceConfigurationCacheKey.sidPrefix + sidIdentity.Sid.ToString();
				}

				// Token: 0x060035B6 RID: 13750 RVA: 0x000C078C File Offset: 0x000BE98C
				public override WebServiceRunspaceConfiguration Create()
				{
					ExTraceGlobals.AuthorizationTracer.TraceDebug<IIdentity>(0L, "WebServiceRunspaceConfigurationCacheKey.Create: create runspace configuration for user identity {0}", this.identity);
					return new WebServiceRunspaceConfiguration(this.identity);
				}

				// Token: 0x060035B7 RID: 13751 RVA: 0x000C07B0 File Offset: 0x000BE9B0
				public override bool Equals(object obj)
				{
					AuthZBehavior.WebServiceRunspaceConfigurationCache.WebServiceRunspaceConfigurationCacheKey webServiceRunspaceConfigurationCacheKey = obj as AuthZBehavior.WebServiceRunspaceConfigurationCache.WebServiceRunspaceConfigurationCacheKey;
					return webServiceRunspaceConfigurationCacheKey != null && this.keyString.Equals(webServiceRunspaceConfigurationCacheKey.keyString);
				}

				// Token: 0x060035B8 RID: 13752 RVA: 0x000C07DA File Offset: 0x000BE9DA
				public override int GetHashCode()
				{
					return this.keyString.GetHashCode();
				}

				// Token: 0x060035B9 RID: 13753 RVA: 0x000C07E7 File Offset: 0x000BE9E7
				public override string ToString()
				{
					return this.keyString;
				}

				// Token: 0x04001E0A RID: 7690
				private static readonly string sidPrefix = "sid~";

				// Token: 0x04001E0B RID: 7691
				private readonly string keyString;

				// Token: 0x04001E0C RID: 7692
				private readonly IIdentity identity;
			}

			// Token: 0x020006D9 RID: 1753
			internal sealed class PartnerApplicationRunspaceConfigurationCacheKey : AuthZBehavior.WebServiceRunspaceConfigurationCache.CacheKey
			{
				// Token: 0x060035BB RID: 13755 RVA: 0x000C07FB File Offset: 0x000BE9FB
				public PartnerApplicationRunspaceConfigurationCacheKey(PartnerApplication partnerApplication)
				{
					this.partnerApplication = partnerApplication;
				}

				// Token: 0x060035BC RID: 13756 RVA: 0x000C080A File Offset: 0x000BEA0A
				public override WebServiceRunspaceConfiguration Create()
				{
					ExTraceGlobals.AuthorizationTracer.TraceDebug<PartnerApplication>(0L, "PartnerApplicationRunspaceConfigurationCacheKey.Create: create runspace configuration for partner application {0}", this.partnerApplication);
					return PartnerApplicationRunspaceConfiguration.Create(this.partnerApplication);
				}

				// Token: 0x060035BD RID: 13757 RVA: 0x000C0830 File Offset: 0x000BEA30
				public override bool Equals(object obj)
				{
					AuthZBehavior.WebServiceRunspaceConfigurationCache.PartnerApplicationRunspaceConfigurationCacheKey partnerApplicationRunspaceConfigurationCacheKey = obj as AuthZBehavior.WebServiceRunspaceConfigurationCache.PartnerApplicationRunspaceConfigurationCacheKey;
					return partnerApplicationRunspaceConfigurationCacheKey != null && this.partnerApplication.Id.Equals(partnerApplicationRunspaceConfigurationCacheKey.partnerApplication.Id);
				}

				// Token: 0x060035BE RID: 13758 RVA: 0x000C0864 File Offset: 0x000BEA64
				public override int GetHashCode()
				{
					return this.partnerApplication.Id.GetHashCode();
				}

				// Token: 0x060035BF RID: 13759 RVA: 0x000C0876 File Offset: 0x000BEA76
				public override string ToString()
				{
					return this.partnerApplication.Id.ToString();
				}

				// Token: 0x04001E0D RID: 7693
				private readonly PartnerApplication partnerApplication;
			}
		}

		// Token: 0x020006DA RID: 1754
		internal enum ActAsUserRequirement
		{
			// Token: 0x04001E0F RID: 7695
			MustPresent,
			// Token: 0x04001E10 RID: 7696
			MustNotPresent,
			// Token: 0x04001E11 RID: 7697
			Either
		}
	}
}
