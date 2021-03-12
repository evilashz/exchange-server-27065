using System;
using System.Management.Automation;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.LiveServices;
using Microsoft.Exchange.Management.NamespaceServices;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Sharing
{
	// Token: 0x02000D89 RID: 3465
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Get", "WindowsLiveIdNamespace", DefaultParameterSetName = "Namespace")]
	public sealed class GetWindowsLiveIdNamespace : WindowsLiveIdTask
	{
		// Token: 0x1700295C RID: 10588
		// (get) Token: 0x06008512 RID: 34066 RVA: 0x002205EC File Offset: 0x0021E7EC
		// (set) Token: 0x06008513 RID: 34067 RVA: 0x00220603 File Offset: 0x0021E803
		[Parameter(Mandatory = true, ParameterSetName = "Namespace", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNullOrEmpty]
		public string Namespace
		{
			get
			{
				return (string)base.Fields["Namespace"];
			}
			set
			{
				base.Fields["Namespace"] = value;
			}
		}

		// Token: 0x06008514 RID: 34068 RVA: 0x00220616 File Offset: 0x0021E816
		protected override ConfigurableObject ParseResponse(LiveIdInstanceType liveIdInstanceType, XmlDocument xmlResponse)
		{
			return WindowsLiveIdNamespace.ParseXml(liveIdInstanceType, xmlResponse);
		}

		// Token: 0x06008515 RID: 34069 RVA: 0x00220620 File Offset: 0x0021E820
		protected override XmlDocument WindowsLiveIdMethod(LiveIdInstanceType liveIdInstanceType)
		{
			XmlDocument xmlDocument = null;
			using (NamespaceServiceAPISoapServer namespaceServiceAPISoapServer = LiveServicesHelper.ConnectToNamespaceService(liveIdInstanceType))
			{
				base.WriteVerbose(Strings.NamespaceServiceUrl(namespaceServiceAPISoapServer.Url.ToString()));
				string namespaceAttributes = namespaceServiceAPISoapServer.GetNamespaceAttributes(this.Namespace, string.Empty);
				xmlDocument = new SafeXmlDocument();
				xmlDocument.LoadXml(namespaceAttributes);
			}
			return xmlDocument;
		}

		// Token: 0x0400404C RID: 16460
		private const string NamespaceParameterSet = "Namespace";
	}
}
