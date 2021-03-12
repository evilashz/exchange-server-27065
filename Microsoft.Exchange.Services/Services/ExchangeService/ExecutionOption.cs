using System;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DDF RID: 3551
	internal class ExecutionOption
	{
		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x0011A86B File Offset: 0x00118A6B
		public static ExecutionOption Default
		{
			get
			{
				return ExecutionOption.defaultOption;
			}
		}

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x06005B47 RID: 23367 RVA: 0x0011A872 File Offset: 0x00118A72
		// (set) Token: 0x06005B48 RID: 23368 RVA: 0x0011A87A File Offset: 0x00118A7A
		public bool WrapExecutionExceptions { get; set; }

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x06005B49 RID: 23369 RVA: 0x0011A883 File Offset: 0x00118A83
		// (set) Token: 0x06005B4A RID: 23370 RVA: 0x0011A88B File Offset: 0x00118A8B
		public ResponseValidationBehavior ResponseValidationBehavior { get; set; }

		// Token: 0x04003210 RID: 12816
		private static readonly ExecutionOption defaultOption = new ExecutionOption
		{
			WrapExecutionExceptions = true,
			ResponseValidationBehavior = ResponseValidationBehavior.ThrowOnAnyResponseError
		};
	}
}
