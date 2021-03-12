using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x02000015 RID: 21
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UsedOnlyEncryptionOnNonWin8ServerException : BitlockerUtilException
	{
		// Token: 0x06000075 RID: 117 RVA: 0x000049CD File Offset: 0x00002BCD
		public UsedOnlyEncryptionOnNonWin8ServerException(string volumeID) : base(DiskManagementStrings.UsedOnlyEncryptionOnNonWin8ServerError(volumeID))
		{
			this.volumeID = volumeID;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000049E7 File Offset: 0x00002BE7
		public UsedOnlyEncryptionOnNonWin8ServerException(string volumeID, Exception innerException) : base(DiskManagementStrings.UsedOnlyEncryptionOnNonWin8ServerError(volumeID), innerException)
		{
			this.volumeID = volumeID;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004A02 File Offset: 0x00002C02
		protected UsedOnlyEncryptionOnNonWin8ServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeID = (string)info.GetValue("volumeID", typeof(string));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004A2C File Offset: 0x00002C2C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeID", this.volumeID);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004A47 File Offset: 0x00002C47
		public string VolumeID
		{
			get
			{
				return this.volumeID;
			}
		}

		// Token: 0x04000057 RID: 87
		private readonly string volumeID;
	}
}
