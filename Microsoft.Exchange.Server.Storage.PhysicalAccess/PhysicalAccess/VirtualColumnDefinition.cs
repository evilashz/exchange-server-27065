using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000096 RID: 150
	public class VirtualColumnDefinition
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x0001D3AF File Offset: 0x0001B5AF
		public VirtualColumnDefinition(VirtualColumnId id, Type type, int size, string name, string description)
		{
			this.id = id;
			this.type = type;
			this.size = size;
			this.name = name;
			this.description = description;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001D3DC File Offset: 0x0001B5DC
		public VirtualColumnId Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0001D3E4 File Offset: 0x0001B5E4
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001D3EC File Offset: 0x0001B5EC
		public int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001D3F4 File Offset: 0x0001B5F4
		[Queryable]
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001D3FC File Offset: 0x0001B5FC
		[Queryable]
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x04000256 RID: 598
		private readonly VirtualColumnId id;

		// Token: 0x04000257 RID: 599
		private readonly Type type;

		// Token: 0x04000258 RID: 600
		private readonly int size;

		// Token: 0x04000259 RID: 601
		private readonly string name;

		// Token: 0x0400025A RID: 602
		private readonly string description;
	}
}
