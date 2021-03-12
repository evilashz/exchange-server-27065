using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Common.LocStrings
{
	// Token: 0x020002A3 RID: 675
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReadOnlyPropertyBagException : LocalizedException
	{
		// Token: 0x060018A3 RID: 6307 RVA: 0x0005C105 File Offset: 0x0005A305
		public ReadOnlyPropertyBagException() : base(Strings.ExceptionReadOnlyPropertyBag)
		{
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0005C112 File Offset: 0x0005A312
		public ReadOnlyPropertyBagException(Exception innerException) : base(Strings.ExceptionReadOnlyPropertyBag, innerException)
		{
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0005C120 File Offset: 0x0005A320
		protected ReadOnlyPropertyBagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0005C12A File Offset: 0x0005A32A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
