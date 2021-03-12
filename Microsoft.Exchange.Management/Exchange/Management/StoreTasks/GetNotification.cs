using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000774 RID: 1908
	[Cmdlet("Get", "Notification", DefaultParameterSetName = "Filter")]
	public sealed class GetNotification : GetTenantADObjectWithIdentityTaskBase<EwsStoreObjectIdParameter, AsyncOperationNotification>
	{
		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x06004368 RID: 17256 RVA: 0x00114971 File Offset: 0x00112B71
		// (set) Token: 0x06004369 RID: 17257 RVA: 0x00114997 File Offset: 0x00112B97
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[Parameter(Mandatory = false, ParameterSetName = "Filter")]
		public SwitchParameter Summary
		{
			get
			{
				return (SwitchParameter)(base.Fields["Summary"] ?? false);
			}
			set
			{
				base.Fields["Summary"] = value;
			}
		}

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x0600436A RID: 17258 RVA: 0x001149AF File Offset: 0x00112BAF
		// (set) Token: 0x0600436B RID: 17259 RVA: 0x001149D5 File Offset: 0x00112BD5
		[Parameter(Mandatory = true, ParameterSetName = "Settings")]
		public SwitchParameter Settings
		{
			get
			{
				return (SwitchParameter)(base.Fields["Settings"] ?? false);
			}
			set
			{
				base.Fields["Settings"] = value;
			}
		}

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x001149ED File Offset: 0x00112BED
		// (set) Token: 0x0600436D RID: 17261 RVA: 0x00114A04 File Offset: 0x00112C04
		[Parameter]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x0600436E RID: 17262 RVA: 0x00114A17 File Offset: 0x00112C17
		// (set) Token: 0x0600436F RID: 17263 RVA: 0x00114A1F File Offset: 0x00112C1F
		[Parameter(Mandatory = false, ParameterSetName = "Filter")]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x06004370 RID: 17264 RVA: 0x00114A28 File Offset: 0x00112C28
		// (set) Token: 0x06004371 RID: 17265 RVA: 0x00114A3F File Offset: 0x00112C3F
		[Parameter(Mandatory = false, ParameterSetName = "Filter")]
		public ExDateTime? StartDate
		{
			get
			{
				return (ExDateTime?)base.Fields["StartDate"];
			}
			set
			{
				base.Fields["StartDate"] = value;
			}
		}

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x06004372 RID: 17266 RVA: 0x00114A57 File Offset: 0x00112C57
		// (set) Token: 0x06004373 RID: 17267 RVA: 0x00114A78 File Offset: 0x00112C78
		[Parameter(Mandatory = true, ParameterSetName = "Settings")]
		[Parameter(Mandatory = false, ParameterSetName = "Filter")]
		public AsyncOperationType ProcessType
		{
			get
			{
				return (AsyncOperationType)(base.Fields["ProcessType"] ?? AsyncOperationType.Unknown);
			}
			set
			{
				base.Fields["ProcessType"] = value;
			}
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x06004374 RID: 17268 RVA: 0x00114A90 File Offset: 0x00112C90
		protected override Unlimited<uint> DefaultResultSize
		{
			get
			{
				return new Unlimited<uint>(50U);
			}
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x00114A99 File Offset: 0x00112C99
		protected override IConfigDataProvider CreateSession()
		{
			return new AsyncOperationNotificationDataProvider(base.CurrentOrganizationId);
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x00114AA8 File Offset: 0x00112CA8
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 161, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\AsyncOperationNotification\\GetNotification.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				return adorganizationalUnit.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x00114B54 File Offset: 0x00112D54
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (base.ParameterSetName == "Settings")
			{
				string id;
				if (AsyncOperationNotificationDataProvider.SettingsObjectIdentityMap.TryGetValue(this.ProcessType, out id))
				{
					this.Identity = new EwsStoreObjectIdParameter(id);
				}
				else
				{
					base.WriteError(new ArgumentException(Strings.ErrorInvalidAsyncNotificationProcessType(this.ProcessType.ToString())), ErrorCategory.InvalidArgument, this.ProcessType);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x00114BD8 File Offset: 0x00112DD8
		protected override void InternalProcessRecord()
		{
			if (this.Identity != null)
			{
				this.WriteResult(base.GetDataObject(this.Identity));
				return;
			}
			ProviderPropertyDefinition[] properties = this.Summary ? new ProviderPropertyDefinition[]
			{
				EwsStoreObjectSchema.AlternativeId,
				AsyncOperationNotificationSchema.Type,
				AsyncOperationNotificationSchema.Status
			} : null;
			this.WriteResult<AsyncOperationNotification>(((AsyncOperationNotificationDataProvider)base.DataSession).GetNotificationDetails(base.Fields.IsModified("ProcessType") ? new AsyncOperationType?(this.ProcessType) : null, this.StartDate, this.ResultSize.IsUnlimited ? null : new int?((int)this.ResultSize.Value), properties));
		}

		// Token: 0x04002A02 RID: 10754
		private const string FilterParameterSet = "Filter";

		// Token: 0x04002A03 RID: 10755
		private const string SettingsParameterSet = "Settings";

		// Token: 0x04002A04 RID: 10756
		private const string SummaryParameter = "Summary";

		// Token: 0x04002A05 RID: 10757
		private const string SettingsParameter = "Settings";

		// Token: 0x04002A06 RID: 10758
		private const string ProcessTypeParameter = "ProcessType";
	}
}
