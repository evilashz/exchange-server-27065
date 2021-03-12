using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Win32;

namespace Microsoft.Exchange.Provisioning
{
	// Token: 0x020001F9 RID: 505
	internal static class ProvisioningLayer
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00036509 File Offset: 0x00034709
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x00036501 File Offset: 0x00034701
		public static bool Disabled
		{
			get
			{
				return ProvisioningLayer.disabled || ProvisioningLayer.IsEdge() || !ProvisioningLayer.InDomain();
			}
			set
			{
				ProvisioningLayer.disabled = value;
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00036524 File Offset: 0x00034724
		public static void RefreshProvisioningBroker(Task task)
		{
			using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, "ProvisioningLayerLatency", "RefreshProvisioningBroker", LoggerHelper.CmdletPerfMonitors))
			{
				ExchangePropertyContainer.RefreshProvisioningBroker(task.SessionState);
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00036578 File Offset: 0x00034778
		public static ProvisioningHandler[] GetProvisioningHandlers(Task task)
		{
			ProvisioningHandler[] provisioningHandlersImpl;
			using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, "ProvisioningLayerLatency", "GetProvisioningHandlers", LoggerHelper.CmdletPerfMonitors))
			{
				provisioningHandlersImpl = ProvisioningLayer.GetProvisioningHandlersImpl(task);
			}
			return provisioningHandlersImpl;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000365CC File Offset: 0x000347CC
		private static ProvisioningHandler[] GetProvisioningHandlersImpl(Task task)
		{
			if (ProvisioningLayer.Disabled)
			{
				return null;
			}
			ProvisioningBroker provisioningBroker = ExchangePropertyContainer.GetProvisioningBroker(task.SessionState);
			if (provisioningBroker.InitializationException != null && !task.CurrentTaskContext.InvocationInfo.CommandName.StartsWith("Get-"))
			{
				string commandName;
				if ((commandName = task.CurrentTaskContext.InvocationInfo.CommandName) == null || (!(commandName == "Set-CmdletExtensionAgent") && !(commandName == "Remove-CmdletExtensionAgent") && !(commandName == "Disable-CmdletExtensionAgent")))
				{
					ProvisioningBrokerException ex = new ProvisioningBrokerException(Strings.ProvisioningBrokerInitializationFailed(provisioningBroker.InitializationException.Message), provisioningBroker.InitializationException);
					TaskLogger.LogError(ex);
					throw ex;
				}
				task.WriteWarning(provisioningBroker.InitializationException.Message);
			}
			return provisioningBroker.GetProvisioningHandlers(task);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00036690 File Offset: 0x00034890
		public static void SetLogMessageDelegate(Task task)
		{
			using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, "ProvisioningLayerLatency", "SetLogMessageDelegate", LoggerHelper.CmdletPerfMonitors))
			{
				ProvisioningLayer.SetLogMessageDelegateImpl(task);
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0003671C File Offset: 0x0003491C
		private static void SetLogMessageDelegateImpl(Task task)
		{
			if (ProvisioningLayer.Disabled || !task.IsProvisioningLayerAvailable)
			{
				return;
			}
			LogMessageDelegate logMessage = delegate(string message)
			{
				task.WriteVerbose(new LocalizedString(message));
			};
			WriteErrorDelegate writeError = delegate(LocalizedException ex, ExchangeErrorCategory category)
			{
				task.WriteError(ex, category, task.CurrentObjectIndex);
			};
			for (int i = 0; i < task.ProvisioningHandlers.Length; i++)
			{
				task.ProvisioningHandlers[i].LogMessage = logMessage;
				task.ProvisioningHandlers[i].WriteError = writeError;
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x000367A4 File Offset: 0x000349A4
		public static void SetUserScope(Task task)
		{
			using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, "ProvisioningLayerLatency", "SetUserScope", LoggerHelper.CmdletPerfMonitors))
			{
				ProvisioningLayer.SetUserScopeImpl(task);
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x000367F4 File Offset: 0x000349F4
		private static void SetUserScopeImpl(Task task)
		{
			if (ProvisioningLayer.Disabled || !task.IsProvisioningLayerAvailable)
			{
				return;
			}
			UserScopeFlags userScopeFlags = UserScopeFlags.None;
			if (task.ExchangeRunspaceConfig == null)
			{
				userScopeFlags |= UserScopeFlags.Local;
			}
			string userId = null;
			ADObjectId adobjectId;
			if (task.ExchangeRunspaceConfig != null)
			{
				userId = task.ExchangeRunspaceConfig.IdentityName;
			}
			else if (task.TryGetExecutingUserId(out adobjectId))
			{
				userId = adobjectId.ToString();
			}
			UserScope userScope = new UserScope(userId, task.ExecutingUserOrganizationId, task.CurrentOrganizationId, userScopeFlags, task.ScopeSet);
			for (int i = 0; i < task.ProvisioningHandlers.Length; i++)
			{
				task.ProvisioningHandlers[i].UserScope = userScope;
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00036888 File Offset: 0x00034A88
		public static void SetUserSpecifiedParameters(Task task, PropertyBag userSpecifiedParameters)
		{
			using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, "ProvisioningLayerLatency", "SetUserSpecifiedParameters", LoggerHelper.CmdletPerfMonitors))
			{
				if (!ProvisioningLayer.Disabled && task.IsProvisioningLayerAvailable)
				{
					for (int i = 0; i < task.ProvisioningHandlers.Length; i++)
					{
						task.ProvisioningHandlers[i].UserSpecifiedParameters = userSpecifiedParameters;
					}
				}
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00036904 File Offset: 0x00034B04
		public static void SetProvisioningCache(Task task, ProvisioningCache cache)
		{
			using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, "ProvisioningLayerLatency", "SetProvisioningCache", LoggerHelper.CmdletPerfMonitors))
			{
				if (!ProvisioningLayer.Disabled && task.IsProvisioningLayerAvailable)
				{
					for (int i = 0; i < task.ProvisioningHandlers.Length; i++)
					{
						task.ProvisioningHandlers[i].ProvisioningCache = cache;
					}
				}
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00036980 File Offset: 0x00034B80
		public static void ProvisionDefaultProperties(Task task, IConfigurable temporaryObject, IConfigurable dataObject, bool checkProvisioningLayerAvailability)
		{
			using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, "ProvisioningLayerLatency", "ProvisionDefaultProperties", LoggerHelper.CmdletPerfMonitors))
			{
				ProvisioningLayer.ProvisionDefaultPropertiesImpl(task, temporaryObject, dataObject, checkProvisioningLayerAvailability);
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000369D4 File Offset: 0x00034BD4
		private static void ProvisionDefaultPropertiesImpl(Task task, IConfigurable temporaryObject, IConfigurable dataObject, bool checkProvisioningLayerAvailability)
		{
			if (checkProvisioningLayerAvailability && (ProvisioningLayer.Disabled || !task.IsProvisioningLayerAvailable))
			{
				return;
			}
			ADObject adobject = dataObject as ADObject;
			HashSet<PropertyDefinition> hashSet = null;
			for (int i = 0; i < task.ProvisioningHandlers.Length; i++)
			{
				IConfigurable configurable = null;
				using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, task.ProvisioningHandlers[i].AgentName, "ProvisionDefaultProperties", LoggerHelper.CmdletPerfMonitors))
				{
					configurable = task.ProvisioningHandlers[i].ProvisionDefaultProperties(temporaryObject);
				}
				task.WriteVerbose(Strings.ProvisionDefaultProperties(i));
				if (configurable != null)
				{
					if (hashSet == null)
					{
						hashSet = ProvisioningLayer.LoadPrefilledProperties(dataObject as ADObject);
					}
					ADObject adobject2 = configurable as ADObject;
					if (adobject2 != null && adobject != null)
					{
						foreach (object obj in adobject2.propertyBag.Keys)
						{
							PropertyDefinition propertyDefinition = (PropertyDefinition)obj;
							ProviderPropertyDefinition providerPropertyDefinition = propertyDefinition as ProviderPropertyDefinition;
							if (providerPropertyDefinition != null && !hashSet.Contains(providerPropertyDefinition))
							{
								if (ProvisioningLayer.IsPropertyPrefilled(adobject.propertyBag, providerPropertyDefinition))
								{
									throw new ProvisioningException(Strings.PropertyIsAlreadyProvisioned(propertyDefinition.Name, i));
								}
								if (task.IsVerboseOn)
								{
									task.WriteVerbose(Strings.PropertyProvisioned(i, propertyDefinition.Name, (adobject2[propertyDefinition] ?? "<null>").ToString()));
								}
							}
						}
					}
					dataObject.CopyChangesFrom(configurable);
					temporaryObject.CopyChangesFrom(configurable);
				}
			}
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00036B70 File Offset: 0x00034D70
		private static bool IsPropertyPrefilled(PropertyBag propertyBag, ProviderPropertyDefinition property)
		{
			return propertyBag.Contains(property) && propertyBag[property] != property.DefaultValue;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00036B90 File Offset: 0x00034D90
		private static HashSet<PropertyDefinition> LoadPrefilledProperties(IConfigurable inObject)
		{
			ADObject adobject = inObject as ADObject;
			if (adobject == null)
			{
				return new HashSet<PropertyDefinition>();
			}
			return new HashSet<PropertyDefinition>(adobject.propertyBag.Keys.Cast<PropertyDefinition>());
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00036BC4 File Offset: 0x00034DC4
		public static bool UpdateAffectedIConfigurable(Task task, IConfigurable writeableIConfigurable, bool checkProvisioningLayerAvailability)
		{
			bool result;
			using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, "ProvisioningLayerLatency", "UpdateAffectedIConfigurable", LoggerHelper.CmdletPerfMonitors))
			{
				if (checkProvisioningLayerAvailability && (ProvisioningLayer.Disabled || !task.IsProvisioningLayerAvailable))
				{
					result = false;
				}
				else
				{
					bool flag = false;
					for (int i = 0; i < task.ProvisioningHandlers.Length; i++)
					{
						using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, task.ProvisioningHandlers[i].AgentName, "UpdateAffectedIConfigurable", LoggerHelper.CmdletPerfMonitors))
						{
							flag |= task.ProvisioningHandlers[i].UpdateAffectedIConfigurable(writeableIConfigurable);
						}
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00036C90 File Offset: 0x00034E90
		public static bool PreInternalProcessRecord(Task task, IConfigurable writeableIConfigurable, bool checkProvisioningLayerAvailability)
		{
			bool result;
			using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, "ProvisioningLayerLatency", "PreInternalProcessRecord", LoggerHelper.CmdletPerfMonitors))
			{
				if (checkProvisioningLayerAvailability && (ProvisioningLayer.Disabled || !task.IsProvisioningLayerAvailable))
				{
					result = false;
				}
				else
				{
					bool flag = false;
					for (int i = 0; i < task.ProvisioningHandlers.Length; i++)
					{
						using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, task.ProvisioningHandlers[i].AgentName, "PreInternalProcessRecord", LoggerHelper.CmdletPerfMonitors))
						{
							flag |= task.ProvisioningHandlers[i].PreInternalProcessRecord(writeableIConfigurable);
							task.WriteVerbose(Strings.ProvisioningPreInternalProcessRecord(i, flag));
						}
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00036D68 File Offset: 0x00034F68
		public static ProvisioningValidationError[] Validate(Task task, IConfigurable readOnlyIConfigurable)
		{
			ProvisioningValidationError[] result;
			using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, "ProvisioningLayerLatency", "Validate", LoggerHelper.CmdletPerfMonitors))
			{
				result = ProvisioningLayer.ValidateImpl(task, readOnlyIConfigurable);
			}
			return result;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00036DBC File Offset: 0x00034FBC
		public static ProvisioningValidationError[] ValidateImpl(Task task, IConfigurable readOnlyIConfigurable)
		{
			if (ProvisioningLayer.Disabled || !task.IsProvisioningLayerAvailable)
			{
				return null;
			}
			List<ProvisioningValidationError> list = new List<ProvisioningValidationError>();
			for (int i = 0; i < task.ProvisioningHandlers.Length; i++)
			{
				using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, task.ProvisioningHandlers[i].AgentName, "Validate", LoggerHelper.CmdletPerfMonitors))
				{
					ProvisioningValidationError[] array = task.ProvisioningHandlers[i].Validate(readOnlyIConfigurable);
					if (array != null && array.Length > 0)
					{
						for (int j = 0; j < array.Length; j++)
						{
							array[j].AgentName = task.ProvisioningHandlers[i].AgentName;
						}
						list.AddRange(array);
						if (task.IsVerboseOn)
						{
							task.WriteVerbose(TaskVerboseStringHelper.GetProvisioningValidationErrors(array, i));
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00036EA0 File Offset: 0x000350A0
		public static ProvisioningValidationError[] ValidateUserScope(Task task)
		{
			ProvisioningValidationError[] result;
			using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, "ProvisioningLayerLatency", "ValidateUserScope", LoggerHelper.CmdletPerfMonitors))
			{
				result = ProvisioningLayer.ValidateUserScopeImpl(task);
			}
			return result;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00036EF4 File Offset: 0x000350F4
		private static ProvisioningValidationError[] ValidateUserScopeImpl(Task task)
		{
			if (ProvisioningLayer.Disabled || !task.IsProvisioningLayerAvailable)
			{
				return null;
			}
			List<ProvisioningValidationError> list = new List<ProvisioningValidationError>();
			for (int i = 0; i < task.ProvisioningHandlers.Length; i++)
			{
				using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, task.ProvisioningHandlers[i].AgentName, "ValidateUserScope", LoggerHelper.CmdletPerfMonitors))
				{
					ProvisioningValidationError[] array = task.ProvisioningHandlers[i].ValidateUserScope();
					if (array != null && array.Length > 0)
					{
						for (int j = 0; j < array.Length; j++)
						{
							array[j].AgentName = task.ProvisioningHandlers[i].AgentName;
						}
						list.AddRange(array);
						if (task.IsVerboseOn)
						{
							task.WriteVerbose(TaskVerboseStringHelper.GetProvisioningValidationErrors(array, i));
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00036FD4 File Offset: 0x000351D4
		public static void OnComplete(Task task, bool succeeded, Exception exception)
		{
			using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, "ProvisioningLayerLatency", "OnComplete", LoggerHelper.CmdletPerfMonitors))
			{
				ProvisioningLayer.OnCompleteImpl(task, succeeded, exception);
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00037028 File Offset: 0x00035228
		private static void OnCompleteImpl(Task task, bool succeeded, Exception exception)
		{
			Dictionary<int, Exception> dictionary = null;
			if (ProvisioningLayer.Disabled || !task.IsProvisioningLayerAvailable)
			{
				return;
			}
			for (int i = 0; i < task.ProvisioningHandlers.Length; i++)
			{
				try
				{
					using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, task.ProvisioningHandlers[i].AgentName, "OnComplete", LoggerHelper.CmdletPerfMonitors))
					{
						task.ProvisioningHandlers[i].OnComplete(succeeded, exception);
					}
				}
				catch (Exception ex)
				{
					if (ProvisioningLayer.IsUnsafeException(ex))
					{
						throw;
					}
					if (dictionary == null)
					{
						dictionary = new Dictionary<int, Exception>();
					}
					dictionary.Add(i, ex);
				}
			}
			if (dictionary != null)
			{
				foreach (int num in dictionary.Keys)
				{
					task.WriteWarning(Strings.HandlerThronwExceptionInOnComplete(num, dictionary[num].ToString()));
				}
			}
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00037134 File Offset: 0x00035334
		public static void EndProcessing(Task task)
		{
			using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, "ProvisioningLayerLatency", "EndProcessiong", LoggerHelper.CmdletPerfMonitors))
			{
				ProvisioningLayer.EndProcessingImpl(task);
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00037184 File Offset: 0x00035384
		private static void EndProcessingImpl(Task task)
		{
			Dictionary<int, Exception> dictionary = null;
			if (ProvisioningLayer.Disabled || !task.IsProvisioningLayerAvailable)
			{
				return;
			}
			for (int i = 0; i < task.ProvisioningHandlers.Length; i++)
			{
				using (new CmdletMonitoredScope(task.CurrentTaskContext.UniqueId, task.ProvisioningHandlers[i].AgentName, "Dispose", LoggerHelper.CmdletPerfMonitors))
				{
					try
					{
						IDisposable disposable = task.ProvisioningHandlers[i] as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					catch (Exception ex)
					{
						if (ProvisioningLayer.IsUnsafeException(ex))
						{
							throw;
						}
						if (dictionary == null)
						{
							dictionary = new Dictionary<int, Exception>();
						}
						dictionary.Add(i, ex);
					}
				}
			}
			if (dictionary != null)
			{
				foreach (int key in dictionary.Keys)
				{
					task.WriteWarning(dictionary[key].ToString());
				}
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00037294 File Offset: 0x00035494
		private static bool IsEdge()
		{
			if (ADSession.IsBoundToAdam)
			{
				return true;
			}
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole";
			string text = (string)Registry.GetValue(keyName, "ConfiguredVersion", null);
			return text != null;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x000372CC File Offset: 0x000354CC
		private static bool InDomain()
		{
			bool result;
			try
			{
				NativeHelpers.GetDomainName();
				result = true;
			}
			catch (CannotGetDomainInfoException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000372FC File Offset: 0x000354FC
		private static bool IsUnsafeException(Exception e)
		{
			return e is StackOverflowException || e is OutOfMemoryException || e is ThreadAbortException;
		}

		// Token: 0x04000427 RID: 1063
		private static bool disabled;
	}
}
