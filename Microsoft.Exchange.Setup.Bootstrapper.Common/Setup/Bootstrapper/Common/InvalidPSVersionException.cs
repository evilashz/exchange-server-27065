using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x02000012 RID: 18
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidPSVersionException : LocalizedException
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00004E4E File Offset: 0x0000304E
		public InvalidPSVersionException() : base(Strings.InvalidPSVersion)
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004E5B File Offset: 0x0000305B
		public InvalidPSVersionException(Exception innerException) : base(Strings.InvalidPSVersion, innerException)
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004E69 File Offset: 0x00003069
		protected InvalidPSVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004E73 File Offset: 0x00003073
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
