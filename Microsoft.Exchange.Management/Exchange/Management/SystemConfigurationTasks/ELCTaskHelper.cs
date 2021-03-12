﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000314 RID: 788
	internal static class ELCTaskHelper
	{
		// Token: 0x06001A77 RID: 6775 RVA: 0x00075168 File Offset: 0x00073368
		internal static List<ElcContentSettings> FindELCContentSettings(IConfigurationSession session, string name)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, name);
			return ELCTaskHelper.FindElcObject<ElcContentSettings>(session, null, filter);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0007518C File Offset: 0x0007338C
		internal static List<ELCFolder> FindELCFolder(IConfigurationSession session, object valueToSearch, FindByType findByType)
		{
			return ELCTaskHelper.FindELCFolder(session, null, valueToSearch, findByType);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00075198 File Offset: 0x00073398
		internal static List<ELCFolder> FindELCFolder(IConfigurationSession session, ADObjectId rootId, object valueToSearch, FindByType findByType)
		{
			QueryFilter filter;
			switch (findByType)
			{
			case FindByType.Name:
				filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, (string)valueToSearch);
				goto IL_D4;
			case FindByType.FolderName:
				filter = new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ELCFolderSchema.FolderName, (string)valueToSearch),
					new TextFilter(ELCFolderSchema.LocalizedFolderName, (string)valueToSearch, MatchOptions.SubString, MatchFlags.IgnoreCase)
				});
				goto IL_D4;
			case FindByType.FolderType:
				filter = new ComparisonFilter(ComparisonOperator.Equal, ELCFolderSchema.FolderType, (ElcFolderType)valueToSearch);
				goto IL_D4;
			case FindByType.FolderAdObjectId:
				filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, valueToSearch as ADObjectId);
				goto IL_D4;
			case FindByType.FolderDefaultType:
				filter = new ComparisonFilter(ComparisonOperator.NotEqual, ELCFolderSchema.FolderType, ElcFolderType.ManagedCustomFolder);
				goto IL_D4;
			case FindByType.FolderOrganizationalType:
				filter = new ComparisonFilter(ComparisonOperator.Equal, ELCFolderSchema.FolderType, ElcFolderType.ManagedCustomFolder);
				goto IL_D4;
			}
			filter = null;
			IL_D4:
			return ELCTaskHelper.FindElcObject<ELCFolder>(session, rootId, filter);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00075284 File Offset: 0x00073484
		internal static List<RetentionPolicyTag> FindRetentionPolicyTag(IConfigurationSession session, Guid tagGuid)
		{
			OrFilter filter = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, RetentionPolicyTagSchema.RetentionId, tagGuid),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, tagGuid)
			});
			return ELCTaskHelper.FindElcObject<RetentionPolicyTag>(session, null, filter);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000752D0 File Offset: 0x000734D0
		internal static string CheckELCFolderNameUniqueness(IConfigurationSession session, ADObjectId id, string newFolderName, MultiValuedProperty<string> newLocalizedFolderNames)
		{
			ADPagedReader<ELCFolder> adpagedReader = session.FindAllPaged<ELCFolder>();
			foreach (ELCFolder elcfolder in adpagedReader)
			{
				if (id == null || id != elcfolder.Id)
				{
					if (string.Compare(newFolderName, elcfolder.FolderName, true, CultureInfo.InvariantCulture) == 0)
					{
						return newFolderName;
					}
					if (elcfolder.LocalizedFolderName != null)
					{
						foreach (string text in elcfolder.LocalizedFolderName)
						{
							int num = text.IndexOf(':');
							if (string.Compare(newFolderName, 0, text, num + 1, 2147483647, true, CultureInfo.InvariantCulture) == 0)
							{
								return newFolderName;
							}
							foreach (string text2 in newLocalizedFolderNames)
							{
								int num2 = text2.IndexOf(':');
								if (string.Compare(text2, num2 + 1, text, num + 1, 2147483647, true, CultureInfo.InvariantCulture) == 0)
								{
									return text2;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00075454 File Offset: 0x00073654
		internal static MailboxSession OpenMailboxSession(ADUser adUser, string clientString, Task.TaskErrorLoggingDelegate writeError)
		{
			LocalizedString? localizedString = null;
			try
			{
				ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(adUser.OrganizationId.ToADSessionSettings(), adUser);
				if (exchangePrincipal == null)
				{
					return null;
				}
				return MailboxSession.OpenAsAdmin(exchangePrincipal, CultureInfo.InvariantCulture, clientString);
			}
			catch (StorageTransientException ex)
			{
				localizedString = new LocalizedString?(Strings.ExceptionStorageOther(ex.ErrorCode, ex.Message));
			}
			catch (StoragePermanentException ex2)
			{
				if (ex2 is AccessDeniedException)
				{
					localizedString = new LocalizedString?(Strings.ExceptionStorageAccessDenied(ex2.ErrorCode, ex2.Message));
				}
				else
				{
					localizedString = new LocalizedString?(Strings.ExceptionStorageOther(ex2.ErrorCode, ex2.Message));
				}
			}
			if (localizedString != null)
			{
				writeError(new TaskException(localizedString.Value), ErrorCategory.ReadError, null);
			}
			return null;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0007552C File Offset: 0x0007372C
		internal static void VerifyIsInScopes(ADObject adObject, ScopeSet scopeSet, Task.TaskErrorLoggingDelegate writeErrorDelegate)
		{
			ADScopeException ex;
			if (!ADSession.TryVerifyIsWithinScopes(adObject, scopeSet.RecipientReadScope, scopeSet.RecipientWriteScopes, scopeSet.ExclusiveRecipientScopes, false, out ex))
			{
				writeErrorDelegate(new TaskException(Strings.ErrorCannotChangeObjectOutOfWriteScope(adObject.Identity.ToString(), (ex == null) ? string.Empty : ex.Message), ex), ErrorCategory.PermissionDenied, null);
			}
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x00075588 File Offset: 0x00073788
		internal static void VerifyIsInConfigScopes(ADObject adObject, ADSessionSettings sessionSettings, Task.TaskErrorLoggingDelegate writeErrorDelegate)
		{
			IDirectorySession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 312, "VerifyIsInConfigScopes", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Elc\\ELCTaskHelper.cs");
			ADScopeException ex;
			if (!tenantOrTopologyConfigurationSession.TryVerifyIsWithinScopes(adObject, false, out ex))
			{
				writeErrorDelegate(new TaskException(Strings.ErrorCannotChangeObjectOutOfWriteScope(adObject.Identity.ToString(), (ex == null) ? string.Empty : ex.Message), ex), ErrorCategory.PermissionDenied, null);
			}
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x000755EC File Offset: 0x000737EC
		private static List<Tobject> FindElcObject<Tobject>(IConfigurationSession session, ADObjectId rootId, QueryFilter filter) where Tobject : ADConfigurationObject, new()
		{
			List<Tobject> list = null;
			if (session == null)
			{
				throw new ArgumentNullException(Strings.ErrorSessionNotFound);
			}
			ADPagedReader<Tobject> adpagedReader = session.FindPaged<Tobject>(rootId, QueryScope.SubTree, filter, null, 0);
			if (adpagedReader != null)
			{
				list = new List<Tobject>();
				foreach (Tobject item in adpagedReader)
				{
					list.Add(item);
				}
			}
			if (list == null || list.Count <= 0)
			{
				return null;
			}
			return list;
		}
	}
}
