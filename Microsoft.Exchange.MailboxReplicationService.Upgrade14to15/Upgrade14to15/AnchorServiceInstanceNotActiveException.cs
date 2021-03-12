using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000EE RID: 238
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AnchorServiceInstanceNotActiveException : MigrationPermanentException
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x0001008D File Offset: 0x0000E28D
		public AnchorServiceInstanceNotActiveException() : base(UpgradeHandlerStrings.AnchorServiceInstanceNotActive)
		{
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001009A File Offset: 0x0000E29A
		public AnchorServiceInstanceNotActiveException(Exception innerException) : base(UpgradeHandlerStrings.AnchorServiceInstanceNotActive, innerException)
		{
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000100A8 File Offset: 0x0000E2A8
		protected AnchorServiceInstanceNotActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000100B2 File Offset: 0x0000E2B2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
