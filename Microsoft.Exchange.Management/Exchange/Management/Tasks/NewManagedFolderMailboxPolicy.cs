using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200043B RID: 1083
	[Cmdlet("new", "ManagedFolderMailboxPolicy", SupportsShouldProcess = true)]
	public sealed class NewManagedFolderMailboxPolicy : NewMailboxPolicyBase<ManagedFolderMailboxPolicy>
	{
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x00098433 File Offset: 0x00096633
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewManagedFolderMailboxPolicy(base.Name.ToString());
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x00098445 File Offset: 0x00096645
		// (set) Token: 0x0600261E RID: 9758 RVA: 0x0009845C File Offset: 0x0009665C
		[Parameter(Mandatory = false)]
		public ELCFolderIdParameter[] ManagedFolderLinks
		{
			get
			{
				return (ELCFolderIdParameter[])base.Fields["ManagedFolderLinks"];
			}
			set
			{
				base.Fields["ManagedFolderLinks"] = value;
			}
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x00098470 File Offset: 0x00096670
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.ManagedFolderLinks != null)
			{
				foreach (ELCFolderIdParameter elcfolderIdParameter in this.ManagedFolderLinks)
				{
					IConfigurable dataObject = base.GetDataObject<ELCFolder>(elcfolderIdParameter, base.DataSession, null, new LocalizedString?(Strings.ErrorElcFolderNotFound(elcfolderIdParameter.ToString())), new LocalizedString?(Strings.ErrorAmbiguousElcFolderId(elcfolderIdParameter.ToString())));
					IConfigurationSession session = base.DataSession as IConfigurationSession;
					ValidationError validationError = this.DataObject.AddManagedFolderToPolicy(session, (ELCFolder)dataObject);
					if (validationError != null)
					{
						base.WriteError(new InvalidOperationException(validationError.Description), ErrorCategory.InvalidOperation, null);
					}
				}
			}
		}
	}
}
