using System;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	internal class PersistableClass : SchemaMappingEntry
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00005A7C File Offset: 0x00003C7C
		public PersistableClass()
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005A84 File Offset: 0x00003C84
		public PersistableClass(string sourceClassName)
		{
			this.sourceClassName = sourceClassName;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005A93 File Offset: 0x00003C93
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00005A9B File Offset: 0x00003C9B
		public string SourceClassName
		{
			get
			{
				return this.sourceClassName;
			}
			set
			{
				this.sourceClassName = value;
			}
		}

		// Token: 0x04000061 RID: 97
		private string sourceClassName;
	}
}
