using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000D8 RID: 216
	internal class ConfigStore
	{
		// Token: 0x0600089C RID: 2204 RVA: 0x00028F5C File Offset: 0x0002715C
		public static string ReadCompressionConfigString(out Exception ex)
		{
			ex = null;
			string result = null;
			try
			{
				IRegistryReader instance = RegistryReader.Instance;
				result = instance.GetValue<string>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", "CompressionConfig", null);
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			catch (SecurityException ex4)
			{
				ex = ex4;
			}
			return result;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00028FCC File Offset: 0x000271CC
		public static Exception WriteCompressionConfigString(string xmlString)
		{
			Exception result = null;
			try
			{
				IRegistryWriter instance = RegistryWriter.Instance;
				instance.SetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", "CompressionConfig", xmlString, RegistryValueKind.String);
			}
			catch (IOException ex)
			{
				result = ex;
			}
			catch (UnauthorizedAccessException ex2)
			{
				result = ex2;
			}
			catch (SecurityException ex3)
			{
				result = ex3;
			}
			return result;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00029034 File Offset: 0x00027234
		public static CompressionConfig ReadCompressionConfig(out Exception ex)
		{
			ex = null;
			try
			{
				IRegistryReader instance = RegistryReader.Instance;
				string value = instance.GetValue<string>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", "CompressionConfig", null);
				if (value == null)
				{
					return new CompressionConfig
					{
						Provider = CompressionConfig.CompressionProvider.Xpress
					};
				}
				if (!string.IsNullOrEmpty(value))
				{
					return (CompressionConfig)SerializationUtil.XmlToObject(value, typeof(CompressionConfig));
				}
			}
			catch (InvalidOperationException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				ex = ex4;
			}
			catch (SecurityException ex5)
			{
				ex = ex5;
			}
			catch (SerializationException ex6)
			{
				ex = ex6;
			}
			return new CompressionConfig();
		}

		// Token: 0x040003B6 RID: 950
		public const string CompressionConfigRegValueName = "CompressionConfig";
	}
}
