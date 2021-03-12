using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000020 RID: 32
	[Cmdlet("Get", "SendAddress", DefaultParameterSetName = "Identity")]
	public sealed class GetSendAddress : GetTenantADObjectWithIdentityTaskBase<SendAddressIdParameter, SendAddress>
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000074E4 File Offset: 0x000056E4
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000074FB File Offset: 0x000056FB
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "LookUpId", ValueFromPipeline = true)]
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

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000750E File Offset: 0x0000570E
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00007525 File Offset: 0x00005725
		[Parameter(Mandatory = false, ParameterSetName = "LookUpId")]
		public string AddressId
		{
			get
			{
				return (string)base.Fields["AddressId"];
			}
			set
			{
				base.Fields["AddressId"] = value;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007538 File Offset: 0x00005738
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 73, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Aggregation\\GetSendAddress.cs");
			if (this.Identity != null && this.Identity.MailboxIdParameter != null)
			{
				this.Mailbox = this.Identity.MailboxIdParameter;
			}
			if (this.Mailbox == null)
			{
				this.WriteDebugInfoAndError(new MailboxParameterNotSpecifiedException(), ErrorCategory.InvalidData, this.Mailbox);
			}
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.Mailbox, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorUserNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(this.Mailbox.ToString())));
			ADSessionSettings adSettings = ADSessionSettings.RescopeToOrganization(base.SessionSettings, aduser.OrganizationId, true);
			try
			{
				this.userPrincipal = ExchangePrincipal.FromADUser(adSettings, aduser, RemotingOptions.AllowCrossSite);
			}
			catch (ObjectNotFoundException exception)
			{
				this.WriteDebugInfoAndError(exception, ErrorCategory.InvalidArgument, this.Mailbox);
			}
			SendAddressDataProvider result = null;
			try
			{
				result = new SendAddressDataProvider(this.userPrincipal, this.Mailbox.ToString());
			}
			catch (MailboxFailureException exception2)
			{
				this.WriteDebugInfoAndError(exception2, ErrorCategory.InvalidArgument, this.Mailbox);
			}
			return result;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007670 File Offset: 0x00005870
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			base.InternalStateReset();
			if (this.Identity == null)
			{
				if (this.AddressId != null)
				{
					SendAddressIdentity sendAddressIdentity = new SendAddressIdentity(this.Mailbox.ToString(), this.AddressId);
					this.Identity = new SendAddressIdParameter(sendAddressIdentity);
				}
				else
				{
					this.Identity = new SendAddressIdParameter();
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000076E0 File Offset: 0x000058E0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			try
			{
				if (this.Identity.IsUniqueIdentity)
				{
					this.WriteResult(base.GetDataObject(this.Identity));
				}
				else
				{
					IEnumerable<SendAddress> dataObjects = null;
					LocalizedString? localizedString = null;
					try
					{
						dataObjects = base.GetDataObjects(this.Identity, base.OptionalIdentityData, out localizedString);
					}
					catch (LocalizedException exception)
					{
						this.WriteDebugInfoAndError(exception, ErrorCategory.InvalidOperation, this.Mailbox);
					}
					this.WriteResult<SendAddress>(dataObjects);
					if (!base.HasErrors && base.WriteObjectCount == 0U && localizedString != null)
					{
						this.WriteDebugInfoAndError(new ManagementObjectNotFoundException(localizedString.Value), ErrorCategory.InvalidData, null);
					}
				}
			}
			finally
			{
				this.WriteDebugInfo();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000077B4 File Offset: 0x000059B4
		private void WriteDebugInfoAndError(Exception exception, ErrorCategory category, object target)
		{
			this.WriteDebugInfo();
			base.WriteError(exception, category, target);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000077C5 File Offset: 0x000059C5
		private void WriteDebugInfo()
		{
			if (base.IsDebugOn)
			{
				base.WriteDebug(CommonLoggingHelper.SyncLogSession.GetBlackBoxText());
			}
			CommonLoggingHelper.SyncLogSession.ClearBlackBox();
		}

		// Token: 0x04000073 RID: 115
		private const string LookUpIdParameterSet = "LookUpId";

		// Token: 0x04000074 RID: 116
		private ExchangePrincipal userPrincipal;
	}
}
