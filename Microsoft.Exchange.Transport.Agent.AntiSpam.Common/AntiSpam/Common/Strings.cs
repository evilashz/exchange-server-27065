using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Agent.AntiSpam.Common
{
	// Token: 0x0200001D RID: 29
	internal static class Strings
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00005138 File Offset: 0x00003338
		static Strings()
		{
			Strings.stringIDs.Add(84245057U, "OptInNotConfigured");
			Strings.stringIDs.Add(19418672U, "OptInConfigured");
			Strings.stringIDs.Add(1349797536U, "UpdateModeDisabled");
			Strings.stringIDs.Add(1359024969U, "OptInRequestDisabled");
			Strings.stringIDs.Add(620496255U, "UpdateModeEnabled");
			Strings.stringIDs.Add(264826011U, "OptInRequestNotifyInstall");
			Strings.stringIDs.Add(1549663132U, "OptInRequestScheduled");
			Strings.stringIDs.Add(1731110810U, "UpdateModeManual");
			Strings.stringIDs.Add(4029010872U, "OptInRequestNotifyDownload");
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005228 File Offset: 0x00003428
		public static LocalizedString OptInNotConfigured
		{
			get
			{
				return new LocalizedString("OptInNotConfigured", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005248 File Offset: 0x00003448
		public static LocalizedString FailedToRegisterForConfigChangeNotification(string agentName)
		{
			return new LocalizedString("FailedToRegisterForConfigChangeNotification", "ExF907B1", false, true, Strings.ResourceManager, new object[]
			{
				agentName
			});
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00005277 File Offset: 0x00003477
		public static LocalizedString OptInConfigured
		{
			get
			{
				return new LocalizedString("OptInConfigured", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005298 File Offset: 0x00003498
		public static LocalizedString FailedToReadConfiguration(string agentName)
		{
			return new LocalizedString("FailedToReadConfiguration", "Ex05145E", false, true, Strings.ResourceManager, new object[]
			{
				agentName
			});
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000052C7 File Offset: 0x000034C7
		public static LocalizedString UpdateModeDisabled
		{
			get
			{
				return new LocalizedString("UpdateModeDisabled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000052E5 File Offset: 0x000034E5
		public static LocalizedString OptInRequestDisabled
		{
			get
			{
				return new LocalizedString("OptInRequestDisabled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00005303 File Offset: 0x00003503
		public static LocalizedString UpdateModeEnabled
		{
			get
			{
				return new LocalizedString("UpdateModeEnabled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00005321 File Offset: 0x00003521
		public static LocalizedString OptInRequestNotifyInstall
		{
			get
			{
				return new LocalizedString("OptInRequestNotifyInstall", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000533F File Offset: 0x0000353F
		public static LocalizedString OptInRequestScheduled
		{
			get
			{
				return new LocalizedString("OptInRequestScheduled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000535D File Offset: 0x0000355D
		public static LocalizedString UpdateModeManual
		{
			get
			{
				return new LocalizedString("UpdateModeManual", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000537B File Offset: 0x0000357B
		public static LocalizedString OptInRequestNotifyDownload
		{
			get
			{
				return new LocalizedString("OptInRequestNotifyDownload", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005399 File Offset: 0x00003599
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040000B1 RID: 177
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(9);

		// Token: 0x040000B2 RID: 178
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Transport.Agent.AntiSpam.Common.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200001E RID: 30
		public enum IDs : uint
		{
			// Token: 0x040000B4 RID: 180
			OptInNotConfigured = 84245057U,
			// Token: 0x040000B5 RID: 181
			OptInConfigured = 19418672U,
			// Token: 0x040000B6 RID: 182
			UpdateModeDisabled = 1349797536U,
			// Token: 0x040000B7 RID: 183
			OptInRequestDisabled = 1359024969U,
			// Token: 0x040000B8 RID: 184
			UpdateModeEnabled = 620496255U,
			// Token: 0x040000B9 RID: 185
			OptInRequestNotifyInstall = 264826011U,
			// Token: 0x040000BA RID: 186
			OptInRequestScheduled = 1549663132U,
			// Token: 0x040000BB RID: 187
			UpdateModeManual = 1731110810U,
			// Token: 0x040000BC RID: 188
			OptInRequestNotifyDownload = 4029010872U
		}

		// Token: 0x0200001F RID: 31
		private enum ParamIDs
		{
			// Token: 0x040000BE RID: 190
			FailedToRegisterForConfigChangeNotification,
			// Token: 0x040000BF RID: 191
			FailedToReadConfiguration
		}
	}
}
