using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x02000010 RID: 16
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidOSVersionException : LocalizedException
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public InvalidOSVersionException() : base(Strings.InvalidOSVersion)
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004DFD File Offset: 0x00002FFD
		public InvalidOSVersionException(Exception innerException) : base(Strings.InvalidOSVersion, innerException)
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004E0B File Offset: 0x0000300B
		protected InvalidOSVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004E15 File Offset: 0x00003015
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
