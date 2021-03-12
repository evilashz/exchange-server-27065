using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004B2 RID: 1202
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class KernelWatchdogTimerException : LocalizedException
	{
		// Token: 0x06002D44 RID: 11588 RVA: 0x000C112C File Offset: 0x000BF32C
		public KernelWatchdogTimerException(string msg) : base(ReplayStrings.KernelWatchdogTimerError(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000C1141 File Offset: 0x000BF341
		public KernelWatchdogTimerException(string msg, Exception innerException) : base(ReplayStrings.KernelWatchdogTimerError(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000C1157 File Offset: 0x000BF357
		protected KernelWatchdogTimerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000C1181 File Offset: 0x000BF381
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06002D48 RID: 11592 RVA: 0x000C119C File Offset: 0x000BF39C
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04001523 RID: 5411
		private readonly string msg;
	}
}
