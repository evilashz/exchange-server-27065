using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200036E RID: 878
	[Cmdlet("New", "MSOFullSyncObjectRequest", SupportsShouldProcess = true)]
	public sealed class NewMSOFullSyncObjectRequest : NewTaskBase<FullSyncObjectRequest>
	{
		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x0008545C File Offset: 0x0008365C
		// (set) Token: 0x06001EBF RID: 7871 RVA: 0x00085473 File Offset: 0x00083673
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public SyncObjectId ObjectId
		{
			get
			{
				return (SyncObjectId)base.Fields["SyncObjectIdParameter"];
			}
			set
			{
				base.Fields["SyncObjectIdParameter"] = value;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x00085486 File Offset: 0x00083686
		// (set) Token: 0x06001EC1 RID: 7873 RVA: 0x0008549D File Offset: 0x0008369D
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public ServiceInstanceId ServiceInstanceId
		{
			get
			{
				return (ServiceInstanceId)base.Fields["ServiceInstanceIdParameter"];
			}
			set
			{
				base.Fields["ServiceInstanceIdParameter"] = value;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x000854B0 File Offset: 0x000836B0
		// (set) Token: 0x06001EC3 RID: 7875 RVA: 0x000854D1 File Offset: 0x000836D1
		[Parameter]
		public FullSyncObjectRequestOptions Options
		{
			get
			{
				return (FullSyncObjectRequestOptions)(base.Fields["OptionsParameter"] ?? FullSyncObjectRequestOptions.None);
			}
			set
			{
				base.Fields["OptionsParameter"] = value;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x000854E9 File Offset: 0x000836E9
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMSOFullSyncRequest(this.DataObject.Identity.ToString(), this.DataObject.ServiceInstanceId, this.DataObject.Options.ToString());
			}
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00085520 File Offset: 0x00083720
		protected override IConfigDataProvider CreateSession()
		{
			return new FullSyncObjectRequestDataProvider(false, this.ServiceInstanceId.InstanceId);
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x00085534 File Offset: 0x00083734
		protected override IConfigurable PrepareDataObject()
		{
			FullSyncObjectRequest fullSyncObjectRequest = (FullSyncObjectRequest)base.PrepareDataObject();
			fullSyncObjectRequest.SetIdentity(this.ObjectId);
			fullSyncObjectRequest.ServiceInstanceId = this.ServiceInstanceId.InstanceId;
			fullSyncObjectRequest.Options = this.Options;
			fullSyncObjectRequest.CreationTime = ExDateTime.UtcNow;
			return fullSyncObjectRequest;
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00085584 File Offset: 0x00083784
		protected override void InternalValidate()
		{
			Exception innerException;
			if (!NewMSOFullSyncObjectRequest.IsValidGuid(this.ObjectId.ContextId, out innerException) || !NewMSOFullSyncObjectRequest.IsValidGuid(this.ObjectId.ObjectId, out innerException))
			{
				base.WriteError(new LocalizedException(DirectoryStrings.ExArgumentException("ObjectId", this.ObjectId), innerException), ExchangeErrorCategory.Client, null);
			}
			Guid externalDirectoryOrganizationId = new Guid(this.ObjectId.ContextId);
			ITenantConfigurationSession tenantConfigurationSession = null;
			try
			{
				tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromExternalDirectoryOrganizationId(externalDirectoryOrganizationId), 134, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ForwardSync\\NewMSOFullSyncObjectRequest.cs");
			}
			catch (CannotResolveExternalDirectoryOrganizationIdException)
			{
			}
			if (tenantConfigurationSession != null)
			{
				ExchangeConfigurationUnit exchangeConfigurationUnitByExternalId = tenantConfigurationSession.GetExchangeConfigurationUnitByExternalId(this.ObjectId.ContextId);
				if (exchangeConfigurationUnitByExternalId != null && !StringComparer.OrdinalIgnoreCase.Equals(exchangeConfigurationUnitByExternalId.DirSyncServiceInstance, this.ServiceInstanceId.InstanceId))
				{
					base.WriteError(new ServiceInstanceNotMatchException(this.ObjectId.ToString(), this.ServiceInstanceId.InstanceId, exchangeConfigurationUnitByExternalId.DirSyncServiceInstance), ExchangeErrorCategory.Client, this.ObjectId);
				}
			}
			base.InternalValidate();
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00085694 File Offset: 0x00083894
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is RidMasterConfigException;
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x000856AC File Offset: 0x000838AC
		private static bool IsValidGuid(string guidString, out Exception parseException)
		{
			parseException = null;
			try
			{
				new Guid(guidString);
			}
			catch (FormatException ex)
			{
				parseException = ex;
				return false;
			}
			catch (OverflowException ex2)
			{
				parseException = ex2;
				return false;
			}
			return true;
		}

		// Token: 0x04001946 RID: 6470
		private const string SyncObjectIdParameter = "SyncObjectIdParameter";

		// Token: 0x04001947 RID: 6471
		private const string ServiceInstanceIdParameter = "ServiceInstanceIdParameter";

		// Token: 0x04001948 RID: 6472
		private const string OptionsParameter = "OptionsParameter";
	}
}
