using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Exchange.Provisioning.Agent
{
	// Token: 0x02000203 RID: 515
	public abstract class ProvisioningAgentClassFactoryBase : IProvisioningAgent
	{
		// Token: 0x060011FA RID: 4602 RVA: 0x000378CF File Offset: 0x00035ACF
		public ProvisioningAgentClassFactoryBase()
		{
			this.lookupTable = new Dictionary<string, Type>(StringComparer.CurrentCultureIgnoreCase);
			this.PopulateDictionary();
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000378F0 File Offset: 0x00035AF0
		private void PopulateDictionary()
		{
			Assembly assembly = Assembly.GetAssembly(base.GetType());
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				if (type.IsPublic && type.IsVisible && !type.IsAbstract && !type.IsInterface)
				{
					object[] customAttributes = type.GetCustomAttributes(typeof(CmdletHandlerAttribute), false);
					foreach (CmdletHandlerAttribute cmdletHandlerAttribute in customAttributes)
					{
						if (this.lookupTable.ContainsKey(cmdletHandlerAttribute.TaskName))
						{
							throw new ArgumentException("Task " + cmdletHandlerAttribute.TaskName + " is handled by two or more handlers defined in assembly a");
						}
						this.lookupTable.Add(cmdletHandlerAttribute.TaskName, type);
					}
				}
			}
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x000379D4 File Offset: 0x00035BD4
		public IEnumerable<string> GetSupportedCmdlets()
		{
			return this.lookupTable.Keys;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x000379E4 File Offset: 0x00035BE4
		public ProvisioningHandler GetCmdletHandler(string cmdletName)
		{
			Type type;
			if (this.lookupTable.TryGetValue(cmdletName, out type))
			{
				return Activator.CreateInstance(type) as ProvisioningHandler;
			}
			if (this.lookupTable.TryGetValue("*", out type))
			{
				return Activator.CreateInstance(type) as ProvisioningHandler;
			}
			throw new ArgumentException(cmdletName);
		}

		// Token: 0x04000443 RID: 1091
		private Dictionary<string, Type> lookupTable;
	}
}
