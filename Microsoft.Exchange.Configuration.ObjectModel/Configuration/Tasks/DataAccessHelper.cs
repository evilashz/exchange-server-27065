using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000052 RID: 82
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class DataAccessHelper
	{
		// Token: 0x06000373 RID: 883 RVA: 0x0000D180 File Offset: 0x0000B380
		public static bool IsDataAccessKnownException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			for (int i = 0; i < DataAccessHelper.knownExceptionTypes.Length; i++)
			{
				if (DataAccessHelper.knownExceptionTypes[i].IsInstanceOfType(exception))
				{
					return true;
				}
			}
			return TaskHelper.IsTaskKnownException(exception);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
		public static ExchangeErrorCategory ResolveExceptionErrorCategory(Exception exception)
		{
			if (typeof(ManagementObjectNotFoundException).IsInstanceOfType(exception))
			{
				return ExchangeErrorCategory.Context;
			}
			if (typeof(TransientException).IsInstanceOfType(exception))
			{
				return ExchangeErrorCategory.ServerTransient;
			}
			if (typeof(DataSourceOperationException).IsInstanceOfType(exception))
			{
				if (typeof(ADFilterException).IsInstanceOfType(exception) || typeof(ADInvalidPasswordException).IsInstanceOfType(exception))
				{
					return ExchangeErrorCategory.Client;
				}
				if (typeof(ADObjectAlreadyExistsException).IsInstanceOfType(exception) || typeof(ADObjectEntryAlreadyExistsException).IsInstanceOfType(exception) || typeof(ADNoSuchObjectException).IsInstanceOfType(exception) || typeof(ADRemoveContainerException).IsInstanceOfType(exception))
				{
					return ExchangeErrorCategory.Context;
				}
				if (typeof(ADScopeException).IsInstanceOfType(exception) || typeof(ADInvalidCredentialException).IsInstanceOfType(exception))
				{
					return ExchangeErrorCategory.Authorization;
				}
				return ExchangeErrorCategory.ServerOperation;
			}
			else
			{
				if (typeof(DataValidationException).IsInstanceOfType(exception))
				{
					return ExchangeErrorCategory.Context;
				}
				if (typeof(ManagementObjectAmbiguousException).IsInstanceOfType(exception))
				{
					return ExchangeErrorCategory.Context;
				}
				return (ExchangeErrorCategory)0;
			}
		}

		// Token: 0x040000E0 RID: 224
		private static Type[] knownExceptionTypes = new Type[]
		{
			typeof(ManagementObjectNotFoundException),
			typeof(ManagementObjectAmbiguousException)
		};

		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x06000377 RID: 887
		internal delegate IConfigurable GetDataObjectDelegate(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, OptionalIdentityData optionalData, LocalizedString? notFoundError, LocalizedString? multipleFoundError);

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x0600037B RID: 891
		internal delegate IConfigurable CategorizedGetDataObjectDelegate(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, OptionalIdentityData optionalData, LocalizedString? notFoundError, LocalizedString? multipleFoundError, ExchangeErrorCategory category);
	}
}
