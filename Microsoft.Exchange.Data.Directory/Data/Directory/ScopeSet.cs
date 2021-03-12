using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ValidationRules;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000189 RID: 393
	[Serializable]
	internal class ScopeSet
	{
		// Token: 0x06001086 RID: 4230 RVA: 0x0004FA99 File Offset: 0x0004DC99
		public ScopeSet(ADScope recipientReadScope, IList<ADScopeCollection> recipientWriteScopes, ADScope configReadScope, ADScope configWriteScope) : this(recipientReadScope, recipientWriteScopes, null, configReadScope, configWriteScope, null, null)
		{
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0004FAAC File Offset: 0x0004DCAC
		public ScopeSet(ADScope recipientReadScope, IList<ADScopeCollection> recipientWriteScopes, IList<ADScope> exclusiveRecipientScopes, ADScope configReadScope, ADScope configWriteScope, Dictionary<string, IList<ADScopeCollection>> objectSpecificConfigWriteScopes, Dictionary<string, ADScopeCollection> objectSpecificExclusiveConfigWriteScopes) : this(recipientReadScope, recipientWriteScopes, exclusiveRecipientScopes, configReadScope, configWriteScope, objectSpecificConfigWriteScopes, objectSpecificExclusiveConfigWriteScopes, null)
		{
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0004FACC File Offset: 0x0004DCCC
		public ScopeSet(ADScope recipientReadScope, IList<ADScopeCollection> recipientWriteScopes, IList<ADScope> exclusiveRecipientScopes, ADScope configReadScope, ADScope configWriteScope, Dictionary<string, IList<ADScopeCollection>> objectSpecificConfigWriteScopes, Dictionary<string, ADScopeCollection> objectSpecificExclusiveConfigWriteScopes, IList<ValidationRule> applicableValidationRules)
		{
			this.recipientReadScope = (recipientReadScope ?? ADScope.Empty);
			this.recipientWriteScopes = new ReadOnlyCollection<ADScopeCollection>(recipientWriteScopes ?? ((IList<ADScopeCollection>)ADScopeCollection.EmptyArray));
			this.exclusiveRecipientScopes = ((exclusiveRecipientScopes == null || exclusiveRecipientScopes.Count == 0) ? ADScopeCollection.Empty : new ADScopeCollection(exclusiveRecipientScopes));
			this.configReadScope = (configReadScope ?? ADScope.Empty);
			this.configWriteScope = (configWriteScope ?? ADScope.Empty);
			if (this.configWriteScope != ADScope.NoAccess && this.configReadScope == ADScope.NoAccess)
			{
				throw new ArgumentException("configReadScope must be granted when configWriteScope is allowed");
			}
			this.objectSpecificConfigWriteScopes = objectSpecificConfigWriteScopes;
			if (this.objectSpecificConfigWriteScopes != null && this.configWriteScope == ADScope.NoAccess)
			{
				throw new ArgumentException("configWriteScope must be granted when objectSpecificConfigWriteScopes is defined");
			}
			this.objectSpecificExclusiveConfigWriteScopes = objectSpecificExclusiveConfigWriteScopes;
			this.validationRules = ((applicableValidationRules == null) ? new List<ValidationRule>(0) : applicableValidationRules);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0004FBB2 File Offset: 0x0004DDB2
		internal static ScopeSet GetOrgWideDefaultScopeSet(OrganizationId organizationId)
		{
			return ScopeSet.GetOrgWideDefaultScopeSet(organizationId, null);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0004FBBC File Offset: 0x0004DDBC
		internal static ScopeSet GetOrgWideDefaultScopeSet(OrganizationId organizationId, QueryFilter recipientReadFilter)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			ADScopeCollection item = new ADScopeCollection(new List<ADScope>
			{
				new ADScope(organizationId.OrganizationalUnit, null)
			});
			IList<ADScopeCollection> list = new List<ADScopeCollection>();
			list.Add(item);
			return new ScopeSet(new ADScope(organizationId.OrganizationalUnit, recipientReadFilter), list, new ADScope(organizationId.ConfigurationUnit, null), null);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0004FC28 File Offset: 0x0004DE28
		internal static ScopeSet GetAllTenantsDefaultScopeSet(string partitionFqdn)
		{
			ADScope item = new ADScope(ADSession.GetHostedOrganizationsRoot(partitionFqdn), null);
			ADScopeCollection item2 = new ADScopeCollection(new List<ADScope>(1)
			{
				item
			});
			return new ScopeSet(item, new List<ADScopeCollection>(1)
			{
				item2
			}, new ADScope(ADSession.GetHostedOrganizationsRoot(partitionFqdn), null), null);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0004FC79 File Offset: 0x0004DE79
		public static ScopeSet ResolveUnderScope(OrganizationId organizationId, ScopeSet scopeSet)
		{
			return ScopeSet.ResolveUnderScope(organizationId, scopeSet, true);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0004FC84 File Offset: 0x0004DE84
		internal static ScopeSet ResolveUnderScope(OrganizationId organizationId, ScopeSet scopeSet, bool checkOrgScope)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			if (organizationId.OrganizationalUnit == null || organizationId.ConfigurationUnit == null)
			{
				throw new ArgumentException("Invalid under scope organization provided");
			}
			if (scopeSet == null)
			{
				return ScopeSet.GetOrgWideDefaultScopeSet(organizationId);
			}
			if (checkOrgScope)
			{
				if (scopeSet.RecipientReadScope.Root != null && !organizationId.OrganizationalUnit.IsDescendantOf(scopeSet.RecipientReadScope.Root))
				{
					throw new ADScopeException(DirectoryStrings.ExceptionOrgScopeNotInUserScope(organizationId.OrganizationalUnit.ToString(), scopeSet.RecipientReadScope.Root.ToString()), null);
				}
				if (scopeSet.ConfigReadScope.Root != null && !organizationId.ConfigurationUnit.Parent.IsDescendantOf(scopeSet.ConfigReadScope.Root.Parent))
				{
					throw new ADScopeException(DirectoryStrings.ExceptionOrgScopeNotInUserScope(organizationId.ConfigurationUnit.Parent.ToString(), scopeSet.ConfigReadScope.Root.Parent.ToString()), null);
				}
			}
			return new ScopeSet(new ADScope(organizationId.OrganizationalUnit, (scopeSet.RecipientReadScope != null) ? scopeSet.RecipientReadScope.Filter : null), scopeSet.RecipientWriteScopes, scopeSet.exclusiveRecipientScopes, new ADScope(organizationId.ConfigurationUnit, (scopeSet.ConfigReadScope != null) ? scopeSet.ConfigReadScope.Filter : null), new ADScope(organizationId.ConfigurationUnit, (scopeSet.configWriteScope != null) ? scopeSet.configWriteScope.Filter : null), scopeSet.objectSpecificConfigWriteScopes, scopeSet.objectSpecificExclusiveConfigWriteScopes, scopeSet.validationRules);
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x0004FE02 File Offset: 0x0004E002
		public ADScope RecipientReadScope
		{
			get
			{
				return this.recipientReadScope;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x0004FE0A File Offset: 0x0004E00A
		public IList<ADScopeCollection> RecipientWriteScopes
		{
			get
			{
				return this.recipientWriteScopes;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0004FE12 File Offset: 0x0004E012
		public ADScopeCollection ExclusiveRecipientScopes
		{
			get
			{
				return this.exclusiveRecipientScopes;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x0004FE1A File Offset: 0x0004E01A
		public ADScope ConfigReadScope
		{
			get
			{
				return this.configReadScope;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x0004FE22 File Offset: 0x0004E022
		public ADScope ConfigWriteScope
		{
			get
			{
				return this.configWriteScope;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x0004FE2A File Offset: 0x0004E02A
		public IList<ValidationRule> ValidationRules
		{
			get
			{
				return this.validationRules;
			}
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0004FE34 File Offset: 0x0004E034
		public IList<ADScopeCollection> GetConfigWriteScopes(string className)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			IList<ADScopeCollection> result;
			if (this.objectSpecificConfigWriteScopes != null && this.objectSpecificConfigWriteScopes.TryGetValue(className, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0004FE6C File Offset: 0x0004E06C
		public ADScopeCollection GetExclusiveConfigWriteScopes(string className)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			ADScopeCollection result;
			if (this.objectSpecificExclusiveConfigWriteScopes != null && this.objectSpecificExclusiveConfigWriteScopes.TryGetValue(className, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0004FEA4 File Offset: 0x0004E0A4
		internal LocalizedString ToLocalizedString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(DirectoryStrings.RecipientReadScope);
			stringBuilder.Append("{");
			if (this.RecipientReadScope != null)
			{
				stringBuilder.Append(this.RecipientReadScope.ToString());
			}
			stringBuilder.Append("}");
			stringBuilder.Append(", ");
			stringBuilder.Append(DirectoryStrings.RecipientWriteScopes);
			stringBuilder.Append("{");
			if (this.RecipientWriteScopes != null)
			{
				foreach (ADScopeCollection adscopeCollection in this.RecipientWriteScopes)
				{
					stringBuilder.Append(adscopeCollection.ToString());
				}
			}
			stringBuilder.Append("}");
			stringBuilder.Append(", ");
			stringBuilder.Append(DirectoryStrings.ConfigReadScope);
			stringBuilder.Append("{");
			if (this.ConfigReadScope != null)
			{
				stringBuilder.Append(this.ConfigReadScope.ToString());
			}
			stringBuilder.Append("}");
			stringBuilder.Append(", ");
			stringBuilder.Append(DirectoryStrings.ConfigWriteScopes);
			stringBuilder.Append("{");
			if (this.ConfigWriteScope != null)
			{
				stringBuilder.Append(this.ConfigWriteScope.ToString());
				stringBuilder.Append(", ");
			}
			if (this.objectSpecificConfigWriteScopes != null)
			{
				foreach (KeyValuePair<string, IList<ADScopeCollection>> keyValuePair in this.objectSpecificConfigWriteScopes)
				{
					foreach (ADScopeCollection adscopeCollection2 in keyValuePair.Value)
					{
						stringBuilder.Append(adscopeCollection2.ToString());
						stringBuilder.Append(", ");
					}
				}
			}
			stringBuilder.Append("}");
			stringBuilder.Append(", ");
			stringBuilder.Append(DirectoryStrings.ExclusiveRecipientScopes);
			stringBuilder.Append("{");
			if (this.ExclusiveRecipientScopes != null)
			{
				stringBuilder.Append(this.ExclusiveRecipientScopes.ToString());
			}
			stringBuilder.Append("}");
			stringBuilder.Append(", ");
			stringBuilder.Append(DirectoryStrings.ExclusiveConfigScopes);
			stringBuilder.Append("{");
			if (this.objectSpecificExclusiveConfigWriteScopes != null)
			{
				foreach (KeyValuePair<string, ADScopeCollection> keyValuePair2 in this.objectSpecificExclusiveConfigWriteScopes)
				{
					stringBuilder.Append(keyValuePair2.Value.ToString());
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.Append("}");
			return new LocalizedString(stringBuilder.ToString());
		}

		// Token: 0x0400096A RID: 2410
		private ADScope recipientReadScope;

		// Token: 0x0400096B RID: 2411
		private ReadOnlyCollection<ADScopeCollection> recipientWriteScopes;

		// Token: 0x0400096C RID: 2412
		private ADScopeCollection exclusiveRecipientScopes;

		// Token: 0x0400096D RID: 2413
		private ADScope configReadScope;

		// Token: 0x0400096E RID: 2414
		private ADScope configWriteScope;

		// Token: 0x0400096F RID: 2415
		private Dictionary<string, IList<ADScopeCollection>> objectSpecificConfigWriteScopes;

		// Token: 0x04000970 RID: 2416
		private Dictionary<string, ADScopeCollection> objectSpecificExclusiveConfigWriteScopes;

		// Token: 0x04000971 RID: 2417
		private IList<ValidationRule> validationRules;
	}
}
