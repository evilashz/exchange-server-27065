using System;

namespace Microsoft.Exchange.Data.Globalization.Iso2022Jp
{
	// Token: 0x02000122 RID: 290
	internal enum EscapeState
	{
		// Token: 0x04000E6C RID: 3692
		Begin,
		// Token: 0x04000E6D RID: 3693
		Esc_1,
		// Token: 0x04000E6E RID: 3694
		Esc_Dollar_2,
		// Token: 0x04000E6F RID: 3695
		Esc_OpenParen_2,
		// Token: 0x04000E70 RID: 3696
		Esc_Ampersand_2,
		// Token: 0x04000E71 RID: 3697
		Esc_K_2,
		// Token: 0x04000E72 RID: 3698
		Esc_Dollar_OpenParen_3,
		// Token: 0x04000E73 RID: 3699
		Esc_Dollar_CloseParen_3,
		// Token: 0x04000E74 RID: 3700
		Esc_Ampersand_At_3,
		// Token: 0x04000E75 RID: 3701
		Esc_Ampersand_At_Esc_4,
		// Token: 0x04000E76 RID: 3702
		Esc_Ampersand_At_Esc_Dollar_5,
		// Token: 0x04000E77 RID: 3703
		Esc_Esc_Reset,
		// Token: 0x04000E78 RID: 3704
		Esc_SISO_Reset
	}
}
