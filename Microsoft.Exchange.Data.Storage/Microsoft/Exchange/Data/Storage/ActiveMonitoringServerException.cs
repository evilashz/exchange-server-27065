using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000123 RID: 291
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ActiveMonitoringServerException : HaRpcServerBaseException
	{
		// Token: 0x0600143B RID: 5179 RVA: 0x0006A6B0 File Offset: 0x000688B0
		public ActiveMonitoringServerException(string errorMessage) : base(ServerStrings.ActiveMonitoringServerException(errorMessage))
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0006A6CA File Offset: 0x000688CA
		public ActiveMonitoringServerException(string errorMessage, Exception innerException) : base(ServerStrings.ActiveMonitoringServerException(errorMessage), innerException)
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0006A6E5 File Offset: 0x000688E5
		protected ActiveMonitoringServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0006A6EF File Offset: 0x000688EF
		public override string ErrorMessage
		{
			get
			{
				return this.m_exceptionInfo.ErrorMessage;
			}
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0006A6FC File Offset: 0x000688FC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
