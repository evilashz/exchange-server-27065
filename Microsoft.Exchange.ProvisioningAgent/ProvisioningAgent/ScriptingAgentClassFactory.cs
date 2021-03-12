using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.Provisioning.Agent;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200005D RID: 93
	[ProvisioningAgentClassFactory]
	internal class ScriptingAgentClassFactory : IProvisioningAgent
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000F34E File Offset: 0x0000D54E
		public ScriptingAgentClassFactory()
		{
			this.xmlConfigPath = Path.Combine(CmdletExtensionAgentsGlobalConfig.CmdletExtensionAgentsFolder, "ScriptingAgentConfig.xml");
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000F36C File Offset: 0x0000D56C
		private ScriptingAgentConfiguration Configuration
		{
			get
			{
				if (this.configuration == null || !File.Exists(this.xmlConfigPath) || this.configFileLastWriteTime < File.GetLastWriteTimeUtc(this.xmlConfigPath))
				{
					this.configuration = new ScriptingAgentConfiguration(this.xmlConfigPath);
					this.configFileLastWriteTime = DateTime.UtcNow;
				}
				return this.configuration;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000F3C8 File Offset: 0x0000D5C8
		public IEnumerable<string> GetSupportedCmdlets()
		{
			return this.Configuration.GetAllSupportedCmdlets();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000F3D5 File Offset: 0x0000D5D5
		public ProvisioningHandler GetCmdletHandler(string cmdletName)
		{
			return new ScriptingAgentHandler(this.Configuration);
		}

		// Token: 0x04000138 RID: 312
		private const string ScriptingAgentConfig = "ScriptingAgentConfig.xml";

		// Token: 0x04000139 RID: 313
		private ScriptingAgentConfiguration configuration;

		// Token: 0x0400013A RID: 314
		private string xmlConfigPath;

		// Token: 0x0400013B RID: 315
		private DateTime configFileLastWriteTime;
	}
}
