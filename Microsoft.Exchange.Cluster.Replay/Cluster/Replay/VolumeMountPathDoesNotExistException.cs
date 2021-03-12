using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000510 RID: 1296
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class VolumeMountPathDoesNotExistException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F65 RID: 12133 RVA: 0x000C57F7 File Offset: 0x000C39F7
		public VolumeMountPathDoesNotExistException() : base(ReplayStrings.VolumeMountPathDoesNotExistException)
		{
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x000C5809 File Offset: 0x000C3A09
		public VolumeMountPathDoesNotExistException(Exception innerException) : base(ReplayStrings.VolumeMountPathDoesNotExistException, innerException)
		{
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x000C581C File Offset: 0x000C3A1C
		protected VolumeMountPathDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000C5826 File Offset: 0x000C3A26
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
