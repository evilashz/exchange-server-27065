using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000034 RID: 52
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FileCopierInsufficientDiskSpaceException : LocalizedException
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000D3A5 File Offset: 0x0000B5A5
		public FileCopierInsufficientDiskSpaceException() : base(Strings.InsufficientDiskSpace)
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000D3B2 File Offset: 0x0000B5B2
		public FileCopierInsufficientDiskSpaceException(Exception innerException) : base(Strings.InsufficientDiskSpace, innerException)
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		protected FileCopierInsufficientDiskSpaceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000D3CA File Offset: 0x0000B5CA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
