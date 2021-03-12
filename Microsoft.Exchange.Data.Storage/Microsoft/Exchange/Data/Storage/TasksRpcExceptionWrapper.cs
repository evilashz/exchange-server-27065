using System;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200042A RID: 1066
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TasksRpcExceptionWrapper : HaRpcExceptionWrapperBase<TaskServerException, TaskServerTransientException>
	{
		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000C3D09 File Offset: 0x000C1F09
		public static TasksRpcExceptionWrapper Instance
		{
			get
			{
				return TasksRpcExceptionWrapper.s_tasksRpcWrapper;
			}
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000C3D10 File Offset: 0x000C1F10
		protected TasksRpcExceptionWrapper()
		{
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000C3D18 File Offset: 0x000C1F18
		protected override TaskServerException GetGenericOperationFailedException(string message)
		{
			return new TaskOperationFailedException(message);
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000C3D20 File Offset: 0x000C1F20
		protected override TaskServerException GetGenericOperationFailedException(string message, Exception innerException)
		{
			return new TaskOperationFailedException(message, innerException);
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000C3D29 File Offset: 0x000C1F29
		protected override TaskServerException GetGenericOperationFailedWithEcException(int errorCode)
		{
			return new TaskOperationFailedWithEcException(errorCode);
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000C3D31 File Offset: 0x000C1F31
		protected override TaskServerException GetServiceDownException(string serverName, Exception innerException)
		{
			return new ReplayServiceDownException(serverName, innerException.Message, innerException);
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000C3D40 File Offset: 0x000C1F40
		protected override TaskServerTransientException GetGenericOperationFailedTransientException(string message, Exception innerException)
		{
			return new TaskServerTransientException(message, innerException);
		}

		// Token: 0x04001A0B RID: 6667
		private static TasksRpcExceptionWrapper s_tasksRpcWrapper = new TasksRpcExceptionWrapper();
	}
}
