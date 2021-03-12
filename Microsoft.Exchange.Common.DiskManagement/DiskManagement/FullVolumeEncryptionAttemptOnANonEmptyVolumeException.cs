using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x02000019 RID: 25
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FullVolumeEncryptionAttemptOnANonEmptyVolumeException : BitlockerUtilException
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00004BD5 File Offset: 0x00002DD5
		public FullVolumeEncryptionAttemptOnANonEmptyVolumeException(string volume) : base(DiskManagementStrings.FullVolumeEncryptionAttemptOnANonEmptyVolumeError(volume))
		{
			this.volume = volume;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004BEF File Offset: 0x00002DEF
		public FullVolumeEncryptionAttemptOnANonEmptyVolumeException(string volume, Exception innerException) : base(DiskManagementStrings.FullVolumeEncryptionAttemptOnANonEmptyVolumeError(volume), innerException)
		{
			this.volume = volume;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004C0A File Offset: 0x00002E0A
		protected FullVolumeEncryptionAttemptOnANonEmptyVolumeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volume = (string)info.GetValue("volume", typeof(string));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004C34 File Offset: 0x00002E34
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volume", this.volume);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004C4F File Offset: 0x00002E4F
		public string Volume
		{
			get
			{
				return this.volume;
			}
		}

		// Token: 0x0400005B RID: 91
		private readonly string volume;
	}
}
