using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020001F0 RID: 496
	internal class ADDynamicGroupPagedReader<TResult> : ADPagedReader<TResult> where TResult : IConfigurable, new()
	{
		// Token: 0x0600197B RID: 6523 RVA: 0x0006C0C4 File Offset: 0x0006A2C4
		internal ADDynamicGroupPagedReader(IDirectorySession session, ADObjectId rootId, QueryScope scope, string ldapFilter, int pageSize, CustomExceptionHandler customExceptionHandler, IEnumerable<PropertyDefinition> properties) : base(session, rootId, scope, new CustomLdapFilter(ldapFilter), null, pageSize, properties, false)
		{
			base.CustomExceptionHandler = customExceptionHandler;
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0006C0F0 File Offset: 0x0006A2F0
		protected override SearchResultEntryCollection GetNextResultCollection()
		{
			SearchResultEntryCollection result;
			try
			{
				result = base.GetNextResultCollection();
			}
			catch (DataValidationException)
			{
				base.RetrievedAllData = new bool?(true);
				result = null;
			}
			return result;
		}
	}
}
