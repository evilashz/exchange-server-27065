using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000104 RID: 260
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class BulkProvisioningTransientException : TransientException
	{
		// Token: 0x0600139E RID: 5022 RVA: 0x000698A5 File Offset: 0x00067AA5
		public BulkProvisioningTransientException() : base(ServerStrings.OperationalError)
		{
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000698B2 File Offset: 0x00067AB2
		public BulkProvisioningTransientException(Exception innerException) : base(ServerStrings.OperationalError, innerException)
		{
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x000698C0 File Offset: 0x00067AC0
		protected BulkProvisioningTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000698CA File Offset: 0x00067ACA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
