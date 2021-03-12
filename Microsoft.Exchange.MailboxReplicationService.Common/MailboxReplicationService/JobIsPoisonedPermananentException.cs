using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002FB RID: 763
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class JobIsPoisonedPermananentException : MailboxReplicationPermanentException
	{
		// Token: 0x060024A0 RID: 9376 RVA: 0x000503FF File Offset: 0x0004E5FF
		public JobIsPoisonedPermananentException(int poisonCount) : base(MrsStrings.JobIsPoisoned(poisonCount))
		{
			this.poisonCount = poisonCount;
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00050414 File Offset: 0x0004E614
		public JobIsPoisonedPermananentException(int poisonCount, Exception innerException) : base(MrsStrings.JobIsPoisoned(poisonCount), innerException)
		{
			this.poisonCount = poisonCount;
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x0005042A File Offset: 0x0004E62A
		protected JobIsPoisonedPermananentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.poisonCount = (int)info.GetValue("poisonCount", typeof(int));
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x00050454 File Offset: 0x0004E654
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("poisonCount", this.poisonCount);
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060024A4 RID: 9380 RVA: 0x0005046F File Offset: 0x0004E66F
		public int PoisonCount
		{
			get
			{
				return this.poisonCount;
			}
		}

		// Token: 0x04000FFD RID: 4093
		private readonly int poisonCount;
	}
}
