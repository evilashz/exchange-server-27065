using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004B8 RID: 1208
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileSharingViolationOnSourceException : LocalizedException
	{
		// Token: 0x06002D65 RID: 11621 RVA: 0x000C1500 File Offset: 0x000BF700
		public FileSharingViolationOnSourceException(string serverName, string fileFullPath) : base(ReplayStrings.FileSharingViolationOnSourceException(serverName, fileFullPath))
		{
			this.serverName = serverName;
			this.fileFullPath = fileFullPath;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000C151D File Offset: 0x000BF71D
		public FileSharingViolationOnSourceException(string serverName, string fileFullPath, Exception innerException) : base(ReplayStrings.FileSharingViolationOnSourceException(serverName, fileFullPath), innerException)
		{
			this.serverName = serverName;
			this.fileFullPath = fileFullPath;
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000C153C File Offset: 0x000BF73C
		protected FileSharingViolationOnSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.fileFullPath = (string)info.GetValue("fileFullPath", typeof(string));
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000C1591 File Offset: 0x000BF791
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("fileFullPath", this.fileFullPath);
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x000C15BD File Offset: 0x000BF7BD
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x000C15C5 File Offset: 0x000BF7C5
		public string FileFullPath
		{
			get
			{
				return this.fileFullPath;
			}
		}

		// Token: 0x0400152C RID: 5420
		private readonly string serverName;

		// Token: 0x0400152D RID: 5421
		private readonly string fileFullPath;
	}
}
