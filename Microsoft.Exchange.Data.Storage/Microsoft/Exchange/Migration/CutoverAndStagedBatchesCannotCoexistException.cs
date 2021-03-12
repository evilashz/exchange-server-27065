using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000165 RID: 357
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CutoverAndStagedBatchesCannotCoexistException : MigrationPermanentException
	{
		// Token: 0x06001664 RID: 5732 RVA: 0x0006F629 File Offset: 0x0006D829
		public CutoverAndStagedBatchesCannotCoexistException() : base(Strings.CutoverAndStagedBatchesCannotCoexistError)
		{
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0006F636 File Offset: 0x0006D836
		public CutoverAndStagedBatchesCannotCoexistException(Exception innerException) : base(Strings.CutoverAndStagedBatchesCannotCoexistError, innerException)
		{
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0006F644 File Offset: 0x0006D844
		protected CutoverAndStagedBatchesCannotCoexistException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0006F64E File Offset: 0x0006D84E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
