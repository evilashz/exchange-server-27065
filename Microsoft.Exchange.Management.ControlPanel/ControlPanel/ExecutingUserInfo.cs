using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005DD RID: 1501
	public class ExecutingUserInfo : ValuePair
	{
		// Token: 0x06004379 RID: 17273 RVA: 0x000CC456 File Offset: 0x000CA656
		public ExecutingUserInfo()
		{
			this.ValueProperty = "PrimarySmtpAddress";
		}

		// Token: 0x1700262B RID: 9771
		// (get) Token: 0x0600437A RID: 17274 RVA: 0x000CC469 File Offset: 0x000CA669
		// (set) Token: 0x0600437B RID: 17275 RVA: 0x000CC471 File Offset: 0x000CA671
		[DefaultValue("PrimarySmtpAddress")]
		public string ValueProperty { get; set; }

		// Token: 0x1700262C RID: 9772
		// (get) Token: 0x0600437C RID: 17276 RVA: 0x000CC47C File Offset: 0x000CA67C
		// (set) Token: 0x0600437D RID: 17277 RVA: 0x000CC4C6 File Offset: 0x000CA6C6
		[DefaultValue("")]
		public override object Value
		{
			get
			{
				RecipientObjectResolverRow recipientObjectResolverRow = RecipientObjectResolver.Instance.ResolveObjects(new ADObjectId[]
				{
					RbacPrincipal.Current.ExecutingUserId
				}).FirstOrDefault<RecipientObjectResolverRow>();
				return recipientObjectResolverRow.GetType().GetProperty(this.ValueProperty).GetValue(recipientObjectResolverRow, null);
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
