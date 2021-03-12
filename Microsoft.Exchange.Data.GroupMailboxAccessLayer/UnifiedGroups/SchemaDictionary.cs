using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.UnifiedGroups
{
	// Token: 0x02000055 RID: 85
	[KnownType("GetExplicitKnownTypes")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Directory")]
	public abstract class SchemaDictionary
	{
		// Token: 0x170000BE RID: 190
		public abstract object this[string key]
		{
			get;
			set;
		}

		// Token: 0x060002BA RID: 698
		public abstract bool ContainsKey(string key);

		// Token: 0x060002BB RID: 699
		public abstract bool TryGetValue(string key, out object value);

		// Token: 0x060002BC RID: 700 RVA: 0x000101E0 File Offset: 0x0000E3E0
		public virtual bool TryGetValue<T>(string key, out T value)
		{
			bool result = false;
			object value2 = null;
			value = default(T);
			if (!this.TryGetValue(key, out value2))
			{
				result = false;
			}
			else
			{
				try
				{
					value = (T)((object)Convert.ChangeType(value2, typeof(T), CultureInfo.CurrentCulture));
					result = true;
				}
				catch (Exception)
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060002BD RID: 701
		public abstract IEnumerator<KeyValuePair<string, object>> GetEnumerator();

		// Token: 0x060002BE RID: 702 RVA: 0x00010244 File Offset: 0x0000E444
		public bool IsSupported(Type type)
		{
			return !(type == null) && (type.IsPrimitive || type.IsEnum || SchemaDictionary.WcfDefaultKnownTypes.Contains(type) || SchemaDictionary.ExplicitKnownTypes.Contains(type));
		}

		// Token: 0x060002BF RID: 703
		internal abstract void SetSchemaObject(string key, SchemaObject value);

		// Token: 0x060002C0 RID: 704
		internal abstract SchemaObject GetSchemaObject(string key);

		// Token: 0x060002C1 RID: 705
		internal abstract bool TryGetSchemaObject(string key, out SchemaObject schemaObject);

		// Token: 0x060002C2 RID: 706
		internal abstract IDictionary<string, object> GetDictionaryForSerialization();

		// Token: 0x060002C3 RID: 707
		internal abstract void InternalSet(string key, object value);

		// Token: 0x060002C4 RID: 708 RVA: 0x0001027B File Offset: 0x0000E47B
		internal static List<Type> GetExplicitKnownTypes()
		{
			return SchemaDictionary.ExplicitKnownTypes;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010282 File Offset: 0x0000E482
		internal static IEnumerable<Type> GetNonPrimitiveSupportedTypes()
		{
			return SchemaDictionary.WcfDefaultKnownTypes.Concat(SchemaDictionary.ExplicitKnownTypes);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00010293 File Offset: 0x0000E493
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0001029B File Offset: 0x0000E49B
		[DataMember]
		internal ConcurrentDictionary<string, object> InternalStorage
		{
			get
			{
				return this._Properties;
			}
			set
			{
				this._Properties = value;
			}
		}

		// Token: 0x04000177 RID: 375
		private static readonly List<Type> WcfDefaultKnownTypes = new List<Type>
		{
			typeof(DateTime),
			typeof(string),
			typeof(Guid)
		};

		// Token: 0x04000178 RID: 376
		private static readonly List<Type> ExplicitKnownTypes = new List<Type>
		{
			typeof(List<string>),
			typeof(StringCollection),
			typeof(List<Guid>),
			typeof(SchemaDictionary<Relation>),
			typeof(Relation)
		};

		// Token: 0x04000179 RID: 377
		protected ConcurrentDictionary<string, object> _Properties = new ConcurrentDictionary<string, object>(StringComparer.OrdinalIgnoreCase);
	}
}
