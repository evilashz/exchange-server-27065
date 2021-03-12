using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Security;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Provisioning.Agent;

namespace Microsoft.Exchange.Provisioning
{
	// Token: 0x020001F7 RID: 503
	public class ProvisioningBroker
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x00035F4D File Offset: 0x0003414D
		internal Exception InitializationException
		{
			get
			{
				return this.initializationException;
			}
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00035F58 File Offset: 0x00034158
		internal ProvisioningBroker()
		{
			this.lookupTable = new Dictionary<string, List<ProvisioningBroker.ClassFactoryBucket>>(StringComparer.CurrentCultureIgnoreCase);
			this.agentNameLookupTable = new Dictionary<IProvisioningAgent, string>();
			CmdletExtensionAgent[] enabledAgents = null;
			try
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 53, ".ctor", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\Provisioning\\ProvisioningBroker.cs");
				enabledAgents = topologyConfigurationSession.FindCmdletExtensionAgents(true, true);
			}
			catch (Exception ex)
			{
				if (DataAccessHelper.IsDataAccessKnownException(ex))
				{
					throw new ProvisioningBrokerException(Strings.ProvisioningBrokerInitializationFailed(ex.Message), ex);
				}
				throw;
			}
			this.BuildHandlerLookupTable(enabledAgents, out this.initializationException);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00035FEC File Offset: 0x000341EC
		public ProvisioningHandler[] GetProvisioningHandlers(Task task)
		{
			string commandName = task.CurrentTaskContext.InvocationInfo.CommandName;
			if (commandName.StartsWith("Get-"))
			{
				return new ProvisioningHandler[0];
			}
			List<ProvisioningHandler> list = new List<ProvisioningHandler>();
			List<ProvisioningBroker.ClassFactoryBucket> list2 = new List<ProvisioningBroker.ClassFactoryBucket>();
			List<ProvisioningBroker.ClassFactoryBucket> collection;
			if (this.lookupTable.TryGetValue(commandName, out collection))
			{
				list2.AddRange(collection);
			}
			if (this.lookupTable.TryGetValue("*", out collection))
			{
				bool flag = list2.Count > 0;
				list2.AddRange(collection);
				if (flag)
				{
					list2.Sort();
				}
			}
			for (int i = 0; i < list2.Count; i++)
			{
				if (!TaskLogger.IsSetupLogging)
				{
					task.WriteVerbose(Strings.InstantiatingHandlerForAgent(i, this.agentNameLookupTable[list2[i].ClassFactory]));
				}
				ProvisioningHandler cmdletHandler = list2[i].ClassFactory.GetCmdletHandler(commandName);
				if (cmdletHandler != null)
				{
					cmdletHandler.TaskName = commandName;
					cmdletHandler.AgentName = list2[i].AgentName;
					list.Add(cmdletHandler);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000360F8 File Offset: 0x000342F8
		private void BuildHandlerLookupTable(CmdletExtensionAgent[] enabledAgents, out Exception ex)
		{
			ex = null;
			CmdletExtensionAgent cmdletExtensionAgent = null;
			try
			{
				for (int i = 0; i < enabledAgents.Length; i++)
				{
					if (cmdletExtensionAgent != null && cmdletExtensionAgent.Priority == enabledAgents[i].Priority)
					{
						throw new ArgumentException(Strings.ClashingPriorities(cmdletExtensionAgent.Priority.ToString(), enabledAgents[i].Name, cmdletExtensionAgent.Name));
					}
					cmdletExtensionAgent = enabledAgents[i];
					IProvisioningAgent classFactoryInstance = ProvisioningBroker.GetClassFactoryInstance(enabledAgents[i].Assembly, enabledAgents[i].ClassFactory, out ex);
					if (ex != null)
					{
						break;
					}
					this.agentNameLookupTable.Add(classFactoryInstance, enabledAgents[i].Name);
					IEnumerable<string> supportedCmdlets = classFactoryInstance.GetSupportedCmdlets();
					foreach (string key in supportedCmdlets)
					{
						List<ProvisioningBroker.ClassFactoryBucket> list;
						if (!this.lookupTable.TryGetValue(key, out list))
						{
							list = new List<ProvisioningBroker.ClassFactoryBucket>();
							this.lookupTable.Add(key, list);
						}
						list.Add(new ProvisioningBroker.ClassFactoryBucket(classFactoryInstance, enabledAgents[i].Name, enabledAgents[i].Priority));
					}
				}
			}
			catch (ConfigurationErrorsException ex2)
			{
				ex = ex2;
			}
			catch (ProvisioningException ex3)
			{
				ex = ex3;
			}
			catch (FileNotFoundException ex4)
			{
				ex = ex4;
			}
			catch (FileLoadException ex5)
			{
				ex = ex5;
			}
			catch (BadImageFormatException ex6)
			{
				ex = ex6;
			}
			catch (SecurityException ex7)
			{
				ex = ex7;
			}
			catch (UnauthorizedAccessException ex8)
			{
				ex = ex8;
			}
			catch (ArgumentException ex9)
			{
				ex = ex9;
			}
			catch (IOException ex10)
			{
				ex = ex10;
			}
			catch (MissingMethodException ex11)
			{
				ex = ex11;
			}
			catch (AmbiguousMatchException ex12)
			{
				ex = ex12;
			}
			catch (ReflectionTypeLoadException ex13)
			{
				ex = ex13;
			}
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000363A8 File Offset: 0x000345A8
		internal static IProvisioningAgent GetClassFactoryInstance(string assemblyName, string classFactoryName, out Exception ex)
		{
			string assemblyFile = Path.Combine(CmdletExtensionAgentsGlobalConfig.CmdletExtensionAgentsFolder, assemblyName);
			IProvisioningAgent provisioningAgent = null;
			ex = null;
			try
			{
				Assembly assembly = Assembly.LoadFrom(assemblyFile);
				Type type = assembly.GetType(classFactoryName);
				if (type == null)
				{
					ex = new ArgumentException(Strings.CouldntFindClassFactoryInAssembly(classFactoryName, assembly.FullName));
					return provisioningAgent;
				}
				object obj = assembly.CreateInstance(type.FullName);
				provisioningAgent = (obj as IProvisioningAgent);
				if (provisioningAgent == null)
				{
					ex = new ArgumentException(Strings.ClassFactoryDoesNotImplementIProvisioningAgent(classFactoryName, assembly.FullName));
					return provisioningAgent;
				}
			}
			catch (TargetInvocationException ex2)
			{
				ex = ex2.InnerException;
			}
			catch (ReflectionTypeLoadException ex3)
			{
				ex = ex3;
			}
			catch (MissingMethodException ex4)
			{
				ex = ex4;
			}
			catch (FileNotFoundException ex5)
			{
				ex = ex5;
			}
			catch (FileLoadException ex6)
			{
				ex = ex6;
			}
			catch (BadImageFormatException ex7)
			{
				ex = ex7;
			}
			catch (ArgumentException ex8)
			{
				ex = ex8;
			}
			return provisioningAgent;
		}

		// Token: 0x04000421 RID: 1057
		private Dictionary<string, List<ProvisioningBroker.ClassFactoryBucket>> lookupTable;

		// Token: 0x04000422 RID: 1058
		private Dictionary<IProvisioningAgent, string> agentNameLookupTable;

		// Token: 0x04000423 RID: 1059
		private Exception initializationException;

		// Token: 0x020001F8 RID: 504
		private class ClassFactoryBucket : IComparable<ProvisioningBroker.ClassFactoryBucket>
		{
			// Token: 0x060011B5 RID: 4533 RVA: 0x000364D0 File Offset: 0x000346D0
			public ClassFactoryBucket(IProvisioningAgent classFactory, string agentName, byte priority)
			{
				this.ClassFactory = classFactory;
				this.Priority = priority;
				this.AgentName = agentName;
			}

			// Token: 0x060011B6 RID: 4534 RVA: 0x000364ED File Offset: 0x000346ED
			int IComparable<ProvisioningBroker.ClassFactoryBucket>.CompareTo(ProvisioningBroker.ClassFactoryBucket other)
			{
				if (other != null)
				{
					return (int)(this.Priority - other.Priority);
				}
				return -1;
			}

			// Token: 0x04000424 RID: 1060
			public readonly IProvisioningAgent ClassFactory;

			// Token: 0x04000425 RID: 1061
			public readonly string AgentName;

			// Token: 0x04000426 RID: 1062
			public readonly byte Priority;
		}
	}
}
