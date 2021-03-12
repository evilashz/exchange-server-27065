using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport.Delivery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.EventLog;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x0200007B RID: 123
	internal sealed class FactoryTable
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x00012FD0 File Offset: 0x000111D0
		public FactoryTable(IEnumerable agents, FactoryInitializer factoryInitializer)
		{
			this.factoriesByAgentId = new Dictionary<string, AgentFactory>();
			this.agentManagersByAgentId = new Dictionary<string, AgentManager>();
			Dictionary<string, AgentFactory> dictionary = new Dictionary<string, AgentFactory>();
			DateTime utcNow = DateTime.UtcNow;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in agents)
			{
				AgentInfo agentInfo = (AgentInfo)obj;
				if (this.factoriesByAgentId.ContainsKey(agentInfo.Id))
				{
					throw new ExchangeConfigurationException(MExRuntimeStrings.DuplicateAgentName(agentInfo.AgentName));
				}
				DateTime utcNow2 = DateTime.UtcNow;
				AgentFactory agentFactory;
				if (!dictionary.TryGetValue(agentInfo.FactoryTypeName, out agentFactory))
				{
					agentFactory = FactoryTable.CreateAgentFactory(agentInfo);
					if (factoryInitializer != null)
					{
						factoryInitializer(agentFactory);
					}
					dictionary.Add(agentInfo.FactoryTypeName, agentFactory);
				}
				this.factoriesByAgentId.Add(agentInfo.Id, agentFactory);
				AgentManager agentManagerInstance = FactoryTable.GetAgentManagerInstance(agentInfo);
				if (agentManagerInstance != null)
				{
					this.agentManagersByAgentId.Add(agentInfo.Id, agentManagerInstance);
				}
				TimeSpan timeSpan = DateTime.UtcNow - utcNow2;
				stringBuilder.AppendLine();
				stringBuilder.Append(agentInfo.AgentName);
				stringBuilder.Append(": ");
				stringBuilder.Append(timeSpan);
			}
			this.startupDiagnosticInfo = stringBuilder.ToString();
			TimeSpan timeSpan2 = DateTime.UtcNow - utcNow;
			if (timeSpan2 > FactoryTable.StartupThreshold)
			{
				MExDiagnostics.EventLog.LogEvent(EdgeExtensibilityEventLogConstants.Tuple_MExAgentFactoryStartupDelay, null, new object[]
				{
					timeSpan2,
					this.startupDiagnosticInfo
				});
			}
			this.factories = new AgentFactory[this.factoriesByAgentId.Count];
			this.factoriesByAgentId.Values.CopyTo(this.factories, 0);
		}

		// Token: 0x170000EC RID: 236
		public AgentFactory this[string agentId]
		{
			get
			{
				return this.factoriesByAgentId[agentId];
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x000131B6 File Offset: 0x000113B6
		internal string StartupDiagnosticInfo
		{
			get
			{
				return this.startupDiagnosticInfo;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00013240 File Offset: 0x00011440
		public static AgentManager GetAgentManagerInstance(AgentInfo agentInfo)
		{
			string text;
			Exception ex;
			return FactoryTable.LoadAssemblyAndCreateInstance<AgentManager>(agentInfo, delegate(Assembly assembly)
			{
				AgentManager agentManager = null;
				Type baseDeliveryAgentFactoryType = FactoryTable.GetBaseDeliveryAgentFactoryType(assembly.GetType(agentInfo.FactoryTypeName));
				if (baseDeliveryAgentFactoryType != null)
				{
					Type[] genericArguments = baseDeliveryAgentFactoryType.GetGenericArguments();
					if (genericArguments.Length == 1 && typeof(AgentManager).IsAssignableFrom(genericArguments[0]))
					{
						agentManager = (AgentManager)assembly.CreateInstance(genericArguments[0].FullName);
						agentManager.AgentName = agentInfo.AgentName;
					}
				}
				return agentManager;
			}, out text, out ex);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00013277 File Offset: 0x00011477
		public AgentManager GetAgentManager(string agentId)
		{
			return this.agentManagersByAgentId[agentId];
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00013288 File Offset: 0x00011488
		public void Shutdown()
		{
			for (int i = 0; i < this.factories.Length; i++)
			{
				this.factories[i].Close();
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000132B8 File Offset: 0x000114B8
		private static T LoadAssemblyAndCreateInstance<T>(AgentInfo agentInfo, FactoryTable.CreateInstance<T> createInstance, out string agentPath, out Exception exception)
		{
			agentPath = Path.Combine(Constants.MExRuntimeLocation, agentInfo.FactoryAssemblyPath);
			exception = null;
			T result = default(T);
			try
			{
				if (!File.Exists(agentPath))
				{
					exception = new ArgumentException(MExRuntimeStrings.InvalidAgentAssemblyPath);
				}
				else if (string.IsNullOrEmpty(agentInfo.FactoryTypeName))
				{
					exception = new ArgumentException(MExRuntimeStrings.InvalidAgentFactoryType);
				}
				else
				{
					Assembly assembly = Assembly.LoadFrom(agentPath);
					result = createInstance(assembly);
				}
			}
			catch (IOException ex)
			{
				exception = ex;
			}
			catch (BadImageFormatException ex2)
			{
				exception = ex2;
			}
			catch (SecurityException ex3)
			{
				exception = ex3;
			}
			catch (MissingMethodException ex4)
			{
				exception = ex4;
			}
			catch (TargetInvocationException ex5)
			{
				Exception innerException = ex5.InnerException;
				if (!FactoryTable.IsSafeToHandle(innerException))
				{
					throw;
				}
				exception = innerException;
			}
			catch (TypeInitializationException ex6)
			{
				Exception innerException2 = ex6.InnerException;
				if (!FactoryTable.IsSafeToHandle(innerException2))
				{
					throw;
				}
				exception = innerException2;
			}
			catch (InvalidCastException ex7)
			{
				exception = ex7;
			}
			return result;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00013400 File Offset: 0x00011600
		private static AgentFactory CreateAgentFactory(AgentInfo agentInfo)
		{
			string assembly2;
			Exception ex;
			AgentFactory agentFactory = FactoryTable.LoadAssemblyAndCreateInstance<AgentFactory>(agentInfo, (Assembly assembly) => (AgentFactory)assembly.CreateInstance(agentInfo.FactoryTypeName), out assembly2, out ex);
			if (agentFactory == null)
			{
				ExEventLog.EventTuple tuple = EdgeExtensibilityEventLogConstants.Tuple_MExAgentFactoryCreationFailure;
				if (ex is InvalidCastException)
				{
					tuple = EdgeExtensibilityEventLogConstants.Tuple_MExAgentVersionMismatch;
				}
				ExchangeConfigurationException ex2 = new ExchangeConfigurationException(MExRuntimeStrings.InvalidTypeInConfiguration(agentInfo.FactoryTypeName, assembly2, (ex == null) ? "type not found" : ex.Message), ex);
				MExDiagnostics.EventLog.LogEvent(tuple, null, new object[]
				{
					agentInfo.AgentName,
					ex2.Message
				});
				throw ex2;
			}
			return agentFactory;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000134B4 File Offset: 0x000116B4
		private static Type GetBaseDeliveryAgentFactoryType(Type factory)
		{
			Type type = null;
			if (factory != null)
			{
				type = factory.BaseType;
				while (type != null && (!type.IsGenericType || !(type.GetGenericTypeDefinition() == typeof(DeliveryAgentFactory<>))))
				{
					type = type.BaseType;
				}
			}
			return type;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00013505 File Offset: 0x00011705
		private static bool IsSafeToHandle(Exception e)
		{
			return e is ConfigurationErrorsException || e is LocalizedException;
		}

		// Token: 0x0400048B RID: 1163
		private static readonly TimeSpan StartupThreshold = TimeSpan.FromSeconds(15.0);

		// Token: 0x0400048C RID: 1164
		private readonly AgentFactory[] factories;

		// Token: 0x0400048D RID: 1165
		private readonly string startupDiagnosticInfo;

		// Token: 0x0400048E RID: 1166
		private readonly Dictionary<string, AgentFactory> factoriesByAgentId;

		// Token: 0x0400048F RID: 1167
		private readonly Dictionary<string, AgentManager> agentManagersByAgentId;

		// Token: 0x0200007C RID: 124
		// (Invoke) Token: 0x060003DF RID: 991
		private delegate T CreateInstance<T>(Assembly assembly);
	}
}
