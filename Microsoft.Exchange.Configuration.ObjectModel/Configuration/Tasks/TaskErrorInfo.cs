using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000A3 RID: 163
	public class TaskErrorInfo
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0001804F File Offset: 0x0001624F
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x00018057 File Offset: 0x00016257
		public ExchangeErrorCategory? ExchangeErrorCategory { get; private set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00018060 File Offset: 0x00016260
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00018068 File Offset: 0x00016268
		public object Target { get; private set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00018071 File Offset: 0x00016271
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x00018079 File Offset: 0x00016279
		public string HelpUrl { get; private set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00018082 File Offset: 0x00016282
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0001808A File Offset: 0x0001628A
		public bool TerminatePipeline
		{
			get
			{
				return this.terminatePipeline;
			}
			set
			{
				this.terminatePipeline = (this.terminatePipeline || value);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001809A File Offset: 0x0001629A
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x000180A2 File Offset: 0x000162A2
		public bool HasErrors { get; private set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x000180AB File Offset: 0x000162AB
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x000180B3 File Offset: 0x000162B3
		public Exception Exception { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x000180BC File Offset: 0x000162BC
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x000180C4 File Offset: 0x000162C4
		public bool IsKnownError { get; private set; }

		// Token: 0x06000677 RID: 1655 RVA: 0x000180CD File Offset: 0x000162CD
		public void SetErrorInfo(Exception exception, ExchangeErrorCategory errorCategory, object target, string helpUrl, bool terminatePipeline, bool isKnownError)
		{
			this.HasErrors = true;
			this.Exception = exception;
			this.ExchangeErrorCategory = new ExchangeErrorCategory?(errorCategory);
			this.Target = target;
			this.HelpUrl = helpUrl;
			this.TerminatePipeline = terminatePipeline;
			this.IsKnownError = isKnownError;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00018108 File Offset: 0x00016308
		public void ResetErrorInfo()
		{
			this.HasErrors = false;
			this.Exception = null;
			this.ExchangeErrorCategory = new ExchangeErrorCategory?((ExchangeErrorCategory)0);
			this.Target = null;
			this.HelpUrl = null;
			this.TerminatePipeline = false;
			this.IsKnownError = false;
		}

		// Token: 0x04000159 RID: 345
		private bool terminatePipeline;
	}
}
