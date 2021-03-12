using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000036 RID: 54
	public class RuleTag
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000067FA File Offset: 0x000049FA
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00006802 File Offset: 0x00004A02
		public int Size { get; protected set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000680B File Offset: 0x00004A0B
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00006813 File Offset: 0x00004A13
		public string Name { get; protected set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000681C File Offset: 0x00004A1C
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00006824 File Offset: 0x00004A24
		public string TagType { get; protected set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000682D File Offset: 0x00004A2D
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00006835 File Offset: 0x00004A35
		public Dictionary<string, string> Data { get; protected set; }

		// Token: 0x0600017E RID: 382 RVA: 0x00006840 File Offset: 0x00004A40
		public RuleTag(string name, string tagType)
		{
			this.Name = name;
			this.TagType = tagType;
			this.Data = new Dictionary<string, string>();
			if (this.Name != null)
			{
				this.Size += this.Name.Length * 2;
				this.Size += 18;
			}
			if (this.TagType != null)
			{
				this.Size += this.TagType.Length * 2;
				this.Size += 18;
			}
			if (this.Data != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.Data)
				{
					if (keyValuePair.Key != null)
					{
						this.Size += keyValuePair.Key.Length * 2;
						this.Size += 18;
					}
					if (keyValuePair.Value != null)
					{
						this.Size += keyValuePair.Value.Length * 2;
						this.Size += 18;
					}
				}
			}
		}
	}
}
