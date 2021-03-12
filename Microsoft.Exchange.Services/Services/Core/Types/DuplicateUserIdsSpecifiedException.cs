using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200075B RID: 1883
	internal sealed class DuplicateUserIdsSpecifiedException : ServicePermanentExceptionWithXmlNodeArray
	{
		// Token: 0x06003847 RID: 14407 RVA: 0x000C72BC File Offset: 0x000C54BC
		public DuplicateUserIdsSpecifiedException(List<List<BasePermissionType>> duplicatePermissionsLists) : base((CoreResources.IDs)4289255106U)
		{
			this.Initialize(duplicatePermissionsLists);
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x000C72D5 File Offset: 0x000C54D5
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x000C72DC File Offset: 0x000C54DC
		public DuplicateUserIdsSpecifiedException(List<List<XmlElement>> duplicatePermissionsLists) : base((CoreResources.IDs)4289255106U)
		{
			this.Initialize(duplicatePermissionsLists);
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x000C72F8 File Offset: 0x000C54F8
		private void Initialize(List<List<BasePermissionType>> duplicatePermissionsLists)
		{
			List<List<XmlElement>> list = new List<List<XmlElement>>();
			foreach (List<BasePermissionType> list2 in duplicatePermissionsLists)
			{
				List<XmlElement> list3 = new List<XmlElement>();
				foreach (BasePermissionType serializationObject in list2)
				{
					list3.Add(base.SerializeObjectToXml(serializationObject, DuplicateUserIdsSpecifiedException.basePermissionTypeSerializer));
				}
				list.Add(list3);
			}
			this.Initialize(list);
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x000C73A4 File Offset: 0x000C55A4
		private void Initialize(List<List<XmlElement>> duplicatePermissionsLists)
		{
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			XmlElement parentElement = ServiceXml.CreateElement(safeXmlDocument, "MessageXml", "http://schemas.microsoft.com/exchange/services/2006/types");
			foreach (List<XmlElement> list in duplicatePermissionsLists)
			{
				XmlElement xmlElement = ServiceXml.CreateElement(parentElement, "DuplicateUserIds", "http://schemas.microsoft.com/exchange/services/2006/types");
				foreach (XmlElement node in list)
				{
					xmlElement.AppendChild(safeXmlDocument.ImportNode(node, true));
				}
				base.NodeArray.Nodes.Add(xmlElement);
			}
		}

		// Token: 0x04001F3F RID: 7999
		private static SafeXmlSerializer basePermissionTypeSerializer = new SafeXmlSerializer(typeof(BasePermissionType));
	}
}
