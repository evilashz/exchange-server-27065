using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200021C RID: 540
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SpeechGrammarLoadException : SpeechGrammarException
	{
		// Token: 0x06001143 RID: 4419 RVA: 0x00039D7C File Offset: 0x00037F7C
		public SpeechGrammarLoadException(string grammar) : base(Strings.SpeechGrammarLoadException(grammar))
		{
			this.grammar = grammar;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00039D91 File Offset: 0x00037F91
		public SpeechGrammarLoadException(string grammar, Exception innerException) : base(Strings.SpeechGrammarLoadException(grammar), innerException)
		{
			this.grammar = grammar;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00039DA7 File Offset: 0x00037FA7
		protected SpeechGrammarLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.grammar = (string)info.GetValue("grammar", typeof(string));
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00039DD1 File Offset: 0x00037FD1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("grammar", this.grammar);
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x00039DEC File Offset: 0x00037FEC
		public string Grammar
		{
			get
			{
				return this.grammar;
			}
		}

		// Token: 0x04000891 RID: 2193
		private readonly string grammar;
	}
}
