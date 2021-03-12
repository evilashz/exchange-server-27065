using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000F0 RID: 240
	public class ObjectSchemaLoader
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x0001DCF4 File Offset: 0x0001BEF4
		static ObjectSchemaLoader()
		{
			ObjectSchemaLoader.unHandledTypeList["System.Net.IPAddress"] = typeof(IPAddress);
			ObjectSchemaLoader.unHandledTypeList["Microsoft.Exchange.Data.Common.LocalizedString, Microsoft.Exchange.Data.Common"] = typeof(LocalizedString);
			ObjectSchemaLoader.unHandledTypeList["Microsoft.Exchange.Data.Common.LocalizedString"] = typeof(LocalizedString);
			ExpressionParser.EnrolPredefinedTypes(typeof(CertificateStatus));
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001DD68 File Offset: 0x0001BF68
		public ObjectSchemaLoader(DataDrivenCategory dataDrivenCategory, string xPath, string schema)
		{
			Stream manifestResource = WinformsHelper.GetManifestResource(dataDrivenCategory);
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.Load(manifestResource);
			foreach (object obj in safeXmlDocument.SelectNodes(xPath))
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNode namedItem = xmlNode.Attributes.GetNamedItem("Name");
				if (namedItem.Value.Equals(schema))
				{
					this.objectDefinition = xmlNode;
					break;
				}
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0001DE04 File Offset: 0x0001C004
		protected XmlNode ObjectDefinition
		{
			get
			{
				return this.objectDefinition;
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001DE0C File Offset: 0x0001C00C
		public static Type GetTypeByString(string name)
		{
			Type type = Type.GetType(name);
			if (null == type && ObjectSchemaLoader.unHandledTypeList.ContainsKey(name))
			{
				type = ObjectSchemaLoader.unHandledTypeList[name];
			}
			return type;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001DE44 File Offset: 0x0001C044
		public static object GetStaticField(string typeName, string fieldName)
		{
			FieldInfo field = Type.GetType(typeName).GetField(fieldName, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (!(field != null))
			{
				return null;
			}
			return field.GetValue(null);
		}

		// Token: 0x04000401 RID: 1025
		private static Dictionary<string, Type> unHandledTypeList = new Dictionary<string, Type>();

		// Token: 0x04000402 RID: 1026
		private XmlNode objectDefinition;
	}
}
