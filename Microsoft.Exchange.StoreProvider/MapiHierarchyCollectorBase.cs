using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001C6 RID: 454
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiHierarchyCollectorBase : MapiUnk
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x00018AA1 File Offset: 0x00016CA1
		internal MapiHierarchyCollectorBase(IExImportHierarchyChanges iExchangeImportHierarchyChanges, MapiStore mapiStore) : base(iExchangeImportHierarchyChanges, null, mapiStore)
		{
			this.iExchangeImportHierarchyChanges = iExchangeImportHierarchyChanges;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00018AB3 File Offset: 0x00016CB3
		protected override void MapiInternalDispose()
		{
			this.iExchangeImportHierarchyChanges = null;
			base.MapiInternalDispose();
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00018AC2 File Offset: 0x00016CC2
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiHierarchyCollectorBase>(this);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00018ACA File Offset: 0x00016CCA
		internal IntPtr ExchangeCollector
		{
			get
			{
				base.CheckDisposed();
				return ((SafeExImportHierarchyChangesHandle)this.iExchangeImportHierarchyChanges).DangerousGetHandle();
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00018AE4 File Offset: 0x00016CE4
		public unsafe void ImportFolderChange(PropValue[] propValues)
		{
			base.CheckDisposed();
			if (propValues == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("propValues");
			}
			if (propValues.Length <= 0)
			{
				throw MapiExceptionHelper.ArgumentOutOfRangeException("propValues", "values must contain at least 1 element");
			}
			base.LockStore();
			try
			{
				int num = 0;
				for (int i = 0; i < propValues.Length; i++)
				{
					num += propValues[i].GetBytesToMarshal();
				}
				try
				{
					fixed (byte* ptr = new byte[num])
					{
						PropValue.MarshalToNative(propValues, ptr);
						int num2 = this.iExchangeImportHierarchyChanges.ImportFolderChange(propValues.Length, (SPropValue*)ptr);
						if (num2 != 0)
						{
							base.ThrowIfErrorOrWarning("Unable to import folder change.", num2);
						}
					}
				}
				finally
				{
					byte* ptr = null;
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00018BB4 File Offset: 0x00016DB4
		public unsafe void ImportFolderDeletion(ImportDeletionFlags importDeletionFlags, params PropValue[] sourceKeys)
		{
			base.CheckDisposed();
			if (sourceKeys == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("sourceKeys");
			}
			if (sourceKeys.Length <= 0)
			{
				throw MapiExceptionHelper.ArgumentOutOfRangeException("sourceKeys", "sourceKeys must contain at least 1 element");
			}
			base.LockStore();
			try
			{
				SBinary[] array = new SBinary[sourceKeys.Length];
				for (int i = 0; i < sourceKeys.Length; i++)
				{
					array[i] = new SBinary(sourceKeys[i].GetBytes());
				}
				int bytesToMarshal = SBinaryArray.GetBytesToMarshal(array);
				try
				{
					fixed (byte* ptr = new byte[bytesToMarshal])
					{
						SBinaryArray.MarshalToNative(ptr, array);
						int num = this.iExchangeImportHierarchyChanges.ImportFolderDeletion((int)importDeletionFlags, (_SBinaryArray*)ptr);
						if (num != 0)
						{
							base.ThrowIfErrorOrWarning("Unable to import folder deletion.", num);
						}
					}
				}
				finally
				{
					byte* ptr = null;
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00018C98 File Offset: 0x00016E98
		public void ImportFolderDeletion(params PropValue[] sourceKeys)
		{
			this.ImportFolderDeletion(ImportDeletionFlags.None, sourceKeys);
		}

		// Token: 0x04000611 RID: 1553
		private IExImportHierarchyChanges iExchangeImportHierarchyChanges;
	}
}
