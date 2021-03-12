using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E12 RID: 3602
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RoleDoesNotSupportExchangeCertificateTasksException : LocalizedException
	{
		// Token: 0x0600A55B RID: 42331 RVA: 0x00285E39 File Offset: 0x00284039
		public RoleDoesNotSupportExchangeCertificateTasksException() : base(Strings.RoleDoesNotSupportExchangeCertificateTasksException)
		{
		}

		// Token: 0x0600A55C RID: 42332 RVA: 0x00285E46 File Offset: 0x00284046
		public RoleDoesNotSupportExchangeCertificateTasksException(Exception innerException) : base(Strings.RoleDoesNotSupportExchangeCertificateTasksException, innerException)
		{
		}

		// Token: 0x0600A55D RID: 42333 RVA: 0x00285E54 File Offset: 0x00284054
		protected RoleDoesNotSupportExchangeCertificateTasksException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A55E RID: 42334 RVA: 0x00285E5E File Offset: 0x0028405E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
