using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000830 RID: 2096
	[Serializable]
	public sealed class DataClassificationIdParameter : ClassificationRuleCollectionIdParameter
	{
		// Token: 0x060048A1 RID: 18593 RVA: 0x0012A2D0 File Offset: 0x001284D0
		private DataClassificationIdParameter(string rawIdentity) : base(rawIdentity)
		{
		}

		// Token: 0x060048A2 RID: 18594 RVA: 0x0012A2D9 File Offset: 0x001284D9
		private DataClassificationIdParameter(string ruleCollectionObjectIdentity, string dataClassificationId) : base(ruleCollectionObjectIdentity)
		{
			ExAssert.RetailAssert(!string.IsNullOrEmpty(dataClassificationId), "The data classification ID passed to DataClassificationIdParameter must be validated in Parse()");
			this.dataClassificationIdentityFilter = dataClassificationId;
			this.InitializeDataClassificationMatchFilter();
		}

		// Token: 0x060048A3 RID: 18595 RVA: 0x0012A302 File Offset: 0x00128502
		public DataClassificationIdParameter()
		{
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x0012A30C File Offset: 0x0012850C
		public new static DataClassificationIdParameter Parse(string identity)
		{
			DataClassificationIdParameter dataClassificationIdParameter = new DataClassificationIdParameter(identity);
			return new DataClassificationIdParameter(dataClassificationIdParameter.IsHierarchical ? ClassificationDefinitionUtils.CreateHierarchicalIdentityString(dataClassificationIdParameter.OrganizationName, "*") : "*", dataClassificationIdParameter.FriendlyName);
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x0012A34C File Offset: 0x0012854C
		internal override void Initialize(ObjectId objectId)
		{
			ADObjectId adobjectId = objectId as ADObjectId;
			if (adobjectId == null || base.InternalADObjectId != null)
			{
				base.Initialize(objectId);
				return;
			}
			if (!string.IsNullOrEmpty(adobjectId.DistinguishedName))
			{
				throw new ArgumentException(Strings.DataClassificationDnIdentityNotSupported, "objectId");
			}
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x0012A398 File Offset: 0x00128598
		public override string ToString()
		{
			string result;
			if (this.dataClassificationIdentityFilter != null)
			{
				result = (base.IsHierarchical ? ClassificationDefinitionUtils.CreateHierarchicalIdentityString(base.OrganizationName, this.dataClassificationIdentityFilter) : this.dataClassificationIdentityFilter);
			}
			else
			{
				result = base.ToString();
			}
			return result;
		}

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x060048A7 RID: 18599 RVA: 0x0012A3D9 File Offset: 0x001285D9
		public string DataClassificationIdentity
		{
			get
			{
				return this.dataClassificationIdentityFilter;
			}
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x0012A3E4 File Offset: 0x001285E4
		private void InitializeDataClassificationMatchFilter()
		{
			this.resultsFilter = null;
			TextFilter nameQueryFilter = this.IsWildcardDefined(this.dataClassificationIdentityFilter) ? ((TextFilter)base.CreateWildcardFilter(ADObjectSchema.Name, this.dataClassificationIdentityFilter)) : new TextFilter(ADObjectSchema.Name, this.dataClassificationIdentityFilter, MatchOptions.FullString, MatchFlags.Default);
			DataClassificationIdentityMatcher dataClassificationIdentityMatcher = DataClassificationIdentityMatcher.CreateFrom(nameQueryFilter, this.dataClassificationIdentityFilter);
			if (dataClassificationIdentityMatcher != null)
			{
				this.resultsFilter = dataClassificationIdentityMatcher;
			}
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x0012A448 File Offset: 0x00128648
		internal IEnumerable<DataClassificationPresentationObject> FilterResults(IEnumerable<DataClassificationPresentationObject> unfilteredResults)
		{
			if (this.resultsFilter != null)
			{
				return unfilteredResults.Where(new Func<DataClassificationPresentationObject, bool>(this.resultsFilter.Match));
			}
			return unfilteredResults;
		}

		// Token: 0x04002C0A RID: 11274
		private const string AllRuleCollectionObjectIdentityQuery = "*";

		// Token: 0x04002C0B RID: 11275
		private readonly string dataClassificationIdentityFilter;

		// Token: 0x04002C0C RID: 11276
		private ClassificationIdentityMatcher<DataClassificationPresentationObject> resultsFilter;
	}
}
