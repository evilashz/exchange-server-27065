using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x02000018 RID: 24
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UsedOnlySpaceEncryptionAttemptOnANonEmptyVolumeException : BitlockerUtilException
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00004B53 File Offset: 0x00002D53
		public UsedOnlySpaceEncryptionAttemptOnANonEmptyVolumeException(string volume) : base(DiskManagementStrings.UsedOnlySpaceEncryptionAttemptOnANonEmptyVolumeError(volume))
		{
			this.volume = volume;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004B6D File Offset: 0x00002D6D
		public UsedOnlySpaceEncryptionAttemptOnANonEmptyVolumeException(string volume, Exception innerException) : base(DiskManagementStrings.UsedOnlySpaceEncryptionAttemptOnANonEmptyVolumeError(volume), innerException)
		{
			this.volume = volume;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004B88 File Offset: 0x00002D88
		protected UsedOnlySpaceEncryptionAttemptOnANonEmptyVolumeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volume = (string)info.GetValue("volume", typeof(string));
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004BB2 File Offset: 0x00002DB2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volume", this.volume);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004BCD File Offset: 0x00002DCD
		public string Volume
		{
			get
			{
				return this.volume;
			}
		}

		// Token: 0x0400005A RID: 90
		private readonly string volume;
	}
}
