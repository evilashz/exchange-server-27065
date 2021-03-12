using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200024D RID: 589
	internal class SettingsCommand : Command
	{
		// Token: 0x0600157D RID: 5501 RVA: 0x0007EB13 File Offset: 0x0007CD13
		public SettingsCommand()
		{
			base.PerfCounter = AirSyncCounters.NumberOfSettingsRequests;
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x0007EB31 File Offset: 0x0007CD31
		internal override int MinVersion
		{
			get
			{
				return 120;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x0007EB35 File Offset: 0x0007CD35
		protected override string RootNodeName
		{
			get
			{
				return "Settings";
			}
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0007EB3C File Offset: 0x0007CD3C
		internal override Command.ExecutionState ExecuteCommand()
		{
			Command.ExecutionState result;
			using (base.Context.Tracker.Start(TimeId.SettingsExecuteCommand))
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Settings command received. Processing request...");
				this.InitializeResponseXmlDocument();
				this.ReadXmlRequest();
				foreach (SettingsBase settingsBase in this.properties)
				{
					settingsBase.Execute();
				}
				this.FinalizeResponseXmlDocument();
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Settings command finished processing.");
				result = Command.ExecutionState.Complete;
			}
			return result;
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0007EBF0 File Offset: 0x0007CDF0
		protected override bool HandleQuarantinedState()
		{
			return true;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0007EBF4 File Offset: 0x0007CDF4
		internal override XmlDocument GetValidationErrorXml()
		{
			XmlDocument result;
			using (base.Context.Tracker.Start(TimeId.SettingsGetValidationErrorXml))
			{
				if (SettingsCommand.validationErrorXml == null)
				{
					XmlDocument commandXmlStub = base.GetCommandXmlStub();
					XmlElement xmlElement = commandXmlStub.CreateElement("Status", this.RootNodeNamespace);
					xmlElement.InnerText = XmlConvert.ToString(2);
					commandXmlStub[this.RootNodeName].AppendChild(xmlElement);
					SettingsCommand.validationErrorXml = commandXmlStub;
				}
				result = SettingsCommand.validationErrorXml;
			}
			return result;
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0007EC7C File Offset: 0x0007CE7C
		private void ReadXmlRequest()
		{
			using (base.Context.Tracker.Start(TimeId.SettingsReadXmlRequest))
			{
				XmlNode xmlRequest = base.XmlRequest;
				foreach (object obj in xmlRequest.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlNode response = base.XmlResponse.CreateElement(xmlNode.LocalName, "Settings:");
					string localName;
					switch (localName = xmlNode.LocalName)
					{
					case "Oof":
					{
						SettingsBase item = new OofSetting(xmlNode, response, base.MailboxSession, base.CurrentAccessState, base.ProtocolLogger);
						this.properties.Add(item);
						break;
					}
					case "DevicePassword":
					{
						SettingsBase item = new DevicePasswordSetting(xmlNode, response, base.User, base.GlobalInfo, base.ProtocolLogger);
						this.properties.Add(item);
						break;
					}
					case "DeviceInformation":
					{
						SettingsBase item = new DeviceInformationSetting(xmlNode, response, this, base.ProtocolLogger);
						this.properties.Add(item);
						break;
					}
					case "UserInformation":
					{
						SettingsBase item = new UserInformationSetting(xmlNode, response, base.User, base.MailboxSession, base.Version, base.ProtocolLogger);
						this.properties.Add(item);
						break;
					}
					case "RightsManagementInformation":
					{
						SettingsBase item = new RightsManagementInformationSetting(xmlNode, response, base.User, base.MailboxSession.PreferedCulture, base.ProtocolLogger, base.MailboxLogger);
						this.properties.Add(item);
						break;
					}
					case "TimeZoneOffsets":
					{
						SettingsBase item = new TimeZoneOffsetSettings(xmlNode, response, base.User, base.ProtocolLogger);
						this.properties.Add(item);
						break;
					}
					}
				}
			}
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0007EEE0 File Offset: 0x0007D0E0
		private void InitializeResponseXmlDocument()
		{
			using (base.Context.Tracker.Start(TimeId.SettingsInitializeResponseXmlDocument))
			{
				base.XmlResponse = new SafeXmlDocument();
				this.settingsNode = base.XmlResponse.CreateElement("Settings", "Settings:");
				base.XmlResponse.AppendChild(this.settingsNode);
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0007EF54 File Offset: 0x0007D154
		private void FinalizeResponseXmlDocument()
		{
			using (base.Context.Tracker.Start(TimeId.SettingsFinalizeResponseXmlDocument))
			{
				XmlElement xmlElement = base.XmlResponse.CreateElement("Status", "Settings:");
				xmlElement.InnerText = "1";
				this.settingsNode.AppendChild(xmlElement);
				foreach (SettingsBase settingsBase in this.properties)
				{
					XmlNode response = settingsBase.Response;
					this.settingsNode.AppendChild(response);
				}
			}
		}

		// Token: 0x04000CA8 RID: 3240
		private static XmlDocument validationErrorXml;

		// Token: 0x04000CA9 RID: 3241
		private List<SettingsBase> properties = new List<SettingsBase>();

		// Token: 0x04000CAA RID: 3242
		private XmlNode settingsNode;
	}
}
