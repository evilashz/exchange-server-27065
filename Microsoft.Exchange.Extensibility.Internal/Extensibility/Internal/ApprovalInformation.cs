using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000040 RID: 64
	internal class ApprovalInformation
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x00010268 File Offset: 0x0000E468
		public ApprovalInformation(Charset messageCharset, CultureInfo culture, string subject, string topText, string topTextFont, IEnumerable<string> details, string finalText, string finalTextFont, string bodyTextFont, IEnumerable<int> codepages)
		{
			this.Culture = culture;
			this.Details = details;
			this.FinalText = finalText;
			this.FinalTextFont = finalTextFont;
			this.BodyTextFont = bodyTextFont;
			this.MessageCharset = messageCharset;
			this.Subject = subject;
			this.Codepages = codepages;
			this.TopText = topText;
			this.TopTextFont = topTextFont;
		}

		// Token: 0x0400030F RID: 783
		public readonly string Subject;

		// Token: 0x04000310 RID: 784
		public readonly string TopText;

		// Token: 0x04000311 RID: 785
		public readonly string TopTextFont;

		// Token: 0x04000312 RID: 786
		public readonly string FinalText;

		// Token: 0x04000313 RID: 787
		public readonly IEnumerable<string> Details;

		// Token: 0x04000314 RID: 788
		public readonly string FinalTextFont;

		// Token: 0x04000315 RID: 789
		public readonly string BodyTextFont;

		// Token: 0x04000316 RID: 790
		public readonly Charset MessageCharset;

		// Token: 0x04000317 RID: 791
		public readonly CultureInfo Culture;

		// Token: 0x04000318 RID: 792
		public readonly IEnumerable<int> Codepages;

		// Token: 0x02000041 RID: 65
		internal enum ApprovalNotificationType
		{
			// Token: 0x0400031A RID: 794
			DecisionConflict,
			// Token: 0x0400031B RID: 795
			ApprovalRequest,
			// Token: 0x0400031C RID: 796
			ModeratedTransportReject,
			// Token: 0x0400031D RID: 797
			ModeratedTransportRejectWithComments,
			// Token: 0x0400031E RID: 798
			DecisionUpdate,
			// Token: 0x0400031F RID: 799
			ApprovalRequestExpiry,
			// Token: 0x04000320 RID: 800
			ExpiryNotification,
			// Token: 0x04000321 RID: 801
			ModeratorsNdrNotification,
			// Token: 0x04000322 RID: 802
			ModeratorsOofNotification,
			// Token: 0x04000323 RID: 803
			ModeratorExpiryNotification
		}
	}
}
