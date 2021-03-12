using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002D2 RID: 722
	internal sealed class Deprovision : BaseProvisionCommand<DeprovisionRequest, DeprovisionResponse>
	{
		// Token: 0x0600140E RID: 5134 RVA: 0x000644DB File Offset: 0x000626DB
		public Deprovision(CallContext callContext, DeprovisionRequest request) : base(callContext, request, request.HasPAL, request.DeviceType, request.DeviceID, request.SpecifyProtocol, request.Protocol)
		{
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00064503 File Offset: 0x00062703
		protected override void InternalExecute()
		{
			SyncStateStorage.DeleteSyncStateStorage(base.MailboxIdentityMailboxSession, new DeviceIdentity(base.Request.DeviceID, base.Request.DeviceType, base.Protocol), null);
		}
	}
}
