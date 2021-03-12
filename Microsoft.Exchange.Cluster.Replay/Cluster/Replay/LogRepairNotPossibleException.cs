using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004DA RID: 1242
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogRepairNotPossibleException : LocalizedException
	{
		// Token: 0x06002E2B RID: 11819 RVA: 0x000C2E92 File Offset: 0x000C1092
		public LogRepairNotPossibleException(string reason) : base(ReplayStrings.LogRepairNotPossible(reason))
		{
			this.reason = reason;
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000C2EA7 File Offset: 0x000C10A7
		public LogRepairNotPossibleException(string reason, Exception innerException) : base(ReplayStrings.LogRepairNotPossible(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000C2EBD File Offset: 0x000C10BD
		protected LogRepairNotPossibleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000C2EE7 File Offset: 0x000C10E7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x000C2F02 File Offset: 0x000C1102
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x0400156A RID: 5482
		private readonly string reason;
	}
}
