using System;
using System.Configuration;
using System.Management.Automation;
using System.Reflection;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.PswsClient;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000037 RID: 55
	internal static class EOPRecipient
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000CE6C File Offset: 0x0000B06C
		public static string GetPsWsHostServerName()
		{
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
			string result = string.Empty;
			KeyValueConfigurationElement keyValueConfigurationElement = configuration.AppSettings.Settings[EOPRecipient.PsWsHostServerKey];
			if (keyValueConfigurationElement != null && !string.IsNullOrEmpty(keyValueConfigurationElement.Value))
			{
				result = keyValueConfigurationElement.Value;
			}
			return result;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000CEBD File Offset: 0x0000B0BD
		public static void SetProperty(PswsCmdlet cmdlet, string propertyName, string propertyValue)
		{
			ArgumentValidator.ThrowIfNull("cmdlet", cmdlet);
			if (!string.IsNullOrEmpty(propertyValue))
			{
				cmdlet.Parameters[propertyName] = propertyValue;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000CEDF File Offset: 0x0000B0DF
		public static void SetProperty(PswsCmdlet cmdlet, string propertyName, string[] propertyValue)
		{
			ArgumentValidator.ThrowIfNull("cmdlet", cmdlet);
			if (propertyValue != null)
			{
				cmdlet.Parameters[propertyName] = propertyValue;
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		public static void SetProperty(PswsCmdlet cmdlet, string propertyName, ProxyAddress propertyValue)
		{
			ArgumentValidator.ThrowIfNull("cmdlet", cmdlet);
			if (propertyValue != null)
			{
				cmdlet.Parameters[propertyName] = propertyValue.ProxyAddressString;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000CF24 File Offset: 0x0000B124
		public static void SetProperty(PswsCmdlet cmdlet, string propertyName, WindowsLiveId propertyValue)
		{
			if (propertyValue != null)
			{
				cmdlet.Parameters[propertyName] = propertyValue.ToString();
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000CF3B File Offset: 0x0000B13B
		public static void SetProperty(PswsCmdlet cmdlet, string propertyName, OrganizationIdParameter propertyValue)
		{
			if (propertyValue != null)
			{
				cmdlet.Parameters[propertyName] = propertyValue.ToString();
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000CF52 File Offset: 0x0000B152
		public static void SetProperty(PswsCmdlet cmdlet, string propertyName, SmtpAddress propertyValue)
		{
			if (propertyValue.Length > 0)
			{
				cmdlet.Parameters[propertyName] = propertyValue.ToString();
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000CF77 File Offset: 0x0000B177
		public static void SetProperty(PswsCmdlet cmdlet, string propertyName, SecureString propertyValue)
		{
			ArgumentValidator.ThrowIfNull("cmdlet", cmdlet);
			if (propertyValue != null)
			{
				cmdlet.Parameters[propertyName] = propertyValue;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000CF94 File Offset: 0x0000B194
		public static void SetProperty(PswsCmdlet cmdlet, string propertyName, CountryInfo propertyValue)
		{
			ArgumentValidator.ThrowIfNull("cmdlet", cmdlet);
			if (propertyValue != null)
			{
				cmdlet.Parameters[propertyName] = propertyValue;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000CFB7 File Offset: 0x0000B1B7
		public static void SetProperty(PswsCmdlet cmdlet, string propertyName, ProxyAddressCollection propertyValue)
		{
			ArgumentValidator.ThrowIfNull("cmdlet", cmdlet);
			if (propertyValue != null)
			{
				cmdlet.Parameters[propertyName] = propertyValue.ToStringArray();
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000CFDC File Offset: 0x0000B1DC
		public static void CheckForError(Task task, PswsCmdlet cmdlet)
		{
			ArgumentValidator.ThrowIfNull("task", task);
			ArgumentValidator.ThrowIfNull("cmdlet", cmdlet);
			if (!string.IsNullOrEmpty(cmdlet.Error))
			{
				string errMsg = cmdlet.Error.ToString();
				EOPRecipient.PublishErrorEvent(errMsg);
				string text = "<NULL>";
				Authenticator authenticator = cmdlet.Authenticator as Authenticator;
				if (authenticator != null)
				{
					text = text.ToString();
				}
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_FfoReportingRecipientTaskFailure, new string[]
				{
					cmdlet.Organization ?? "<NULL>",
					cmdlet.HostServerName ?? "<NULL>",
					cmdlet.ToString() ?? "<NULL>",
					cmdlet.AdditionalHeaders.ToString() ?? "<NULL>",
					(cmdlet.RequestTimeout != null) ? cmdlet.RequestTimeout.Value.ToString() : "<NULL>",
					cmdlet.Exception.ToString() ?? "<NULL>"
				});
				task.WriteError(cmdlet.Exception, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		private static void PublishErrorEvent(string errMsg)
		{
			if (!errMsg.Contains("ProtocolError"))
			{
				EventNotificationItem.Publish(ExchangeComponent.FfoUmc.Name, EOPRecipient.PswsFailureMonitor, null, errMsg, ResultSeverityLevel.Error, false);
			}
		}

		// Token: 0x0400005A RID: 90
		private static string PsWsHostServerKey = "PsWsHostServerName";

		// Token: 0x0400005B RID: 91
		internal static readonly string PswsFailureMonitor = "FFO-UMC-EOPRecipient-PSWS-Failure";
	}
}
