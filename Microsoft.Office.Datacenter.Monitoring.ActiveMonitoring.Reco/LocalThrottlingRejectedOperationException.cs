using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000058 RID: 88
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LocalThrottlingRejectedOperationException : ThrottlingRejectedOperationException
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0000BFCE File Offset: 0x0000A1CE
		public LocalThrottlingRejectedOperationException(string actionId, string resourceName, string requester, string failedChecks) : base(StringsRecovery.LocalThrottlingRejectedOperation(actionId, resourceName, requester, failedChecks))
		{
			this.actionId = actionId;
			this.resourceName = resourceName;
			this.requester = requester;
			this.failedChecks = failedChecks;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000C002 File Offset: 0x0000A202
		public LocalThrottlingRejectedOperationException(string actionId, string resourceName, string requester, string failedChecks, Exception innerException) : base(StringsRecovery.LocalThrottlingRejectedOperation(actionId, resourceName, requester, failedChecks), innerException)
		{
			this.actionId = actionId;
			this.resourceName = resourceName;
			this.requester = requester;
			this.failedChecks = failedChecks;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000C038 File Offset: 0x0000A238
		protected LocalThrottlingRejectedOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actionId = (string)info.GetValue("actionId", typeof(string));
			this.resourceName = (string)info.GetValue("resourceName", typeof(string));
			this.requester = (string)info.GetValue("requester", typeof(string));
			this.failedChecks = (string)info.GetValue("failedChecks", typeof(string));
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000C0D0 File Offset: 0x0000A2D0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("actionId", this.actionId);
			info.AddValue("resourceName", this.resourceName);
			info.AddValue("requester", this.requester);
			info.AddValue("failedChecks", this.failedChecks);
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000C129 File Offset: 0x0000A329
		public string ActionId
		{
			get
			{
				return this.actionId;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000C131 File Offset: 0x0000A331
		public string ResourceName
		{
			get
			{
				return this.resourceName;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000C139 File Offset: 0x0000A339
		public string Requester
		{
			get
			{
				return this.requester;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000C141 File Offset: 0x0000A341
		public string FailedChecks
		{
			get
			{
				return this.failedChecks;
			}
		}

		// Token: 0x04000213 RID: 531
		private readonly string actionId;

		// Token: 0x04000214 RID: 532
		private readonly string resourceName;

		// Token: 0x04000215 RID: 533
		private readonly string requester;

		// Token: 0x04000216 RID: 534
		private readonly string failedChecks;
	}
}
