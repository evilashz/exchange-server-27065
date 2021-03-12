using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000054 RID: 84
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ArbitrationMinimumRequiredReadyNotSatisfiedException : ArbitrationExceptionCommon
	{
		// Token: 0x0600035E RID: 862 RVA: 0x0000BC25 File Offset: 0x00009E25
		public ArbitrationMinimumRequiredReadyNotSatisfiedException(int totalReady, int minimumRequired) : base(StringsRecovery.ArbitrationMinimumRequiredReadyNotSatisfied(totalReady, minimumRequired))
		{
			this.totalReady = totalReady;
			this.minimumRequired = minimumRequired;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000BC47 File Offset: 0x00009E47
		public ArbitrationMinimumRequiredReadyNotSatisfiedException(int totalReady, int minimumRequired, Exception innerException) : base(StringsRecovery.ArbitrationMinimumRequiredReadyNotSatisfied(totalReady, minimumRequired), innerException)
		{
			this.totalReady = totalReady;
			this.minimumRequired = minimumRequired;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000BC6C File Offset: 0x00009E6C
		protected ArbitrationMinimumRequiredReadyNotSatisfiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.totalReady = (int)info.GetValue("totalReady", typeof(int));
			this.minimumRequired = (int)info.GetValue("minimumRequired", typeof(int));
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000BCC1 File Offset: 0x00009EC1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("totalReady", this.totalReady);
			info.AddValue("minimumRequired", this.minimumRequired);
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000BCED File Offset: 0x00009EED
		public int TotalReady
		{
			get
			{
				return this.totalReady;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000BCF5 File Offset: 0x00009EF5
		public int MinimumRequired
		{
			get
			{
				return this.minimumRequired;
			}
		}

		// Token: 0x0400020A RID: 522
		private readonly int totalReady;

		// Token: 0x0400020B RID: 523
		private readonly int minimumRequired;
	}
}
