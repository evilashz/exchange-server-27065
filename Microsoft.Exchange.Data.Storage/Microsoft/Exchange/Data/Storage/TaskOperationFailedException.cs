using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000C3 RID: 195
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskOperationFailedException : TaskServerException
	{
		// Token: 0x0600124E RID: 4686 RVA: 0x0006777C File Offset: 0x0006597C
		public TaskOperationFailedException(string errMessage) : base(ServerStrings.TaskOperationFailedException(errMessage))
		{
			this.errMessage = errMessage;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00067796 File Offset: 0x00065996
		public TaskOperationFailedException(string errMessage, Exception innerException) : base(ServerStrings.TaskOperationFailedException(errMessage), innerException)
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x000677B1 File Offset: 0x000659B1
		protected TaskOperationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x000677DB File Offset: 0x000659DB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x000677F6 File Offset: 0x000659F6
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x04000954 RID: 2388
		private readonly string errMessage;
	}
}
