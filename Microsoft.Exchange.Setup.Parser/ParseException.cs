using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Parser
{
	// Token: 0x02000013 RID: 19
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParseException : LocalizedException
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00004D61 File Offset: 0x00002F61
		public ParseException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004D6A File Offset: 0x00002F6A
		public ParseException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004D74 File Offset: 0x00002F74
		protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004D7E File Offset: 0x00002F7E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
