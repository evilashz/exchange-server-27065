using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Cluster;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000C2 RID: 194
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskServerException : HaRpcServerBaseException
	{
		// Token: 0x06001249 RID: 4681 RVA: 0x00067726 File Offset: 0x00065926
		public TaskServerException(string errorMessage) : base(ServerStrings.TaskServerException(errorMessage))
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00067740 File Offset: 0x00065940
		public TaskServerException(string errorMessage, Exception innerException) : base(ServerStrings.TaskServerException(errorMessage), innerException)
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0006775B File Offset: 0x0006595B
		protected TaskServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00067765 File Offset: 0x00065965
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0006776F File Offset: 0x0006596F
		public override string ErrorMessage
		{
			get
			{
				return this.m_exceptionInfo.ErrorMessage;
			}
		}
	}
}
