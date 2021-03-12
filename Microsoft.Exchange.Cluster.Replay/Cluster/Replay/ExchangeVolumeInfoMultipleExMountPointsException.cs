using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004F9 RID: 1273
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeVolumeInfoMultipleExMountPointsException : DatabaseVolumeInfoException
	{
		// Token: 0x06002ED4 RID: 11988 RVA: 0x000C429D File Offset: 0x000C249D
		public ExchangeVolumeInfoMultipleExMountPointsException(string volumeName, string exVolRootPath, string exMountPoints) : base(ReplayStrings.ExchangeVolumeInfoMultipleExMountPointsException(volumeName, exVolRootPath, exMountPoints))
		{
			this.volumeName = volumeName;
			this.exVolRootPath = exVolRootPath;
			this.exMountPoints = exMountPoints;
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000C42C7 File Offset: 0x000C24C7
		public ExchangeVolumeInfoMultipleExMountPointsException(string volumeName, string exVolRootPath, string exMountPoints, Exception innerException) : base(ReplayStrings.ExchangeVolumeInfoMultipleExMountPointsException(volumeName, exVolRootPath, exMountPoints), innerException)
		{
			this.volumeName = volumeName;
			this.exVolRootPath = exVolRootPath;
			this.exMountPoints = exMountPoints;
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000C42F4 File Offset: 0x000C24F4
		protected ExchangeVolumeInfoMultipleExMountPointsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
			this.exVolRootPath = (string)info.GetValue("exVolRootPath", typeof(string));
			this.exMountPoints = (string)info.GetValue("exMountPoints", typeof(string));
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000C4369 File Offset: 0x000C2569
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeName", this.volumeName);
			info.AddValue("exVolRootPath", this.exVolRootPath);
			info.AddValue("exMountPoints", this.exMountPoints);
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x000C43A6 File Offset: 0x000C25A6
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x000C43AE File Offset: 0x000C25AE
		public string ExVolRootPath
		{
			get
			{
				return this.exVolRootPath;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x000C43B6 File Offset: 0x000C25B6
		public string ExMountPoints
		{
			get
			{
				return this.exMountPoints;
			}
		}

		// Token: 0x04001597 RID: 5527
		private readonly string volumeName;

		// Token: 0x04001598 RID: 5528
		private readonly string exVolRootPath;

		// Token: 0x04001599 RID: 5529
		private readonly string exMountPoints;
	}
}
