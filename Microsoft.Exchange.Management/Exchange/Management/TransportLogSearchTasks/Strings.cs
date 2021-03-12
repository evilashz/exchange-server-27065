using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.TransportLogSearchTasks
{
	// Token: 0x0200120A RID: 4618
	internal static class Strings
	{
		// Token: 0x0600BA13 RID: 47635 RVA: 0x002A6FF8 File Offset: 0x002A51F8
		static Strings()
		{
			Strings.stringIDs.Add(170709306U, "OldServerSearchLogic");
			Strings.stringIDs.Add(3002944702U, "WarningMoreResultsAvailable");
			Strings.stringIDs.Add(704769464U, "ServerTooBusy");
			Strings.stringIDs.Add(2480663381U, "Complete");
			Strings.stringIDs.Add(1997066575U, "EmptyTimeRange");
			Strings.stringIDs.Add(2255522196U, "LogNotAvailable");
			Strings.stringIDs.Add(906793263U, "OldServerSchema");
			Strings.stringIDs.Add(3194642979U, "InternalError");
			Strings.stringIDs.Add(2886675525U, "SearchTimeout");
			Strings.stringIDs.Add(2445664782U, "MessageTrackingActivityName");
			Strings.stringIDs.Add(97483436U, "QueryTooComplex");
		}

		// Token: 0x0600BA14 RID: 47636 RVA: 0x002A7110 File Offset: 0x002A5310
		public static LocalizedString WarningProxyAddressIsInvalid(string address, string message)
		{
			return new LocalizedString("WarningProxyAddressIsInvalid", "", false, false, Strings.ResourceManager, new object[]
			{
				address,
				message
			});
		}

		// Token: 0x17003A60 RID: 14944
		// (get) Token: 0x0600BA15 RID: 47637 RVA: 0x002A7143 File Offset: 0x002A5343
		public static LocalizedString OldServerSearchLogic
		{
			get
			{
				return new LocalizedString("OldServerSearchLogic", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A61 RID: 14945
		// (get) Token: 0x0600BA16 RID: 47638 RVA: 0x002A7161 File Offset: 0x002A5361
		public static LocalizedString WarningMoreResultsAvailable
		{
			get
			{
				return new LocalizedString("WarningMoreResultsAvailable", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA17 RID: 47639 RVA: 0x002A7180 File Offset: 0x002A5380
		public static LocalizedString RpcUnavailable(string computername)
		{
			return new LocalizedString("RpcUnavailable", "", false, false, Strings.ResourceManager, new object[]
			{
				computername
			});
		}

		// Token: 0x17003A62 RID: 14946
		// (get) Token: 0x0600BA18 RID: 47640 RVA: 0x002A71AF File Offset: 0x002A53AF
		public static LocalizedString ServerTooBusy
		{
			get
			{
				return new LocalizedString("ServerTooBusy", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA19 RID: 47641 RVA: 0x002A71D0 File Offset: 0x002A53D0
		public static LocalizedString RpcNotRegistered(string computername)
		{
			return new LocalizedString("RpcNotRegistered", "", false, false, Strings.ResourceManager, new object[]
			{
				computername
			});
		}

		// Token: 0x0600BA1A RID: 47642 RVA: 0x002A7200 File Offset: 0x002A5400
		public static LocalizedString ServerNameAmbiguous(string server)
		{
			return new LocalizedString("ServerNameAmbiguous", "", false, false, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003A63 RID: 14947
		// (get) Token: 0x0600BA1B RID: 47643 RVA: 0x002A722F File Offset: 0x002A542F
		public static LocalizedString Complete
		{
			get
			{
				return new LocalizedString("Complete", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA1C RID: 47644 RVA: 0x002A7250 File Offset: 0x002A5450
		public static LocalizedString SearchStatus(string server)
		{
			return new LocalizedString("SearchStatus", "", false, false, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003A64 RID: 14948
		// (get) Token: 0x0600BA1D RID: 47645 RVA: 0x002A727F File Offset: 0x002A547F
		public static LocalizedString EmptyTimeRange
		{
			get
			{
				return new LocalizedString("EmptyTimeRange", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA1E RID: 47646 RVA: 0x002A72A0 File Offset: 0x002A54A0
		public static LocalizedString PreE12Server(string server)
		{
			return new LocalizedString("PreE12Server", "", false, false, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003A65 RID: 14949
		// (get) Token: 0x0600BA1F RID: 47647 RVA: 0x002A72CF File Offset: 0x002A54CF
		public static LocalizedString LogNotAvailable
		{
			get
			{
				return new LocalizedString("LogNotAvailable", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA20 RID: 47648 RVA: 0x002A72F0 File Offset: 0x002A54F0
		public static LocalizedString GenericRpcError(string message, string computername)
		{
			return new LocalizedString("GenericRpcError", "", false, false, Strings.ResourceManager, new object[]
			{
				message,
				computername
			});
		}

		// Token: 0x0600BA21 RID: 47649 RVA: 0x002A7324 File Offset: 0x002A5524
		public static LocalizedString ServerNotFound(string server)
		{
			return new LocalizedString("ServerNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003A66 RID: 14950
		// (get) Token: 0x0600BA22 RID: 47650 RVA: 0x002A7353 File Offset: 0x002A5553
		public static LocalizedString OldServerSchema
		{
			get
			{
				return new LocalizedString("OldServerSchema", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA23 RID: 47651 RVA: 0x002A7374 File Offset: 0x002A5574
		public static LocalizedString GenericError(string message)
		{
			return new LocalizedString("GenericError", "", false, false, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x0600BA24 RID: 47652 RVA: 0x002A73A4 File Offset: 0x002A55A4
		public static LocalizedString NotTransportServer(string server)
		{
			return new LocalizedString("NotTransportServer", "", false, false, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003A67 RID: 14951
		// (get) Token: 0x0600BA25 RID: 47653 RVA: 0x002A73D3 File Offset: 0x002A55D3
		public static LocalizedString InternalError
		{
			get
			{
				return new LocalizedString("InternalError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA26 RID: 47654 RVA: 0x002A73F4 File Offset: 0x002A55F4
		public static LocalizedString EventIdNotFound(string eventid)
		{
			return new LocalizedString("EventIdNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				eventid
			});
		}

		// Token: 0x17003A68 RID: 14952
		// (get) Token: 0x0600BA27 RID: 47655 RVA: 0x002A7423 File Offset: 0x002A5623
		public static LocalizedString SearchTimeout
		{
			get
			{
				return new LocalizedString("SearchTimeout", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA28 RID: 47656 RVA: 0x002A7444 File Offset: 0x002A5644
		public static LocalizedString WarningProxyListUnavailable(string address, string message)
		{
			return new LocalizedString("WarningProxyListUnavailable", "", false, false, Strings.ResourceManager, new object[]
			{
				address,
				message
			});
		}

		// Token: 0x17003A69 RID: 14953
		// (get) Token: 0x0600BA29 RID: 47657 RVA: 0x002A7477 File Offset: 0x002A5677
		public static LocalizedString MessageTrackingActivityName
		{
			get
			{
				return new LocalizedString("MessageTrackingActivityName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA2A RID: 47658 RVA: 0x002A7498 File Offset: 0x002A5698
		public static LocalizedString ErrorADTopologyServiceNotAvailable(string server, string errorMessage)
		{
			return new LocalizedString("ErrorADTopologyServiceNotAvailable", "", false, false, Strings.ResourceManager, new object[]
			{
				server,
				errorMessage
			});
		}

		// Token: 0x0600BA2B RID: 47659 RVA: 0x002A74CC File Offset: 0x002A56CC
		public static LocalizedString MissingServerFQDN(string server)
		{
			return new LocalizedString("MissingServerFQDN", "", false, false, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003A6A RID: 14954
		// (get) Token: 0x0600BA2C RID: 47660 RVA: 0x002A74FB File Offset: 0x002A56FB
		public static LocalizedString QueryTooComplex
		{
			get
			{
				return new LocalizedString("QueryTooComplex", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA2D RID: 47661 RVA: 0x002A7519 File Offset: 0x002A5719
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04006494 RID: 25748
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(11);

		// Token: 0x04006495 RID: 25749
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.LogSearchStrings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200120B RID: 4619
		public enum IDs : uint
		{
			// Token: 0x04006497 RID: 25751
			OldServerSearchLogic = 170709306U,
			// Token: 0x04006498 RID: 25752
			WarningMoreResultsAvailable = 3002944702U,
			// Token: 0x04006499 RID: 25753
			ServerTooBusy = 704769464U,
			// Token: 0x0400649A RID: 25754
			Complete = 2480663381U,
			// Token: 0x0400649B RID: 25755
			EmptyTimeRange = 1997066575U,
			// Token: 0x0400649C RID: 25756
			LogNotAvailable = 2255522196U,
			// Token: 0x0400649D RID: 25757
			OldServerSchema = 906793263U,
			// Token: 0x0400649E RID: 25758
			InternalError = 3194642979U,
			// Token: 0x0400649F RID: 25759
			SearchTimeout = 2886675525U,
			// Token: 0x040064A0 RID: 25760
			MessageTrackingActivityName = 2445664782U,
			// Token: 0x040064A1 RID: 25761
			QueryTooComplex = 97483436U
		}

		// Token: 0x0200120C RID: 4620
		private enum ParamIDs
		{
			// Token: 0x040064A3 RID: 25763
			WarningProxyAddressIsInvalid,
			// Token: 0x040064A4 RID: 25764
			RpcUnavailable,
			// Token: 0x040064A5 RID: 25765
			RpcNotRegistered,
			// Token: 0x040064A6 RID: 25766
			ServerNameAmbiguous,
			// Token: 0x040064A7 RID: 25767
			SearchStatus,
			// Token: 0x040064A8 RID: 25768
			PreE12Server,
			// Token: 0x040064A9 RID: 25769
			GenericRpcError,
			// Token: 0x040064AA RID: 25770
			ServerNotFound,
			// Token: 0x040064AB RID: 25771
			GenericError,
			// Token: 0x040064AC RID: 25772
			NotTransportServer,
			// Token: 0x040064AD RID: 25773
			EventIdNotFound,
			// Token: 0x040064AE RID: 25774
			WarningProxyListUnavailable,
			// Token: 0x040064AF RID: 25775
			ErrorADTopologyServiceNotAvailable,
			// Token: 0x040064B0 RID: 25776
			MissingServerFQDN
		}
	}
}
