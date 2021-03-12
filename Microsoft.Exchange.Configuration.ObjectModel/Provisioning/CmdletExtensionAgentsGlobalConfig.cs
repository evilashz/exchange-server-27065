using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Provisioning
{
	// Token: 0x020001FA RID: 506
	internal class CmdletExtensionAgentsGlobalConfig
	{
		// Token: 0x060011D3 RID: 4563 RVA: 0x0003731C File Offset: 0x0003551C
		public CmdletExtensionAgentsGlobalConfig(ITopologyConfigurationSession session)
		{
			CmdletExtensionAgent[] array = session.FindCmdletExtensionAgents(false, false);
			this.prioritiesInUse = new CmdletExtensionAgent[256];
			this.agentIdentities = new List<string>(array.Length);
			this.configurationIssues = new List<LocalizedString>();
			foreach (CmdletExtensionAgent cmdletExtensionAgent in array)
			{
				if (this.prioritiesInUse[(int)cmdletExtensionAgent.Priority] != null)
				{
					this.configurationIssues.Add(Strings.ClashingPriorities(cmdletExtensionAgent.Priority.ToString(), cmdletExtensionAgent.Name, this.prioritiesInUse[(int)cmdletExtensionAgent.Priority].Name));
				}
				else
				{
					this.prioritiesInUse[(int)cmdletExtensionAgent.Priority] = cmdletExtensionAgent;
				}
				string factoryIdentity = CmdletExtensionAgentsGlobalConfig.GetFactoryIdentity(cmdletExtensionAgent.Assembly, cmdletExtensionAgent.ClassFactory);
				if (this.agentIdentities.Contains(factoryIdentity))
				{
					this.configurationIssues.Add(Strings.ClashingIdentities(cmdletExtensionAgent.Assembly, cmdletExtensionAgent.ClassFactory));
				}
				else
				{
					this.agentIdentities.Add(factoryIdentity);
				}
				if (this.nextAvailablePriority < (int)(cmdletExtensionAgent.Priority + 1))
				{
					this.nextAvailablePriority = (int)(cmdletExtensionAgent.Priority + 1);
				}
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00037440 File Offset: 0x00035640
		public static string CmdletExtensionAgentsFolder
		{
			get
			{
				if (CmdletExtensionAgentsGlobalConfig.IsNotRunningDfpowa)
				{
					return Path.Combine(ConfigurationContext.Setup.BinPath, "CmdletExtensionAgents");
				}
				string codeBase = Assembly.GetExecutingAssembly().CodeBase;
				UriBuilder uriBuilder = new UriBuilder(codeBase);
				string path = Uri.UnescapeDataString(uriBuilder.Path);
				return Path.GetDirectoryName(path);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00037488 File Offset: 0x00035688
		public bool IsNextAvailablePriorityValid
		{
			get
			{
				return this.nextAvailablePriority < 256;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00037497 File Offset: 0x00035697
		public byte NextAvailablePriority
		{
			get
			{
				return (byte)this.nextAvailablePriority;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x000374A0 File Offset: 0x000356A0
		internal CmdletExtensionAgent[] PrioritiesInUse
		{
			get
			{
				return this.prioritiesInUse;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x000374A8 File Offset: 0x000356A8
		internal List<CmdletExtensionAgent> ObjectsToSave
		{
			get
			{
				return this.objectsToSave;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x000374B0 File Offset: 0x000356B0
		internal LocalizedString[] ConfigurationIssues
		{
			get
			{
				return this.configurationIssues.ToArray();
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000374BD File Offset: 0x000356BD
		public bool IsPriorityAvailable(byte priority, CmdletExtensionAgent thisAgent)
		{
			return this.prioritiesInUse[(int)priority] == null || (thisAgent != null && this.prioritiesInUse[(int)priority].Guid == thisAgent.Guid);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000374E8 File Offset: 0x000356E8
		public bool IsFactoryIdentityInUse(string assembly, string classFactory)
		{
			return this.agentIdentities.Contains(CmdletExtensionAgentsGlobalConfig.GetFactoryIdentity(assembly, classFactory));
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000374FC File Offset: 0x000356FC
		public bool FreeUpPriorityValue(byte priority)
		{
			this.objectsToSave = new List<CmdletExtensionAgent>();
			for (byte b = priority; b <= 255; b += 1)
			{
				CmdletExtensionAgent cmdletExtensionAgent = this.PrioritiesInUse[(int)b];
				if (cmdletExtensionAgent == null)
				{
					return true;
				}
				if (b == 255)
				{
					return false;
				}
				CmdletExtensionAgent cmdletExtensionAgent2 = cmdletExtensionAgent;
				cmdletExtensionAgent2.Priority += 1;
				this.objectsToSave.Add(cmdletExtensionAgent);
			}
			return true;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0003755B File Offset: 0x0003575B
		internal static string GetFactoryIdentity(string assembly, string classFactory)
		{
			return assembly.ToLower() + "::" + classFactory;
		}

		// Token: 0x04000428 RID: 1064
		private const string exchangeSetupLocation = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";

		// Token: 0x04000429 RID: 1065
		private const string exchangeInstallPathValue = "MsiInstallPath";

		// Token: 0x0400042A RID: 1066
		private const string CmdletExtensionAgentsSubFolder = "CmdletExtensionAgents";

		// Token: 0x0400042B RID: 1067
		private static readonly bool IsNotRunningDfpowa = string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["IsPreCheckinApp"]) || StringComparer.OrdinalIgnoreCase.Equals("false", ConfigurationManager.AppSettings["IsPreCheckinApp"]);

		// Token: 0x0400042C RID: 1068
		private int nextAvailablePriority;

		// Token: 0x0400042D RID: 1069
		private CmdletExtensionAgent[] prioritiesInUse;

		// Token: 0x0400042E RID: 1070
		private List<string> agentIdentities;

		// Token: 0x0400042F RID: 1071
		private List<CmdletExtensionAgent> objectsToSave;

		// Token: 0x04000430 RID: 1072
		private List<LocalizedString> configurationIssues;
	}
}
