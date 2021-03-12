using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x0200013A RID: 314
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidDomainException : ProcessingException
	{
		// Token: 0x060014C8 RID: 5320 RVA: 0x0006B99D File Offset: 0x00069B9D
		public InvalidDomainException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0006B9A6 File Offset: 0x00069BA6
		public InvalidDomainException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0006B9B0 File Offset: 0x00069BB0
		protected InvalidDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0006B9BA File Offset: 0x00069BBA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
