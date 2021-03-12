using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay.Dumpster
{
	// Token: 0x0200017B RID: 379
	internal class SafetyNetRegKey
	{
		// Token: 0x06000F57 RID: 3927 RVA: 0x0004202A File Offset: 0x0004022A
		public SafetyNetRegKey(string dbGuidStr, string dbName)
		{
			this.m_dbGuidStr = dbGuidStr;
			this.m_dbName = dbName;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x000421F8 File Offset: 0x000403F8
		public IEnumerable<SafetyNetRequestKey> ReadRequestKeys()
		{
			string[] valueNames = null;
			using (SafetyNetRegKeyStore safetyNetRegKeyStore = new SafetyNetRegKeyStore(this.m_dbGuidStr, this.m_dbName))
			{
				valueNames = safetyNetRegKeyStore.ReadRequestKeyNames();
			}
			foreach (string valueName in valueNames)
			{
				yield return SafetyNetRequestKey.Parse(valueName);
			}
			yield break;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00042218 File Offset: 0x00040418
		public SafetyNetInfo ReadRequestInfo(SafetyNetRequestKey requestKey, SafetyNetInfo prevInfo)
		{
			SafetyNetInfo result;
			using (SafetyNetRegKeyStore safetyNetRegKeyStore = new SafetyNetRegKeyStore(this.m_dbGuidStr, this.m_dbName))
			{
				result = safetyNetRegKeyStore.ReadRequestInfo(requestKey, prevInfo);
			}
			return result;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00042260 File Offset: 0x00040460
		public void WriteRequestInfo(SafetyNetInfo info)
		{
			using (SafetyNetRegKeyStore safetyNetRegKeyStore = new SafetyNetRegKeyStore(this.m_dbGuidStr, this.m_dbName))
			{
				safetyNetRegKeyStore.WriteRequestInfo(info);
			}
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x000422A4 File Offset: 0x000404A4
		public void DeleteRequest(SafetyNetInfo info)
		{
			using (SafetyNetRegKeyStore safetyNetRegKeyStore = new SafetyNetRegKeyStore(this.m_dbGuidStr, this.m_dbName))
			{
				safetyNetRegKeyStore.DeleteRequest(info);
			}
		}

		// Token: 0x0400064E RID: 1614
		private readonly string m_dbGuidStr;

		// Token: 0x0400064F RID: 1615
		private readonly string m_dbName;
	}
}
