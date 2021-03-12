using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Monad;
using Microsoft.Win32;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001D6 RID: 470
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MonadRunspaceConfiguration : RunspaceConfiguration
	{
		// Token: 0x06001109 RID: 4361 RVA: 0x000341E5 File Offset: 0x000323E5
		private MonadRunspaceConfiguration()
		{
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x000341ED File Offset: 0x000323ED
		public override string ShellId
		{
			get
			{
				return "Exchange";
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x000341F4 File Offset: 0x000323F4
		public override RunspaceConfigurationEntryCollection<CmdletConfigurationEntry> Cmdlets
		{
			get
			{
				return this.miniShellCmdlets;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x000341FC File Offset: 0x000323FC
		private static bool IsEdgeMachine
		{
			get
			{
				if (MonadRunspaceConfiguration.isEdgeMachine == null)
				{
					object value = Registry.LocalMachine.GetValue("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole\\AdamSettings\\MSExchange", null);
					MonadRunspaceConfiguration.isEdgeMachine = new bool?(null != value);
				}
				return MonadRunspaceConfiguration.isEdgeMachine.Value;
			}
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00034244 File Offset: 0x00032444
		public static void EnsureMonadScriptsAreRunnable()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\PowerShell\\1\\ShellIds\\Microsoft.PowerShell"))
			{
				registryKey.SetValue("ExecutionPolicy", "RemoteSigned");
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00034290 File Offset: 0x00032490
		public static void AddArray(CmdletConfigurationEntry[] cmdlets)
		{
			if (MonadRunspaceConfiguration.singleShellConfiguration != MonadRunspaceConfiguration.SingleShellConfigurationMode.Mini)
			{
				MonadRunspaceConfiguration.singleShellConfiguration = MonadRunspaceConfiguration.SingleShellConfigurationMode.Mixed;
			}
			MonadRunspaceConfiguration.cmdletConfigurationEntries.Add(cmdlets);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x000342AB File Offset: 0x000324AB
		public static void AddPSSnapInName(string mshSnapInName)
		{
			if (MonadRunspaceConfiguration.cmdletConfigurationEntries.Count == 0)
			{
				MonadRunspaceConfiguration.singleShellConfiguration = MonadRunspaceConfiguration.SingleShellConfigurationMode.Default;
			}
			else
			{
				MonadRunspaceConfiguration.singleShellConfiguration = MonadRunspaceConfiguration.SingleShellConfigurationMode.Mixed;
			}
			MonadRunspaceConfiguration.mshSnapInNames.Add(mshSnapInName);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x000342D2 File Offset: 0x000324D2
		public static void Clear()
		{
			MonadRunspaceConfiguration.singleShellConfiguration = MonadRunspaceConfiguration.SingleShellConfigurationMode.Clear;
			MonadRunspaceConfiguration.ClearEntries();
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x000342DF File Offset: 0x000324DF
		public static void ClearAll()
		{
			MonadRunspaceConfiguration.singleShellConfiguration = MonadRunspaceConfiguration.SingleShellConfigurationMode.Mini;
			MonadRunspaceConfiguration.ClearEntries();
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x000342EC File Offset: 0x000324EC
		public new static RunspaceConfiguration Create()
		{
			RunspaceConfiguration runspaceConfiguration = null;
			if (MonadRunspaceConfiguration.singleShellConfiguration == MonadRunspaceConfiguration.SingleShellConfigurationMode.Mini)
			{
				MonadRunspaceConfiguration monadRunspaceConfiguration = new MonadRunspaceConfiguration();
				monadRunspaceConfiguration.miniShellCmdlets = new RunspaceConfigurationEntryCollection<CmdletConfigurationEntry>();
				foreach (CmdletConfigurationEntry[] items in MonadRunspaceConfiguration.cmdletConfigurationEntries)
				{
					monadRunspaceConfiguration.miniShellCmdlets.Append(items);
				}
				runspaceConfiguration = monadRunspaceConfiguration;
			}
			else
			{
				runspaceConfiguration = RunspaceConfiguration.Create();
			}
			if (MonadRunspaceConfiguration.singleShellConfiguration == MonadRunspaceConfiguration.SingleShellConfigurationMode.Default)
			{
				if (MonadRunspaceConfiguration.IsEdgeMachine)
				{
					MonadRunspaceConfiguration.AddPSSnapIn(runspaceConfiguration, "Microsoft.Exchange.Management.PowerShell.E2010");
				}
				else
				{
					CmdletConfigurationEntry[] nonEdgeCmdletConfigurationEntries = MonadRunspaceConfiguration.GetNonEdgeCmdletConfigurationEntries();
					runspaceConfiguration.Cmdlets.Append(nonEdgeCmdletConfigurationEntries);
				}
			}
			if (MonadRunspaceConfiguration.IsMixedOrDefaultMode())
			{
				foreach (string mshSnapInName in MonadRunspaceConfiguration.mshSnapInNames)
				{
					MonadRunspaceConfiguration.AddPSSnapIn(runspaceConfiguration, mshSnapInName);
				}
				if (MonadRunspaceConfiguration.singleShellConfiguration == MonadRunspaceConfiguration.SingleShellConfigurationMode.Mixed)
				{
					foreach (CmdletConfigurationEntry[] items2 in MonadRunspaceConfiguration.cmdletConfigurationEntries)
					{
						runspaceConfiguration.Cmdlets.Append(items2);
					}
				}
			}
			return runspaceConfiguration;
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00034434 File Offset: 0x00032634
		private static void ClearEntries()
		{
			MonadRunspaceConfiguration.cmdletConfigurationEntries.Clear();
			MonadRunspaceConfiguration.mshSnapInNames.Clear();
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0003444C File Offset: 0x0003264C
		private static void AddPSSnapIn(RunspaceConfiguration runspaceConfiguration, string mshSnapInName)
		{
			PSSnapInException ex = null;
			PSSnapInInfo pssnapInInfo = runspaceConfiguration.AddPSSnapIn(mshSnapInName, out ex);
			if (ex != null)
			{
				throw ex;
			}
			if (pssnapInInfo != null)
			{
				ExTraceGlobals.IntegrationTracer.Information(0L, mshSnapInName + " added to Runspace:" + pssnapInInfo.ToString());
			}
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0003448A File Offset: 0x0003268A
		private static bool IsMixedOrDefaultMode()
		{
			return MonadRunspaceConfiguration.singleShellConfiguration == MonadRunspaceConfiguration.SingleShellConfigurationMode.Default || MonadRunspaceConfiguration.singleShellConfiguration == MonadRunspaceConfiguration.SingleShellConfigurationMode.Mixed;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000344A0 File Offset: 0x000326A0
		private static CmdletConfigurationEntry[] GetNonEdgeCmdletConfigurationEntries()
		{
			string assemblyFile = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.PowerShell.Configuration.dll");
			Assembly assembly = Assembly.LoadFrom(assemblyFile);
			Type type = assembly.GetType("Microsoft.Exchange.Management.PowerShell.CmdletConfigurationEntries", true, true);
			List<CmdletConfigurationEntry> list = new List<CmdletConfigurationEntry>();
			foreach (string name in new string[]
			{
				"ExchangeCmdletConfigurationEntries",
				"ExchangeNonEdgeCmdletConfigurationEntries"
			})
			{
				PropertyInfo property = type.GetProperty(name, BindingFlags.Static | BindingFlags.Public);
				CmdletConfigurationEntry[] collection = (CmdletConfigurationEntry[])property.GetValue(null, null);
				list.AddRange(collection);
			}
			return list.ToArray();
		}

		// Token: 0x040003B5 RID: 949
		private const string monadShellIds = "SOFTWARE\\Microsoft\\PowerShell\\1\\ShellIds\\Microsoft.PowerShell";

		// Token: 0x040003B6 RID: 950
		private const string shellPolicy = "ExecutionPolicy";

		// Token: 0x040003B7 RID: 951
		private const string shellPolicyValue = "RemoteSigned";

		// Token: 0x040003B8 RID: 952
		private const string edgeRegistryKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole\\AdamSettings\\MSExchange";

		// Token: 0x040003B9 RID: 953
		private const string shellId = "Exchange";

		// Token: 0x040003BA RID: 954
		private static MonadRunspaceConfiguration.SingleShellConfigurationMode singleShellConfiguration;

		// Token: 0x040003BB RID: 955
		private static List<CmdletConfigurationEntry[]> cmdletConfigurationEntries = new List<CmdletConfigurationEntry[]>();

		// Token: 0x040003BC RID: 956
		private static List<string> mshSnapInNames = new List<string>();

		// Token: 0x040003BD RID: 957
		private static bool? isEdgeMachine;

		// Token: 0x040003BE RID: 958
		private RunspaceConfigurationEntryCollection<CmdletConfigurationEntry> miniShellCmdlets;

		// Token: 0x020001D7 RID: 471
		private enum SingleShellConfigurationMode
		{
			// Token: 0x040003C0 RID: 960
			Default,
			// Token: 0x040003C1 RID: 961
			Mixed,
			// Token: 0x040003C2 RID: 962
			Clear,
			// Token: 0x040003C3 RID: 963
			Mini
		}
	}
}
