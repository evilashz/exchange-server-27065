using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001E0 RID: 480
	[Serializable]
	public class SupportedVersionList
	{
		// Token: 0x06001139 RID: 4409 RVA: 0x00034B6C File Offset: 0x00032D6C
		static SupportedVersionList()
		{
			string name = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\AdminTools";
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
				{
					if (registryKey != null)
					{
						bool.TryParse(registryKey.GetValue("EMC.SkipVersionCheck") as string, out SupportedVersionList.skipVersionCheck);
					}
				}
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00034BFC File Offset: 0x00032DFC
		public SupportedVersionList(string versionList)
		{
			string[] array = versionList.Split(new char[]
			{
				';'
			});
			foreach (string text in array)
			{
				string text2 = text.Trim();
				if (!string.IsNullOrEmpty(text2))
				{
					this.supportedVersionList.Add(ExchangeBuild.Parse(text2));
				}
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x00034C6A File Offset: 0x00032E6A
		public int Count
		{
			get
			{
				return this.supportedVersionList.Count;
			}
		}

		// Token: 0x17000327 RID: 807
		public ExchangeBuild this[int pos]
		{
			get
			{
				return this.supportedVersionList[pos];
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00034CD0 File Offset: 0x00032ED0
		public bool IsSupported(string version)
		{
			if (SupportedVersionList.skipVersionCheck)
			{
				return false;
			}
			ExchangeBuild build = ExchangeBuild.Parse(version);
			return (from c in this.supportedVersionList
			where c.Major == build.Major && c.Minor == build.Minor && c.Build == build.Build
			select c).Count<ExchangeBuild>() > 0;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00034D18 File Offset: 0x00032F18
		public string GetLatestVersion()
		{
			if (this.supportedVersionList.Count <= 0)
			{
				return string.Empty;
			}
			return this.supportedVersionList.Max<ExchangeBuild>().ToString();
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00034D54 File Offset: 0x00032F54
		public static SupportedVersionList Parse(string list)
		{
			SupportedVersionList result = null;
			try
			{
				result = new SupportedVersionList((list != null) ? list.ToString() : string.Empty);
			}
			catch (ArgumentException ex)
			{
				throw new SupportedVersionListFormatException(new LocalizedString(ex.Message));
			}
			return result;
		}

		// Token: 0x040003D3 RID: 979
		public static string DefaultVersionString = ConfigurationContext.Setup.GetExecutingVersion().ToString();

		// Token: 0x040003D4 RID: 980
		private List<ExchangeBuild> supportedVersionList = new List<ExchangeBuild>();

		// Token: 0x040003D5 RID: 981
		private static bool skipVersionCheck = false;
	}
}
