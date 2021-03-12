using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004D7 RID: 1239
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogRepairFailedToCopyFromActiveException : LocalizedException
	{
		// Token: 0x06002E19 RID: 11801 RVA: 0x000C2C35 File Offset: 0x000C0E35
		public LogRepairFailedToCopyFromActiveException(string tempLogName, string exceptionText) : base(ReplayStrings.LogRepairFailedToCopyFromActive(tempLogName, exceptionText))
		{
			this.tempLogName = tempLogName;
			this.exceptionText = exceptionText;
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000C2C52 File Offset: 0x000C0E52
		public LogRepairFailedToCopyFromActiveException(string tempLogName, string exceptionText, Exception innerException) : base(ReplayStrings.LogRepairFailedToCopyFromActive(tempLogName, exceptionText), innerException)
		{
			this.tempLogName = tempLogName;
			this.exceptionText = exceptionText;
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000C2C70 File Offset: 0x000C0E70
		protected LogRepairFailedToCopyFromActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.tempLogName = (string)info.GetValue("tempLogName", typeof(string));
			this.exceptionText = (string)info.GetValue("exceptionText", typeof(string));
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000C2CC5 File Offset: 0x000C0EC5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("tempLogName", this.tempLogName);
			info.AddValue("exceptionText", this.exceptionText);
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x000C2CF1 File Offset: 0x000C0EF1
		public string TempLogName
		{
			get
			{
				return this.tempLogName;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x000C2CF9 File Offset: 0x000C0EF9
		public string ExceptionText
		{
			get
			{
				return this.exceptionText;
			}
		}

		// Token: 0x04001564 RID: 5476
		private readonly string tempLogName;

		// Token: 0x04001565 RID: 5477
		private readonly string exceptionText;
	}
}
