using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000440 RID: 1088
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederInstanceAlreadyCancelledException : SeedPrepareException
	{
		// Token: 0x06002ACF RID: 10959 RVA: 0x000BC4B3 File Offset: 0x000BA6B3
		public SeederInstanceAlreadyCancelledException(string sourceMachine) : base(ReplayStrings.SeederInstanceAlreadyCancelledException(sourceMachine))
		{
			this.sourceMachine = sourceMachine;
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000BC4CD File Offset: 0x000BA6CD
		public SeederInstanceAlreadyCancelledException(string sourceMachine, Exception innerException) : base(ReplayStrings.SeederInstanceAlreadyCancelledException(sourceMachine), innerException)
		{
			this.sourceMachine = sourceMachine;
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000BC4E8 File Offset: 0x000BA6E8
		protected SeederInstanceAlreadyCancelledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sourceMachine = (string)info.GetValue("sourceMachine", typeof(string));
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000BC512 File Offset: 0x000BA712
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sourceMachine", this.sourceMachine);
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x000BC52D File Offset: 0x000BA72D
		public string SourceMachine
		{
			get
			{
				return this.sourceMachine;
			}
		}

		// Token: 0x04001476 RID: 5238
		private readonly string sourceMachine;
	}
}
