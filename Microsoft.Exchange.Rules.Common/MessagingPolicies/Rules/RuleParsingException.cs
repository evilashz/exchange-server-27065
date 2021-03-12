using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000016 RID: 22
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RuleParsingException : LocalizedException
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00003277 File Offset: 0x00001477
		public RuleParsingException(string diagnostic, int lineNumber, int linePosition) : base(RulesStrings.RuleParsingError(diagnostic, lineNumber, linePosition))
		{
			this.diagnostic = diagnostic;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000329C File Offset: 0x0000149C
		public RuleParsingException(string diagnostic, int lineNumber, int linePosition, Exception innerException) : base(RulesStrings.RuleParsingError(diagnostic, lineNumber, linePosition), innerException)
		{
			this.diagnostic = diagnostic;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000032C4 File Offset: 0x000014C4
		protected RuleParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.diagnostic = (string)info.GetValue("diagnostic", typeof(string));
			this.lineNumber = (int)info.GetValue("lineNumber", typeof(int));
			this.linePosition = (int)info.GetValue("linePosition", typeof(int));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003339 File Offset: 0x00001539
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("diagnostic", this.diagnostic);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("linePosition", this.linePosition);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003376 File Offset: 0x00001576
		public string Diagnostic
		{
			get
			{
				return this.diagnostic;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000337E File Offset: 0x0000157E
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003386 File Offset: 0x00001586
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x04000026 RID: 38
		private readonly string diagnostic;

		// Token: 0x04000027 RID: 39
		private readonly int lineNumber;

		// Token: 0x04000028 RID: 40
		private readonly int linePosition;
	}
}
