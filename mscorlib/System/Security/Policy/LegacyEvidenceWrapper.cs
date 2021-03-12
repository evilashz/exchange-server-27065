using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000323 RID: 803
	[Serializable]
	internal sealed class LegacyEvidenceWrapper : EvidenceBase, ILegacyEvidenceAdapter
	{
		// Token: 0x0600291E RID: 10526 RVA: 0x00098084 File Offset: 0x00096284
		internal LegacyEvidenceWrapper(object legacyEvidence)
		{
			this.m_legacyEvidence = legacyEvidence;
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x00098093 File Offset: 0x00096293
		public object EvidenceObject
		{
			get
			{
				return this.m_legacyEvidence;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x0009809B File Offset: 0x0009629B
		public Type EvidenceType
		{
			get
			{
				return this.m_legacyEvidence.GetType();
			}
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000980A8 File Offset: 0x000962A8
		public override bool Equals(object obj)
		{
			return this.m_legacyEvidence.Equals(obj);
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000980B6 File Offset: 0x000962B6
		public override int GetHashCode()
		{
			return this.m_legacyEvidence.GetHashCode();
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000980C3 File Offset: 0x000962C3
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override EvidenceBase Clone()
		{
			return base.Clone();
		}

		// Token: 0x04001081 RID: 4225
		private object m_legacyEvidence;
	}
}
