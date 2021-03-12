using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200034A RID: 842
	[Cmdlet("Get", "FailedMSOSyncObject", DefaultParameterSetName = "Identity")]
	public sealed class GetFailedMSOSyncObject : GetObjectWithIdentityTaskBase<FailedMSOSyncObjectIdParameter, FailedMSOSyncObject>
	{
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x00080694 File Offset: 0x0007E894
		// (set) Token: 0x06001D0A RID: 7434 RVA: 0x000806AC File Offset: 0x0007E8AC
		[Parameter]
		[ValidateNotNullOrEmpty]
		public string Filter
		{
			get
			{
				return (string)base.Fields["Filter"];
			}
			set
			{
				MonadFilter monadFilter = new MonadFilter(value, this, this.FilterableObjectSchema);
				this.inputFilter = monadFilter.InnerFilter;
				base.Fields["Filter"] = value;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x000806E4 File Offset: 0x0007E8E4
		internal ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<FailedMSOSyncObjectSchema>();
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x000806EC File Offset: 0x0007E8EC
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter internalFilter = base.InternalFilter;
				if (this.inputFilter == null)
				{
					return internalFilter;
				}
				if (internalFilter != null)
				{
					return new AndFilter(new QueryFilter[]
					{
						internalFilter,
						this.inputFilter
					});
				}
				return this.inputFilter;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0008072E File Offset: 0x0007E92E
		protected override ObjectId RootId
		{
			get
			{
				return ForwardSyncDataAccessHelper.GetRootId(this.Identity);
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0008073B File Offset: 0x0007E93B
		protected override bool DeepSearch
		{
			get
			{
				return this.Identity == null || !this.Identity.IsServiceInstanceDefinied;
			}
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x00080755 File Offset: 0x0007E955
		protected override IConfigDataProvider CreateSession()
		{
			return ForwardSyncDataAccessHelper.CreateSession(false);
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0008075D File Offset: 0x0007E95D
		protected override void WriteResult(IConfigurable dataObject)
		{
			base.WriteResult(new FailedMSOSyncObjectPresentationObject((FailedMSOSyncObject)dataObject));
		}

		// Token: 0x04001872 RID: 6258
		private QueryFilter inputFilter;
	}
}
