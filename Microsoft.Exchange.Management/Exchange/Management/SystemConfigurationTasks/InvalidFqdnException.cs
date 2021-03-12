using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F73 RID: 3955
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidFqdnException : LocalizedException
	{
		// Token: 0x0600AC2B RID: 44075 RVA: 0x00290026 File Offset: 0x0028E226
		public InvalidFqdnException() : base(Strings.InvalidFqdn)
		{
		}

		// Token: 0x0600AC2C RID: 44076 RVA: 0x00290033 File Offset: 0x0028E233
		public InvalidFqdnException(Exception innerException) : base(Strings.InvalidFqdn, innerException)
		{
		}

		// Token: 0x0600AC2D RID: 44077 RVA: 0x00290041 File Offset: 0x0028E241
		protected InvalidFqdnException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC2E RID: 44078 RVA: 0x0029004B File Offset: 0x0028E24B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
