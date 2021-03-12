using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A59 RID: 2649
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADPossibleOperationException : ADTransientException
	{
		// Token: 0x06007EBC RID: 32444 RVA: 0x001A38F0 File Offset: 0x001A1AF0
		public ADPossibleOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EBD RID: 32445 RVA: 0x001A38F9 File Offset: 0x001A1AF9
		public ADPossibleOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EBE RID: 32446 RVA: 0x001A3903 File Offset: 0x001A1B03
		protected ADPossibleOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007EBF RID: 32447 RVA: 0x001A390D File Offset: 0x001A1B0D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
