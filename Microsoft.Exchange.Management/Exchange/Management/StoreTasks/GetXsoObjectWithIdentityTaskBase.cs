using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000794 RID: 1940
	public abstract class GetXsoObjectWithIdentityTaskBase<TDataObject, TDirectoryObject> : GetRecipientObjectTask<MailboxIdParameter, TDirectoryObject> where TDataObject : IConfigurable, new() where TDirectoryObject : ADRecipient, new()
	{
		// Token: 0x0600445A RID: 17498 RVA: 0x00118643 File Offset: 0x00116843
		public GetXsoObjectWithIdentityTaskBase()
		{
		}

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x0011864B File Offset: 0x0011684B
		// (set) Token: 0x0600445C RID: 17500 RVA: 0x00118653 File Offset: 0x00116853
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0, ParameterSetName = "Identity")]
		public override MailboxIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x0011865C File Offset: 0x0011685C
		protected virtual bool ShouldProcessArchive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x0011865F File Offset: 0x0011685F
		protected override void WriteResult(IConfigurable dataObject)
		{
			if (dataObject is ADUser || dataObject is ADSystemMailbox)
			{
				base.WriteResult(dataObject);
			}
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x00118678 File Offset: 0x00116878
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			ExchangePrincipal exchangePrincipal = this.GetExchangePrincipal(dataObject);
			if (exchangePrincipal != null)
			{
				using (XsoMailboxDataProviderBase xsoMailboxDataProviderBase = (XsoMailboxDataProviderBase)this.CreateXsoMailboxDataProvider(exchangePrincipal, (base.ExchangeRunspaceConfig == null) ? null : base.ExchangeRunspaceConfig.SecurityAccessToken))
				{
					return xsoMailboxDataProviderBase.Read<TDataObject>(dataObject.Identity);
				}
			}
			return null;
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x001186E4 File Offset: 0x001168E4
		private ExchangePrincipal GetExchangePrincipal(IConfigurable dataObject)
		{
			ADUser aduser = dataObject as ADUser;
			if (aduser != null)
			{
				if (!this.ShouldProcessArchive)
				{
					if (aduser.RecipientType == RecipientType.MailUser)
					{
						base.WriteError(new InvalidOperationException(Strings.RecipientTypeNotValid(this.Identity.ToString())), ErrorCategory.InvalidOperation, null);
					}
					return XsoStoreDataProviderBase.GetExchangePrincipalWithAdSessionSettingsForOrg(base.SessionSettings.CurrentOrganizationId, aduser);
				}
				if (aduser.ArchiveState != ArchiveState.None)
				{
					return ExchangePrincipal.FromMailboxGuid(base.SessionSettings, aduser.ArchiveGuid, RemotingOptions.AllowCrossSite | RemotingOptions.AllowCrossPremise, null);
				}
				base.WriteError(new InvalidOperationException(Strings.VerboseArchiveNotExistInStore(aduser.DisplayName)), ErrorCategory.InvalidOperation, null);
			}
			ADSystemMailbox adsystemMailbox = dataObject as ADSystemMailbox;
			if (adsystemMailbox != null)
			{
				return ExchangePrincipal.FromADSystemMailbox(base.SessionSettings, adsystemMailbox, ((ITopologyConfigurationSession)this.ConfigurationSession).FindServerByLegacyDN(adsystemMailbox.ServerLegacyDN));
			}
			return null;
		}

		// Token: 0x06004461 RID: 17505
		internal abstract IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken);

		// Token: 0x06004462 RID: 17506 RVA: 0x001187AC File Offset: 0x001169AC
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}
	}
}
