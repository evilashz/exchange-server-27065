using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x0200013D RID: 317
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RowValidationException : ProcessingException
	{
		// Token: 0x060014D4 RID: 5332 RVA: 0x0006BA12 File Offset: 0x00069C12
		public RowValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0006BA1B File Offset: 0x00069C1B
		public RowValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0006BA25 File Offset: 0x00069C25
		protected RowValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0006BA2F File Offset: 0x00069C2F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
