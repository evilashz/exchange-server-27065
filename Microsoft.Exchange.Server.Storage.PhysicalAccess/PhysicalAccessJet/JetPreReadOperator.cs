﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Windows7;
using Microsoft.Isam.Esent.Interop.Windows8;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000B5 RID: 181
	internal class JetPreReadOperator : PreReadOperator
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x00026318 File Offset: 0x00024518
		internal JetPreReadOperator(CultureInfo culture, IConnectionProvider connectionProvider, Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table, Index index, IList<KeyRange> keyRanges, IList<Column> longValueColumns, bool frequentOperation) : base(culture, connectionProvider, table, index, keyRanges, longValueColumns, frequentOperation)
		{
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0002632B File Offset: 0x0002452B
		private JetConnection JetConnection
		{
			get
			{
				return (JetConnection)base.Connection;
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00026344 File Offset: 0x00024544
		public static int PreReadKeys(JetConnection jetConnection, JET_TABLEID jetCursor, byte[][] keys, IList<Column> longValueColumns)
		{
			if (keys.Length == 0)
			{
				return 0;
			}
			int result;
			try
			{
				int num = 0;
				Array.Sort<byte[]>(keys, (byte[] x, byte[] y) => ValueHelper.ArraysCompare<byte>(x, y));
				int[] array = new int[keys.Length];
				for (int i = 0; i < keys.Length; i++)
				{
					array[i] = keys[i].Length;
				}
				if (longValueColumns != null && longValueColumns.Count > 0)
				{
					PrereadIndexRangesGrbit prereadIndexRangesGrbit = PrereadIndexRangesGrbit.Forward;
					JET_COLUMNID[] array2 = new JET_COLUMNID[longValueColumns.Count];
					for (int j = 0; j < longValueColumns.Count; j++)
					{
						JetPhysicalColumn jetPhysicalColumn = (JetPhysicalColumn)longValueColumns[j];
						array2[j] = jetPhysicalColumn.GetJetColumnId(jetConnection);
					}
					prereadIndexRangesGrbit |= PrereadIndexRangesGrbit.FirstPageOnly;
					Windows8Api.PrereadKeyRanges(jetConnection.JetSession, jetCursor, keys, array, null, null, 0, keys.Length, out num, array2, prereadIndexRangesGrbit);
				}
				else
				{
					PrereadKeysGrbit grbit = PrereadKeysGrbit.Forward;
					Windows7Api.JetPrereadKeys(jetConnection.JetSession, jetCursor, keys, array, keys.Length, out num, grbit);
				}
				result = num;
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)64560U, "JetPreReadOperator.PreReadKeys", ex);
			}
			return result;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00026464 File Offset: 0x00024664
		public static byte[][] GetKeys(JetConnection jetConnection, JET_TABLEID jetCursor, Index index, IList<StartStopKey> startKeys)
		{
			byte[][] array = new byte[startKeys.Count][];
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(BufferPoolCollection.BufferSize.Size2K);
			byte[] array2 = bufferPool.Acquire();
			try
			{
				for (int i = 0; i < startKeys.Count; i++)
				{
					JetPreReadOperator.MakeKey(jetConnection, jetCursor, index, startKeys[i]);
					int num;
					try
					{
						Api.JetRetrieveKey(jetConnection.JetSession, jetCursor, array2, array2.Length, out num, RetrieveKeyGrbit.RetrieveCopy);
					}
					catch (EsentErrorException ex)
					{
						jetConnection.OnExceptionCatch(ex);
						throw jetConnection.ProcessJetError((LID)56892U, "JetPreReadOperator.GetKeysFromPrimaryIndex", ex);
					}
					byte[] array3 = new byte[num];
					Array.Copy(array2, 0, array3, 0, num);
					array[i] = array3;
				}
			}
			finally
			{
				bufferPool.Release(array2);
			}
			return array;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00026528 File Offset: 0x00024728
		private static void MakeKey(JetConnection jetConnection, JET_TABLEID jetCursor, Index index, StartStopKey key)
		{
			try
			{
				int numberOfPartioningColumns = index.Table.SpecialCols.NumberOfPartioningColumns;
				for (int i = numberOfPartioningColumns; i < key.Count; i++)
				{
					MakeKeyGrbit grbit = (i == numberOfPartioningColumns) ? MakeKeyGrbit.NewKey : MakeKeyGrbit.None;
					JetColumnValueHelper.MakeJetKeyFromValue(jetConnection.JetSession, jetCursor, grbit, key.Values[i], index.SortOrder.Columns[i]);
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)44136U, "JetPreReadOperator.MakeKey", ex);
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000265C4 File Offset: 0x000247C4
		public override object ExecuteScalar()
		{
			int num = 0;
			base.TraceOperation("ExecuteScalar");
			JetConnection jetConnection = this.JetConnection;
			jetConnection.CountStatement(Connection.OperationType.Other);
			using (jetConnection.TrackDbOperationExecution(this))
			{
				using (jetConnection.TrackTimeInDatabase())
				{
					try
					{
						string text;
						if (!base.Table.IsPartitioned)
						{
							text = base.Table.Name;
						}
						else
						{
							Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table = base.Table;
							StartStopKey startKey = base.KeyRanges[0].StartKey;
							text = JetPartitionHelper.GetPartitionName(table, startKey.Values, base.Table.SpecialCols.NumberOfPartioningColumns);
						}
						string text2 = text;
						JET_TABLEID openTable;
						if (base.Table.IsPartitioned)
						{
							JetConnection jetConnection2 = jetConnection;
							Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table2 = base.Table;
							string tableName = text2;
							StartStopKey startKey2 = base.KeyRanges[0].StartKey;
							if (!jetConnection2.TryOpenTable(table2, tableName, startKey2.Values, Connection.OperationType.Query, out openTable))
							{
								ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, "No pre-read done because table is empty");
								return null;
							}
						}
						else
						{
							openTable = jetConnection.GetOpenTable(base.Table, text2, null, Connection.OperationType.Query);
						}
						byte[][] array;
						if (!base.Index.PrimaryKey)
						{
							array = this.GetKeysFromSecondaryIndex(jetConnection, openTable);
							num = base.KeyRanges.Count - array.Length;
						}
						else
						{
							array = this.GetKeysFromPrimaryIndex(jetConnection, openTable);
						}
						if (array.Length > 0)
						{
							num += JetPreReadOperator.PreReadKeys(jetConnection, openTable, array, base.LongValueColumns);
						}
						Api.JetCloseTable(jetConnection.JetSession, openTable);
					}
					catch (EsentErrorException ex)
					{
						jetConnection.OnExceptionCatch(ex);
						throw jetConnection.ProcessJetError((LID)60520U, "JetPreReadOperator.ExecuteScalar", ex);
					}
				}
			}
			base.TraceOperationResult("ExecuteScalar", null, num);
			return num;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000267B4 File Offset: 0x000249B4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetPreReadOperator>(this);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000267BC File Offset: 0x000249BC
		protected override void InternalDispose(bool calledFromDispose)
		{
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000267C0 File Offset: 0x000249C0
		private byte[][] GetKeysFromPrimaryIndex(JetConnection jetConnection, JET_TABLEID jetCursor)
		{
			byte[][] array = new byte[base.KeyRanges.Count][];
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(BufferPoolCollection.BufferSize.Size2K);
			byte[] array2 = bufferPool.Acquire();
			try
			{
				for (int i = 0; i < base.KeyRanges.Count; i++)
				{
					JetPreReadOperator.MakeKey(jetConnection, jetCursor, base.Index, base.KeyRanges[i].StartKey);
					int num;
					try
					{
						Api.JetRetrieveKey(jetConnection.JetSession, jetCursor, array2, array2.Length, out num, RetrieveKeyGrbit.RetrieveCopy);
					}
					catch (EsentErrorException ex)
					{
						jetConnection.OnExceptionCatch(ex);
						throw jetConnection.ProcessJetError((LID)40040U, "JetPreReadOperator.GetKeysFromPrimaryIndex", ex);
					}
					byte[] array3 = new byte[num];
					Array.Copy(array2, 0, array3, 0, num);
					array[i] = array3;
				}
			}
			finally
			{
				bufferPool.Release(array2);
			}
			return array;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000268A0 File Offset: 0x00024AA0
		private byte[][] GetKeysFromSecondaryIndex(JetConnection jetConnection, JET_TABLEID jetCursor)
		{
			byte[][] array = new byte[base.KeyRanges.Count][];
			int newSize = 0;
			try
			{
				Api.JetSetCurrentIndex(jetConnection.JetSession, jetCursor, base.Index.Name);
				for (int i = 0; i < base.KeyRanges.Count; i++)
				{
					JetPreReadOperator.MakeKey(jetConnection, jetCursor, base.Index, base.KeyRanges[i].StartKey);
					bool flag = Api.TrySeek(jetConnection.JetSession, jetCursor, SeekGrbit.SeekEQ);
					if (flag)
					{
						array[newSize++] = Api.GetBookmark(jetConnection.JetSession, jetCursor);
					}
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)56424U, "JetPreReadOperator.GetKeysFromSecondaryIndex", ex);
			}
			finally
			{
				Api.JetSetCurrentIndex(jetConnection.JetSession, jetCursor, null);
			}
			Array.Resize<byte[]>(ref array, newSize);
			return array;
		}
	}
}
