using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003E7 RID: 999
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskFileShareWitnessResourceIsStillNotOnlineException : DagTaskServerException
	{
		// Token: 0x060028F2 RID: 10482 RVA: 0x000B8D5D File Offset: 0x000B6F5D
		public DagTaskFileShareWitnessResourceIsStillNotOnlineException(string fswResource, string currentState) : base(ReplayStrings.DagTaskFileShareWitnessResourceIsStillNotOnlineException(fswResource, currentState))
		{
			this.fswResource = fswResource;
			this.currentState = currentState;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000B8D7F File Offset: 0x000B6F7F
		public DagTaskFileShareWitnessResourceIsStillNotOnlineException(string fswResource, string currentState, Exception innerException) : base(ReplayStrings.DagTaskFileShareWitnessResourceIsStillNotOnlineException(fswResource, currentState), innerException)
		{
			this.fswResource = fswResource;
			this.currentState = currentState;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000B8DA4 File Offset: 0x000B6FA4
		protected DagTaskFileShareWitnessResourceIsStillNotOnlineException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fswResource = (string)info.GetValue("fswResource", typeof(string));
			this.currentState = (string)info.GetValue("currentState", typeof(string));
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000B8DF9 File Offset: 0x000B6FF9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fswResource", this.fswResource);
			info.AddValue("currentState", this.currentState);
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x000B8E25 File Offset: 0x000B7025
		public string FswResource
		{
			get
			{
				return this.fswResource;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x000B8E2D File Offset: 0x000B702D
		public string CurrentState
		{
			get
			{
				return this.currentState;
			}
		}

		// Token: 0x040013FD RID: 5117
		private readonly string fswResource;

		// Token: 0x040013FE RID: 5118
		private readonly string currentState;
	}
}
