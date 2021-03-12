using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A32 RID: 2610
	internal class ValidationRuleDefinition
	{
		// Token: 0x060077F6 RID: 30710 RVA: 0x0018B55C File Offset: 0x0018975C
		public ValidationRuleDefinition(string name, string feature, ValidationRuleSkus applicableSku, List<RoleEntry> applicableRoleEntries, List<Capability> restrictedCapabilities, List<Capability> overridingAllowCapabilities, ValidationErrorStringProvider errorStringProvider)
		{
			if (applicableRoleEntries == null)
			{
				throw new ArgumentNullException("applicableRoleEntries");
			}
			if (restrictedCapabilities == null)
			{
				throw new ArgumentNullException("restrictedCapabilities");
			}
			if (overridingAllowCapabilities == null)
			{
				throw new ArgumentNullException("overridingAllowCapabilities");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (string.IsNullOrEmpty(feature))
			{
				throw new ArgumentNullException("feature");
			}
			if (errorStringProvider == null)
			{
				throw new ArgumentNullException("errorStringProvider");
			}
			this.Name = name;
			this.Feature = feature;
			this.ApplicableSku = applicableSku;
			this.ApplicableRoleEntries = applicableRoleEntries;
			this.ErrorString = errorStringProvider;
			this.ApplicableRoleEntries.Sort(RoleEntryComparer.Instance);
			restrictedCapabilities.Sort();
			this.RestrictedCapabilities = new ReadOnlyCollection<Capability>(restrictedCapabilities);
			overridingAllowCapabilities.Sort();
			this.OverridingAllowCapabilities = new ReadOnlyCollection<Capability>(overridingAllowCapabilities);
		}

		// Token: 0x060077F7 RID: 30711 RVA: 0x0018B62E File Offset: 0x0018982E
		public ValidationRuleDefinition(string name, string feature, ValidationRuleSkus applicableSku, List<RoleEntry> applicableRoleEntries, List<Capability> restrictedCapabilities, List<Capability> overridingAllowCapabilities, ValidationErrorStringProvider errorStringProvider, List<ValidationRuleExpression> expressions) : this(name, feature, applicableSku, applicableRoleEntries, restrictedCapabilities, overridingAllowCapabilities, errorStringProvider)
		{
			if (expressions == null)
			{
				throw new ArgumentNullException("expressions");
			}
			this.Expressions = expressions;
		}

		// Token: 0x17002AD0 RID: 10960
		// (get) Token: 0x060077F8 RID: 30712 RVA: 0x0018B658 File Offset: 0x00189858
		// (set) Token: 0x060077F9 RID: 30713 RVA: 0x0018B660 File Offset: 0x00189860
		public string Name { get; private set; }

		// Token: 0x17002AD1 RID: 10961
		// (get) Token: 0x060077FA RID: 30714 RVA: 0x0018B669 File Offset: 0x00189869
		// (set) Token: 0x060077FB RID: 30715 RVA: 0x0018B671 File Offset: 0x00189871
		public string Feature { get; private set; }

		// Token: 0x17002AD2 RID: 10962
		// (get) Token: 0x060077FC RID: 30716 RVA: 0x0018B67A File Offset: 0x0018987A
		// (set) Token: 0x060077FD RID: 30717 RVA: 0x0018B682 File Offset: 0x00189882
		public List<ValidationRuleExpression> Expressions { get; private set; }

		// Token: 0x17002AD3 RID: 10963
		// (get) Token: 0x060077FE RID: 30718 RVA: 0x0018B68B File Offset: 0x0018988B
		// (set) Token: 0x060077FF RID: 30719 RVA: 0x0018B693 File Offset: 0x00189893
		public List<RoleEntry> ApplicableRoleEntries { get; protected set; }

		// Token: 0x17002AD4 RID: 10964
		// (get) Token: 0x06007800 RID: 30720 RVA: 0x0018B69C File Offset: 0x0018989C
		// (set) Token: 0x06007801 RID: 30721 RVA: 0x0018B6A4 File Offset: 0x001898A4
		public ReadOnlyCollection<Capability> RestrictedCapabilities { get; private set; }

		// Token: 0x17002AD5 RID: 10965
		// (get) Token: 0x06007802 RID: 30722 RVA: 0x0018B6AD File Offset: 0x001898AD
		// (set) Token: 0x06007803 RID: 30723 RVA: 0x0018B6B5 File Offset: 0x001898B5
		public ReadOnlyCollection<Capability> OverridingAllowCapabilities { get; private set; }

		// Token: 0x17002AD6 RID: 10966
		// (get) Token: 0x06007804 RID: 30724 RVA: 0x0018B6BE File Offset: 0x001898BE
		// (set) Token: 0x06007805 RID: 30725 RVA: 0x0018B6C6 File Offset: 0x001898C6
		public ValidationErrorStringProvider ErrorString { get; private set; }

		// Token: 0x17002AD7 RID: 10967
		// (get) Token: 0x06007806 RID: 30726 RVA: 0x0018B6CF File Offset: 0x001898CF
		// (set) Token: 0x06007807 RID: 30727 RVA: 0x0018B6D7 File Offset: 0x001898D7
		public ValidationRuleSkus ApplicableSku { get; private set; }

		// Token: 0x06007808 RID: 30728 RVA: 0x0018B6E0 File Offset: 0x001898E0
		public bool IsRuleApplicable(RoleEntry targetRoleEntry, out RoleEntry matchingRoleEntry)
		{
			if (null == targetRoleEntry)
			{
				throw new ArgumentNullException("targetRoleEntry");
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<RoleEntry, string>((long)this.GetHashCode(), "Entering ValidationRuleDefinition.IsRuleApplicable({0}). Rule name: '{1}'", targetRoleEntry, this.Name);
			matchingRoleEntry = null;
			int num = this.ApplicableRoleEntries.BinarySearch(targetRoleEntry, RoleEntry.NameComparer);
			if (num < 0)
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<RoleEntry, string>((long)this.GetHashCode(), "ValidationRuleDefinition.IsRuleApplicable({0}) returns false. Rule name: '{1}'. Cmdlet not applicable.", targetRoleEntry, this.Name);
				return false;
			}
			RoleEntry roleEntry = this.ApplicableRoleEntries[num];
			matchingRoleEntry = roleEntry.IntersectParameters(targetRoleEntry);
			if (matchingRoleEntry.Parameters.Count > 0)
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<RoleEntry, string>((long)this.GetHashCode(), "ValidationRuleDefinition.IsRuleApplicable({0}) returns true. Rule name: '{1}'. Parameters match.", targetRoleEntry, this.Name);
				return true;
			}
			if (targetRoleEntry.Parameters.Count == 0 && roleEntry.Parameters.Contains("_RestrictionDefinedForAllParameters"))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<RoleEntry, string>((long)this.GetHashCode(), "ValidationRuleDefinition.IsRuleApplicable({0}) returns true. Rule name: '{1}'. No parameters specified but restriction defined for all parameters.", targetRoleEntry, this.Name);
				return true;
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<RoleEntry, string>((long)this.GetHashCode(), "ValidationRuleDefinition.IsRuleApplicable({0}) returns false. Rule name: '{1}'. Parameters doesn't match. ", targetRoleEntry, this.Name);
			return false;
		}

		// Token: 0x06007809 RID: 30729 RVA: 0x0018B7F8 File Offset: 0x001899F8
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				throw new NullReferenceException("obj");
			}
			ValidationRuleDefinition validationRuleDefinition = obj as ValidationRuleDefinition;
			if (validationRuleDefinition == null)
			{
				return false;
			}
			if (!this.Name.Equals(validationRuleDefinition.Name, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (validationRuleDefinition.RestrictedCapabilities.Count != this.RestrictedCapabilities.Count)
			{
				return false;
			}
			for (int i = 0; i < this.RestrictedCapabilities.Count<Capability>(); i++)
			{
				if (!validationRuleDefinition.RestrictedCapabilities[i].Equals(this.RestrictedCapabilities[i]))
				{
					return false;
				}
			}
			if (validationRuleDefinition.ApplicableRoleEntries.Count != this.ApplicableRoleEntries.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ApplicableRoleEntries.Count; j++)
			{
				if (!this.ApplicableRoleEntries[j].Equals(validationRuleDefinition.ApplicableRoleEntries[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600780A RID: 30730 RVA: 0x0018B8E2 File Offset: 0x00189AE2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04004CCB RID: 19659
		private const string SpecialParameterForRestrictionRequiredForAllParameters = "_RestrictionDefinedForAllParameters";
	}
}
