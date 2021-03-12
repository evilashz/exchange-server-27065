using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200011F RID: 287
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DefaultTaskGroupCreationException : StorageTransientException
	{
		// Token: 0x06001428 RID: 5160 RVA: 0x0006A514 File Offset: 0x00068714
		public DefaultTaskGroupCreationException() : base(ServerStrings.idUnableToCreateDefaultTaskGroupException)
		{
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0006A521 File Offset: 0x00068721
		public DefaultTaskGroupCreationException(Exception innerException) : base(ServerStrings.idUnableToCreateDefaultTaskGroupException, innerException)
		{
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0006A52F File Offset: 0x0006872F
		protected DefaultTaskGroupCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0006A539 File Offset: 0x00068739
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
