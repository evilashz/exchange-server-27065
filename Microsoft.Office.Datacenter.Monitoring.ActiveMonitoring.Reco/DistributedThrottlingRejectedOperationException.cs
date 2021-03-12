using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000056 RID: 86
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DistributedThrottlingRejectedOperationException : ArbitrationExceptionCommon
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000BDD5 File Offset: 0x00009FD5
		public DistributedThrottlingRejectedOperationException(string actionId, string requester) : base(StringsRecovery.DistributedThrottlingRejectedOperation(actionId, requester))
		{
			this.actionId = actionId;
			this.requester = requester;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000BDF7 File Offset: 0x00009FF7
		public DistributedThrottlingRejectedOperationException(string actionId, string requester, Exception innerException) : base(StringsRecovery.DistributedThrottlingRejectedOperation(actionId, requester), innerException)
		{
			this.actionId = actionId;
			this.requester = requester;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000BE1C File Offset: 0x0000A01C
		protected DistributedThrottlingRejectedOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actionId = (string)info.GetValue("actionId", typeof(string));
			this.requester = (string)info.GetValue("requester", typeof(string));
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000BE71 File Offset: 0x0000A071
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("actionId", this.actionId);
			info.AddValue("requester", this.requester);
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000BE9D File Offset: 0x0000A09D
		public string ActionId
		{
			get
			{
				return this.actionId;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000BEA5 File Offset: 0x0000A0A5
		public string Requester
		{
			get
			{
				return this.requester;
			}
		}

		// Token: 0x0400020E RID: 526
		private readonly string actionId;

		// Token: 0x0400020F RID: 527
		private readonly string requester;
	}
}
