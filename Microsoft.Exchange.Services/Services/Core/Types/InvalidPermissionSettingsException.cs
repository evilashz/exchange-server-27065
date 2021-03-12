using System;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007C0 RID: 1984
	internal sealed class InvalidPermissionSettingsException<PermissionType> : ServicePermanentExceptionWithXmlNodeArray where PermissionType : BasePermissionType
	{
		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06003AD8 RID: 15064 RVA: 0x000CF836 File Offset: 0x000CDA36
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x000CF83D File Offset: 0x000CDA3D
		public InvalidPermissionSettingsException(PermissionInformationBase<PermissionType> permissionInformation) : base(CoreResources.IDs.ErrorInvalidPermissionSettings)
		{
			this.AddXmlElement(base.SerializeObjectToXml(permissionInformation, InvalidPermissionSettingsException<PermissionType>.permissionInformationBaseSerializer));
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x000CF864 File Offset: 0x000CDA64
		private void AddXmlElement(XmlElement permissionInformation)
		{
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			XmlElement parentElement = ServiceXml.CreateElement(safeXmlDocument, "MessageXml", "http://schemas.microsoft.com/exchange/services/2006/types");
			XmlElement xmlElement = ServiceXml.CreateElement(parentElement, "InvalidPermission", "http://schemas.microsoft.com/exchange/services/2006/types");
			xmlElement.AppendChild(safeXmlDocument.ImportNode(permissionInformation, true));
			base.NodeArray.Nodes.Add(xmlElement);
		}

		// Token: 0x040020AF RID: 8367
		private static readonly SafeXmlSerializer permissionInformationBaseSerializer = new SafeXmlSerializer(typeof(PermissionInformationBase<PermissionType>));
	}
}
