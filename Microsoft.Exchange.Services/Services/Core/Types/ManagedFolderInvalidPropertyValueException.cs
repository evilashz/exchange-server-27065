using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007FF RID: 2047
	internal sealed class ManagedFolderInvalidPropertyValueException : ServicePermanentException
	{
		// Token: 0x06003BEE RID: 15342 RVA: 0x000D4E78 File Offset: 0x000D3078
		static ManagedFolderInvalidPropertyValueException()
		{
			ManagedFolderInvalidPropertyValueException.errorMappings.Add(FolderSchema.AdminFolderFlags.Name, CoreResources.IDs.ErrorInvalidManagedFolderProperty);
			ManagedFolderInvalidPropertyValueException.errorMappings.Add(FolderSchema.ELCPolicyIds.Name, (CoreResources.IDs)2518142400U);
			ManagedFolderInvalidPropertyValueException.errorMappings.Add(FolderSchema.FolderQuota.Name, (CoreResources.IDs)2756368512U);
			ManagedFolderInvalidPropertyValueException.errorMappings.Add(FolderSchema.FolderSize.Name, (CoreResources.IDs)4227165423U);
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x000D4F07 File Offset: 0x000D3107
		public ManagedFolderInvalidPropertyValueException(string propertyName) : base(ManagedFolderInvalidPropertyValueException.errorMappings[propertyName])
		{
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x000D4F1A File Offset: 0x000D311A
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}

		// Token: 0x040020FD RID: 8445
		private static Dictionary<string, Enum> errorMappings = new Dictionary<string, Enum>();
	}
}
