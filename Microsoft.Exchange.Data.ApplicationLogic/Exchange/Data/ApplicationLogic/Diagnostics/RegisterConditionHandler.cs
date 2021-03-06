using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000DE RID: 222
	internal sealed class RegisterConditionHandler : ExchangeDiagnosableWrapper<RegisterConditionResult>
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x000250BF File Offset: 0x000232BF
		protected override string UsageText
		{
			get
			{
				return "This diagnostics handler registers a “diagnostics job” with a unique identifier. Below are examples for using this diagnostics handler: ";
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x000250C8 File Offset: 0x000232C8
		protected override string UsageSample
		{
			get
			{
				string text = BaseConditionalRegistration.GetConfigurationValue("UserPropertyGroup") ?? "SmtpAddress,DisplayName,TenantName,WindowsLiveId,MailboxServer,MailboxDatabase,MailboxServerVersion,IsMonitoringUser";
				string text2 = BaseConditionalRegistration.GetConfigurationValue("PolicyPropertyGroup") ?? "ThrottlingPolicyName,MaxConcurrency,MaxBurst,RechargeRate,CutoffBalance,ThrottlingPolicyScope,ConcurrencyStart,ConcurrencyEnd";
				string text3 = BaseConditionalRegistration.GetConfigurationValue("WlmPropertyGroup") ?? "IsOverBudgetAtStart,IsOverBudgetAtEnd,BudgetBalanceStart,BudgetBalanceEnd,BudgetDelay,BudgetUsed,BudgetLockedOut,BudgetLockedUntil";
				return string.Format("Example 1: Registers a new diagnostics job with EDI framework\r\n  Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component RegisterCondition -Argument \"select DisplayName,XmlRequest where DisplayName -like 'John*' options TimeToLive='01:00:25',MaxHits='20',Description='Testing to see if this is happening'\"\r\n\r\n  You will find a Cookie node in output which can be used to check the current status of this job & retrieve the collected data.\r\n\r\n  <Cookie>0d341cfc-8577-4677-aa73-187b9ba6cc5c</Cookie>\r\n\r\nExample 2: Property groups can be provided in \"select\" clause in Arguments parameter. A property group is basically a name given to a set of predefined properties. So we don't have to type the list of properties individually. Below is the list of predefined prperty groups & associated properties.\r\n\r\n  '[User]' = {0}\r\n  '[Wlm]' = {1}\r\n  '[Policy]' = {2}\r\n\r\n  Usage:\r\n  Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component RegisterCondition -Argument \"select [Policy],[Wlm],[User],DisplayName where DisplayName -like 'John*' options TimeToLive='01:00:25',MaxHits='20',Description='Testing to see if this is happening'\"\r\n\r\n  You will find a Cookie node in output which can be used to check the current status of this job & retrieve the collected data.\r\n  <Cookie>0d341cfc-8577-4677-aa73-187b9ba6cc5c</Cookie>\r\n\r\nCurrently Supported Fetch Properties and [Types]:\r\n{3}\r\n\r\nCurrently Supported Query Properties and [Types]:\r\n{4}", new object[]
				{
					text,
					text3,
					text2,
					RegisterConditionHandler.GetSchemaPropertiesString(BaseConditionalRegistration.FetchSchema),
					RegisterConditionHandler.GetSchemaPropertiesString(BaseConditionalRegistration.QuerySchema)
				});
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002514C File Offset: 0x0002334C
		private static string GetSchemaPropertiesString(ObjectSchema schema)
		{
			if (schema != null)
			{
				bool flag = true;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (PropertyDefinition propertyDefinition in schema.AllProperties)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.AppendFormat("{0} [{1}]", propertyDefinition.Name, propertyDefinition.Type.Name);
				}
				return stringBuilder.ToString();
			}
			return "<NOT INITIALIZED>";
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000251E0 File Offset: 0x000233E0
		public static RegisterConditionHandler GetInstance()
		{
			if (RegisterConditionHandler.instance == null)
			{
				lock (RegisterConditionHandler.lockObject)
				{
					if (RegisterConditionHandler.instance == null)
					{
						RegisterConditionHandler.instance = new RegisterConditionHandler();
					}
				}
			}
			return RegisterConditionHandler.instance;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00025238 File Offset: 0x00023438
		private RegisterConditionHandler()
		{
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00025240 File Offset: 0x00023440
		protected override string ComponentName
		{
			get
			{
				return "RegisterCondition";
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00025247 File Offset: 0x00023447
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x0002524F File Offset: 0x0002344F
		internal XElement Data { get; set; }

		// Token: 0x06000963 RID: 2403 RVA: 0x00025258 File Offset: 0x00023458
		internal void HydratePersistentHandlers()
		{
			if (this.Data == null || this.hydrated)
			{
				return;
			}
			foreach (XNode xnode in this.Data.DescendantNodes())
			{
				XElement xelement = xnode as XElement;
				if (xelement != null)
				{
					try
					{
						PersistentConditionalRegistration persistentRegistration = PersistentConditionalRegistration.CreateFromXml(xelement);
						ConditionalRegistrationCache.Singleton.Register(persistentRegistration);
					}
					catch (Exception exception)
					{
						ConditionalRegistrationLog.LogFailedHydration(xelement, exception);
					}
				}
			}
			this.hydrated = true;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x000252F4 File Offset: 0x000234F4
		internal void HydrateNonPersistentRegistrations()
		{
			string conditionalRegistrationsDirectory = ConditionalRegistrationLog.GetConditionalRegistrationsDirectory();
			if (!string.IsNullOrEmpty(conditionalRegistrationsDirectory))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(conditionalRegistrationsDirectory);
				FileInfo[] files = directoryInfo.GetFiles("*.xml");
				foreach (FileInfo fileInfo in files)
				{
					try
					{
						ConditionalRegistration conditionalRegistration;
						using (StreamReader streamReader = fileInfo.OpenText())
						{
							conditionalRegistration = ConditionalRegistration.DeserializeFromStreamReader(streamReader);
						}
						if (conditionalRegistration != null)
						{
							ConditionalRegistrationCache.Singleton.Register(conditionalRegistration);
						}
						else
						{
							File.Delete(fileInfo.FullName);
						}
					}
					catch (Exception exception)
					{
						ConditionalRegistrationLog.LogFailedHydration(new XElement(string.Format("Failed to hydrate registration with cookie:{0}", fileInfo.Name)), exception);
					}
				}
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x000253C0 File Offset: 0x000235C0
		internal void ResetHydratedForTest()
		{
			this.hydrated = false;
			this.Data = null;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x000253D0 File Offset: 0x000235D0
		internal override RegisterConditionResult GetExchangeDiagnosticsInfoData(DiagnosableParameters argument)
		{
			ConditionalRegistration registration = ConditionalRegistration.CreateFromArgument(argument);
			ConditionalRegistrationCache.Singleton.Register(registration);
			return new RegisterConditionResult(registration);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x000253F5 File Offset: 0x000235F5
		protected override void InternalSetData(XElement data)
		{
			this.Data = data;
			if (BaseConditionalRegistration.Initialized)
			{
				this.HydratePersistentHandlers();
			}
		}

		// Token: 0x0400046A RID: 1130
		private static RegisterConditionHandler instance;

		// Token: 0x0400046B RID: 1131
		private static object lockObject = new object();

		// Token: 0x0400046C RID: 1132
		private bool hydrated;
	}
}
