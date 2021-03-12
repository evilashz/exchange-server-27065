using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200005A RID: 90
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ThrottlingInProgressException : ThrottlingRejectedOperationException
	{
		// Token: 0x06000387 RID: 903 RVA: 0x0000C2C8 File Offset: 0x0000A4C8
		public ThrottlingInProgressException(long instanceId, string actionId, string resourceName, string currentRequester, string inProgressRequester, DateTime operationStartTime, DateTime expectedEndTime) : base(StringsRecovery.ThrottlingInProgressException(instanceId, actionId, resourceName, currentRequester, inProgressRequester, operationStartTime, expectedEndTime))
		{
			this.instanceId = instanceId;
			this.actionId = actionId;
			this.resourceName = resourceName;
			this.currentRequester = currentRequester;
			this.inProgressRequester = inProgressRequester;
			this.operationStartTime = operationStartTime;
			this.expectedEndTime = expectedEndTime;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000C328 File Offset: 0x0000A528
		public ThrottlingInProgressException(long instanceId, string actionId, string resourceName, string currentRequester, string inProgressRequester, DateTime operationStartTime, DateTime expectedEndTime, Exception innerException) : base(StringsRecovery.ThrottlingInProgressException(instanceId, actionId, resourceName, currentRequester, inProgressRequester, operationStartTime, expectedEndTime), innerException)
		{
			this.instanceId = instanceId;
			this.actionId = actionId;
			this.resourceName = resourceName;
			this.currentRequester = currentRequester;
			this.inProgressRequester = inProgressRequester;
			this.operationStartTime = operationStartTime;
			this.expectedEndTime = expectedEndTime;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000C388 File Offset: 0x0000A588
		protected ThrottlingInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.instanceId = (long)info.GetValue("instanceId", typeof(long));
			this.actionId = (string)info.GetValue("actionId", typeof(string));
			this.resourceName = (string)info.GetValue("resourceName", typeof(string));
			this.currentRequester = (string)info.GetValue("currentRequester", typeof(string));
			this.inProgressRequester = (string)info.GetValue("inProgressRequester", typeof(string));
			this.operationStartTime = (DateTime)info.GetValue("operationStartTime", typeof(DateTime));
			this.expectedEndTime = (DateTime)info.GetValue("expectedEndTime", typeof(DateTime));
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000C480 File Offset: 0x0000A680
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("instanceId", this.instanceId);
			info.AddValue("actionId", this.actionId);
			info.AddValue("resourceName", this.resourceName);
			info.AddValue("currentRequester", this.currentRequester);
			info.AddValue("inProgressRequester", this.inProgressRequester);
			info.AddValue("operationStartTime", this.operationStartTime);
			info.AddValue("expectedEndTime", this.expectedEndTime);
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000C50C File Offset: 0x0000A70C
		public long InstanceId
		{
			get
			{
				return this.instanceId;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000C514 File Offset: 0x0000A714
		public string ActionId
		{
			get
			{
				return this.actionId;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000C51C File Offset: 0x0000A71C
		public string ResourceName
		{
			get
			{
				return this.resourceName;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000C524 File Offset: 0x0000A724
		public string CurrentRequester
		{
			get
			{
				return this.currentRequester;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000C52C File Offset: 0x0000A72C
		public string InProgressRequester
		{
			get
			{
				return this.inProgressRequester;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000C534 File Offset: 0x0000A734
		public DateTime OperationStartTime
		{
			get
			{
				return this.operationStartTime;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000C53C File Offset: 0x0000A73C
		public DateTime ExpectedEndTime
		{
			get
			{
				return this.expectedEndTime;
			}
		}

		// Token: 0x0400021B RID: 539
		private readonly long instanceId;

		// Token: 0x0400021C RID: 540
		private readonly string actionId;

		// Token: 0x0400021D RID: 541
		private readonly string resourceName;

		// Token: 0x0400021E RID: 542
		private readonly string currentRequester;

		// Token: 0x0400021F RID: 543
		private readonly string inProgressRequester;

		// Token: 0x04000220 RID: 544
		private readonly DateTime operationStartTime;

		// Token: 0x04000221 RID: 545
		private readonly DateTime expectedEndTime;
	}
}
