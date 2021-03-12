using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	public class ExportException : Exception
	{
		// Token: 0x06000651 RID: 1617 RVA: 0x000175A6 File Offset: 0x000157A6
		public ExportException()
		{
			this.ErrorType = ExportErrorType.Unknown;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000175B5 File Offset: 0x000157B5
		public ExportException(string message)
		{
			this.errorMessage = message;
			this.ErrorType = ExportErrorType.Unknown;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000175CB File Offset: 0x000157CB
		public ExportException(ExportErrorType errorType)
		{
			this.ErrorType = errorType;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000175DA File Offset: 0x000157DA
		public ExportException(ExportErrorType errorType, string message)
		{
			this.errorMessage = message;
			this.ErrorType = errorType;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000175F0 File Offset: 0x000157F0
		public ExportException(ExportErrorType errorType, Exception innerException) : base(string.Empty, innerException)
		{
			this.ErrorType = errorType;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00017605 File Offset: 0x00015805
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0001760D File Offset: 0x0001580D
		public ExportErrorType ErrorType { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00017616 File Offset: 0x00015816
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x0001761E File Offset: 0x0001581E
		public Uri ServiceEndpoint { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00017627 File Offset: 0x00015827
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x0001762F File Offset: 0x0001582F
		public ServiceHttpContext ServiceHttpContext { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00017638 File Offset: 0x00015838
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x00017640 File Offset: 0x00015840
		public string ScenarioData { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001764C File Offset: 0x0001584C
		public override string Message
		{
			get
			{
				return string.Concat(new object[]
				{
					"Export failed with error type: '",
					this.ErrorType,
					"'.",
					(!string.IsNullOrEmpty(this.errorMessage)) ? (" Message: " + this.errorMessage) : string.Empty,
					(base.InnerException != null) ? (" Details: " + base.InnerException.Message) : string.Empty,
					(this.ServiceEndpoint != null) ? (" Endpoint: " + this.ServiceEndpoint.ToString()) : string.Empty,
					(this.ServiceHttpContext != null) ? (" HttpContext: " + this.ServiceHttpContext.ToString()) : string.Empty,
					(!string.IsNullOrWhiteSpace(this.ScenarioData)) ? (" ScenarioData: " + this.ScenarioData) : string.Empty
				});
			}
		}

		// Token: 0x040001E2 RID: 482
		private readonly string errorMessage;
	}
}
