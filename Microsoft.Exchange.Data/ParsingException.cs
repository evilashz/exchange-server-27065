using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000DA RID: 218
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParsingException : LocalizedException
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x0001A705 File Offset: 0x00018905
		public ParsingException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001A70E File Offset: 0x0001890E
		public ParsingException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001A718 File Offset: 0x00018918
		protected ParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001A722 File Offset: 0x00018922
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
