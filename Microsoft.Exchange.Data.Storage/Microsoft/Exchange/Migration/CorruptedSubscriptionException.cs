using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200017F RID: 383
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CorruptedSubscriptionException : MigrationPermanentException
	{
		// Token: 0x060016D6 RID: 5846 RVA: 0x0006FDE6 File Offset: 0x0006DFE6
		public CorruptedSubscriptionException() : base(Strings.CorruptedSubscriptionStatus)
		{
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0006FDF3 File Offset: 0x0006DFF3
		public CorruptedSubscriptionException(Exception innerException) : base(Strings.CorruptedSubscriptionStatus, innerException)
		{
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0006FE01 File Offset: 0x0006E001
		protected CorruptedSubscriptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0006FE0B File Offset: 0x0006E00B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
