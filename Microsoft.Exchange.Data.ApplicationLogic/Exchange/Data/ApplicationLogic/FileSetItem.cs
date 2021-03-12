using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200013C RID: 316
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FileSetItem
	{
		// Token: 0x06000CEC RID: 3308 RVA: 0x00035A2C File Offset: 0x00033C2C
		public FileSetItem(string name, StoreId id, ExDateTime time)
		{
			this.Name = name;
			this.Id = id;
			this.Time = time;
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00035A49 File Offset: 0x00033C49
		// (set) Token: 0x06000CEE RID: 3310 RVA: 0x00035A51 File Offset: 0x00033C51
		public string Name { get; private set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00035A5A File Offset: 0x00033C5A
		// (set) Token: 0x06000CF0 RID: 3312 RVA: 0x00035A62 File Offset: 0x00033C62
		public StoreId Id { get; private set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00035A6B File Offset: 0x00033C6B
		// (set) Token: 0x06000CF2 RID: 3314 RVA: 0x00035A73 File Offset: 0x00033C73
		public ExDateTime Time { get; private set; }

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00035A7C File Offset: 0x00033C7C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Name=",
				this.Name,
				", Id=",
				this.Id,
				", Time=",
				this.Time
			});
		}
	}
}
