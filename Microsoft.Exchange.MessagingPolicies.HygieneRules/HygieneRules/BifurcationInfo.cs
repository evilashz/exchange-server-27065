using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.HygieneRules
{
	// Token: 0x02000002 RID: 2
	internal sealed class BifurcationInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public List<string> Recipients
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public List<string> Lists
		{
			get
			{
				return this.lists;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E0 File Offset: 0x000002E0
		public List<string> RecipientDomainIs
		{
			get
			{
				return this.recipientDomainIs;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020F0 File Offset: 0x000002F0
		public bool Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020FC File Offset: 0x000002FC
		public int GetEstimatedSize()
		{
			int result = 18;
			this.AddStringListPropertySize(this.recipients, ref result);
			this.AddStringListPropertySize(this.lists, ref result);
			this.AddStringListPropertySize(this.recipientDomainIs, ref result);
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002138 File Offset: 0x00000338
		private void AddStringListPropertySize(List<string> property, ref int size)
		{
			if (property != null)
			{
				size += 18;
				foreach (string text in property)
				{
					size += text.Length * 2;
					size += 18;
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly List<string> recipients = new List<string>();

		// Token: 0x04000002 RID: 2
		private readonly List<string> lists = new List<string>();

		// Token: 0x04000003 RID: 3
		private readonly List<string> recipientDomainIs = new List<string>();

		// Token: 0x04000004 RID: 4
		private bool exception;
	}
}
