using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D7 RID: 2519
	[Guid("4bd682dd-7554-40e9-9a9b-82654ede7e62")]
	[ComImport]
	internal interface IPropertyValue
	{
		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06006437 RID: 25655
		PropertyType Type { get; }

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x06006438 RID: 25656
		bool IsNumericScalar { get; }

		// Token: 0x06006439 RID: 25657
		byte GetUInt8();

		// Token: 0x0600643A RID: 25658
		short GetInt16();

		// Token: 0x0600643B RID: 25659
		ushort GetUInt16();

		// Token: 0x0600643C RID: 25660
		int GetInt32();

		// Token: 0x0600643D RID: 25661
		uint GetUInt32();

		// Token: 0x0600643E RID: 25662
		long GetInt64();

		// Token: 0x0600643F RID: 25663
		ulong GetUInt64();

		// Token: 0x06006440 RID: 25664
		float GetSingle();

		// Token: 0x06006441 RID: 25665
		double GetDouble();

		// Token: 0x06006442 RID: 25666
		char GetChar16();

		// Token: 0x06006443 RID: 25667
		bool GetBoolean();

		// Token: 0x06006444 RID: 25668
		string GetString();

		// Token: 0x06006445 RID: 25669
		Guid GetGuid();

		// Token: 0x06006446 RID: 25670
		DateTimeOffset GetDateTime();

		// Token: 0x06006447 RID: 25671
		TimeSpan GetTimeSpan();

		// Token: 0x06006448 RID: 25672
		Point GetPoint();

		// Token: 0x06006449 RID: 25673
		Size GetSize();

		// Token: 0x0600644A RID: 25674
		Rect GetRect();

		// Token: 0x0600644B RID: 25675
		byte[] GetUInt8Array();

		// Token: 0x0600644C RID: 25676
		short[] GetInt16Array();

		// Token: 0x0600644D RID: 25677
		ushort[] GetUInt16Array();

		// Token: 0x0600644E RID: 25678
		int[] GetInt32Array();

		// Token: 0x0600644F RID: 25679
		uint[] GetUInt32Array();

		// Token: 0x06006450 RID: 25680
		long[] GetInt64Array();

		// Token: 0x06006451 RID: 25681
		ulong[] GetUInt64Array();

		// Token: 0x06006452 RID: 25682
		float[] GetSingleArray();

		// Token: 0x06006453 RID: 25683
		double[] GetDoubleArray();

		// Token: 0x06006454 RID: 25684
		char[] GetChar16Array();

		// Token: 0x06006455 RID: 25685
		bool[] GetBooleanArray();

		// Token: 0x06006456 RID: 25686
		string[] GetStringArray();

		// Token: 0x06006457 RID: 25687
		object[] GetInspectableArray();

		// Token: 0x06006458 RID: 25688
		Guid[] GetGuidArray();

		// Token: 0x06006459 RID: 25689
		DateTimeOffset[] GetDateTimeArray();

		// Token: 0x0600645A RID: 25690
		TimeSpan[] GetTimeSpanArray();

		// Token: 0x0600645B RID: 25691
		Point[] GetPointArray();

		// Token: 0x0600645C RID: 25692
		Size[] GetSizeArray();

		// Token: 0x0600645D RID: 25693
		Rect[] GetRectArray();
	}
}
