using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x02000012 RID: 18
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidVolumeIdException : BitlockerUtilException
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00004753 File Offset: 0x00002953
		public InvalidVolumeIdException(string volumeId) : base(DiskManagementStrings.InvalidVolumeIdError(volumeId))
		{
			this.volumeId = volumeId;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000476D File Offset: 0x0000296D
		public InvalidVolumeIdException(string volumeId, Exception innerException) : base(DiskManagementStrings.InvalidVolumeIdError(volumeId), innerException)
		{
			this.volumeId = volumeId;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004788 File Offset: 0x00002988
		protected InvalidVolumeIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeId = (string)info.GetValue("volumeId", typeof(string));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000047B2 File Offset: 0x000029B2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeId", this.volumeId);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000047CD File Offset: 0x000029CD
		public string VolumeId
		{
			get
			{
				return this.volumeId;
			}
		}

		// Token: 0x04000051 RID: 81
		private readonly string volumeId;
	}
}
