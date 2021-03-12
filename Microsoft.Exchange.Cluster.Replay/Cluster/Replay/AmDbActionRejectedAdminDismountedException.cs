using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000477 RID: 1143
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbActionRejectedAdminDismountedException : AmCommonException
	{
		// Token: 0x06002BEE RID: 11246 RVA: 0x000BE4D7 File Offset: 0x000BC6D7
		public AmDbActionRejectedAdminDismountedException(string actionCode) : base(ReplayStrings.AmDbActionRejectedAdminDismountedException(actionCode))
		{
			this.actionCode = actionCode;
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000BE4F1 File Offset: 0x000BC6F1
		public AmDbActionRejectedAdminDismountedException(string actionCode, Exception innerException) : base(ReplayStrings.AmDbActionRejectedAdminDismountedException(actionCode), innerException)
		{
			this.actionCode = actionCode;
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000BE50C File Offset: 0x000BC70C
		protected AmDbActionRejectedAdminDismountedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actionCode = (string)info.GetValue("actionCode", typeof(string));
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000BE536 File Offset: 0x000BC736
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("actionCode", this.actionCode);
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x000BE551 File Offset: 0x000BC751
		public string ActionCode
		{
			get
			{
				return this.actionCode;
			}
		}

		// Token: 0x040014B9 RID: 5305
		private readonly string actionCode;
	}
}
