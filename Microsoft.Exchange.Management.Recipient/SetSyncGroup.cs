using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000DB RID: 219
	[Cmdlet("Set", "SyncGroup", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetSyncGroup : SetRecipientObjectTask<NonMailEnabledGroupIdParameter, SyncGroup, ADGroup>
	{
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0003CE83 File Offset: 0x0003B083
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetGroup(this.Identity.ToString());
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0003CE95 File Offset: 0x0003B095
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x0003CEB6 File Offset: 0x0003B0B6
		[Parameter]
		public GroupType Type
		{
			get
			{
				return (GroupType)(base.Fields[ADGroupSchema.GroupType] ?? GroupType.Distribution);
			}
			set
			{
				base.Fields[ADGroupSchema.GroupType] = value;
			}
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0003CED0 File Offset: 0x0003B0D0
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADGroup adgroup = (ADGroup)base.PrepareDataObject();
			if (base.Fields.IsModified(ADGroupSchema.GroupType))
			{
				if (this.Type != GroupType.Distribution && this.Type != GroupType.Security)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorGroupTypeInvalid), ExchangeErrorCategory.Client, null);
				}
				bool flag = (adgroup.GroupType & GroupTypeFlags.SecurityEnabled) == GroupTypeFlags.SecurityEnabled;
				if (this.Type == GroupType.Distribution && flag)
				{
					adgroup.GroupType &= (GroupTypeFlags)2147483647;
				}
				else if (this.Type == GroupType.Security && !flag)
				{
					adgroup.GroupType |= GroupTypeFlags.SecurityEnabled;
				}
			}
			return adgroup;
		}
	}
}
