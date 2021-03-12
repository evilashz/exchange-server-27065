using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x0200013C RID: 316
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PermissionDeniedException : ProcessingException
	{
		// Token: 0x060014D0 RID: 5328 RVA: 0x0006B9EB File Offset: 0x00069BEB
		public PermissionDeniedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0006B9F4 File Offset: 0x00069BF4
		public PermissionDeniedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0006B9FE File Offset: 0x00069BFE
		protected PermissionDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0006BA08 File Offset: 0x00069C08
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
