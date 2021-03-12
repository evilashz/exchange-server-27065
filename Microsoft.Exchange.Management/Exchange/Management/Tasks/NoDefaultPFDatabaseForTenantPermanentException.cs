using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EBB RID: 3771
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoDefaultPFDatabaseForTenantPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A883 RID: 43139 RVA: 0x0028A2E2 File Offset: 0x002884E2
		public NoDefaultPFDatabaseForTenantPermanentException() : base(Strings.ErrorNoDefaultPFDatabaseForTenant)
		{
		}

		// Token: 0x0600A884 RID: 43140 RVA: 0x0028A2EF File Offset: 0x002884EF
		public NoDefaultPFDatabaseForTenantPermanentException(Exception innerException) : base(Strings.ErrorNoDefaultPFDatabaseForTenant, innerException)
		{
		}

		// Token: 0x0600A885 RID: 43141 RVA: 0x0028A2FD File Offset: 0x002884FD
		protected NoDefaultPFDatabaseForTenantPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A886 RID: 43142 RVA: 0x0028A307 File Offset: 0x00288507
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
