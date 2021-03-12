using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004C7 RID: 1223
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RepairStateException : LocalizedException
	{
		// Token: 0x06002DC1 RID: 11713 RVA: 0x000C21A5 File Offset: 0x000C03A5
		public RepairStateException(string error) : base(ReplayStrings.RepairStateError(error))
		{
			this.error = error;
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000C21BA File Offset: 0x000C03BA
		public RepairStateException(string error, Exception innerException) : base(ReplayStrings.RepairStateError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000C21D0 File Offset: 0x000C03D0
		protected RepairStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000C21FA File Offset: 0x000C03FA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06002DC5 RID: 11717 RVA: 0x000C2215 File Offset: 0x000C0415
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400154C RID: 5452
		private readonly string error;
	}
}
