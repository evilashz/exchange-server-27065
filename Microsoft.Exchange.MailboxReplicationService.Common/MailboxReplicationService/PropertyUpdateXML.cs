using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000185 RID: 389
	public sealed class PropertyUpdateXML : XMLSerializableBase
	{
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x0002148D File Offset: 0x0001F68D
		// (set) Token: 0x06000E92 RID: 3730 RVA: 0x00021495 File Offset: 0x0001F695
		[XmlElement("Operation")]
		public PropertyUpdateOperation Operation
		{
			get
			{
				return this.operation;
			}
			set
			{
				this.operation = value;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0002149E File Offset: 0x0001F69E
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x000214A6 File Offset: 0x0001F6A6
		[XmlElement("Property")]
		public PropertyXML Property
		{
			get
			{
				return this.property;
			}
			set
			{
				this.property = value;
			}
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000214B0 File Offset: 0x0001F6B0
		internal static void Add(List<PropertyUpdateXML> updates, ProviderPropertyDefinition pdef, object value, PropertyUpdateOperation op)
		{
			updates.Add(new PropertyUpdateXML
			{
				Operation = op,
				Property = PropertyXML.Create(pdef, value)
			});
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x000214E0 File Offset: 0x0001F6E0
		internal static void Apply(ICollection<PropertyUpdateXML> updates, ConfigurableObject targetObject)
		{
			if (updates == null)
			{
				return;
			}
			foreach (PropertyUpdateXML propertyUpdateXML in updates)
			{
				ProviderPropertyDefinition providerPropertyDefinition = null;
				foreach (PropertyDefinition propertyDefinition in targetObject.ObjectSchema.AllProperties)
				{
					if (string.Compare(propertyDefinition.Name, propertyUpdateXML.Property.PropertyName, StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						providerPropertyDefinition = (propertyDefinition as ProviderPropertyDefinition);
						break;
					}
				}
				if (providerPropertyDefinition == null)
				{
					MrsTracer.Common.Warning("Ignoring property update for '{0}', no such property found in the schema.", new object[]
					{
						propertyUpdateXML.Property.PropertyName
					});
				}
				else
				{
					propertyUpdateXML.Property.TryApplyChange(providerPropertyDefinition, targetObject, propertyUpdateXML.Operation);
				}
			}
		}

		// Token: 0x0400082C RID: 2092
		private PropertyUpdateOperation operation;

		// Token: 0x0400082D RID: 2093
		private PropertyXML property;
	}
}
