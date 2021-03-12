using System;
using System.IO;
using System.Management.Automation;
using System.Security;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management
{
	// Token: 0x02000101 RID: 257
	[LocDescription(Strings.IDs.SetAppConfigValue)]
	[Cmdlet("Set", "AppConfigValue", DefaultParameterSetName = "Attribute")]
	public class SetAppConfigValue : Task
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x000208E0 File Offset: 0x0001EAE0
		public SetAppConfigValue()
		{
			TaskLogger.LogEnter();
			this.OldValue = string.Empty;
			TaskLogger.LogExit();
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x000208FD File Offset: 0x0001EAFD
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00020914 File Offset: 0x0001EB14
		[Parameter(Mandatory = true)]
		public string ConfigFileFullPath
		{
			get
			{
				return (string)base.Fields["ConfigFileFullPath"];
			}
			set
			{
				base.Fields["ConfigFileFullPath"] = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00020927 File Offset: 0x0001EB27
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0002093E File Offset: 0x0001EB3E
		[Parameter(Mandatory = true)]
		public string Element
		{
			get
			{
				return (string)base.Fields["Element"];
			}
			set
			{
				base.Fields["Element"] = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00020951 File Offset: 0x0001EB51
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x00020968 File Offset: 0x0001EB68
		[Parameter(ParameterSetName = "Attribute", Mandatory = true)]
		public string Attribute
		{
			get
			{
				return (string)base.Fields["Attribute"];
			}
			set
			{
				base.Fields["Attribute"] = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0002097B File Offset: 0x0001EB7B
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x00020992 File Offset: 0x0001EB92
		[Parameter(ParameterSetName = "Remove", Mandatory = false)]
		[Parameter(ParameterSetName = "AppSettingKey", Mandatory = true)]
		public string AppSettingKey
		{
			get
			{
				return (string)base.Fields["AppSettingKey"];
			}
			set
			{
				base.Fields["AppSettingKey"] = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x000209A5 File Offset: 0x0001EBA5
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x000209BC File Offset: 0x0001EBBC
		[Parameter(ParameterSetName = "AppSettingKey", Mandatory = true)]
		[AllowEmptyString]
		[Parameter(ParameterSetName = "Attribute", Mandatory = true)]
		public string NewValue
		{
			get
			{
				return (string)base.Fields["NewValue"];
			}
			set
			{
				base.Fields["NewValue"] = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x000209CF File Offset: 0x0001EBCF
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x000209E6 File Offset: 0x0001EBE6
		[Parameter(ParameterSetName = "Attribute", Mandatory = false)]
		[Parameter(ParameterSetName = "AppSettingKey", Mandatory = false)]
		public string OldValue
		{
			get
			{
				return (string)base.Fields["OldValue"];
			}
			set
			{
				base.Fields["OldValue"] = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x000209F9 File Offset: 0x0001EBF9
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x00020A10 File Offset: 0x0001EC10
		[Parameter(ParameterSetName = "ListValues", Mandatory = true)]
		public MultiValuedProperty<string> ListValues
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["ListValues"];
			}
			set
			{
				base.Fields["ListValues"] = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00020A23 File Offset: 0x0001EC23
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x00020A49 File Offset: 0x0001EC49
		[Parameter(ParameterSetName = "Attribute", Mandatory = false)]
		[Parameter(ParameterSetName = "XmlNode", Mandatory = false)]
		public SwitchParameter InsertAsFirst
		{
			get
			{
				return (SwitchParameter)(base.Fields["InsertAsFirst"] ?? false);
			}
			set
			{
				base.Fields["InsertAsFirst"] = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00020A61 File Offset: 0x0001EC61
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x00020A87 File Offset: 0x0001EC87
		[Parameter(ParameterSetName = "Remove", Mandatory = true)]
		public SwitchParameter Remove
		{
			get
			{
				return (SwitchParameter)(base.Fields["Remove"] ?? false);
			}
			set
			{
				base.Fields["Remove"] = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00020A9F File Offset: 0x0001EC9F
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00020AB6 File Offset: 0x0001ECB6
		[Parameter(ParameterSetName = "XmlNode", Mandatory = true)]
		public XmlNode XmlNode
		{
			get
			{
				return (XmlNode)base.Fields["XmlNode"];
			}
			set
			{
				base.Fields["XmlNode"] = value;
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00020ACC File Offset: 0x0001ECCC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			try
			{
				int num = 0;
				try
				{
					IL_0D:
					safeXmlDocument.Load(this.ConfigFileFullPath);
					XmlElement xmlElement = this.FindOrCreateElement(safeXmlDocument, base.ParameterSetName != "Remove");
					if (xmlElement == null)
					{
						if (base.ParameterSetName == "Remove")
						{
							this.WriteWarning(Strings.ElementNotFound(this.Element));
						}
						return;
					}
					string parameterSetName;
					if ((parameterSetName = base.ParameterSetName) != null)
					{
						if (parameterSetName == "AppSettingKey")
						{
							this.AddAppSettingEntry(xmlElement);
							goto IL_F3;
						}
						if (parameterSetName == "Attribute")
						{
							this.AddAttribute(xmlElement);
							goto IL_F3;
						}
						if (parameterSetName == "ListValues")
						{
							this.AddListValues(xmlElement);
							goto IL_F3;
						}
						if (parameterSetName == "Remove")
						{
							this.RemoveElementOrAppSettingEntry(xmlElement);
							goto IL_F3;
						}
						if (parameterSetName == "XmlNode")
						{
							this.SetXmlNode(safeXmlDocument, xmlElement);
							goto IL_F3;
						}
					}
					base.WriteError(new InvalidOperationException("invalid parameter set name"), (ErrorCategory)1003, null);
					IL_F3:
					safeXmlDocument.Save(this.ConfigFileFullPath);
				}
				catch (IOException exception)
				{
					num++;
					if (num > 3)
					{
						base.WriteError(exception, (ErrorCategory)1003, null);
					}
					else
					{
						this.WriteWarning(Strings.AppConfigIOException(this.ConfigFileFullPath, 3, 3));
						Thread.Sleep(3000);
					}
					goto IL_0D;
				}
			}
			catch (XmlException exception2)
			{
				base.WriteError(exception2, (ErrorCategory)1003, null);
			}
			catch (ArgumentException exception3)
			{
				base.WriteError(exception3, (ErrorCategory)1003, null);
			}
			catch (UnauthorizedAccessException exception4)
			{
				base.WriteError(exception4, (ErrorCategory)1003, null);
			}
			catch (NotSupportedException exception5)
			{
				base.WriteError(exception5, (ErrorCategory)1003, null);
			}
			catch (SecurityException exception6)
			{
				base.WriteError(exception6, (ErrorCategory)1003, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00020D0C File Offset: 0x0001EF0C
		private XmlElement FindOrCreateElement(SafeXmlDocument xmlDoc, bool create)
		{
			string[] array = this.Element.Split(new char[]
			{
				'\\',
				'/'
			});
			XmlNode xmlNode = null;
			bool flag = true;
			string text = array[0];
			using (XmlNodeList xmlNodeList = xmlDoc.SelectNodes(text))
			{
				if (xmlNodeList.Count != 1)
				{
					base.WriteError(new LocalizedException(Strings.RootNodeDoesNotMatch(text, this.ConfigFileFullPath)), (ErrorCategory)1003, null);
				}
				XmlNode xmlNode2;
				xmlNode = (xmlNode2 = xmlNodeList[0]);
				int i = 1;
				while (i < array.GetLength(0))
				{
					text = array[i];
					if (flag)
					{
						using (XmlNodeList xmlNodeList2 = xmlNode2.SelectNodes(text))
						{
							if (xmlNodeList2.Count > 1)
							{
								base.WriteError(new LocalizedException(Strings.NodeNotUnique(text, this.ConfigFileFullPath)), (ErrorCategory)1003, null);
							}
							else if (xmlNodeList2.Count == 1)
							{
								xmlNode = xmlNodeList2[0];
							}
							else
							{
								if (!create)
								{
									return null;
								}
								xmlNode = this.CreateChild(xmlDoc, xmlNode2, text);
								flag = false;
							}
							goto IL_F9;
						}
						goto IL_E5;
					}
					goto IL_E5;
					IL_F9:
					xmlNode2 = xmlNode;
					i++;
					continue;
					IL_E5:
					if (create)
					{
						xmlNode = this.CreateChild(xmlDoc, xmlNode2, text);
						goto IL_F9;
					}
					return null;
				}
			}
			XmlElement xmlElement = xmlNode as XmlElement;
			if (xmlElement == null)
			{
				base.WriteError(new LocalizedException(Strings.NodeNotElement(xmlNode.Name, this.ConfigFileFullPath)), (ErrorCategory)1003, null);
			}
			return xmlElement;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00020E88 File Offset: 0x0001F088
		private void AddAppSettingEntry(XmlElement currElement)
		{
			XmlDocument ownerDocument = currElement.OwnerDocument;
			XmlElement xmlElement;
			if (this.TryFindAppSettingEntry(currElement, out xmlElement))
			{
				XmlAttribute xmlAttribute = xmlElement.GetAttributeNode("value");
				if (this.ShouldUpdate(this.OldValue, this.NewValue, xmlAttribute.Value, StringComparison.OrdinalIgnoreCase))
				{
					string value = xmlAttribute.Value;
					xmlAttribute.Value = this.NewValue;
					XmlComment newChild = ownerDocument.CreateComment(string.Format("Exchange cmdlet Set-AppConfigValue updated value for dictionary key {0} from {1} to {2} at {3}", new object[]
					{
						this.AppSettingKey,
						value,
						this.NewValue,
						DateTime.UtcNow.ToLocalTime()
					}));
					currElement.InsertBefore(newChild, xmlElement);
					return;
				}
			}
			else
			{
				XmlElement xmlElement2 = ownerDocument.CreateElement("add");
				currElement.AppendChild(xmlElement2);
				XmlAttribute xmlAttribute2 = ownerDocument.CreateAttribute("key");
				xmlAttribute2.Value = this.AppSettingKey;
				xmlElement2.SetAttributeNode(xmlAttribute2);
				XmlAttribute xmlAttribute = ownerDocument.CreateAttribute("value");
				xmlAttribute.Value = this.NewValue;
				xmlElement2.SetAttributeNode(xmlAttribute);
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00020FA0 File Offset: 0x0001F1A0
		private bool TryFindAppSettingEntry(XmlElement currElement, out XmlElement foundEntry)
		{
			using (XmlNodeList xmlNodeList = currElement.SelectNodes("add"))
			{
				foundEntry = null;
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlElement xmlElement = xmlNode as XmlElement;
					if (xmlElement == null)
					{
						base.WriteError(new LocalizedException(Strings.NodeNotElement("add", this.ConfigFileFullPath)), (ErrorCategory)1003, null);
					}
					if (xmlElement.HasAttribute("key") && xmlElement.HasAttribute("value"))
					{
						XmlAttribute attributeNode = xmlElement.GetAttributeNode("key");
						if (this.AppSettingKey.Equals(attributeNode.Value))
						{
							foundEntry = xmlElement;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00021090 File Offset: 0x0001F290
		private void AddAttribute(XmlElement currElement)
		{
			XmlAttribute xmlAttribute = currElement.GetAttributeNode(this.Attribute);
			XmlDocument ownerDocument = currElement.OwnerDocument;
			if (xmlAttribute == null)
			{
				xmlAttribute = ownerDocument.CreateAttribute(this.Attribute);
				xmlAttribute.Value = this.NewValue;
				currElement.SetAttributeNode(xmlAttribute);
				return;
			}
			if (this.ShouldUpdate(this.OldValue, this.NewValue, xmlAttribute.Value, StringComparison.OrdinalIgnoreCase))
			{
				string value = xmlAttribute.Value;
				xmlAttribute.Value = this.NewValue;
				XmlComment newChild = ownerDocument.CreateComment(string.Format("Exchange cmdlet Set-AppConfigValue updated value of attribute {0} from {1} to {2} at {3}", new object[]
				{
					this.Attribute,
					value,
					this.NewValue,
					DateTime.UtcNow.ToLocalTime()
				}));
				currElement.ParentNode.InsertBefore(newChild, currElement);
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0002115D File Offset: 0x0001F35D
		private void RemoveElementOrAppSettingEntry(XmlElement currElement)
		{
			if (this.AppSettingKey != null)
			{
				this.RemoveAppSettingEntry(currElement);
				return;
			}
			this.RemoveElement(currElement);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00021178 File Offset: 0x0001F378
		private void SetXmlNode(XmlDocument xmlDoc, XmlElement currElement)
		{
			XmlNode xmlNode = xmlDoc.ImportNode(this.XmlNode, true);
			if (!string.Equals(currElement.Name, xmlNode.Name, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new ArgumentException(Strings.ImportNodeNameDoesNotMatch(xmlNode.Name, currElement.Name));
			}
			currElement.ParentNode.AppendChild(xmlNode);
			currElement.ParentNode.RemoveChild(currElement);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000211DD File Offset: 0x0001F3DD
		private void RemoveElement(XmlElement currElement)
		{
			if (currElement.ParentNode is XmlDocument)
			{
				base.WriteError(new LocalizedException(Strings.RootNodeCannotBeRemoved(currElement.Name, this.ConfigFileFullPath)), (ErrorCategory)1003, null);
			}
			currElement.ParentNode.RemoveChild(currElement);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0002121C File Offset: 0x0001F41C
		private void RemoveAppSettingEntry(XmlElement currElement)
		{
			XmlDocument ownerDocument = currElement.OwnerDocument;
			XmlElement xmlElement = null;
			if (this.TryFindAppSettingEntry(currElement, out xmlElement))
			{
				XmlComment newChild = ownerDocument.CreateComment(string.Format("Exchange cmdlet Set-AppConfigValue removed dictionary key {0} at {1}", this.AppSettingKey, DateTime.UtcNow.ToLocalTime()));
				currElement.InsertBefore(newChild, xmlElement);
				currElement.RemoveChild(xmlElement);
				return;
			}
			this.WriteWarning(Strings.AppSettingKeyNotFound(this.AppSettingKey));
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0002128B File Offset: 0x0001F48B
		private bool ShouldUpdate(string expectedValue, string newValue, string actualValue, StringComparison stringComparison)
		{
			return (string.IsNullOrEmpty(expectedValue) || actualValue.Equals(expectedValue, stringComparison)) && !actualValue.Equals(newValue, stringComparison);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000212B0 File Offset: 0x0001F4B0
		private XmlNode CreateChild(SafeXmlDocument xmlDoc, XmlNode parentNode, string childName)
		{
			string name = childName;
			int num = childName.IndexOf("[");
			if (num >= 0)
			{
				name = childName.Substring(0, num);
			}
			XmlNode xmlNode = xmlDoc.CreateElement(name);
			XmlNode firstChild = parentNode.FirstChild;
			if (this.InsertAsFirst && firstChild != null)
			{
				parentNode.InsertBefore(xmlNode, firstChild);
			}
			else
			{
				parentNode.AppendChild(xmlNode);
			}
			return xmlNode;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0002130C File Offset: 0x0001F50C
		private void AddListValues(XmlElement currElement)
		{
			XmlDocument ownerDocument = currElement.OwnerDocument;
			currElement.RemoveAll();
			foreach (string value in this.ListValues)
			{
				XmlElement xmlElement = ownerDocument.CreateElement("add");
				currElement.AppendChild(xmlElement);
				XmlAttribute xmlAttribute = ownerDocument.CreateAttribute("value");
				xmlAttribute.Value = value;
				xmlElement.SetAttributeNode(xmlAttribute);
			}
		}

		// Token: 0x0400039C RID: 924
		private const string AttributeParameterSetName = "Attribute";

		// Token: 0x0400039D RID: 925
		private const string AppSettingKeyParameterSetName = "AppSettingKey";

		// Token: 0x0400039E RID: 926
		private const string ListValuesParameterSetName = "ListValues";

		// Token: 0x0400039F RID: 927
		private const string RemoveParameterSetName = "Remove";

		// Token: 0x040003A0 RID: 928
		private const string XmlNodeParameterSetName = "XmlNode";
	}
}
