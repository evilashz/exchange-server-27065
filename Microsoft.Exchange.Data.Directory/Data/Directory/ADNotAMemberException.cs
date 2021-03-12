using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A6D RID: 2669
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADNotAMemberException : ADOperationException
	{
		// Token: 0x06007F11 RID: 32529 RVA: 0x001A3D90 File Offset: 0x001A1F90
		public ADNotAMemberException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F12 RID: 32530 RVA: 0x001A3D99 File Offset: 0x001A1F99
		public ADNotAMemberException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F13 RID: 32531 RVA: 0x001A3DA3 File Offset: 0x001A1FA3
		protected ADNotAMemberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F14 RID: 32532 RVA: 0x001A3DAD File Offset: 0x001A1FAD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
