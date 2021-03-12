using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000A1 RID: 161
	internal class OpenMessageStates : IComponentData
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x0004D474 File Offset: 0x0004B674
		internal OpenMessageStates()
		{
			this.openStates = new Dictionary<int, OpenMessageStates.OpenMessageState>(8);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0004D488 File Offset: 0x0004B688
		internal static void Initialize()
		{
			if (OpenMessageStates.openMessageStateSlot == -1)
			{
				OpenMessageStates.openMessageStateSlot = MailboxState.AllocateComponentDataSlot(false);
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0004D4A0 File Offset: 0x0004B6A0
		internal static OpenMessageInstance AddInstance(Context context, Mailbox mailbox, int documentId, DataRow dataRow)
		{
			OpenMessageStates cachedForMailbox = OpenMessageStates.GetCachedForMailbox(mailbox.SharedState, true);
			OpenMessageInstance result;
			using (LockManager.Lock(cachedForMailbox.openStates))
			{
				OpenMessageStates.OpenMessageState openMessageState;
				if (!cachedForMailbox.TryGetOpenMessageState(documentId, out openMessageState))
				{
					openMessageState = new OpenMessageStates.OpenMessageState(cachedForMailbox, documentId);
					cachedForMailbox.AddDocumentId(documentId, openMessageState);
				}
				OpenMessageInstance openMessageInstance = openMessageState.AddInstance(context, dataRow);
				result = openMessageInstance;
			}
			return result;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0004D510 File Offset: 0x0004B710
		internal static bool DoesOpenMessageStateExist(Context context, Mailbox mailbox, int documentId)
		{
			OpenMessageStates cachedForMailbox = OpenMessageStates.GetCachedForMailbox(mailbox.SharedState, false);
			if (cachedForMailbox == null)
			{
				return false;
			}
			bool result;
			using (LockManager.Lock(cachedForMailbox.openStates))
			{
				OpenMessageStates.OpenMessageState openMessageState = null;
				result = cachedForMailbox.TryGetOpenMessageState(documentId, out openMessageState);
			}
			return result;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0004D568 File Offset: 0x0004B768
		internal static void RemoveInstance(Context context, Mailbox mailbox, OpenMessageInstance instance)
		{
			OpenMessageStates.OpenMessageState state = instance.State;
			OpenMessageStates states = state.States;
			using (LockManager.Lock(states.openStates))
			{
				state.RemoveInstance(context, instance);
				if (state.Instances == null || state.Instances.Count == 0)
				{
					states.RemoveDocumentId(state.DocumentId);
				}
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0004D5D8 File Offset: 0x0004B7D8
		internal static void OnBeforeFlushOrDelete(Context context, OpenMessageInstance instance, bool delete, bool move, bool nonConflictingChange)
		{
			OpenMessageStates.OpenMessageState state = instance.State;
			OpenMessageStates states = state.States;
			using (LockManager.Lock(states.openStates))
			{
				state.OnBeforeInstanceFlushOrDelete(context, instance, delete, move, nonConflictingChange);
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0004D62C File Offset: 0x0004B82C
		private static OpenMessageStates GetCachedForMailbox(MailboxState mailboxState, bool create)
		{
			OpenMessageStates openMessageStates = (OpenMessageStates)mailboxState.GetComponentData(OpenMessageStates.openMessageStateSlot);
			if (openMessageStates == null && create)
			{
				openMessageStates = new OpenMessageStates();
				OpenMessageStates openMessageStates2 = (OpenMessageStates)mailboxState.CompareExchangeComponentData(OpenMessageStates.openMessageStateSlot, null, openMessageStates);
				if (openMessageStates2 != null)
				{
					openMessageStates = openMessageStates2;
				}
			}
			return openMessageStates;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0004D66F File Offset: 0x0004B86F
		bool IComponentData.DoCleanup(Context context)
		{
			return this.openStates.Count == 0;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0004D67F File Offset: 0x0004B87F
		private bool TryGetOpenMessageState(int documentId, out OpenMessageStates.OpenMessageState value)
		{
			return this.openStates.TryGetValue(documentId, out value);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0004D68E File Offset: 0x0004B88E
		private void AddDocumentId(int documentId, OpenMessageStates.OpenMessageState value)
		{
			this.openStates.Add(documentId, value);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0004D69D File Offset: 0x0004B89D
		private void RemoveDocumentId(int documentId)
		{
			this.openStates.Remove(documentId);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0004D6AC File Offset: 0x0004B8AC
		private void InvokeOnCommit(Context context, OpenMessageStates.OpenMessageState openMessageState)
		{
			using (LockManager.Lock(this.openStates))
			{
				openMessageState.OnCommitImplementation(context);
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0004D6EC File Offset: 0x0004B8EC
		private void InvokeOnAbort(Context context, OpenMessageStates.OpenMessageState openMessageState)
		{
			using (LockManager.Lock(this.openStates))
			{
				openMessageState.OnAbortImplementation(context);
			}
		}

		// Token: 0x04000485 RID: 1157
		private static int openMessageStateSlot = -1;

		// Token: 0x04000486 RID: 1158
		private Dictionary<int, OpenMessageStates.OpenMessageState> openStates;

		// Token: 0x020000A2 RID: 162
		internal class OpenMessageState : IStateObject
		{
			// Token: 0x06000947 RID: 2375 RVA: 0x0004D734 File Offset: 0x0004B934
			internal OpenMessageState(OpenMessageStates states, int documentId)
			{
				this.states = states;
				this.documentId = documentId;
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x06000948 RID: 2376 RVA: 0x0004D74A File Offset: 0x0004B94A
			internal IList<OpenMessageInstance> Instances
			{
				get
				{
					return this.instances;
				}
			}

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x06000949 RID: 2377 RVA: 0x0004D752 File Offset: 0x0004B952
			internal int DocumentId
			{
				get
				{
					return this.documentId;
				}
			}

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x0600094A RID: 2378 RVA: 0x0004D75A File Offset: 0x0004B95A
			internal OpenMessageStates States
			{
				get
				{
					return this.states;
				}
			}

			// Token: 0x0600094B RID: 2379 RVA: 0x0004D764 File Offset: 0x0004B964
			internal void OnBeforeInstanceFlushOrDelete(Context context, OpenMessageInstance instance, bool delete, bool move, bool nonConflictingChange)
			{
				using (context.CriticalBlock((LID)36832U, CriticalBlockScope.MailboxShared))
				{
					if (!context.IsStateObjectRegistered(this))
					{
						context.RegisterStateObject(this);
					}
					instance.Tentative = true;
					if (this.instances != null && this.instances.Count != 0 && (this.instances.Count > 1 || this.instances[0] != instance))
					{
						IList<PhysicalColumn> list = instance.DataRow.Table.Columns;
						if (!delete)
						{
							list = new List<PhysicalColumn>(8);
							foreach (PhysicalColumn physicalColumn in instance.DataRow.Table.Columns)
							{
								if (instance.DataRow.ColumnDirty(physicalColumn))
								{
									list.Add(physicalColumn);
								}
							}
						}
						foreach (OpenMessageInstance openMessageInstance in this.instances)
						{
							if (!object.ReferenceEquals(openMessageInstance, instance))
							{
								bool flag = true;
								if (!delete && nonConflictingChange)
								{
									if (openMessageInstance.Current)
									{
										flag = false;
										foreach (PhysicalColumn column in list)
										{
											if (openMessageInstance.DataRow.ColumnDirty(column))
											{
												flag = true;
												break;
											}
										}
									}
									foreach (PhysicalColumn physicalColumn2 in list)
									{
										if (!openMessageInstance.DataRow.ColumnDirty(physicalColumn2))
										{
											if ((openMessageInstance.Current && !openMessageInstance.Tentative) || (!openMessageInstance.Current && openMessageInstance.Tentative))
											{
												bool flag2 = false;
												if (this.tentativelyOverridenColumns != null)
												{
													foreach (ColumnValue columnValue in this.tentativelyOverridenColumns)
													{
														if (columnValue.Column == physicalColumn2)
														{
															flag2 = true;
															break;
														}
													}
												}
												if (!flag2)
												{
													if (this.tentativelyOverridenColumns == null)
													{
														this.tentativelyOverridenColumns = new List<ColumnValue>(list.Count);
													}
													this.tentativelyOverridenColumns.Add(new ColumnValue(physicalColumn2, openMessageInstance.DataRow.GetValue(context, physicalColumn2)));
												}
											}
											openMessageInstance.DataRow.SetValue(context, physicalColumn2, instance.DataRow.GetValue(context, physicalColumn2), true);
										}
									}
								}
								if (flag)
								{
									openMessageInstance.DataRow.Load(context, list, true);
									if (openMessageInstance.Current)
									{
										openMessageInstance.DataRow.MarkDisconnected();
										openMessageInstance.Current = false;
										openMessageInstance.Tentative = !openMessageInstance.Tentative;
										openMessageInstance.Moved = move;
										openMessageInstance.Deleted = delete;
									}
								}
							}
						}
					}
					context.EndCriticalBlock();
				}
			}

			// Token: 0x0600094C RID: 2380 RVA: 0x0004DAE0 File Offset: 0x0004BCE0
			void IStateObject.OnBeforeCommit(Context context)
			{
			}

			// Token: 0x0600094D RID: 2381 RVA: 0x0004DAE2 File Offset: 0x0004BCE2
			void IStateObject.OnCommit(Context context)
			{
				this.States.InvokeOnCommit(context, this);
			}

			// Token: 0x0600094E RID: 2382 RVA: 0x0004DAF1 File Offset: 0x0004BCF1
			void IStateObject.OnAbort(Context context)
			{
				this.States.InvokeOnAbort(context, this);
			}

			// Token: 0x0600094F RID: 2383 RVA: 0x0004DB00 File Offset: 0x0004BD00
			internal void OnCommitImplementation(Context context)
			{
				if (this.instances != null)
				{
					foreach (OpenMessageInstance openMessageInstance in this.instances)
					{
						openMessageInstance.Tentative = false;
					}
				}
				this.tentativelyOverridenColumns = null;
			}

			// Token: 0x06000950 RID: 2384 RVA: 0x0004DB64 File Offset: 0x0004BD64
			internal void OnAbortImplementation(Context context)
			{
				using (context.CriticalBlock((LID)53216U, CriticalBlockScope.MailboxShared))
				{
					if (this.instances != null)
					{
						foreach (OpenMessageInstance openMessageInstance in this.instances)
						{
							if (openMessageInstance.Tentative)
							{
								openMessageInstance.Tentative = false;
								openMessageInstance.Moved = false;
								openMessageInstance.Deleted = false;
								if (!openMessageInstance.Current)
								{
									openMessageInstance.Current = true;
									openMessageInstance.DataRow.MarkReconnected();
								}
								else
								{
									openMessageInstance.Current = false;
									openMessageInstance.DataRow.MarkDisconnected();
								}
							}
							if (this.tentativelyOverridenColumns != null)
							{
								bool flag = false;
								foreach (ColumnValue columnValue in this.tentativelyOverridenColumns)
								{
									if (openMessageInstance.DataRow.ColumnDirty((PhysicalColumn)columnValue.Column))
									{
										flag = true;
									}
									else
									{
										openMessageInstance.DataRow.SetValue(context, (PhysicalColumn)columnValue.Column, columnValue.Value, true);
									}
								}
								if (flag && openMessageInstance.Current)
								{
									openMessageInstance.Current = false;
									openMessageInstance.DataRow.MarkDisconnected();
								}
							}
						}
					}
					this.tentativelyOverridenColumns = null;
					context.EndCriticalBlock();
				}
			}

			// Token: 0x06000951 RID: 2385 RVA: 0x0004DD10 File Offset: 0x0004BF10
			internal OpenMessageInstance AddInstance(Context context, DataRow dataRow)
			{
				if (this.instances == null)
				{
					this.instances = new List<OpenMessageInstance>(2);
				}
				OpenMessageInstance openMessageInstance = new OpenMessageInstance(this, dataRow);
				this.instances.Add(openMessageInstance);
				if (context.TransactionStarted)
				{
					if (!context.IsStateObjectRegistered(this))
					{
						context.RegisterStateObject(this);
					}
					openMessageInstance.Tentative = true;
				}
				this.CleanUnreferencedInstances(context);
				return openMessageInstance;
			}

			// Token: 0x06000952 RID: 2386 RVA: 0x0004DD6C File Offset: 0x0004BF6C
			internal void RemoveInstance(Context context, OpenMessageInstance instance)
			{
				this.instances.Remove(instance);
				if (this.instances.Count == 0)
				{
					if (context != null)
					{
						context.UnregisterStateObject(this);
					}
					this.instances = null;
				}
			}

			// Token: 0x06000953 RID: 2387 RVA: 0x0004DD9C File Offset: 0x0004BF9C
			private void CleanUnreferencedInstances(Context context)
			{
				if (this.instances != null)
				{
					for (int i = this.instances.Count - 1; i >= 0; i--)
					{
						OpenMessageInstance openMessageInstance = this.instances[i];
						if (!openMessageInstance.Referenced)
						{
							this.RemoveInstance(context, openMessageInstance);
						}
					}
				}
			}

			// Token: 0x04000487 RID: 1159
			private readonly OpenMessageStates states;

			// Token: 0x04000488 RID: 1160
			private readonly int documentId;

			// Token: 0x04000489 RID: 1161
			private List<OpenMessageInstance> instances;

			// Token: 0x0400048A RID: 1162
			private List<ColumnValue> tentativelyOverridenColumns;
		}
	}
}
