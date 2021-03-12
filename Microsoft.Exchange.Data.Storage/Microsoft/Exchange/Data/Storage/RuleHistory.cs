using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000A1 RID: 161
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RuleHistory : ICollection<long>, IEnumerable<long>, IEnumerable
	{
		// Token: 0x06000B3E RID: 2878 RVA: 0x0004EEF8 File Offset: 0x0004D0F8
		internal RuleHistory(Item item, byte[] ruleHistoryData, StoreSession session)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (ruleHistoryData == null)
			{
				throw new ArgumentNullException("ruleHistoryData");
			}
			this.item = item;
			this.gids = new Collection<byte[]>();
			this.session = session;
			this.Initialize(ruleHistoryData);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0004EF48 File Offset: 0x0004D148
		private void Initialize(byte[] ruleHistoryData)
		{
			int num = ruleHistoryData.Length / 22;
			int i = 0;
			int num2 = 0;
			while (i < num)
			{
				byte[] destinationArray = new byte[22];
				Array.Copy(ruleHistoryData, num2, destinationArray, 0, 22);
				this.gids.Add(destinationArray);
				i++;
				num2 += 22;
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0004EF90 File Offset: 0x0004D190
		public void Save()
		{
			byte[] value = this.Serialize();
			this.item[ItemSchema.RuleTriggerHistory] = value;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0004EFB8 File Offset: 0x0004D1B8
		private byte[] Serialize()
		{
			byte[] array = new byte[22 * this.gids.Count];
			int num = 0;
			foreach (byte[] sourceArray in this.gids)
			{
				Array.Copy(sourceArray, 0, array, num, 22);
				num += 22;
			}
			return array;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0004F028 File Offset: 0x0004D228
		public void Add(long item)
		{
			StoreSession storeSession = this.session;
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
				this.gids.Add(this.session.Mailbox.MapiStore.GlobalIdFromId(item));
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.RuleHistoryError, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("RuleHistory.Add. item = {0}.", item),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.RuleHistoryError, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("RuleHistory.Add. item = {0}.", item),
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

		// Token: 0x06000B43 RID: 2883 RVA: 0x0004F160 File Offset: 0x0004D360
		public void Clear()
		{
			this.gids.Clear();
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0004F170 File Offset: 0x0004D370
		public bool Contains(long item)
		{
			StoreSession storeSession = this.session;
			bool flag = false;
			byte[] x;
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
				x = this.session.Mailbox.MapiStore.GlobalIdFromId(item);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.RuleHistoryError, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("RuleHistory.Contains. item = {0}.", item),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.RuleHistoryError, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("RuleHistory.Contains. item = {0}.", item),
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
			ArrayComparer<byte> comparer = ArrayComparer<byte>.Comparer;
			foreach (byte[] y in this.gids)
			{
				if (comparer.Equals(x, y))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0004F2F8 File Offset: 0x0004D4F8
		public void CopyTo(long[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0004F2FF File Offset: 0x0004D4FF
		public int Count
		{
			get
			{
				return this.gids.Count;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0004F30C File Offset: 0x0004D50C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0004F310 File Offset: 0x0004D510
		public bool Remove(long item)
		{
			StoreSession storeSession = this.session;
			bool flag = false;
			bool result;
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
				result = this.gids.Remove(this.session.Mailbox.MapiStore.GlobalIdFromId(item));
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.RuleHistoryError, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("RuleHistory.Remove. item = {0}.", item),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.RuleHistoryError, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("RuleHistory.Remove. item = {0}.", item),
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

		// Token: 0x06000B49 RID: 2889 RVA: 0x0004F44C File Offset: 0x0004D64C
		public IEnumerator<long> GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0004F453 File Offset: 0x0004D653
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000312 RID: 786
		internal const int GidSize = 22;

		// Token: 0x04000313 RID: 787
		private readonly Item item;

		// Token: 0x04000314 RID: 788
		private readonly StoreSession session;

		// Token: 0x04000315 RID: 789
		private readonly Collection<byte[]> gids;
	}
}
