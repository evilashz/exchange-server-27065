using System;

namespace System.Security.Policy
{
	// Token: 0x02000325 RID: 805
	[Serializable]
	internal sealed class EvidenceTypeDescriptor
	{
		// Token: 0x0600292B RID: 10539 RVA: 0x00098173 File Offset: 0x00096373
		public EvidenceTypeDescriptor()
		{
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x0009817C File Offset: 0x0009637C
		private EvidenceTypeDescriptor(EvidenceTypeDescriptor descriptor)
		{
			this.m_hostCanGenerate = descriptor.m_hostCanGenerate;
			if (descriptor.m_assemblyEvidence != null)
			{
				this.m_assemblyEvidence = descriptor.m_assemblyEvidence.Clone();
			}
			if (descriptor.m_hostEvidence != null)
			{
				this.m_hostEvidence = descriptor.m_hostEvidence.Clone();
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x000981CD File Offset: 0x000963CD
		// (set) Token: 0x0600292E RID: 10542 RVA: 0x000981D5 File Offset: 0x000963D5
		public EvidenceBase AssemblyEvidence
		{
			get
			{
				return this.m_assemblyEvidence;
			}
			set
			{
				this.m_assemblyEvidence = value;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600292F RID: 10543 RVA: 0x000981DE File Offset: 0x000963DE
		// (set) Token: 0x06002930 RID: 10544 RVA: 0x000981E6 File Offset: 0x000963E6
		public bool Generated
		{
			get
			{
				return this.m_generated;
			}
			set
			{
				this.m_generated = value;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x000981EF File Offset: 0x000963EF
		// (set) Token: 0x06002932 RID: 10546 RVA: 0x000981F7 File Offset: 0x000963F7
		public bool HostCanGenerate
		{
			get
			{
				return this.m_hostCanGenerate;
			}
			set
			{
				this.m_hostCanGenerate = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x00098200 File Offset: 0x00096400
		// (set) Token: 0x06002934 RID: 10548 RVA: 0x00098208 File Offset: 0x00096408
		public EvidenceBase HostEvidence
		{
			get
			{
				return this.m_hostEvidence;
			}
			set
			{
				this.m_hostEvidence = value;
			}
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x00098211 File Offset: 0x00096411
		public EvidenceTypeDescriptor Clone()
		{
			return new EvidenceTypeDescriptor(this);
		}

		// Token: 0x04001083 RID: 4227
		[NonSerialized]
		private bool m_hostCanGenerate;

		// Token: 0x04001084 RID: 4228
		[NonSerialized]
		private bool m_generated;

		// Token: 0x04001085 RID: 4229
		private EvidenceBase m_hostEvidence;

		// Token: 0x04001086 RID: 4230
		private EvidenceBase m_assemblyEvidence;
	}
}
