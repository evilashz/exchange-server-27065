using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Configuration
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000F RID: 15 RVA: 0x00002184 File Offset: 0x00000384
		// (remove) Token: 0x06000010 RID: 16 RVA: 0x000021B8 File Offset: 0x000003B8
		public static event Action<object> ConfigurationChanged;

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021EB File Offset: 0x000003EB
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002209 File Offset: 0x00000409
		public static string AppConfigFileName
		{
			get
			{
				if (Configuration.appConfigFile == null)
				{
					Configuration.appConfigFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
				}
				return Configuration.appConfigFile;
			}
			set
			{
				Configuration.appConfigFile = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002214 File Offset: 0x00000414
		public static ICollection<AuxiliaryBlock> DefaultEcDoConnectExAuxOutBlocks
		{
			get
			{
				if (Configuration.defaultEcDoConnectExAuxOutBlocks != null)
				{
					return Configuration.defaultEcDoConnectExAuxOutBlocks;
				}
				return Configuration.defaultEcDoConnectExAuxOutBlocks = Array.AsReadOnly<AuxiliaryBlock>(new AuxiliaryBlock[]
				{
					new MapiEndpointAuxiliaryBlock(MapiEndpointProcessType.RpcClientAccess, Configuration.ServiceConfiguration.ThisServerFqdn),
					new ServerCapabilitiesAuxiliaryBlock(ServerCapabilityFlag.PackedFastTransferUploadBuffers | ServerCapabilityFlag.PackedWriteStreamExtendedUploadBuffers),
					new EndpointCapabilitiesAuxiliaryBlock(EndpointCapabilityFlag.SingleEndpoint)
				});
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000226B File Offset: 0x0000046B
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002272 File Offset: 0x00000472
		public static ConfigurationSchema.EventLogger EventLogger { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000227A File Offset: 0x0000047A
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002281 File Offset: 0x00000481
		public static ProtocolLogConfiguration ProtocolLogConfiguration
		{
			get
			{
				return Configuration.protocolLogConfiguration;
			}
			set
			{
				Configuration.protocolLogConfiguration = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002289 File Offset: 0x00000489
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002290 File Offset: 0x00000490
		public static ServiceConfiguration ServiceConfiguration { get; set; }

		// Token: 0x0600001A RID: 26 RVA: 0x00002298 File Offset: 0x00000498
		internal static void InternalFireOnChanged(object newConfiguration)
		{
			Action<object> configurationChanged = Configuration.ConfigurationChanged;
			if (configurationChanged != null)
			{
				configurationChanged(newConfiguration);
			}
		}

		// Token: 0x04000007 RID: 7
		private static ICollection<AuxiliaryBlock> defaultEcDoConnectExAuxOutBlocks;

		// Token: 0x04000008 RID: 8
		private static ProtocolLogConfiguration protocolLogConfiguration = ProtocolLogConfiguration.Default;

		// Token: 0x04000009 RID: 9
		private static string appConfigFile = null;
	}
}
