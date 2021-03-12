using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMWorkerProcess.Exceptions
{
	// Token: 0x0200022B RID: 555
	internal static class Strings
	{
		// Token: 0x0600119B RID: 4507 RVA: 0x0003A974 File Offset: 0x00038B74
		static Strings()
		{
			Strings.stringIDs.Add(3800718957U, "WPSemaphoreOpenFailure");
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0003A9C4 File Offset: 0x00038BC4
		public static LocalizedString WPUnableToFindCertificate(string s)
		{
			return new LocalizedString("WPUnableToFindCertificate", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0003A9EC File Offset: 0x00038BEC
		public static LocalizedString WPInvalidControlPort(int p, int min, int max)
		{
			return new LocalizedString("WPInvalidControlPort", Strings.ResourceManager, new object[]
			{
				p,
				min,
				max
			});
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0003AA2C File Offset: 0x00038C2C
		public static LocalizedString WPDirectoryNotFound(string s)
		{
			return new LocalizedString("WPDirectoryNotFound", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0003AA54 File Offset: 0x00038C54
		public static LocalizedString WPInvalidArguments(string arg)
		{
			return new LocalizedString("WPInvalidArguments", Strings.ResourceManager, new object[]
			{
				arg
			});
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0003AA7C File Offset: 0x00038C7C
		public static LocalizedString WPSemaphoreOpenFailure
		{
			get
			{
				return new LocalizedString("WPSemaphoreOpenFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0003AA94 File Offset: 0x00038C94
		public static LocalizedString WPInvalidSipPort(int p, int min, int max)
		{
			return new LocalizedString("WPInvalidSipPort", Strings.ResourceManager, new object[]
			{
				p,
				min,
				max
			});
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0003AAD4 File Offset: 0x00038CD4
		public static LocalizedString WPFileNotFound(string s)
		{
			return new LocalizedString("WPFileNotFound", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0003AAFC File Offset: 0x00038CFC
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040008C9 RID: 2249
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x040008CA RID: 2250
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.UM.UMWorkerProcess.Exceptions.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200022C RID: 556
		public enum IDs : uint
		{
			// Token: 0x040008CC RID: 2252
			WPSemaphoreOpenFailure = 3800718957U
		}

		// Token: 0x0200022D RID: 557
		private enum ParamIDs
		{
			// Token: 0x040008CE RID: 2254
			WPUnableToFindCertificate,
			// Token: 0x040008CF RID: 2255
			WPInvalidControlPort,
			// Token: 0x040008D0 RID: 2256
			WPDirectoryNotFound,
			// Token: 0x040008D1 RID: 2257
			WPInvalidArguments,
			// Token: 0x040008D2 RID: 2258
			WPInvalidSipPort,
			// Token: 0x040008D3 RID: 2259
			WPFileNotFound
		}
	}
}
