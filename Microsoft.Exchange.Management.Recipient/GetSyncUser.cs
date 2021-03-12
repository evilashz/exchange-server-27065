using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000D7 RID: 215
	[Cmdlet("Get", "SyncUser", DefaultParameterSetName = "Identity")]
	public sealed class GetSyncUser : GetADUserBase<NonMailEnabledUserIdParameter>
	{
		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0003C75A File Offset: 0x0003A95A
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<SyncUserSchema>();
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x0003C761 File Offset: 0x0003A961
		// (set) Token: 0x060010B5 RID: 4277 RVA: 0x0003C769 File Offset: 0x0003A969
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

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0003C772 File Offset: 0x0003A972
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return NonMailEnabledUserIdParameter.AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0003C779 File Offset: 0x0003A979
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncUser.FromDataObject((ADUser)dataObject);
		}
	}
}
