using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001E3 RID: 483
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidSpeechGrammarFilterListException : LocalizedException
	{
		// Token: 0x06000F8E RID: 3982 RVA: 0x00036994 File Offset: 0x00034B94
		public InvalidSpeechGrammarFilterListException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0003699D File Offset: 0x00034B9D
		public InvalidSpeechGrammarFilterListException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000369A7 File Offset: 0x00034BA7
		protected InvalidSpeechGrammarFilterListException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x000369B1 File Offset: 0x00034BB1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
