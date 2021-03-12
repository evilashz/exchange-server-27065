using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000054 RID: 84
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileIOonSourceException : LocalizedException
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0000979D File Offset: 0x0000799D
		public FileIOonSourceException(string serverName, string fileFullPath, string ioErrorMessage) : base(Strings.FileIOonSourceException(serverName, fileFullPath, ioErrorMessage))
		{
			this.serverName = serverName;
			this.fileFullPath = fileFullPath;
			this.ioErrorMessage = ioErrorMessage;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000097C2 File Offset: 0x000079C2
		public FileIOonSourceException(string serverName, string fileFullPath, string ioErrorMessage, Exception innerException) : base(Strings.FileIOonSourceException(serverName, fileFullPath, ioErrorMessage), innerException)
		{
			this.serverName = serverName;
			this.fileFullPath = fileFullPath;
			this.ioErrorMessage = ioErrorMessage;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000097EC File Offset: 0x000079EC
		protected FileIOonSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.fileFullPath = (string)info.GetValue("fileFullPath", typeof(string));
			this.ioErrorMessage = (string)info.GetValue("ioErrorMessage", typeof(string));
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009861 File Offset: 0x00007A61
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("fileFullPath", this.fileFullPath);
			info.AddValue("ioErrorMessage", this.ioErrorMessage);
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000989E File Offset: 0x00007A9E
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600029F RID: 671 RVA: 0x000098A6 File Offset: 0x00007AA6
		public string FileFullPath
		{
			get
			{
				return this.fileFullPath;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000098AE File Offset: 0x00007AAE
		public string IoErrorMessage
		{
			get
			{
				return this.ioErrorMessage;
			}
		}

		// Token: 0x04000170 RID: 368
		private readonly string serverName;

		// Token: 0x04000171 RID: 369
		private readonly string fileFullPath;

		// Token: 0x04000172 RID: 370
		private readonly string ioErrorMessage;
	}
}
