using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002B9 RID: 697
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskTotalFailureException : TaskException
	{
		// Token: 0x0600190F RID: 6415 RVA: 0x0005CAE7 File Offset: 0x0005ACE7
		public TaskTotalFailureException(Type taskType, Exception rollbackException) : base(Strings.ExceptionRollbackFailed(taskType, rollbackException))
		{
			this.taskType = taskType;
			this.rollbackException = rollbackException;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0005CB04 File Offset: 0x0005AD04
		public TaskTotalFailureException(Type taskType, Exception rollbackException, Exception innerException) : base(Strings.ExceptionRollbackFailed(taskType, rollbackException), innerException)
		{
			this.taskType = taskType;
			this.rollbackException = rollbackException;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0005CB24 File Offset: 0x0005AD24
		protected TaskTotalFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.taskType = (Type)info.GetValue("taskType", typeof(Type));
			this.rollbackException = (Exception)info.GetValue("rollbackException", typeof(Exception));
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0005CB79 File Offset: 0x0005AD79
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("taskType", this.taskType);
			info.AddValue("rollbackException", this.rollbackException);
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x0005CBA5 File Offset: 0x0005ADA5
		public Type TaskType
		{
			get
			{
				return this.taskType;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x0005CBAD File Offset: 0x0005ADAD
		public Exception RollbackException
		{
			get
			{
				return this.rollbackException;
			}
		}

		// Token: 0x04000990 RID: 2448
		private readonly Type taskType;

		// Token: 0x04000991 RID: 2449
		private readonly Exception rollbackException;
	}
}
