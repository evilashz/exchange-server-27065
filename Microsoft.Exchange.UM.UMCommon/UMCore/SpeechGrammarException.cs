using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000218 RID: 536
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SpeechGrammarException : LocalizedException
	{
		// Token: 0x06001130 RID: 4400 RVA: 0x00039BED File Offset: 0x00037DED
		public SpeechGrammarException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00039BF6 File Offset: 0x00037DF6
		public SpeechGrammarException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00039C00 File Offset: 0x00037E00
		protected SpeechGrammarException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00039C0A File Offset: 0x00037E0A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
