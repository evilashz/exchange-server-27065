using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x02000145 RID: 325
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class GrammarGeneratorADException : LocalizedException
	{
		// Token: 0x06000D2E RID: 3374 RVA: 0x00052042 File Offset: 0x00050242
		public GrammarGeneratorADException() : base(Strings.GrammarGeneratorADException)
		{
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0005204F File Offset: 0x0005024F
		public GrammarGeneratorADException(Exception innerException) : base(Strings.GrammarGeneratorADException, innerException)
		{
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0005205D File Offset: 0x0005025D
		protected GrammarGeneratorADException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00052067 File Offset: 0x00050267
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
