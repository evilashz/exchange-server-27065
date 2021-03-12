using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Agent.Hygiene
{
	// Token: 0x0200006C RID: 108
	public static class AgentStrings
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x00012EE0 File Offset: 0x000110E0
		static AgentStrings()
		{
			AgentStrings.stringIDs.Add(1295055262U, "WinlenShrinkFactorRange");
			AgentStrings.stringIDs.Add(4279554728U, "InvalidProxyChain");
			AgentStrings.stringIDs.Add(4193024477U, "BlockSenderRemoteOP");
			AgentStrings.stringIDs.Add(1988599787U, "WinlenExpandFactorRange");
			AgentStrings.stringIDs.Add(708167962U, "InitWinLenRange");
			AgentStrings.stringIDs.Add(893210808U, "MinWinLenRange");
			AgentStrings.stringIDs.Add(3745363330U, "InvalidArgument");
			AgentStrings.stringIDs.Add(1999162800U, "BlockSenderLocalSRL");
			AgentStrings.stringIDs.Add(3696302047U, "WritingDisallowedOnClosedConnection");
			AgentStrings.stringIDs.Add(553826582U, "BlockSenderLocalOP");
			AgentStrings.stringIDs.Add(3589472089U, "InvalidProxyType");
			AgentStrings.stringIDs.Add(672016929U, "InvalidOpenProxyType");
			AgentStrings.stringIDs.Add(4259117195U, "BlockSenderRemoteSRL");
			AgentStrings.stringIDs.Add(2034608252U, "FailedToFindInsertionPoint");
			AgentStrings.stringIDs.Add(1497417765U, "GoodBehaviorPeriodRange");
			AgentStrings.stringIDs.Add(589634914U, "MaxWinLenRange");
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0001305C File Offset: 0x0001125C
		public static LocalizedString WinlenShrinkFactorRange
		{
			get
			{
				return new LocalizedString("WinlenShrinkFactorRange", "", false, false, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0001307A File Offset: 0x0001127A
		public static LocalizedString InvalidProxyChain
		{
			get
			{
				return new LocalizedString("InvalidProxyChain", "Ex3D991D", false, true, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00013098 File Offset: 0x00011298
		public static LocalizedString BlockSenderRemoteOP
		{
			get
			{
				return new LocalizedString("BlockSenderRemoteOP", "", false, false, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000130B6 File Offset: 0x000112B6
		public static LocalizedString WinlenExpandFactorRange
		{
			get
			{
				return new LocalizedString("WinlenExpandFactorRange", "", false, false, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002DE RID: 734 RVA: 0x000130D4 File Offset: 0x000112D4
		public static LocalizedString InitWinLenRange
		{
			get
			{
				return new LocalizedString("InitWinLenRange", "", false, false, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000130F4 File Offset: 0x000112F4
		public static LocalizedString InvalidScl(int s)
		{
			return new LocalizedString("InvalidScl", "ExA11912", false, true, AgentStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00013128 File Offset: 0x00011328
		public static LocalizedString InvalidLogLength(int length)
		{
			return new LocalizedString("InvalidLogLength", "Ex7D9896", false, true, AgentStrings.ResourceManager, new object[]
			{
				length
			});
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0001315C File Offset: 0x0001135C
		public static LocalizedString MinWinLenRange
		{
			get
			{
				return new LocalizedString("MinWinLenRange", "", false, false, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0001317A File Offset: 0x0001137A
		public static LocalizedString InvalidArgument
		{
			get
			{
				return new LocalizedString("InvalidArgument", "Ex01E420", false, true, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00013198 File Offset: 0x00011398
		public static LocalizedString BlockSenderLocalSRL
		{
			get
			{
				return new LocalizedString("BlockSenderLocalSRL", "", false, false, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000131B6 File Offset: 0x000113B6
		public static LocalizedString WritingDisallowedOnClosedConnection
		{
			get
			{
				return new LocalizedString("WritingDisallowedOnClosedConnection", "Ex91C974", false, true, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x000131D4 File Offset: 0x000113D4
		public static LocalizedString BlockSenderLocalOP
		{
			get
			{
				return new LocalizedString("BlockSenderLocalOP", "", false, false, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000131F4 File Offset: 0x000113F4
		public static LocalizedString InvalidInsertValue(int val)
		{
			return new LocalizedString("InvalidInsertValue", "ExC1E84F", false, true, AgentStrings.ResourceManager, new object[]
			{
				val
			});
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00013228 File Offset: 0x00011428
		public static LocalizedString InvalidProxyType
		{
			get
			{
				return new LocalizedString("InvalidProxyType", "Ex328029", false, true, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00013246 File Offset: 0x00011446
		public static LocalizedString InvalidOpenProxyType
		{
			get
			{
				return new LocalizedString("InvalidOpenProxyType", "ExBC6118", false, true, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00013264 File Offset: 0x00011464
		public static LocalizedString BlockSenderRemoteSRL
		{
			get
			{
				return new LocalizedString("BlockSenderRemoteSRL", "", false, false, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00013284 File Offset: 0x00011484
		public static LocalizedString InvalidSclLevel(int scl)
		{
			return new LocalizedString("InvalidSclLevel", "ExFAEE6D", false, true, AgentStrings.ResourceManager, new object[]
			{
				scl
			});
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002EB RID: 747 RVA: 0x000132B8 File Offset: 0x000114B8
		public static LocalizedString FailedToFindInsertionPoint
		{
			get
			{
				return new LocalizedString("FailedToFindInsertionPoint", "Ex1B0A54", false, true, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000132D6 File Offset: 0x000114D6
		public static LocalizedString GoodBehaviorPeriodRange
		{
			get
			{
				return new LocalizedString("GoodBehaviorPeriodRange", "", false, false, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000132F4 File Offset: 0x000114F4
		public static LocalizedString MaxWinLenRange
		{
			get
			{
				return new LocalizedString("MaxWinLenRange", "", false, false, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00013314 File Offset: 0x00011514
		public static LocalizedString InvalidFeatureThreshold(int srl)
		{
			return new LocalizedString("InvalidFeatureThreshold", "ExA7B6B6", false, true, AgentStrings.ResourceManager, new object[]
			{
				srl
			});
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00013348 File Offset: 0x00011548
		public static LocalizedString GetLocalizedString(AgentStrings.IDs key)
		{
			return new LocalizedString(AgentStrings.stringIDs[(uint)key], AgentStrings.ResourceManager, new object[0]);
		}

		// Token: 0x0400025D RID: 605
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(16);

		// Token: 0x0400025E RID: 606
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Transport.Agent.Hygiene.AgentStrings", typeof(AgentStrings).GetTypeInfo().Assembly);

		// Token: 0x0200006D RID: 109
		public enum IDs : uint
		{
			// Token: 0x04000260 RID: 608
			WinlenShrinkFactorRange = 1295055262U,
			// Token: 0x04000261 RID: 609
			InvalidProxyChain = 4279554728U,
			// Token: 0x04000262 RID: 610
			BlockSenderRemoteOP = 4193024477U,
			// Token: 0x04000263 RID: 611
			WinlenExpandFactorRange = 1988599787U,
			// Token: 0x04000264 RID: 612
			InitWinLenRange = 708167962U,
			// Token: 0x04000265 RID: 613
			MinWinLenRange = 893210808U,
			// Token: 0x04000266 RID: 614
			InvalidArgument = 3745363330U,
			// Token: 0x04000267 RID: 615
			BlockSenderLocalSRL = 1999162800U,
			// Token: 0x04000268 RID: 616
			WritingDisallowedOnClosedConnection = 3696302047U,
			// Token: 0x04000269 RID: 617
			BlockSenderLocalOP = 553826582U,
			// Token: 0x0400026A RID: 618
			InvalidProxyType = 3589472089U,
			// Token: 0x0400026B RID: 619
			InvalidOpenProxyType = 672016929U,
			// Token: 0x0400026C RID: 620
			BlockSenderRemoteSRL = 4259117195U,
			// Token: 0x0400026D RID: 621
			FailedToFindInsertionPoint = 2034608252U,
			// Token: 0x0400026E RID: 622
			GoodBehaviorPeriodRange = 1497417765U,
			// Token: 0x0400026F RID: 623
			MaxWinLenRange = 589634914U
		}

		// Token: 0x0200006E RID: 110
		private enum ParamIDs
		{
			// Token: 0x04000271 RID: 625
			InvalidScl,
			// Token: 0x04000272 RID: 626
			InvalidLogLength,
			// Token: 0x04000273 RID: 627
			InvalidInsertValue,
			// Token: 0x04000274 RID: 628
			InvalidSclLevel,
			// Token: 0x04000275 RID: 629
			InvalidFeatureThreshold
		}
	}
}
