using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200009C RID: 156
	public interface IPerfEventData
	{
		// Token: 0x060003BA RID: 954
		byte[] ToBytes();

		// Token: 0x060003BB RID: 955
		void FromBytes(byte[] data);

		// Token: 0x060003BC RID: 956
		string[] ToCsvRecord();
	}
}
