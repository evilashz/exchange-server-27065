using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003E8 RID: 1000
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskQuorumNotAchievedException : DagTaskServerException
	{
		// Token: 0x060028F8 RID: 10488 RVA: 0x000B8E35 File Offset: 0x000B7035
		public DagTaskQuorumNotAchievedException(string dagName) : base(ReplayStrings.DagTaskQuorumNotAchievedException(dagName))
		{
			this.dagName = dagName;
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000B8E4F File Offset: 0x000B704F
		public DagTaskQuorumNotAchievedException(string dagName, Exception innerException) : base(ReplayStrings.DagTaskQuorumNotAchievedException(dagName), innerException)
		{
			this.dagName = dagName;
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x000B8E6A File Offset: 0x000B706A
		protected DagTaskQuorumNotAchievedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000B8E94 File Offset: 0x000B7094
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x000B8EAF File Offset: 0x000B70AF
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x040013FF RID: 5119
		private readonly string dagName;
	}
}
