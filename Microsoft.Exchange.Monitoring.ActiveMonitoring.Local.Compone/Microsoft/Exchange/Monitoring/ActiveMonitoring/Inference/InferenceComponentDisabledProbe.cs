using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Inference
{
	// Token: 0x02000496 RID: 1174
	public class InferenceComponentDisabledProbe : ProbeWorkItem
	{
		// Token: 0x06001D9C RID: 7580 RVA: 0x000B239C File Offset: 0x000B059C
		public static ProbeDefinition CreateDefinition(string probeName, int recurrenceIntervalSeconds, bool enabled)
		{
			return new ProbeDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				TypeName = typeof(InferenceComponentDisabledProbe).FullName,
				Name = probeName,
				RecurrenceIntervalSeconds = recurrenceIntervalSeconds,
				TimeoutSeconds = recurrenceIntervalSeconds,
				MaxRetryAttempts = 3,
				TargetResource = string.Empty,
				ServiceName = ExchangeComponent.Inference.Name,
				Enabled = enabled
			};
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x000B2414 File Offset: 0x000B0614
		protected override void DoWork(CancellationToken cancellationToken)
		{
			string text = Path.Combine(ExchangeSetupContext.BinPath, "MSExchangeDelivery.exe.config");
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.IsClassificationPipelineEnabled(text))
			{
				stringBuilder.AppendLine(Strings.InferenceDisabledComponentDetails("Classification", text));
			}
			string[] array = new string[]
			{
				"Classification",
				"Data Collection",
				"Training"
			};
			string[] array2 = new string[]
			{
				"SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference",
				"SYSTEM\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters",
				"SYSTEM\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"
			};
			string[] array3 = new string[]
			{
				"ClassificationPipelineEnabled",
				"InferenceDataCollectionAssistantEnabledOverride",
				"InferenceTrainingAssistantEnabledOverride"
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (!this.IsRegistrySettingEnabled(array2[i], array3[i]))
				{
					stringBuilder.AppendLine(Strings.InferenceDisabledComponentDetails(array[i], array2[i]));
				}
			}
			if (stringBuilder.Length > 0)
			{
				throw new InferenceComponentDisabledException(stringBuilder.ToString());
			}
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x000B251C File Offset: 0x000B071C
		private bool IsRegistrySettingEnabled(string regKeyPath, string regKeyName)
		{
			bool result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(regKeyPath, false))
			{
				if (registryKey == null)
				{
					result = false;
				}
				else
				{
					object value = registryKey.GetValue(regKeyName, null);
					result = (value is int && (int)value == 1);
				}
			}
			return result;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x000B2578 File Offset: 0x000B0778
		private bool IsClassificationPipelineEnabled(string xmlFile)
		{
			if (!File.Exists(xmlFile))
			{
				return true;
			}
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(xmlFile);
			}
			catch (IOException)
			{
				return true;
			}
			XmlNode xmlNode = xmlDocument.SelectSingleNode(string.Format("/configuration/appSettings/add[@key='{0}']/@value", "InferenceClassificationAgentEnabledOverride"));
			return xmlNode == null || !xmlNode.Value.Equals("false", StringComparison.OrdinalIgnoreCase);
		}
	}
}
