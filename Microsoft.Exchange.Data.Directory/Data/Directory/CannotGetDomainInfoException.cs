using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A88 RID: 2696
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotGetDomainInfoException : ADExternalException
	{
		// Token: 0x06007F7D RID: 32637 RVA: 0x001A41C5 File Offset: 0x001A23C5
		public CannotGetDomainInfoException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F7E RID: 32638 RVA: 0x001A41CE File Offset: 0x001A23CE
		public CannotGetDomainInfoException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F7F RID: 32639 RVA: 0x001A41D8 File Offset: 0x001A23D8
		protected CannotGetDomainInfoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F80 RID: 32640 RVA: 0x001A41E2 File Offset: 0x001A23E2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
