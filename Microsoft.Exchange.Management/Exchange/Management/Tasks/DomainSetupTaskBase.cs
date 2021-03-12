using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002BD RID: 701
	public abstract class DomainSetupTaskBase : SetupTaskBase
	{
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x00068CE8 File Offset: 0x00066EE8
		// (set) Token: 0x060018A5 RID: 6309 RVA: 0x00068CFF File Offset: 0x00066EFF
		[Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
		public string Domain
		{
			get
			{
				return (string)base.Fields["Domain"];
			}
			set
			{
				base.Fields["Domain"] = value;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x00068D12 File Offset: 0x00066F12
		// (set) Token: 0x060018A7 RID: 6311 RVA: 0x00068D33 File Offset: 0x00066F33
		[Parameter(Mandatory = false)]
		public bool AllDomains
		{
			get
			{
				return (bool)(base.Fields["AllDomains"] ?? false);
			}
			set
			{
				base.Fields["AllDomains"] = value;
			}
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00068D4C File Offset: 0x00066F4C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			List<ADDomain> list = new List<ADDomain>();
			if (this.AllDomains && base.Fields.IsModified("Domain"))
			{
				base.WriteError(new CannotSpecifyBothAllDomainsAndDomainException(), ErrorCategory.InvalidArgument, null);
			}
			this.unreachableDomains = new List<DomainNotReachableException>();
			ADForest localForest = ADForest.GetLocalForest();
			if (this.AllDomains)
			{
				ADCrossRef[] domainPartitions = localForest.GetDomainPartitions();
				if (domainPartitions == null || domainPartitions.Length == 0)
				{
					base.WriteError(new DomainsNotFoundException(), ErrorCategory.InvalidData, null);
				}
				foreach (ADCrossRef adcrossRef in domainPartitions)
				{
					Exception ex = null;
					try
					{
						if (this.IsDomainSetupNeeded(adcrossRef.NCName))
						{
							this.domainConfigurationSession.DomainController = null;
							ADDomain addomain = this.domainConfigurationSession.Read<ADDomain>(adcrossRef.NCName);
							base.LogReadObject(addomain);
							list.Add(addomain);
						}
					}
					catch (ADExternalException ex2)
					{
						ex = ex2;
					}
					catch (ADTransientException ex3)
					{
						ex = ex3;
					}
					if (ex != null)
					{
						this.unreachableDomains.Add(new DomainNotReachableException(adcrossRef.DnsRoot[0], this.TaskName, ex));
					}
				}
				this.domainConfigurationSession.DomainController = null;
			}
			else if (base.Fields.IsModified("Domain"))
			{
				string text = (string)base.Fields["Domain"];
				if (string.IsNullOrEmpty(text))
				{
					base.WriteError(new DomainNotFoundException("<null>"), ErrorCategory.InvalidArgument, null);
				}
				ADCrossRef adcrossRef2 = localForest.FindDomainPartitionByFqdn(text);
				if (adcrossRef2 == null)
				{
					base.WriteError(new DomainNotFoundException(text), ErrorCategory.InvalidArgument, null);
				}
				if (this.IsDomainSetupNeeded(adcrossRef2.NCName))
				{
					ADDomain addomain2 = localForest.FindDomainByFqdn(text);
					addomain2 = this.domainConfigurationSession.Read<ADDomain>(addomain2.Id);
					base.LogReadObject(addomain2);
					list.Add(addomain2);
				}
			}
			else
			{
				ADCrossRef localDomainPartition = localForest.GetLocalDomainPartition();
				if (localDomainPartition == null)
				{
					base.WriteError(new LocalDomainNotFoundException(), ErrorCategory.InvalidData, null);
				}
				if (this.IsDomainSetupNeeded(localDomainPartition.NCName))
				{
					ADDomain addomain3 = localForest.FindLocalDomain();
					addomain3 = this.domainConfigurationSession.Read<ADDomain>(addomain3.Id);
					base.LogReadObject(addomain3);
					list.Add(addomain3);
				}
			}
			this.domains = list.ToArray();
			TaskLogger.LogExit();
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00068FA8 File Offset: 0x000671A8
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (this.UnreachableDomainErrorIsFatal())
			{
				using (List<DomainNotReachableException>.Enumerator enumerator = this.unreachableDomains.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Exception exception = enumerator.Current;
						this.WriteError(exception, ErrorCategory.ObjectNotFound, null, false);
					}
					return;
				}
			}
			foreach (DomainNotReachableException ex in this.unreachableDomains)
			{
				this.WriteWarning(Strings.LegacyPermissionsDomainNotReachableWarning(ex.Dom));
			}
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0006905C File Offset: 0x0006725C
		protected virtual bool IsDomainSetupNeeded(ADObjectId domainId)
		{
			return true;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0006905F File Offset: 0x0006725F
		protected virtual bool UnreachableDomainErrorIsFatal()
		{
			return true;
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060018AC RID: 6316
		protected abstract string TaskName { get; }

		// Token: 0x04000ABC RID: 2748
		private const string paramDomain = "Domain";

		// Token: 0x04000ABD RID: 2749
		private const string paramAllDomains = "AllDomains";

		// Token: 0x04000ABE RID: 2750
		protected ADDomain[] domains;

		// Token: 0x04000ABF RID: 2751
		private List<DomainNotReachableException> unreachableDomains;
	}
}
