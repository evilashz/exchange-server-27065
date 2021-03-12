using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x02000015 RID: 21
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InsufficientDiskSpaceException : LocalizedException
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00004F1C File Offset: 0x0000311C
		public InsufficientDiskSpaceException() : base(Strings.InsufficientDiskSpace)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004F29 File Offset: 0x00003129
		public InsufficientDiskSpaceException(Exception innerException) : base(Strings.InsufficientDiskSpace, innerException)
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004F37 File Offset: 0x00003137
		protected InsufficientDiskSpaceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004F41 File Offset: 0x00003141
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
