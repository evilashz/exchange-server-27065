using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000007 RID: 7
	public class DagNetEnvironment
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000027B3 File Offset: 0x000009B3
		private DagNetEnvironment()
		{
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000027CE File Offset: 0x000009CE
		public static DagNetEnvironment Instance
		{
			get
			{
				return DagNetEnvironment.instance;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000027D8 File Offset: 0x000009D8
		public static DagNetChooser NetChooser
		{
			get
			{
				if (DagNetEnvironment.Instance.currentChooser == null)
				{
					lock (DagNetEnvironment.Instance)
					{
						if (DagNetEnvironment.Instance.currentChooser == null)
						{
							DagNetEnvironment.Instance.currentChooser = new DagNetChooser();
						}
					}
				}
				return DagNetEnvironment.Instance.currentChooser;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002844 File Offset: 0x00000A44
		public static int ConnectTimeoutInSec
		{
			get
			{
				int num;
				if (DagNetEnvironment.TryRegistryRead<int>("ConnectTimeoutInSec", 15, out num) == null)
				{
					DagNetEnvironment.Instance.connectTimeoutInSec = num;
				}
				return DagNetEnvironment.Instance.connectTimeoutInSec;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002876 File Offset: 0x00000A76
		private static ITracer Tracer
		{
			get
			{
				return Dependencies.DagNetEnvironmentTracer;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000287D File Offset: 0x00000A7D
		public static void Initialize()
		{
			DagNetEnvironment.instance = new DagNetEnvironment();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000288C File Offset: 0x00000A8C
		public static Exception PublishDagNetConfig(DagNetConfig cfg)
		{
			Exception result;
			try
			{
				string persistString = cfg.Serialize();
				result = DagNetEnvironment.PublishDagNetConfig(persistString);
			}
			catch (SerializationException ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002908 File Offset: 0x00000B08
		public static Exception PublishDagNetConfig(string persistString)
		{
			Exception ex = RegistryUtil.RunRegistryFunction(delegate()
			{
				IRegistryWriter registryWriter = Dependencies.RegistryWriter;
				registryWriter.CreateSubKey(Registry.LocalMachine, DagNetEnvironment.RegistryRootKeyName);
				registryWriter.SetValue(Registry.LocalMachine, DagNetEnvironment.RegistryRootKeyName, "DagNetConfig", persistString);
			});
			if (ex != null)
			{
				DagNetEnvironment.Tracer.TraceError(0L, "PublishDagNetConfig fails: {0}", new object[]
				{
					ex
				});
			}
			return ex;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002955 File Offset: 0x00000B55
		public static DagNetConfig FetchNetConfig()
		{
			return DagNetEnvironment.Instance.FetchConfigInternal();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002964 File Offset: 0x00000B64
		public static DagNetConfig FetchLastKnownNetConfig()
		{
			DagNetConfig dagNetConfig = DagNetEnvironment.Instance.currentNetConfig;
			if (dagNetConfig == null)
			{
				dagNetConfig = DagNetEnvironment.Instance.FetchConfigInternal();
			}
			return dagNetConfig;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000298B File Offset: 0x00000B8B
		public static int CircularIncrement(int input, int limit)
		{
			if (input < limit - 1)
			{
				return input + 1;
			}
			return 0;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029CC File Offset: 0x00000BCC
		internal static Exception TryRegistryRead<T>(string valueName, T defaultVal, out T returnedVal)
		{
			returnedVal = defaultVal;
			IRegistryReader reader = Dependencies.RegistryReader;
			T readVal = defaultVal;
			Exception ex = RegistryUtil.RunRegistryFunction(delegate()
			{
				readVal = reader.GetValue<T>(Registry.LocalMachine, DagNetEnvironment.RegistryRootKeyName, valueName, defaultVal);
			});
			if (ex != null)
			{
				DagNetEnvironment.Tracer.TraceError(0L, "TryRegistryRead({0}) fails: {1}", new object[]
				{
					valueName,
					ex
				});
			}
			else
			{
				returnedVal = readVal;
			}
			return ex;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A5C File Offset: 0x00000C5C
		private static DagNetConfig TryDeserializeConfig(string serializedConfig, out Exception ex)
		{
			DagNetConfig result = null;
			ex = null;
			try
			{
				result = DagNetConfig.Deserialize(serializedConfig);
			}
			catch (SerializationException ex2)
			{
				ex = ex2;
				DagNetEnvironment.Tracer.TraceError(0L, "TryDeserializeConfig failed {0}", new object[]
				{
					ex
				});
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002AAC File Offset: 0x00000CAC
		private DagNetConfig FetchConfigInternal()
		{
			DagNetConfig result;
			lock (this.dagNetConfigLock)
			{
				string text;
				if (DagNetEnvironment.TryRegistryRead<string>("DagNetConfig", null, out text) == null && !string.IsNullOrEmpty(text))
				{
					int hashCode = text.GetHashCode();
					if (this.serializedNetConfig == null || this.serializedNetConfigHashCode != hashCode)
					{
						DagNetEnvironment.Tracer.TraceDebug((long)this.GetHashCode(), "NetConfig change detected.", new object[0]);
						Exception ex;
						DagNetConfig dagNetConfig = DagNetEnvironment.TryDeserializeConfig(text, out ex);
						if (dagNetConfig != null)
						{
							this.currentNetConfig = dagNetConfig;
						}
						this.serializedNetConfig = text;
						this.serializedNetConfigHashCode = hashCode;
					}
				}
				if (this.currentNetConfig == null)
				{
					this.currentNetConfig = new DagNetConfig();
				}
				result = this.currentNetConfig;
			}
			return result;
		}

		// Token: 0x04000017 RID: 23
		public const string DagNetConfigRegistryValueName = "DagNetConfig";

		// Token: 0x04000018 RID: 24
		public const string DagNetTimeoutRegistryValue = "ConnectTimeoutInSec";

		// Token: 0x04000019 RID: 25
		public const int DefaultConnectTimeoutInSec = 15;

		// Token: 0x0400001A RID: 26
		public static readonly string RegistryRootKeyName = Dependencies.Config.RegistryRootKeyName;

		// Token: 0x0400001B RID: 27
		private static DagNetEnvironment instance = new DagNetEnvironment();

		// Token: 0x0400001C RID: 28
		private DagNetChooser currentChooser;

		// Token: 0x0400001D RID: 29
		private int connectTimeoutInSec = 15;

		// Token: 0x0400001E RID: 30
		private DagNetConfig currentNetConfig;

		// Token: 0x0400001F RID: 31
		private string serializedNetConfig;

		// Token: 0x04000020 RID: 32
		private int serializedNetConfigHashCode;

		// Token: 0x04000021 RID: 33
		private object dagNetConfigLock = new object();
	}
}
