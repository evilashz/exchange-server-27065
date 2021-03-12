using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x02000139 RID: 313
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DuplicateUserException : ProcessingException
	{
		// Token: 0x060014C4 RID: 5316 RVA: 0x0006B976 File Offset: 0x00069B76
		public DuplicateUserException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0006B97F File Offset: 0x00069B7F
		public DuplicateUserException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0006B989 File Offset: 0x00069B89
		protected DuplicateUserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0006B993 File Offset: 0x00069B93
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
