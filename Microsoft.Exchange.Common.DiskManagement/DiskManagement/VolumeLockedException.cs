using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x0200001B RID: 27
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class VolumeLockedException : BitlockerUtilException
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00004D7A File Offset: 0x00002F7A
		public VolumeLockedException(string volume) : base(DiskManagementStrings.VolumeLockedError(volume))
		{
			this.volume = volume;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004D94 File Offset: 0x00002F94
		public VolumeLockedException(string volume, Exception innerException) : base(DiskManagementStrings.VolumeLockedError(volume), innerException)
		{
			this.volume = volume;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004DAF File Offset: 0x00002FAF
		protected VolumeLockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volume = (string)info.GetValue("volume", typeof(string));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004DD9 File Offset: 0x00002FD9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volume", this.volume);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004DF4 File Offset: 0x00002FF4
		public string Volume
		{
			get
			{
				return this.volume;
			}
		}

		// Token: 0x0400005F RID: 95
		private readonly string volume;
	}
}
