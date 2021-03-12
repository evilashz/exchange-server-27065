using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000595 RID: 1429
	[XmlType(TypeName = "MailboxProvisioningAttributes")]
	[Serializable]
	public sealed class MailboxProvisioningAttributes : XMLSerializableBase
	{
		// Token: 0x06004295 RID: 17045 RVA: 0x000FAE4D File Offset: 0x000F904D
		public MailboxProvisioningAttributes()
		{
			this.propertyBag = new SimpleProviderPropertyBag();
			this.propertyBag.SetObjectVersion(ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x000FAE70 File Offset: 0x000F9070
		public MailboxProvisioningAttributes(MailboxProvisioningAttribute[] attributes)
		{
			this.propertyBag = new SimpleProviderPropertyBag();
			this.propertyBag.SetObjectVersion(ExchangeObjectVersion.Exchange2010);
			this.Attributes = attributes;
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06004297 RID: 17047 RVA: 0x000FAE9A File Offset: 0x000F909A
		public static IEnumerable<string> PermanentAttributeNames
		{
			get
			{
				return MailboxProvisioningAttributes.permanentReadOnlyNames;
			}
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06004298 RID: 17048 RVA: 0x000FAEA4 File Offset: 0x000F90A4
		// (set) Token: 0x06004299 RID: 17049 RVA: 0x000FAF58 File Offset: 0x000F9158
		[XmlArray("Attributes")]
		[XmlArrayItem("MailboxProvisioningAttribute")]
		public MailboxProvisioningAttribute[] Attributes
		{
			get
			{
				List<MailboxProvisioningAttribute> list = new List<MailboxProvisioningAttribute>();
				foreach (PropertyDefinition propertyDefinition in ObjectSchema.GetInstance<MailboxProvisioningAttributesSchema>().AllFilterableProperties)
				{
					object obj = this.propertyBag[propertyDefinition];
					if (obj != null)
					{
						list.Add(new MailboxProvisioningAttribute
						{
							Key = propertyDefinition.Name,
							Value = (string)obj
						});
					}
				}
				return list.ToArray();
			}
			set
			{
				this.propertyBag.Clear();
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						MailboxProvisioningAttribute attribute = value[i];
						PropertyDefinition key = ObjectSchema.GetInstance<MailboxProvisioningAttributesSchema>().AllFilterableProperties.First((PropertyDefinition x) => x.Name.Equals(attribute.Key));
						this.propertyBag[key] = attribute.Value;
					}
				}
			}
		}

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x0600429A RID: 17050 RVA: 0x000FAFD1 File Offset: 0x000F91D1
		internal IReadOnlyPropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x000FAFDC File Offset: 0x000F91DC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MailboxProvisioningAttribute mailboxProvisioningAttribute in this.Attributes)
			{
				stringBuilder.AppendFormat("{0};", mailboxProvisioningAttribute.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x000FB048 File Offset: 0x000F9248
		public static MailboxProvisioningAttributes Parse(string attributes)
		{
			if (string.IsNullOrEmpty(attributes) || string.IsNullOrWhiteSpace(attributes))
			{
				return null;
			}
			string[] array = attributes.Split(MailboxProvisioningAttributes.provisoningAttributeDelimiter, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 0 || array.Length > 30)
			{
				throw new InvalidMailboxProvisioningAttributeException(DirectoryStrings.ErrorInvalidMailboxProvisioningAttributes(30));
			}
			HashSet<string> hashSet = new HashSet<string>();
			List<MailboxProvisioningAttribute> list = new List<MailboxProvisioningAttribute>();
			ReadOnlyCollection<PropertyDefinition> allFilterableProperties = ObjectSchema.GetInstance<MailboxProvisioningAttributesSchema>().AllFilterableProperties;
			for (int i = 0; i < array.Length; i++)
			{
				MailboxProvisioningAttribute attribute = MailboxProvisioningAttribute.Parse(array[i]);
				if (hashSet.Contains(attribute.Key))
				{
					throw new InvalidMailboxProvisioningAttributeException(DirectoryStrings.ErrorDuplicateKeyInMailboxProvisioningAttributes(attribute.Key));
				}
				if (!allFilterableProperties.Any((PropertyDefinition x) => x.Name.Equals(attribute.Key)))
				{
					string validKeys = string.Join(",", from x in allFilterableProperties
					select x.Name);
					throw new ProvisioningAttributeDoesNotMatchSchemaException(attribute.Key, validKeys);
				}
				if (MailboxProvisioningAttributes.PermanentAttributeNames.Contains(attribute.Key))
				{
					throw new CannotSetPermanentAttributesException(string.Join(",", MailboxProvisioningAttributes.PermanentAttributeNames));
				}
				list.Add(attribute);
			}
			return new MailboxProvisioningAttributes(list.ToArray());
		}

		// Token: 0x04002D4B RID: 11595
		private const int MaxAllowedAttributes = 30;

		// Token: 0x04002D4C RID: 11596
		private static char[] provisoningAttributeDelimiter = new char[]
		{
			';'
		};

		// Token: 0x04002D4D RID: 11597
		private SimpleProviderPropertyBag propertyBag;

		// Token: 0x04002D4E RID: 11598
		private static readonly HashSet<string> permanentReadOnlyNames = new HashSet<string>(new string[]
		{
			MailboxProvisioningAttributesSchema.DagName.Name,
			MailboxProvisioningAttributesSchema.ServerName.Name,
			MailboxProvisioningAttributesSchema.DatabaseName.Name
		});
	}
}
