using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.AAD
{
	// Token: 0x020000F2 RID: 242
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AADException : LocalizedException
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x000162A3 File Offset: 0x000144A3
		public AADException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000162AC File Offset: 0x000144AC
		public AADException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000162B6 File Offset: 0x000144B6
		protected AADException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000162C0 File Offset: 0x000144C0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
