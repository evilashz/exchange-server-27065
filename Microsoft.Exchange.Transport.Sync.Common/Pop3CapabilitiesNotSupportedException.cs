using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200003F RID: 63
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3CapabilitiesNotSupportedException : LocalizedException
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00005D74 File Offset: 0x00003F74
		public Pop3CapabilitiesNotSupportedException() : base(Strings.Pop3CapabilitiesNotSupportedException)
		{
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005D81 File Offset: 0x00003F81
		public Pop3CapabilitiesNotSupportedException(Exception innerException) : base(Strings.Pop3CapabilitiesNotSupportedException, innerException)
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005D8F File Offset: 0x00003F8F
		protected Pop3CapabilitiesNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005D99 File Offset: 0x00003F99
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
