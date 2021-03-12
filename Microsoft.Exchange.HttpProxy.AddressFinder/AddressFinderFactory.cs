using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy.AddressFinder
{
	// Token: 0x02000005 RID: 5
	internal sealed class AddressFinderFactory : IAddressFinderFactory
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021FC File Offset: 0x000003FC
		static AddressFinderFactory()
		{
			RoutingHintAddressFinder routingHintAddressFinder = new RoutingHintAddressFinder();
			LogonUserAddressFinder logonUserAddressFinder = new LogonUserAddressFinder();
			AddressFinderFactory.DefaultAddressFinder = new CompositeAddressFinder(new IAddressFinder[]
			{
				routingHintAddressFinder,
				logonUserAddressFinder
			});
			AddressFinderFactory.EasAddressFinder = AddressFinderFactory.DefaultAddressFinder;
			AddressFinderFactory.MapiAddressFinder = new CompositeAddressFinder(new IAddressFinder[]
			{
				new MapiAddressFinder(),
				AddressFinderFactory.DefaultAddressFinder
			});
			AddressFinderFactory.RpcHttpAddressFinder = new CompositeAddressFinder(new IAddressFinder[]
			{
				new RpcHttpAddressFinder(),
				logonUserAddressFinder
			});
			AddressFinderFactory.EmptyAddressFinder = new CompositeAddressFinder(new IAddressFinder[0]);
			CompositeAddressFinder compositeAddressFinder = new CompositeAddressFinder(new IAddressFinder[]
			{
				new ExplicitLogonAddressFinder(),
				AddressFinderFactory.DefaultAddressFinder
			});
			AddressFinderFactory.EwsAddressFinder = new CompositeAddressFinder(new IAddressFinder[]
			{
				new EwsAddressFinder(),
				compositeAddressFinder
			});
			AddressFinderFactory.EwsODataAddressFinder = new CompositeAddressFinder(new IAddressFinder[]
			{
				new EwsODataAddressFinder(),
				AddressFinderFactory.EwsAddressFinder
			});
			AddressFinderFactory.EwsUserPhotoAddressFinder = new CompositeAddressFinder(new IAddressFinder[]
			{
				new EwsUserPhotoAddressFinder(),
				AddressFinderFactory.EwsAddressFinder
			});
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000232B File Offset: 0x0000052B
		private AddressFinderFactory()
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002334 File Offset: 0x00000534
		public static IAddressFinderFactory GetInstance()
		{
			if (AddressFinderFactory.instance == null)
			{
				lock (AddressFinderFactory.addressFinderFactoryLock)
				{
					if (AddressFinderFactory.instance == null)
					{
						AddressFinderFactory.instance = new AddressFinderFactory();
					}
				}
			}
			return AddressFinderFactory.instance;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002394 File Offset: 0x00000594
		IAddressFinder IAddressFinderFactory.CreateAddressFinder(ProtocolType protocolType, string urlAbsolutePath)
		{
			ArgumentValidator.ThrowIfNull("urlAbsolutePath", urlAbsolutePath);
			switch (protocolType)
			{
			case ProtocolType.Eas:
				return AddressFinderFactory.EasAddressFinder;
			case ProtocolType.Ecp:
				break;
			case ProtocolType.Ews:
				if (ProtocolHelper.IsEwsGetUserPhotoRequest(urlAbsolutePath))
				{
					return AddressFinderFactory.EwsUserPhotoAddressFinder;
				}
				if (ProtocolHelper.IsEwsODataRequest(urlAbsolutePath))
				{
					return AddressFinderFactory.EwsODataAddressFinder;
				}
				if (ProtocolHelper.IsWsSecurityRequest(urlAbsolutePath) || ProtocolHelper.IsPartnerAuthRequest(urlAbsolutePath) || ProtocolHelper.IsX509CertAuthRequest(urlAbsolutePath))
				{
					return AddressFinderFactory.EmptyAddressFinder;
				}
				return AddressFinderFactory.EwsAddressFinder;
			default:
				if (protocolType == ProtocolType.RpcHttp)
				{
					return AddressFinderFactory.RpcHttpAddressFinder;
				}
				if (protocolType == ProtocolType.Mapi)
				{
					return AddressFinderFactory.MapiAddressFinder;
				}
				break;
			}
			return AddressFinderFactory.DefaultAddressFinder;
		}

		// Token: 0x04000005 RID: 5
		private static readonly IAddressFinder DefaultAddressFinder;

		// Token: 0x04000006 RID: 6
		private static readonly IAddressFinder EasAddressFinder;

		// Token: 0x04000007 RID: 7
		private static readonly IAddressFinder EmptyAddressFinder;

		// Token: 0x04000008 RID: 8
		private static readonly IAddressFinder EwsAddressFinder;

		// Token: 0x04000009 RID: 9
		private static readonly IAddressFinder EwsODataAddressFinder;

		// Token: 0x0400000A RID: 10
		private static readonly IAddressFinder EwsUserPhotoAddressFinder;

		// Token: 0x0400000B RID: 11
		private static readonly IAddressFinder MapiAddressFinder;

		// Token: 0x0400000C RID: 12
		private static readonly IAddressFinder RpcHttpAddressFinder;

		// Token: 0x0400000D RID: 13
		private static volatile AddressFinderFactory instance;

		// Token: 0x0400000E RID: 14
		private static object addressFinderFactoryLock = new object();
	}
}
