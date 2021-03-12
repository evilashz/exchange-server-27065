using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000F4 RID: 244
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OrganizationNotFederatedException : StoragePermanentException
	{
		// Token: 0x06001355 RID: 4949 RVA: 0x00069310 File Offset: 0x00067510
		public OrganizationNotFederatedException() : base(ServerStrings.OrganizationNotFederatedException)
		{
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0006931D File Offset: 0x0006751D
		public OrganizationNotFederatedException(Exception innerException) : base(ServerStrings.OrganizationNotFederatedException, innerException)
		{
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0006932B File Offset: 0x0006752B
		protected OrganizationNotFederatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00069335 File Offset: 0x00067535
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
