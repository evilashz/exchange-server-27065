using System;
using System.Data;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000160 RID: 352
	public class SecurityIdListEditor : ListEditorBase<SecurityIdentifier>
	{
		// Token: 0x06000E72 RID: 3698 RVA: 0x000373ED File Offset: 0x000355ED
		public SecurityIdListEditor()
		{
			base.ObjectFilterProperty = IADSecurityPrincipalSchema.Sid;
			base.Name = "SecurityIdListEditor";
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0003740B File Offset: 0x0003560B
		protected override void InsertChangedObject(DataRow dataRow)
		{
			base.ChangedObjects.Add(dataRow["Sid"] as SecurityIdentifier, (dataRow["UserFriendlyName"] ?? string.Empty).ToString());
		}
	}
}
