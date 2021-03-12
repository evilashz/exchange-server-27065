using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000A7 RID: 167
	internal abstract class GreetingBase : DisposableBase
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005AB RID: 1451
		internal abstract string Name { get; }

		// Token: 0x060005AC RID: 1452
		internal abstract ITempWavFile Get();

		// Token: 0x060005AD RID: 1453
		internal abstract void Put(string sourceFileName);

		// Token: 0x060005AE RID: 1454
		internal abstract void Delete();

		// Token: 0x060005AF RID: 1455
		internal abstract bool Exists();
	}
}
