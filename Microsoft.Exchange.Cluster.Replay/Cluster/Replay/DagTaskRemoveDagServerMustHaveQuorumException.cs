using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003E4 RID: 996
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskRemoveDagServerMustHaveQuorumException : DagTaskServerException
	{
		// Token: 0x060028E0 RID: 10464 RVA: 0x000B8AE4 File Offset: 0x000B6CE4
		public DagTaskRemoveDagServerMustHaveQuorumException(string dagName) : base(ReplayStrings.DagTaskRemoveDagServerMustHaveQuorumException(dagName))
		{
			this.dagName = dagName;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000B8AFE File Offset: 0x000B6CFE
		public DagTaskRemoveDagServerMustHaveQuorumException(string dagName, Exception innerException) : base(ReplayStrings.DagTaskRemoveDagServerMustHaveQuorumException(dagName), innerException)
		{
			this.dagName = dagName;
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000B8B19 File Offset: 0x000B6D19
		protected DagTaskRemoveDagServerMustHaveQuorumException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000B8B43 File Offset: 0x000B6D43
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060028E4 RID: 10468 RVA: 0x000B8B5E File Offset: 0x000B6D5E
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x040013F7 RID: 5111
		private readonly string dagName;
	}
}
