using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000324 RID: 804
	[Serializable]
	internal sealed class LegacyEvidenceList : EvidenceBase, IEnumerable<EvidenceBase>, IEnumerable, ILegacyEvidenceAdapter
	{
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002924 RID: 10532 RVA: 0x000980CB File Offset: 0x000962CB
		public object EvidenceObject
		{
			get
			{
				if (this.m_legacyEvidenceList.Count <= 0)
				{
					return null;
				}
				return this.m_legacyEvidenceList[0];
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x000980EC File Offset: 0x000962EC
		public Type EvidenceType
		{
			get
			{
				ILegacyEvidenceAdapter legacyEvidenceAdapter = this.m_legacyEvidenceList[0] as ILegacyEvidenceAdapter;
				if (legacyEvidenceAdapter != null)
				{
					return legacyEvidenceAdapter.EvidenceType;
				}
				return this.m_legacyEvidenceList[0].GetType();
			}
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x00098126 File Offset: 0x00096326
		public void Add(EvidenceBase evidence)
		{
			this.m_legacyEvidenceList.Add(evidence);
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x00098134 File Offset: 0x00096334
		public IEnumerator<EvidenceBase> GetEnumerator()
		{
			return this.m_legacyEvidenceList.GetEnumerator();
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x00098146 File Offset: 0x00096346
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_legacyEvidenceList.GetEnumerator();
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x00098158 File Offset: 0x00096358
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override EvidenceBase Clone()
		{
			return base.Clone();
		}

		// Token: 0x04001082 RID: 4226
		private List<EvidenceBase> m_legacyEvidenceList = new List<EvidenceBase>();
	}
}
