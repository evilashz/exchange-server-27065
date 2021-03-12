using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200001B RID: 27
	internal static class ClientTypeHelper
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00005508 File Offset: 0x00003708
		internal static bool TryGetClientType(string applicationIdString, out ClientType clientType)
		{
			clientType = ClientType.User;
			if (applicationIdString == null)
			{
				return false;
			}
			bool result = false;
			foreach (string key in applicationIdString.Split(new char[]
			{
				';'
			}))
			{
				ClientType clientType2;
				if (!ClientTypeHelper.applicationId2ClientTypeMap.TryGetValue(key, out clientType2))
				{
					break;
				}
				clientType = clientType2;
				result = true;
			}
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005562 File Offset: 0x00003762
		internal static bool IsContentIndexing(ClientType clientType)
		{
			return clientType == ClientType.ContentIndexing || clientType == ClientType.ContentIndexingMoveDestination;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005570 File Offset: 0x00003770
		private static Dictionary<string, ClientType> BuildApplicationId2ClientTypeMap()
		{
			return new Dictionary<string, ClientType>(StringComparer.OrdinalIgnoreCase)
			{
				{
					"Client=ActiveSync",
					ClientType.AirSync
				},
				{
					"Client=AS",
					ClientType.AvailabilityService
				},
				{
					"Client=CI",
					ClientType.ContentIndexing
				},
				{
					"Client=CIMoveDestination",
					ClientType.ContentIndexingMoveDestination
				},
				{
					"Client=ELC",
					ClientType.ELC
				},
				{
					"Client=Management",
					ClientType.Management
				},
				{
					"Client=Monitoring",
					ClientType.Monitoring
				},
				{
					"Client=StoreActiveMonitoring",
					ClientType.StoreActiveMonitoring
				},
				{
					"Client=ResourceHealth",
					ClientType.Monitoring
				},
				{
					"Client=EventBased MSExchangeMailboxAssistants",
					ClientType.EventBasedAssistants
				},
				{
					"Client=EBA",
					ClientType.EventBasedAssistants
				},
				{
					"Client=OWA",
					ClientType.OWA
				},
				{
					"Client=UM",
					ClientType.UnifiedMessaging
				},
				{
					"Client=WebServices",
					ClientType.WebServices
				},
				{
					"Client=ApprovalAPI",
					ClientType.ApprovalAPI
				},
				{
					"Client=POP3/IMAP4",
					ClientType.Imap
				},
				{
					"Protocol=POP3",
					ClientType.Pop
				},
				{
					"Protocol=IMAP4",
					ClientType.Imap
				},
				{
					"Action=FixImapId",
					ClientType.Imap
				},
				{
					"Client=TimeBased MSExchangeMailboxAssistants",
					ClientType.TimeBasedAssistants
				},
				{
					"Client=TBA",
					ClientType.TimeBasedAssistants
				},
				{
					"Client=MSExchangeRPC",
					ClientType.MoMT
				},
				{
					"Client=MSExchangeMigration",
					ClientType.Migration
				},
				{
					"Client=MSExchangeSimpleMigration",
					ClientType.SimpleMigration
				},
				{
					"Client=PublicFolderSystem",
					ClientType.PublicFolderSystem
				},
				{
					"Client=TransportSync",
					ClientType.TransportSync
				},
				{
					"Client=Hub Transport",
					ClientType.Transport
				},
				{
					"Client=HUB",
					ClientType.Transport
				},
				{
					"Client=HA",
					ClientType.HA
				},
				{
					"Client=Maintenance",
					ClientType.Maintenance
				},
				{
					"Client=Inference",
					ClientType.Inference
				},
				{
					"Client=SMS",
					ClientType.SMS
				},
				{
					"Client=TeamMailbox",
					ClientType.TeamMailbox
				},
				{
					"Client=LoadGen",
					ClientType.LoadGen
				},
				{
					"Client=EDiscoverySearch",
					ClientType.EDiscoverySearch
				},
				{
					"Client=AnchorService",
					ClientType.AnchorService
				},
				{
					"Client=MSExchangeMailboxLoadBalance",
					ClientType.MailboxLoadBalance
				},
				{
					"Client=OutlookService",
					ClientType.OutlookService
				},
				{
					"Client=UnifiedPolicy",
					ClientType.UnifiedPolicy
				},
				{
					"Client=NotificationBroker",
					ClientType.NotificationBroker
				},
				{
					"Client=LiveIdBasicAuth",
					ClientType.LiveIdBasicAuth
				},
				{
					"Client=ADDriver",
					ClientType.ADDriver
				},
				{
					"Client=SnackyService",
					ClientType.SnackyService
				}
			};
		}

		// Token: 0x040000E3 RID: 227
		private static readonly Dictionary<string, ClientType> applicationId2ClientTypeMap = ClientTypeHelper.BuildApplicationId2ClientTypeMap();
	}
}
