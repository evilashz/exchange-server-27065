using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000DC RID: 220
	[Cmdlet("Get", "SyncGroup", DefaultParameterSetName = "Identity")]
	public sealed class GetSyncGroup : GetRecipientBase<NonMailEnabledGroupIdParameter, ADGroup>
	{
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0003CF8C File Offset: 0x0003B18C
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x0003CF94 File Offset: 0x0003B194
		[Parameter(Mandatory = false)]
		public new long UsnForReconciliationSearch
		{
			get
			{
				return base.UsnForReconciliationSearch;
			}
			set
			{
				base.UsnForReconciliationSearch = value;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x0003CF9D File Offset: 0x0003B19D
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetSyncGroup.SortPropertiesArray;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0003CFA4 File Offset: 0x0003B1A4
		protected override RecipientType[] RecipientTypes
		{
			get
			{
				return NonMailEnabledGroupIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0003CFAB File Offset: 0x0003B1AB
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return NonMailEnabledGroupIdParameter.AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0003CFB2 File Offset: 0x0003B1B2
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncGroup.FromDataObject((ADGroup)dataObject);
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0003CFBF File Offset: 0x0003B1BF
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<SyncGroupSchema>();
			}
		}

		// Token: 0x040002FE RID: 766
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			ADRecipientSchema.DisplayName
		};
	}
}
