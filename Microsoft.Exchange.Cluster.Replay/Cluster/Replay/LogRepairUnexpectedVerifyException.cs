using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004D6 RID: 1238
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogRepairUnexpectedVerifyException : LocalizedException
	{
		// Token: 0x06002E13 RID: 11795 RVA: 0x000C2B69 File Offset: 0x000C0D69
		public LogRepairUnexpectedVerifyException(string logName, string exceptionText) : base(ReplayStrings.LogRepairUnexpectedVerifyError(logName, exceptionText))
		{
			this.logName = logName;
			this.exceptionText = exceptionText;
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000C2B86 File Offset: 0x000C0D86
		public LogRepairUnexpectedVerifyException(string logName, string exceptionText, Exception innerException) : base(ReplayStrings.LogRepairUnexpectedVerifyError(logName, exceptionText), innerException)
		{
			this.logName = logName;
			this.exceptionText = exceptionText;
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000C2BA4 File Offset: 0x000C0DA4
		protected LogRepairUnexpectedVerifyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.logName = (string)info.GetValue("logName", typeof(string));
			this.exceptionText = (string)info.GetValue("exceptionText", typeof(string));
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x000C2BF9 File Offset: 0x000C0DF9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("logName", this.logName);
			info.AddValue("exceptionText", this.exceptionText);
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x000C2C25 File Offset: 0x000C0E25
		public string LogName
		{
			get
			{
				return this.logName;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06002E18 RID: 11800 RVA: 0x000C2C2D File Offset: 0x000C0E2D
		public string ExceptionText
		{
			get
			{
				return this.exceptionText;
			}
		}

		// Token: 0x04001562 RID: 5474
		private readonly string logName;

		// Token: 0x04001563 RID: 5475
		private readonly string exceptionText;
	}
}
