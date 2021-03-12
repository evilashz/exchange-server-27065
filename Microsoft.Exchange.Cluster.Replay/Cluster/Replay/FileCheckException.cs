using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003A3 RID: 931
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckException : LocalizedException
	{
		// Token: 0x0600277D RID: 10109 RVA: 0x000B6081 File Offset: 0x000B4281
		public FileCheckException(string error) : base(ReplayStrings.FileCheck(error))
		{
			this.error = error;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000B6096 File Offset: 0x000B4296
		public FileCheckException(string error, Exception innerException) : base(ReplayStrings.FileCheck(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000B60AC File Offset: 0x000B42AC
		protected FileCheckException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000B60D6 File Offset: 0x000B42D6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x000B60F1 File Offset: 0x000B42F1
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001398 RID: 5016
		private readonly string error;
	}
}
