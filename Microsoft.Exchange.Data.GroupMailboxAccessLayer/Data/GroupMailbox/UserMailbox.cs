using System;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000050 RID: 80
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UserMailbox : ILocatableMailbox
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0000FB5D File Offset: 0x0000DD5D
		public UserMailbox(UserMailboxLocator locator)
		{
			this.Locator = locator;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000FB6C File Offset: 0x0000DD6C
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000FB74 File Offset: 0x0000DD74
		public IMailboxLocator Locator { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000FB7D File Offset: 0x0000DD7D
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000FB85 File Offset: 0x0000DD85
		public string Alias { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000FB8E File Offset: 0x0000DD8E
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000FB96 File Offset: 0x0000DD96
		public string DisplayName { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000FB9F File Offset: 0x0000DD9F
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000FBA7 File Offset: 0x0000DDA7
		public SmtpAddress SmtpAddress { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000FBB8 File Offset: 0x0000DDB8
		public string Title { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000FBC1 File Offset: 0x0000DDC1
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0000FBC9 File Offset: 0x0000DDC9
		public string ImAddress { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000FBD2 File Offset: 0x0000DDD2
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000FBDA File Offset: 0x0000DDDA
		public bool IsMember { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000FBE3 File Offset: 0x0000DDE3
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000FBEB File Offset: 0x0000DDEB
		public bool IsOwner { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000FBF4 File Offset: 0x0000DDF4
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000FBFC File Offset: 0x0000DDFC
		public bool ShouldEscalate { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000FC05 File Offset: 0x0000DE05
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000FC0D File Offset: 0x0000DE0D
		public bool IsAutoSubscribed { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000FC16 File Offset: 0x0000DE16
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000FC1E File Offset: 0x0000DE1E
		public bool IsPin { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000FC27 File Offset: 0x0000DE27
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000FC2F File Offset: 0x0000DE2F
		public ExDateTime JoinDate { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000FC38 File Offset: 0x0000DE38
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000FC40 File Offset: 0x0000DE40
		public ExDateTime LastVisitedDate { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000FC49 File Offset: 0x0000DE49
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000FC51 File Offset: 0x0000DE51
		public ADObjectId ADObjectId { get; set; }

		// Token: 0x06000296 RID: 662 RVA: 0x0000FC5C File Offset: 0x0000DE5C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("Identification={");
			stringBuilder.Append(this.Locator);
			stringBuilder.Append("}, Alias=");
			stringBuilder.Append(this.Alias);
			stringBuilder.Append(", DisplayName=");
			stringBuilder.Append(this.DisplayName);
			stringBuilder.Append(", SmtpAddress=");
			stringBuilder.Append(this.SmtpAddress);
			stringBuilder.Append(", Title=");
			stringBuilder.Append(this.Title);
			stringBuilder.Append(", ImAddress=");
			stringBuilder.Append(this.ImAddress);
			stringBuilder.Append(", IsMember=");
			stringBuilder.Append(this.IsMember);
			stringBuilder.Append(", IsOwner=");
			stringBuilder.Append(this.IsOwner);
			stringBuilder.Append(", ShouldEscalate=");
			stringBuilder.Append(this.ShouldEscalate);
			stringBuilder.Append(", IsAutoSubscribed=");
			stringBuilder.Append(this.IsAutoSubscribed);
			stringBuilder.Append(", IsPin=");
			stringBuilder.Append(this.IsPin);
			stringBuilder.Append(", JoinDate=");
			stringBuilder.Append(this.JoinDate);
			stringBuilder.Append(", LastVisitedDate=");
			stringBuilder.Append(this.LastVisitedDate);
			stringBuilder.Append(", ADObjectId=");
			stringBuilder.Append(this.ADObjectId);
			return stringBuilder.ToString();
		}
	}
}
