using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.UnifiedGroups
{
	// Token: 0x02000056 RID: 86
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Directory")]
	public class SchemaDictionary<T> : SchemaDictionary
	{
		// Token: 0x060002CA RID: 714 RVA: 0x00010361 File Offset: 0x0000E561
		private SchemaDictionary()
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001036C File Offset: 0x0000E56C
		internal SchemaDictionary(T owner, string bagName, Action<string> propertyKeyValidator, Action<string, object> propertyValueValidator, Func<string, SchemaDictionary, object> propertyValueGetterTransfom, Func<string, object, bool, object> propertyValueSetterTransform)
		{
			this._owner = owner;
			this._PropertyKeyValidator = propertyKeyValidator;
			this._PropertyValueValidator = propertyValueValidator;
			this._PropertyValueGetterTransform = propertyValueGetterTransfom;
			this._PropertyValueSetterTransform = propertyValueSetterTransform;
			if (!SchemaDictionary<T>._SchemaDictionaryNameToAccessDelegates.TryGetValue(bagName, out this._accessDelegates))
			{
				this._accessDelegates = SchemaDictionary<T>._emptyAccessDelegates;
			}
		}

		// Token: 0x170000C0 RID: 192
		public override object this[string key]
		{
			get
			{
				this.ValidateKey(key);
				Func<T, object> func;
				if (this._accessDelegates._Getters.TryGetValue(key, out func))
				{
					return func(this._owner);
				}
				if (this._PropertyValueGetterTransform != null)
				{
					return this._PropertyValueGetterTransform(key, this);
				}
				return this._Properties[key];
			}
			set
			{
				if (this._accessDelegates._ForbiddenSetters.Contains(key))
				{
					throw new FieldAccessException(key + " is not accessible because its setter is protected or private.");
				}
				this.ValidateValue(key, value);
				this.InternalSet(key, value);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00010452 File Offset: 0x0000E652
		public override bool ContainsKey(string key)
		{
			return this._Properties.ContainsKey(key);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00010460 File Offset: 0x0000E660
		public override bool TryGetValue(string key, out object value)
		{
			if (this._Properties.TryGetValue(key, out value))
			{
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000106F0 File Offset: 0x0000E8F0
		public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			foreach (KeyValuePair<string, object> pair in this._Properties)
			{
				IDictionary<string, Func<T, object>> getters = this._accessDelegates._Getters;
				KeyValuePair<string, object> keyValuePair = pair;
				if (!getters.ContainsKey(keyValuePair.Key))
				{
					KeyValuePair<string, object> keyValuePair2 = pair;
					string key = keyValuePair2.Key;
					KeyValuePair<string, object> keyValuePair3 = pair;
					yield return new KeyValuePair<string, object>(key, this[keyValuePair3.Key]);
				}
			}
			foreach (KeyValuePair<string, Func<T, object>> getter in this._accessDelegates._Getters)
			{
				KeyValuePair<string, Func<T, object>> keyValuePair4 = getter;
				string key2 = keyValuePair4.Key;
				KeyValuePair<string, Func<T, object>> keyValuePair5 = getter;
				yield return new KeyValuePair<string, object>(key2, keyValuePair5.Value(this._owner));
			}
			yield break;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0001070C File Offset: 0x0000E90C
		protected static Dictionary<string, Func<T, object>> _AllGetters
		{
			get
			{
				return SchemaDictionary<T>._allGetters;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00010713 File Offset: 0x0000E913
		protected static Dictionary<string, Action<T, object>> _AllSetters
		{
			get
			{
				return SchemaDictionary<T>._allSetters;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0001071A File Offset: 0x0000E91A
		private static Dictionary<string, SchemaDictionary<T>.AccessDelegates> _SchemaDictionaryNameToAccessDelegates
		{
			get
			{
				return SchemaDictionary<T>._schemaDictionaryNameToAccessDelegates;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00010721 File Offset: 0x0000E921
		protected void ValidateKey(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentException("key is null or empty", "key");
			}
			if (this._PropertyKeyValidator != null)
			{
				this._PropertyKeyValidator(key);
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00010758 File Offset: 0x0000E958
		protected void ValidateValue(string key, object value)
		{
			if (value != null && !base.IsSupported(value.GetType()))
			{
				throw new ArgumentException("value is of a unsupported Type. The supported types are the primitive ones plus " + string.Join(", ", from type in SchemaDictionary.GetNonPrimitiveSupportedTypes()
				select type.ToString()) + ".", "value");
			}
			if (this._PropertyValueValidator != null)
			{
				this._PropertyValueValidator(key, value);
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000107E8 File Offset: 0x0000E9E8
		internal override void SetSchemaObject(string key, SchemaObject value)
		{
			this.ValidateKey(key);
			this._Properties.AddOrUpdate(key, value, (string existingKey, object existingVale) => value);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00010828 File Offset: 0x0000EA28
		internal override SchemaObject GetSchemaObject(string key)
		{
			this.ValidateKey(key);
			return this._Properties[key] as SchemaObject;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00010844 File Offset: 0x0000EA44
		internal override bool TryGetSchemaObject(string key, out SchemaObject schemaObject)
		{
			schemaObject = null;
			this.ValidateKey(key);
			object obj;
			if (this._Properties.TryGetValue(key, out obj))
			{
				schemaObject = (obj as SchemaObject);
				return true;
			}
			return false;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00010878 File Offset: 0x0000EA78
		internal override IDictionary<string, object> GetDictionaryForSerialization()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (KeyValuePair<string, object> keyValuePair in this._Properties)
			{
				if (!dictionary.ContainsKey(keyValuePair.Key))
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			foreach (KeyValuePair<string, Func<T, object>> keyValuePair2 in this._accessDelegates._GettersForSerialization)
			{
				if (!dictionary.ContainsKey(keyValuePair2.Key))
				{
					dictionary.Add(keyValuePair2.Key, keyValuePair2.Value(this._owner));
				}
			}
			return dictionary;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00010958 File Offset: 0x0000EB58
		internal bool IsReadOnlyProperty(string key)
		{
			Func<T, object> func;
			Action<T, object> action;
			return this._accessDelegates._Getters.TryGetValue(key, out func) && !this._accessDelegates._Setters.TryGetValue(key, out action);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000109A4 File Offset: 0x0000EBA4
		internal override void InternalSet(string key, object value)
		{
			this.ValidateKey(key);
			Action<T, object> action;
			if (this._accessDelegates._Setters.TryGetValue(key, out action))
			{
				action(this._owner, value);
				return;
			}
			Func<T, object> func;
			if (this._accessDelegates._Getters.TryGetValue(key, out func))
			{
				throw new FieldAccessException(string.Format(CultureInfo.InvariantCulture, "Property '{0}' has a public getter but not setter", new object[]
				{
					key
				}));
			}
			if (this._PropertyValueSetterTransform != null)
			{
				this._PropertyValueSetterTransform(key, value, true);
				return;
			}
			this._Properties.AddOrUpdate(key, value, (string existingKey, object existingVale) => value);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		internal static void BuildGetterSetterDictionaries(IDictionary<string, SchemaDictionary<T>.AccessDelegates> schemaDictionaryNameToAccessDelegates, IDictionary<string, Func<T, object>> allGetters, IDictionary<string, Action<T, object>> allSetters)
		{
			foreach (PropertyInfo propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				if (!SchemaDictionary<T>.ShouldIgnoreProperty(propertyInfo))
				{
					Func<T, object> func = SchemaDictionary<T>.MakeGetterDelegate(propertyInfo, true);
					if (func != null)
					{
						allGetters.Add(propertyInfo.Name, func);
					}
					Action<T, object> action = SchemaDictionary<T>.MakeSetterDelegate(propertyInfo, true);
					if (action != null)
					{
						allSetters.Add(propertyInfo.Name, action);
					}
					string schemaDictionaryName = SchemaDictionary<T>.GetSchemaDictionaryName(propertyInfo);
					SchemaDictionary<T>.AccessDelegates accessDelegates = null;
					if (!schemaDictionaryNameToAccessDelegates.TryGetValue(schemaDictionaryName, out accessDelegates))
					{
						accessDelegates = new SchemaDictionary<T>.AccessDelegates();
						schemaDictionaryNameToAccessDelegates.Add(schemaDictionaryName, accessDelegates);
					}
					func = SchemaDictionary<T>.MakeGetterDelegate(propertyInfo, false);
					if (func != null)
					{
						accessDelegates._Getters.Add(propertyInfo.Name, func);
					}
					if (SchemaDictionary<T>.ShouldSerialize(propertyInfo))
					{
						accessDelegates._GettersForSerialization.Add(propertyInfo.Name, func);
					}
					action = SchemaDictionary<T>.MakeSetterDelegate(propertyInfo, false);
					if (action != null)
					{
						accessDelegates._Setters.Add(propertyInfo.Name, action);
					}
					MethodInfo setMethod = propertyInfo.GetSetMethod(true);
					if (setMethod != null && (setMethod.IsPrivate || setMethod.IsFamily))
					{
						accessDelegates._ForbiddenSetters.Add(propertyInfo.Name);
					}
				}
			}
			foreach (PropertyInfo propertyInfo2 in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.NonPublic))
			{
				Func<T, object> func2 = SchemaDictionary<T>.MakeGetterDelegate(propertyInfo2, true);
				if (func2 != null)
				{
					allGetters.Add(propertyInfo2.Name, func2);
				}
				Action<T, object> action2 = SchemaDictionary<T>.MakeSetterDelegate(propertyInfo2, true);
				if (action2 != null)
				{
					allSetters.Add(propertyInfo2.Name, action2);
				}
				MethodInfo setMethod2 = propertyInfo2.GetSetMethod(true);
				if (setMethod2 != null)
				{
					foreach (SchemaDictionary<T>.AccessDelegates accessDelegates2 in schemaDictionaryNameToAccessDelegates.Values)
					{
						accessDelegates2._ForbiddenSetters.Add(propertyInfo2.Name);
					}
				}
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00010CA0 File Offset: 0x0000EEA0
		private static string GetSchemaDictionaryName(PropertyInfo property)
		{
			return string.Empty;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00010CA7 File Offset: 0x0000EEA7
		private static bool ShouldIgnoreProperty(PropertyInfo property)
		{
			return true;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00010CAA File Offset: 0x0000EEAA
		private static bool ShouldSerialize(PropertyInfo property)
		{
			return true;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00010CB0 File Offset: 0x0000EEB0
		private static Action<T, object> MakeSetterDelegate(PropertyInfo property, bool includePrivate = false)
		{
			MethodInfo setMethod = property.GetSetMethod(true);
			if (setMethod != null && (includePrivate || !setMethod.IsPrivate) && setMethod.GetParameters().Length == 1)
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(T));
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object));
				MethodCallExpression body = Expression.Call(parameterExpression, setMethod, new Expression[]
				{
					Expression.Convert(parameterExpression2, property.PropertyType)
				});
				return Expression.Lambda<Action<T, object>>(body, new ParameterExpression[]
				{
					parameterExpression,
					parameterExpression2
				}).Compile();
			}
			return null;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00010D48 File Offset: 0x0000EF48
		private static Func<T, object> MakeGetterDelegate(PropertyInfo property, bool includePrivate = false)
		{
			MethodInfo getMethod = property.GetGetMethod(true);
			if (getMethod != null && (includePrivate || !getMethod.IsPrivate) && getMethod.GetParameters().Length == 0)
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(T));
				UnaryExpression body = Expression.Convert(Expression.Call(parameterExpression, getMethod), typeof(object));
				return Expression.Lambda<Func<T, object>>(body, new ParameterExpression[]
				{
					parameterExpression
				}).Compile();
			}
			return null;
		}

		// Token: 0x0400017A RID: 378
		private static Dictionary<string, Func<T, object>> _allGetters = new Dictionary<string, Func<T, object>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400017B RID: 379
		private static Dictionary<string, Action<T, object>> _allSetters = new Dictionary<string, Action<T, object>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400017C RID: 380
		protected T _owner;

		// Token: 0x0400017D RID: 381
		private SchemaDictionary<T>.AccessDelegates _accessDelegates;

		// Token: 0x0400017E RID: 382
		private Action<string> _PropertyKeyValidator;

		// Token: 0x0400017F RID: 383
		private Action<string, object> _PropertyValueValidator;

		// Token: 0x04000180 RID: 384
		private Func<string, SchemaDictionary, object> _PropertyValueGetterTransform;

		// Token: 0x04000181 RID: 385
		private Func<string, object, bool, object> _PropertyValueSetterTransform;

		// Token: 0x04000182 RID: 386
		private static Dictionary<string, SchemaDictionary<T>.AccessDelegates> _schemaDictionaryNameToAccessDelegates = new Dictionary<string, SchemaDictionary<T>.AccessDelegates>();

		// Token: 0x04000183 RID: 387
		private static SchemaDictionary<T>.AccessDelegates _emptyAccessDelegates = new SchemaDictionary<T>.AccessDelegates();

		// Token: 0x02000057 RID: 87
		internal class AccessDelegates
		{
			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x060002E4 RID: 740 RVA: 0x00010DBB File Offset: 0x0000EFBB
			internal IDictionary<string, Action<T, object>> _Setters
			{
				get
				{
					return this._setters;
				}
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x060002E5 RID: 741 RVA: 0x00010DC3 File Offset: 0x0000EFC3
			internal IDictionary<string, Func<T, object>> _Getters
			{
				get
				{
					return this._getters;
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x060002E6 RID: 742 RVA: 0x00010DCB File Offset: 0x0000EFCB
			internal IDictionary<string, Func<T, object>> _GettersForSerialization
			{
				get
				{
					return this._gettersForSerialization;
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x060002E7 RID: 743 RVA: 0x00010DD3 File Offset: 0x0000EFD3
			internal ICollection<string> _ForbiddenSetters
			{
				get
				{
					return this._forbiddenSetters;
				}
			}

			// Token: 0x04000185 RID: 389
			private Dictionary<string, Action<T, object>> _setters = new Dictionary<string, Action<T, object>>(StringComparer.OrdinalIgnoreCase);

			// Token: 0x04000186 RID: 390
			private Dictionary<string, Func<T, object>> _getters = new Dictionary<string, Func<T, object>>(StringComparer.OrdinalIgnoreCase);

			// Token: 0x04000187 RID: 391
			private Dictionary<string, Func<T, object>> _gettersForSerialization = new Dictionary<string, Func<T, object>>(StringComparer.OrdinalIgnoreCase);

			// Token: 0x04000188 RID: 392
			private HashSet<string> _forbiddenSetters = new HashSet<string>();
		}
	}
}
