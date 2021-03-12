using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000185 RID: 389
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SubscriptionNotFoundPermanentException : MigrationPermanentException
	{
		// Token: 0x060016F0 RID: 5872 RVA: 0x0006FF92 File Offset: 0x0006E192
		public SubscriptionNotFoundPermanentException() : base(Strings.SubscriptionNotFound)
		{
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0006FF9F File Offset: 0x0006E19F
		public SubscriptionNotFoundPermanentException(Exception innerException) : base(Strings.SubscriptionNotFound, innerException)
		{
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0006FFAD File Offset: 0x0006E1AD
		protected SubscriptionNotFoundPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0006FFB7 File Offset: 0x0006E1B7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
