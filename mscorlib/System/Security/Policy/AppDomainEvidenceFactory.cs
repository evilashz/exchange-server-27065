using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Security.Policy
{
	// Token: 0x02000313 RID: 787
	internal sealed class AppDomainEvidenceFactory : IRuntimeEvidenceFactory
	{
		// Token: 0x06002846 RID: 10310 RVA: 0x000944B0 File Offset: 0x000926B0
		internal AppDomainEvidenceFactory(AppDomain target)
		{
			this.m_targetDomain = target;
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x000944BF File Offset: 0x000926BF
		public IEvidenceFactory Target
		{
			get
			{
				return this.m_targetDomain;
			}
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000944C7 File Offset: 0x000926C7
		public IEnumerable<EvidenceBase> GetFactorySuppliedEvidence()
		{
			return new EvidenceBase[0];
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000944D0 File Offset: 0x000926D0
		[SecuritySafeCritical]
		public EvidenceBase GenerateEvidence(Type evidenceType)
		{
			if (!this.m_targetDomain.IsDefaultAppDomain())
			{
				AppDomain defaultDomain = AppDomain.GetDefaultDomain();
				return defaultDomain.GetHostEvidence(evidenceType);
			}
			if (this.m_entryPointEvidence == null)
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				RuntimeAssembly runtimeAssembly = entryAssembly as RuntimeAssembly;
				if (runtimeAssembly != null)
				{
					this.m_entryPointEvidence = runtimeAssembly.EvidenceNoDemand.Clone();
				}
				else if (entryAssembly != null)
				{
					this.m_entryPointEvidence = entryAssembly.Evidence;
				}
			}
			if (this.m_entryPointEvidence != null)
			{
				return this.m_entryPointEvidence.GetHostEvidence(evidenceType);
			}
			return null;
		}

		// Token: 0x0400104C RID: 4172
		private AppDomain m_targetDomain;

		// Token: 0x0400104D RID: 4173
		private Evidence m_entryPointEvidence;
	}
}
