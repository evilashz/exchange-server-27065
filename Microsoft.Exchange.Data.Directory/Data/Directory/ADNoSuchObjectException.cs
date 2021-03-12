using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A6C RID: 2668
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADNoSuchObjectException : ADOperationException
	{
		// Token: 0x06007F0D RID: 32525 RVA: 0x001A3D69 File Offset: 0x001A1F69
		public ADNoSuchObjectException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F0E RID: 32526 RVA: 0x001A3D72 File Offset: 0x001A1F72
		public ADNoSuchObjectException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F0F RID: 32527 RVA: 0x001A3D7C File Offset: 0x001A1F7C
		protected ADNoSuchObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F10 RID: 32528 RVA: 0x001A3D86 File Offset: 0x001A1F86
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
