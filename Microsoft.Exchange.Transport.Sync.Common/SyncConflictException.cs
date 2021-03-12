using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200004F RID: 79
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SyncConflictException : TransientException
	{
		// Token: 0x06000218 RID: 536 RVA: 0x00006332 File Offset: 0x00004532
		public SyncConflictException() : base(Strings.SyncConflictException)
		{
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000633F File Offset: 0x0000453F
		public SyncConflictException(Exception innerException) : base(Strings.SyncConflictException, innerException)
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000634D File Offset: 0x0000454D
		protected SyncConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006357 File Offset: 0x00004557
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
