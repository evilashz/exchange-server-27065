using System;
using System.Configuration;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200003A RID: 58
	internal sealed class AppConfig : IAppConfiguration
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000CB16 File Offset: 0x0000AD16
		internal AppConfig()
		{
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000CB1E File Offset: 0x0000AD1E
		public bool IsFolderPickupEnabled
		{
			get
			{
				return AppConfig.GetConfigBool(AppConfig.folderPickupEnabled, false);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000CB2B File Offset: 0x0000AD2B
		public int PoisonRegistryEntryMaxCount
		{
			get
			{
				return AppConfig.GetConfigInt(AppConfig.poisonRegistryEntryMaxCount, 1, int.MaxValue, 100);
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000CB3F File Offset: 0x0000AD3F
		public void Load()
		{
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000CB44 File Offset: 0x0000AD44
		private static bool GetConfigBool(string label, bool defaultValue)
		{
			bool result;
			try
			{
				result = TransportAppConfig.GetConfigBool(label, defaultValue);
			}
			catch (ConfigurationErrorsException)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000CB74 File Offset: 0x0000AD74
		private static int GetConfigInt(string label, int minimumValue, int maximumValue, int defaultValue)
		{
			int result;
			try
			{
				result = TransportAppConfig.GetConfigInt(label, minimumValue, maximumValue, defaultValue);
			}
			catch (ConfigurationErrorsException)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x0400011A RID: 282
		private static string folderPickupEnabled = "FolderPickupEnabled";

		// Token: 0x0400011B RID: 283
		private static string poisonRegistryEntryMaxCount = "PoisonRegistryEntryMaxCount";
	}
}
