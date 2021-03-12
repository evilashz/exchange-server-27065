using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x0200036C RID: 876
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FolderBasedCalendarItemStateDefinition : SinglePropertyValueBasedCalendarItemStateDefinition<byte[]>
	{
		// Token: 0x060026C0 RID: 9920 RVA: 0x0009B3A6 File Offset: 0x000995A6
		public FolderBasedCalendarItemStateDefinition(byte[] targetFolderId) : base(InternalSchema.OriginalFolderId, targetFolderId, ArrayComparer<byte>.Comparer)
		{
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x0009B3B9 File Offset: 0x000995B9
		public override string SchemaKey
		{
			get
			{
				return "{4BFFBA09-A6D3-4144-A2B5-0FD7B57C3FFD}";
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x0009B3C0 File Offset: 0x000995C0
		public override StorePropertyDefinition[] RequiredProperties
		{
			get
			{
				return FolderBasedCalendarItemStateDefinition.requiredProperties;
			}
		}

		// Token: 0x0400170F RID: 5903
		private static readonly StorePropertyDefinition[] requiredProperties = new StorePropertyDefinition[]
		{
			CalendarItemBaseSchema.ClientIntent,
			InternalSchema.OriginalFolderId
		};
	}
}
