using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002AF RID: 687
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public class SeederInstanceAlreadyFailedException : SeedPrepareException
	{
		// Token: 0x06001AE2 RID: 6882 RVA: 0x00073898 File Offset: 0x00071A98
		internal SeederInstanceAlreadyFailedException(RpcSeederStatus seederStatus, string sourceMachine) : base(ReplayStrings.SeederInstanceAlreadyFailedException)
		{
			this.seederStatus = seederStatus;
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000738B1 File Offset: 0x00071AB1
		internal SeederInstanceAlreadyFailedException(RpcSeederStatus seederStatus, string sourceMachine, Exception innerException) : base(ReplayStrings.SeederInstanceAlreadyFailedException, innerException)
		{
			this.seederStatus = seederStatus;
			this.sourceMachine = sourceMachine;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000738D4 File Offset: 0x00071AD4
		protected SeederInstanceAlreadyFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.seederStatus = (RpcSeederStatus)info.GetValue("seederStatus", typeof(RpcSeederStatus));
			this.sourceMachine = (string)info.GetValue("sourceMachine", typeof(string));
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0007392C File Offset: 0x00071B2C
		public override string Message
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_message))
				{
					if (this.seederStatus == null || this.seederStatus.ErrorInfo == null || !this.seederStatus.ErrorInfo.IsFailed())
					{
						return base.Message;
					}
					this.m_message = HaRpcExceptionHelper.AppendLastErrorString(base.Message, this.seederStatus.ErrorInfo.ErrorMessage);
				}
				return this.m_message;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0007399D File Offset: 0x00071B9D
		internal RpcSeederStatus SeederStatus
		{
			get
			{
				return this.seederStatus;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x000739A5 File Offset: 0x00071BA5
		internal string SourceMachine
		{
			get
			{
				return this.sourceMachine;
			}
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x000739AD File Offset: 0x00071BAD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("seederStatus", this.seederStatus, typeof(RpcSeederStatus));
			info.AddValue("sourceMachine", this.sourceMachine, typeof(string));
		}

		// Token: 0x04000AC2 RID: 2754
		private string m_message;

		// Token: 0x04000AC3 RID: 2755
		private RpcSeederStatus seederStatus;

		// Token: 0x04000AC4 RID: 2756
		private string sourceMachine;
	}
}
