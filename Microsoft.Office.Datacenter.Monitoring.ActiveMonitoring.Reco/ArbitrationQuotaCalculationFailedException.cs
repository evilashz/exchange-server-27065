using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200005C RID: 92
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ArbitrationQuotaCalculationFailedException : ArbitrationExceptionCommon
	{
		// Token: 0x0600039C RID: 924 RVA: 0x0000C773 File Offset: 0x0000A973
		public ArbitrationQuotaCalculationFailedException(int exhaustedQuota, int allowedQuota, bool isConcluded, bool isInvokedTooSoon) : base(StringsRecovery.ArbitrationQuotaCalculationFailed(exhaustedQuota, allowedQuota, isConcluded, isInvokedTooSoon))
		{
			this.exhaustedQuota = exhaustedQuota;
			this.allowedQuota = allowedQuota;
			this.isConcluded = isConcluded;
			this.isInvokedTooSoon = isInvokedTooSoon;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000C7A7 File Offset: 0x0000A9A7
		public ArbitrationQuotaCalculationFailedException(int exhaustedQuota, int allowedQuota, bool isConcluded, bool isInvokedTooSoon, Exception innerException) : base(StringsRecovery.ArbitrationQuotaCalculationFailed(exhaustedQuota, allowedQuota, isConcluded, isInvokedTooSoon), innerException)
		{
			this.exhaustedQuota = exhaustedQuota;
			this.allowedQuota = allowedQuota;
			this.isConcluded = isConcluded;
			this.isInvokedTooSoon = isInvokedTooSoon;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		protected ArbitrationQuotaCalculationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exhaustedQuota = (int)info.GetValue("exhaustedQuota", typeof(int));
			this.allowedQuota = (int)info.GetValue("allowedQuota", typeof(int));
			this.isConcluded = (bool)info.GetValue("isConcluded", typeof(bool));
			this.isInvokedTooSoon = (bool)info.GetValue("isInvokedTooSoon", typeof(bool));
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000C878 File Offset: 0x0000AA78
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exhaustedQuota", this.exhaustedQuota);
			info.AddValue("allowedQuota", this.allowedQuota);
			info.AddValue("isConcluded", this.isConcluded);
			info.AddValue("isInvokedTooSoon", this.isInvokedTooSoon);
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000C8D1 File Offset: 0x0000AAD1
		public int ExhaustedQuota
		{
			get
			{
				return this.exhaustedQuota;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000C8D9 File Offset: 0x0000AAD9
		public int AllowedQuota
		{
			get
			{
				return this.allowedQuota;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000C8E1 File Offset: 0x0000AAE1
		public bool IsConcluded
		{
			get
			{
				return this.isConcluded;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000C8E9 File Offset: 0x0000AAE9
		public bool IsInvokedTooSoon
		{
			get
			{
				return this.isInvokedTooSoon;
			}
		}

		// Token: 0x04000228 RID: 552
		private readonly int exhaustedQuota;

		// Token: 0x04000229 RID: 553
		private readonly int allowedQuota;

		// Token: 0x0400022A RID: 554
		private readonly bool isConcluded;

		// Token: 0x0400022B RID: 555
		private readonly bool isInvokedTooSoon;
	}
}
