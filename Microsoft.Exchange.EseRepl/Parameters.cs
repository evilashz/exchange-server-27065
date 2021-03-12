using System;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000012 RID: 18
	internal class Parameters
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003370 File Offset: 0x00001570
		public static Parameters CurrentValues
		{
			get
			{
				if (Parameters.instance == null)
				{
					Parameters parameters = new Parameters();
					parameters.ReadFromRegistry();
					Parameters.instance = parameters;
				}
				return Parameters.instance;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000339C File Offset: 0x0000159C
		public Parameters()
		{
			this.RegistryRootKeyName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters";
			this.DefaultEventSuppressionInterval = TimeSpan.FromSeconds(900.0);
			this.DisableSocketStream = false;
			this.LogShipCompressionDisable = false;
			this.DisableNetworkSigning = false;
			this.LogDiagnosticNetworkEvents = false;
			this.LogCopyNetworkTransferSize = 16777216;
			this.SeedingNetworkTransferSize = 16777216;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003400 File Offset: 0x00001600
		private bool IntToBool(int i)
		{
			return i != 0;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003574 File Offset: 0x00001774
		private void ReadFromRegistry()
		{
			ITracer dagNetEnvironmentTracer = Dependencies.DagNetEnvironmentTracer;
			IRegistryReader reader = Dependencies.RegistryReader;
			Exception ex = RegistryUtil.RunRegistryFunction(delegate()
			{
				int value = reader.GetValue<int>(Registry.LocalMachine, this.RegistryRootKeyName, "DisableSocketStream", 0);
				this.DisableSocketStream = this.IntToBool(value);
				value = reader.GetValue<int>(Registry.LocalMachine, this.RegistryRootKeyName, "LogShipCompressionDisable", 0);
				this.LogShipCompressionDisable = this.IntToBool(value);
				value = reader.GetValue<int>(Registry.LocalMachine, this.RegistryRootKeyName, "DisableNetworkSigning", 0);
				this.DisableNetworkSigning = this.IntToBool(value);
				value = reader.GetValue<int>(Registry.LocalMachine, this.RegistryRootKeyName, "LogDiagnosticNetworkEvents", 0);
				this.LogDiagnosticNetworkEvents = this.IntToBool(value);
				value = reader.GetValue<int>(Registry.LocalMachine, this.RegistryRootKeyName, "LogCopyNetworkTransferSize", this.LogCopyNetworkTransferSize);
				this.LogCopyNetworkTransferSize = value;
				value = reader.GetValue<int>(Registry.LocalMachine, this.RegistryRootKeyName, "SeedingNetworkTransferSize", this.SeedingNetworkTransferSize);
				this.SeedingNetworkTransferSize = value;
			});
			if (ex != null)
			{
				dagNetEnvironmentTracer.TraceError(0L, "ReadFromRegistry({0}) fails: {1}", new object[]
				{
					this.RegistryRootKeyName,
					ex
				});
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000035D6 File Offset: 0x000017D6
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000035DE File Offset: 0x000017DE
		public string RegistryRootKeyName { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000035E7 File Offset: 0x000017E7
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000035EF File Offset: 0x000017EF
		public TimeSpan DefaultEventSuppressionInterval { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000035F8 File Offset: 0x000017F8
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003600 File Offset: 0x00001800
		public bool DisableSocketStream { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003609 File Offset: 0x00001809
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003611 File Offset: 0x00001811
		public bool LogShipCompressionDisable { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000361A File Offset: 0x0000181A
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003622 File Offset: 0x00001822
		public bool DisableNetworkSigning { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000362B File Offset: 0x0000182B
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003633 File Offset: 0x00001833
		public int LogCopyNetworkTransferSize { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000363C File Offset: 0x0000183C
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003644 File Offset: 0x00001844
		public int SeedingNetworkTransferSize { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000364D File Offset: 0x0000184D
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003655 File Offset: 0x00001855
		public bool LogDiagnosticNetworkEvents { get; set; }

		// Token: 0x0400003F RID: 63
		public const int LogFileSize = 1048576;

		// Token: 0x04000040 RID: 64
		internal const string DefaultRegistryRootKeyName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters";

		// Token: 0x04000041 RID: 65
		private static Parameters instance;
	}
}
