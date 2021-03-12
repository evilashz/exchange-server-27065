using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000085 RID: 133
	internal class UserResultMapping
	{
		// Token: 0x06000376 RID: 886 RVA: 0x00015CD4 File Offset: 0x00013ED4
		internal UserResultMapping(string mailbox, CallContext callContext)
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "UserResultMapping constructor called for '{0}'.", mailbox);
			this.mailbox = mailbox;
			this.isValidSmtpAddress = SmtpAddress.IsValidSmtpAddress(mailbox);
			this.smtpAddress = (this.isValidSmtpAddress ? new SmtpAddress(mailbox) : SmtpAddress.Empty);
			this.smtpProxyAddress = (this.isValidSmtpAddress ? new SmtpProxyAddress(mailbox, true) : null);
			this.callContext = callContext;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00015D4B File Offset: 0x00013F4B
		internal string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00015D53 File Offset: 0x00013F53
		internal bool IsValidSmtpAddress
		{
			get
			{
				return this.isValidSmtpAddress;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00015D5B File Offset: 0x00013F5B
		internal SmtpAddress SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00015D63 File Offset: 0x00013F63
		internal SmtpProxyAddress SmtpProxyAddress
		{
			get
			{
				return this.smtpProxyAddress;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00015D6B File Offset: 0x00013F6B
		internal CallContext CallContext
		{
			get
			{
				return this.callContext;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00015D73 File Offset: 0x00013F73
		// (set) Token: 0x0600037D RID: 893 RVA: 0x00015D7B File Offset: 0x00013F7B
		internal ResultBase Result { get; set; }

		// Token: 0x04000311 RID: 785
		private string mailbox;

		// Token: 0x04000312 RID: 786
		private bool isValidSmtpAddress;

		// Token: 0x04000313 RID: 787
		private SmtpAddress smtpAddress;

		// Token: 0x04000314 RID: 788
		private SmtpProxyAddress smtpProxyAddress;

		// Token: 0x04000315 RID: 789
		private CallContext callContext;
	}
}
