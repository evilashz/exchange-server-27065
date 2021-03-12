using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.CallRouter.Exceptions
{
	// Token: 0x0200022E RID: 558
	internal static class Strings
	{
		// Token: 0x060011A4 RID: 4516 RVA: 0x0003AB1C File Offset: 0x00038D1C
		static Strings()
		{
			Strings.stringIDs.Add(3452399285U, "Server");
			Strings.stringIDs.Add(1422214618U, "ServiceName");
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x0003AB7F File Offset: 0x00038D7F
		public static LocalizedString Server
		{
			get
			{
				return new LocalizedString("Server", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x0003AB96 File Offset: 0x00038D96
		public static LocalizedString ServiceName
		{
			get
			{
				return new LocalizedString("ServiceName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0003ABAD File Offset: 0x00038DAD
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040008D4 RID: 2260
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(2);

		// Token: 0x040008D5 RID: 2261
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.UM.CallRouter.Exceptions.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200022F RID: 559
		public enum IDs : uint
		{
			// Token: 0x040008D7 RID: 2263
			Server = 3452399285U,
			// Token: 0x040008D8 RID: 2264
			ServiceName = 1422214618U
		}
	}
}
