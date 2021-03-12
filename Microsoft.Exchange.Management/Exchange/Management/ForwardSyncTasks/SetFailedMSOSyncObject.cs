using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000379 RID: 889
	[Cmdlet("Set", "FailedMSOSyncObject", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetFailedMSOSyncObject : SetObjectWithIdentityTaskBase<FailedMSOSyncObjectIdParameter, FailedMSOSyncObjectPresentationObject, FailedMSOSyncObject>
	{
		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x000868AC File Offset: 0x00084AAC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetFailedMSOSyncObject(this.Identity.ToString());
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x000868BE File Offset: 0x00084ABE
		protected override ObjectId RootId
		{
			get
			{
				return ForwardSyncDataAccessHelper.GetRootId(this.Identity);
			}
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x000868CB File Offset: 0x00084ACB
		protected override IConfigDataProvider CreateSession()
		{
			return ForwardSyncDataAccessHelper.CreateSession(false);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x000868D4 File Offset: 0x00084AD4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.DataObject.IsValidationDivergence && !this.DataObject.IsIgnoredInHaltCondition)
			{
				base.WriteError(new ValidationDivergenceMustBeNonHaltingException(this.DataObject.Identity.ToString()), ExchangeErrorCategory.Client, this.DataObject);
			}
			TaskLogger.LogExit();
		}
	}
}
