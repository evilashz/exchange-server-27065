using System;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x02000175 RID: 373
	internal abstract class UMErrorCode
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002B60D File Offset: 0x0002980D
		internal static bool IsPermanent(int errorCode)
		{
			return errorCode == -2147466750;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002B617 File Offset: 0x00029817
		internal static bool IsUserInputError(int errorCode)
		{
			return errorCode == -2147466743;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002B621 File Offset: 0x00029821
		internal static bool IsNetworkError(int errorCode)
		{
			return errorCode == 1727 || errorCode == 1722 || errorCode == 1753;
		}

		// Token: 0x04000650 RID: 1616
		internal const int UMSUCCESS = 0;

		// Token: 0x04000651 RID: 1617
		internal const int UMEACCESSDENIED = 5;

		// Token: 0x04000652 RID: 1618
		internal const int UMESERVERUNAVAILABLE = 1722;

		// Token: 0x04000653 RID: 1619
		internal const int UMEENDPOINTNOTREGISTERED = 1753;

		// Token: 0x04000654 RID: 1620
		internal const int UMRPCCALLFAILEDDNE = 1727;

		// Token: 0x04000655 RID: 1621
		internal const int UMEGENERIC = -2147466752;

		// Token: 0x04000656 RID: 1622
		internal const int UMETRANSIENT = -2147466751;

		// Token: 0x04000657 RID: 1623
		internal const int UMEINVALIDREQUEST = -2147466750;

		// Token: 0x04000658 RID: 1624
		internal const int UMEINCOMPATIBLEVERSION = -2147466749;

		// Token: 0x04000659 RID: 1625
		internal const int UMENONUNIQUERECIPIENT = -2147466748;

		// Token: 0x0400065A RID: 1626
		internal const int UMERECIPIENTNOTFOUND = -2147466747;

		// Token: 0x0400065B RID: 1627
		internal const int UMERECOGNIZERNOTINSTALLED = -2147466746;

		// Token: 0x0400065C RID: 1628
		internal const int UMENOSPEECHDETECTED = -2147466743;

		// Token: 0x0400065D RID: 1629
		internal const int UMEREQUESTTIMEDOUT = -2147466742;

		// Token: 0x0400065E RID: 1630
		internal const int UMESPEECHGRAMMARERROR = -2147466741;
	}
}
