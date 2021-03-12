using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Local.Components
{
	// Token: 0x020002A4 RID: 676
	internal static class Strings
	{
		// Token: 0x06001685 RID: 5765 RVA: 0x00048A0C File Offset: 0x00046C0C
		static Strings()
		{
			Strings.stringIDs.Add(3960730520U, "QueueFull");
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00048A5B File Offset: 0x00046C5B
		public static LocalizedString QueueFull
		{
			get
			{
				return new LocalizedString("QueueFull", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00048A74 File Offset: 0x00046C74
		public static LocalizedString TestMailNotFound(string clientMsgId)
		{
			return new LocalizedString("TestMailNotFound", Strings.ResourceManager, new object[]
			{
				clientMsgId
			});
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00048A9C File Offset: 0x00046C9C
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000AFE RID: 2814
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x04000AFF RID: 2815
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Forefront.Monitoring.ActiveMonitoring.Local.Components.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x020002A5 RID: 677
		public enum IDs : uint
		{
			// Token: 0x04000B01 RID: 2817
			QueueFull = 3960730520U
		}

		// Token: 0x020002A6 RID: 678
		private enum ParamIDs
		{
			// Token: 0x04000B03 RID: 2819
			TestMailNotFound
		}
	}
}
