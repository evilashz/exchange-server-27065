using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200043D RID: 1085
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederInstanceAlreadyAddedException : SeedPrepareException
	{
		// Token: 0x06002AC0 RID: 10944 RVA: 0x000BC32D File Offset: 0x000BA52D
		public SeederInstanceAlreadyAddedException(string sourceMachine) : base(ReplayStrings.SeederInstanceAlreadyAddedException(sourceMachine))
		{
			this.sourceMachine = sourceMachine;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x000BC347 File Offset: 0x000BA547
		public SeederInstanceAlreadyAddedException(string sourceMachine, Exception innerException) : base(ReplayStrings.SeederInstanceAlreadyAddedException(sourceMachine), innerException)
		{
			this.sourceMachine = sourceMachine;
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x000BC362 File Offset: 0x000BA562
		protected SeederInstanceAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sourceMachine = (string)info.GetValue("sourceMachine", typeof(string));
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000BC38C File Offset: 0x000BA58C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sourceMachine", this.sourceMachine);
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x000BC3A7 File Offset: 0x000BA5A7
		public string SourceMachine
		{
			get
			{
				return this.sourceMachine;
			}
		}

		// Token: 0x04001473 RID: 5235
		private readonly string sourceMachine;
	}
}
