using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003C9 RID: 969
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckIOErrorException : FileCheckException
	{
		// Token: 0x06002851 RID: 10321 RVA: 0x000B7A89 File Offset: 0x000B5C89
		public FileCheckIOErrorException(string errorMessage) : base(ReplayStrings.FileCheckIOError(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000B7AA3 File Offset: 0x000B5CA3
		public FileCheckIOErrorException(string errorMessage, Exception innerException) : base(ReplayStrings.FileCheckIOError(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000B7ABE File Offset: 0x000B5CBE
		protected FileCheckIOErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000B7AE8 File Offset: 0x000B5CE8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x000B7B03 File Offset: 0x000B5D03
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x040013D4 RID: 5076
		private readonly string errorMessage;
	}
}
