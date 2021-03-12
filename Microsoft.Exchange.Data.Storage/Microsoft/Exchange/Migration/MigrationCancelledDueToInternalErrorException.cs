using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000173 RID: 371
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationCancelledDueToInternalErrorException : MigrationPermanentException
	{
		// Token: 0x060016A4 RID: 5796 RVA: 0x0006FB20 File Offset: 0x0006DD20
		public MigrationCancelledDueToInternalErrorException() : base(Strings.MigrationCancelledDueToInternalError)
		{
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0006FB2D File Offset: 0x0006DD2D
		public MigrationCancelledDueToInternalErrorException(Exception innerException) : base(Strings.MigrationCancelledDueToInternalError, innerException)
		{
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0006FB3B File Offset: 0x0006DD3B
		protected MigrationCancelledDueToInternalErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0006FB45 File Offset: 0x0006DD45
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
