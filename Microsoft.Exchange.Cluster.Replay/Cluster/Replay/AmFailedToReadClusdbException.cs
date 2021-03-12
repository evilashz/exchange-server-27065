using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000424 RID: 1060
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmFailedToReadClusdbException : ClusterException
	{
		// Token: 0x06002A32 RID: 10802 RVA: 0x000BB199 File Offset: 0x000B9399
		public AmFailedToReadClusdbException(string error) : base(ReplayStrings.AmFailedToReadClusdbException(error))
		{
			this.error = error;
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x000BB1B3 File Offset: 0x000B93B3
		public AmFailedToReadClusdbException(string error, Exception innerException) : base(ReplayStrings.AmFailedToReadClusdbException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000BB1CE File Offset: 0x000B93CE
		protected AmFailedToReadClusdbException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000BB1F8 File Offset: 0x000B93F8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002A36 RID: 10806 RVA: 0x000BB213 File Offset: 0x000B9413
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001449 RID: 5193
		private readonly string error;
	}
}
