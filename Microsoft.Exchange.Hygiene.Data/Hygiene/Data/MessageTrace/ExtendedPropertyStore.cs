using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000144 RID: 324
	internal class ExtendedPropertyStore<T> : IExtendedPropertyStore<T> where T : PropertyBase
	{
		// Token: 0x06000C84 RID: 3204 RVA: 0x0002704A File Offset: 0x0002524A
		public ExtendedPropertyStore()
		{
			this.propertyStore = new Dictionary<string, Dictionary<string, T>>(StringComparer.OrdinalIgnoreCase);
			this.propertiesList = new List<T>();
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0002706D File Offset: 0x0002526D
		public int ExtendedPropertiesCount
		{
			get
			{
				return this.propertiesList.Count;
			}
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00027098 File Offset: 0x00025298
		public bool TryGetExtendedProperty(string nameSpace, string name, out T extendedProperty)
		{
			if (nameSpace == null)
			{
				throw new ArgumentNullException("nameSpace");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Dictionary<string, T> dictionary = null;
			if (this.propertyStore.TryGetValue(nameSpace, out dictionary) && dictionary.TryGetValue(name, out extendedProperty))
			{
				if (!MessageTraceCollapsedProperty.IsCollapsableProperty(nameSpace, name))
				{
					return true;
				}
				IEnumerable<PropertyBase> source;
				if (ExtendedPropertyStore<T>.TryGetCollapsedProperties(dictionary, nameSpace, name, out source))
				{
					extendedProperty = (source.LastOrDefault((PropertyBase p) => string.Equals(p.PropertyName, name, StringComparison.OrdinalIgnoreCase)) as T);
					if (extendedProperty != null)
					{
						return true;
					}
				}
			}
			extendedProperty = default(T);
			return false;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00027174 File Offset: 0x00025374
		public T GetExtendedProperty(string nameSpace, string name)
		{
			if (nameSpace == null)
			{
				throw new ArgumentNullException("nameSpace");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			T result = default(T);
			Dictionary<string, T> dictionary = this.propertyStore[nameSpace];
			if (MessageTraceCollapsedProperty.IsCollapsableProperty(nameSpace, name))
			{
				IEnumerable<PropertyBase> source;
				if (!ExtendedPropertyStore<T>.TryGetCollapsedProperties(dictionary, nameSpace, name, out source))
				{
					throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, "The key '{0}' was not found in the property bag", new object[]
					{
						name
					}));
				}
				result = (T)((object)source.LastOrDefault((PropertyBase p) => string.Equals(p.PropertyName, name, StringComparison.OrdinalIgnoreCase)));
			}
			else
			{
				result = dictionary[name];
			}
			return result;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00027243 File Offset: 0x00025443
		public IEnumerable<T> GetExtendedPropertiesEnumerable()
		{
			return this.propertiesList;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00027258 File Offset: 0x00025458
		public void AddExtendedProperty(T extendedProperty)
		{
			if (extendedProperty == null)
			{
				throw new ArgumentNullException("extendedProperty");
			}
			Dictionary<string, T> orAdd = this.propertyStore.GetOrAdd(extendedProperty.Namespace, () => new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase));
			if (MessageTraceCollapsedProperty.IsCollapsableProperty(extendedProperty.Namespace, extendedProperty.PropertyName))
			{
				byte[] array = null;
				T t;
				if (orAdd.TryGetValue(MessageTraceCollapsedProperty.PropertyDefinition.Name, out t))
				{
					array = Convert.FromBase64String(t.PropertyValueBlob.Value);
				}
				array = MessageTraceCollapsedProperty.Collapse(array, extendedProperty.Namespace, extendedProperty);
				if (t == null)
				{
					t = extendedProperty;
					t.PropertyName = MessageTraceCollapsedProperty.PropertyDefinition.Name;
					t.PropertyValueGuid = Guid.Empty;
					t.PropertyValueInteger = null;
					t.PropertyValueString = null;
					t.PropertyValueDatetime = null;
					t.PropertyValueDecimal = null;
					t.PropertyValueBit = null;
					t.PropertyValueLong = null;
					orAdd[extendedProperty.PropertyName] = t;
					t.PropertyIndex = this.propertiesList.Count;
					this.propertiesList.Add(extendedProperty);
				}
				t.PropertyValueBlob = new BlobType(Convert.ToBase64String(array));
				return;
			}
			orAdd[extendedProperty.PropertyName] = extendedProperty;
			extendedProperty.PropertyIndex = this.propertiesList.Count;
			this.propertiesList.Add(extendedProperty);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002747C File Offset: 0x0002567C
		private static bool TryGetCollapsedProperties(IDictionary<string, T> nameToPropDict, string nameSpace, string name, out IEnumerable<PropertyBase> expandedProperties)
		{
			expandedProperties = null;
			T t;
			bool result;
			if (result = nameToPropDict.TryGetValue(MessageTraceCollapsedProperty.PropertyDefinition.Name, out t))
			{
				byte[] data = Convert.FromBase64String(t.PropertyValueBlob.Value);
				expandedProperties = MessageTraceCollapsedProperty.Expand<MessageEventProperty>(data, name, () => new MessageEventProperty(nameSpace, name, false));
			}
			return result;
		}

		// Token: 0x04000645 RID: 1605
		private Dictionary<string, Dictionary<string, T>> propertyStore;

		// Token: 0x04000646 RID: 1606
		private List<T> propertiesList;
	}
}
