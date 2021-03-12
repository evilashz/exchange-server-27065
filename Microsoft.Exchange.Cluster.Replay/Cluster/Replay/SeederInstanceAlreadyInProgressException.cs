using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200043E RID: 1086
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederInstanceAlreadyInProgressException : SeedPrepareException
	{
		// Token: 0x06002AC5 RID: 10949 RVA: 0x000BC3AF File Offset: 0x000BA5AF
		public SeederInstanceAlreadyInProgressException(string sourceMachine) : base(ReplayStrings.SeederInstanceAlreadyInProgressException(sourceMachine))
		{
			this.sourceMachine = sourceMachine;
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000BC3C9 File Offset: 0x000BA5C9
		public SeederInstanceAlreadyInProgressException(string sourceMachine, Exception innerException) : base(ReplayStrings.SeederInstanceAlreadyInProgressException(sourceMachine), innerException)
		{
			this.sourceMachine = sourceMachine;
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000BC3E4 File Offset: 0x000BA5E4
		protected SeederInstanceAlreadyInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sourceMachine = (string)info.GetValue("sourceMachine", typeof(string));
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000BC40E File Offset: 0x000BA60E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sourceMachine", this.sourceMachine);
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x000BC429 File Offset: 0x000BA629
		public string SourceMachine
		{
			get
			{
				return this.sourceMachine;
			}
		}

		// Token: 0x04001474 RID: 5236
		private readonly string sourceMachine;
	}
}
