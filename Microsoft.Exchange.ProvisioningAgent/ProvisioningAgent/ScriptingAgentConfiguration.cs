using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000060 RID: 96
	internal class ScriptingAgentConfiguration
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		public ScriptingAgentConfiguration(string xmlConfigPath)
		{
			this.provisionDefaultPropertiesDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.provisionDefaultPropertiesDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.provisionPhysicalPropertiesDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.updateAffectedIConfigurableDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.validateDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.onCompleteDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.supportedCmdlets = new List<string>();
			Exception ex = null;
			try
			{
				this.Initialize(xmlConfigPath);
			}
			catch (FileNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (InvalidOperationException ex3)
			{
				ex = ex3;
			}
			catch (XmlException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				throw new ProvisioningException(Strings.ScriptingAgentInitializationFailed(ex.Message), ex);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000FB34 File Offset: 0x0000DD34
		public Dictionary<string, string> ProvisionDefaultPropertiesDictionary
		{
			get
			{
				return this.provisionDefaultPropertiesDictionary;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000FB3C File Offset: 0x0000DD3C
		public Dictionary<string, string> ProvisionPhysicalPropertiesDictionary
		{
			get
			{
				return this.provisionPhysicalPropertiesDictionary;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000FB44 File Offset: 0x0000DD44
		public Dictionary<string, string> UpdateAffectedIConfigurableDictionary
		{
			get
			{
				return this.updateAffectedIConfigurableDictionary;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000FB4C File Offset: 0x0000DD4C
		public Dictionary<string, string> ValidateDictionary
		{
			get
			{
				return this.validateDictionary;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000FB54 File Offset: 0x0000DD54
		public Dictionary<string, string> OnCompleteDictionary
		{
			get
			{
				return this.onCompleteDictionary;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		public string CommonFunctions
		{
			get
			{
				return this.commonFunctionsBuffer;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000FB64 File Offset: 0x0000DD64
		public void Initialize(string xmlConfigPath)
		{
			if (!File.Exists(xmlConfigPath))
			{
				throw new FileNotFoundException(Strings.ErrorFileIsNotFound(xmlConfigPath));
			}
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.Load(xmlConfigPath);
			XmlElement documentElement = safeXmlDocument.DocumentElement;
			if (documentElement == null)
			{
				throw new InvalidOperationException(Strings.XmlErrorMissingNode("xml", xmlConfigPath));
			}
			XmlNodeList xmlNodeList = documentElement.SelectNodes("/Configuration/Feature");
			if (xmlNodeList == null)
			{
				throw new InvalidOperationException(Strings.XmlErrorMissingNode("/Configuration/Feature", xmlConfigPath));
			}
			foreach (object obj in xmlNodeList)
			{
				XmlElement xmlElement = (XmlElement)obj;
				string attribute = xmlElement.GetAttribute("Cmdlets");
				if (attribute == null)
				{
					throw new InvalidOperationException(Strings.XmlErrorMissingAttribute("cmdlets", xmlConfigPath));
				}
				string[] array = attribute.Trim().Split(new char[]
				{
					','
				});
				XmlNodeList childNodes = xmlElement.ChildNodes;
				if (childNodes.Count == 0)
				{
					throw new InvalidOperationException(Strings.XmlErrorMissingNode("<ApiCall>", xmlConfigPath));
				}
				foreach (object obj2 in childNodes)
				{
					XmlElement xmlElement2 = (XmlElement)obj2;
					string innerText = xmlElement2.InnerText;
					if (innerText == null)
					{
						throw new XmlException(Strings.XmlErrorMissingInnerText(xmlConfigPath));
					}
					string text = xmlElement2.GetAttribute("Name");
					if (text == null)
					{
						throw new InvalidOperationException(Strings.XmlErrorMissingAttribute("Name", xmlConfigPath));
					}
					text = text.Trim();
					string[] array2 = array;
					int i = 0;
					while (i < array2.Length)
					{
						string cmdletName = array2[i];
						string a;
						if ((a = text) != null)
						{
							if (!(a == "Validate"))
							{
								if (!(a == "OnComplete"))
								{
									if (!(a == "ProvisionDefaultProperties"))
									{
										if (!(a == "ProvisionPhysicalProperties"))
										{
											if (!(a == "UpdateAffectedIConfigurable"))
											{
												goto IL_21B;
											}
											this.AddScriptToDictionary(innerText, cmdletName, this.UpdateAffectedIConfigurableDictionary);
										}
										else
										{
											this.AddScriptToDictionary(innerText, cmdletName, this.ProvisionPhysicalPropertiesDictionary);
										}
									}
									else
									{
										this.AddScriptToDictionary(innerText, cmdletName, this.ProvisionDefaultPropertiesDictionary);
									}
								}
								else
								{
									this.AddScriptToDictionary(innerText, cmdletName, this.OnCompleteDictionary);
								}
							}
							else
							{
								this.AddScriptToDictionary(innerText, cmdletName, this.ValidateDictionary);
							}
							i++;
							continue;
						}
						IL_21B:
						throw new InvalidOperationException(Strings.XmlErrorWrongAPI(xmlConfigPath, text));
					}
				}
			}
			XmlNode xmlNode = documentElement.SelectSingleNode("/Configuration/Common");
			if (xmlNode != null)
			{
				this.commonFunctionsBuffer = xmlNode.InnerText;
				return;
			}
			this.commonFunctionsBuffer = string.Empty;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000FE54 File Offset: 0x0000E054
		private void AddScriptToDictionary(string scriptletCode, string cmdletName, Dictionary<string, string> dictionary)
		{
			if (!this.supportedCmdlets.Contains(cmdletName))
			{
				this.supportedCmdlets.Add(cmdletName);
			}
			string arg;
			if (dictionary.TryGetValue(cmdletName, out arg))
			{
				dictionary[cmdletName] = string.Format("{0};\r\n{1}", arg, scriptletCode);
				return;
			}
			dictionary[cmdletName] = scriptletCode;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000FEA2 File Offset: 0x0000E0A2
		public IEnumerable<string> GetAllSupportedCmdlets()
		{
			return this.supportedCmdlets;
		}

		// Token: 0x0400013F RID: 319
		private Dictionary<string, string> provisionDefaultPropertiesDictionary;

		// Token: 0x04000140 RID: 320
		private Dictionary<string, string> provisionPhysicalPropertiesDictionary;

		// Token: 0x04000141 RID: 321
		private Dictionary<string, string> updateAffectedIConfigurableDictionary;

		// Token: 0x04000142 RID: 322
		private Dictionary<string, string> validateDictionary;

		// Token: 0x04000143 RID: 323
		private Dictionary<string, string> onCompleteDictionary;

		// Token: 0x04000144 RID: 324
		private List<string> supportedCmdlets;

		// Token: 0x04000145 RID: 325
		private string commonFunctionsBuffer;
	}
}
