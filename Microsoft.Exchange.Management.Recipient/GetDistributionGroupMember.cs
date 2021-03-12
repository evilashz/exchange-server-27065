using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000020 RID: 32
	[Cmdlet("Get", "DistributionGroupMember")]
	public sealed class GetDistributionGroupMember : GetRecipientObjectTask<DistributionGroupMemberIdParameter, ReducedRecipient>
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00009220 File Offset: 0x00007420
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00009237 File Offset: 0x00007437
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true)]
		public override DistributionGroupMemberIdParameter Identity
		{
			get
			{
				return (DistributionGroupMemberIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000924A File Offset: 0x0000744A
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00009252 File Offset: 0x00007452
		[Parameter]
		public SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return base.InternalIgnoreDefaultScope;
			}
			set
			{
				base.InternalIgnoreDefaultScope = value;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000925C File Offset: 0x0000745C
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession reducedRecipientSession = DirectorySessionFactory.Default.GetReducedRecipientSession((IRecipientSession)base.CreateSession(), 58, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\DistributionList\\GetDistributionGroupMember.cs");
			if (base.InternalIgnoreDefaultScope.IsPresent)
			{
				reducedRecipientSession.EnforceDefaultScope = false;
				reducedRecipientSession.UseGlobalCatalog = true;
			}
			return reducedRecipientSession;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000092AC File Offset: 0x000074AC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			LocalizedString? localizedString;
			IEnumerable<ReducedRecipient> dataObjects = base.GetDataObjects(this.Identity, base.OptionalIdentityData, out localizedString);
			this.WriteResult<ReducedRecipient>(dataObjects);
			if (!base.HasErrors && localizedString != null)
			{
				base.WriteError(new ManagementObjectNotFoundException(localizedString.Value), ErrorCategory.InvalidData, null);
			}
			TaskLogger.LogExit();
		}
	}
}
