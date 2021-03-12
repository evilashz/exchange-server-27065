using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000071 RID: 113
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class HierarchySynchronizationUploadContext : SynchronizationUploadContextBase<MapiHierarchyCollectorEx>
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x0003D21F File Offset: 0x0003B41F
		public HierarchySynchronizationUploadContext(CoreFolder folder, StorageIcsState initialState) : base(folder, initialState)
		{
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0003D22C File Offset: 0x0003B42C
		internal void ImportChange(ExTimeZone exTimeZone, IList<PropertyDefinition> propertyDefinitions, IList<object> propertyValues)
		{
			this.CheckDisposed(null);
			PropValue[] propValues = base.GetPropValuesFromValues(exTimeZone, propertyDefinitions, propertyValues).ToArray();
			StoreSession session = base.Session;
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
				base.MapiCollector.ImportFolderChange(propValues);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotImportFolderChange, ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Import of the folder change failed", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotImportFolderChange, ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Import of the folder change failed", new object[0]),
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

		// Token: 0x060007D2 RID: 2002 RVA: 0x0003D368 File Offset: 0x0003B568
		protected override MapiHierarchyCollectorEx MapiCreateCollector(StorageIcsState initialState)
		{
			return base.MapiFolder.CreateHierarchyCollectorEx(initialState.StateIdsetGiven, initialState.StateCnsetSeen, CollectorConfigFlags.None);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0003D384 File Offset: 0x0003B584
		protected override void MapiGetCurrentState(ref StorageIcsState finalState)
		{
			byte[] stateIdsetGiven;
			byte[] stateCnsetSeen;
			base.MapiCollector.GetState(out stateIdsetGiven, out stateCnsetSeen);
			finalState.StateIdsetGiven = stateIdsetGiven;
			finalState.StateCnsetSeen = stateCnsetSeen;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0003D3AE File Offset: 0x0003B5AE
		protected override void MapiImportDeletes(ImportDeletionFlags importDeletionFlags, PropValue[] sourceKeys)
		{
			base.MapiCollector.ImportFolderDeletion(importDeletionFlags, sourceKeys);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0003D3BD File Offset: 0x0003B5BD
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<HierarchySynchronizationUploadContext>(this);
		}
	}
}
