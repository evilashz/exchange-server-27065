using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200007A RID: 122
	[OutputType(new Type[]
	{
		typeof(MailContact)
	})]
	[Cmdlet("Get", "MailContact", DefaultParameterSetName = "Identity")]
	public sealed class GetMailContact : GetMailContactBase
	{
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x000269D6 File Offset: 0x00024BD6
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x000269DE File Offset: 0x00024BDE
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
	}
}
