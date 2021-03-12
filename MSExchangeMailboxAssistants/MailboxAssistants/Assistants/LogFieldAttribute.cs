using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200001A RID: 26
	[AttributeUsage(AttributeTargets.Property)]
	internal class LogFieldAttribute : Attribute
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00005404 File Offset: 0x00003604
		public LogFieldAttribute()
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000540C File Offset: 0x0000360C
		public LogFieldAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000541B File Offset: 0x0000361B
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00005423 File Offset: 0x00003623
		public string Name { get; private set; }
	}
}
