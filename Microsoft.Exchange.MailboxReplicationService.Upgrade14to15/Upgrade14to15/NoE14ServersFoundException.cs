using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000E6 RID: 230
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NoE14ServersFoundException : MigrationTransientException
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x0000FB15 File Offset: 0x0000DD15
		public NoE14ServersFoundException() : base(UpgradeHandlerStrings.ErrorNoE14ServersFound)
		{
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000FB22 File Offset: 0x0000DD22
		public NoE14ServersFoundException(Exception innerException) : base(UpgradeHandlerStrings.ErrorNoE14ServersFound, innerException)
		{
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0000FB30 File Offset: 0x0000DD30
		protected NoE14ServersFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0000FB3A File Offset: 0x0000DD3A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
