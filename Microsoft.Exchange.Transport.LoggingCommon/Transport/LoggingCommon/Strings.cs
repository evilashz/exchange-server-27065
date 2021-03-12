using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x0200001C RID: 28
	internal static class Strings
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00003B14 File Offset: 0x00001D14
		static Strings()
		{
			Strings.stringIDs.Add(581603311U, "MissingLogSchemaInfo");
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003B63 File Offset: 0x00001D63
		public static LocalizedString MissingLogSchemaInfo
		{
			get
			{
				return new LocalizedString("MissingLogSchemaInfo", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003B7A File Offset: 0x00001D7A
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000119 RID: 281
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x0400011A RID: 282
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Transport.LoggingCommon.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200001D RID: 29
		public enum IDs : uint
		{
			// Token: 0x0400011C RID: 284
			MissingLogSchemaInfo = 581603311U
		}
	}
}
