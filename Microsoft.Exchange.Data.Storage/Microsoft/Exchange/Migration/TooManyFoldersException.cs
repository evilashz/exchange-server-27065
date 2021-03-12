using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000176 RID: 374
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TooManyFoldersException : MigrationPermanentException
	{
		// Token: 0x060016B2 RID: 5810 RVA: 0x0006FC3F File Offset: 0x0006DE3F
		public TooManyFoldersException() : base(Strings.TooManyFoldersStatus)
		{
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0006FC4C File Offset: 0x0006DE4C
		public TooManyFoldersException(Exception innerException) : base(Strings.TooManyFoldersStatus, innerException)
		{
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0006FC5A File Offset: 0x0006DE5A
		protected TooManyFoldersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0006FC64 File Offset: 0x0006DE64
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
