using System;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport.Delivery;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x0200001E RID: 30
	[Cmdlet("Install", "TransportAgent", SupportsShouldProcess = true)]
	public class InstallTransportAgent : AgentBaseTask
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004845 File Offset: 0x00002A45
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return AgentStrings.ConfirmationMessageInstallTransportAgent(this.Name.ToString(), this.TransportAgentFactory.ToString(), this.AssemblyPath.ToString());
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004878 File Offset: 0x00002A78
		private Type ReflectAgentType(string assemblyPath, string transportAgentFactory)
		{
			Assembly assembly;
			try
			{
				assembly = Assembly.LoadFrom(assemblyPath);
			}
			catch (FileNotFoundException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
				return null;
			}
			catch (FileLoadException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
				return null;
			}
			catch (BadImageFormatException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, null);
				return null;
			}
			catch (ArgumentException exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidArgument, null);
				return null;
			}
			Type type = assembly.GetType(transportAgentFactory, false);
			if (null == type)
			{
				base.WriteError(new ArgumentException(AgentStrings.AgentFactoryTypeNotExist(transportAgentFactory), "TransportAgentFactory"), ErrorCategory.InvalidArgument, null);
				return null;
			}
			while (type != null)
			{
				if (type == typeof(SmtpReceiveAgentFactory))
				{
					return typeof(SmtpReceiveAgent);
				}
				if (type == typeof(RoutingAgentFactory))
				{
					return typeof(RoutingAgent);
				}
				if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(DeliveryAgentFactory<>))
				{
					return typeof(DeliveryAgent);
				}
				type = type.BaseType;
			}
			base.WriteError(new ArgumentException(AgentStrings.InvalidTransportAgentFactory(transportAgentFactory), "TransportAgentFactory"), ErrorCategory.InvalidArgument, null);
			return null;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000049E0 File Offset: 0x00002BE0
		protected override void InternalValidate()
		{
			if (this.Name.Length > 64)
			{
				base.WriteError(new ArgumentException(AgentStrings.AgentNameTooLargeArgument, "Name"), ErrorCategory.InvalidArgument, null);
			}
			if (!InstallTransportAgent.IsValidAgentName(this.Name))
			{
				base.WriteError(new ArgumentException(AgentStrings.AgentNameContainsInvalidCharacters, "Name"), ErrorCategory.InvalidArgument, null);
			}
			this.absoluteAssemblyPath = this.AssemblyPath;
			Server serverObject = base.GetServerObject();
			bool flag = serverObject != null && serverObject.IsEdgeServer;
			try
			{
				if (flag)
				{
					string currentPathProviderName = base.SessionState.CurrentPathProviderName;
					if (string.Compare(currentPathProviderName, "FileSystem", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.absoluteAssemblyPath = Path.Combine(base.SessionState.CurrentPath, this.AssemblyPath);
					}
				}
				else if (!Path.IsPathRooted(this.AssemblyPath))
				{
					base.WriteError(new ArgumentException(AgentStrings.AssemblyFilePathRelativeOnHub(this.AssemblyPath), "AssemblyPath"), ErrorCategory.InvalidArgument, null);
				}
			}
			catch (ArgumentException)
			{
				base.WriteError(new ArgumentException(AgentStrings.AssemblyFileNotExist(this.AssemblyPath), "AssemblyPath"), ErrorCategory.InvalidArgument, this.AssemblyPath);
			}
			if (new Uri(this.absoluteAssemblyPath).IsUnc)
			{
				base.WriteError(new ArgumentException(AgentStrings.AssemblyFilePathCanNotBeUNC(this.AssemblyPath), "AssemblyPath"), ErrorCategory.InvalidArgument, this.AssemblyPath);
			}
			if (!File.Exists(this.absoluteAssemblyPath))
			{
				base.WriteError(new ArgumentException(AgentStrings.AssemblyFileNotExist(this.AssemblyPath), "AssemblyPath"), ErrorCategory.InvalidArgument, null);
			}
			base.InternalValidate();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004B78 File Offset: 0x00002D78
		private static bool IsValidAgentName(string stringToValidate)
		{
			int length = stringToValidate.Length;
			bool result = true;
			for (int i = 0; i < length; i++)
			{
				char c = stringToValidate[i];
				if (!char.IsLetterOrDigit(c) && c != ' ' && c != '.' && c != '_' && c != '-')
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004BC4 File Offset: 0x00002DC4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			string text = base.ValidateAndNormalizeAgentIdentity(this.Name);
			if (base.AgentExists(text))
			{
				base.WriteError(new ArgumentException(AgentStrings.AgentAlreadyExist(this.Name), "Name"), ErrorCategory.InvalidArgument, null);
			}
			Type type = this.ReflectAgentType(this.absoluteAssemblyPath, this.TransportAgentFactory);
			AgentInfo agentInfo = new AgentInfo(text, type.ToString(), this.TransportAgentFactory, this.absoluteAssemblyPath, false, false);
			if (base.TransportService == TransportService.FrontEnd && type != typeof(SmtpReceiveAgent))
			{
				base.WriteError(new InvalidOperationException(AgentStrings.AgentTypeNotSupportedOnFrontEnd(type.ToString())), ErrorCategory.InvalidOperation, null);
			}
			if (type == typeof(DeliveryAgent))
			{
				this.ValidateDeliveryAgent(agentInfo);
			}
			TransportAgent sendToPipeline = new TransportAgent(text, false, base.MExConfiguration.GetPublicAgentList().Count + 1, this.TransportAgentFactory, this.absoluteAssemblyPath);
			int index = base.MExConfiguration.GetPublicAgentList().Count + base.MExConfiguration.GetPreExecutionInternalAgents().Count;
			base.MExConfiguration.AgentList.Insert(index, agentInfo);
			base.Save();
			base.WriteObject(sendToPipeline);
			if (base.MissingConfigFile)
			{
				this.WriteWarning(AgentStrings.MissingConfigurationFileCreate(base.MExConfigPath));
			}
			this.WriteWarning(AgentStrings.ReleaseAgentBinaryReference);
			this.WriteWarning(AgentStrings.RestartServiceForChanges(base.GetTransportServiceName()));
			TaskLogger.LogExit();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004D38 File Offset: 0x00002F38
		private void ValidateDeliveryAgent(AgentInfo agentInfo)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (AgentInfo agentInfo2 in base.MExConfiguration.AgentList)
			{
				DeliveryAgentManager deliveryAgentManager = FactoryTable.GetAgentManagerInstance(agentInfo2) as DeliveryAgentManager;
				if (deliveryAgentManager != null)
				{
					if (string.IsNullOrEmpty(deliveryAgentManager.SupportedDeliveryProtocol))
					{
						this.WriteWarning(AgentStrings.DeliveryProtocolNotValid(agentInfo2.AgentName));
					}
					else
					{
						hashSet.Add(deliveryAgentManager.SupportedDeliveryProtocol);
					}
				}
			}
			DeliveryAgentManager deliveryAgentManager2 = FactoryTable.GetAgentManagerInstance(agentInfo) as DeliveryAgentManager;
			if (deliveryAgentManager2 == null)
			{
				base.WriteError(new ArgumentException(AgentStrings.InvalidDeliveryAgentManager(this.Name)), ErrorCategory.InvalidArgument, null);
			}
			if (string.IsNullOrEmpty(deliveryAgentManager2.SupportedDeliveryProtocol))
			{
				base.WriteError(new ArgumentException(AgentStrings.DeliveryProtocolNotSpecified(this.Name)), ErrorCategory.InvalidArgument, null);
			}
			if (hashSet.Contains(deliveryAgentManager2.SupportedDeliveryProtocol))
			{
				base.WriteError(new ArgumentException(AgentStrings.MustHaveUniqueDeliveryProtocol(this.Name, deliveryAgentManager2.SupportedDeliveryProtocol)), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004E58 File Offset: 0x00003058
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00004E6F File Offset: 0x0000306F
		[Parameter(Mandatory = true, Position = 0)]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004E82 File Offset: 0x00003082
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00004E99 File Offset: 0x00003099
		[Parameter(Mandatory = true)]
		public string TransportAgentFactory
		{
			get
			{
				return (string)base.Fields["TransportAgentFactory"];
			}
			set
			{
				base.Fields["TransportAgentFactory"] = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004EAC File Offset: 0x000030AC
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00004EC3 File Offset: 0x000030C3
		[Parameter(Mandatory = true)]
		public string AssemblyPath
		{
			get
			{
				return (string)base.Fields["AssemblyPath"];
			}
			set
			{
				base.Fields["AssemblyPath"] = value;
			}
		}

		// Token: 0x0400003C RID: 60
		private string absoluteAssemblyPath;
	}
}
