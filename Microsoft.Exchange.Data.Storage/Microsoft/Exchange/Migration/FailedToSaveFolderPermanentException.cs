using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000149 RID: 329
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedToSaveFolderPermanentException : MigrationPermanentException
	{
		// Token: 0x060015D9 RID: 5593 RVA: 0x0006E90D File Offset: 0x0006CB0D
		public FailedToSaveFolderPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0006E916 File Offset: 0x0006CB16
		public FailedToSaveFolderPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0006E920 File Offset: 0x0006CB20
		protected FailedToSaveFolderPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0006E92A File Offset: 0x0006CB2A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
