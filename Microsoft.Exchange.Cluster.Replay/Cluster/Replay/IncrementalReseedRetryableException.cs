using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002D3 RID: 723
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IncrementalReseedRetryableException : TransientException
	{
		// Token: 0x06001C8D RID: 7309 RVA: 0x0007DC7E File Offset: 0x0007BE7E
		public IncrementalReseedRetryableException(string error) : base(ReplayStrings.IncrementalReseedRetryableException(error))
		{
			this.error = error;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0007DC93 File Offset: 0x0007BE93
		public IncrementalReseedRetryableException(string error, Exception innerException) : base(ReplayStrings.IncrementalReseedRetryableException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0007DCA9 File Offset: 0x0007BEA9
		protected IncrementalReseedRetryableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0007DCD3 File Offset: 0x0007BED3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x0007DCEE File Offset: 0x0007BEEE
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000BD8 RID: 3032
		private readonly string error;
	}
}
