using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BD5 RID: 3029
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class RuleId
	{
		// Token: 0x06006B9D RID: 27549 RVA: 0x001CCA58 File Offset: 0x001CAC58
		internal RuleId()
		{
		}

		// Token: 0x06006B9E RID: 27550 RVA: 0x001CCA67 File Offset: 0x001CAC67
		internal RuleId(int ruleIndex, long storeRuleId)
		{
			if (ruleIndex < 0)
			{
				ruleIndex = 0;
			}
			this.ruleIndex = ruleIndex;
			this.storeRuleId = storeRuleId;
		}

		// Token: 0x06006B9F RID: 27551 RVA: 0x001CCA8C File Offset: 0x001CAC8C
		public static RuleId Deserialize(byte[] byteArrayId, int ruleIndex)
		{
			if (byteArrayId == null)
			{
				throw new ArgumentNullException("byteArrayId");
			}
			RuleId ruleId = new RuleId();
			ruleId.Parse(byteArrayId, ruleIndex);
			return ruleId;
		}

		// Token: 0x06006BA0 RID: 27552 RVA: 0x001CCAB8 File Offset: 0x001CACB8
		public static RuleId Deserialize(string base64Id, int ruleIndex)
		{
			if (base64Id == null)
			{
				throw new ArgumentNullException(base64Id);
			}
			byte[] byteArrayId = StoreId.Base64ToByteArray(base64Id);
			return RuleId.Deserialize(byteArrayId, ruleIndex);
		}

		// Token: 0x06006BA1 RID: 27553 RVA: 0x001CCADD File Offset: 0x001CACDD
		public byte[] ToByteArray()
		{
			return BitConverter.GetBytes(this.storeRuleId);
		}

		// Token: 0x06006BA2 RID: 27554 RVA: 0x001CCAEA File Offset: 0x001CACEA
		public string ToBase64String()
		{
			return Convert.ToBase64String(this.ToByteArray());
		}

		// Token: 0x06006BA3 RID: 27555 RVA: 0x001CCAF8 File Offset: 0x001CACF8
		public override bool Equals(object id)
		{
			RuleId ruleId = id as RuleId;
			return ruleId != null && this.storeRuleId == ruleId.storeRuleId;
		}

		// Token: 0x06006BA4 RID: 27556 RVA: 0x001CCB1F File Offset: 0x001CAD1F
		public override int GetHashCode()
		{
			if (this.hashCode == -1 && this.storeRuleId != 0L)
			{
				this.hashCode = this.storeRuleId.GetHashCode();
			}
			return this.hashCode;
		}

		// Token: 0x06006BA5 RID: 27557 RVA: 0x001CCB4B File Offset: 0x001CAD4B
		public override string ToString()
		{
			return this.ToBase64String();
		}

		// Token: 0x17001D38 RID: 7480
		// (get) Token: 0x06006BA6 RID: 27558 RVA: 0x001CCB53 File Offset: 0x001CAD53
		public long StoreRuleId
		{
			get
			{
				return this.storeRuleId;
			}
		}

		// Token: 0x17001D39 RID: 7481
		// (get) Token: 0x06006BA7 RID: 27559 RVA: 0x001CCB5B File Offset: 0x001CAD5B
		internal int RuleIndex
		{
			get
			{
				return this.ruleIndex;
			}
		}

		// Token: 0x06006BA8 RID: 27560 RVA: 0x001CCB64 File Offset: 0x001CAD64
		internal void Parse(byte[] idBytes, int ruleIndex)
		{
			if (0 > ruleIndex)
			{
				ExTraceGlobals.StorageTracer.TraceError<int>((long)this.GetHashCode(), "RuleId::Parse. The rule index {0} is negative, hence invalid.", ruleIndex);
				throw new InvalidDataException(ServerStrings.ExRuleIdInvalid);
			}
			if (8 != idBytes.Length)
			{
				ExTraceGlobals.StorageTracer.TraceError<int, int>((long)this.GetHashCode(), "RuleId::Parse. The byte array length {0} does not match the expected length of {1}, hence it is invalid.", idBytes.Length, 8);
				throw new InvalidDataException(ServerStrings.ExRuleIdInvalid);
			}
			this.storeRuleId = BitConverter.ToInt64(idBytes, 0);
			this.ruleIndex = ruleIndex;
		}

		// Token: 0x04003D9B RID: 15771
		private int hashCode = -1;

		// Token: 0x04003D9C RID: 15772
		private long storeRuleId;

		// Token: 0x04003D9D RID: 15773
		private int ruleIndex;
	}
}
