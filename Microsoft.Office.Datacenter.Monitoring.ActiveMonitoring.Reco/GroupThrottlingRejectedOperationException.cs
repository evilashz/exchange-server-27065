using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000059 RID: 89
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupThrottlingRejectedOperationException : ThrottlingRejectedOperationException
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0000C149 File Offset: 0x0000A349
		public GroupThrottlingRejectedOperationException(string actionId, string resourceName, string requester, string failedChecks) : base(StringsRecovery.GroupThrottlingRejectedOperation(actionId, resourceName, requester, failedChecks))
		{
			this.actionId = actionId;
			this.resourceName = resourceName;
			this.requester = requester;
			this.failedChecks = failedChecks;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000C17D File Offset: 0x0000A37D
		public GroupThrottlingRejectedOperationException(string actionId, string resourceName, string requester, string failedChecks, Exception innerException) : base(StringsRecovery.GroupThrottlingRejectedOperation(actionId, resourceName, requester, failedChecks), innerException)
		{
			this.actionId = actionId;
			this.resourceName = resourceName;
			this.requester = requester;
			this.failedChecks = failedChecks;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		protected GroupThrottlingRejectedOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actionId = (string)info.GetValue("actionId", typeof(string));
			this.resourceName = (string)info.GetValue("resourceName", typeof(string));
			this.requester = (string)info.GetValue("requester", typeof(string));
			this.failedChecks = (string)info.GetValue("failedChecks", typeof(string));
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000C24C File Offset: 0x0000A44C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("actionId", this.actionId);
			info.AddValue("resourceName", this.resourceName);
			info.AddValue("requester", this.requester);
			info.AddValue("failedChecks", this.failedChecks);
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000C2A5 File Offset: 0x0000A4A5
		public string ActionId
		{
			get
			{
				return this.actionId;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000C2AD File Offset: 0x0000A4AD
		public string ResourceName
		{
			get
			{
				return this.resourceName;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000C2B5 File Offset: 0x0000A4B5
		public string Requester
		{
			get
			{
				return this.requester;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000C2BD File Offset: 0x0000A4BD
		public string FailedChecks
		{
			get
			{
				return this.failedChecks;
			}
		}

		// Token: 0x04000217 RID: 535
		private readonly string actionId;

		// Token: 0x04000218 RID: 536
		private readonly string resourceName;

		// Token: 0x04000219 RID: 537
		private readonly string requester;

		// Token: 0x0400021A RID: 538
		private readonly string failedChecks;
	}
}
