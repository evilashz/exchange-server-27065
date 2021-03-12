using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004C0 RID: 1216
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogTruncationException : LocalizedException
	{
		// Token: 0x06002D96 RID: 11670 RVA: 0x000C1BAA File Offset: 0x000BFDAA
		public LogTruncationException(string error) : base(ReplayStrings.LogTruncationException(error))
		{
			this.error = error;
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000C1BBF File Offset: 0x000BFDBF
		public LogTruncationException(string error, Exception innerException) : base(ReplayStrings.LogTruncationException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000C1BD5 File Offset: 0x000BFDD5
		protected LogTruncationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x000C1BFF File Offset: 0x000BFDFF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06002D9A RID: 11674 RVA: 0x000C1C1A File Offset: 0x000BFE1A
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400153D RID: 5437
		private readonly string error;
	}
}
