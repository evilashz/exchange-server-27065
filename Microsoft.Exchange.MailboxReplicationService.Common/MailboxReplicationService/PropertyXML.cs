using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000C3 RID: 195
	[Serializable]
	public sealed class PropertyXML : XMLSerializableBase
	{
		// Token: 0x06000796 RID: 1942 RVA: 0x0000BF46 File Offset: 0x0000A146
		public PropertyXML()
		{
			this.values = new List<PropertyValueBaseXML>();
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0000BF59 File Offset: 0x0000A159
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x0000BF61 File Offset: 0x0000A161
		[XmlAttribute("Name")]
		public string PropertyName { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0000BF6A File Offset: 0x0000A16A
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x0000BF72 File Offset: 0x0000A172
		[XmlAttribute("Class")]
		public string ClassName { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0000BF7B File Offset: 0x0000A17B
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x0000BF8C File Offset: 0x0000A18C
		[XmlAttribute("IsDefault")]
		public string IsDefault
		{
			get
			{
				if (!this.isDefault)
				{
					return null;
				}
				return "true";
			}
			set
			{
				this.isDefault = !string.IsNullOrEmpty(value);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0000BF9D File Offset: 0x0000A19D
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		[XmlElement(typeof(PropertyStringValueXML), ElementName = "Value")]
		[XmlElement(typeof(ADObjectIdXML), ElementName = "DNValue")]
		[XmlElement(typeof(PropertyBinaryValueXML), ElementName = "BinValue")]
		[XmlElement(typeof(OrganizationIdXML), ElementName = "OrgIdValue")]
		public PropertyValueBaseXML[] Values
		{
			get
			{
				return this.values.ToArray();
			}
			set
			{
				this.values.Clear();
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						PropertyValueBaseXML item = value[i];
						this.values.Add(item);
					}
				}
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
		public override string ToString()
		{
			if (this.values.Count == 0)
			{
				return string.Format("{0}: ", this.PropertyName);
			}
			if (this.values.Count != 1)
			{
				return string.Format("{0}: {1}", this.PropertyName, CommonUtils.ConcatEntries<PropertyValueBaseXML>(this.values, null));
			}
			PropertyValueBaseXML propertyValueBaseXML = this.values[0];
			if (propertyValueBaseXML is PropertyBinaryValueXML && this.PropertyName.ToUpper().EndsWith("GUID"))
			{
				return string.Format("{0}: {1}", this.PropertyName, ((PropertyBinaryValueXML)propertyValueBaseXML).AsGuid);
			}
			return string.Format("{0}: {1}", this.PropertyName, propertyValueBaseXML);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0000C09C File Offset: 0x0000A29C
		internal static PropertyXML Create(ProviderPropertyDefinition pdef, object value)
		{
			PropertyXML propertyXML = new PropertyXML();
			propertyXML.PropertyName = pdef.Name;
			propertyXML.ClassName = pdef.Type.FullName;
			propertyXML.values = new List<PropertyValueBaseXML>();
			propertyXML.isDefault = object.Equals(value, pdef.DefaultValue);
			if (pdef.IsMultivalued && value is MultiValuedPropertyBase)
			{
				MultiValuedPropertyBase multiValuedPropertyBase = (MultiValuedPropertyBase)value;
				using (IEnumerator enumerator = ((IEnumerable)multiValuedPropertyBase).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object value2 = enumerator.Current;
						PropertyValueBaseXML propertyValueBaseXML = PropertyValueBaseXML.Create(pdef, value2);
						if (propertyValueBaseXML != null)
						{
							propertyXML.values.Add(propertyValueBaseXML);
						}
					}
					return propertyXML;
				}
			}
			if (value != null)
			{
				PropertyValueBaseXML propertyValueBaseXML2 = PropertyValueBaseXML.Create(pdef, value);
				if (propertyValueBaseXML2 != null)
				{
					propertyXML.values.Add(propertyValueBaseXML2);
				}
			}
			return propertyXML;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0000C178 File Offset: 0x0000A378
		internal bool HasValue()
		{
			foreach (PropertyValueBaseXML propertyValueBaseXML in this.values)
			{
				if (propertyValueBaseXML.HasValue())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		internal bool TryGetValue(ProviderPropertyDefinition pdef, out object result)
		{
			if (this.values.Count == 0)
			{
				result = null;
				return true;
			}
			if (pdef.IsMultivalued)
			{
				List<object> list = new List<object>(this.values.Count);
				foreach (PropertyValueBaseXML propertyValueBaseXML in this.values)
				{
					object item;
					if (propertyValueBaseXML.TryGetValue(pdef, out item))
					{
						list.Add(item);
					}
				}
				MultiValuedPropertyBase multiValuedPropertyBase = null;
				List<object> invalidValues = new List<object>();
				if (!ADValueConvertor.TryCreateGenericMultiValuedProperty(pdef, false, list, invalidValues, null, out multiValuedPropertyBase))
				{
					MrsTracer.Common.Warning("Failed to convert array to MultiValued property {0}", new object[]
					{
						pdef.Name
					});
					result = null;
					return false;
				}
				result = multiValuedPropertyBase;
				return true;
			}
			else
			{
				if (this.values.Count != 1)
				{
					result = null;
					return false;
				}
				return this.values[0].TryGetValue(pdef, out result);
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		internal bool TryApplyChange(ProviderPropertyDefinition pdef, ConfigurableObject targetObject, PropertyUpdateOperation op)
		{
			object obj = null;
			if (this.TryGetValue(pdef, out obj))
			{
				Exception ex = null;
				try
				{
					if (op == PropertyUpdateOperation.Replace)
					{
						object obj2 = targetObject[pdef];
						if ((!object.Equals(obj, pdef.DefaultValue) || obj2 != null) && !object.Equals(obj2, obj))
						{
							if (pdef == ADObjectSchema.ExchangeVersion)
							{
								targetObject.SetExchangeVersion((ExchangeObjectVersion)obj);
							}
							else
							{
								targetObject[pdef] = obj;
							}
						}
					}
					else
					{
						MultiValuedPropertyBase multiValuedPropertyBase = obj as MultiValuedPropertyBase;
						MultiValuedPropertyBase multiValuedPropertyBase2 = targetObject[pdef] as MultiValuedPropertyBase;
						if (multiValuedPropertyBase != null && multiValuedPropertyBase2 != null)
						{
							foreach (object item in ((IEnumerable)multiValuedPropertyBase))
							{
								switch (op)
								{
								case PropertyUpdateOperation.AddValues:
									multiValuedPropertyBase2.Add(item);
									break;
								case PropertyUpdateOperation.RemoveValues:
									multiValuedPropertyBase2.Remove(item);
									break;
								}
							}
						}
					}
					return true;
				}
				catch (DataValidationException ex2)
				{
					ex = ex2;
				}
				catch (ArgumentException ex3)
				{
					ex = ex3;
				}
				catch (FormatException ex4)
				{
					ex = ex4;
				}
				catch (LocalizedException ex5)
				{
					ex = ex5;
				}
				if (ex != null)
				{
					MrsTracer.Common.Warning("Property {0} could not be set to '{1}', error {2}", new object[]
					{
						pdef.Name,
						obj,
						CommonUtils.FullExceptionMessage(ex)
					});
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x0400048D RID: 1165
		private List<PropertyValueBaseXML> values;

		// Token: 0x0400048E RID: 1166
		private bool isDefault;
	}
}
