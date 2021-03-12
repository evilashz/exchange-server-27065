using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008EA RID: 2282
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LoadItemEventArgs : EventArgs
	{
		// Token: 0x060055A4 RID: 21924 RVA: 0x00162A47 File Offset: 0x00160C47
		internal LoadItemEventArgs(IConversationTreeNode treeNode, IStorePropertyBag propertyBag)
		{
			this.treeNode = treeNode;
			this.propertyBag = propertyBag;
		}

		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x060055A5 RID: 21925 RVA: 0x00162A5D File Offset: 0x00160C5D
		public IConversationTreeNode TreeNode
		{
			get
			{
				return this.treeNode;
			}
		}

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x060055A6 RID: 21926 RVA: 0x00162A65 File Offset: 0x00160C65
		public IStorePropertyBag StorePropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x060055A7 RID: 21927 RVA: 0x00162A6D File Offset: 0x00160C6D
		// (set) Token: 0x060055A8 RID: 21928 RVA: 0x00162A75 File Offset: 0x00160C75
		public PropertyDefinition[] MessagePropertyDefinitions
		{
			get
			{
				return this.propertyDefinitions;
			}
			set
			{
				this.propertyDefinitions = value;
			}
		}

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x060055A9 RID: 21929 RVA: 0x00162A7E File Offset: 0x00160C7E
		// (set) Token: 0x060055AA RID: 21930 RVA: 0x00162A86 File Offset: 0x00160C86
		public HtmlStreamOptionCallback HtmlStreamOptionCallback
		{
			get
			{
				return this.htmlStreamOptionCallback;
			}
			set
			{
				this.htmlStreamOptionCallback = value;
			}
		}

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x060055AB RID: 21931 RVA: 0x00162A8F File Offset: 0x00160C8F
		// (set) Token: 0x060055AC RID: 21932 RVA: 0x00162A97 File Offset: 0x00160C97
		public PropertyDefinition[] OpportunisticLoadPropertyDefinitions
		{
			get
			{
				return this.opportunitisticPropertyDefinitions;
			}
			set
			{
				this.opportunitisticPropertyDefinitions = value;
			}
		}

		// Token: 0x04002DF6 RID: 11766
		private readonly IConversationTreeNode treeNode;

		// Token: 0x04002DF7 RID: 11767
		private readonly IStorePropertyBag propertyBag;

		// Token: 0x04002DF8 RID: 11768
		private PropertyDefinition[] propertyDefinitions;

		// Token: 0x04002DF9 RID: 11769
		private HtmlStreamOptionCallback htmlStreamOptionCallback;

		// Token: 0x04002DFA RID: 11770
		private PropertyDefinition[] opportunitisticPropertyDefinitions;
	}
}
