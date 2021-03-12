using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200113A RID: 4410
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdminAuditLogException : ProvisioningException
	{
		// Token: 0x0600B508 RID: 46344 RVA: 0x0029D96E File Offset: 0x0029BB6E
		public AdminAuditLogException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B509 RID: 46345 RVA: 0x0029D977 File Offset: 0x0029BB77
		public AdminAuditLogException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B50A RID: 46346 RVA: 0x0029D981 File Offset: 0x0029BB81
		protected AdminAuditLogException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B50B RID: 46347 RVA: 0x0029D98B File Offset: 0x0029BB8B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
