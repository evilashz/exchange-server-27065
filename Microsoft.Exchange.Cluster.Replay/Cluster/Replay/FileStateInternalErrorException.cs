using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003D2 RID: 978
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileStateInternalErrorException : FileCheckException
	{
		// Token: 0x06002882 RID: 10370 RVA: 0x000B806D File Offset: 0x000B626D
		public FileStateInternalErrorException(string condition) : base(ReplayStrings.FileStateInternalError(condition))
		{
			this.condition = condition;
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000B8087 File Offset: 0x000B6287
		public FileStateInternalErrorException(string condition, Exception innerException) : base(ReplayStrings.FileStateInternalError(condition), innerException)
		{
			this.condition = condition;
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000B80A2 File Offset: 0x000B62A2
		protected FileStateInternalErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.condition = (string)info.GetValue("condition", typeof(string));
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000B80CC File Offset: 0x000B62CC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("condition", this.condition);
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002886 RID: 10374 RVA: 0x000B80E7 File Offset: 0x000B62E7
		public string Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x040013E1 RID: 5089
		private readonly string condition;
	}
}
