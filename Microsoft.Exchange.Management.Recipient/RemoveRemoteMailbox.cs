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
	// Token: 0x020000B9 RID: 185
	[Cmdlet("Remove", "RemoteMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveRemoteMailbox : RemoveMailUserOrRemoteMailboxBase<RemoteMailboxIdParameter>
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00030D70 File Offset: 0x0002EF70
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveRemoteMailbox(this.Identity.ToString());
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00030D82 File Offset: 0x0002EF82
		// (set) Token: 0x06000B95 RID: 2965 RVA: 0x00030D8A File Offset: 0x0002EF8A
		private new SwitchParameter KeepWindowsLiveID
		{
			get
			{
				return base.KeepWindowsLiveID;
			}
			set
			{
				base.KeepWindowsLiveID = value;
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00030D93 File Offset: 0x0002EF93
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return RemoteMailbox.FromDataObject((ADUser)dataObject);
		}
	}
}
