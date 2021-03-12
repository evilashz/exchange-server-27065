using System;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003D5 RID: 981
	internal abstract class E4eCssStyle
	{
		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06002CCE RID: 11470
		internal abstract string ArrowImgBase64 { get; }

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06002CCF RID: 11471
		internal abstract string LockImgBase64 { get; }

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06002CD0 RID: 11472
		internal abstract string RegularTextStyle { get; }

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06002CD1 RID: 11473
		internal abstract string DisclaimerTextStyle { get; }

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06002CD2 RID: 11474
		internal abstract string HostedTextStyle { get; }

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06002CD3 RID: 11475
		internal abstract string AnchorTagStyle { get; }

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06002CD4 RID: 11476
		internal abstract string EmailTextAnchorStyle { get; }

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06002CD5 RID: 11477
		internal abstract string LogoSizeStyle { get; }

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06002CD6 RID: 11478
		internal abstract string LockSizeStyle { get; }

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06002CD7 RID: 11479
		internal abstract string BoldTextStyle { get; }

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06002CD8 RID: 11480
		internal abstract string HeaderDivStyle { get; }

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06002CD9 RID: 11481
		internal abstract string HeaderTextStyle { get; }

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06002CDA RID: 11482
		internal abstract string ViewMessageOTPButtonStyle { get; }

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06002CDB RID: 11483
		internal abstract string ViewportMetaTag { get; }

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06002CDC RID: 11484
		internal abstract string MainContentDivStyle { get; }

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06002CDD RID: 11485
		internal abstract string EncryptedMessageDivStyle { get; }

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06002CDE RID: 11486
		internal abstract string ViewMessageButtonDivStyle { get; }

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06002CDF RID: 11487
		internal abstract string HostedMessageTableStyle { get; }

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06002CE0 RID: 11488
		internal abstract string EmailAddressSpanStyle { get; }

		// Token: 0x06002CE1 RID: 11489
		internal abstract string ButtonStyle(string base64Image);
	}
}
