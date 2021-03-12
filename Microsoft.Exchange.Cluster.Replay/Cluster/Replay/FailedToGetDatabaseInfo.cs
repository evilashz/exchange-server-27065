using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004A4 RID: 1188
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToGetDatabaseInfo : TransientException
	{
		// Token: 0x06002CEE RID: 11502 RVA: 0x000C0525 File Offset: 0x000BE725
		public FailedToGetDatabaseInfo(int error) : base(ReplayStrings.FailedToGetDatabaseInfo(error))
		{
			this.error = error;
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000C053A File Offset: 0x000BE73A
		public FailedToGetDatabaseInfo(int error, Exception innerException) : base(ReplayStrings.FailedToGetDatabaseInfo(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000C0550 File Offset: 0x000BE750
		protected FailedToGetDatabaseInfo(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (int)info.GetValue("error", typeof(int));
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000C057A File Offset: 0x000BE77A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x000C0595 File Offset: 0x000BE795
		public int Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001505 RID: 5381
		private readonly int error;
	}
}
