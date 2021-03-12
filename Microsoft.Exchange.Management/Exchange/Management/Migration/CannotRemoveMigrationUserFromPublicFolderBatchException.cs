using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001129 RID: 4393
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveMigrationUserFromPublicFolderBatchException : LocalizedException
	{
		// Token: 0x0600B4BE RID: 46270 RVA: 0x0029D4B1 File Offset: 0x0029B6B1
		public CannotRemoveMigrationUserFromPublicFolderBatchException() : base(Strings.ErrorCannotRemoveMigrationUserFromPublicFolderBatch)
		{
		}

		// Token: 0x0600B4BF RID: 46271 RVA: 0x0029D4BE File Offset: 0x0029B6BE
		public CannotRemoveMigrationUserFromPublicFolderBatchException(Exception innerException) : base(Strings.ErrorCannotRemoveMigrationUserFromPublicFolderBatch, innerException)
		{
		}

		// Token: 0x0600B4C0 RID: 46272 RVA: 0x0029D4CC File Offset: 0x0029B6CC
		protected CannotRemoveMigrationUserFromPublicFolderBatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B4C1 RID: 46273 RVA: 0x0029D4D6 File Offset: 0x0029B6D6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
