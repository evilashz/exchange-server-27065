using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000053 RID: 83
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ArbitrationExceptionCommon : ThrottlingRejectedOperationException
	{
		// Token: 0x06000359 RID: 857 RVA: 0x0000BBA3 File Offset: 0x00009DA3
		public ArbitrationExceptionCommon(string arbitrationMsg) : base(StringsRecovery.ArbitrationExceptionCommon(arbitrationMsg))
		{
			this.arbitrationMsg = arbitrationMsg;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000BBBD File Offset: 0x00009DBD
		public ArbitrationExceptionCommon(string arbitrationMsg, Exception innerException) : base(StringsRecovery.ArbitrationExceptionCommon(arbitrationMsg), innerException)
		{
			this.arbitrationMsg = arbitrationMsg;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000BBD8 File Offset: 0x00009DD8
		protected ArbitrationExceptionCommon(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.arbitrationMsg = (string)info.GetValue("arbitrationMsg", typeof(string));
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000BC02 File Offset: 0x00009E02
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("arbitrationMsg", this.arbitrationMsg);
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000BC1D File Offset: 0x00009E1D
		public string ArbitrationMsg
		{
			get
			{
				return this.arbitrationMsg;
			}
		}

		// Token: 0x04000209 RID: 521
		private readonly string arbitrationMsg;
	}
}
