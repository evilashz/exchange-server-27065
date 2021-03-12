using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000436 RID: 1078
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileOpenException : TransientException
	{
		// Token: 0x06002A9A RID: 10906 RVA: 0x000BBE9A File Offset: 0x000BA09A
		public FileOpenException(string fileName, string errMsg) : base(ReplayStrings.FileOpenError(fileName, errMsg))
		{
			this.fileName = fileName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000BBEB7 File Offset: 0x000BA0B7
		public FileOpenException(string fileName, string errMsg, Exception innerException) : base(ReplayStrings.FileOpenError(fileName, errMsg), innerException)
		{
			this.fileName = fileName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000BBED8 File Offset: 0x000BA0D8
		protected FileOpenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileName = (string)info.GetValue("fileName", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000BBF2D File Offset: 0x000BA12D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fileName", this.fileName);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x000BBF59 File Offset: 0x000BA159
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x000BBF61 File Offset: 0x000BA161
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x04001469 RID: 5225
		private readonly string fileName;

		// Token: 0x0400146A RID: 5226
		private readonly string errMsg;
	}
}
