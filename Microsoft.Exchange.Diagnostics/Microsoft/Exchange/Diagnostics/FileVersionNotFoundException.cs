using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000415 RID: 1045
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileVersionNotFoundException : LocalizedException
	{
		// Token: 0x06001942 RID: 6466 RVA: 0x0005F78D File Offset: 0x0005D98D
		public FileVersionNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0005F796 File Offset: 0x0005D996
		public FileVersionNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0005F7A0 File Offset: 0x0005D9A0
		protected FileVersionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0005F7AA File Offset: 0x0005D9AA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
