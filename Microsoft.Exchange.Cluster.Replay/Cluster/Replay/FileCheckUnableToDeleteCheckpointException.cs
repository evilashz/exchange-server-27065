using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003CF RID: 975
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckUnableToDeleteCheckpointException : FileCheckException
	{
		// Token: 0x06002871 RID: 10353 RVA: 0x000B7E3F File Offset: 0x000B603F
		public FileCheckUnableToDeleteCheckpointException(string file, string errorMessage) : base(ReplayStrings.FileCheckUnableToDeleteCheckpointError(file, errorMessage))
		{
			this.file = file;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000B7E61 File Offset: 0x000B6061
		public FileCheckUnableToDeleteCheckpointException(string file, string errorMessage, Exception innerException) : base(ReplayStrings.FileCheckUnableToDeleteCheckpointError(file, errorMessage), innerException)
		{
			this.file = file;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000B7E84 File Offset: 0x000B6084
		protected FileCheckUnableToDeleteCheckpointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.file = (string)info.GetValue("file", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000B7ED9 File Offset: 0x000B60D9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("file", this.file);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x000B7F05 File Offset: 0x000B6105
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002876 RID: 10358 RVA: 0x000B7F0D File Offset: 0x000B610D
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x040013DC RID: 5084
		private readonly string file;

		// Token: 0x040013DD RID: 5085
		private readonly string errorMessage;
	}
}
