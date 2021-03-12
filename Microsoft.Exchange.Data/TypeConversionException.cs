using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000D6 RID: 214
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TypeConversionException : LocalizedException
	{
		// Token: 0x060007C4 RID: 1988 RVA: 0x0001A669 File Offset: 0x00018869
		public TypeConversionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001A672 File Offset: 0x00018872
		public TypeConversionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001A67C File Offset: 0x0001887C
		protected TypeConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001A686 File Offset: 0x00018886
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
