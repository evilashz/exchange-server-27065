using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000072 RID: 114
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ManifestProviderBase<TMapiManifest, TPhase> : DisposableObject where TMapiManifest : MapiUnk where TPhase : struct
	{
		// Token: 0x060007D6 RID: 2006 RVA: 0x0003D3C8 File Offset: 0x0003B5C8
		protected ManifestProviderBase(CoreFolder folder, ManifestConfigFlags flags, QueryFilter filter, StorageIcsState initialState, PropertyDefinition[] includeProperties, PropertyDefinition[] excludeProperties)
		{
			this.folder = folder;
			this.disposeTracker = this.GetDisposeTracker();
			bool flag = false;
			try
			{
				PersistablePropertyBag persistablePropertyBag = CoreObject.GetPersistablePropertyBag(this.folder);
				Restriction restriction = null;
				if (filter != null)
				{
					restriction = FilterRestrictionConverter.CreateRestriction(folder.Session, persistablePropertyBag.ExTimeZone, persistablePropertyBag.MapiProp, filter);
				}
				ICollection<PropTag> includePropertyTags = PropertyTagCache.Cache.PropTagsFromPropertyDefinitions<PropertyDefinition>(this.MapiFolder, this.Session, true, includeProperties);
				ICollection<PropTag> excludePropertyTags = PropertyTagCache.Cache.PropTagsFromPropertyDefinitions<PropertyDefinition>(this.MapiFolder, this.Session, true, excludeProperties);
				StoreSession session = this.Session;
				object thisObject = this.folder;
				bool flag2 = false;
				try
				{
					if (session != null)
					{
						session.BeginMapiCall();
						session.BeginServerHealthCall();
						flag2 = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					this.mapiManifest = this.MapiCreateManifest(flags, restriction, initialState, includePropertyTags, excludePropertyTags);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.CannotCreateManifestEx(base.GetType()), ex, session, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("ManifestProviderBase..ctor. Failed to create/configure HierarchyManifestEx.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.CannotCreateManifestEx(base.GetType()), ex2, session, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("ManifestProviderBase..ctor. Failed to create/configure HierarchyManifestEx.", new object[0]),
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
							if (flag2)
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
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					base.Dispose();
				}
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0003D5E8 File Offset: 0x0003B7E8
		public void GetFinalState(ref StorageIcsState finalState)
		{
			this.CheckDisposed(null);
			if (this.nextChange == null && !this.finalStateReturned)
			{
				StoreSession session = this.Session;
				bool flag = false;
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
					this.MapiGetFinalState(ref finalState);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.CannotSynchronizeManifestEx(typeof(TMapiManifest), this.clientPhase, this.manifestPhase), ex, session, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("ManifestProviderBase::GetFinalState failed", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.CannotSynchronizeManifestEx(typeof(TMapiManifest), this.clientPhase, this.manifestPhase), ex2, session, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("ManifestProviderBase::GetFinalState failed", new object[0]),
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
				this.finalStateReturned = true;
				return;
			}
			throw new InvalidOperationException("Consumers cannot get a final state twice or until they consumed all changes.");
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0003D778 File Offset: 0x0003B978
		protected static void TranslateFlag(ManifestConfigFlags sourceFlag, SyncConfigFlags destinationFlag, ManifestConfigFlags sourceFlags, ref SyncConfigFlags destinationFlags)
		{
			if ((sourceFlags & sourceFlag) == sourceFlag)
			{
				destinationFlags |= destinationFlag;
				return;
			}
			destinationFlags &= ~destinationFlag;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0003D78E File Offset: 0x0003B98E
		protected StoreSession Session
		{
			get
			{
				return this.folder.Session;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0003D79B File Offset: 0x0003B99B
		protected TMapiManifest MapiManifest
		{
			get
			{
				return this.mapiManifest;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0003D7A3 File Offset: 0x0003B9A3
		protected MapiFolder MapiFolder
		{
			get
			{
				return (MapiFolder)CoreObject.GetPersistablePropertyBag(this.folder).MapiProp;
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0003D7BA File Offset: 0x0003B9BA
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				Util.DisposeIfPresent(this.mapiManifest);
				Util.DisposeIfPresent(this.disposeTracker);
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0003D7E4 File Offset: 0x0003B9E4
		protected PropValue[] FromMapiPropValueToXsoPropValue(PropValue[] propValues)
		{
			PropTag[] array = new PropTag[propValues.Length];
			for (int i = 0; i < propValues.Length; i++)
			{
				array[i] = propValues[i].PropTag;
			}
			NativeStorePropertyDefinition[] array2 = PropertyTagCache.Cache.PropertyDefinitionsFromPropTags(NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck, this.MapiFolder, this.Session, array);
			PropValue[] array3 = new PropValue[propValues.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				if (array2[j] == null)
				{
					throw new NotSupportedException(string.Format("The property tag cannot be resolved to a property definition. PropertyTag = {0}", array[j]));
				}
				object valueFromPropValue = MapiPropertyBag.GetValueFromPropValue(this.Session, CoreObject.GetPersistablePropertyBag(this.folder).ExTimeZone, array2[j], propValues[j]);
				array3[j] = new PropValue(array2[j], valueFromPropValue);
			}
			return array3;
		}

		// Token: 0x060007DE RID: 2014
		protected abstract bool IsValidTransition(TPhase oldPhase, TPhase newPhase);

		// Token: 0x060007DF RID: 2015
		protected abstract TMapiManifest MapiCreateManifest(ManifestConfigFlags flags, Restriction restriction, StorageIcsState initialState, ICollection<PropTag> includePropertyTags, ICollection<PropTag> excludePropertyTags);

		// Token: 0x060007E0 RID: 2016
		protected abstract void MapiGetFinalState(ref StorageIcsState finalState);

		// Token: 0x060007E1 RID: 2017
		protected abstract ManifestStatus MapiSynchronize();

		// Token: 0x060007E2 RID: 2018 RVA: 0x0003D8B1 File Offset: 0x0003BAB1
		protected void SetChange(TPhase newPhase, ManifestChangeBase changeBase)
		{
			if (this.nextChange != null)
			{
				throw new InvalidOperationException("The change should have been retrieved by the consumer.");
			}
			this.nextChange = changeBase;
			this.SetPhase(ref this.manifestPhase, newPhase);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0003D8DC File Offset: 0x0003BADC
		protected bool TryGetChange<T>(TPhase newClientPhase, out T change) where T : ManifestChangeBase
		{
			if (this.finalStateReturned)
			{
				throw new InvalidOperationException("Final state has been returned. No other data can be requested");
			}
			this.CacheNextChangeFromMapi();
			if (this.nextChange != null && !newClientPhase.Equals(this.manifestPhase) && !this.IsValidTransition(newClientPhase, this.manifestPhase))
			{
				throw new InvalidOperationException(string.Format("Client phase cannot be advanced from {0} to {1} when manifest phase is {2}", this.clientPhase, newClientPhase, this.manifestPhase));
			}
			this.SetPhase(ref this.clientPhase, newClientPhase);
			if (this.clientPhase.Equals(this.manifestPhase))
			{
				change = (T)((object)Interlocked.Exchange<ManifestChangeBase>(ref this.nextChange, null));
				return change != null;
			}
			change = default(T);
			return false;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0003D9C0 File Offset: 0x0003BBC0
		private void CacheNextChangeFromMapi()
		{
			if (this.nextChange != null || this.noMoreData)
			{
				return;
			}
			StoreSession session = this.folder.Session;
			bool flag = false;
			ManifestStatus manifestStatus;
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
				manifestStatus = this.MapiSynchronize();
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotSynchronizeManifestEx(typeof(TMapiManifest), this.clientPhase, this.manifestPhase), ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("ManifestProviderBase.CacheNextChangeFromMapi. Call to Synchronize failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotSynchronizeManifestEx(typeof(TMapiManifest), this.clientPhase, this.manifestPhase), ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("ManifestProviderBase.CacheNextChangeFromMapi. Call to Synchronize failed.", new object[0]),
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
			switch (manifestStatus)
			{
			case ManifestStatus.Done:
				this.noMoreData = true;
				return;
			case ManifestStatus.Yielded:
				if (this.nextChange == null)
				{
					throw new InvalidOperationException("Mapi reported that a callback was called, but no new changes got recorded");
				}
				return;
			}
			throw new InvalidOperationException(string.Format("By design, we should continue until we are done the whole changes. Status = {0}.", manifestStatus));
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0003DB80 File Offset: 0x0003BD80
		private void SetPhase(ref TPhase phase, TPhase newPhase)
		{
			if (!this.IsValidTransition(phase, newPhase))
			{
				throw new InvalidOperationException(string.Format("Change of phases from {0} to {1} is not supported", phase, newPhase));
			}
			phase = newPhase;
		}

		// Token: 0x0400021C RID: 540
		private readonly TMapiManifest mapiManifest;

		// Token: 0x0400021D RID: 541
		private readonly CoreFolder folder;

		// Token: 0x0400021E RID: 542
		private readonly DisposeTracker disposeTracker;

		// Token: 0x0400021F RID: 543
		private ManifestChangeBase nextChange;

		// Token: 0x04000220 RID: 544
		private TPhase manifestPhase;

		// Token: 0x04000221 RID: 545
		private TPhase clientPhase;

		// Token: 0x04000222 RID: 546
		private bool noMoreData;

		// Token: 0x04000223 RID: 547
		private bool finalStateReturned;
	}
}
