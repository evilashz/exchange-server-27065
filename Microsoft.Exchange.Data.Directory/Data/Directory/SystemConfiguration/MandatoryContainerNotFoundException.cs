using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A8F RID: 2703
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MandatoryContainerNotFoundException : ADExternalException
	{
		// Token: 0x06007F9E RID: 32670 RVA: 0x001A4471 File Offset: 0x001A2671
		public MandatoryContainerNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F9F RID: 32671 RVA: 0x001A447A File Offset: 0x001A267A
		public MandatoryContainerNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007FA0 RID: 32672 RVA: 0x001A4484 File Offset: 0x001A2684
		protected MandatoryContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007FA1 RID: 32673 RVA: 0x001A448E File Offset: 0x001A268E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
