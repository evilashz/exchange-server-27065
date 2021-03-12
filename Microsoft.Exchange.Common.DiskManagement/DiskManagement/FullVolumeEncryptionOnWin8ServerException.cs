using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x02000016 RID: 22
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FullVolumeEncryptionOnWin8ServerException : BitlockerUtilException
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00004A4F File Offset: 0x00002C4F
		public FullVolumeEncryptionOnWin8ServerException(string volumeID) : base(DiskManagementStrings.FullVolumeEncryptionOnWin8ServerError(volumeID))
		{
			this.volumeID = volumeID;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004A69 File Offset: 0x00002C69
		public FullVolumeEncryptionOnWin8ServerException(string volumeID, Exception innerException) : base(DiskManagementStrings.FullVolumeEncryptionOnWin8ServerError(volumeID), innerException)
		{
			this.volumeID = volumeID;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004A84 File Offset: 0x00002C84
		protected FullVolumeEncryptionOnWin8ServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeID = (string)info.GetValue("volumeID", typeof(string));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004AAE File Offset: 0x00002CAE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeID", this.volumeID);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004AC9 File Offset: 0x00002CC9
		public string VolumeID
		{
			get
			{
				return this.volumeID;
			}
		}

		// Token: 0x04000058 RID: 88
		private readonly string volumeID;
	}
}
