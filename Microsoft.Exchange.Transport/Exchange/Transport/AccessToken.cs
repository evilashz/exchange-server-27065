using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000327 RID: 807
	internal class AccessToken
	{
		// Token: 0x060022F5 RID: 8949 RVA: 0x0008463C File Offset: 0x0008283C
		public AccessToken(WaitCondition condition, NextHopSolutionKey queue, WaitConditionManager issuingMap)
		{
			this.condition = condition;
			this.queue = queue;
			this.issuingMap = issuingMap;
			lock (AccessToken.validEpochs)
			{
				this.epoch = 0;
				if (!AccessToken.validEpochs.TryGetValue(condition, out this.epoch))
				{
					AccessToken.validEpochs[condition] = 0;
				}
			}
			lock (AccessToken.outstandingCount)
			{
				int num;
				if (!AccessToken.outstandingCount.TryGetValue(condition, out num))
				{
					AccessToken.outstandingCount[condition] = 1;
				}
				else
				{
					num = (AccessToken.outstandingCount[condition] = num + 1);
				}
			}
			this.valid = true;
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x00084714 File Offset: 0x00082914
		public static void ReclaimAll(WaitCondition condition)
		{
			lock (AccessToken.validEpochs)
			{
				if (AccessToken.validEpochs.ContainsKey(condition))
				{
					Dictionary<WaitCondition, int> dictionary;
					(dictionary = AccessToken.validEpochs)[condition] = dictionary[condition] + 1;
				}
			}
			lock (AccessToken.outstandingCount)
			{
				AccessToken.outstandingCount.Remove(condition);
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x000847AC File Offset: 0x000829AC
		internal static Dictionary<WaitCondition, int>.Enumerator OutstandingCountsEnumerator
		{
			get
			{
				return AccessToken.outstandingCount.GetEnumerator();
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060022F8 RID: 8952 RVA: 0x000847B8 File Offset: 0x000829B8
		public WaitCondition Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000847C0 File Offset: 0x000829C0
		public bool Return(bool returnToIssuingMap)
		{
			if (this.valid)
			{
				this.valid = false;
				lock (AccessToken.outstandingCount)
				{
					int num;
					if (AccessToken.outstandingCount.TryGetValue(this.condition, out num))
					{
						num = (AccessToken.outstandingCount[this.condition] = num - 1);
						if (num == 0)
						{
							AccessToken.outstandingCount.Remove(this.condition);
						}
					}
				}
				if (returnToIssuingMap)
				{
					this.issuingMap.RevokeToken(this.condition, this.queue);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x00084864 File Offset: 0x00082A64
		public bool Validate(WaitCondition accessFor)
		{
			if (this.valid && this.condition.Equals(accessFor))
			{
				lock (AccessToken.validEpochs)
				{
					if (AccessToken.validEpochs.ContainsKey(this.condition))
					{
						this.valid = (this.epoch >= AccessToken.validEpochs[this.condition]);
						return this.valid;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x000848F4 File Offset: 0x00082AF4
		internal static void CleanupUnusedCondition(WaitCondition condition)
		{
			lock (AccessToken.outstandingCount)
			{
				if (AccessToken.outstandingCount.ContainsKey(condition))
				{
					AccessToken.ReclaimAll(condition);
					return;
				}
			}
			lock (AccessToken.validEpochs)
			{
				AccessToken.validEpochs.Remove(condition);
			}
		}

		// Token: 0x04001230 RID: 4656
		private static Dictionary<WaitCondition, int> validEpochs = new Dictionary<WaitCondition, int>();

		// Token: 0x04001231 RID: 4657
		private static Dictionary<WaitCondition, int> outstandingCount = new Dictionary<WaitCondition, int>();

		// Token: 0x04001232 RID: 4658
		private WaitCondition condition;

		// Token: 0x04001233 RID: 4659
		private NextHopSolutionKey queue;

		// Token: 0x04001234 RID: 4660
		private WaitConditionManager issuingMap;

		// Token: 0x04001235 RID: 4661
		private int epoch;

		// Token: 0x04001236 RID: 4662
		private bool valid;
	}
}
