using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000027 RID: 39
	internal static class Strings
	{
		// Token: 0x060001ED RID: 493 RVA: 0x0000A414 File Offset: 0x00008614
		static Strings()
		{
			Strings.stringIDs.Add(191973706U, "OrgIdNotFound");
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000A464 File Offset: 0x00008664
		public static LocalizedString ItemProcessingTimeExceeded(string mdbName, int thresholdTimeInMilliSeconds, int actualTimeInMilliSeconds)
		{
			return new LocalizedString("ItemProcessingTimeExceeded", Strings.ResourceManager, new object[]
			{
				mdbName,
				thresholdTimeInMilliSeconds,
				actualTimeInMilliSeconds
			});
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000A49E File Offset: 0x0000869E
		public static LocalizedString OrgIdNotFound
		{
			get
			{
				return new LocalizedString("OrgIdNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000A4B8 File Offset: 0x000086B8
		public static LocalizedString RetrieverProducerRopNotSupported(string mdbGuid, string mbxGuid, string errorDetails)
		{
			return new LocalizedString("RetrieverProducerRopNotSupported", Strings.ResourceManager, new object[]
			{
				mdbGuid,
				mbxGuid,
				errorDetails
			});
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000A4E8 File Offset: 0x000086E8
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000177 RID: 375
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x04000178 RID: 376
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Search.MailboxOperators.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000028 RID: 40
		public enum IDs : uint
		{
			// Token: 0x0400017A RID: 378
			OrgIdNotFound = 191973706U
		}

		// Token: 0x02000029 RID: 41
		private enum ParamIDs
		{
			// Token: 0x0400017C RID: 380
			ItemProcessingTimeExceeded,
			// Token: 0x0400017D RID: 381
			RetrieverProducerRopNotSupported
		}
	}
}
