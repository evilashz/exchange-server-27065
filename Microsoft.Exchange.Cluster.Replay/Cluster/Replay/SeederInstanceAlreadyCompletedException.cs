using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200043F RID: 1087
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederInstanceAlreadyCompletedException : SeedPrepareException
	{
		// Token: 0x06002ACA RID: 10954 RVA: 0x000BC431 File Offset: 0x000BA631
		public SeederInstanceAlreadyCompletedException(string sourceMachine) : base(ReplayStrings.SeederInstanceAlreadyCompletedException(sourceMachine))
		{
			this.sourceMachine = sourceMachine;
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x000BC44B File Offset: 0x000BA64B
		public SeederInstanceAlreadyCompletedException(string sourceMachine, Exception innerException) : base(ReplayStrings.SeederInstanceAlreadyCompletedException(sourceMachine), innerException)
		{
			this.sourceMachine = sourceMachine;
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000BC466 File Offset: 0x000BA666
		protected SeederInstanceAlreadyCompletedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sourceMachine = (string)info.GetValue("sourceMachine", typeof(string));
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000BC490 File Offset: 0x000BA690
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sourceMachine", this.sourceMachine);
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002ACE RID: 10958 RVA: 0x000BC4AB File Offset: 0x000BA6AB
		public string SourceMachine
		{
			get
			{
				return this.sourceMachine;
			}
		}

		// Token: 0x04001475 RID: 5237
		private readonly string sourceMachine;
	}
}
