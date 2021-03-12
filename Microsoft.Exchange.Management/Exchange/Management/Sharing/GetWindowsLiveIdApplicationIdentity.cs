using System;
using System.Management.Automation;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.AppIdServices;
using Microsoft.Exchange.Management.LiveServices;
using Microsoft.Exchange.Management.LiveServicesHelper;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Sharing
{
	// Token: 0x02000D88 RID: 3464
	[Cmdlet("Get", "WindowsLiveIdApplicationIdentity", DefaultParameterSetName = "AppID")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class GetWindowsLiveIdApplicationIdentity : WindowsLiveIdTask
	{
		// Token: 0x1700295A RID: 10586
		// (get) Token: 0x0600850A RID: 34058 RVA: 0x00220478 File Offset: 0x0021E678
		// (set) Token: 0x0600850B RID: 34059 RVA: 0x0022048F File Offset: 0x0021E68F
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "Uri", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public string Uri
		{
			get
			{
				return (string)base.Fields["Uri"];
			}
			set
			{
				base.Fields["Uri"] = value;
			}
		}

		// Token: 0x1700295B RID: 10587
		// (get) Token: 0x0600850C RID: 34060 RVA: 0x002204A2 File Offset: 0x0021E6A2
		// (set) Token: 0x0600850D RID: 34061 RVA: 0x002204B9 File Offset: 0x0021E6B9
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "AppID", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public string AppId
		{
			get
			{
				return (string)base.Fields["AppID"];
			}
			set
			{
				base.Fields["AppID"] = value;
			}
		}

		// Token: 0x0600850E RID: 34062 RVA: 0x002204CC File Offset: 0x0021E6CC
		protected override ConfigurableObject ParseResponse(LiveIdInstanceType liveIdInstanceType, XmlDocument xmlResponse)
		{
			return WindowsLiveIdApplicationIdentity.ParseXml(liveIdInstanceType, xmlResponse);
		}

		// Token: 0x0600850F RID: 34063 RVA: 0x002204D8 File Offset: 0x0021E6D8
		protected override XmlDocument WindowsLiveIdMethod(LiveIdInstanceType liveIdInstanceType)
		{
			XmlDocument xmlDocument = null;
			using (AppIDServiceAPISoapServer appIDServiceAPISoapServer = LiveServicesHelper.ConnectToAppIDService(liveIdInstanceType))
			{
				base.WriteVerbose(Strings.AppIDServiceUrl(appIDServiceAPISoapServer.Url.ToString()));
				if (!string.IsNullOrEmpty(this.Uri))
				{
					new Uri(this.Uri, UriKind.RelativeOrAbsolute);
					string text = string.Format(GetWindowsLiveIdApplicationIdentity.AppIDFindTemplate, this.Uri);
					string xml = appIDServiceAPISoapServer.FindApplication(text);
					XmlDocument xmlDocument2 = new SafeXmlDocument();
					xmlDocument2.LoadXml(xml);
					XmlNode xmlNode = xmlDocument2.SelectSingleNode("AppidData/Application/ID");
					if (xmlNode == null)
					{
						base.WriteVerbose(Strings.AppIdElementIsEmpty);
						throw new LiveServicesException(Strings.AppIdElementIsEmpty);
					}
					base.WriteVerbose(Strings.FoundAppId(xmlNode.InnerText));
					this.AppId = xmlNode.InnerText;
				}
				if (!string.IsNullOrEmpty(this.AppId))
				{
					xmlDocument = new SafeXmlDocument();
					xmlDocument.LoadXml(appIDServiceAPISoapServer.GetApplicationEntity(new tagPASSID(), this.AppId));
				}
			}
			return xmlDocument;
		}

		// Token: 0x04004049 RID: 16457
		private const string UriParameterSet = "Uri";

		// Token: 0x0400404A RID: 16458
		private const string AppIDParameterSet = "AppID";

		// Token: 0x0400404B RID: 16459
		private static readonly string AppIDFindTemplate = "<SearchCriteria><Object name=\"Application\"><Property name=\"URI\">{0}</Property></Object></SearchCriteria>";
	}
}
