using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005D9 RID: 1497
	[Cmdlet("Test", "OAuthConnectivity", SupportsShouldProcess = true)]
	public sealed class TestOAuthConnectivity : DataAccessTask<ADUser>
	{
		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x000D8D0C File Offset: 0x000D6F0C
		// (set) Token: 0x060034F0 RID: 13552 RVA: 0x000D8D14 File Offset: 0x000D6F14
		[Parameter(Mandatory = false)]
		public SwitchParameter AppOnly { get; set; }

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x060034F1 RID: 13553 RVA: 0x000D8D1D File Offset: 0x000D6F1D
		// (set) Token: 0x060034F2 RID: 13554 RVA: 0x000D8D25 File Offset: 0x000D6F25
		[Parameter(Mandatory = false)]
		public SwitchParameter UseCachedToken { get; set; }

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x060034F3 RID: 13555 RVA: 0x000D8D2E File Offset: 0x000D6F2E
		// (set) Token: 0x060034F4 RID: 13556 RVA: 0x000D8D36 File Offset: 0x000D6F36
		[Parameter(Mandatory = false)]
		public SwitchParameter ReloadConfig { get; set; }

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x060034F5 RID: 13557 RVA: 0x000D8D3F File Offset: 0x000D6F3F
		// (set) Token: 0x060034F6 RID: 13558 RVA: 0x000D8D56 File Offset: 0x000D6F56
		[Parameter(Mandatory = true)]
		public ModServiceType Service
		{
			get
			{
				return (ModServiceType)base.Fields["Service"];
			}
			set
			{
				base.Fields["Service"] = value;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x000D8D6E File Offset: 0x000D6F6E
		// (set) Token: 0x060034F8 RID: 13560 RVA: 0x000D8D85 File Offset: 0x000D6F85
		[Parameter(Mandatory = true)]
		public Uri TargetUri
		{
			get
			{
				return (Uri)base.Fields["TargetUri"];
			}
			set
			{
				base.Fields["TargetUri"] = value;
			}
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x000D8D98 File Offset: 0x000D6F98
		// (set) Token: 0x060034FA RID: 13562 RVA: 0x000D8DAF File Offset: 0x000D6FAF
		[Parameter(Mandatory = false)]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x000D8DC2 File Offset: 0x000D6FC2
		// (set) Token: 0x060034FC RID: 13564 RVA: 0x000D8DD9 File Offset: 0x000D6FD9
		[Parameter(Mandatory = false)]
		public string OrganizationDomain
		{
			get
			{
				return (string)base.Fields["OrganizationDomain"];
			}
			set
			{
				base.Fields["OrganizationDomain"] = value;
			}
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000D8DEC File Offset: 0x000D6FEC
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.ExecutingUserOrganizationId, base.ExecutingUserOrganizationId, true), 115, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Tasks\\TestOAuthConnectivity.cs");
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000D8E20 File Offset: 0x000D7020
		protected override void InternalProcessRecord()
		{
			ADUser aduser = null;
			string empty = string.Empty;
			ResultType type = ResultType.Success;
			if (this.Mailbox != null)
			{
				aduser = (ADUser)base.GetDataObject(this.Mailbox);
				if (aduser == null && !this.AppOnly)
				{
					base.ThrowTerminatingError(new MailboxUserNotFoundException(this.Mailbox.ToString()), ErrorCategory.ObjectNotFound, null);
				}
			}
			if (this.AppOnly)
			{
				if (this.Mailbox == null && string.IsNullOrEmpty(this.OrganizationDomain))
				{
					base.ThrowTerminatingError(new NoUserOrOrganiztionProvidedException(), ErrorCategory.ObjectNotFound, null);
				}
				if (this.Service == ModServiceType.EWS)
				{
					base.ThrowTerminatingError(new EwsNotSupportedException(), ErrorCategory.NotEnabled, null);
				}
			}
			else if (this.Mailbox == null)
			{
				base.ThrowTerminatingError(new MailboxParameterMissingException(), ErrorCategory.ObjectNotFound, null);
			}
			switch (this.Service)
			{
			case ModServiceType.EWS:
				type = TestOAuthConnectivityHelper.SendExchangeOAuthRequest(aduser, this.OrganizationDomain, this.TargetUri, out empty, this.AppOnly, this.UseCachedToken, this.ReloadConfig);
				break;
			case ModServiceType.AutoD:
				type = TestOAuthConnectivityHelper.SendAutodiscoverOAuthRequest(aduser, this.OrganizationDomain, this.TargetUri, out empty, this.AppOnly, this.UseCachedToken, this.ReloadConfig);
				break;
			case ModServiceType.Generic:
				type = TestOAuthConnectivityHelper.SendGenericOAuthRequest(aduser, this.OrganizationDomain, this.TargetUri, out empty, this.AppOnly, this.UseCachedToken, this.ReloadConfig);
				break;
			}
			ValidationResultNode sendToPipeline = new ValidationResultNode(Strings.TestApiCallUnderOauthTask(this.Service.ToString()), new LocalizedString(empty), type);
			base.WriteObject(sendToPipeline);
		}
	}
}
