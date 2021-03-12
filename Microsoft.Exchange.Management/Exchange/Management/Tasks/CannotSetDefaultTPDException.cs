using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010D9 RID: 4313
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSetDefaultTPDException : LocalizedException
	{
		// Token: 0x0600B32F RID: 45871 RVA: 0x0029AE2D File Offset: 0x0029902D
		public CannotSetDefaultTPDException() : base(Strings.CannotSetDefaultTPD)
		{
		}

		// Token: 0x0600B330 RID: 45872 RVA: 0x0029AE3A File Offset: 0x0029903A
		public CannotSetDefaultTPDException(Exception innerException) : base(Strings.CannotSetDefaultTPD, innerException)
		{
		}

		// Token: 0x0600B331 RID: 45873 RVA: 0x0029AE48 File Offset: 0x00299048
		protected CannotSetDefaultTPDException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B332 RID: 45874 RVA: 0x0029AE52 File Offset: 0x00299052
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
