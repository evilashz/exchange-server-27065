using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A8A RID: 2698
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotGetForestInfoException : ADExternalException
	{
		// Token: 0x06007F85 RID: 32645 RVA: 0x001A4213 File Offset: 0x001A2413
		public CannotGetForestInfoException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F86 RID: 32646 RVA: 0x001A421C File Offset: 0x001A241C
		public CannotGetForestInfoException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F87 RID: 32647 RVA: 0x001A4226 File Offset: 0x001A2426
		protected CannotGetForestInfoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F88 RID: 32648 RVA: 0x001A4230 File Offset: 0x001A2430
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
