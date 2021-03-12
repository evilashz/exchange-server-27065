using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x02000011 RID: 17
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MountPointsFindException : BitlockerUtilException
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000046D1 File Offset: 0x000028D1
		public MountPointsFindException(string error) : base(DiskManagementStrings.MountpointsFindError(error))
		{
			this.error = error;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000046EB File Offset: 0x000028EB
		public MountPointsFindException(string error, Exception innerException) : base(DiskManagementStrings.MountpointsFindError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004706 File Offset: 0x00002906
		protected MountPointsFindException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004730 File Offset: 0x00002930
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000474B File Offset: 0x0000294B
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000050 RID: 80
		private readonly string error;
	}
}
