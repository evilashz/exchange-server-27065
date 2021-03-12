using System;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000363 RID: 867
	[Serializable]
	public class ADE4eVirtualDirectory : ExchangeWebAppVirtualDirectory
	{
		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x000A7AAD File Offset: 0x000A5CAD
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADE4eVirtualDirectory.schema;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x000A7AB4 File Offset: 0x000A5CB4
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchEncryptionVirtualDirectory";
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060027EB RID: 10219 RVA: 0x000A7ABB File Offset: 0x000A5CBB
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000A7ACE File Offset: 0x000A5CCE
		internal override void Initialize()
		{
			this.LoadSettings();
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060027ED RID: 10221 RVA: 0x000A7AD6 File Offset: 0x000A5CD6
		// (set) Token: 0x060027EE RID: 10222 RVA: 0x000A7AE8 File Offset: 0x000A5CE8
		public string E4EConfigurationXML
		{
			get
			{
				return (string)this[ADE4eVirtualDirectorySchema.E4EConfigurationXML];
			}
			set
			{
				this[ADE4eVirtualDirectorySchema.E4EConfigurationXML] = value;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x000A7AF8 File Offset: 0x000A5CF8
		// (set) Token: 0x060027F0 RID: 10224 RVA: 0x000A7B18 File Offset: 0x000A5D18
		public MultiValuedProperty<string> AllowedFileTypes
		{
			get
			{
				string valueForTag = this.GetValueForTag("AllowedFileTypes");
				return this.StringToArray(valueForTag);
			}
			set
			{
				string text = this.ArrayToString(value);
				if (text != this.GetValueForTag("AllowedFileTypes"))
				{
					this.SetValueForTag("AllowedFileTypes", text);
				}
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x000A7B4C File Offset: 0x000A5D4C
		// (set) Token: 0x060027F2 RID: 10226 RVA: 0x000A7B6C File Offset: 0x000A5D6C
		public MultiValuedProperty<string> AllowedMimeTypes
		{
			get
			{
				string valueForTag = this.GetValueForTag("AllowedMimeTypes");
				return this.StringToArray(valueForTag);
			}
			set
			{
				string text = this.ArrayToString(value);
				if (text != this.GetValueForTag("AllowedMimeTypes"))
				{
					this.SetValueForTag("AllowedMimeTypes", text);
				}
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x000A7BA0 File Offset: 0x000A5DA0
		// (set) Token: 0x060027F4 RID: 10228 RVA: 0x000A7BC0 File Offset: 0x000A5DC0
		public MultiValuedProperty<string> BlockedFileTypes
		{
			get
			{
				string valueForTag = this.GetValueForTag("BlockedFileTypes");
				return this.StringToArray(valueForTag);
			}
			set
			{
				string text = this.ArrayToString(value);
				if (text != this.GetValueForTag("BlockedFileTypes"))
				{
					this.SetValueForTag("BlockedFileTypes", text);
				}
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x000A7BF4 File Offset: 0x000A5DF4
		// (set) Token: 0x060027F6 RID: 10230 RVA: 0x000A7C14 File Offset: 0x000A5E14
		public MultiValuedProperty<string> BlockedMimeTypes
		{
			get
			{
				string valueForTag = this.GetValueForTag("BlockedMimeTypes");
				return this.StringToArray(valueForTag);
			}
			set
			{
				string text = this.ArrayToString(value);
				if (text != this.GetValueForTag("BlockedMimeTypes"))
				{
					this.SetValueForTag("BlockedMimeTypes", text);
				}
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x000A7C48 File Offset: 0x000A5E48
		// (set) Token: 0x060027F8 RID: 10232 RVA: 0x000A7C68 File Offset: 0x000A5E68
		public MultiValuedProperty<string> ForceSaveFileTypes
		{
			get
			{
				string valueForTag = this.GetValueForTag("ForceSaveFileTypes");
				return this.StringToArray(valueForTag);
			}
			set
			{
				string text = this.ArrayToString(value);
				if (text != this.GetValueForTag("ForceSaveFileTypes"))
				{
					this.SetValueForTag("ForceSaveFileTypes", text);
				}
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x000A7C9C File Offset: 0x000A5E9C
		// (set) Token: 0x060027FA RID: 10234 RVA: 0x000A7CBC File Offset: 0x000A5EBC
		public MultiValuedProperty<string> ForceSaveMimeTypes
		{
			get
			{
				string valueForTag = this.GetValueForTag("ForceSaveMimeTypes");
				return this.StringToArray(valueForTag);
			}
			set
			{
				string text = this.ArrayToString(value);
				if (text != this.GetValueForTag("ForceSaveMimeTypes"))
				{
					this.SetValueForTag("ForceSaveMimeTypes", text);
				}
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x000A7CF0 File Offset: 0x000A5EF0
		// (set) Token: 0x060027FC RID: 10236 RVA: 0x000A7D28 File Offset: 0x000A5F28
		public bool? AlwaysShowBcc
		{
			get
			{
				string valueForTag = this.GetValueForTag("AlwaysShowBcc");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new bool?(bool.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("AlwaysShowBcc"))
				{
					this.SetValueForTag("AlwaysShowBcc", text);
				}
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x000A7D70 File Offset: 0x000A5F70
		// (set) Token: 0x060027FE RID: 10238 RVA: 0x000A7DA8 File Offset: 0x000A5FA8
		public bool? CheckForForgottenAttachments
		{
			get
			{
				string valueForTag = this.GetValueForTag("CheckForForgottenAttachments");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new bool?(bool.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("CheckForForgottenAttachments"))
				{
					this.SetValueForTag("CheckForForgottenAttachments", text);
				}
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060027FF RID: 10239 RVA: 0x000A7DF0 File Offset: 0x000A5FF0
		// (set) Token: 0x06002800 RID: 10240 RVA: 0x000A7E28 File Offset: 0x000A6028
		public bool? HideMailTipsByDefault
		{
			get
			{
				string valueForTag = this.GetValueForTag("HideMailTipsByDefault");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new bool?(bool.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("HideMailTipsByDefault"))
				{
					this.SetValueForTag("HideMailTipsByDefault", text);
				}
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002801 RID: 10241 RVA: 0x000A7E70 File Offset: 0x000A6070
		// (set) Token: 0x06002802 RID: 10242 RVA: 0x000A7EA8 File Offset: 0x000A60A8
		public uint? MailTipsLargeAudienceThreshold
		{
			get
			{
				string valueForTag = this.GetValueForTag("MailTipsLargeAudienceThreshold");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new uint?(uint.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("MailTipsLargeAudienceThreshold"))
				{
					this.SetValueForTag("MailTipsLargeAudienceThreshold", text);
				}
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002803 RID: 10243 RVA: 0x000A7EF0 File Offset: 0x000A60F0
		// (set) Token: 0x06002804 RID: 10244 RVA: 0x000A7F28 File Offset: 0x000A6128
		public int? MaxRecipientsPerMessage
		{
			get
			{
				string valueForTag = this.GetValueForTag("MaxRecipientsPerMessage");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new int?(int.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("MaxRecipientsPerMessage"))
				{
					this.SetValueForTag("MaxRecipientsPerMessage", text);
				}
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002805 RID: 10245 RVA: 0x000A7F70 File Offset: 0x000A6170
		// (set) Token: 0x06002806 RID: 10246 RVA: 0x000A7FA8 File Offset: 0x000A61A8
		public int? MaxMessageSizeInKb
		{
			get
			{
				string valueForTag = this.GetValueForTag("MaxMessageSizeInKb");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new int?(int.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("MaxMessageSizeInKb"))
				{
					this.SetValueForTag("MaxMessageSizeInKb", text);
				}
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002807 RID: 10247 RVA: 0x000A7FEE File Offset: 0x000A61EE
		// (set) Token: 0x06002808 RID: 10248 RVA: 0x000A7FFB File Offset: 0x000A61FB
		public string ComposeFontColor
		{
			get
			{
				return this.GetValueForTag("ComposeFontColor");
			}
			set
			{
				if (value != this.GetValueForTag("ComposeFontColor"))
				{
					this.SetValueForTag("ComposeFontColor", value);
				}
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x000A801C File Offset: 0x000A621C
		// (set) Token: 0x0600280A RID: 10250 RVA: 0x000A8029 File Offset: 0x000A6229
		public string ComposeFontName
		{
			get
			{
				return this.GetValueForTag("ComposeFontName");
			}
			set
			{
				if (value != this.GetValueForTag("ComposeFontName"))
				{
					this.SetValueForTag("ComposeFontName", value);
				}
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x0600280B RID: 10251 RVA: 0x000A804C File Offset: 0x000A624C
		// (set) Token: 0x0600280C RID: 10252 RVA: 0x000A8084 File Offset: 0x000A6284
		public int? ComposeFontSize
		{
			get
			{
				string valueForTag = this.GetValueForTag("ComposeFontSize");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new int?(int.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("ComposeFontSize"))
				{
					this.SetValueForTag("ComposeFontSize", text);
				}
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600280D RID: 10253 RVA: 0x000A80CC File Offset: 0x000A62CC
		// (set) Token: 0x0600280E RID: 10254 RVA: 0x000A8104 File Offset: 0x000A6304
		public int? MaxImageSizeKB
		{
			get
			{
				string valueForTag = this.GetValueForTag("MaxImageSizeKB");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new int?(int.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("MaxImageSizeKB"))
				{
					this.SetValueForTag("MaxImageSizeKB", text);
				}
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x000A814C File Offset: 0x000A634C
		// (set) Token: 0x06002810 RID: 10256 RVA: 0x000A8184 File Offset: 0x000A6384
		public int? MaxAttachmentSizeKB
		{
			get
			{
				string valueForTag = this.GetValueForTag("MaxAttachmentSizeKB");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new int?(int.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("MaxAttachmentSizeKB"))
				{
					this.SetValueForTag("MaxAttachmentSizeKB", text);
				}
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x000A81CC File Offset: 0x000A63CC
		// (set) Token: 0x06002812 RID: 10258 RVA: 0x000A8204 File Offset: 0x000A6404
		public int? MaxEncryptedContentSizeKB
		{
			get
			{
				string valueForTag = this.GetValueForTag("MaxEncryptedContentSizeKB");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new int?(int.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("MaxEncryptedContentSizeKB"))
				{
					this.SetValueForTag("MaxEncryptedContentSizeKB", text);
				}
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002813 RID: 10259 RVA: 0x000A824C File Offset: 0x000A644C
		// (set) Token: 0x06002814 RID: 10260 RVA: 0x000A8284 File Offset: 0x000A6484
		public int? MaxEmailStringSize
		{
			get
			{
				string valueForTag = this.GetValueForTag("MaxEmailStringSize");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new int?(int.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("MaxEmailStringSize"))
				{
					this.SetValueForTag("MaxEmailStringSize", text);
				}
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002815 RID: 10261 RVA: 0x000A82CC File Offset: 0x000A64CC
		// (set) Token: 0x06002816 RID: 10262 RVA: 0x000A8304 File Offset: 0x000A6504
		public int? MaxPortalStringSize
		{
			get
			{
				string valueForTag = this.GetValueForTag("MaxPortalStringSize");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new int?(int.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("MaxPortalStringSize"))
				{
					this.SetValueForTag("MaxPortalStringSize", text);
				}
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x000A834C File Offset: 0x000A654C
		// (set) Token: 0x06002818 RID: 10264 RVA: 0x000A8384 File Offset: 0x000A6584
		public int? MaxFwdAllowed
		{
			get
			{
				string valueForTag = this.GetValueForTag("MaxFwdAllowed");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new int?(int.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("MaxFwdAllowed"))
				{
					this.SetValueForTag("MaxFwdAllowed", text);
				}
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002819 RID: 10265 RVA: 0x000A83CC File Offset: 0x000A65CC
		// (set) Token: 0x0600281A RID: 10266 RVA: 0x000A8404 File Offset: 0x000A6604
		public int? PortalInactivityTimeout
		{
			get
			{
				string valueForTag = this.GetValueForTag("PortalInactivityTimeout");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new int?(int.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("PortalInactivityTimeout"))
				{
					this.SetValueForTag("PortalInactivityTimeout", text);
				}
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x000A844C File Offset: 0x000A664C
		// (set) Token: 0x0600281C RID: 10268 RVA: 0x000A8484 File Offset: 0x000A6684
		public int? TDSTimeOut
		{
			get
			{
				string valueForTag = this.GetValueForTag("TDSTimeOut");
				if (string.IsNullOrEmpty(valueForTag))
				{
					return null;
				}
				return new int?(int.Parse(valueForTag));
			}
			set
			{
				string text = (value != null) ? value.ToString() : null;
				if (text != this.GetValueForTag("TDSTimeOut"))
				{
					this.SetValueForTag("TDSTimeOut", text);
				}
			}
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000A84CA File Offset: 0x000A66CA
		public void LoadSettings()
		{
			this.LoadVDirSettings();
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000A84D2 File Offset: 0x000A66D2
		public void SaveSettings()
		{
			this.SaveVDirSettings();
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x000A84DC File Offset: 0x000A66DC
		private string ArrayToString(MultiValuedProperty<string> input)
		{
			if (input == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < input.Count; i++)
			{
				string value = input[i];
				if (!string.IsNullOrWhiteSpace(value))
				{
					if (i != 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000A8534 File Offset: 0x000A6734
		private MultiValuedProperty<string> StringToArray(string input)
		{
			if (input == null)
			{
				return null;
			}
			string[] array = input.Split(new char[]
			{
				','
			});
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			foreach (string text in array)
			{
				if (!string.IsNullOrWhiteSpace(text))
				{
					multiValuedProperty.Add(text);
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000A858C File Offset: 0x000A678C
		private string GetValueForTag(string tag)
		{
			if (this.xmlDoc != null && this.xmlDoc.DocumentElement != null)
			{
				XmlElement documentElement = this.xmlDoc.DocumentElement;
				if (documentElement.HasChildNodes)
				{
					for (int i = 0; i < documentElement.ChildNodes.Count; i++)
					{
						if (documentElement.ChildNodes[i].Name == tag)
						{
							return ((XmlElement)documentElement.ChildNodes[i]).GetAttribute("Value");
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000A8610 File Offset: 0x000A6810
		private void SetValueForTag(string tag, string value)
		{
			if (this.xmlDoc != null)
			{
				XmlElement xmlElement;
				if (this.xmlDoc.DocumentElement != null)
				{
					xmlElement = this.xmlDoc.DocumentElement;
				}
				else
				{
					xmlElement = this.xmlDoc.CreateElement("E4E");
					this.xmlDoc.AppendChild(xmlElement);
				}
				XmlElement xmlElement2 = null;
				if (xmlElement.HasChildNodes)
				{
					for (int i = 0; i < xmlElement.ChildNodes.Count; i++)
					{
						if (xmlElement.ChildNodes[i].Name == tag)
						{
							xmlElement2 = (XmlElement)xmlElement.ChildNodes[i];
						}
					}
				}
				if (value == null)
				{
					if (xmlElement2 != null)
					{
						xmlElement.RemoveChild(xmlElement2);
						return;
					}
				}
				else
				{
					if (xmlElement2 == null)
					{
						xmlElement2 = this.xmlDoc.CreateElement(tag);
						xmlElement.AppendChild(xmlElement2);
					}
					xmlElement2.SetAttribute("Value", value);
				}
			}
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000A86E4 File Offset: 0x000A68E4
		private void LoadVDirSettings()
		{
			if (this.xmlDoc == null)
			{
				string e4EConfigurationXML = this.E4EConfigurationXML;
				this.xmlDoc = new SafeXmlDocument();
				if (string.IsNullOrEmpty(e4EConfigurationXML))
				{
					XmlElement newChild = this.xmlDoc.CreateElement("E4E");
					this.xmlDoc.AppendChild(newChild);
					return;
				}
				this.xmlDoc.LoadXml(e4EConfigurationXML);
			}
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000A8740 File Offset: 0x000A6940
		private void SaveVDirSettings()
		{
			if (this.xmlDoc != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
				{
					this.xmlDoc.Save(xmlWriter);
					xmlWriter.Close();
				}
				this.E4EConfigurationXML = stringBuilder.ToString();
			}
		}

		// Token: 0x04001844 RID: 6212
		private const string MostDerivedClassName = "msExchEncryptionVirtualDirectory";

		// Token: 0x04001845 RID: 6213
		internal const string VDirName = "Encryption";

		// Token: 0x04001846 RID: 6214
		private const string ValueAttribute = "Value";

		// Token: 0x04001847 RID: 6215
		private const string RootNodeName = "E4E";

		// Token: 0x04001848 RID: 6216
		public const string AllowedFileTypesProperty = "AllowedFileTypes";

		// Token: 0x04001849 RID: 6217
		public const string AllowedMimeTypesProperty = "AllowedMimeTypes";

		// Token: 0x0400184A RID: 6218
		public const string BlockedFileTypesProperty = "BlockedFileTypes";

		// Token: 0x0400184B RID: 6219
		public const string BlockedMimeTypesProperty = "BlockedMimeTypes";

		// Token: 0x0400184C RID: 6220
		public const string ForceSaveFileTypesProperty = "ForceSaveFileTypes";

		// Token: 0x0400184D RID: 6221
		public const string ForceSaveMimeTypesProperty = "ForceSaveMimeTypes";

		// Token: 0x0400184E RID: 6222
		public const string AlwaysShowBccProperty = "AlwaysShowBcc";

		// Token: 0x0400184F RID: 6223
		public const string CheckForForgottenAttachmentsProperty = "CheckForForgottenAttachments";

		// Token: 0x04001850 RID: 6224
		public const string HideMailTipsByDefaultProperty = "HideMailTipsByDefault";

		// Token: 0x04001851 RID: 6225
		public const string MailTipsLargeAudienceThresholdProperty = "MailTipsLargeAudienceThreshold";

		// Token: 0x04001852 RID: 6226
		public const string MaxRecipientsPerMessageProperty = "MaxRecipientsPerMessage";

		// Token: 0x04001853 RID: 6227
		public const string MaxMessageSizeInKbProperty = "MaxMessageSizeInKb";

		// Token: 0x04001854 RID: 6228
		public const string ComposeFontColorProperty = "ComposeFontColor";

		// Token: 0x04001855 RID: 6229
		public const string ComposeFontNameProperty = "ComposeFontName";

		// Token: 0x04001856 RID: 6230
		public const string ComposeFontSizeProperty = "ComposeFontSize";

		// Token: 0x04001857 RID: 6231
		public const string MaxImageSizeKBProperty = "MaxImageSizeKB";

		// Token: 0x04001858 RID: 6232
		public const string MaxAttachmentSizeKBProperty = "MaxAttachmentSizeKB";

		// Token: 0x04001859 RID: 6233
		public const string MaxEncryptedContentSizeKBProperty = "MaxEncryptedContentSizeKB";

		// Token: 0x0400185A RID: 6234
		public const string MaxEmailStringSizeProperty = "MaxEmailStringSize";

		// Token: 0x0400185B RID: 6235
		public const string MaxPortalStringSizeProperty = "MaxPortalStringSize";

		// Token: 0x0400185C RID: 6236
		public const string MaxFwdAllowedProperty = "MaxFwdAllowed";

		// Token: 0x0400185D RID: 6237
		public const string PortalInactivityTimeoutProperty = "PortalInactivityTimeout";

		// Token: 0x0400185E RID: 6238
		public const string TDSTimeOutProperty = "TDSTimeOut";

		// Token: 0x0400185F RID: 6239
		private static readonly ADE4eVirtualDirectorySchema schema = ObjectSchema.GetInstance<ADE4eVirtualDirectorySchema>();

		// Token: 0x04001860 RID: 6240
		private SafeXmlDocument xmlDoc;
	}
}
