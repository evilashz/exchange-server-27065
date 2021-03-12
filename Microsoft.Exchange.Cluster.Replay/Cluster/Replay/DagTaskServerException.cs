using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000DB RID: 219
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskServerException : HaRpcServerBaseException
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x0002A405 File Offset: 0x00028605
		public DagTaskServerException(string errorMessage) : base(ReplayStrings.DagTaskServerException(errorMessage))
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0002A41F File Offset: 0x0002861F
		public DagTaskServerException(string errorMessage, Exception innerException) : base(ReplayStrings.DagTaskServerException(errorMessage), innerException)
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0002A43A File Offset: 0x0002863A
		protected DagTaskServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0002A444 File Offset: 0x00028644
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0002A44E File Offset: 0x0002864E
		public override string ErrorMessage
		{
			get
			{
				return this.m_exceptionInfo.ErrorMessage;
			}
		}
	}
}
