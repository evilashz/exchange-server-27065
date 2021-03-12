using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000455 RID: 1109
	[Cmdlet("New", "SiteMailboxProvisioningPolicy", SupportsShouldProcess = true)]
	public sealed class NewSiteMailboxProvisioningPolicy : NewMailboxPolicyBase<TeamMailboxProvisioningPolicy>
	{
		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x0009B5B8 File Offset: 0x000997B8
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Static;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x0009B5CA File Offset: 0x000997CA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewTeamMailboxProvisioningPolicy(base.Name.ToString());
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06002741 RID: 10049 RVA: 0x0009B5DC File Offset: 0x000997DC
		// (set) Token: 0x06002742 RID: 10050 RVA: 0x0009B5E4 File Offset: 0x000997E4
		[Parameter(Mandatory = false)]
		public override SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x0009B5ED File Offset: 0x000997ED
		// (set) Token: 0x06002744 RID: 10052 RVA: 0x0009B613 File Offset: 0x00099813
		[Parameter(Mandatory = false)]
		public SwitchParameter IsDefault
		{
			get
			{
				return (SwitchParameter)(base.Fields["IsDefault"] ?? false);
			}
			set
			{
				base.Fields["IsDefault"] = value;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x0009B62B File Offset: 0x0009982B
		// (set) Token: 0x06002746 RID: 10054 RVA: 0x0009B638 File Offset: 0x00099838
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MaxReceiveSize
		{
			get
			{
				return this.DataObject.MaxReceiveSize;
			}
			set
			{
				this.DataObject.MaxReceiveSize = value;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x0009B646 File Offset: 0x00099846
		// (set) Token: 0x06002748 RID: 10056 RVA: 0x0009B653 File Offset: 0x00099853
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize IssueWarningQuota
		{
			get
			{
				return this.DataObject.IssueWarningQuota;
			}
			set
			{
				this.DataObject.IssueWarningQuota = value;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x0009B661 File Offset: 0x00099861
		// (set) Token: 0x0600274A RID: 10058 RVA: 0x0009B66E File Offset: 0x0009986E
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ProhibitSendReceiveQuota
		{
			get
			{
				return this.DataObject.ProhibitSendReceiveQuota;
			}
			set
			{
				this.DataObject.ProhibitSendReceiveQuota = value;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x0009B67C File Offset: 0x0009987C
		// (set) Token: 0x0600274C RID: 10060 RVA: 0x0009B689 File Offset: 0x00099889
		[Parameter(Mandatory = false)]
		public bool DefaultAliasPrefixEnabled
		{
			get
			{
				return this.DataObject.DefaultAliasPrefixEnabled;
			}
			set
			{
				this.DataObject.DefaultAliasPrefixEnabled = value;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x0009B697 File Offset: 0x00099897
		// (set) Token: 0x0600274E RID: 10062 RVA: 0x0009B6A4 File Offset: 0x000998A4
		[Parameter(Mandatory = false)]
		public string AliasPrefix
		{
			get
			{
				return this.DataObject.AliasPrefix;
			}
			set
			{
				this.DataObject.AliasPrefix = value;
			}
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x0009B6B4 File Offset: 0x000998B4
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.IsDefault)
			{
				this.DataObject.IsDefault = true;
				this.existingDefaultPolicies = DefaultTeamMailboxProvisioningPolicyUtility.GetDefaultPolicies((IConfigurationSession)base.DataSession);
				if (this.existingDefaultPolicies != null && this.existingDefaultPolicies.Count > 0)
				{
					this.updateExistingDefaultPolicies = true;
				}
			}
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x0009B714 File Offset: 0x00099914
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.DataObject != null && SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			if (this.updateExistingDefaultPolicies)
			{
				try
				{
					DefaultMailboxPolicyUtility<TeamMailboxProvisioningPolicy>.ClearDefaultPolicies(base.DataSession as IConfigurationSession, this.existingDefaultPolicies);
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, ErrorCategory.ReadError, null);
				}
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
