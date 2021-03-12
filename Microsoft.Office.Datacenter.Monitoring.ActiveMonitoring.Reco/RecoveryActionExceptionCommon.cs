using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000051 RID: 81
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecoveryActionExceptionCommon : LocalizedException
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000BAA9 File Offset: 0x00009CA9
		public RecoveryActionExceptionCommon(string recoveryActionMsg) : base(StringsRecovery.RecoveryActionExceptionCommon(recoveryActionMsg))
		{
			this.recoveryActionMsg = recoveryActionMsg;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000BABE File Offset: 0x00009CBE
		public RecoveryActionExceptionCommon(string recoveryActionMsg, Exception innerException) : base(StringsRecovery.RecoveryActionExceptionCommon(recoveryActionMsg), innerException)
		{
			this.recoveryActionMsg = recoveryActionMsg;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		protected RecoveryActionExceptionCommon(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recoveryActionMsg = (string)info.GetValue("recoveryActionMsg", typeof(string));
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000BAFE File Offset: 0x00009CFE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recoveryActionMsg", this.recoveryActionMsg);
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000BB19 File Offset: 0x00009D19
		public string RecoveryActionMsg
		{
			get
			{
				return this.recoveryActionMsg;
			}
		}

		// Token: 0x04000207 RID: 519
		private readonly string recoveryActionMsg;
	}
}
