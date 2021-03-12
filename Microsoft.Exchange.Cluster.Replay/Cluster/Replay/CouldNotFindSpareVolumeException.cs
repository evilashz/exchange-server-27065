using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004FB RID: 1275
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotFindSpareVolumeException : DatabaseVolumeInfoException
	{
		// Token: 0x06002EE3 RID: 12003 RVA: 0x000C4539 File Offset: 0x000C2739
		public CouldNotFindSpareVolumeException(string databases) : base(ReplayStrings.CouldNotFindSpareVolumeException(databases))
		{
			this.databases = databases;
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000C4553 File Offset: 0x000C2753
		public CouldNotFindSpareVolumeException(string databases, Exception innerException) : base(ReplayStrings.CouldNotFindSpareVolumeException(databases), innerException)
		{
			this.databases = databases;
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000C456E File Offset: 0x000C276E
		protected CouldNotFindSpareVolumeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databases = (string)info.GetValue("databases", typeof(string));
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000C4598 File Offset: 0x000C2798
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databases", this.databases);
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x000C45B3 File Offset: 0x000C27B3
		public string Databases
		{
			get
			{
				return this.databases;
			}
		}

		// Token: 0x0400159E RID: 5534
		private readonly string databases;
	}
}
