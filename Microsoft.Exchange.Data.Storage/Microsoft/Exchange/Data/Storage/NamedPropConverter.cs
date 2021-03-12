using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000A2 RID: 162
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class NamedPropConverter
	{
		// Token: 0x06000B4B RID: 2891 RVA: 0x0004F45C File Offset: 0x0004D65C
		public static NamedPropertyDefinition.NamedPropertyKey[] GetNamesFromIds(StoreSession session, List<ushort> propIds)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(propIds, "propIds");
			NamedProp[] namedPropsFromIds = NamedPropConverter.GetNamedPropsFromIds(session, session.Mailbox.MapiStore, propIds);
			return NamedPropConverter.MapiNamedPropToNamedPropertyKey(namedPropsFromIds);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0004F498 File Offset: 0x0004D698
		public static NamedPropertyDefinition.NamedPropertyKey[] GetNamesFromGuid(StoreSession session, Guid guid, bool skipKindString, bool skipKindId)
		{
			Util.ThrowOnNullArgument(session, "session");
			NamedProp[] namedProps = NamedPropConverter.MapiGetNamesFromGuid(session, session.Mailbox.MapiStore, guid, skipKindString, skipKindId);
			return NamedPropConverter.MapiNamedPropToNamedPropertyKey(namedProps);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0004F4CC File Offset: 0x0004D6CC
		public static ushort[] GetIdsFromNames(StoreSession session, bool createNamedProperties, List<NamedPropertyDefinition.NamedPropertyKey> propertyKeys)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(propertyKeys, "propertyKeys");
			List<NamedProp> list = new List<NamedProp>(propertyKeys.Count);
			foreach (NamedPropertyDefinition.NamedPropertyKey namedPropertyKey in propertyKeys)
			{
				if (namedPropertyKey == null)
				{
					throw new ArgumentNullException("propertyKeys", "Element in propertyKeys cannot be null.");
				}
				list.Add(namedPropertyKey.NamedProp);
			}
			return NamedPropConverter.GetIdsFromNamedProps(session, session.Mailbox.MapiStore, createNamedProperties, list);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0004F568 File Offset: 0x0004D768
		public static void Reset()
		{
			NamedPropMapCache.Default.Reset();
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0004F574 File Offset: 0x0004D774
		public static void UpdateCacheLimits(int namedPropertyCacheNumberOfUsers, int namedPropertyCachePropertiesPerUser, out int oldNamedPropertyCacheNumberOfUsers, out int oldNamedPropertyCachePropertiesPerUser)
		{
			Util.ThrowOnArgumentOutOfRangeOnLessThan(namedPropertyCacheNumberOfUsers, 1, "namedPropertyCacheNumberOfUsers");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(namedPropertyCachePropertiesPerUser, 1, "namedPropertyCachePropertiesPerUser");
			NamedPropMapCache.Default.UpdateCacheLimits(namedPropertyCacheNumberOfUsers, namedPropertyCachePropertiesPerUser, out oldNamedPropertyCacheNumberOfUsers, out oldNamedPropertyCachePropertiesPerUser);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0004F59C File Offset: 0x0004D79C
		public static NamedProp[] GetNamedPropsFromIds(StoreSession session, MapiProp mapiProp, ICollection<ushort> propIds)
		{
			int count = propIds.Count;
			NamedProp[] array = new NamedProp[count];
			List<ushort> list = new List<ushort>();
			List<int> list2 = new List<int>();
			NamedPropMap mapping = NamedPropMapCache.Default.GetMapping(session);
			int num = 0;
			foreach (ushort num2 in propIds)
			{
				if (num2 < 32768)
				{
					throw new ArgumentOutOfRangeException(string.Format("PropId is not in the range of named props.  PropId = 0x{0:x}.", num2));
				}
				NamedProp namedProp = null;
				if (mapping != null && mapping.TryGetNamedPropFromPropId(num2, out namedProp))
				{
					array[num] = namedProp;
				}
				else
				{
					list.Add(num2);
					list2.Add(num);
				}
				num++;
			}
			int num3 = 0;
			while (list.Count > 0)
			{
				int count2 = Math.Min(list.Count, 256);
				List<ushort> range = list.GetRange(0, count2);
				NamedProp[] array2 = NamedPropConverter.MapiGetNamesFromIds(session, mapiProp, range);
				if (mapping != null)
				{
					mapping.AddMapping(false, range, array2);
				}
				for (int i = 0; i < array2.Length; i++)
				{
					int num4 = list2[i + num3 * 256];
					array[num4] = array2[i];
				}
				list.RemoveRange(0, count2);
				num3++;
			}
			return array;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0004F6E4 File Offset: 0x0004D8E4
		internal static ushort[] GetIdsFromNamedProps(StoreSession session, MapiProp mapiProp, bool createNamedProperties, List<NamedProp> namedProps)
		{
			int count = namedProps.Count;
			ushort[] array = new ushort[count];
			List<NamedProp> list = new List<NamedProp>(count);
			List<NamedProp> list2 = new List<NamedProp>(count);
			List<int> list3 = new List<int>(count);
			List<int> list4 = new List<int>(count);
			NamedPropMap mapping = NamedPropMapCache.Default.GetMapping(session);
			for (int i = 0; i < count; i++)
			{
				NamedProp namedProp = namedProps[i];
				bool flag = false;
				ushort num;
				if (mapping != null && mapping.TryGetPropIdFromNamedProp(namedProp, out num))
				{
					array[i] = num;
					flag = true;
				}
				if (!flag)
				{
					if (namedProp.Guid == WellKnownPropertySet.InternetHeaders)
					{
						list2.Add(namedProp);
						list4.Add(i);
					}
					else
					{
						list.Add(namedProp);
						list3.Add(i);
					}
				}
			}
			list.AddRange(list2);
			list3.AddRange(list4);
			if (list.Count > 0)
			{
				PropTag[] array2 = NamedPropConverter.MapiGetIdsFromNames(session, mapiProp, createNamedProperties, list);
				ushort[] array3 = new ushort[array2.Length];
				for (int j = 0; j < array2.Length; j++)
				{
					PropTag propTag = array2[j];
					if (propTag == PropTag.Unresolved)
					{
						array3[j] = 0;
					}
					else
					{
						array3[j] = (ushort)propTag.Id();
					}
				}
				if (mapping != null)
				{
					mapping.AddMapping(createNamedProperties, array3, list);
				}
				for (int k = 0; k < array3.Length; k++)
				{
					array[list3[k]] = array3[k];
				}
			}
			return array;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0004F834 File Offset: 0x0004DA34
		private static NamedProp[] MapiGetNamesFromIds(StoreSession session, MapiProp mapiProp, IList<ushort> propIds)
		{
			PropTag[] array = new PropTag[propIds.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = PropTagHelper.PropTagFromIdAndType((int)propIds[i], PropType.Unspecified);
			}
			object thisObject = null;
			bool flag = false;
			NamedProp[] namesFromIDs;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				try
				{
					namesFromIDs = mapiProp.GetNamesFromIDs(array);
				}
				catch (MapiExceptionArgument innerException)
				{
					throw new CorruptDataException(ServerStrings.MapiCannotGetNamedProperties, innerException);
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetNamedProperties, ex, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PropertyTagCache.MapiResolveTags failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetNamedProperties, ex2, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PropertyTagCache.MapiResolveTags failed.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
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
			return namesFromIDs;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0004F99C File Offset: 0x0004DB9C
		private static NamedProp[] MapiGetNamesFromGuid(StoreSession session, MapiProp mapiProp, Guid guid, bool skipKindString, bool skipKindId)
		{
			GetNamesFromIDsFlags getNamesFromIDsFlags = GetNamesFromIDsFlags.None;
			if (skipKindString)
			{
				getNamesFromIDsFlags |= GetNamesFromIDsFlags.NoStrings;
			}
			if (skipKindId)
			{
				getNamesFromIDsFlags |= GetNamesFromIDsFlags.NoIds;
			}
			object thisObject = null;
			bool flag = false;
			NamedProp[] namesFromIDs;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				namesFromIDs = mapiProp.GetNamesFromIDs(guid, getNamesFromIDsFlags);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetNamedProperties, ex, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PropertyTagCache.MapiResolveTags failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetNamedProperties, ex2, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PropertyTagCache.MapiResolveTags failed.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
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
			return namesFromIDs;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0004FACC File Offset: 0x0004DCCC
		private static NamedPropertyDefinition.NamedPropertyKey[] MapiNamedPropToNamedPropertyKey(NamedProp[] namedProps)
		{
			NamedPropertyDefinition.NamedPropertyKey[] array = new NamedPropertyDefinition.NamedPropertyKey[namedProps.Length];
			for (int i = 0; i < namedProps.Length; i++)
			{
				NamedProp namedProp = namedProps[i];
				if (namedProp == null)
				{
					array[i] = null;
				}
				else
				{
					NamedProp namedProp2 = WellKnownNamedProperties.Find(namedProp);
					if (namedProp2 != null)
					{
						namedProp = namedProp2;
					}
					else
					{
						namedProp = NamedPropertyDefinition.NamedPropertyKey.GetSingleton(namedProp);
					}
					if (namedProp.Kind == NamedPropKind.Id)
					{
						array[i] = new GuidIdPropertyDefinition.GuidIdKey(namedProp);
					}
					else
					{
						array[i] = new GuidNamePropertyDefinition.GuidNameKey(namedProp);
					}
				}
			}
			return array;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0004FB30 File Offset: 0x0004DD30
		private static PropTag[] MapiGetIdsFromNames(StoreSession session, MapiProp mapiProp, bool createNamedProperties, ICollection<NamedProp> namedProperties)
		{
			object thisObject = null;
			bool flag = false;
			PropTag[] idsFromNames;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				idsFromNames = mapiProp.GetIDsFromNames(createNamedProperties, namedProperties);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetIDFromNames, ex, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PropertyTagCache.MapiResolveProps failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetIDFromNames, ex2, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PropertyTagCache.MapiResolveProps failed.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
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
			return idsFromNames;
		}

		// Token: 0x04000316 RID: 790
		private const int NamedPropBatchSize = 256;

		// Token: 0x04000317 RID: 791
		internal const ushort PropIdUnresolved = 0;

		// Token: 0x04000318 RID: 792
		internal const ushort MinPropIdForNamedProperties = 32768;
	}
}
