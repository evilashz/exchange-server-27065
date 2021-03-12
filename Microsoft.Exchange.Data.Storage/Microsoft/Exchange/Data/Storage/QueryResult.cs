using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200006D RID: 109
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class QueryResult : ITableView, IPagedView, IQueryResult, IDisposeTrackable, IDisposable, INotificationSource
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x0003A7E3 File Offset: 0x000389E3
		internal QueryResult(MapiTable mapiTable, ICollection<PropertyDefinition> propertyDefinitions, IList<PropTag> alteredProperties, StoreSession session, bool isTableOwned) : this(mapiTable, propertyDefinitions, alteredProperties, session, isTableOwned, null)
		{
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0003A7F4 File Offset: 0x000389F4
		internal QueryResult(MapiTable mapiTable, ICollection<PropertyDefinition> propertyDefinitions, IList<PropTag> alteredProperties, StoreSession session, bool isTableOwned, SortOrder sortOrder)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				StorageGlobals.TraceConstructIDisposable(this);
				this.disposeTracker = this.GetDisposeTracker();
				this.mapiTable = mapiTable;
				this.propertyDefinitions = propertyDefinitions;
				this.storeSession = session;
				this.isTableOwned = isTableOwned;
				this.alteredProperties = alteredProperties;
				this.isAtTheBeginningOfTable = true;
				this.SetTableColumns(propertyDefinitions);
				this.SortTable(sortOrder);
				disposeGuard.Success();
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0003A88C File Offset: 0x00038A8C
		public static PropTag[] GetColumnPropertyTags(StoreSession session, MapiProp propertyReference, ICollection<PropertyDefinition> dataColumns, out NativeStorePropertyDefinition[] nativeColumns)
		{
			ICollection<NativeStorePropertyDefinition> nativePropertyDefinitions = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.NeedForRead, dataColumns);
			nativeColumns = nativePropertyDefinitions.ToArray<NativeStorePropertyDefinition>();
			return PropertyTagCache.Cache.PropTagsFromPropertyDefinitions(propertyReference, session, nativeColumns).ToArray<PropTag>();
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0003A8BC File Offset: 0x00038ABC
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<QueryResult>(this);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0003A8C4 File Offset: 0x00038AC4
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0003A8D9 File Offset: 0x00038AD9
		private static bool SeekReferenceToBookmark(SeekReference reference, out BookMark bookMark)
		{
			bookMark = (BookMark)(reference & ~SeekReference.SeekBackward);
			return (reference & SeekReference.SeekBackward) != SeekReference.SeekBackward;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0003A954 File Offset: 0x00038B54
		private static void ExcludeProperties(PropertyTagPropertyDefinition[] excludeProperties, ref PropTag[] propertyTags)
		{
			if (excludeProperties == null || excludeProperties.Length == 0)
			{
				return;
			}
			List<PropTag> list = new List<PropTag>();
			list.AddRange(from propTag in propertyTags
			where null == excludeProperties.FirstOrDefault((PropertyTagPropertyDefinition excludedProperty) => (excludedProperty.PropertyTag & 4294901760U) == (uint)(propTag & (PropTag)4294901760U))
			select propTag);
			propertyTags = list.ToArray();
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0003A9A8 File Offset: 0x00038BA8
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0003A9CA File Offset: 0x00038BCA
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0003A9D9 File Offset: 0x00038BD9
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0003A9FE File Offset: 0x00038BFE
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.isTableOwned)
				{
					this.mapiTable.Dispose();
				}
				if (this.OnDisposing != null)
				{
					this.OnDisposing();
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0003AA3C File Offset: 0x00038C3C
		public int EstimatedRowCount
		{
			get
			{
				this.CheckDisposed("RowCount::get");
				StoreSession storeSession = this.storeSession;
				bool flag = false;
				int rowCount;
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
					rowCount = this.mapiTable.GetRowCount();
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetRowCount, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Folder::RowCount::get.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetRowCount, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Folder::RowCount::get.", new object[0]),
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
				return rowCount;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0003AB6C File Offset: 0x00038D6C
		public int CurrentRow
		{
			get
			{
				this.CheckDisposed("CurrentRow::get");
				if (this.isAtTheBeginningOfTable)
				{
					return 0;
				}
				StoreSession storeSession = this.storeSession;
				bool flag = false;
				int result;
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
					result = this.mapiTable.QueryPosition();
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetCurrentRow, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Folder::CurrentRow::get.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetCurrentRow, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Folder::CurrentRow::get.", new object[0]),
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
				return result;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0003ACA8 File Offset: 0x00038EA8
		public bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0003ACB0 File Offset: 0x00038EB0
		public bool SeekToCondition(SeekReference reference, QueryFilter seekFilter, SeekToConditionFlags flags)
		{
			this.CheckDisposed("SeekToCondition");
			EnumValidator.ThrowIfInvalid<SeekReference>(reference, "reference");
			BookMark bookMark;
			bool useForwardDirection = QueryResult.SeekReferenceToBookmark(reference, out bookMark);
			if ((flags & SeekToConditionFlags.AllowExtendedSeekReferences) == SeekToConditionFlags.None && reference != SeekReference.OriginCurrent && reference != SeekReference.OriginBeginning)
			{
				throw new NotSupportedException("Seek references other than forward-from-current/beginning require explicit enabling through SeekToConditionFlags");
			}
			return this.SeekToCondition((uint)bookMark, useForwardDirection, seekFilter, flags);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0003AD00 File Offset: 0x00038F00
		public bool SeekToCondition(uint bookMark, bool useForwardDirection, QueryFilter seekFilter, SeekToConditionFlags flags)
		{
			this.CheckDisposed("SeekToCondition");
			Util.ThrowOnNullArgument(seekFilter, "seekFilter");
			EnumValidator.ThrowIfInvalid<SeekToConditionFlags>(flags, "flags");
			if ((flags & SeekToConditionFlags.AllowExtendedFilters) == SeekToConditionFlags.None && !(seekFilter is ComparisonFilter))
			{
				throw new NotSupportedException("Filters that are more complex that simple property comparisons require explicit enabling through SeekToConditionFlags");
			}
			Restriction restriction = FilterRestrictionConverter.CreateRestriction(this.storeSession, this.storeSession.ExTimeZone, this.storeSession.Mailbox.MapiStore, seekFilter);
			if (this.isAtTheBeginningOfTable && useForwardDirection && bookMark == 1U)
			{
				bookMark = 0U;
			}
			bool flag = false;
			StoreSession storeSession = this.storeSession;
			bool flag2 = false;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag2 = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				flag = this.mapiTable.FindRow(restriction, (BookMark)bookMark, useForwardDirection ? FindRowFlag.None : FindRowFlag.FindBackward);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotFindRow, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Folder::SeekToCondition failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotFindRow, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Folder::SeekToCondition failed.", new object[0]),
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
						if (flag2)
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
			if (!flag && (flags & SeekToConditionFlags.KeepCursorPositionWhenNoMatch) == SeekToConditionFlags.None)
			{
				StoreSession storeSession2 = this.storeSession;
				bool flag3 = false;
				try
				{
					if (storeSession2 != null)
					{
						storeSession2.BeginMapiCall();
						storeSession2.BeginServerHealthCall();
						flag3 = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					this.mapiTable.SeekRow(useForwardDirection ? BookMark.End : BookMark.Beginning, 0);
				}
				catch (MapiPermanentException ex3)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSeekRow, ex3, storeSession2, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Folder::SeekToCondition failed.", new object[0]),
						ex3
					});
				}
				catch (MapiRetryableException ex4)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSeekRow, ex4, storeSession2, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Folder::SeekToCondition failed.", new object[0]),
						ex4
					});
				}
				finally
				{
					try
					{
						if (storeSession2 != null)
						{
							storeSession2.EndMapiCall();
							if (flag3)
							{
								storeSession2.EndServerHealthCall();
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
				this.isAtTheBeginningOfTable = false;
			}
			else if (flag)
			{
				this.isAtTheBeginningOfTable = false;
			}
			return flag;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0003AFF4 File Offset: 0x000391F4
		public bool SeekToCondition(SeekReference reference, QueryFilter seekFilter)
		{
			return this.SeekToCondition(reference, seekFilter, SeekToConditionFlags.None);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0003B000 File Offset: 0x00039200
		public int SeekToOffset(SeekReference reference, int offset)
		{
			this.CheckDisposed("SeekToOffset");
			EnumValidator.ThrowIfInvalid<SeekReference>(reference, "reference");
			BookMark bookmark;
			QueryResult.SeekReferenceToBookmark(reference, out bookmark);
			int result = 0;
			StoreSession storeSession = this.storeSession;
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
				result = this.mapiTable.SeekRow(bookmark, offset);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSeekRow, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Folder::SeekToOffset.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSeekRow, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Folder::SeekToOffset.", new object[0]),
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
			if (reference == SeekReference.OriginBeginning && offset == 0)
			{
				this.isAtTheBeginningOfTable = true;
			}
			else
			{
				this.isAtTheBeginningOfTable = false;
			}
			return result;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0003B160 File Offset: 0x00039360
		public void SetTableColumns(ICollection<PropertyDefinition> propertyDefinitions)
		{
			this.CheckDisposed("SetTableColumns");
			Util.ThrowOnNullArgument(propertyDefinitions, "propertyDefinitions");
			this.columns = this.SetTableColumns(propertyDefinitions, this.alteredProperties);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0003B18B File Offset: 0x0003938B
		public object[][] GetRows(int rowCount)
		{
			return this.GetRows(rowCount, QueryRowsFlags.None);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0003B198 File Offset: 0x00039398
		public object[][] GetRows(int rowCount, QueryRowsFlags flags)
		{
			bool flag;
			return this.GetRows(rowCount, flags, out flag);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0003B1AF File Offset: 0x000393AF
		public object[][] GetRows(int rowCount, out bool mightBeMoreRows)
		{
			return this.GetRows(rowCount, QueryRowsFlags.None, out mightBeMoreRows);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0003B1BC File Offset: 0x000393BC
		public virtual object[][] GetRows(int rowCount, QueryRowsFlags flags, out bool mightBeMoreRows)
		{
			this.CheckDisposed("GetRows");
			EnumValidator.ThrowIfInvalid<QueryRowsFlags>(flags, "flags");
			PropValue[][] array = this.Fetch(rowCount, flags);
			mightBeMoreRows = (array.Length > 0);
			return this.PropValuesToObjectArray(array);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0003B1F8 File Offset: 0x000393F8
		public virtual object[][] ExpandRow(int rowCount, long categoryId, out int rowsInExpandedCategory)
		{
			this.CheckDisposed("ExpandRow");
			if (rowCount < 0)
			{
				throw new ArgumentOutOfRangeException("rowCount", ServerStrings.ExInvalidRowCount);
			}
			PropValue[][] propertyValues = null;
			StoreSession storeSession = this.storeSession;
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
				propertyValues = this.mapiTable.ExpandRow(categoryId, rowCount, 0, out rowsInExpandedCategory);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotExpandRow, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::ExpandRow. categoryId = {0}, rowCount = {1}.", categoryId, rowCount),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotExpandRow, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::ExpandRow. categoryId = {0}, rowCount = {1}.", categoryId, rowCount),
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
			return this.PropValuesToObjectArray(propertyValues);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0003B35C File Offset: 0x0003955C
		public virtual int CollapseRow(long categoryId)
		{
			this.CheckDisposed("CollapseRow");
			StoreSession storeSession = this.storeSession;
			bool flag = false;
			int result;
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
				result = this.mapiTable.CollapseRow(categoryId, 0);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCollapseRow, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::CollapseRow. categoryId = {0}.", categoryId),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCollapseRow, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::CollapseRow. categoryId = {0}.", categoryId),
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
			return result;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0003B490 File Offset: 0x00039690
		public uint CreateBookmark()
		{
			this.CheckDisposed("CreateBookmark");
			StoreSession storeSession = this.storeSession;
			bool flag = false;
			uint result;
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
				result = (uint)this.mapiTable.CreateBookmark();
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateBookmark, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::CreateBookmark failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateBookmark, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::CreateBookmark failed.", new object[0]),
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
			return result;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0003B5C0 File Offset: 0x000397C0
		public void FreeBookmark(uint bookmarkPosition)
		{
			this.CheckDisposed("FreeBookmark");
			StoreSession storeSession = this.storeSession;
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
				this.mapiTable.FreeBookmark((BookMark)bookmarkPosition);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotFreeBookmark, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::FreeBookmark failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotFreeBookmark, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::FreeBookmark failed.", new object[0]),
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

		// Token: 0x060007A0 RID: 1952 RVA: 0x0003B6EC File Offset: 0x000398EC
		public int SeekRowBookmark(uint bookmarkPosition, int rowCount, bool wantRowsSought, out bool soughtLess, out bool positionChanged)
		{
			this.CheckDisposed("SeekRowBookmark");
			StoreSession storeSession = this.storeSession;
			bool flag = false;
			int result;
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
				result = this.mapiTable.SeekRowBookmark((BookMark)bookmarkPosition, rowCount, wantRowsSought, out soughtLess, out positionChanged);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSeekRowBookmark, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::SeekRowBookmark failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSeekRowBookmark, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::SeekRowBookmark failed.", new object[0]),
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
			return result;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0003B824 File Offset: 0x00039A24
		public NativeStorePropertyDefinition[] GetAllPropertyDefinitions(params PropertyTagPropertyDefinition[] excludeProperties)
		{
			this.CheckDisposed("GetAllPropertyDefinitions");
			StoreSession storeSession = this.storeSession;
			bool flag = false;
			PropTag[] propTags;
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
				propTags = this.mapiTable.QueryColumns(QueryColumnsFlags.AllColumns);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotQueryColumns, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::QueryColumns", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotQueryColumns, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::QueryColumns", new object[0]),
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
			QueryResult.ExcludeProperties(excludeProperties, ref propTags);
			NativeStorePropertyDefinition[] array = PropertyTagCache.Cache.PropertyDefinitionsFromPropTags(NativeStorePropertyDefinition.TypeCheckingFlag.DoNotCreateInvalidType, this.StoreSession.Mailbox.MapiStore, this.StoreSession, propTags);
			List<NativeStorePropertyDefinition> list = new List<NativeStorePropertyDefinition>();
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in array)
			{
				if (nativeStorePropertyDefinition != null)
				{
					list.Add(nativeStorePropertyDefinition);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0003B9B8 File Offset: 0x00039BB8
		public virtual byte[] GetCollapseState(byte[] instanceKey)
		{
			this.CheckDisposed("GetCollapseState");
			Util.ThrowOnNullArgument(instanceKey, "instanceKey");
			byte[] result = Array<byte>.Empty;
			StoreSession storeSession = this.storeSession;
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
				result = this.mapiTable.GetCollapseState(instanceKey);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetCollapseState, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::GetCollapseState. failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetCollapseState, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::GetCollapseState. failed.", new object[0]),
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
			return result;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0003BAFC File Offset: 0x00039CFC
		public virtual uint SetCollapseState(byte[] collapseState)
		{
			this.CheckDisposed("SetCollapseState");
			Util.ThrowOnNullArgument(collapseState, "collapseState");
			StoreSession storeSession = this.storeSession;
			bool flag = false;
			uint result;
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
				result = (uint)this.mapiTable.SetCollapseState(collapseState);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetCollapseState, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::SetCollapseState failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetCollapseState, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::SetCollapseState failed.", new object[0]),
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
			this.isAtTheBeginningOfTable = false;
			return result;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0003BC40 File Offset: 0x00039E40
		public object Advise(SubscriptionSink subscriptionSink, bool asyncMode)
		{
			this.CheckDisposed("Advise");
			Util.ThrowOnNullArgument(subscriptionSink, "subscriptionSink");
			StoreSession storeSession = this.StoreSession;
			bool flag = false;
			object result;
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
				result = this.MapiTable.Advise(new MapiNotificationHandler(subscriptionSink.OnNotify), asyncMode ? NotificationCallbackMode.Async : NotificationCallbackMode.Sync);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotAddNotification, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::Advise failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotAddNotification, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::Advise failed.", new object[0]),
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
			return result;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0003BD90 File Offset: 0x00039F90
		public void Unadvise(object notificationHandle)
		{
			this.CheckDisposed("Unadvise");
			Util.ThrowOnNullArgument(notificationHandle, "notificationHandle");
			StoreSession storeSession = this.StoreSession;
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
				if (this.MapiTable != null && !this.MapiTable.IsDisposed)
				{
					this.MapiTable.Unadvise((MapiNotificationHandle)notificationHandle);
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotRemoveNotification, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::Unadvise failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotRemoveNotification, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::Unadvise failed.", new object[0]),
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

		// Token: 0x060007A6 RID: 1958 RVA: 0x0003BEE0 File Offset: 0x0003A0E0
		public virtual IStorePropertyBag[] GetPropertyBags(int rowCount)
		{
			this.CheckDisposed("GetPropertyBags");
			if (rowCount < 0)
			{
				throw new ArgumentOutOfRangeException("rowCount", ServerStrings.ExInvalidRowCount);
			}
			PropValue[][] array = this.Fetch(rowCount);
			IStorePropertyBag[] array2 = new IStorePropertyBag[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				QueryResultPropertyBag queryResultPropertyBag = new QueryResultPropertyBag(this.HeaderPropBag);
				queryResultPropertyBag.SetQueryResultRow(array[i]);
				array2[i] = queryResultPropertyBag.AsIStorePropertyBag();
			}
			return array2;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0003BF4F File Offset: 0x0003A14F
		protected QueryResultPropertyBag HeaderPropBag
		{
			get
			{
				if (this.headerPropBag == null)
				{
					this.headerPropBag = new QueryResultPropertyBag(this.storeSession, this.columns.SimplePropertyDefinitions);
				}
				return this.headerPropBag;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0003BF7B File Offset: 0x0003A17B
		protected ICollection<PropertyDefinition> PropertyDefinitions
		{
			get
			{
				return this.propertyDefinitions;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0003BF83 File Offset: 0x0003A183
		protected Schema Schema
		{
			get
			{
				return this.storeSession.Schema;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0003BF90 File Offset: 0x0003A190
		public StoreSession StoreSession
		{
			get
			{
				this.CheckDisposed("StoreSession");
				return this.storeSession;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0003BFA3 File Offset: 0x0003A1A3
		public ColumnPropertyDefinitions Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0003BFAC File Offset: 0x0003A1AC
		internal static bool DoPropertyValuesMatchColumns(ColumnPropertyDefinitions columns, PropValue[] values)
		{
			if (columns.PropertyTags.Count != values.Length)
			{
				return false;
			}
			int num = 0;
			foreach (PropTag propTag in columns.PropertyTags)
			{
				if (propTag.Id() != values[num++].PropTag.Id())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0003C02C File Offset: 0x0003A22C
		bool INotificationSource.IsDisposedOrDead
		{
			get
			{
				return this.IsDisposed || this.mapiTable.IsDisposed;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0003C043 File Offset: 0x0003A243
		private MapiTable MapiTable
		{
			get
			{
				return this.mapiTable;
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0003C04C File Offset: 0x0003A24C
		private ColumnPropertyDefinitions SetTableColumns(ICollection<PropertyDefinition> propertyDefinitions, IList<PropTag> alteredProperties)
		{
			ICollection<NativeStorePropertyDefinition> nativePropertyDefinitions = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.NeedForRead, propertyDefinitions);
			ICollection<PropTag> collection = PropertyTagCache.Cache.PropTagsFromPropertyDefinitions(this.storeSession.Mailbox.MapiStore, this.storeSession, nativePropertyDefinitions);
			HashSet<int> hashSet = new HashSet<int>(collection.Count);
			int num = 0;
			foreach (PropTag item in collection)
			{
				if (hashSet.Contains((int)item))
				{
					num++;
					if (num > 6)
					{
						throw new ArgumentException(ServerStrings.ExTooManyDuplicateDataColumns(6), "propertyDefinitions");
					}
				}
				else
				{
					hashSet.Add((int)item);
				}
			}
			if (alteredProperties != null && alteredProperties.Count > 0 && collection.Count > 0)
			{
				PropTag[] array = new PropTag[collection.Count];
				collection.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					for (int j = 0; j < alteredProperties.Count; j++)
					{
						if (array[i] == alteredProperties[j])
						{
							array[i] |= (PropTag)12288U;
						}
					}
				}
				collection = array;
			}
			ColumnPropertyDefinitions columnPropertyDefinitions = new ColumnPropertyDefinitions(propertyDefinitions, nativePropertyDefinitions.ToArray<NativeStorePropertyDefinition>(), collection);
			StoreSession storeSession = this.storeSession;
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
				this.mapiTable.SetColumns(columnPropertyDefinitions.PropertyTags);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetTableColumns, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Folder::SetTableColumns. Failed to set columns to the table.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetTableColumns, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Folder::SetTableColumns. Failed to set columns to the table.", new object[0]),
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
			return columnPropertyDefinitions;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0003C2A8 File Offset: 0x0003A4A8
		private void SortTable(SortOrder sortOrder)
		{
			if (sortOrder == null)
			{
				return;
			}
			StoreSession storeSession = this.storeSession;
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
				this.mapiTable.SortTable(sortOrder, SortTableFlags.Batch);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSortTable, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Folder::SortTable. Failed due to sort a table. sortOrder = {0}; SortTableFlags = {1}.", sortOrder, SortTableFlags.Batch),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSortTable, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Folder::SortTable. Failed due to sort a table. sortOrder = {0}; SortTableFlags = {1}.", sortOrder, SortTableFlags.Batch),
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

		// Token: 0x060007B1 RID: 1969 RVA: 0x0003C3D0 File Offset: 0x0003A5D0
		protected PropValue[][] Fetch(int rowCount)
		{
			return this.Fetch(rowCount, QueryRowsFlags.None);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0003C3DC File Offset: 0x0003A5DC
		protected PropValue[][] Fetch(int rowCount, QueryRowsFlags flags)
		{
			QueryRowsFlags flags2 = ((flags & QueryRowsFlags.NoAdvance) == QueryRowsFlags.NoAdvance) ? QueryRowsFlags.NoAdvance : QueryRowsFlags.None;
			PropValue[][] result = null;
			StoreSession storeSession = this.storeSession;
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
				result = this.mapiTable.QueryRows(rowCount, flags2);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotQueryRows, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::Fetch. rowCount = {0}.", rowCount),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotQueryRows, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("QueryResult::Fetch. rowCount = {0}.", rowCount),
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
			this.isAtTheBeginningOfTable = false;
			return result;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0003C51C File Offset: 0x0003A71C
		private object[][] PropValuesToObjectArray(PropValue[][] propertyValues)
		{
			object[][] array = new object[propertyValues.Length][];
			if (propertyValues.Length > 0)
			{
				ColumnPropertyDefinitions columnPropertyDefinitions = this.Columns;
				QueryResultPropertyBag queryResultPropertyBag = new QueryResultPropertyBag(this.storeSession, columnPropertyDefinitions.SimplePropertyDefinitions);
				for (int i = 0; i < propertyValues.Length; i++)
				{
					queryResultPropertyBag.SetQueryResultRow(propertyValues[i]);
					array[i] = queryResultPropertyBag.GetProperties(columnPropertyDefinitions.PropertyDefinitions);
				}
			}
			return array;
		}

		// Token: 0x0400020D RID: 525
		private const int MaxDuplicateTableColumns = 6;

		// Token: 0x0400020E RID: 526
		public const int MaxRows = 10000;

		// Token: 0x0400020F RID: 527
		private readonly MapiTable mapiTable;

		// Token: 0x04000210 RID: 528
		private readonly bool isTableOwned = true;

		// Token: 0x04000211 RID: 529
		private readonly StoreSession storeSession;

		// Token: 0x04000212 RID: 530
		private readonly IList<PropTag> alteredProperties;

		// Token: 0x04000213 RID: 531
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000214 RID: 532
		private bool isDisposed;

		// Token: 0x04000215 RID: 533
		private ICollection<PropertyDefinition> propertyDefinitions;

		// Token: 0x04000216 RID: 534
		private ColumnPropertyDefinitions columns;

		// Token: 0x04000217 RID: 535
		private bool isAtTheBeginningOfTable;

		// Token: 0x04000218 RID: 536
		private QueryResultPropertyBag headerPropBag;

		// Token: 0x04000219 RID: 537
		public Action OnDisposing;
	}
}
