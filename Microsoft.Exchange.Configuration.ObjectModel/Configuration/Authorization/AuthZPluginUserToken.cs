using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000213 RID: 531
	internal class AuthZPluginUserToken
	{
		// Token: 0x06001279 RID: 4729 RVA: 0x0003BA0E File Offset: 0x00039C0E
		internal AuthZPluginUserToken(DelegatedPrincipal delegatedPrincipal, ADRawEntry userEntry, Microsoft.Exchange.Configuration.Core.AuthenticationType authenticatedType, string defaultUserName)
		{
			this.DelegatedPrincipal = delegatedPrincipal;
			this.UserEntry = userEntry;
			this.AuthenticationType = authenticatedType;
			this.DefaultUserName = defaultUserName;
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0003BA33 File Offset: 0x00039C33
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x0003BA3B File Offset: 0x00039C3B
		internal DelegatedPrincipal DelegatedPrincipal { get; private set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0003BA44 File Offset: 0x00039C44
		internal bool IsDelegatedUser
		{
			get
			{
				return this.DelegatedPrincipal != null;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0003BA52 File Offset: 0x00039C52
		// (set) Token: 0x0600127E RID: 4734 RVA: 0x0003BA5A File Offset: 0x00039C5A
		internal virtual ADRawEntry UserEntry { get; private set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0003BA63 File Offset: 0x00039C63
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x0003BA6B File Offset: 0x00039C6B
		internal string DefaultUserName { get; private set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x0003BA74 File Offset: 0x00039C74
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x0003BA7C File Offset: 0x00039C7C
		internal Microsoft.Exchange.Configuration.Core.AuthenticationType AuthenticationType { get; private set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x0003BA88 File Offset: 0x00039C88
		internal virtual string UserName
		{
			get
			{
				if (this.userName == null)
				{
					this.userName = this.DefaultUserName;
					if (this.DelegatedPrincipal != null)
					{
						this.userName = this.DelegatedPrincipal.GetUserName();
						ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>(0L, "Generate username {0} for AuthZPluginUserToken using DelegatedPrincipal.", this.userName);
					}
					else
					{
						object obj = this.UserEntry[ADObjectSchema.Id];
						if (obj != null)
						{
							this.userName = obj.ToString();
							ADObjectId adobjectId = obj as ADObjectId;
							if (adobjectId != null)
							{
								this.userNameForLogging = adobjectId.Name;
							}
							ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>(0L, "Generate username {0} for AuthZPluginUserToken using UserEntry.", this.userName);
						}
					}
				}
				return this.userName;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0003BB34 File Offset: 0x00039D34
		internal virtual string WindowsLiveId
		{
			get
			{
				if (!this.windowsLiveIdCalculated)
				{
					if (this.DelegatedPrincipal != null)
					{
						string userId = this.DelegatedPrincipal.UserId;
						if (SmtpAddress.IsValidSmtpAddress(userId))
						{
							this.windowsLiveId = userId;
						}
					}
					else if (this.UserEntry != null)
					{
						object obj = this.UserEntry[ADRecipientSchema.WindowsLiveID];
						this.windowsLiveId = ((obj == null) ? null : obj.ToString());
					}
					this.windowsLiveIdCalculated = true;
				}
				return this.windowsLiveId;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x0003BBA8 File Offset: 0x00039DA8
		internal virtual string OrgIdInString
		{
			get
			{
				if (!this.orgIdCalculated)
				{
					this.orgIdCalculated = true;
					this.CalculateOrgId();
				}
				if (!(this.orgId == null) && this.orgId.ConfigurationUnit != null)
				{
					return this.orgId.ConfigurationUnit.ToString();
				}
				return null;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0003BBF7 File Offset: 0x00039DF7
		internal virtual OrganizationId OrgId
		{
			get
			{
				if (!this.orgIdCalculated)
				{
					this.orgIdCalculated = true;
					this.CalculateOrgId();
				}
				return this.orgId;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0003BC14 File Offset: 0x00039E14
		internal string UserNameForLogging
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(this.WindowsLiveId))
				{
					return this.WindowsLiveId;
				}
				if (!string.IsNullOrWhiteSpace(this.userNameForLogging))
				{
					return this.userNameForLogging;
				}
				if (string.IsNullOrWhiteSpace(this.UserName))
				{
					return this.DefaultUserName;
				}
				if (!string.IsNullOrWhiteSpace(this.userNameForLogging))
				{
					return this.userNameForLogging;
				}
				return this.UserName;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0003BC78 File Offset: 0x00039E78
		internal virtual IList<string> DomainsToBlockTogether
		{
			get
			{
				if (this.domainsToBlockTogether == null)
				{
					OrganizationId organizationId = this.OrgId;
					IEnumerable<SmtpDomainWithSubdomains> enumerable = null;
					if (organizationId != null && organizationId.ConfigurationUnit != null)
					{
						enumerable = AuthZPluginHelper.GetAcceptedDomains(this.OrgId, this.OrgId);
					}
					this.domainsToBlockTogether = new List<string>();
					if (enumerable != null)
					{
						foreach (SmtpDomainWithSubdomains smtpDomainWithSubdomains in enumerable)
						{
							if (!smtpDomainWithSubdomains.IsStar && !smtpDomainWithSubdomains.IncludeSubDomains)
							{
								this.domainsToBlockTogether.Add(smtpDomainWithSubdomains.Domain);
							}
						}
					}
				}
				return this.domainsToBlockTogether;
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0003BD24 File Offset: 0x00039F24
		internal IThrottlingPolicy GetThrottlingPolicy()
		{
			using (IPowerShellBudget powerShellBudget = this.CreateBudget(BudgetType.PowerShell))
			{
				if (powerShellBudget != null)
				{
					return powerShellBudget.ThrottlingPolicy;
				}
			}
			if (this.UserEntry is MiniRecipient)
			{
				return (this.UserEntry as MiniRecipient).ReadThrottlingPolicy();
			}
			return (this.UserEntry as ADUser).ReadThrottlingPolicy();
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0003BD94 File Offset: 0x00039F94
		internal virtual IPowerShellBudget CreateBudget(BudgetType budgetType)
		{
			IPowerShellBudget result = null;
			if (this.DelegatedPrincipal != null)
			{
				ExTraceGlobals.PublicPluginAPITracer.TraceDebug<BudgetType, string>(0L, "Create Budge {0} for AuthZPluginUserToken {1} using DelegatedPrincipal.", budgetType, this.UserName);
				result = PowerShellBudget.Acquire(new DelegatedPrincipalBudgetKey(this.DelegatedPrincipal, budgetType));
			}
			else
			{
				ExTraceGlobals.PublicPluginAPITracer.TraceDebug<BudgetType, string>(0L, "Create Budge {0} for AuthZPluginUserToken {1} using UserEntry.", budgetType, this.UserName);
				if (budgetType == BudgetType.WSManTenant)
				{
					return PowerShellBudget.Acquire(new TenantBudgetKey(this.OrgId, budgetType));
				}
				SecurityIdentifier securityIdentifier = (SecurityIdentifier)this.UserEntry[IADSecurityPrincipalSchema.Sid];
				if (securityIdentifier != null)
				{
					ADObjectId rootOrgId;
					if (this.OrgId == null || this.OrgId.Equals(OrganizationId.ForestWideOrgId))
					{
						rootOrgId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
					}
					else
					{
						rootOrgId = ADSystemConfigurationSession.GetRootOrgContainerId(this.OrgId.PartitionId.ForestFQDN, null, null);
					}
					result = PowerShellBudget.Acquire(securityIdentifier, budgetType, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgId, this.OrgId, this.OrgId, true));
				}
				else
				{
					ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>(0L, "Sid is null, return null budget for AuthZPluginUserToken {0}.", this.UserName);
				}
			}
			return result;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0003BEA0 File Offset: 0x0003A0A0
		protected virtual void CalculateOrgId()
		{
			if (this.DelegatedPrincipal != null)
			{
				ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>(0L, "Create OrgId for AuthZPluginUserToken {0} using DelegatedPrincipal.", this.UserName);
				ExchangeAuthorizationPlugin.TryFindOrganizationIdForDelegatedPrincipal(this.DelegatedPrincipal, out this.orgId);
				return;
			}
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>(0L, "Create OrgId for AuthZPluginUserToken {0} from UserEntry.", this.UserName);
			this.orgId = (OrganizationId)this.UserEntry[ADObjectSchema.OrganizationId];
		}

		// Token: 0x0400047C RID: 1148
		protected string userName;

		// Token: 0x0400047D RID: 1149
		protected bool orgIdCalculated;

		// Token: 0x0400047E RID: 1150
		protected OrganizationId orgId;

		// Token: 0x0400047F RID: 1151
		protected string windowsLiveId;

		// Token: 0x04000480 RID: 1152
		protected bool windowsLiveIdCalculated;

		// Token: 0x04000481 RID: 1153
		protected IList<string> domainsToBlockTogether;

		// Token: 0x04000482 RID: 1154
		private string userNameForLogging;
	}
}
