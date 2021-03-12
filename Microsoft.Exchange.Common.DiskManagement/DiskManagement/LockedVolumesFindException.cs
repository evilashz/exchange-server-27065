using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x0200000F RID: 15
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LockedVolumesFindException : BitlockerUtilException
	{
		// Token: 0x06000054 RID: 84 RVA: 0x000045CD File Offset: 0x000027CD
		public LockedVolumesFindException(string error) : base(DiskManagementStrings.LockedVolumesFindError(error))
		{
			this.error = error;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000045E7 File Offset: 0x000027E7
		public LockedVolumesFindException(string error, Exception innerException) : base(DiskManagementStrings.LockedVolumesFindError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004602 File Offset: 0x00002802
		protected LockedVolumesFindException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000462C File Offset: 0x0000282C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004647 File Offset: 0x00002847
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400004E RID: 78
		private readonly string error;
	}
}
