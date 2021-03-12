using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxTransport.Delivery
{
	// Token: 0x02000008 RID: 8
	internal static class Strings
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002A70 File Offset: 0x00000C70
		static Strings()
		{
			Strings.stringIDs.Add(2338474726U, "MailboxTransportDeliveryAssistantName");
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public static LocalizedString UsageText(string processName)
		{
			return new LocalizedString("UsageText", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public static LocalizedString MailboxTransportDeliveryAssistantName
		{
			get
			{
				return new LocalizedString("MailboxTransportDeliveryAssistantName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002AFF File Offset: 0x00000CFF
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x0400002D RID: 45
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x0400002E RID: 46
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MailboxTransport.Delivery.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000009 RID: 9
		public enum IDs : uint
		{
			// Token: 0x04000030 RID: 48
			MailboxTransportDeliveryAssistantName = 2338474726U
		}

		// Token: 0x0200000A RID: 10
		private enum ParamIDs
		{
			// Token: 0x04000032 RID: 50
			UsageText
		}
	}
}
