using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000A3 RID: 163
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PropertyDefinitionToPropTagCollection : ICollection<PropTag>, IEnumerable<PropTag>, IEnumerable
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0004FC4C File Offset: 0x0004DE4C
		private static HashSet<string> PromotableInternetHeaders
		{
			get
			{
				if (PropertyDefinitionToPropTagCollection.promotableInternetHeaders == null)
				{
					lock (PropertyDefinitionToPropTagCollection.lockObject)
					{
						PropertyDefinitionToPropTagCollection.promotableInternetHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
						foreach (FieldInfo fieldInfo in ReflectionHelper.AggregateTypeHierarchy<FieldInfo>(typeof(InternalSchema), new AggregateType<FieldInfo>(ReflectionHelper.AggregateStaticFields)))
						{
							object value = fieldInfo.GetValue(null);
							GuidNamePropertyDefinition guidNamePropertyDefinition = value as GuidNamePropertyDefinition;
							if (guidNamePropertyDefinition != null && guidNamePropertyDefinition.Guid == WellKnownPropertySet.InternetHeaders)
							{
								PropertyDefinitionToPropTagCollection.promotableInternetHeaders.Add(guidNamePropertyDefinition.PropertyName);
							}
						}
					}
				}
				return PropertyDefinitionToPropTagCollection.promotableInternetHeaders;
			}
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0004FD2C File Offset: 0x0004DF2C
		public PropertyDefinitionToPropTagCollection(MapiProp mapiProp, StoreSession storeSession, bool allowUnresolvedHeaders, bool allowCreate, bool allowCreateHeaders, IEnumerable<NativeStorePropertyDefinition> propertyDefinitions)
		{
			this.mapiProp = mapiProp;
			this.storeSession = storeSession;
			this.allowUnresolvedHeaders = allowUnresolvedHeaders;
			this.allowCreate = allowCreate;
			this.allowCreateHeaders = allowCreateHeaders;
			this.properties = propertyDefinitions;
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0004FD68 File Offset: 0x0004DF68
		public int Count
		{
			get
			{
				if (this.count == -1)
				{
					this.count = this.properties.Count<NativeStorePropertyDefinition>();
				}
				return this.count;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0004FD8A File Offset: 0x0004DF8A
		bool ICollection<PropTag>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0004FD8D File Offset: 0x0004DF8D
		private NamedPropMap NamedPropertyMap
		{
			get
			{
				if (this.namedPropertyMap == null)
				{
					this.namedPropertyMap = NamedPropMapCache.Default.GetMapping(this.storeSession);
					if (this.namedPropertyMap == null)
					{
						this.namedPropertyMap = new NamedPropMap(null);
					}
				}
				return this.namedPropertyMap;
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0004FDC7 File Offset: 0x0004DFC7
		public void Add(PropTag item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0004FDCE File Offset: 0x0004DFCE
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0004FDD5 File Offset: 0x0004DFD5
		public bool Remove(PropTag item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0004FDDC File Offset: 0x0004DFDC
		public bool Contains(PropTag item)
		{
			EnumValidator.ThrowIfInvalid<PropTag>(item, "item");
			foreach (PropTag propTag in this)
			{
				if (propTag == item)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0004FE3C File Offset: 0x0004E03C
		public void CopyTo(PropTag[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			foreach (PropTag propTag in this)
			{
				if (index >= array.Length)
				{
					throw new ArgumentException("Destination array is too small", "index");
				}
				array[index++] = propTag;
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0004FEC0 File Offset: 0x0004E0C0
		public PropertyDefinitionToPropTagCollection.Enumerator GetEnumerator()
		{
			return new PropertyDefinitionToPropTagCollection.Enumerator(this, this.properties.GetEnumerator());
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0004FED3 File Offset: 0x0004E0D3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new PropertyDefinitionToPropTagCollection.Enumerator(this, this.properties.GetEnumerator());
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0004FEEB File Offset: 0x0004E0EB
		IEnumerator<PropTag> IEnumerable<PropTag>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0004FEF8 File Offset: 0x0004E0F8
		private bool ResolveAllNamedProperties(int startFrom)
		{
			if (this.resolved)
			{
				return false;
			}
			this.resolved = true;
			NamedPropMap namedPropMap = this.NamedPropertyMap;
			IEnumerator<NativeStorePropertyDefinition> enumerator = this.properties.GetEnumerator();
			for (int i = 0; i < startFrom - 1; i++)
			{
				enumerator.MoveNext();
			}
			List<NamedProp> list = null;
			List<NamedProp> list2 = null;
			while (enumerator.MoveNext())
			{
				NativeStorePropertyDefinition nativeStorePropertyDefinition = enumerator.Current;
				if (nativeStorePropertyDefinition.SpecifiedWith != PropertyTypeSpecifier.PropertyTag)
				{
					NamedProp namedProp = ((NamedPropertyDefinition)nativeStorePropertyDefinition).GetKey().NamedProp;
					ushort num;
					if (!namedPropMap.TryGetPropIdFromNamedProp(namedProp, out num))
					{
						if (namedProp.Guid == WellKnownPropertySet.InternetHeaders && !PropertyDefinitionToPropTagCollection.PromotableInternetHeaders.Contains(namedProp.Name))
						{
							if (list2 == null)
							{
								list2 = new List<NamedProp>(20);
							}
							list2.Add(namedProp);
						}
						else
						{
							if (list == null)
							{
								list = new List<NamedProp>(20);
							}
							list.Add(namedProp);
						}
					}
				}
			}
			if (this.allowCreate == this.allowCreateHeaders && list2 != null)
			{
				if (list == null)
				{
					list = list2;
				}
				else
				{
					list.AddRange(list2);
				}
				list2 = null;
			}
			this.GetIdsFromNamedPropsWithRetry(this.allowCreate, list);
			this.GetIdsFromNamedPropsWithRetry(this.allowCreateHeaders, list2);
			return true;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00050018 File Offset: 0x0004E218
		private void GetIdsFromNamedPropsWithRetry(bool allowCreate, List<NamedProp> resolveList)
		{
			if (resolveList != null)
			{
				try
				{
					this.GetIdsFromNamedProps(allowCreate, resolveList);
				}
				catch (QuotaExceededException)
				{
					if (!allowCreate)
					{
						throw;
					}
					this.GetIdsFromNamedProps(false, resolveList);
				}
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00050054 File Offset: 0x0004E254
		private void GetIdsFromNamedProps(bool allowCreate, IList<NamedProp> namedProps)
		{
			PropTag[] array = null;
			StoreSession storeSession = this.storeSession;
			object thisObject = null;
			bool flag = false;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				array = this.mapiProp.GetIDsFromNames(allowCreate, namedProps);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetIDFromNames, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PropertyDefinitionToPropTagCollection.GetIdsFromNamedProps failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetIDFromNames, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PropertyDefinitionToPropTagCollection.GetIdsFromNamedProps failed.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			ushort[] array2 = new ushort[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				PropTag propTag = array[i];
				array2[i] = ((propTag == PropTag.Unresolved) ? 0 : ((ushort)propTag.Id()));
			}
			NamedPropMap namedPropMap = this.NamedPropertyMap;
			namedPropMap.AddMapping(allowCreate, array2, namedProps);
		}

		// Token: 0x04000319 RID: 793
		private const PropTag PropTagUnresolved = PropTag.Unresolved;

		// Token: 0x0400031A RID: 794
		private static object lockObject = new object();

		// Token: 0x0400031B RID: 795
		private static HashSet<string> promotableInternetHeaders = null;

		// Token: 0x0400031C RID: 796
		private IEnumerable<NativeStorePropertyDefinition> properties;

		// Token: 0x0400031D RID: 797
		private MapiProp mapiProp;

		// Token: 0x0400031E RID: 798
		private StoreSession storeSession;

		// Token: 0x0400031F RID: 799
		private bool allowUnresolvedHeaders;

		// Token: 0x04000320 RID: 800
		private bool allowCreateHeaders;

		// Token: 0x04000321 RID: 801
		private bool allowCreate;

		// Token: 0x04000322 RID: 802
		private int count = -1;

		// Token: 0x04000323 RID: 803
		private NamedPropMap namedPropertyMap;

		// Token: 0x04000324 RID: 804
		private bool resolved;

		// Token: 0x020000A4 RID: 164
		public struct Enumerator : IEnumerator<PropTag>, IDisposable, IEnumerator
		{
			// Token: 0x06000B67 RID: 2919 RVA: 0x000501DA File Offset: 0x0004E3DA
			public Enumerator(PropertyDefinitionToPropTagCollection parent, IEnumerator<NativeStorePropertyDefinition> propertyDefinitions)
			{
				this.parent = parent;
				this.definitions = propertyDefinitions;
				this.currentIndex = 0;
			}

			// Token: 0x17000234 RID: 564
			// (get) Token: 0x06000B68 RID: 2920 RVA: 0x000501F4 File Offset: 0x0004E3F4
			public PropTag Current
			{
				get
				{
					if (this.currentIndex == 0)
					{
						throw new InvalidOperationException();
					}
					NativeStorePropertyDefinition nativeStorePropertyDefinition = this.definitions.Current;
					return (PropTag)((nativeStorePropertyDefinition.SpecifiedWith == PropertyTypeSpecifier.PropertyTag) ? ((PropertyTagPropertyDefinition)nativeStorePropertyDefinition).PropertyTag : ((uint)this.GetPropertyTagFromNamedProperty((NamedPropertyDefinition)nativeStorePropertyDefinition)));
				}
			}

			// Token: 0x17000235 RID: 565
			// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0005023F File Offset: 0x0004E43F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000B6A RID: 2922 RVA: 0x0005024C File Offset: 0x0004E44C
			public bool MoveNext()
			{
				if (!this.definitions.MoveNext())
				{
					return false;
				}
				if (this.definitions.Current != null)
				{
					this.currentIndex++;
					return true;
				}
				throw new NullReferenceException(string.Format("Null NativeStorePropertyDefinition in list.  Current Index: {0}", this.currentIndex));
			}

			// Token: 0x06000B6B RID: 2923 RVA: 0x0005029F File Offset: 0x0004E49F
			public void Dispose()
			{
				this.definitions.Dispose();
			}

			// Token: 0x06000B6C RID: 2924 RVA: 0x000502AC File Offset: 0x0004E4AC
			void IEnumerator.Reset()
			{
				this.definitions.Reset();
				this.currentIndex = 0;
			}

			// Token: 0x06000B6D RID: 2925 RVA: 0x000502C0 File Offset: 0x0004E4C0
			private PropTag GetPropertyTagFromNamedProperty(NamedPropertyDefinition propertyDefinition)
			{
				NamedProp namedProp = propertyDefinition.GetKey().NamedProp;
				NamedPropMap namedPropertyMap = this.parent.NamedPropertyMap;
				ushort num;
				if (!namedPropertyMap.TryGetPropIdFromNamedProp(namedProp, out num) && (!this.parent.ResolveAllNamedProperties(this.currentIndex) || !namedPropertyMap.TryGetPropIdFromNamedProp(namedProp, out num)))
				{
					num = 0;
				}
				if (num != 0)
				{
					return PropTagHelper.PropTagFromIdAndType((int)num, propertyDefinition.MapiPropertyType);
				}
				if (!this.parent.storeSession.Capabilities.IsReadOnly && (!this.parent.allowUnresolvedHeaders || namedProp.Guid != WellKnownPropertySet.InternetHeaders || (namedProp.Guid == WellKnownPropertySet.InternetHeaders && PropertyDefinitionToPropTagCollection.PromotableInternetHeaders.Contains(namedProp.Name))))
				{
					LocalizedString localizedString = ServerStrings.ExInvalidNamedProperty(propertyDefinition.ToString());
					ExTraceGlobals.StorageTracer.TraceError(0L, localizedString);
					throw new StoragePermanentException(localizedString);
				}
				return PropTag.Unresolved;
			}

			// Token: 0x04000325 RID: 805
			private IEnumerator<NativeStorePropertyDefinition> definitions;

			// Token: 0x04000326 RID: 806
			private PropertyDefinitionToPropTagCollection parent;

			// Token: 0x04000327 RID: 807
			private int currentIndex;
		}
	}
}
