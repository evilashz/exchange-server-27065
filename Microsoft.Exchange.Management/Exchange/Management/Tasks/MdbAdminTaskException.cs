using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EF9 RID: 3833
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MdbAdminTaskException : LocalizedException
	{
		// Token: 0x0600A9C7 RID: 43463 RVA: 0x0028C51E File Offset: 0x0028A71E
		public MdbAdminTaskException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A9C8 RID: 43464 RVA: 0x0028C527 File Offset: 0x0028A727
		public MdbAdminTaskException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A9C9 RID: 43465 RVA: 0x0028C531 File Offset: 0x0028A731
		protected MdbAdminTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A9CA RID: 43466 RVA: 0x0028C53B File Offset: 0x0028A73B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
