using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000129 RID: 297
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ActiveMonitoringServerTransientException : HaRpcServerTransientBaseException
	{
		// Token: 0x06001462 RID: 5218 RVA: 0x0006AC80 File Offset: 0x00068E80
		public ActiveMonitoringServerTransientException(string errorMessage) : base(ServerStrings.ActiveMonitoringServerTransientException(errorMessage))
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0006AC9A File Offset: 0x00068E9A
		public ActiveMonitoringServerTransientException(string errorMessage, Exception innerException) : base(ServerStrings.ActiveMonitoringServerTransientException(errorMessage), innerException)
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0006ACB5 File Offset: 0x00068EB5
		protected ActiveMonitoringServerTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0006ACBF File Offset: 0x00068EBF
		public override string ErrorMessage
		{
			get
			{
				return this.m_exceptionInfo.ErrorMessage;
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0006ACCC File Offset: 0x00068ECC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
