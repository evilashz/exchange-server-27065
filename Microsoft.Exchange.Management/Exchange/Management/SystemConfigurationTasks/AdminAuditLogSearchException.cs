using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	internal class AdminAuditLogSearchException : LocalizedException
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00007FE3 File Offset: 0x000061E3
		public AdminAuditLogSearchException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007FEC File Offset: 0x000061EC
		public AdminAuditLogSearchException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007FF6 File Offset: 0x000061F6
		public AdminAuditLogSearchException(LocalizedString message, AdminAuditLogSearch searchCriteria) : base(message)
		{
			this.searchCriteria = searchCriteria;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008006 File Offset: 0x00006206
		public AdminAuditLogSearchException(LocalizedString message, Exception innerException, AdminAuditLogSearch searchCriteria) : base(message, innerException)
		{
			this.searchCriteria = searchCriteria;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008017 File Offset: 0x00006217
		public AdminAuditLogSearchException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008024 File Offset: 0x00006224
		public override string ToString()
		{
			return base.ToString() + Strings.AdminAuditLogSearchCriteria((this.searchCriteria == null) ? string.Empty : this.searchCriteria.ToString());
		}

		// Token: 0x0400012A RID: 298
		private AdminAuditLogSearch searchCriteria;
	}
}
