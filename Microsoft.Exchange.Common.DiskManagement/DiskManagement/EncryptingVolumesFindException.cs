using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x02000010 RID: 16
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EncryptingVolumesFindException : BitlockerUtilException
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000464F File Offset: 0x0000284F
		public EncryptingVolumesFindException(string error) : base(DiskManagementStrings.EncryptingVolumesFindError(error))
		{
			this.error = error;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004669 File Offset: 0x00002869
		public EncryptingVolumesFindException(string error, Exception innerException) : base(DiskManagementStrings.EncryptingVolumesFindError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004684 File Offset: 0x00002884
		protected EncryptingVolumesFindException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000046AE File Offset: 0x000028AE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000046C9 File Offset: 0x000028C9
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400004F RID: 79
		private readonly string error;
	}
}
