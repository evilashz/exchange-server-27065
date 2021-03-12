using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003D0 RID: 976
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckInternalErrorException : FileCheckException
	{
		// Token: 0x06002877 RID: 10359 RVA: 0x000B7F15 File Offset: 0x000B6115
		public FileCheckInternalErrorException(string condition) : base(ReplayStrings.FileCheckInternalError(condition))
		{
			this.condition = condition;
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000B7F2F File Offset: 0x000B612F
		public FileCheckInternalErrorException(string condition, Exception innerException) : base(ReplayStrings.FileCheckInternalError(condition), innerException)
		{
			this.condition = condition;
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000B7F4A File Offset: 0x000B614A
		protected FileCheckInternalErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.condition = (string)info.GetValue("condition", typeof(string));
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000B7F74 File Offset: 0x000B6174
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("condition", this.condition);
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600287B RID: 10363 RVA: 0x000B7F8F File Offset: 0x000B618F
		public string Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x040013DE RID: 5086
		private readonly string condition;
	}
}
