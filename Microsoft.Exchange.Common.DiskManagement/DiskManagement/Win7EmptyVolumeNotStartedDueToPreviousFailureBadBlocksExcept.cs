using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x0200001E RID: 30
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class Win7EmptyVolumeNotStartedDueToPreviousFailureBadBlocksException : BitlockerUtilException
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00004F0D File Offset: 0x0000310D
		public Win7EmptyVolumeNotStartedDueToPreviousFailureBadBlocksException(string volume, string mountPoint, string eventXML) : base(DiskManagementStrings.Win7EmptyVolumeNotStartedDueToPreviousFailureBadBlocksError(volume, mountPoint, eventXML))
		{
			this.volume = volume;
			this.mountPoint = mountPoint;
			this.eventXML = eventXML;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004F37 File Offset: 0x00003137
		public Win7EmptyVolumeNotStartedDueToPreviousFailureBadBlocksException(string volume, string mountPoint, string eventXML, Exception innerException) : base(DiskManagementStrings.Win7EmptyVolumeNotStartedDueToPreviousFailureBadBlocksError(volume, mountPoint, eventXML), innerException)
		{
			this.volume = volume;
			this.mountPoint = mountPoint;
			this.eventXML = eventXML;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004F64 File Offset: 0x00003164
		protected Win7EmptyVolumeNotStartedDueToPreviousFailureBadBlocksException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volume = (string)info.GetValue("volume", typeof(string));
			this.mountPoint = (string)info.GetValue("mountPoint", typeof(string));
			this.eventXML = (string)info.GetValue("eventXML", typeof(string));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004FD9 File Offset: 0x000031D9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volume", this.volume);
			info.AddValue("mountPoint", this.mountPoint);
			info.AddValue("eventXML", this.eventXML);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00005016 File Offset: 0x00003216
		public string Volume
		{
			get
			{
				return this.volume;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000501E File Offset: 0x0000321E
		public string MountPoint
		{
			get
			{
				return this.mountPoint;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00005026 File Offset: 0x00003226
		public string EventXML
		{
			get
			{
				return this.eventXML;
			}
		}

		// Token: 0x04000062 RID: 98
		private readonly string volume;

		// Token: 0x04000063 RID: 99
		private readonly string mountPoint;

		// Token: 0x04000064 RID: 100
		private readonly string eventXML;
	}
}
