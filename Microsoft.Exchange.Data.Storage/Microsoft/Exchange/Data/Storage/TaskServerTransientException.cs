using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Cluster;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000429 RID: 1065
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskServerTransientException : HaRpcServerTransientBaseException
	{
		// Token: 0x06002FB3 RID: 12211 RVA: 0x000C3CB3 File Offset: 0x000C1EB3
		public TaskServerTransientException(string errorMessage) : base(ServerStrings.TaskServerTransientException(errorMessage))
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000C3CCD File Offset: 0x000C1ECD
		public TaskServerTransientException(string errorMessage, Exception innerException) : base(ServerStrings.TaskServerTransientException(errorMessage), innerException)
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000C3CE8 File Offset: 0x000C1EE8
		protected TaskServerTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000C3CF2 File Offset: 0x000C1EF2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x000C3CFC File Offset: 0x000C1EFC
		public override string ErrorMessage
		{
			get
			{
				return this.m_exceptionInfo.ErrorMessage;
			}
		}
	}
}
