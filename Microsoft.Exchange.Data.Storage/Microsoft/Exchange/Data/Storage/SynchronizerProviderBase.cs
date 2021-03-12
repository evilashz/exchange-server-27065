using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000073 RID: 115
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SynchronizerProviderBase : DisposableObject
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x0003DBBC File Offset: 0x0003BDBC
		protected SynchronizerProviderBase(CoreFolder folder, SynchronizerConfigFlags flags, QueryFilter filter, StorageIcsState initialState, PropertyDefinition[] includeProperties, PropertyDefinition[] excludeProperties, short[] unspecifiedIncludeProperties, short[] unspecifiedExcludeProperties, int fastTransferBlockSize)
		{
			this.folder = folder;
			bool flag = false;
			try
			{
				PersistablePropertyBag persistablePropertyBag = CoreObject.GetPersistablePropertyBag(this.folder);
				Restriction restriction = null;
				if (filter != null)
				{
					restriction = FilterRestrictionConverter.CreateRestriction(folder.Session, persistablePropertyBag.ExTimeZone, persistablePropertyBag.MapiProp, filter);
				}
				ICollection<PropTag> includePropertyTags = null;
				if (includeProperties != null && includeProperties.Length > 0)
				{
					includePropertyTags = PropertyTagCache.Cache.PropTagsFromPropertyDefinitions<PropertyDefinition>(this.MapiFolder, this.folder.Session, true, includeProperties);
				}
				ICollection<PropTag> excludePropertyTags = null;
				if (excludeProperties != null && excludeProperties.Length > 0)
				{
					excludePropertyTags = PropertyTagCache.Cache.PropTagsFromPropertyDefinitions<PropertyDefinition>(this.MapiFolder, this.folder.Session, true, excludeProperties);
				}
				SynchronizerProviderBase.ReconstituteProperties(unspecifiedIncludeProperties, ref includePropertyTags);
				SynchronizerProviderBase.ReconstituteProperties(unspecifiedExcludeProperties, ref excludePropertyTags);
				StoreSession session = this.folder.Session;
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
					this.MapiCreateSynchronizer(flags, restriction, initialState, includePropertyTags, excludePropertyTags, fastTransferBlockSize);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.CannotCreateSynchronizerEx(base.GetType()), ex, session, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("SynchronizerProviderBase..ctor. Failed to create/configure MapiSynchronizerEx.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.CannotCreateSynchronizerEx(base.GetType()), ex2, session, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("SynchronizerProviderBase..ctor. Failed to create/configure MapiSynchronizerEx.", new object[0]),
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

		// Token: 0x060007E7 RID: 2023 RVA: 0x0003DE08 File Offset: 0x0003C008
		public void GetBuffer(out byte[] buffer, out uint steps, out uint progress, out FastTransferState state, out int residualCacheSize, out bool doneInCache)
		{
			if (this.cachedBuffer == null)
			{
				this.InternalGetBuffer(out buffer, out steps, out progress, out state, out residualCacheSize, out doneInCache);
			}
			else
			{
				buffer = this.cachedBuffer;
				steps = this.cachedSteps;
				progress = this.cachedProgress;
				state = this.cachedState;
				residualCacheSize = this.cachedResidualCacheSize;
				doneInCache = this.cachedDoneInCache;
				this.cachedBuffer = null;
			}
			if (state == FastTransferState.Partial || state == FastTransferState.NoRoom)
			{
				this.InternalGetBuffer(out this.cachedBuffer, out this.cachedSteps, out this.cachedProgress, out this.cachedState, out this.cachedResidualCacheSize, out this.cachedDoneInCache);
				if ((this.cachedBuffer == null || this.cachedBuffer.Length == 0) && this.cachedState == FastTransferState.Done)
				{
					state = this.cachedState;
					steps = this.cachedSteps;
					progress = this.cachedProgress;
					residualCacheSize = this.cachedResidualCacheSize;
					doneInCache = this.cachedDoneInCache;
				}
			}
			if (buffer == null)
			{
				buffer = Array<byte>.Empty;
			}
			if (state == FastTransferState.Done)
			{
				this.allowGetFinalState = true;
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0003DEFC File Offset: 0x0003C0FC
		public void GetFinalState(ref StorageIcsState finalState)
		{
			this.CheckDisposed(null);
			if (this.allowGetFinalState)
			{
				StoreSession session = this.folder.Session;
				bool flag = false;
				try
				{
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
						throw StorageGlobals.TranslateMapiException(ServerStrings.CannotGetFinalStateSynchronizerProviderBase, ex, session, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("SynchronizerProviderBase::GetFinalState failed", new object[0]),
							ex
						});
					}
					catch (MapiRetryableException ex2)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.CannotGetFinalStateSynchronizerProviderBase, ex2, session, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("SynchronizerProviderBase::GetFinalState failed", new object[0]),
							ex2
						});
					}
					return;
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
			}
			throw new InvalidOperationException("Consumers cannot get a final state until they consumed all changes.");
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0003E03C File Offset: 0x0003C23C
		protected static SyncConfigFlags ConvertSynchronizerConfigFlags(SynchronizerConfigFlags syncFlag)
		{
			EnumValidator.ThrowIfInvalid<SynchronizerConfigFlags>(syncFlag);
			return (SyncConfigFlags)(syncFlag | (SynchronizerConfigFlags)131072);
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0003E058 File Offset: 0x0003C258
		protected MapiFolder MapiFolder
		{
			get
			{
				return (MapiFolder)CoreObject.GetPersistablePropertyBag(this.folder).MapiProp;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0003E06F File Offset: 0x0003C26F
		protected CoreFolder CoreFolder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x060007EC RID: 2028
		protected abstract void MapiCreateSynchronizer(SynchronizerConfigFlags flags, Restriction restriction, StorageIcsState initialState, ICollection<PropTag> includePropertyTags, ICollection<PropTag> excludePropertyTags, int bufferSize);

		// Token: 0x060007ED RID: 2029
		protected abstract FastTransferBlock MapiGetBuffer(out int residualCacheSize, out bool doneInCache);

		// Token: 0x060007EE RID: 2030
		protected abstract void MapiGetFinalState(ref StorageIcsState finalState);

		// Token: 0x060007EF RID: 2031 RVA: 0x0003E078 File Offset: 0x0003C278
		private void InternalGetBuffer(out byte[] buffer, out uint steps, out uint progress, out FastTransferState state, out int residualCacheSize, out bool doneInCache)
		{
			StoreSession session = this.folder.Session;
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
				FastTransferBlock fastTransferBlock = this.MapiGetBuffer(out residualCacheSize, out doneInCache);
				buffer = fastTransferBlock.Buffer;
				steps = fastTransferBlock.Steps;
				progress = fastTransferBlock.Progress;
				state = (FastTransferState)fastTransferBlock.State;
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotGetSynchronizeBuffers(typeof(SynchronizerProviderBase)), ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("SynchronizerProviderBase.GetBuffer. Call to GetBuffer failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotGetSynchronizeBuffers(typeof(SynchronizerProviderBase)), ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("SynchronizerProviderBase.GetBuffer. Call to GetBuffer failed.", new object[0]),
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
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0003E1DC File Offset: 0x0003C3DC
		private static void ReconstituteProperties(short[] unspecifiedTags, ref ICollection<PropTag> props)
		{
			if (unspecifiedTags != null)
			{
				List<PropTag> list = new List<PropTag>();
				foreach (short propId in unspecifiedTags)
				{
					list.Add(PropTagHelper.PropTagFromIdAndType((int)propId, PropType.Unspecified));
				}
				list.AddRange(props);
				props = list;
			}
		}

		// Token: 0x04000224 RID: 548
		private readonly CoreFolder folder;

		// Token: 0x04000225 RID: 549
		private byte[] cachedBuffer;

		// Token: 0x04000226 RID: 550
		private uint cachedSteps;

		// Token: 0x04000227 RID: 551
		private uint cachedProgress;

		// Token: 0x04000228 RID: 552
		private FastTransferState cachedState;

		// Token: 0x04000229 RID: 553
		private int cachedResidualCacheSize;

		// Token: 0x0400022A RID: 554
		private bool cachedDoneInCache;

		// Token: 0x0400022B RID: 555
		private bool allowGetFinalState;
	}
}
