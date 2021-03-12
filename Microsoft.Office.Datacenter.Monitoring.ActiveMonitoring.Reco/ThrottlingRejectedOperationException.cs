using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000052 RID: 82
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ThrottlingRejectedOperationException : RecoveryActionExceptionCommon
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0000BB21 File Offset: 0x00009D21
		public ThrottlingRejectedOperationException(string rejectedOperationMsg) : base(StringsRecovery.ThrottlingRejectedOperationException(rejectedOperationMsg))
		{
			this.rejectedOperationMsg = rejectedOperationMsg;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000BB3B File Offset: 0x00009D3B
		public ThrottlingRejectedOperationException(string rejectedOperationMsg, Exception innerException) : base(StringsRecovery.ThrottlingRejectedOperationException(rejectedOperationMsg), innerException)
		{
			this.rejectedOperationMsg = rejectedOperationMsg;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000BB56 File Offset: 0x00009D56
		protected ThrottlingRejectedOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.rejectedOperationMsg = (string)info.GetValue("rejectedOperationMsg", typeof(string));
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000BB80 File Offset: 0x00009D80
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("rejectedOperationMsg", this.rejectedOperationMsg);
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000BB9B File Offset: 0x00009D9B
		public string RejectedOperationMsg
		{
			get
			{
				return this.rejectedOperationMsg;
			}
		}

		// Token: 0x04000208 RID: 520
		private readonly string rejectedOperationMsg;
	}
}
