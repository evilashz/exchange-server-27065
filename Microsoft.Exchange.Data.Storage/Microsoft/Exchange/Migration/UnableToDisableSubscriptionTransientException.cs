using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000188 RID: 392
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnableToDisableSubscriptionTransientException : MigrationTransientException
	{
		// Token: 0x060016FD RID: 5885 RVA: 0x00070068 File Offset: 0x0006E268
		public UnableToDisableSubscriptionTransientException() : base(Strings.UnableToDisableSubscription)
		{
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00070075 File Offset: 0x0006E275
		public UnableToDisableSubscriptionTransientException(Exception innerException) : base(Strings.UnableToDisableSubscription, innerException)
		{
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00070083 File Offset: 0x0006E283
		protected UnableToDisableSubscriptionTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0007008D File Offset: 0x0006E28D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
