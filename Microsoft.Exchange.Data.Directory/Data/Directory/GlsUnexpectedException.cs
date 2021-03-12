using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000ADE RID: 2782
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GlsUnexpectedException : GlsPermanentException
	{
		// Token: 0x06008110 RID: 33040 RVA: 0x001A627D File Offset: 0x001A447D
		public GlsUnexpectedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008111 RID: 33041 RVA: 0x001A6286 File Offset: 0x001A4486
		public GlsUnexpectedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008112 RID: 33042 RVA: 0x001A6290 File Offset: 0x001A4490
		protected GlsUnexpectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008113 RID: 33043 RVA: 0x001A629A File Offset: 0x001A449A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
