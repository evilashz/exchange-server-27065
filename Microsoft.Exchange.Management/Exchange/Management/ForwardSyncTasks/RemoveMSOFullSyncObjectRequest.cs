using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000372 RID: 882
	[Cmdlet("Remove", "MSOFullSyncObjectRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMSOFullSyncObjectRequest : DataAccessTask<FullSyncObjectRequest>
	{
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x00085E50 File Offset: 0x00084050
		// (set) Token: 0x06001EF8 RID: 7928 RVA: 0x00085E67 File Offset: 0x00084067
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public SyncObjectId Identity
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

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x00085E7A File Offset: 0x0008407A
		// (set) Token: 0x06001EFA RID: 7930 RVA: 0x00085E91 File Offset: 0x00084091
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

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x00085EA4 File Offset: 0x000840A4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMSOFullSyncRequest(this.Identity.ToString());
			}
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00085EB6 File Offset: 0x000840B6
		protected override IConfigDataProvider CreateSession()
		{
			return new FullSyncObjectRequestDataProvider(false, this.ServiceInstanceId.InstanceId);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00085EC9 File Offset: 0x000840C9
		protected override void InternalProcessRecord()
		{
			base.DataSession.Delete(this.request);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00085EDC File Offset: 0x000840DC
		protected override void InternalValidate()
		{
			base.InternalValidate();
			this.request = base.DataSession.Read<FullSyncObjectRequest>(this.Identity);
			if (this.request == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorManagementObjectNotFound(this.Identity.ToString())), ExchangeErrorCategory.Client, this.Identity);
			}
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x00085F34 File Offset: 0x00084134
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is RidMasterConfigException;
		}

		// Token: 0x0400194E RID: 6478
		private const string SyncObjectIdParameter = "SyncObjectIdParameter";

		// Token: 0x0400194F RID: 6479
		private const string ServiceInstanceIdParameter = "ServiceInstanceIdParameter";

		// Token: 0x04001950 RID: 6480
		private IConfigurable request;
	}
}
