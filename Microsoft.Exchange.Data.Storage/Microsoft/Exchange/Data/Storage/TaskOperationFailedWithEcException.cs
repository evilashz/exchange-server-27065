using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000C4 RID: 196
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskOperationFailedWithEcException : TaskServerException
	{
		// Token: 0x06001253 RID: 4691 RVA: 0x000677FE File Offset: 0x000659FE
		public TaskOperationFailedWithEcException(int ec) : base(ServerStrings.TaskOperationFailedWithEcException(ec))
		{
			this.ec = ec;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00067818 File Offset: 0x00065A18
		public TaskOperationFailedWithEcException(int ec, Exception innerException) : base(ServerStrings.TaskOperationFailedWithEcException(ec), innerException)
		{
			this.ec = ec;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00067833 File Offset: 0x00065A33
		protected TaskOperationFailedWithEcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ec = (int)info.GetValue("ec", typeof(int));
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0006785D File Offset: 0x00065A5D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ec", this.ec);
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00067878 File Offset: 0x00065A78
		public int Ec
		{
			get
			{
				return this.ec;
			}
		}

		// Token: 0x04000955 RID: 2389
		private readonly int ec;
	}
}
