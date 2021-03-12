using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004F8 RID: 1272
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeVolumeInfoInitException : DatabaseVolumeInfoException
	{
		// Token: 0x06002ECE RID: 11982 RVA: 0x000C41C5 File Offset: 0x000C23C5
		public ExchangeVolumeInfoInitException(string volumeName, string errMsg) : base(ReplayStrings.ExchangeVolumeInfoInitException(volumeName, errMsg))
		{
			this.volumeName = volumeName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000C41E7 File Offset: 0x000C23E7
		public ExchangeVolumeInfoInitException(string volumeName, string errMsg, Exception innerException) : base(ReplayStrings.ExchangeVolumeInfoInitException(volumeName, errMsg), innerException)
		{
			this.volumeName = volumeName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000C420C File Offset: 0x000C240C
		protected ExchangeVolumeInfoInitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000C4261 File Offset: 0x000C2461
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeName", this.volumeName);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x000C428D File Offset: 0x000C248D
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x000C4295 File Offset: 0x000C2495
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x04001595 RID: 5525
		private readonly string volumeName;

		// Token: 0x04001596 RID: 5526
		private readonly string errMsg;
	}
}
