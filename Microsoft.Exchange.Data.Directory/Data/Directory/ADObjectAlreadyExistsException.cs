using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A6A RID: 2666
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADObjectAlreadyExistsException : ADOperationException
	{
		// Token: 0x06007F05 RID: 32517 RVA: 0x001A3D1B File Offset: 0x001A1F1B
		public ADObjectAlreadyExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F06 RID: 32518 RVA: 0x001A3D24 File Offset: 0x001A1F24
		public ADObjectAlreadyExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F07 RID: 32519 RVA: 0x001A3D2E File Offset: 0x001A1F2E
		protected ADObjectAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F08 RID: 32520 RVA: 0x001A3D38 File Offset: 0x001A1F38
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
