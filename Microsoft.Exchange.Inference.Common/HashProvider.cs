using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.DataMining;
using Microsoft.Win32;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000026 RID: 38
	public static class HashProvider
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002FCC File Offset: 0x000011CC
		internal static Hookable<IHashProvider> HookableDefaultProvider
		{
			get
			{
				return HashProvider.hookableDefaultProvider;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002FD3 File Offset: 0x000011D3
		internal static IHashProvider Default
		{
			get
			{
				return HashProvider.hookableDefaultProvider.Value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002FDF File Offset: 0x000011DF
		internal static IHashProvider MD5
		{
			get
			{
				return HashProvider.MD5HashProvider.Instance;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002FE6 File Offset: 0x000011E6
		internal static IHashProvider Exchange
		{
			get
			{
				return HashProvider.ExchangeDataMiningHashProvider.Instance;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002FF0 File Offset: 0x000011F0
		public static string HashString(string inputString, IHashProvider hashProvider, bool isCaseSensitive)
		{
			string result = null;
			if (inputString != null)
			{
				if (hashProvider != null)
				{
					if (!isCaseSensitive)
					{
						inputString = inputString.ToUpperInvariant();
					}
					return hashProvider.HashString(inputString);
				}
				result = inputString;
			}
			return result;
		}

		// Token: 0x040000B3 RID: 179
		private static Hookable<IHashProvider> hookableDefaultProvider = Hookable<IHashProvider>.Create(true, HashProvider.Exchange);

		// Token: 0x02000028 RID: 40
		internal sealed class MD5HashProvider : IHashProvider
		{
			// Token: 0x06000093 RID: 147 RVA: 0x0000302D File Offset: 0x0000122D
			private MD5HashProvider()
			{
				this.hashAlgorithm = System.Security.Cryptography.MD5.Create();
			}

			// Token: 0x06000094 RID: 148 RVA: 0x00003040 File Offset: 0x00001240
			~MD5HashProvider()
			{
				if (this.hashAlgorithm != null)
				{
					this.hashAlgorithm.Dispose();
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x06000095 RID: 149 RVA: 0x0000307C File Offset: 0x0000127C
			public static HashProvider.MD5HashProvider Instance
			{
				get
				{
					return HashProvider.MD5HashProvider.instance;
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x06000096 RID: 150 RVA: 0x00003083 File Offset: 0x00001283
			public bool IsInitialized
			{
				get
				{
					return this.hashAlgorithm != null;
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x06000097 RID: 151 RVA: 0x00003091 File Offset: 0x00001291
			internal MD5 HashAlgorithm
			{
				get
				{
					return this.hashAlgorithm;
				}
			}

			// Token: 0x06000098 RID: 152 RVA: 0x00003099 File Offset: 0x00001299
			public bool Initialize()
			{
				return this.IsInitialized;
			}

			// Token: 0x06000099 RID: 153 RVA: 0x000030A4 File Offset: 0x000012A4
			public string HashString(string input)
			{
				if (input == null)
				{
					return null;
				}
				byte[] array = this.hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("x2"));
				}
				return stringBuilder.ToString();
			}

			// Token: 0x040000B4 RID: 180
			private static HashProvider.MD5HashProvider instance = new HashProvider.MD5HashProvider();

			// Token: 0x040000B5 RID: 181
			private readonly MD5 hashAlgorithm;
		}

		// Token: 0x02000029 RID: 41
		internal sealed class ExchangeDataMiningHashProvider : IHashProvider
		{
			// Token: 0x0600009B RID: 155 RVA: 0x0000310B File Offset: 0x0000130B
			private ExchangeDataMiningHashProvider()
			{
			}

			// Token: 0x0600009C RID: 156 RVA: 0x00003114 File Offset: 0x00001314
			~ExchangeDataMiningHashProvider()
			{
				if (this.obfuscationProvider != null)
				{
					this.obfuscationProvider.Dispose();
					this.obfuscationProvider = null;
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x0600009D RID: 157 RVA: 0x00003154 File Offset: 0x00001354
			public static HashProvider.ExchangeDataMiningHashProvider Instance
			{
				get
				{
					return HashProvider.ExchangeDataMiningHashProvider.instance;
				}
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x0600009E RID: 158 RVA: 0x0000315B File Offset: 0x0000135B
			public bool IsInitialized
			{
				get
				{
					return this.obfuscationProvider != null;
				}
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x0600009F RID: 159 RVA: 0x00003169 File Offset: 0x00001369
			internal static Hookable<Func<string>> HookableGetExchangeUploaderConfigPathDelegate
			{
				get
				{
					return HashProvider.ExchangeDataMiningHashProvider.hookableGetExchangeUploaderConfigPathDelegate;
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003170 File Offset: 0x00001370
			internal static Hookable<Func<string>> HookableGetOfficeDataLoaderConfigPathDelegate
			{
				get
				{
					return HashProvider.ExchangeDataMiningHashProvider.hookableGetOfficeDataLoaderConfigPathDelegate;
				}
			}

			// Token: 0x060000A1 RID: 161 RVA: 0x00003178 File Offset: 0x00001378
			public bool Initialize()
			{
				if (this.IsInitialized)
				{
					return true;
				}
				bool result;
				lock (HashProvider.ExchangeDataMiningHashProvider.singletonLock)
				{
					if (this.IsInitialized)
					{
						result = true;
					}
					else
					{
						this.obfuscationProvider = HashProvider.ExchangeDataMiningHashProvider.TryCreateObfuscationProvider();
						result = (this.obfuscationProvider != null);
					}
				}
				return result;
			}

			// Token: 0x060000A2 RID: 162 RVA: 0x000031E0 File Offset: 0x000013E0
			public string HashString(string input)
			{
				if (!this.Initialize())
				{
					throw new InvalidOperationException("Obfuscation provider could not be initialized.");
				}
				if (input == null)
				{
					return input;
				}
				return this.obfuscationProvider.ProtectGenericData(input, 5);
			}

			// Token: 0x060000A3 RID: 163 RVA: 0x00003208 File Offset: 0x00001408
			internal static KeyConfiguration GetExchangeUploaderKeyConfiguration()
			{
				KeyConfiguration result = null;
				string text = null;
				string text2 = HashProvider.ExchangeDataMiningHashProvider.hookableGetExchangeUploaderConfigPathDelegate.Value();
				if (string.IsNullOrEmpty(text2) || !File.Exists(text = Path.Combine(text2, "Uploader.xml")))
				{
					text2 = HashProvider.ExchangeDataMiningHashProvider.hookableGetOfficeDataLoaderConfigPathDelegate.Value();
					if (!string.IsNullOrEmpty(text2))
					{
						text = Path.Combine(text2, "Uploader.xml");
					}
				}
				if (File.Exists(text))
				{
					XElement xelement = XElement.Load(text);
					XElement xelement2 = xelement.Element("KeyConfiguration");
					result = ConfigurationParser.ParseKeyConfiguration(xelement2);
				}
				return result;
			}

			// Token: 0x060000A4 RID: 164 RVA: 0x00003294 File Offset: 0x00001494
			private static ObfuscationProvider TryCreateObfuscationProvider()
			{
				ObfuscationProvider result = null;
				KeyConfiguration exchangeUploaderKeyConfiguration = HashProvider.ExchangeDataMiningHashProvider.GetExchangeUploaderKeyConfiguration();
				if (exchangeUploaderKeyConfiguration != null && exchangeUploaderKeyConfiguration.KeySettings != null && exchangeUploaderKeyConfiguration.KeySettings.Length > 0)
				{
					result = ObfuscationProvider.CreateHashOnlyObfuscationProvider(exchangeUploaderKeyConfiguration, null);
				}
				return result;
			}

			// Token: 0x060000A5 RID: 165 RVA: 0x000032C8 File Offset: 0x000014C8
			private static string GetExchangeUploaderConfigurationPath()
			{
				string result = null;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format("System\\CurrentControlSet\\Services\\{0}", HashProvider.ExchangeDataMiningHashProvider.ExchangeFileUploaderServiceName)))
				{
					if (registryKey != null)
					{
						string executablePath = InferenceCommonUtility.GetExecutablePath(registryKey.GetValue("ImagePath") as string);
						if (!string.IsNullOrEmpty(executablePath))
						{
							result = Path.GetDirectoryName(executablePath);
						}
					}
				}
				return result;
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x00003338 File Offset: 0x00001538
			private static string GetOfficeDataLoaderConfigurationPath()
			{
				string result = null;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format("System\\CurrentControlSet\\Services\\{0}", HashProvider.ExchangeDataMiningHashProvider.OfficeDataLoaderServiceName)))
				{
					if (registryKey != null)
					{
						string executablePath = InferenceCommonUtility.GetExecutablePath(registryKey.GetValue("ImagePath") as string);
						if (!string.IsNullOrEmpty(executablePath))
						{
							result = Path.GetDirectoryName(executablePath);
						}
					}
				}
				return result;
			}

			// Token: 0x040000B6 RID: 182
			private static readonly object singletonLock = new object();

			// Token: 0x040000B7 RID: 183
			private static readonly HashProvider.ExchangeDataMiningHashProvider instance = new HashProvider.ExchangeDataMiningHashProvider();

			// Token: 0x040000B8 RID: 184
			private static readonly Hookable<Func<string>> hookableGetExchangeUploaderConfigPathDelegate = Hookable<Func<string>>.Create(true, new Func<string>(HashProvider.ExchangeDataMiningHashProvider.GetExchangeUploaderConfigurationPath));

			// Token: 0x040000B9 RID: 185
			private static readonly Hookable<Func<string>> hookableGetOfficeDataLoaderConfigPathDelegate = Hookable<Func<string>>.Create(true, new Func<string>(HashProvider.ExchangeDataMiningHashProvider.GetOfficeDataLoaderConfigurationPath));

			// Token: 0x040000BA RID: 186
			private static readonly string ExchangeFileUploaderServiceName = "MSExchangeFileUpload";

			// Token: 0x040000BB RID: 187
			private static readonly string OfficeDataLoaderServiceName = "MSOfficeDataLoader";

			// Token: 0x040000BC RID: 188
			private ObfuscationProvider obfuscationProvider;
		}
	}
}
