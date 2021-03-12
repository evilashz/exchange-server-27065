using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002AC RID: 684
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederServerException : HaRpcServerBaseException
	{
		// Token: 0x06001AD3 RID: 6867 RVA: 0x0007376A File Offset: 0x0007196A
		public SeederServerException(string errorMessage) : base(ReplayStrings.SeederServerException(errorMessage))
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00073784 File Offset: 0x00071984
		public SeederServerException(string errorMessage, Exception innerException) : base(ReplayStrings.SeederServerException(errorMessage), innerException)
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0007379F File Offset: 0x0007199F
		protected SeederServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x000737A9 File Offset: 0x000719A9
		public override string ErrorMessage
		{
			get
			{
				return this.m_exceptionInfo.ErrorMessage;
			}
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000737B6 File Offset: 0x000719B6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
