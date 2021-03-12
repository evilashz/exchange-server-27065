using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x0200001D RID: 29
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class Win7EmptyVolumeNotEncryptingAfterStartingEncryptionException : BitlockerUtilException
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00004E35 File Offset: 0x00003035
		public Win7EmptyVolumeNotEncryptingAfterStartingEncryptionException(string volume, string bitlockerState) : base(DiskManagementStrings.Win7EmptyVolumeNotEncryptingAfterStartingEncryptionError(volume, bitlockerState))
		{
			this.volume = volume;
			this.bitlockerState = bitlockerState;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004E57 File Offset: 0x00003057
		public Win7EmptyVolumeNotEncryptingAfterStartingEncryptionException(string volume, string bitlockerState, Exception innerException) : base(DiskManagementStrings.Win7EmptyVolumeNotEncryptingAfterStartingEncryptionError(volume, bitlockerState), innerException)
		{
			this.volume = volume;
			this.bitlockerState = bitlockerState;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004E7C File Offset: 0x0000307C
		protected Win7EmptyVolumeNotEncryptingAfterStartingEncryptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volume = (string)info.GetValue("volume", typeof(string));
			this.bitlockerState = (string)info.GetValue("bitlockerState", typeof(string));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004ED1 File Offset: 0x000030D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volume", this.volume);
			info.AddValue("bitlockerState", this.bitlockerState);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004EFD File Offset: 0x000030FD
		public string Volume
		{
			get
			{
				return this.volume;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004F05 File Offset: 0x00003105
		public string BitlockerState
		{
			get
			{
				return this.bitlockerState;
			}
		}

		// Token: 0x04000060 RID: 96
		private readonly string volume;

		// Token: 0x04000061 RID: 97
		private readonly string bitlockerState;
	}
}
