using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AD5 RID: 2773
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GlsMsaUserNotFoundException : PermanentGlobalException
	{
		// Token: 0x060080EC RID: 33004 RVA: 0x001A610E File Offset: 0x001A430E
		public GlsMsaUserNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060080ED RID: 33005 RVA: 0x001A6117 File Offset: 0x001A4317
		public GlsMsaUserNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060080EE RID: 33006 RVA: 0x001A6121 File Offset: 0x001A4321
		protected GlsMsaUserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060080EF RID: 33007 RVA: 0x001A612B File Offset: 0x001A432B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
