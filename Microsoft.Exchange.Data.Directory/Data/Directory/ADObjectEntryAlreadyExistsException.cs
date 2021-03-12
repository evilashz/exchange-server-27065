using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A6B RID: 2667
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADObjectEntryAlreadyExistsException : ADOperationException
	{
		// Token: 0x06007F09 RID: 32521 RVA: 0x001A3D42 File Offset: 0x001A1F42
		public ADObjectEntryAlreadyExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F0A RID: 32522 RVA: 0x001A3D4B File Offset: 0x001A1F4B
		public ADObjectEntryAlreadyExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F0B RID: 32523 RVA: 0x001A3D55 File Offset: 0x001A1F55
		protected ADObjectEntryAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F0C RID: 32524 RVA: 0x001A3D5F File Offset: 0x001A1F5F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
