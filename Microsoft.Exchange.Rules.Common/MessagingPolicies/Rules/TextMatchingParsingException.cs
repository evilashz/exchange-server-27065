using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200005A RID: 90
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TextMatchingParsingException : LocalizedException
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000ABA2 File Offset: 0x00008DA2
		public TextMatchingParsingException(string diagnostic) : base(TextMatchingStrings.RegexPatternParsingError(diagnostic))
		{
			this.diagnostic = diagnostic;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000ABB7 File Offset: 0x00008DB7
		public TextMatchingParsingException(string diagnostic, Exception innerException) : base(TextMatchingStrings.RegexPatternParsingError(diagnostic), innerException)
		{
			this.diagnostic = diagnostic;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000ABCD File Offset: 0x00008DCD
		protected TextMatchingParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.diagnostic = (string)info.GetValue("diagnostic", typeof(string));
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000ABF7 File Offset: 0x00008DF7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("diagnostic", this.diagnostic);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000AC12 File Offset: 0x00008E12
		public string Diagnostic
		{
			get
			{
				return this.diagnostic;
			}
		}

		// Token: 0x0400014E RID: 334
		private readonly string diagnostic;
	}
}
