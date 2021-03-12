using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000111 RID: 273
	[Flags]
	public enum Feature : ulong
	{
		// Token: 0x0400066C RID: 1644
		GlobalAddressList = 1UL,
		// Token: 0x0400066D RID: 1645
		Calendar = 2UL,
		// Token: 0x0400066E RID: 1646
		Contacts = 4UL,
		// Token: 0x0400066F RID: 1647
		Tasks = 8UL,
		// Token: 0x04000670 RID: 1648
		Journal = 16UL,
		// Token: 0x04000671 RID: 1649
		StickyNotes = 32UL,
		// Token: 0x04000672 RID: 1650
		PublicFolders = 64UL,
		// Token: 0x04000673 RID: 1651
		Organization = 128UL,
		// Token: 0x04000674 RID: 1652
		Notifications = 256UL,
		// Token: 0x04000675 RID: 1653
		RichClient = 512UL,
		// Token: 0x04000676 RID: 1654
		SpellChecker = 1024UL,
		// Token: 0x04000677 RID: 1655
		SMime = 2048UL,
		// Token: 0x04000678 RID: 1656
		SearchFolders = 4096UL,
		// Token: 0x04000679 RID: 1657
		Signature = 8192UL,
		// Token: 0x0400067A RID: 1658
		Rules = 16384UL,
		// Token: 0x0400067B RID: 1659
		Themes = 32768UL,
		// Token: 0x0400067C RID: 1660
		JunkEMail = 65536UL,
		// Token: 0x0400067D RID: 1661
		UMIntegration = 131072UL,
		// Token: 0x0400067E RID: 1662
		WssIntegrationFromPublicComputer = 262144UL,
		// Token: 0x0400067F RID: 1663
		WssIntegrationFromPrivateComputer = 524288UL,
		// Token: 0x04000680 RID: 1664
		UncIntegrationFromPublicComputer = 1048576UL,
		// Token: 0x04000681 RID: 1665
		UncIntegrationFromPrivateComputer = 2097152UL,
		// Token: 0x04000682 RID: 1666
		EasMobileOptions = 4194304UL,
		// Token: 0x04000683 RID: 1667
		ExplicitLogon = 8388608UL,
		// Token: 0x04000684 RID: 1668
		AddressLists = 16777216UL,
		// Token: 0x04000685 RID: 1669
		Dumpster = 33554432UL,
		// Token: 0x04000686 RID: 1670
		ChangePassword = 67108864UL,
		// Token: 0x04000687 RID: 1671
		InstantMessage = 134217728UL,
		// Token: 0x04000688 RID: 1672
		TextMessage = 268435456UL,
		// Token: 0x04000689 RID: 1673
		OWALight = 536870912UL,
		// Token: 0x0400068A RID: 1674
		DelegateAccess = 1073741824UL,
		// Token: 0x0400068B RID: 1675
		Irm = 2147483648UL,
		// Token: 0x0400068C RID: 1676
		ForceSaveAttachmentFiltering = 4294967296UL,
		// Token: 0x0400068D RID: 1677
		Silverlight = 8589934592UL,
		// Token: 0x0400068E RID: 1678
		DisplayPhotos = 2199023255552UL,
		// Token: 0x0400068F RID: 1679
		SetPhoto = 4398046511104UL,
		// Token: 0x04000690 RID: 1680
		All = 18446744073709551615UL
	}
}
