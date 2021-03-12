using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004B9 RID: 1209
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileIOonSourceException : LocalizedException
	{
		// Token: 0x06002D6B RID: 11627 RVA: 0x000C15CD File Offset: 0x000BF7CD
		public FileIOonSourceException(string serverName, string fileFullPath, string ioErrorMessage) : base(ReplayStrings.FileIOonSourceException(serverName, fileFullPath, ioErrorMessage))
		{
			this.serverName = serverName;
			this.fileFullPath = fileFullPath;
			this.ioErrorMessage = ioErrorMessage;
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000C15F2 File Offset: 0x000BF7F2
		public FileIOonSourceException(string serverName, string fileFullPath, string ioErrorMessage, Exception innerException) : base(ReplayStrings.FileIOonSourceException(serverName, fileFullPath, ioErrorMessage), innerException)
		{
			this.serverName = serverName;
			this.fileFullPath = fileFullPath;
			this.ioErrorMessage = ioErrorMessage;
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000C161C File Offset: 0x000BF81C
		protected FileIOonSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.fileFullPath = (string)info.GetValue("fileFullPath", typeof(string));
			this.ioErrorMessage = (string)info.GetValue("ioErrorMessage", typeof(string));
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000C1691 File Offset: 0x000BF891
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("fileFullPath", this.fileFullPath);
			info.AddValue("ioErrorMessage", this.ioErrorMessage);
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x000C16CE File Offset: 0x000BF8CE
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x000C16D6 File Offset: 0x000BF8D6
		public string FileFullPath
		{
			get
			{
				return this.fileFullPath;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x000C16DE File Offset: 0x000BF8DE
		public string IoErrorMessage
		{
			get
			{
				return this.ioErrorMessage;
			}
		}

		// Token: 0x0400152E RID: 5422
		private readonly string serverName;

		// Token: 0x0400152F RID: 5423
		private readonly string fileFullPath;

		// Token: 0x04001530 RID: 5424
		private readonly string ioErrorMessage;
	}
}
