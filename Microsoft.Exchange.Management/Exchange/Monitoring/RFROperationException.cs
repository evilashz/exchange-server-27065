using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001106 RID: 4358
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RFROperationException : LocalizedException
	{
		// Token: 0x0600B40B RID: 46091 RVA: 0x0029C279 File Offset: 0x0029A479
		public RFROperationException(string operation, int returnValue, string serverId, string userName) : base(Strings.messageRFROperationException(operation, returnValue, serverId, userName))
		{
			this.operation = operation;
			this.returnValue = returnValue;
			this.serverId = serverId;
			this.userName = userName;
		}

		// Token: 0x0600B40C RID: 46092 RVA: 0x0029C2A8 File Offset: 0x0029A4A8
		public RFROperationException(string operation, int returnValue, string serverId, string userName, Exception innerException) : base(Strings.messageRFROperationException(operation, returnValue, serverId, userName), innerException)
		{
			this.operation = operation;
			this.returnValue = returnValue;
			this.serverId = serverId;
			this.userName = userName;
		}

		// Token: 0x0600B40D RID: 46093 RVA: 0x0029C2DC File Offset: 0x0029A4DC
		protected RFROperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operation = (string)info.GetValue("operation", typeof(string));
			this.returnValue = (int)info.GetValue("returnValue", typeof(int));
			this.serverId = (string)info.GetValue("serverId", typeof(string));
			this.userName = (string)info.GetValue("userName", typeof(string));
		}

		// Token: 0x0600B40E RID: 46094 RVA: 0x0029C374 File Offset: 0x0029A574
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operation", this.operation);
			info.AddValue("returnValue", this.returnValue);
			info.AddValue("serverId", this.serverId);
			info.AddValue("userName", this.userName);
		}

		// Token: 0x17003910 RID: 14608
		// (get) Token: 0x0600B40F RID: 46095 RVA: 0x0029C3CD File Offset: 0x0029A5CD
		public string Operation
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x17003911 RID: 14609
		// (get) Token: 0x0600B410 RID: 46096 RVA: 0x0029C3D5 File Offset: 0x0029A5D5
		public int ReturnValue
		{
			get
			{
				return this.returnValue;
			}
		}

		// Token: 0x17003912 RID: 14610
		// (get) Token: 0x0600B411 RID: 46097 RVA: 0x0029C3DD File Offset: 0x0029A5DD
		public string ServerId
		{
			get
			{
				return this.serverId;
			}
		}

		// Token: 0x17003913 RID: 14611
		// (get) Token: 0x0600B412 RID: 46098 RVA: 0x0029C3E5 File Offset: 0x0029A5E5
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x04006276 RID: 25206
		private readonly string operation;

		// Token: 0x04006277 RID: 25207
		private readonly int returnValue;

		// Token: 0x04006278 RID: 25208
		private readonly string serverId;

		// Token: 0x04006279 RID: 25209
		private readonly string userName;
	}
}
