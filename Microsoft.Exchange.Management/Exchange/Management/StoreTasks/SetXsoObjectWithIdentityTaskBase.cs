using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000797 RID: 1943
	public abstract class SetXsoObjectWithIdentityTaskBase<TDataObject> : SetRecipientObjectTask<MailboxIdParameter, TDataObject, ADUser> where TDataObject : IConfigurable, new()
	{
		// Token: 0x06004479 RID: 17529 RVA: 0x00118DEF File Offset: 0x00116FEF
		public SetXsoObjectWithIdentityTaskBase()
		{
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x0600447A RID: 17530 RVA: 0x00118DF7 File Offset: 0x00116FF7
		// (set) Token: 0x0600447B RID: 17531 RVA: 0x00118DFF File Offset: 0x00116FFF
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

		// Token: 0x0600447C RID: 17532 RVA: 0x00118E08 File Offset: 0x00117008
		internal override IConfigurationSession CreateConfigurationSession(ADSessionSettings sessionSettings)
		{
			if (sessionSettings.ConfigScopes == ConfigScopes.TenantLocal)
			{
				return base.CreateConfigurationSession(ADSessionSettings.RescopeToSubtree(sessionSettings));
			}
			return base.CreateConfigurationSession(sessionSettings);
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x00118E27 File Offset: 0x00117027
		internal override IRecipientSession CreateTenantGlobalCatalogSession(ADSessionSettings sessionSettings)
		{
			if (sessionSettings.ConfigScopes == ConfigScopes.TenantLocal)
			{
				return base.CreateTenantGlobalCatalogSession(ADSessionSettings.RescopeToSubtree(sessionSettings));
			}
			return base.CreateTenantGlobalCatalogSession(sessionSettings);
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x00118E48 File Offset: 0x00117048
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			using (XsoMailboxDataProviderBase xsoMailboxDataProviderBase = (XsoMailboxDataProviderBase)this.CreateXsoMailboxDataProvider(XsoStoreDataProviderBase.GetExchangePrincipalWithAdSessionSettingsForOrg(base.SessionSettings.CurrentOrganizationId, this.DataObject), (base.ExchangeRunspaceConfig == null) ? null : base.ExchangeRunspaceConfig.SecurityAccessToken))
			{
				TDataObject tdataObject = (TDataObject)((object)xsoMailboxDataProviderBase.Read<TDataObject>(this.DataObject.Identity));
				if (tdataObject == null)
				{
					tdataObject = this.GetDefaultConfiguration();
				}
				this.StampChangesOnXsoObject(tdataObject);
				this.SaveXsoObject(xsoMailboxDataProviderBase, tdataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x00118EF4 File Offset: 0x001170F4
		protected virtual void StampChangesOnXsoObject(IConfigurable dataObject)
		{
			if (this.Instance != null)
			{
				dataObject.CopyChangesFrom(this.Instance);
			}
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x00118F14 File Offset: 0x00117114
		protected virtual void SaveXsoObject(IConfigDataProvider provider, IConfigurable dataObject)
		{
			provider.Save(dataObject);
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x00118F1D File Offset: 0x0011711D
		protected virtual TDataObject GetDefaultConfiguration()
		{
			throw new ManagementObjectNotFoundException(Strings.ErrorObjectNotFound(this.Identity.ToString()));
		}

		// Token: 0x06004482 RID: 17538
		internal abstract IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken);

		// Token: 0x06004483 RID: 17539 RVA: 0x00118F34 File Offset: 0x00117134
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}
	}
}
