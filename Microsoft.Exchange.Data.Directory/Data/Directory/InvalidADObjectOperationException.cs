using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A69 RID: 2665
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidADObjectOperationException : InvalidObjectOperationException
	{
		// Token: 0x06007F01 RID: 32513 RVA: 0x001A3CF4 File Offset: 0x001A1EF4
		public InvalidADObjectOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F02 RID: 32514 RVA: 0x001A3CFD File Offset: 0x001A1EFD
		public InvalidADObjectOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F03 RID: 32515 RVA: 0x001A3D07 File Offset: 0x001A1F07
		protected InvalidADObjectOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F04 RID: 32516 RVA: 0x001A3D11 File Offset: 0x001A1F11
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
