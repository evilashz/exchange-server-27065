using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000180 RID: 384
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SubscriptionMailboxNotFoundException : MigrationPermanentException
	{
		// Token: 0x060016DA RID: 5850 RVA: 0x0006FE15 File Offset: 0x0006E015
		public SubscriptionMailboxNotFoundException() : base(Strings.MailboxNotFoundSubscriptionStatus)
		{
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0006FE22 File Offset: 0x0006E022
		public SubscriptionMailboxNotFoundException(Exception innerException) : base(Strings.MailboxNotFoundSubscriptionStatus, innerException)
		{
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0006FE30 File Offset: 0x0006E030
		protected SubscriptionMailboxNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0006FE3A File Offset: 0x0006E03A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
