using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004DC RID: 1244
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogRepairTransientException : TransientException
	{
		// Token: 0x06002E37 RID: 11831 RVA: 0x000C3022 File Offset: 0x000C1222
		public LogRepairTransientException(string reason) : base(ReplayStrings.LogRepairFailedTransient(reason))
		{
			this.reason = reason;
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000C3037 File Offset: 0x000C1237
		public LogRepairTransientException(string reason, Exception innerException) : base(ReplayStrings.LogRepairFailedTransient(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000C304D File Offset: 0x000C124D
		protected LogRepairTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000C3077 File Offset: 0x000C1277
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x000C3092 File Offset: 0x000C1292
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x0400156E RID: 5486
		private readonly string reason;
	}
}
