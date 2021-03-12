using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000066 RID: 102
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ModifyTableOperation
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x000391BD File Offset: 0x000373BD
		public ModifyTableOperation(ModifyTableOperationType operationType, PropValue[] properties)
		{
			EnumValidator.ThrowIfInvalid<ModifyTableOperationType>(operationType, "operationType");
			this.operationType = operationType;
			this.properties = properties;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x000391DE File Offset: 0x000373DE
		public ModifyTableOperationType Operation
		{
			get
			{
				return this.operationType;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x000391E6 File Offset: 0x000373E6
		public PropValue[] Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04000201 RID: 513
		private readonly ModifyTableOperationType operationType;

		// Token: 0x04000202 RID: 514
		private readonly PropValue[] properties;
	}
}
