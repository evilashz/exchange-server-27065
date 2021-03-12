using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AE2 RID: 2786
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidFederatedOrganizationIdException : ADOperationException
	{
		// Token: 0x06008121 RID: 33057 RVA: 0x001A6372 File Offset: 0x001A4572
		public InvalidFederatedOrganizationIdException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008122 RID: 33058 RVA: 0x001A637B File Offset: 0x001A457B
		public InvalidFederatedOrganizationIdException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008123 RID: 33059 RVA: 0x001A6385 File Offset: 0x001A4585
		protected InvalidFederatedOrganizationIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008124 RID: 33060 RVA: 0x001A638F File Offset: 0x001A458F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
