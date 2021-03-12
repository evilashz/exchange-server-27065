using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.UpdatableHelp
{
	// Token: 0x02000BF7 RID: 3063
	[Serializable]
	internal class UpdatableExchangeHelpSystemException : LocalizedException
	{
		// Token: 0x0600738A RID: 29578 RVA: 0x001D5DB1 File Offset: 0x001D3FB1
		internal UpdatableExchangeHelpSystemException(LocalizedString errorId, LocalizedString message, ErrorCategory cat, object targetObject, Exception innerException) : base(message, innerException)
		{
			this.FullyQualifiedErrorId = errorId;
			this.ErrorCategory = cat;
			this.TargetObject = targetObject;
		}

		// Token: 0x1700238F RID: 9103
		// (get) Token: 0x0600738B RID: 29579 RVA: 0x001D5DD7 File Offset: 0x001D3FD7
		// (set) Token: 0x0600738C RID: 29580 RVA: 0x001D5DDF File Offset: 0x001D3FDF
		internal string FullyQualifiedErrorId { get; private set; }

		// Token: 0x17002390 RID: 9104
		// (get) Token: 0x0600738D RID: 29581 RVA: 0x001D5DE8 File Offset: 0x001D3FE8
		// (set) Token: 0x0600738E RID: 29582 RVA: 0x001D5DF0 File Offset: 0x001D3FF0
		internal ErrorCategory ErrorCategory { get; private set; }

		// Token: 0x17002391 RID: 9105
		// (get) Token: 0x0600738F RID: 29583 RVA: 0x001D5DF9 File Offset: 0x001D3FF9
		// (set) Token: 0x06007390 RID: 29584 RVA: 0x001D5E01 File Offset: 0x001D4001
		internal object TargetObject { get; private set; }

		// Token: 0x06007391 RID: 29585 RVA: 0x001D5E0A File Offset: 0x001D400A
		internal ErrorRecord CreateErrorRecord()
		{
			return new ErrorRecord(new Exception(this.Message), this.FullyQualifiedErrorId, this.ErrorCategory, this.TargetObject);
		}
	}
}
