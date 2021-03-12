using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiApplicationId
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004981 File Offset: 0x00002B81
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004989 File Offset: 0x00002B89
		internal MapiClientType ClientType { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004992 File Offset: 0x00002B92
		// (set) Token: 0x060000DD RID: 221 RVA: 0x0000499A File Offset: 0x00002B9A
		internal string ClientInfo { get; private set; }

		// Token: 0x060000DE RID: 222 RVA: 0x000049A3 File Offset: 0x00002BA3
		internal MapiApplicationId(MapiClientType clientType, string clientInfo)
		{
			if (string.IsNullOrEmpty(clientInfo))
			{
				throw new ArgumentException("ClientInfo cannot be null/empty", "clientInfo");
			}
			this.ClientType = clientType;
			this.ClientInfo = clientInfo;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000049D1 File Offset: 0x00002BD1
		internal static bool TryCreateFromApplicationName(string applicationName, out MapiApplicationId applicationId)
		{
			applicationId = null;
			return !string.IsNullOrEmpty(applicationName) && MapiApplicationId.TryCreateFromClientInfoString("Client=" + applicationName, out applicationId);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000049F4 File Offset: 0x00002BF4
		internal static bool TryCreateFromClientInfoString(string clientInfo, out MapiApplicationId applicationId)
		{
			applicationId = null;
			try
			{
				applicationId = MapiApplicationId.FromClientInfoString(clientInfo);
				return true;
			}
			catch (ArgumentException)
			{
			}
			return false;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004A28 File Offset: 0x00002C28
		internal static MapiApplicationId FromClientInfoString(string clientInfo)
		{
			if (string.IsNullOrEmpty(clientInfo))
			{
				throw new ArgumentException("clientInfo cannot be null", "clientInfo");
			}
			MapiClientType clientType;
			string text;
			if (MapiApplicationId.TryGetClientType(clientInfo, out clientType, out text))
			{
				return new MapiApplicationId(clientType, clientInfo);
			}
			throw new ArgumentException("Invalid clientInfo:" + clientInfo, "clientInfo");
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004A7C File Offset: 0x00002C7C
		private static bool TryGetClientType(string applicationIdString, out MapiClientType clientType, out string clientTypeString)
		{
			clientType = MapiClientType.User;
			clientTypeString = null;
			if (string.IsNullOrEmpty(applicationIdString))
			{
				return false;
			}
			if (!applicationIdString.StartsWith("Client=", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			int num = applicationIdString.IndexOf(';');
			if (num == -1)
			{
				clientTypeString = applicationIdString.Substring("Client=".Length);
			}
			else
			{
				clientTypeString = applicationIdString.Substring("Client=".Length, num - "Client=".Length);
			}
			return MapiApplicationId.ClientString2IdMap.TryGetValue(clientTypeString, out clientType);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004AF8 File Offset: 0x00002CF8
		internal static bool TryNormalizeClientType(ref string applicationIdString)
		{
			MapiClientType mapiClientType = MapiClientType.User;
			string text = null;
			return MapiApplicationId.TryGetClientType(applicationIdString, out mapiClientType, out text);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004B14 File Offset: 0x00002D14
		public string GetNormalizedClientInfo()
		{
			if (string.IsNullOrEmpty(this.ClientInfo))
			{
				return this.ClientInfo;
			}
			int num = this.ClientInfo.IndexOf(';');
			if (num != -1)
			{
				return this.ClientInfo.Substring(0, num);
			}
			return this.ClientInfo;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004B5C File Offset: 0x00002D5C
		public string GetNormalizedClientInfoWithoutPrefix()
		{
			string normalizedClientInfo = this.GetNormalizedClientInfo();
			if (!string.IsNullOrEmpty(normalizedClientInfo) && normalizedClientInfo.StartsWith("Client=", StringComparison.OrdinalIgnoreCase))
			{
				return normalizedClientInfo.Substring("Client=".Length).ToLower();
			}
			return normalizedClientInfo.ToLower();
		}

		// Token: 0x040000F1 RID: 241
		internal const string ClientInfoPrefix = "Client=";

		// Token: 0x040000F2 RID: 242
		private static readonly Dictionary<string, MapiClientType> ClientString2IdMap = new Dictionary<string, MapiClientType>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"ActiveSync",
				MapiClientType.AirSync
			},
			{
				"AS",
				MapiClientType.AvailabilityService
			},
			{
				"CI",
				MapiClientType.ContentIndexing
			},
			{
				"ELC",
				MapiClientType.ELC
			},
			{
				"Management",
				MapiClientType.Management
			},
			{
				"Monitoring",
				MapiClientType.Monitoring
			},
			{
				"StoreActiveMonitoring",
				MapiClientType.StoreActiveMonitoring
			},
			{
				"ResourceHealth",
				MapiClientType.Monitoring
			},
			{
				"EventBased MSExchangeMailboxAssistants",
				MapiClientType.EventBasedAssistants
			},
			{
				"EBA",
				MapiClientType.EventBasedAssistants
			},
			{
				"OWA",
				MapiClientType.OWA
			},
			{
				"POP3/IMAP4",
				MapiClientType.PopImap
			},
			{
				"UM",
				MapiClientType.UnifiedMessaging
			},
			{
				"WebServices",
				MapiClientType.WebServices
			},
			{
				"ApprovalAPI",
				MapiClientType.ApprovalAPI
			},
			{
				"TimeBased MSExchangeMailboxAssistants",
				MapiClientType.TimeBasedAssistants
			},
			{
				"TBA",
				MapiClientType.TimeBasedAssistants
			},
			{
				"MSExchangeRPC",
				MapiClientType.MoMT
			},
			{
				"MSExchangeMigration",
				MapiClientType.Migration
			},
			{
				"MSExchangeSimpleMigration",
				MapiClientType.SimpleMigration
			},
			{
				"TransportSync",
				MapiClientType.TransportSync
			},
			{
				"Hub Transport",
				MapiClientType.Transport
			},
			{
				"HUB",
				MapiClientType.Transport
			},
			{
				"HA",
				MapiClientType.HA
			},
			{
				"Maintenance",
				MapiClientType.Maintenance
			},
			{
				"Inference",
				MapiClientType.Inference
			},
			{
				"SMS",
				MapiClientType.SMS
			},
			{
				"TeamMailbox",
				MapiClientType.TeamMailbox
			},
			{
				"LoadGen",
				MapiClientType.LoadGen
			},
			{
				"PublicFolderSystem",
				MapiClientType.PublicFolderSystem
			},
			{
				"EDiscoverySearch",
				MapiClientType.EDiscoverySearch
			},
			{
				"CIMoveDestination",
				MapiClientType.ContentIndexingMoveDestination
			},
			{
				"AnchorService",
				MapiClientType.AnchorService
			},
			{
				"MSExchangeMailboxLoadBalance",
				MapiClientType.MailboxLoadBalance
			},
			{
				"OutlookService",
				MapiClientType.OutlookService
			},
			{
				"UnifiedPolicy",
				MapiClientType.UnifiedPolicy
			},
			{
				"NotificationBroker",
				MapiClientType.NotificationBroker
			},
			{
				"Pop",
				MapiClientType.Pop
			},
			{
				"LiveIdBasicAuth",
				MapiClientType.LiveIdBasicAuth
			},
			{
				"ADDriver",
				MapiClientType.ADDriver
			},
			{
				"SnackyService",
				MapiClientType.SnackyService
			}
		};
	}
}
