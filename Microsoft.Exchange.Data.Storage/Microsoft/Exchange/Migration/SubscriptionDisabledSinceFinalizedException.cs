using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000177 RID: 375
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SubscriptionDisabledSinceFinalizedException : MigrationPermanentException
	{
		// Token: 0x060016B6 RID: 5814 RVA: 0x0006FC6E File Offset: 0x0006DE6E
		public SubscriptionDisabledSinceFinalizedException() : base(Strings.SubscriptionDisabledSinceFinalized)
		{
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0006FC7B File Offset: 0x0006DE7B
		public SubscriptionDisabledSinceFinalizedException(Exception innerException) : base(Strings.SubscriptionDisabledSinceFinalized, innerException)
		{
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0006FC89 File Offset: 0x0006DE89
		protected SubscriptionDisabledSinceFinalizedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0006FC93 File Offset: 0x0006DE93
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
