using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000661 RID: 1633
	internal sealed class ManagementReporting
	{
		// Token: 0x0600393D RID: 14653 RVA: 0x000F05C1 File Offset: 0x000EE7C1
		internal ManagementReporting(IConfigurationSession configurationSession, IRecipientSession recSession, OrganizationId organizationId, SharedConfiguration sharedConfiguration, Task.TaskWarningLoggingDelegate writeWarning)
		{
			this.WriteWarning = writeWarning;
			this.configSession = configurationSession;
			this.recipientSession = recSession;
			this.orgId = organizationId;
			this.sharedConfig = sharedConfiguration;
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000F05EE File Offset: 0x000EE7EE
		internal ManagementReporting(IConfigurationSession configurationSession, IRecipientSession recSession, OrganizationId organizationId, Task.TaskWarningLoggingDelegate writeWarning) : this(configurationSession, recSession, organizationId, null, writeWarning)
		{
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000F05FC File Offset: 0x000EE7FC
		internal List<ExchangeRoleAssignment> FindRoleAssignmentsWithWritableRecipient(ADRawEntry recepientObject, IEnumerable<ExchangeRoleAssignment> roleAssignments)
		{
			Dictionary<ADObjectId, ADScope> customConfigScopes;
			Dictionary<ADObjectId, ADScope> dictionary;
			this.RetrieveAllScopes(ScopeRestrictionType.RecipientScope, recepientObject, out customConfigScopes, out dictionary);
			if (dictionary.Count > 0 && ManagementReporting.VerifyIsWithinScopes(recepientObject, new List<ADScope>(dictionary.Values), new List<ADScope>(dictionary.Values), new RbacScope(ScopeType.Organization)))
			{
				return this.GetEffectiveRoleAssignmentsForRecipient(recepientObject, roleAssignments, customConfigScopes, dictionary, true);
			}
			return this.GetEffectiveRoleAssignmentsForRecipient(recepientObject, roleAssignments, customConfigScopes, dictionary, false);
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000F065C File Offset: 0x000EE85C
		private List<ExchangeRoleAssignment> GetExclusiveEffectiveRoleAssignmentsForRecipient(ADRawEntry recipientObject, IEnumerable<ExchangeRoleAssignment> roleAssignments, Dictionary<ADObjectId, ADScope> exclusiveConfigScopes)
		{
			List<ExchangeRoleAssignment> list = new List<ExchangeRoleAssignment>();
			foreach (ExchangeRoleAssignment exchangeRoleAssignment in roleAssignments)
			{
				if (this.IsValid(exchangeRoleAssignment) && exchangeRoleAssignment.RecipientWriteScope == RecipientWriteScopeType.ExclusiveRecipientScope && exclusiveConfigScopes.ContainsKey(exchangeRoleAssignment.CustomRecipientWriteScope) && ManagementReporting.VerifyIsWithinScopes(recipientObject, exclusiveConfigScopes[exchangeRoleAssignment.CustomRecipientWriteScope], exclusiveConfigScopes[exchangeRoleAssignment.CustomRecipientWriteScope], new RbacScope(exchangeRoleAssignment.ConfigReadScope)))
				{
					list.Add(exchangeRoleAssignment);
				}
			}
			return list;
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000F06F8 File Offset: 0x000EE8F8
		private List<ExchangeRoleAssignment> GetEffectiveRoleAssignmentsForRecipient(ADRawEntry recipientObject, IEnumerable<ExchangeRoleAssignment> roleAssignments, Dictionary<ADObjectId, ADScope> customConfigScopes, Dictionary<ADObjectId, ADScope> exclusiveConfigScopes, bool onlyExclusive)
		{
			List<ExchangeRoleAssignment> list = new List<ExchangeRoleAssignment>();
			if (onlyExclusive)
			{
				return this.GetExclusiveEffectiveRoleAssignmentsForRecipient(recipientObject, roleAssignments, exclusiveConfigScopes);
			}
			ADObjectId[] array = null;
			foreach (ExchangeRoleAssignment exchangeRoleAssignment in roleAssignments)
			{
				if (this.IsValid(exchangeRoleAssignment))
				{
					switch (exchangeRoleAssignment.RecipientWriteScope)
					{
					case RecipientWriteScopeType.Organization:
						list.Add(exchangeRoleAssignment);
						break;
					case RecipientWriteScopeType.Self:
						if (array == null)
						{
							List<string> tokenSids = this.recipientSession.GetTokenSids(recipientObject, AssignmentMethod.All);
							if (tokenSids == null || tokenSids.Count < 1)
							{
								ExTraceGlobals.ADConfigTracer.TraceError(0L, "ManagementReporting: GetEffectedRoleAssignmentForRecipient(), Token Sid is emptry, there should be at least one entry");
								break;
							}
							array = this.recipientSession.ResolveSidsToADObjectIds(tokenSids.ToArray());
							if (this.sharedConfig != null)
							{
								array = this.sharedConfig.GetSharedRoleGroupIds(array);
							}
						}
						foreach (ADObjectId adobjectId in array)
						{
							if (adobjectId.Equals(exchangeRoleAssignment.User))
							{
								list.Add(exchangeRoleAssignment);
								break;
							}
						}
						break;
					case RecipientWriteScopeType.OU:
					{
						RbacScope rbacScope = new RbacScope(ScopeType.OU, exchangeRoleAssignment.CustomRecipientWriteScope, exchangeRoleAssignment.IsFromEndUserRole);
						ADRecipient adrecipient = this.recipientSession.Read(exchangeRoleAssignment.User);
						if (adrecipient == null)
						{
							ExTraceGlobals.ADConfigTracer.TraceError(0L, "ManagementReporting: GetEffectedRoleAssignmentForRecipient(), roleAssignment.User does not have a corresponding entry in AD");
						}
						else
						{
							rbacScope.PopulateRootAndFilter(this.orgId, adrecipient);
							if (ManagementReporting.VerifyIsWithinScopes(recipientObject, null, rbacScope, new RbacScope(exchangeRoleAssignment.RecipientReadScope)))
							{
								list.Add(exchangeRoleAssignment);
							}
						}
						break;
					}
					case RecipientWriteScopeType.CustomRecipientScope:
						if (customConfigScopes.ContainsKey(exchangeRoleAssignment.CustomRecipientWriteScope))
						{
							if (ManagementReporting.VerifyIsWithinScopes(recipientObject, null, customConfigScopes[exchangeRoleAssignment.CustomRecipientWriteScope], new RbacScope(exchangeRoleAssignment.RecipientReadScope)))
							{
								list.Add(exchangeRoleAssignment);
							}
						}
						else
						{
							ExTraceGlobals.ADConfigTracer.TraceError<ADObjectId>(0L, "Custom Recipient Scope '{0}' was not found.", exchangeRoleAssignment.CustomRecipientWriteScope);
						}
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000F090C File Offset: 0x000EEB0C
		internal List<ExchangeRoleAssignment> FindRoleAssignmentsWithWritableServer(Server serverObject, IEnumerable<ExchangeRoleAssignment> roleAssignments)
		{
			return this.FindRoleAssignmentsWithWritableConfigObject(ScopeRestrictionType.ServerScope, serverObject, roleAssignments);
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000F0917 File Offset: 0x000EEB17
		internal List<ExchangeRoleAssignment> FindRoleAssignmentsWithWritableDatabase(Database database, IEnumerable<ExchangeRoleAssignment> roleAssignments)
		{
			return this.FindRoleAssignmentsWithWritableConfigObject(ScopeRestrictionType.DatabaseScope, database, roleAssignments);
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000F0924 File Offset: 0x000EEB24
		private List<ExchangeRoleAssignment> FindRoleAssignmentsWithWritableConfigObject(ScopeRestrictionType restrictionType, ADConfigurationObject writableObject, IEnumerable<ExchangeRoleAssignment> roleAssignments)
		{
			Dictionary<ADObjectId, ADScope> customConfigScopes;
			Dictionary<ADObjectId, ADScope> dictionary;
			this.RetrieveAllScopes(restrictionType, writableObject, out customConfigScopes, out dictionary);
			if (dictionary.Count > 0 && ManagementReporting.VerifyIsWithinScopes(writableObject, new List<ADScope>(dictionary.Values), new List<ADScope>(dictionary.Values), new RbacScope(ScopeType.Organization)))
			{
				return this.GetEffectiveRoleAssignmentsForConfigObject(writableObject, roleAssignments, customConfigScopes, dictionary, true);
			}
			return this.GetEffectiveRoleAssignmentsForConfigObject(writableObject, roleAssignments, customConfigScopes, dictionary, false);
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000F0984 File Offset: 0x000EEB84
		private List<ExchangeRoleAssignment> GetEffectiveRoleAssignmentsForConfigObject(ADConfigurationObject configObject, IEnumerable<ExchangeRoleAssignment> roleAssignments, Dictionary<ADObjectId, ADScope> customConfigScopes, Dictionary<ADObjectId, ADScope> exclusiveConfigScopes, bool onlyExclusive)
		{
			List<ExchangeRoleAssignment> list = new List<ExchangeRoleAssignment>();
			if (onlyExclusive)
			{
				foreach (ExchangeRoleAssignment exchangeRoleAssignment in roleAssignments)
				{
					if (this.IsValid(exchangeRoleAssignment) && exchangeRoleAssignment.ConfigWriteScope == ConfigWriteScopeType.ExclusiveConfigScope && exclusiveConfigScopes.ContainsKey(exchangeRoleAssignment.CustomConfigWriteScope) && ManagementReporting.VerifyIsWithinScopes(configObject, exclusiveConfigScopes[exchangeRoleAssignment.CustomConfigWriteScope], exclusiveConfigScopes[exchangeRoleAssignment.CustomConfigWriteScope], new RbacScope(exchangeRoleAssignment.ConfigReadScope)))
					{
						list.Add(exchangeRoleAssignment);
					}
				}
				return list;
			}
			foreach (ExchangeRoleAssignment exchangeRoleAssignment2 in roleAssignments)
			{
				if (this.IsValid(exchangeRoleAssignment2))
				{
					switch (exchangeRoleAssignment2.ConfigWriteScope)
					{
					case ConfigWriteScopeType.OrganizationConfig:
						list.Add(exchangeRoleAssignment2);
						break;
					case ConfigWriteScopeType.CustomConfigScope:
						if (customConfigScopes.ContainsKey(exchangeRoleAssignment2.CustomConfigWriteScope) && ManagementReporting.VerifyIsWithinScopes(configObject, null, customConfigScopes[exchangeRoleAssignment2.CustomConfigWriteScope], new RbacScope(exchangeRoleAssignment2.ConfigReadScope)))
						{
							list.Add(exchangeRoleAssignment2);
						}
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000F0AC0 File Offset: 0x000EECC0
		private bool IsValid(ExchangeRoleAssignment dataObject)
		{
			ValidationError[] array = dataObject.Validate();
			if (array != null && array.Length > 0)
			{
				if (dataObject.Identity != null)
				{
					this.WriteWarning(Strings.ErrorObjectHasValidationErrorsWithId(dataObject.Identity.ToString()));
				}
				else
				{
					this.WriteWarning(Strings.ErrorObjectHasValidationErrors);
				}
				foreach (ValidationError validationError in array)
				{
					this.WriteWarning(validationError.Description);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000F0B3C File Offset: 0x000EED3C
		private void RetrieveAllScopes(ScopeRestrictionType scopeType, ADRawEntry givenObject, out Dictionary<ADObjectId, ADScope> customScopes, out Dictionary<ADObjectId, ADScope> exclusiveScopes)
		{
			customScopes = new Dictionary<ADObjectId, ADScope>();
			exclusiveScopes = new Dictionary<ADObjectId, ADScope>();
			ScopeType scopeType2 = ScopeType.ExclusiveConfigScope;
			ScopeType scopeType3 = ScopeType.CustomConfigScope;
			if (scopeType == ScopeRestrictionType.RecipientScope)
			{
				scopeType2 = ScopeType.ExclusiveRecipientScope;
				scopeType3 = ScopeType.CustomRecipientScope;
			}
			ADPagedReader<ManagementScope> allScopes = this.configSession.GetAllScopes(this.orgId, scopeType);
			foreach (ManagementScope managementScope in allScopes)
			{
				if (managementScope.ScopeRestrictionType == scopeType)
				{
					ExchangeRunspaceConfiguration.TryStampQueryFilterOnManagementScope(managementScope);
					if (managementScope.Exclusive)
					{
						RbacScope rbacScope = new RbacScope(scopeType2, managementScope);
						rbacScope.PopulateRootAndFilter(this.orgId, givenObject);
						exclusiveScopes.Add(managementScope.OriginalId, rbacScope);
					}
					RbacScope rbacScope2 = new RbacScope(scopeType3, managementScope);
					rbacScope2.PopulateRootAndFilter(this.orgId, givenObject);
					customScopes.Add(managementScope.OriginalId, rbacScope2);
				}
			}
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x000F0C18 File Offset: 0x000EEE18
		private static bool VerifyIsWithinScopes(ADRawEntry givenObject, List<ADScope> exclusiveScopes, List<ADScope> customScopes, ADScope readScope)
		{
			List<ADScopeCollection> list = null;
			ADScopeCollection exclusiveScopes2 = null;
			if (exclusiveScopes != null && exclusiveScopes.Count > 0)
			{
				exclusiveScopes2 = new ADScopeCollection(exclusiveScopes);
			}
			if (customScopes != null && customScopes.Count > 0)
			{
				list = new List<ADScopeCollection>();
				list.Add(new ADScopeCollection(customScopes));
			}
			ADScopeException ex;
			return ADSession.TryVerifyIsWithinScopes(givenObject, readScope, list, exclusiveScopes2, true, out ex);
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000F0C68 File Offset: 0x000EEE68
		private static bool VerifyIsWithinScopes(ADRawEntry givenObject, ADScope exclusive, ADScope customScope, ADScope readScope)
		{
			List<ADScope> list = null;
			if (exclusive != null)
			{
				list = new List<ADScope>();
				list.Add(exclusive);
			}
			List<ADScope> list2 = new List<ADScope>();
			if (customScope != null)
			{
				list2.Add(customScope);
			}
			return ManagementReporting.VerifyIsWithinScopes(givenObject, list, list2, readScope);
		}

		// Token: 0x04002610 RID: 9744
		private IConfigurationSession configSession;

		// Token: 0x04002611 RID: 9745
		private IRecipientSession recipientSession;

		// Token: 0x04002612 RID: 9746
		private SharedConfiguration sharedConfig;

		// Token: 0x04002613 RID: 9747
		private OrganizationId orgId;

		// Token: 0x04002614 RID: 9748
		private Task.TaskWarningLoggingDelegate WriteWarning;
	}
}
