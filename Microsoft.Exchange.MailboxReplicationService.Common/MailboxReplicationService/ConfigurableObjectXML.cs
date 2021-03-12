using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000C4 RID: 196
	[Serializable]
	public sealed class ConfigurableObjectXML : XMLSerializableBase
	{
		// Token: 0x060007A4 RID: 1956 RVA: 0x0000C460 File Offset: 0x0000A660
		static ConfigurableObjectXML()
		{
			ConfigurableObjectXML.PropertiesNotToSerialize.Add(ADRecipientSchema.Certificate, null);
			ConfigurableObjectXML.PropertiesNotToSerialize.Add(ADRecipientSchema.SMimeCertificate, null);
			ConfigurableObjectXML.PropertiesNotToSerialize.Add(ADUserSchema.DirectReports, null);
			ConfigurableObjectXML.PropertiesNotToSerialize.Add(ADRecipientSchema.AcceptMessagesOnlyFrom, null);
			ConfigurableObjectXML.PropertiesNotToSerialize.Add(ADRecipientSchema.RejectMessagesFrom, null);
			ConfigurableObjectXML.PropertiesNotToSerialize.Add(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers, null);
			ConfigurableObjectXML.PropertiesNotToSerialize.Add(ADRecipientSchema.RejectMessagesFromDLMembers, null);
			ConfigurableObjectXML.PropertiesNotToSerialize.Add(ADRecipientSchema.AddressListMembership, null);
			ConfigurableObjectXML.PropertiesNotToSerialize.Add(ADRecipientSchema.GrantSendOnBehalfTo, null);
			ConfigurableObjectXML.PropertiesNotToSerialize.Add(ADMailboxRecipientSchema.DelegateListBL, null);
			ConfigurableObjectXML.PropertiesNotToSerialize.Add(ADMailboxRecipientSchema.DelegateListLink, null);
			ConfigurableObjectXML.PropertiesNotToSerialize.Add(ADUserSchema.SharingPartnerIdentities, null);
			ConfigurableObjectXML.PropertiesNotToSerialize.Add(ADUserSchema.SharingAnonymousIdentities, null);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000C547 File Offset: 0x0000A747
		public ConfigurableObjectXML()
		{
			this.properties = new Dictionary<string, PropertyXML>();
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0000C55A File Offset: 0x0000A75A
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0000C562 File Offset: 0x0000A762
		[XmlAttribute("Class")]
		public string ClassName { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0000C56C File Offset: 0x0000A76C
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		[XmlElement("Property")]
		public PropertyXML[] Props
		{
			get
			{
				PropertyXML[] array = new PropertyXML[this.properties.Count];
				this.properties.Values.CopyTo(array, 0);
				return array;
			}
			set
			{
				this.properties.Clear();
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						PropertyXML propertyXML = value[i];
						this.properties[propertyXML.PropertyName] = propertyXML;
					}
				}
			}
		}

		// Token: 0x170002A5 RID: 677
		internal object this[string propName]
		{
			get
			{
				PropertyXML propertyXML;
				if (!this.properties.TryGetValue(propName, out propertyXML))
				{
					return null;
				}
				PropertyValueBaseXML[] values = propertyXML.Values;
				if (values.Length == 0)
				{
					return null;
				}
				if (values.Length == 1)
				{
					return values[0].RawValue;
				}
				object[] array = new object[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					array[i] = values[i].RawValue;
				}
				return array;
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0000C644 File Offset: 0x0000A844
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			SortedList<string, PropertyXML> sortedList = new SortedList<string, PropertyXML>();
			foreach (PropertyXML propertyXML in this.properties.Values)
			{
				if (propertyXML.HasValue())
				{
					sortedList.Add(propertyXML.PropertyName, propertyXML);
				}
			}
			foreach (PropertyXML propertyXML2 in sortedList.Values)
			{
				stringBuilder.AppendFormat("{0}; ", propertyXML2.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0000C70C File Offset: 0x0000A90C
		internal static ConfigurableObjectXML Create(ConfigurableObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			ConfigurableObjectXML configurableObjectXML = new ConfigurableObjectXML();
			configurableObjectXML.ClassName = obj.GetType().Name;
			foreach (PropertyDefinition propertyDefinition in obj.ObjectSchema.AllProperties)
			{
				ProviderPropertyDefinition providerPropertyDefinition = propertyDefinition as ProviderPropertyDefinition;
				if (providerPropertyDefinition != null && !ConfigurableObjectXML.PropertiesNotToSerialize.ContainsKey(propertyDefinition))
				{
					object value = obj[providerPropertyDefinition];
					PropertyXML propertyXML = PropertyXML.Create(providerPropertyDefinition, value);
					if (propertyXML != null)
					{
						configurableObjectXML.properties[providerPropertyDefinition.Name] = propertyXML;
					}
				}
			}
			return configurableObjectXML;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0000C7BC File Offset: 0x0000A9BC
		internal static string Serialize<T>(T obj) where T : ConfigurableObject
		{
			ConfigurableObjectXML configurableObjectXML = ConfigurableObjectXML.Create(obj);
			if (configurableObjectXML == null)
			{
				return null;
			}
			return configurableObjectXML.Serialize(false);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		internal static T Deserialize<T>(string xml) where T : ConfigurableObject, new()
		{
			ConfigurableObjectXML configurableObjectXML = ConfigurableObjectXML.Parse(xml);
			if (configurableObjectXML == null)
			{
				return default(T);
			}
			T t = Activator.CreateInstance<T>();
			configurableObjectXML.Populate(t);
			return t;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0000C818 File Offset: 0x0000AA18
		internal static ConfigurableObjectXML Parse(string serializedData)
		{
			return XMLSerializableBase.Deserialize<ConfigurableObjectXML>(serializedData, false);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0000C824 File Offset: 0x0000AA24
		internal void Populate(ConfigurableObject result)
		{
			if (result == null)
			{
				return;
			}
			foreach (PropertyDefinition propertyDefinition in result.ObjectSchema.AllProperties)
			{
				ProviderPropertyDefinition providerPropertyDefinition = propertyDefinition as ProviderPropertyDefinition;
				PropertyXML propertyXML;
				if (providerPropertyDefinition != null && !providerPropertyDefinition.IsCalculated && !providerPropertyDefinition.IsReadOnly && this.properties.TryGetValue(providerPropertyDefinition.Name, out propertyXML))
				{
					propertyXML.TryApplyChange(providerPropertyDefinition, result, PropertyUpdateOperation.Replace);
				}
			}
		}

		// Token: 0x04000491 RID: 1169
		private static readonly Dictionary<PropertyDefinition, object> PropertiesNotToSerialize = new Dictionary<PropertyDefinition, object>();

		// Token: 0x04000492 RID: 1170
		private Dictionary<string, PropertyXML> properties;
	}
}
