using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003CE RID: 974
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckIsamErrorException : FileCheckException
	{
		// Token: 0x0600286C RID: 10348 RVA: 0x000B7DBD File Offset: 0x000B5FBD
		public FileCheckIsamErrorException(string errorMessage) : base(ReplayStrings.FileCheckIsamError(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000B7DD7 File Offset: 0x000B5FD7
		public FileCheckIsamErrorException(string errorMessage, Exception innerException) : base(ReplayStrings.FileCheckIsamError(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000B7DF2 File Offset: 0x000B5FF2
		protected FileCheckIsamErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000B7E1C File Offset: 0x000B601C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x000B7E37 File Offset: 0x000B6037
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x040013DB RID: 5083
		private readonly string errorMessage;
	}
}
