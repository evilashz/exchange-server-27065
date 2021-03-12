using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200002C RID: 44
	internal static class ClientAPIStrings
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00003AD4 File Offset: 0x00001CD4
		static ClientAPIStrings()
		{
			ClientAPIStrings.stringIDs.Add(3277329186U, "BrokerStatus_UnknownError");
			ClientAPIStrings.stringIDs.Add(3160820891U, "CallbackAlreadyRegistered");
			ClientAPIStrings.stringIDs.Add(197742799U, "BrokerStatus_Cancelled");
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003B4B File Offset: 0x00001D4B
		public static LocalizedString BrokerStatus_UnknownError
		{
			get
			{
				return new LocalizedString("BrokerStatus_UnknownError", ClientAPIStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003B62 File Offset: 0x00001D62
		public static LocalizedString CallbackAlreadyRegistered
		{
			get
			{
				return new LocalizedString("CallbackAlreadyRegistered", ClientAPIStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003B7C File Offset: 0x00001D7C
		public static LocalizedString InvalidBrokerSubscriptionOnLoadException(string storeId, string mailbox)
		{
			return new LocalizedString("InvalidBrokerSubscriptionOnLoadException", ClientAPIStrings.ResourceManager, new object[]
			{
				storeId,
				mailbox
			});
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public static LocalizedString BrokerStatus_Cancelled
		{
			get
			{
				return new LocalizedString("BrokerStatus_Cancelled", ClientAPIStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003BBF File Offset: 0x00001DBF
		public static LocalizedString GetLocalizedString(ClientAPIStrings.IDs key)
		{
			return new LocalizedString(ClientAPIStrings.stringIDs[(uint)key], ClientAPIStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000076 RID: 118
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(3);

		// Token: 0x04000077 RID: 119
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Notifications.Broker.Strings", typeof(ClientAPIStrings).GetTypeInfo().Assembly);

		// Token: 0x0200002D RID: 45
		public enum IDs : uint
		{
			// Token: 0x04000079 RID: 121
			BrokerStatus_UnknownError = 3277329186U,
			// Token: 0x0400007A RID: 122
			CallbackAlreadyRegistered = 3160820891U,
			// Token: 0x0400007B RID: 123
			BrokerStatus_Cancelled = 197742799U
		}

		// Token: 0x0200002E RID: 46
		private enum ParamIDs
		{
			// Token: 0x0400007D RID: 125
			InvalidBrokerSubscriptionOnLoadException
		}
	}
}
