using System;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007D8 RID: 2008
	internal sealed class InvalidUserInfoException : ServicePermanentExceptionWithXmlNodeArray
	{
		// Token: 0x06003B1D RID: 15133 RVA: 0x000CFC36 File Offset: 0x000CDE36
		public InvalidUserInfoException(BasePermissionType permissionElement) : base(CoreResources.IDs.ErrorInvalidUserInfo)
		{
			this.SerializePermissionElement(permissionElement);
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x000CFC4F File Offset: 0x000CDE4F
		public InvalidUserInfoException(BasePermissionType permissionElement, Exception innerException) : base(CoreResources.IDs.ErrorInvalidUserInfo, innerException)
		{
			this.SerializePermissionElement(permissionElement);
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000CFC69 File Offset: 0x000CDE69
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x000CFC70 File Offset: 0x000CDE70
		public InvalidUserInfoException(XmlElement permissionElement) : base(CoreResources.IDs.ErrorInvalidUserInfo)
		{
			this.SetupXmlNodeArray(permissionElement);
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x000CFC89 File Offset: 0x000CDE89
		public InvalidUserInfoException(XmlElement permissionElement, Exception innerException) : base(CoreResources.IDs.ErrorInvalidUserInfo, innerException)
		{
			this.SetupXmlNodeArray(permissionElement);
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x000CFCA3 File Offset: 0x000CDEA3
		private void SerializePermissionElement(BasePermissionType permissionElement)
		{
			this.SetupXmlNodeArray(base.SerializeObjectToXml(permissionElement, InvalidUserInfoException.basePermissionTypeSerializer));
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x000CFCB8 File Offset: 0x000CDEB8
		private void SetupXmlNodeArray(XmlElement invalidPermission)
		{
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			XmlElement parentElement = ServiceXml.CreateElement(safeXmlDocument, "MessageXml", "http://schemas.microsoft.com/exchange/services/2006/types");
			XmlElement xmlElement = ServiceXml.CreateElement(parentElement, "InvalidPermission", "http://schemas.microsoft.com/exchange/services/2006/types");
			xmlElement.AppendChild(safeXmlDocument.ImportNode(invalidPermission, true));
			base.NodeArray.Nodes.Add(xmlElement);
		}

		// Token: 0x040020B0 RID: 8368
		private static readonly SafeXmlSerializer basePermissionTypeSerializer = new SafeXmlSerializer(typeof(BasePermissionType));
	}
}
