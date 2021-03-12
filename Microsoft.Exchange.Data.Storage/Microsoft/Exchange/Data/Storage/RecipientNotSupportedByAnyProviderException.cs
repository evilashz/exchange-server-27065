using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000F8 RID: 248
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RecipientNotSupportedByAnyProviderException : StoragePermanentException
	{
		// Token: 0x06001368 RID: 4968 RVA: 0x000694B4 File Offset: 0x000676B4
		public RecipientNotSupportedByAnyProviderException() : base(ServerStrings.RecipientNotSupportedByAnyProviderException)
		{
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000694C1 File Offset: 0x000676C1
		public RecipientNotSupportedByAnyProviderException(Exception innerException) : base(ServerStrings.RecipientNotSupportedByAnyProviderException, innerException)
		{
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x000694CF File Offset: 0x000676CF
		protected RecipientNotSupportedByAnyProviderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x000694D9 File Offset: 0x000676D9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
