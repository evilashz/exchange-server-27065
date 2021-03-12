using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000055 RID: 85
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ArbitrationMaximumAllowedConcurrentNotSatisfiedException : ArbitrationExceptionCommon
	{
		// Token: 0x06000364 RID: 868 RVA: 0x0000BCFD File Offset: 0x00009EFD
		public ArbitrationMaximumAllowedConcurrentNotSatisfiedException(int totalReady, int minimumRequired) : base(StringsRecovery.ArbitrationMaximumAllowedConcurrentNotSatisfied(totalReady, minimumRequired))
		{
			this.totalReady = totalReady;
			this.minimumRequired = minimumRequired;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000BD1F File Offset: 0x00009F1F
		public ArbitrationMaximumAllowedConcurrentNotSatisfiedException(int totalReady, int minimumRequired, Exception innerException) : base(StringsRecovery.ArbitrationMaximumAllowedConcurrentNotSatisfied(totalReady, minimumRequired), innerException)
		{
			this.totalReady = totalReady;
			this.minimumRequired = minimumRequired;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000BD44 File Offset: 0x00009F44
		protected ArbitrationMaximumAllowedConcurrentNotSatisfiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.totalReady = (int)info.GetValue("totalReady", typeof(int));
			this.minimumRequired = (int)info.GetValue("minimumRequired", typeof(int));
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000BD99 File Offset: 0x00009F99
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("totalReady", this.totalReady);
			info.AddValue("minimumRequired", this.minimumRequired);
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000BDC5 File Offset: 0x00009FC5
		public int TotalReady
		{
			get
			{
				return this.totalReady;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000BDCD File Offset: 0x00009FCD
		public int MinimumRequired
		{
			get
			{
				return this.minimumRequired;
			}
		}

		// Token: 0x0400020C RID: 524
		private readonly int totalReady;

		// Token: 0x0400020D RID: 525
		private readonly int minimumRequired;
	}
}
