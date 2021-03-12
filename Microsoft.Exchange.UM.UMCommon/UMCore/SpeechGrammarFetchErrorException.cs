using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000219 RID: 537
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SpeechGrammarFetchErrorException : SpeechGrammarException
	{
		// Token: 0x06001134 RID: 4404 RVA: 0x00039C14 File Offset: 0x00037E14
		public SpeechGrammarFetchErrorException(string grammar) : base(Strings.SpeechGrammarFetchErrorException(grammar))
		{
			this.grammar = grammar;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00039C29 File Offset: 0x00037E29
		public SpeechGrammarFetchErrorException(string grammar, Exception innerException) : base(Strings.SpeechGrammarFetchErrorException(grammar), innerException)
		{
			this.grammar = grammar;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00039C3F File Offset: 0x00037E3F
		protected SpeechGrammarFetchErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.grammar = (string)info.GetValue("grammar", typeof(string));
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00039C69 File Offset: 0x00037E69
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("grammar", this.grammar);
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00039C84 File Offset: 0x00037E84
		public string Grammar
		{
			get
			{
				return this.grammar;
			}
		}

		// Token: 0x0400088E RID: 2190
		private readonly string grammar;
	}
}
