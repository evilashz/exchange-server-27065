using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000093 RID: 147
	[Serializable]
	public class ElcEwsException : Exception
	{
		// Token: 0x060005A2 RID: 1442 RVA: 0x0002B628 File Offset: 0x00029828
		public ElcEwsException()
		{
			this.ErrorType = ElcEwsErrorType.Unknown;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0002B637 File Offset: 0x00029837
		public ElcEwsException(string message)
		{
			this.errorMessage = message;
			this.ErrorType = ElcEwsErrorType.Unknown;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0002B64D File Offset: 0x0002984D
		public ElcEwsException(ElcEwsErrorType errorType)
		{
			this.ErrorType = errorType;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0002B65C File Offset: 0x0002985C
		public ElcEwsException(ElcEwsErrorType errorType, string message)
		{
			this.errorMessage = message;
			this.ErrorType = errorType;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0002B672 File Offset: 0x00029872
		public ElcEwsException(ElcEwsErrorType errorType, Exception innerException) : base(string.Empty, innerException)
		{
			this.ErrorType = errorType;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0002B687 File Offset: 0x00029887
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0002B68F File Offset: 0x0002988F
		public ElcEwsErrorType ErrorType { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0002B698 File Offset: 0x00029898
		public override string Message
		{
			get
			{
				return string.Concat(new object[]
				{
					"ELC EWS failed with error type: '",
					this.ErrorType,
					"'.",
					(!string.IsNullOrEmpty(this.errorMessage)) ? (" Message: " + this.errorMessage) : string.Empty,
					(base.InnerException != null) ? (" Details: " + base.InnerException.Message) : string.Empty
				});
			}
		}

		// Token: 0x04000437 RID: 1079
		private readonly string errorMessage;
	}
}
