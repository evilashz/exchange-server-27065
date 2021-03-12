using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D6D RID: 3437
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ServiceTypeInfo
	{
		// Token: 0x060076BD RID: 30397 RVA: 0x0020CFAF File Offset: 0x0020B1AF
		private ServiceTypeInfo(Type type, ServiceType serviceType, Delegate tryCreateServiceDelegate)
		{
			this.type = type;
			this.serviceType = serviceType;
			this.tryCreateServiceDelegate = tryCreateServiceDelegate;
		}

		// Token: 0x060076BE RID: 30398 RVA: 0x0020CFCC File Offset: 0x0020B1CC
		internal static Service CreateHttpService(MiniVirtualDirectory virtualDirectory, TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod)
		{
			foreach (ServiceTypeInfo serviceTypeInfo in ServiceTypeInfo.serviceTypeInfos)
			{
				ServiceTypeInfo.TryCreateHttpServiceDelegate tryCreateHttpServiceDelegate = serviceTypeInfo.tryCreateServiceDelegate as ServiceTypeInfo.TryCreateHttpServiceDelegate;
				Service result;
				if (tryCreateHttpServiceDelegate != null && tryCreateHttpServiceDelegate(virtualDirectory, serverInfo, url, clientAccessType, authenticationMethod, out result))
				{
					return result;
				}
			}
			throw new InvalidOperationException(ServerStrings.ExInvalidServiceType);
		}

		// Token: 0x060076BF RID: 30399 RVA: 0x0020D030 File Offset: 0x0020B230
		internal static Service CreateEmailTransportService(MiniEmailTransport emailTransport, TopologyServerInfo serverInfo, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod)
		{
			foreach (ServiceTypeInfo serviceTypeInfo in ServiceTypeInfo.serviceTypeInfos)
			{
				ServiceTypeInfo.TryCreateEmailTransportServiceDelegate tryCreateEmailTransportServiceDelegate = serviceTypeInfo.tryCreateServiceDelegate as ServiceTypeInfo.TryCreateEmailTransportServiceDelegate;
				Service result;
				if (tryCreateEmailTransportServiceDelegate != null && tryCreateEmailTransportServiceDelegate(emailTransport, serverInfo, clientAccessType, authenticationMethod, out result))
				{
					return result;
				}
			}
			throw new InvalidOperationException(ServerStrings.ExInvalidServiceType);
		}

		// Token: 0x060076C0 RID: 30400 RVA: 0x0020D090 File Offset: 0x0020B290
		internal static Service CreateSmtpService(MiniReceiveConnector smtpReceiveConnector, TopologyServerInfo serverInfo, ClientAccessType clientAccessType)
		{
			foreach (ServiceTypeInfo serviceTypeInfo in ServiceTypeInfo.serviceTypeInfos)
			{
				ServiceTypeInfo.TryCreateSmtpServiceDelegate tryCreateSmtpServiceDelegate = serviceTypeInfo.tryCreateServiceDelegate as ServiceTypeInfo.TryCreateSmtpServiceDelegate;
				if (tryCreateSmtpServiceDelegate != null)
				{
					Service service;
					Service result;
					if (tryCreateSmtpServiceDelegate(smtpReceiveConnector, serverInfo, clientAccessType, out service))
					{
						result = service;
					}
					else
					{
						result = null;
					}
					return result;
				}
			}
			throw new InvalidOperationException(ServerStrings.ExInvalidServiceType);
		}

		// Token: 0x060076C1 RID: 30401 RVA: 0x0020D0F4 File Offset: 0x0020B2F4
		internal static ServiceType GetServiceType(Type type)
		{
			foreach (ServiceTypeInfo serviceTypeInfo in ServiceTypeInfo.serviceTypeInfos)
			{
				if (type.Equals(serviceTypeInfo.type))
				{
					return serviceTypeInfo.serviceType;
				}
			}
			return ServiceType.Invalid;
		}

		// Token: 0x04005260 RID: 21088
		private static readonly ServiceTypeInfo[] serviceTypeInfos = new ServiceTypeInfo[]
		{
			new ServiceTypeInfo(typeof(WebServicesService), ServiceType.WebServices, new ServiceTypeInfo.TryCreateHttpServiceDelegate(WebServicesService.TryCreateWebServicesService)),
			new ServiceTypeInfo(typeof(MobileSyncService), ServiceType.MobileSync, new ServiceTypeInfo.TryCreateHttpServiceDelegate(MobileSyncService.TryCreateMobileSyncService)),
			new ServiceTypeInfo(typeof(OwaService), ServiceType.OutlookWebAccess, new ServiceTypeInfo.TryCreateHttpServiceDelegate(OwaService.TryCreateOwaService)),
			new ServiceTypeInfo(typeof(RpcHttpService), ServiceType.RpcHttp, new ServiceTypeInfo.TryCreateHttpServiceDelegate(RpcHttpService.TryCreateRpcHttpService)),
			new ServiceTypeInfo(typeof(MapiHttpService), ServiceType.MapiHttp, new ServiceTypeInfo.TryCreateHttpServiceDelegate(MapiHttpService.TryCreateMapiHttpService)),
			new ServiceTypeInfo(typeof(OabService), ServiceType.OfflineAddressBook, new ServiceTypeInfo.TryCreateHttpServiceDelegate(OabService.TryCreateOabService)),
			new ServiceTypeInfo(typeof(AvailabilityForeignConnectorService), ServiceType.AvailabilityForeignConnector, new ServiceTypeInfo.TryCreateHttpServiceDelegate(AvailabilityForeignConnectorService.TryCreateAvailabilityForeignConnectorService)),
			new ServiceTypeInfo(typeof(EcpService), ServiceType.ExchangeControlPanel, new ServiceTypeInfo.TryCreateHttpServiceDelegate(EcpService.TryCreateEcpService)),
			new ServiceTypeInfo(typeof(E12UnifiedMessagingService), ServiceType.UnifiedMessaging, new ServiceTypeInfo.TryCreateHttpServiceDelegate(E12UnifiedMessagingService.TryCreateUnifiedMessagingService)),
			new ServiceTypeInfo(typeof(Pop3Service), ServiceType.Pop3, new ServiceTypeInfo.TryCreateEmailTransportServiceDelegate(Pop3Service.TryCreatePop3Service)),
			new ServiceTypeInfo(typeof(Imap4Service), ServiceType.Imap4, new ServiceTypeInfo.TryCreateEmailTransportServiceDelegate(Imap4Service.TryCreateImap4Service)),
			new ServiceTypeInfo(typeof(SmtpService), ServiceType.Smtp, new ServiceTypeInfo.TryCreateSmtpServiceDelegate(SmtpService.TryCreateSmtpService)),
			new ServiceTypeInfo(typeof(HttpService), ServiceType.Invalid, new ServiceTypeInfo.TryCreateHttpServiceDelegate(HttpService.TryCreateHttpService)),
			new ServiceTypeInfo(typeof(EmailTransportService), ServiceType.Invalid, new ServiceTypeInfo.TryCreateEmailTransportServiceDelegate(EmailTransportService.TryCreateEmailTransportService))
		};

		// Token: 0x04005261 RID: 21089
		private readonly Type type;

		// Token: 0x04005262 RID: 21090
		private readonly ServiceType serviceType;

		// Token: 0x04005263 RID: 21091
		private readonly Delegate tryCreateServiceDelegate;

		// Token: 0x02000D6E RID: 3438
		// (Invoke) Token: 0x060076C4 RID: 30404
		internal delegate bool TryCreateHttpServiceDelegate(MiniVirtualDirectory virtualDirectory, TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service);

		// Token: 0x02000D6F RID: 3439
		// (Invoke) Token: 0x060076C8 RID: 30408
		internal delegate bool TryCreateEmailTransportServiceDelegate(MiniEmailTransport emailTransport, TopologyServerInfo serverInfo, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service);

		// Token: 0x02000D70 RID: 3440
		// (Invoke) Token: 0x060076CC RID: 30412
		internal delegate bool TryCreateSmtpServiceDelegate(MiniReceiveConnector smtpReceiveConnector, TopologyServerInfo serverInfo, ClientAccessType clientAccessType, out Service service);
	}
}
