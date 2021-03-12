using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200042F RID: 1071
	internal class SimpleTree : Tree
	{
		// Token: 0x060026CD RID: 9933 RVA: 0x000DE01E File Offset: 0x000DC21E
		internal SimpleTree(UserContext userContext, TreeNode rootNode, string id) : base(userContext, rootNode)
		{
			this.id = id;
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000DE02F File Offset: 0x000DC22F
		internal SimpleTree(UserContext userContext, TreeNode rootNode) : this(userContext, rootNode, null)
		{
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x000DE03A File Offset: 0x000DC23A
		internal override string Id
		{
			get
			{
				if (this.id == null)
				{
					return base.Id;
				}
				return this.id;
			}
		}

		// Token: 0x04001B2A RID: 6954
		private string id;
	}
}
