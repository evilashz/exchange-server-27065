using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004D8 RID: 1240
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogRepairFailedToVerifyFromActiveException : LocalizedException
	{
		// Token: 0x06002E1F RID: 11807 RVA: 0x000C2D01 File Offset: 0x000C0F01
		public LogRepairFailedToVerifyFromActiveException(string tempLogName, string activeServerName, string exceptionText) : base(ReplayStrings.LogRepairFailedToVerifyFromActive(tempLogName, activeServerName, exceptionText))
		{
			this.tempLogName = tempLogName;
			this.activeServerName = activeServerName;
			this.exceptionText = exceptionText;
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000C2D26 File Offset: 0x000C0F26
		public LogRepairFailedToVerifyFromActiveException(string tempLogName, string activeServerName, string exceptionText, Exception innerException) : base(ReplayStrings.LogRepairFailedToVerifyFromActive(tempLogName, activeServerName, exceptionText), innerException)
		{
			this.tempLogName = tempLogName;
			this.activeServerName = activeServerName;
			this.exceptionText = exceptionText;
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000C2D50 File Offset: 0x000C0F50
		protected LogRepairFailedToVerifyFromActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.tempLogName = (string)info.GetValue("tempLogName", typeof(string));
			this.activeServerName = (string)info.GetValue("activeServerName", typeof(string));
			this.exceptionText = (string)info.GetValue("exceptionText", typeof(string));
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000C2DC5 File Offset: 0x000C0FC5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("tempLogName", this.tempLogName);
			info.AddValue("activeServerName", this.activeServerName);
			info.AddValue("exceptionText", this.exceptionText);
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06002E23 RID: 11811 RVA: 0x000C2E02 File Offset: 0x000C1002
		public string TempLogName
		{
			get
			{
				return this.tempLogName;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x000C2E0A File Offset: 0x000C100A
		public string ActiveServerName
		{
			get
			{
				return this.activeServerName;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x000C2E12 File Offset: 0x000C1012
		public string ExceptionText
		{
			get
			{
				return this.exceptionText;
			}
		}

		// Token: 0x04001566 RID: 5478
		private readonly string tempLogName;

		// Token: 0x04001567 RID: 5479
		private readonly string activeServerName;

		// Token: 0x04001568 RID: 5480
		private readonly string exceptionText;
	}
}
