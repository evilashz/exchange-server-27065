using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A7D RID: 2685
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotResolveExternalDirectoryOrganizationIdException : DataSourceOperationException
	{
		// Token: 0x06007F51 RID: 32593 RVA: 0x001A4000 File Offset: 0x001A2200
		public CannotResolveExternalDirectoryOrganizationIdException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F52 RID: 32594 RVA: 0x001A4009 File Offset: 0x001A2209
		public CannotResolveExternalDirectoryOrganizationIdException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F53 RID: 32595 RVA: 0x001A4013 File Offset: 0x001A2213
		protected CannotResolveExternalDirectoryOrganizationIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F54 RID: 32596 RVA: 0x001A401D File Offset: 0x001A221D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
