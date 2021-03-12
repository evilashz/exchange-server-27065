using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200043E RID: 1086
	[Cmdlet("set", "ManagedFolderMailboxPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetManagedFolderMailboxPolicy : SetMailboxPolicyBase<ManagedFolderMailboxPolicy>
	{
		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x00098608 File Offset: 0x00096808
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetManagedFolderMailboxPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x0009861A File Offset: 0x0009681A
		// (set) Token: 0x0600262A RID: 9770 RVA: 0x00098631 File Offset: 0x00096831
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

		// Token: 0x0600262B RID: 9771 RVA: 0x00098644 File Offset: 0x00096844
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (base.Fields.IsModified("ManagedFolderLinks"))
			{
				this.DataObject.ManagedFolderLinks.Clear();
				if (this.ManagedFolderLinks != null)
				{
					foreach (ELCFolderIdParameter elcfolderIdParameter in this.ManagedFolderLinks)
					{
						IConfigurable dataObject = base.GetDataObject<ELCFolder>(elcfolderIdParameter, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorElcFolderNotFound(elcfolderIdParameter.ToString())), new LocalizedString?(Strings.ErrorAmbiguousElcFolderId(elcfolderIdParameter.ToString())));
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
}
