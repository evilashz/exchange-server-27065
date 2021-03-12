using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000151 RID: 337
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MissingRequiredSubscriptionIdException : MigrationPermanentException
	{
		// Token: 0x060015FD RID: 5629 RVA: 0x0006EBA6 File Offset: 0x0006CDA6
		public MissingRequiredSubscriptionIdException() : base(Strings.MissingRequiredSubscriptionId)
		{
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x0006EBB3 File Offset: 0x0006CDB3
		public MissingRequiredSubscriptionIdException(Exception innerException) : base(Strings.MissingRequiredSubscriptionId, innerException)
		{
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0006EBC1 File Offset: 0x0006CDC1
		protected MissingRequiredSubscriptionIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0006EBCB File Offset: 0x0006CDCB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
