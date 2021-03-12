using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x0200001A RID: 26
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class Win8EmptyVolumeNotFullyEncryptedAfterWaitException : BitlockerUtilException
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00004C57 File Offset: 0x00002E57
		public Win8EmptyVolumeNotFullyEncryptedAfterWaitException(string volume, int milliseconds, string bitlockerState) : base(DiskManagementStrings.Win8EmptyVolumeNotFullyEncryptedAfterWaitError(volume, milliseconds, bitlockerState))
		{
			this.volume = volume;
			this.milliseconds = milliseconds;
			this.bitlockerState = bitlockerState;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004C81 File Offset: 0x00002E81
		public Win8EmptyVolumeNotFullyEncryptedAfterWaitException(string volume, int milliseconds, string bitlockerState, Exception innerException) : base(DiskManagementStrings.Win8EmptyVolumeNotFullyEncryptedAfterWaitError(volume, milliseconds, bitlockerState), innerException)
		{
			this.volume = volume;
			this.milliseconds = milliseconds;
			this.bitlockerState = bitlockerState;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004CB0 File Offset: 0x00002EB0
		protected Win8EmptyVolumeNotFullyEncryptedAfterWaitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volume = (string)info.GetValue("volume", typeof(string));
			this.milliseconds = (int)info.GetValue("milliseconds", typeof(int));
			this.bitlockerState = (string)info.GetValue("bitlockerState", typeof(string));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004D25 File Offset: 0x00002F25
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volume", this.volume);
			info.AddValue("milliseconds", this.milliseconds);
			info.AddValue("bitlockerState", this.bitlockerState);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004D62 File Offset: 0x00002F62
		public string Volume
		{
			get
			{
				return this.volume;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004D6A File Offset: 0x00002F6A
		public int Milliseconds
		{
			get
			{
				return this.milliseconds;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004D72 File Offset: 0x00002F72
		public string BitlockerState
		{
			get
			{
				return this.bitlockerState;
			}
		}

		// Token: 0x0400005C RID: 92
		private readonly string volume;

		// Token: 0x0400005D RID: 93
		private readonly int milliseconds;

		// Token: 0x0400005E RID: 94
		private readonly string bitlockerState;
	}
}
