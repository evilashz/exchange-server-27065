using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x0200000E RID: 14
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class VolumeLockedFindException : BitlockerUtilException
	{
		// Token: 0x0600004E RID: 78 RVA: 0x000044F7 File Offset: 0x000026F7
		public VolumeLockedFindException(string volumeId, string error) : base(DiskManagementStrings.VolumeLockedFindError(volumeId, error))
		{
			this.volumeId = volumeId;
			this.error = error;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004519 File Offset: 0x00002719
		public VolumeLockedFindException(string volumeId, string error, Exception innerException) : base(DiskManagementStrings.VolumeLockedFindError(volumeId, error), innerException)
		{
			this.volumeId = volumeId;
			this.error = error;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000453C File Offset: 0x0000273C
		protected VolumeLockedFindException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeId = (string)info.GetValue("volumeId", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004591 File Offset: 0x00002791
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeId", this.volumeId);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000045BD File Offset: 0x000027BD
		public string VolumeId
		{
			get
			{
				return this.volumeId;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000045C5 File Offset: 0x000027C5
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400004C RID: 76
		private readonly string volumeId;

		// Token: 0x0400004D RID: 77
		private readonly string error;
	}
}
