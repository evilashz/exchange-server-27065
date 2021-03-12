using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E87 RID: 3719
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NotificationRpcOutParameters : RpcParameters
	{
		// Token: 0x17002260 RID: 8800
		// (get) Token: 0x06008175 RID: 33141 RVA: 0x00235EDC File Offset: 0x002340DC
		// (set) Token: 0x06008176 RID: 33142 RVA: 0x00235EE4 File Offset: 0x002340E4
		public SyncNotificationResult Result { get; private set; }

		// Token: 0x06008177 RID: 33143 RVA: 0x00235EED File Offset: 0x002340ED
		public NotificationRpcOutParameters(byte[] data) : base(data)
		{
			this.Result = (SyncNotificationResult)base.GetParameterValue("SyncNotificationResult");
		}

		// Token: 0x06008178 RID: 33144 RVA: 0x00235F0C File Offset: 0x0023410C
		public NotificationRpcOutParameters(SyncNotificationResult result)
		{
			this.Result = result;
			base.SetParameterValue("SyncNotificationResult", this.Result);
		}

		// Token: 0x040056F8 RID: 22264
		private const string SyncNotificationResultParameterName = "SyncNotificationResult";
	}
}
