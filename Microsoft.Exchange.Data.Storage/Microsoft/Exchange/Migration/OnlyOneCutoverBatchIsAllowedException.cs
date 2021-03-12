using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000167 RID: 359
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OnlyOneCutoverBatchIsAllowedException : MigrationPermanentException
	{
		// Token: 0x0600166C RID: 5740 RVA: 0x0006F687 File Offset: 0x0006D887
		public OnlyOneCutoverBatchIsAllowedException() : base(Strings.OnlyOneCutoverBatchIsAllowedError)
		{
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0006F694 File Offset: 0x0006D894
		public OnlyOneCutoverBatchIsAllowedException(Exception innerException) : base(Strings.OnlyOneCutoverBatchIsAllowedError, innerException)
		{
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0006F6A2 File Offset: 0x0006D8A2
		protected OnlyOneCutoverBatchIsAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x0006F6AC File Offset: 0x0006D8AC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
