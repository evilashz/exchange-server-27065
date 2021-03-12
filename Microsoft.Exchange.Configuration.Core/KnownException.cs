using System;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000016 RID: 22
	internal static class KnownException
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00004A84 File Offset: 0x00002C84
		internal static bool IsKnownException(Exception ex)
		{
			if (ex == null)
			{
				return false;
			}
			string fullName = ex.GetType().FullName;
			return KnownException.KnownExceptionInPlainStringList.Contains(fullName) || ex is TransientException || ex is OverBudgetException || ex is CannotResolveTenantNameException || ex is DataSourceOperationException || ex is LdapException;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004ADC File Offset: 0x00002CDC
		internal static bool IsUnhandledException(Exception ex)
		{
			return !KnownException.IsKnownException(ex);
		}

		// Token: 0x0400005A RID: 90
		private static readonly HashSet<string> KnownExceptionInPlainStringList = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"Microsoft.Exchange.Configuration.Authorization.AuthorizationException",
			"Microsoft.Exchange.Configuration.Authorization.CmdletAccessDeniedException",
			"Microsoft.Exchange.Configuration.Authorization.ImpersonationDeniedException",
			"Microsoft.Exchange.Configuration.Authorization.AppPasswordLoginException",
			"Microsoft.Exchange.Configuration.Authorization.FilteringOnlyUserForFfoLoginException",
			"Microsoft.Exchange.Configuration.Authorization.NonMigratedUserDeniedException",
			"Microsoft.Exchange.Configuration.Authorization.RBACContextParserException"
		};
	}
}
