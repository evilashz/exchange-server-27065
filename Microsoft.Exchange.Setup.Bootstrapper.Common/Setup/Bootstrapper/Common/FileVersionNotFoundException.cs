using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x02000014 RID: 20
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileVersionNotFoundException : LocalizedException
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00004EF5 File Offset: 0x000030F5
		public FileVersionNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004EFE File Offset: 0x000030FE
		public FileVersionNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004F08 File Offset: 0x00003108
		protected FileVersionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004F12 File Offset: 0x00003112
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
