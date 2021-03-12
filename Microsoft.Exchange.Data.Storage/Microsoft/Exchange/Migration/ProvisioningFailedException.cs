using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000142 RID: 322
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ProvisioningFailedException : MigrationPermanentException
	{
		// Token: 0x060015B5 RID: 5557 RVA: 0x0006E56C File Offset: 0x0006C76C
		public ProvisioningFailedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0006E575 File Offset: 0x0006C775
		public ProvisioningFailedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0006E57F File Offset: 0x0006C77F
		protected ProvisioningFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0006E589 File Offset: 0x0006C789
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
