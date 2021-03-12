using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200003E RID: 62
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3CannotConnectToServerException : LocalizedException
	{
		// Token: 0x060001CB RID: 459 RVA: 0x00005D45 File Offset: 0x00003F45
		public Pop3CannotConnectToServerException() : base(Strings.Pop3CannotConnectToServerException)
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005D52 File Offset: 0x00003F52
		public Pop3CannotConnectToServerException(Exception innerException) : base(Strings.Pop3CannotConnectToServerException, innerException)
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005D60 File Offset: 0x00003F60
		protected Pop3CannotConnectToServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005D6A File Offset: 0x00003F6A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
