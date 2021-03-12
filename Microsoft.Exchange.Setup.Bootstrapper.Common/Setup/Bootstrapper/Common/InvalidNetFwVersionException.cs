using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x02000011 RID: 17
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidNetFwVersionException : LocalizedException
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00004E1F File Offset: 0x0000301F
		public InvalidNetFwVersionException() : base(Strings.InvalidNetFwVersion)
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004E2C File Offset: 0x0000302C
		public InvalidNetFwVersionException(Exception innerException) : base(Strings.InvalidNetFwVersion, innerException)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004E3A File Offset: 0x0000303A
		protected InvalidNetFwVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004E44 File Offset: 0x00003044
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
