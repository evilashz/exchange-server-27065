using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000DC RID: 220
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskServerTransientException : HaRpcServerTransientBaseException
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x0002A45B File Offset: 0x0002865B
		public DagTaskServerTransientException(string errorMessage) : base(ReplayStrings.DagTaskServerTransientException(errorMessage))
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0002A475 File Offset: 0x00028675
		public DagTaskServerTransientException(string errorMessage, Exception innerException) : base(ReplayStrings.DagTaskServerTransientException(errorMessage), innerException)
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002A490 File Offset: 0x00028690
		protected DagTaskServerTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002A49A File Offset: 0x0002869A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0002A4A4 File Offset: 0x000286A4
		public override string ErrorMessage
		{
			get
			{
				return this.m_exceptionInfo.ErrorMessage;
			}
		}
	}
}
