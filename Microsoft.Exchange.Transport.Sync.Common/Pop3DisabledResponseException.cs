using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000040 RID: 64
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3DisabledResponseException : LocalizedException
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00005DA3 File Offset: 0x00003FA3
		public Pop3DisabledResponseException() : base(Strings.Pop3DisabledResponseException)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public Pop3DisabledResponseException(Exception innerException) : base(Strings.Pop3DisabledResponseException, innerException)
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005DBE File Offset: 0x00003FBE
		protected Pop3DisabledResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005DC8 File Offset: 0x00003FC8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
