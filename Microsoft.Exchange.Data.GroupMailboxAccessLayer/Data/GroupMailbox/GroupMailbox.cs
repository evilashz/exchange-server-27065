using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000049 RID: 73
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GroupMailbox : ILocatableMailbox
	{
		// Token: 0x06000236 RID: 566 RVA: 0x0000EA26 File Offset: 0x0000CC26
		public GroupMailbox(GroupMailboxLocator locator)
		{
			this.Locator = locator;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000EA35 File Offset: 0x0000CC35
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000EA3D File Offset: 0x0000CC3D
		public IMailboxLocator Locator { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000EA46 File Offset: 0x0000CC46
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000EA4E File Offset: 0x0000CC4E
		public string Alias { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000EA57 File Offset: 0x0000CC57
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000EA5F File Offset: 0x0000CC5F
		public string DisplayName { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000EA68 File Offset: 0x0000CC68
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000EA70 File Offset: 0x0000CC70
		public SmtpAddress SmtpAddress { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000EA79 File Offset: 0x0000CC79
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000EA81 File Offset: 0x0000CC81
		public string Description { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000EA8A File Offset: 0x0000CC8A
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000EA92 File Offset: 0x0000CC92
		public ModernGroupObjectType Type { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000EA9B File Offset: 0x0000CC9B
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000EAA3 File Offset: 0x0000CCA3
		public IList<ADObjectId> Owners { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000EAAC File Offset: 0x0000CCAC
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
		public bool IsPinned { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000EABD File Offset: 0x0000CCBD
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000EAC5 File Offset: 0x0000CCC5
		public bool IsMember { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000EACE File Offset: 0x0000CCCE
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000EAD6 File Offset: 0x0000CCD6
		public string JoinedBy { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000EADF File Offset: 0x0000CCDF
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000EAE7 File Offset: 0x0000CCE7
		public ExDateTime JoinDate { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		public ExDateTime PinDate { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000EB01 File Offset: 0x0000CD01
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000EB09 File Offset: 0x0000CD09
		public Uri SharePointUrl { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000EB12 File Offset: 0x0000CD12
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000EB1A File Offset: 0x0000CD1A
		public string SharePointSiteUrl { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000EB23 File Offset: 0x0000CD23
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000EB2B File Offset: 0x0000CD2B
		public string SharePointDocumentsUrl { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000EB34 File Offset: 0x0000CD34
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		public bool RequireSenderAuthenticationEnabled { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000EB45 File Offset: 0x0000CD45
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000EB4D File Offset: 0x0000CD4D
		public bool AutoSubscribeNewGroupMembers { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000EB56 File Offset: 0x0000CD56
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000EB5E File Offset: 0x0000CD5E
		public CultureInfo Language { get; set; }

		// Token: 0x0600025B RID: 603 RVA: 0x0000EB68 File Offset: 0x0000CD68
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("Locator={");
			stringBuilder.Append(this.Locator);
			stringBuilder.Append("}, Alias=");
			stringBuilder.Append(this.Alias);
			stringBuilder.Append(", DisplayName=");
			stringBuilder.Append(this.DisplayName);
			stringBuilder.Append(", SmtpAddress=");
			stringBuilder.Append(this.SmtpAddress);
			stringBuilder.Append(", Type=");
			stringBuilder.Append(this.Type);
			stringBuilder.Append(", IsMember=");
			stringBuilder.Append(this.IsMember);
			stringBuilder.Append(", JoinedBy=");
			stringBuilder.Append(this.JoinedBy);
			stringBuilder.Append(", JoinDate=");
			stringBuilder.Append(this.JoinDate);
			stringBuilder.Append(", IsPinned=");
			stringBuilder.Append(this.IsPinned);
			stringBuilder.Append(", PinDate=");
			stringBuilder.Append(this.PinDate);
			stringBuilder.Append(", SharePointUrl=");
			stringBuilder.Append(this.SharePointUrl);
			stringBuilder.Append(", SharePointSiteUrl=");
			stringBuilder.Append(this.SharePointSiteUrl);
			stringBuilder.Append(", SharePointDocumentsUrl=");
			stringBuilder.Append(this.SharePointDocumentsUrl);
			stringBuilder.Append(", RequireSenderAuthenticationEnabled=");
			stringBuilder.Append(this.RequireSenderAuthenticationEnabled);
			stringBuilder.Append(", AutoSubscribeNewGroupMembers=");
			stringBuilder.Append(this.AutoSubscribeNewGroupMembers);
			stringBuilder.Append(", Language=");
			stringBuilder.Append(this.Language);
			stringBuilder.Append(", Owners={");
			stringBuilder.Append(string.Join<ADObjectId>(",", this.Owners));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
