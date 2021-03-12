using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002BC RID: 700
	[Serializable]
	public class DetailsTemplate : ADConfigurationObject
	{
		// Token: 0x06002009 RID: 8201 RVA: 0x0008E044 File Offset: 0x0008C244
		internal static ICollection<string> GetTemplateNames()
		{
			Collection<string> collection = new Collection<string>();
			foreach (KeyValuePair<string, string> keyValuePair in DetailsTemplate.templateTypes)
			{
				collection.Add(keyValuePair.Key);
			}
			return collection;
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0008E088 File Offset: 0x0008C288
		internal static string TranslateTemplateIDToName(string id)
		{
			foreach (KeyValuePair<string, string> keyValuePair in DetailsTemplate.templateTypes)
			{
				if (string.Compare(id, keyValuePair.Value, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x0008E0D4 File Offset: 0x0008C2D4
		internal static string TranslateTemplateNameToID(string name)
		{
			foreach (KeyValuePair<string, string> keyValuePair in DetailsTemplate.templateTypes)
			{
				if (string.Compare(name, keyValuePair.Key, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x0008E11F File Offset: 0x0008C31F
		public new string Name
		{
			get
			{
				return (string)this[ADObjectSchema.Name];
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x0008E131 File Offset: 0x0008C331
		internal override ADObjectSchema Schema
		{
			get
			{
				return DetailsTemplate.schemaObject;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x0008E138 File Offset: 0x0008C338
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "displayTemplate";
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x0008E140 File Offset: 0x0008C340
		public string TemplateType
		{
			get
			{
				if (this.templateType == null)
				{
					string name = this.Name;
					this.templateType = DetailsTemplate.TranslateTemplateIDToName(name);
					if (this.templateType == null)
					{
						this.templateType = "Unrecognized";
					}
				}
				return this.templateType;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x0008E181 File Offset: 0x0008C381
		// (set) Token: 0x06002011 RID: 8209 RVA: 0x0008E189 File Offset: 0x0008C389
		internal MAPIPropertiesDictionary MAPIPropertiesDictionary { get; set; }

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x0008E194 File Offset: 0x0008C394
		public string Language
		{
			get
			{
				if (this.language == null)
				{
					string distinguishedName = base.DistinguishedName;
					string s = distinguishedName.Split(new char[]
					{
						','
					})[1].Substring(3);
					Culture.TryGetCulture(int.Parse(s, NumberStyles.HexNumber), out this.language);
				}
				if (this.language == null)
				{
					return null;
				}
				CultureInfo cultureInfo = this.language.GetCultureInfo();
				if (this.language.LCID.Equals(cultureInfo.LCID))
				{
					return cultureInfo.DisplayName;
				}
				return this.language.Description;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x0008E227 File Offset: 0x0008C427
		public byte[] TemplateBlob
		{
			get
			{
				return (byte[])this[DetailsTemplateSchema.TemplateBlob];
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x0008E239 File Offset: 0x0008C439
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[DetailsTemplateSchema.ExchangeLegacyDN];
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x0008E24B File Offset: 0x0008C44B
		// (set) Token: 0x06002016 RID: 8214 RVA: 0x0008E25D File Offset: 0x0008C45D
		[Parameter]
		public MultiValuedProperty<Page> Pages
		{
			get
			{
				return (MultiValuedProperty<Page>)this[DetailsTemplateSchema.Pages];
			}
			set
			{
				this[DetailsTemplateSchema.Pages] = value;
				this.validationErrors.Clear();
			}
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x0008E278 File Offset: 0x0008C478
		internal void BlobToPages()
		{
			MultiValuedProperty<Page> multiValuedProperty = this.Pages;
			if (multiValuedProperty.IsReadOnly)
			{
				multiValuedProperty = new MultiValuedProperty<Page>();
			}
			multiValuedProperty = DetailsTemplateAdapter.BlobToPageCollection((byte[])this.propertyBag[DetailsTemplateSchema.TemplateBlob], this.MAPIPropertiesDictionary);
			multiValuedProperty.ResetChangeTracking();
			this.propertyBag.SetField(DetailsTemplateSchema.Pages, multiValuedProperty);
			this.ValidatePages();
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x0008E2D9 File Offset: 0x0008C4D9
		internal void PagesToBlob()
		{
			this[DetailsTemplateSchema.TemplateBlob] = DetailsTemplateAdapter.PageCollectionToBlob(this.Pages, this.MAPIPropertiesDictionary);
			this.Pages.ResetChangeTracking();
			this.ValidatePages();
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x0008E308 File Offset: 0x0008C508
		private void ValidatePages()
		{
			this.validationErrors.Clear();
			if (this.Pages != null && this.Pages.Count != 0)
			{
				string text = this.TemplateType;
				if (text.Equals("Mailbox Agent"))
				{
					return;
				}
				using (MultiValuedProperty<Page>.Enumerator enumerator = this.Pages.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Page page = enumerator.Current;
						int num = -1;
						foreach (DetailsTemplateControl detailsTemplateControl in page.Controls)
						{
							num++;
							string attributeName = detailsTemplateControl.m_AttributeName;
							if (!detailsTemplateControl.ValidateAttribute(this.MAPIPropertiesDictionary))
							{
								this.validationErrors.Add(new PropertyValidationError(DirectoryStrings.InvalidControlAttributeName(detailsTemplateControl.GetType().Name, page.Text, num, attributeName), DetailsTemplateSchema.Pages, this));
							}
							else if (!attributeName.Equals(string.Empty) && !this.MAPIPropertiesDictionary[attributeName][text])
							{
								this.validationErrors.Add(new PropertyValidationError(DirectoryStrings.InvalidControlAttributeForTemplateType(detailsTemplateControl.GetType().Name, page.Text, num, attributeName, text), DetailsTemplateSchema.Pages, this));
							}
						}
					}
					return;
				}
			}
			this.validationErrors.Add(new PropertyValidationError(DirectoryStrings.NoPagesSpecified, DetailsTemplateSchema.Pages, this));
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x0008E494 File Offset: 0x0008C694
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			errors.AddRange(this.validationErrors);
		}

		// Token: 0x04001341 RID: 4929
		private const string LdapName = "displayTemplate";

		// Token: 0x04001342 RID: 4930
		public const string ContactTemplate = "Contact";

		// Token: 0x04001343 RID: 4931
		public const string GroupTemplate = "Group";

		// Token: 0x04001344 RID: 4932
		public const string PublicFolderTemplate = "Public Folder";

		// Token: 0x04001345 RID: 4933
		public const string UserTemplate = "User";

		// Token: 0x04001346 RID: 4934
		public const string SearchTemplate = "Search Dialog";

		// Token: 0x04001347 RID: 4935
		public const string MailboxAgentTemplate = "Mailbox Agent";

		// Token: 0x04001348 RID: 4936
		internal static readonly ADObjectId ContainerId = new ADObjectId("CN=Display-Templates,CN=Addressing");

		// Token: 0x04001349 RID: 4937
		private static readonly DetailsTemplateSchema schemaObject = ObjectSchema.GetInstance<DetailsTemplateSchema>();

		// Token: 0x0400134A RID: 4938
		private static KeyValuePair<string, string>[] templateTypes = new KeyValuePair<string, string>[]
		{
			new KeyValuePair<string, string>("User", "0"),
			new KeyValuePair<string, string>("Group", "1"),
			new KeyValuePair<string, string>("Public Folder", "2"),
			new KeyValuePair<string, string>("Search Dialog", "200"),
			new KeyValuePair<string, string>("Mailbox Agent", "3"),
			new KeyValuePair<string, string>("Contact", "6")
		};

		// Token: 0x0400134B RID: 4939
		private string templateType;

		// Token: 0x0400134C RID: 4940
		private Culture language;

		// Token: 0x0400134D RID: 4941
		private List<ValidationError> validationErrors = new List<ValidationError>();
	}
}
