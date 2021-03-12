using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000141 RID: 321
	internal class DefaultApplicationServices : IApplicationServices
	{
		// Token: 0x06000C86 RID: 3206 RVA: 0x0006E61C File Offset: 0x0006C81C
		public static Stream CreateTemporaryStorage(Func<int, byte[]> acquireBuffer, Action<byte[]> releaseBuffer)
		{
			TemporaryDataStorage temporaryDataStorage = new TemporaryDataStorage(acquireBuffer, releaseBuffer);
			Stream result = temporaryDataStorage.OpenWriteStream(false);
			temporaryDataStorage.Release();
			return result;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0006E640 File Offset: 0x0006C840
		public Stream CreateTemporaryStorage()
		{
			return DefaultApplicationServices.CreateTemporaryStorage(null, null);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0006E64C File Offset: 0x0006C84C
		public IList<CtsConfigurationSetting> GetConfiguration(string subSectionName)
		{
			Dictionary<string, IList<CtsConfigurationSetting>> dictionary = this.GetConfigurationSubSections();
			IList<CtsConfigurationSetting> result;
			if (!dictionary.TryGetValue(subSectionName ?? string.Empty, out result))
			{
				return this.emptySubSection;
			}
			return result;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0006E67C File Offset: 0x0006C87C
		public void LogConfigurationErrorEvent()
		{
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0006E680 File Offset: 0x0006C880
		public void RefreshConfiguration()
		{
			ConfigurationManager.RefreshSection("appSettings");
			ConfigurationManager.RefreshSection("CTS");
			lock (this.lockObject)
			{
				this.configurationSubSections = null;
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0006E6D8 File Offset: 0x0006C8D8
		private Dictionary<string, IList<CtsConfigurationSetting>> GetConfigurationSubSections()
		{
			Dictionary<string, IList<CtsConfigurationSetting>> dictionary = this.configurationSubSections;
			if (dictionary == null)
			{
				CtsConfigurationSection ctsConfigurationSection = null;
				try
				{
					ctsConfigurationSection = (ConfigurationManager.GetSection("CTS") as CtsConfigurationSection);
				}
				catch (ConfigurationErrorsException)
				{
					this.LogConfigurationErrorEvent();
				}
				CtsConfigurationSetting ctsConfigurationSetting = null;
				try
				{
					string value = ConfigurationManager.AppSettings["TemporaryStoragePath"];
					if (!string.IsNullOrEmpty(value))
					{
						ctsConfigurationSetting = new CtsConfigurationSetting("TemporaryStorage");
						ctsConfigurationSetting.AddArgument("Path", value);
					}
				}
				catch (ConfigurationErrorsException)
				{
					this.LogConfigurationErrorEvent();
				}
				lock (this.lockObject)
				{
					dictionary = this.configurationSubSections;
					if (dictionary == null)
					{
						if (ctsConfigurationSection != null)
						{
							dictionary = ctsConfigurationSection.SubSectionsDictionary;
						}
						else
						{
							dictionary = new Dictionary<string, IList<CtsConfigurationSetting>>();
							dictionary.Add(string.Empty, new List<CtsConfigurationSetting>());
						}
						if (ctsConfigurationSetting != null)
						{
							IList<CtsConfigurationSetting> list = dictionary[string.Empty];
							bool flag2 = false;
							foreach (CtsConfigurationSetting ctsConfigurationSetting2 in list)
							{
								if (string.Equals(ctsConfigurationSetting2.Name, ctsConfigurationSetting.Name))
								{
									flag2 = true;
									break;
								}
							}
							if (!flag2)
							{
								list.Add(ctsConfigurationSetting);
							}
						}
						this.configurationSubSections = dictionary;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x04000F0F RID: 3855
		private IList<CtsConfigurationSetting> emptySubSection = new List<CtsConfigurationSetting>();

		// Token: 0x04000F10 RID: 3856
		private object lockObject = new object();

		// Token: 0x04000F11 RID: 3857
		private volatile Dictionary<string, IList<CtsConfigurationSetting>> configurationSubSections;
	}
}
