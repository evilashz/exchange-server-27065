using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000AB RID: 171
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class NamedPropertyDefinition : NativeStorePropertyDefinition
	{
		// Token: 0x06000BBD RID: 3005 RVA: 0x00051582 File Offset: 0x0004F782
		protected NamedPropertyDefinition(PropertyTypeSpecifier propertyTypeSpecifier, string displayName, Type type, PropType mapiPropertyType, PropertyFlags childFlags, PropertyDefinitionConstraint[] constraints) : base(propertyTypeSpecifier, displayName, type, mapiPropertyType, childFlags, constraints)
		{
		}

		// Token: 0x06000BBE RID: 3006
		public abstract NamedPropertyDefinition.NamedPropertyKey GetKey();

		// Token: 0x020000AC RID: 172
		[Serializable]
		public abstract class NamedPropertyKey
		{
			// Token: 0x06000BBF RID: 3007 RVA: 0x00051594 File Offset: 0x0004F794
			internal static void ClearUnreferenced()
			{
				List<NamedProp> list = new List<NamedProp>();
				try
				{
					NamedPropertyDefinition.NamedPropertyKey.lockObject.EnterReadLock();
					foreach (KeyValuePair<NamedProp, WeakReference> keyValuePair in NamedPropertyDefinition.NamedPropertyKey.namedProps)
					{
						if (!keyValuePair.Value.IsAlive)
						{
							list.Add(keyValuePair.Key);
						}
					}
				}
				finally
				{
					try
					{
						NamedPropertyDefinition.NamedPropertyKey.lockObject.ExitReadLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
				if (list.Count > 0)
				{
					try
					{
						NamedPropertyDefinition.NamedPropertyKey.lockObject.EnterWriteLock();
						foreach (NamedProp key in list)
						{
							NamedPropertyDefinition.NamedPropertyKey.namedProps.Remove(key);
						}
					}
					finally
					{
						try
						{
							NamedPropertyDefinition.NamedPropertyKey.lockObject.ExitWriteLock();
						}
						catch (SynchronizationLockException)
						{
						}
					}
				}
			}

			// Token: 0x06000BC0 RID: 3008 RVA: 0x000516B4 File Offset: 0x0004F8B4
			internal static NamedProp GetSingleton(NamedProp property)
			{
				try
				{
					NamedPropertyDefinition.NamedPropertyKey.lockObject.EnterReadLock();
					WeakReference weakReference;
					if (NamedPropertyDefinition.NamedPropertyKey.namedProps.TryGetValue(property, out weakReference))
					{
						NamedProp namedProp = (NamedProp)weakReference.Target;
						if (namedProp != null)
						{
							return namedProp;
						}
					}
				}
				finally
				{
					try
					{
						NamedPropertyDefinition.NamedPropertyKey.lockObject.ExitReadLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
				try
				{
					NamedPropertyDefinition.NamedPropertyKey.lockObject.EnterWriteLock();
					if (NamedPropertyDefinition.NamedPropertyKey.namedProps.ContainsKey(property))
					{
						NamedPropertyDefinition.NamedPropertyKey.namedProps[property] = new WeakReference(property);
					}
					else
					{
						NamedPropertyDefinition.NamedPropertyKey.namedProps.Add(new NamedProp(property), new WeakReference(property));
					}
				}
				finally
				{
					try
					{
						NamedPropertyDefinition.NamedPropertyKey.lockObject.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
				return property;
			}

			// Token: 0x06000BC1 RID: 3009 RVA: 0x00051788 File Offset: 0x0004F988
			internal static ICollection<NamedProp> AddNamedProps(ICollection<NamedProp> props)
			{
				NamedProp[] array = new NamedProp[props.Count];
				int num = 0;
				IEnumerator<NamedProp> enumerator = props.GetEnumerator();
				try
				{
					NamedPropertyDefinition.NamedPropertyKey.lockObject.EnterReadLock();
					while (enumerator.MoveNext())
					{
						NamedProp namedProp = enumerator.Current;
						if (namedProp == null || namedProp.IsSingleInstanced)
						{
							array[num++] = namedProp;
						}
						else
						{
							WeakReference weakReference;
							if (!NamedPropertyDefinition.NamedPropertyKey.namedProps.TryGetValue(namedProp, out weakReference))
							{
								break;
							}
							NamedProp namedProp2 = (NamedProp)weakReference.Target;
							if (namedProp2 == null)
							{
								break;
							}
							array[num++] = namedProp2;
						}
					}
				}
				finally
				{
					try
					{
						NamedPropertyDefinition.NamedPropertyKey.lockObject.ExitReadLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
				if (num == props.Count)
				{
					return array;
				}
				try
				{
					NamedPropertyDefinition.NamedPropertyKey.lockObject.EnterWriteLock();
					do
					{
						NamedProp namedProp3 = enumerator.Current;
						WeakReference weakReference;
						if (namedProp3 == null || namedProp3.IsSingleInstanced)
						{
							array[num++] = namedProp3;
						}
						else if (NamedPropertyDefinition.NamedPropertyKey.namedProps.TryGetValue(namedProp3, out weakReference))
						{
							NamedProp namedProp4 = (NamedProp)weakReference.Target;
							if (namedProp4 != null)
							{
								array[num++] = namedProp4;
							}
							else
							{
								NamedPropertyDefinition.NamedPropertyKey.namedProps[namedProp3] = new WeakReference(namedProp3);
								array[num++] = namedProp3;
							}
						}
						else
						{
							NamedPropertyDefinition.NamedPropertyKey.namedProps.Add(new NamedProp(namedProp3), new WeakReference(namedProp3));
							array[num++] = namedProp3;
						}
					}
					while (enumerator.MoveNext());
				}
				finally
				{
					try
					{
						NamedPropertyDefinition.NamedPropertyKey.lockObject.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
				return array;
			}

			// Token: 0x06000BC2 RID: 3010 RVA: 0x0005190C File Offset: 0x0004FB0C
			protected NamedPropertyKey(NamedProp namedProp)
			{
				this.namedProp = namedProp;
			}

			// Token: 0x06000BC3 RID: 3011 RVA: 0x0005191C File Offset: 0x0004FB1C
			protected NamedPropertyKey(Guid propGuid, string propName)
			{
				StoreSession storeSession = null;
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
					this.namedProp = new NamedProp(propGuid, propName);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiInvalidParam, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Unable to create property key", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiInvalidParam, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Unable to create property key", new object[0]),
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
			}

			// Token: 0x06000BC4 RID: 3012 RVA: 0x00051A40 File Offset: 0x0004FC40
			protected NamedPropertyKey(Guid propGuid, int propId)
			{
				StoreSession storeSession = null;
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
					this.namedProp = new NamedProp(propGuid, propId);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiInvalidParam, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Unable to create property key", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiInvalidParam, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Unable to create property key", new object[0]),
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
			}

			// Token: 0x1700023C RID: 572
			// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00051B64 File Offset: 0x0004FD64
			internal NamedProp NamedProp
			{
				get
				{
					return this.namedProp;
				}
			}

			// Token: 0x04000341 RID: 833
			private readonly NamedProp namedProp;

			// Token: 0x04000342 RID: 834
			private static Dictionary<NamedProp, WeakReference> namedProps = new Dictionary<NamedProp, WeakReference>();

			// Token: 0x04000343 RID: 835
			private static ReaderWriterLockSlim lockObject = new ReaderWriterLockSlim();
		}
	}
}
