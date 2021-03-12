using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AD6 RID: 2774
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GlsMsaUserAlreadyExistsException : PermanentGlobalException
	{
		// Token: 0x060080F0 RID: 33008 RVA: 0x001A6135 File Offset: 0x001A4335
		public GlsMsaUserAlreadyExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060080F1 RID: 33009 RVA: 0x001A613E File Offset: 0x001A433E
		public GlsMsaUserAlreadyExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060080F2 RID: 33010 RVA: 0x001A6148 File Offset: 0x001A4348
		protected GlsMsaUserAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060080F3 RID: 33011 RVA: 0x001A6152 File Offset: 0x001A4352
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
