using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A75 RID: 2677
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GenerateUniqueLegacyDnException : ADOperationException
	{
		// Token: 0x06007F31 RID: 32561 RVA: 0x001A3EC8 File Offset: 0x001A20C8
		public GenerateUniqueLegacyDnException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F32 RID: 32562 RVA: 0x001A3ED1 File Offset: 0x001A20D1
		public GenerateUniqueLegacyDnException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F33 RID: 32563 RVA: 0x001A3EDB File Offset: 0x001A20DB
		protected GenerateUniqueLegacyDnException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F34 RID: 32564 RVA: 0x001A3EE5 File Offset: 0x001A20E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
