using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel.EnumTypes
{
	// Token: 0x02000008 RID: 8
	public static class OwaOptionEnumStrings
	{
		// Token: 0x06000385 RID: 901 RVA: 0x0000CE04 File Offset: 0x0000B004
		static OwaOptionEnumStrings()
		{
			OwaOptionEnumStrings.stringIDs.Add(996355914U, "Quarantined");
			OwaOptionEnumStrings.stringIDs.Add(3601314308U, "UserAgentsChanges");
			OwaOptionEnumStrings.stringIDs.Add(1975373491U, "SyncCommands");
			OwaOptionEnumStrings.stringIDs.Add(3727590521U, "RoleEditor");
			OwaOptionEnumStrings.stringIDs.Add(3728408622U, "DeviceWipePending");
			OwaOptionEnumStrings.stringIDs.Add(3532057202U, "RolePublishingEditor");
			OwaOptionEnumStrings.stringIDs.Add(141120823U, "RecentCommands");
			OwaOptionEnumStrings.stringIDs.Add(3608358242U, "Upgrade");
			OwaOptionEnumStrings.stringIDs.Add(1068346025U, "OutOfBudgets");
			OwaOptionEnumStrings.stringIDs.Add(3159442548U, "DeviceBlocked");
			OwaOptionEnumStrings.stringIDs.Add(2309238384U, "RolePublishingAuthor");
			OwaOptionEnumStrings.stringIDs.Add(3266435989U, "Individual");
			OwaOptionEnumStrings.stringIDs.Add(1636409600U, "RoleNonEditingAuthor");
			OwaOptionEnumStrings.stringIDs.Add(816661212U, "Policy");
			OwaOptionEnumStrings.stringIDs.Add(1140300925U, "RoleReviewer");
			OwaOptionEnumStrings.stringIDs.Add(3485911895U, "StatusUnsuccessFul");
			OwaOptionEnumStrings.stringIDs.Add(403740404U, "NotApplied");
			OwaOptionEnumStrings.stringIDs.Add(3388973407U, "Organization");
			OwaOptionEnumStrings.stringIDs.Add(2979126483U, "CommandFrequency");
			OwaOptionEnumStrings.stringIDs.Add(2589734627U, "RoleContributor");
			OwaOptionEnumStrings.stringIDs.Add(3937861705U, "RoleAvailabilityOnly");
			OwaOptionEnumStrings.stringIDs.Add(3531789014U, "DeviceRule");
			OwaOptionEnumStrings.stringIDs.Add(1981651471U, "StatusPending");
			OwaOptionEnumStrings.stringIDs.Add(3892568161U, "RoleAuthor");
			OwaOptionEnumStrings.stringIDs.Add(1414246128U, "None");
			OwaOptionEnumStrings.stringIDs.Add(14056478U, "StatusTransferred");
			OwaOptionEnumStrings.stringIDs.Add(102260678U, "EnableNotificationEmail");
			OwaOptionEnumStrings.stringIDs.Add(1010456570U, "DeviceDiscovery");
			OwaOptionEnumStrings.stringIDs.Add(3010978409U, "AppliedInFull");
			OwaOptionEnumStrings.stringIDs.Add(3380639415U, "Watsons");
			OwaOptionEnumStrings.stringIDs.Add(3816006969U, "RoleOwner");
			OwaOptionEnumStrings.stringIDs.Add(3617746388U, "DeviceWipeSucceeded");
			OwaOptionEnumStrings.stringIDs.Add(1656602441U, "ExternallyManaged");
			OwaOptionEnumStrings.stringIDs.Add(3060667906U, "RoleLimitedDetails");
			OwaOptionEnumStrings.stringIDs.Add(3231667300U, "StatusRead");
			OwaOptionEnumStrings.stringIDs.Add(2846264340U, "Unknown");
			OwaOptionEnumStrings.stringIDs.Add(1448003977U, "User");
			OwaOptionEnumStrings.stringIDs.Add(1875295180U, "StatusDelivered");
			OwaOptionEnumStrings.stringIDs.Add(3811183882U, "Allowed");
			OwaOptionEnumStrings.stringIDs.Add(278718718U, "DeviceOk");
			OwaOptionEnumStrings.stringIDs.Add(3184119847U, "PartiallyApplied");
			OwaOptionEnumStrings.stringIDs.Add(4019774802U, "Blocked");
			OwaOptionEnumStrings.stringIDs.Add(3905558735U, "Global");
			OwaOptionEnumStrings.stringIDs.Add(3566263623U, "Default");
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
		public static LocalizedString Quarantined
		{
			get
			{
				return new LocalizedString("Quarantined", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000D1C7 File Offset: 0x0000B3C7
		public static LocalizedString UserAgentsChanges
		{
			get
			{
				return new LocalizedString("UserAgentsChanges", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000D1DE File Offset: 0x0000B3DE
		public static LocalizedString SyncCommands
		{
			get
			{
				return new LocalizedString("SyncCommands", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000D1F5 File Offset: 0x0000B3F5
		public static LocalizedString RoleEditor
		{
			get
			{
				return new LocalizedString("RoleEditor", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000D20C File Offset: 0x0000B40C
		public static LocalizedString DeviceWipePending
		{
			get
			{
				return new LocalizedString("DeviceWipePending", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000D223 File Offset: 0x0000B423
		public static LocalizedString RolePublishingEditor
		{
			get
			{
				return new LocalizedString("RolePublishingEditor", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000D23A File Offset: 0x0000B43A
		public static LocalizedString RecentCommands
		{
			get
			{
				return new LocalizedString("RecentCommands", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000D251 File Offset: 0x0000B451
		public static LocalizedString Upgrade
		{
			get
			{
				return new LocalizedString("Upgrade", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000D268 File Offset: 0x0000B468
		public static LocalizedString OutOfBudgets
		{
			get
			{
				return new LocalizedString("OutOfBudgets", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000D27F File Offset: 0x0000B47F
		public static LocalizedString DeviceBlocked
		{
			get
			{
				return new LocalizedString("DeviceBlocked", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000D296 File Offset: 0x0000B496
		public static LocalizedString RolePublishingAuthor
		{
			get
			{
				return new LocalizedString("RolePublishingAuthor", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000D2AD File Offset: 0x0000B4AD
		public static LocalizedString Individual
		{
			get
			{
				return new LocalizedString("Individual", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000D2C4 File Offset: 0x0000B4C4
		public static LocalizedString RoleNonEditingAuthor
		{
			get
			{
				return new LocalizedString("RoleNonEditingAuthor", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000D2DB File Offset: 0x0000B4DB
		public static LocalizedString Policy
		{
			get
			{
				return new LocalizedString("Policy", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000D2F2 File Offset: 0x0000B4F2
		public static LocalizedString RoleReviewer
		{
			get
			{
				return new LocalizedString("RoleReviewer", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000D309 File Offset: 0x0000B509
		public static LocalizedString StatusUnsuccessFul
		{
			get
			{
				return new LocalizedString("StatusUnsuccessFul", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000D320 File Offset: 0x0000B520
		public static LocalizedString NotApplied
		{
			get
			{
				return new LocalizedString("NotApplied", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000D337 File Offset: 0x0000B537
		public static LocalizedString Organization
		{
			get
			{
				return new LocalizedString("Organization", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000D34E File Offset: 0x0000B54E
		public static LocalizedString CommandFrequency
		{
			get
			{
				return new LocalizedString("CommandFrequency", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000D365 File Offset: 0x0000B565
		public static LocalizedString RoleContributor
		{
			get
			{
				return new LocalizedString("RoleContributor", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000D37C File Offset: 0x0000B57C
		public static LocalizedString RoleAvailabilityOnly
		{
			get
			{
				return new LocalizedString("RoleAvailabilityOnly", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000D393 File Offset: 0x0000B593
		public static LocalizedString DeviceRule
		{
			get
			{
				return new LocalizedString("DeviceRule", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000D3AA File Offset: 0x0000B5AA
		public static LocalizedString StatusPending
		{
			get
			{
				return new LocalizedString("StatusPending", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000D3C1 File Offset: 0x0000B5C1
		public static LocalizedString RoleAuthor
		{
			get
			{
				return new LocalizedString("RoleAuthor", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		public static LocalizedString None
		{
			get
			{
				return new LocalizedString("None", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000D3EF File Offset: 0x0000B5EF
		public static LocalizedString StatusTransferred
		{
			get
			{
				return new LocalizedString("StatusTransferred", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000D406 File Offset: 0x0000B606
		public static LocalizedString EnableNotificationEmail
		{
			get
			{
				return new LocalizedString("EnableNotificationEmail", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000D41D File Offset: 0x0000B61D
		public static LocalizedString DeviceDiscovery
		{
			get
			{
				return new LocalizedString("DeviceDiscovery", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000D434 File Offset: 0x0000B634
		public static LocalizedString AppliedInFull
		{
			get
			{
				return new LocalizedString("AppliedInFull", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000D44B File Offset: 0x0000B64B
		public static LocalizedString Watsons
		{
			get
			{
				return new LocalizedString("Watsons", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000D462 File Offset: 0x0000B662
		public static LocalizedString RoleOwner
		{
			get
			{
				return new LocalizedString("RoleOwner", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000D479 File Offset: 0x0000B679
		public static LocalizedString DeviceWipeSucceeded
		{
			get
			{
				return new LocalizedString("DeviceWipeSucceeded", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000D490 File Offset: 0x0000B690
		public static LocalizedString ExternallyManaged
		{
			get
			{
				return new LocalizedString("ExternallyManaged", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000D4A7 File Offset: 0x0000B6A7
		public static LocalizedString RoleLimitedDetails
		{
			get
			{
				return new LocalizedString("RoleLimitedDetails", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000D4BE File Offset: 0x0000B6BE
		public static LocalizedString StatusRead
		{
			get
			{
				return new LocalizedString("StatusRead", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000D4D5 File Offset: 0x0000B6D5
		public static LocalizedString Unknown
		{
			get
			{
				return new LocalizedString("Unknown", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		public static LocalizedString User
		{
			get
			{
				return new LocalizedString("User", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000D503 File Offset: 0x0000B703
		public static LocalizedString StatusDelivered
		{
			get
			{
				return new LocalizedString("StatusDelivered", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000D51A File Offset: 0x0000B71A
		public static LocalizedString Allowed
		{
			get
			{
				return new LocalizedString("Allowed", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000D531 File Offset: 0x0000B731
		public static LocalizedString DeviceOk
		{
			get
			{
				return new LocalizedString("DeviceOk", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000D548 File Offset: 0x0000B748
		public static LocalizedString PartiallyApplied
		{
			get
			{
				return new LocalizedString("PartiallyApplied", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000D55F File Offset: 0x0000B75F
		public static LocalizedString Blocked
		{
			get
			{
				return new LocalizedString("Blocked", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000D576 File Offset: 0x0000B776
		public static LocalizedString Global
		{
			get
			{
				return new LocalizedString("Global", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000D58D File Offset: 0x0000B78D
		public static LocalizedString Default
		{
			get
			{
				return new LocalizedString("Default", OwaOptionEnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000D5A4 File Offset: 0x0000B7A4
		public static LocalizedString GetLocalizedString(OwaOptionEnumStrings.IDs key)
		{
			return new LocalizedString(OwaOptionEnumStrings.stringIDs[(uint)key], OwaOptionEnumStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000389 RID: 905
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(44);

		// Token: 0x0400038A RID: 906
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.ControlPanel.OwaOptionEnumStrings", typeof(OwaOptionEnumStrings).GetTypeInfo().Assembly);

		// Token: 0x02000009 RID: 9
		public enum IDs : uint
		{
			// Token: 0x0400038C RID: 908
			Quarantined = 996355914U,
			// Token: 0x0400038D RID: 909
			UserAgentsChanges = 3601314308U,
			// Token: 0x0400038E RID: 910
			SyncCommands = 1975373491U,
			// Token: 0x0400038F RID: 911
			RoleEditor = 3727590521U,
			// Token: 0x04000390 RID: 912
			DeviceWipePending = 3728408622U,
			// Token: 0x04000391 RID: 913
			RolePublishingEditor = 3532057202U,
			// Token: 0x04000392 RID: 914
			RecentCommands = 141120823U,
			// Token: 0x04000393 RID: 915
			Upgrade = 3608358242U,
			// Token: 0x04000394 RID: 916
			OutOfBudgets = 1068346025U,
			// Token: 0x04000395 RID: 917
			DeviceBlocked = 3159442548U,
			// Token: 0x04000396 RID: 918
			RolePublishingAuthor = 2309238384U,
			// Token: 0x04000397 RID: 919
			Individual = 3266435989U,
			// Token: 0x04000398 RID: 920
			RoleNonEditingAuthor = 1636409600U,
			// Token: 0x04000399 RID: 921
			Policy = 816661212U,
			// Token: 0x0400039A RID: 922
			RoleReviewer = 1140300925U,
			// Token: 0x0400039B RID: 923
			StatusUnsuccessFul = 3485911895U,
			// Token: 0x0400039C RID: 924
			NotApplied = 403740404U,
			// Token: 0x0400039D RID: 925
			Organization = 3388973407U,
			// Token: 0x0400039E RID: 926
			CommandFrequency = 2979126483U,
			// Token: 0x0400039F RID: 927
			RoleContributor = 2589734627U,
			// Token: 0x040003A0 RID: 928
			RoleAvailabilityOnly = 3937861705U,
			// Token: 0x040003A1 RID: 929
			DeviceRule = 3531789014U,
			// Token: 0x040003A2 RID: 930
			StatusPending = 1981651471U,
			// Token: 0x040003A3 RID: 931
			RoleAuthor = 3892568161U,
			// Token: 0x040003A4 RID: 932
			None = 1414246128U,
			// Token: 0x040003A5 RID: 933
			StatusTransferred = 14056478U,
			// Token: 0x040003A6 RID: 934
			EnableNotificationEmail = 102260678U,
			// Token: 0x040003A7 RID: 935
			DeviceDiscovery = 1010456570U,
			// Token: 0x040003A8 RID: 936
			AppliedInFull = 3010978409U,
			// Token: 0x040003A9 RID: 937
			Watsons = 3380639415U,
			// Token: 0x040003AA RID: 938
			RoleOwner = 3816006969U,
			// Token: 0x040003AB RID: 939
			DeviceWipeSucceeded = 3617746388U,
			// Token: 0x040003AC RID: 940
			ExternallyManaged = 1656602441U,
			// Token: 0x040003AD RID: 941
			RoleLimitedDetails = 3060667906U,
			// Token: 0x040003AE RID: 942
			StatusRead = 3231667300U,
			// Token: 0x040003AF RID: 943
			Unknown = 2846264340U,
			// Token: 0x040003B0 RID: 944
			User = 1448003977U,
			// Token: 0x040003B1 RID: 945
			StatusDelivered = 1875295180U,
			// Token: 0x040003B2 RID: 946
			Allowed = 3811183882U,
			// Token: 0x040003B3 RID: 947
			DeviceOk = 278718718U,
			// Token: 0x040003B4 RID: 948
			PartiallyApplied = 3184119847U,
			// Token: 0x040003B5 RID: 949
			Blocked = 4019774802U,
			// Token: 0x040003B6 RID: 950
			Global = 3905558735U,
			// Token: 0x040003B7 RID: 951
			Default = 3566263623U
		}
	}
}
