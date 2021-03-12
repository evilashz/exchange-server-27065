using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000430 RID: 1072
	internal sealed class InvisibleRootTreeNode : TreeNode
	{
		// Token: 0x060026D0 RID: 9936 RVA: 0x000DE054 File Offset: 0x000DC254
		internal InvisibleRootTreeNode(UserContext userContext) : base(userContext)
		{
			base.ChildIndent = 0;
			base.IsExpanded = true;
			this.id = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x000DE08E File Offset: 0x000DC28E
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060026D2 RID: 9938 RVA: 0x000DE096 File Offset: 0x000DC296
		protected override bool ContentVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B2B RID: 6955
		private readonly string id;
	}
}
