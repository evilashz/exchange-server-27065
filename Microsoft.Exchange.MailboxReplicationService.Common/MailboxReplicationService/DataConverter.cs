using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200022A RID: 554
	internal class DataConverter<T, TNative, TData> where T : IDataConverter<TNative, TData>, new()
	{
		// Token: 0x06001DD6 RID: 7638 RVA: 0x0003D874 File Offset: 0x0003BA74
		public static TData GetData(TNative native)
		{
			if (native == null)
			{
				return default(TData);
			}
			return DataConverter<T, TNative, TData>.instance.GetDataRepresentation(native);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0003D8A4 File Offset: 0x0003BAA4
		public static TNative GetNative(TData data)
		{
			if (data == null)
			{
				return default(TNative);
			}
			return DataConverter<T, TNative, TData>.instance.GetNativeRepresentation(data);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x0003D8D4 File Offset: 0x0003BAD4
		public static TData[] GetData(TNative[] a)
		{
			if (a == null)
			{
				return null;
			}
			TData[] array = new TData[a.Length];
			for (int i = 0; i < a.Length; i++)
			{
				array[i] = DataConverter<T, TNative, TData>.instance.GetDataRepresentation(a[i]);
			}
			return array;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x0003D91C File Offset: 0x0003BB1C
		public static TNative[] GetNative(TData[] a)
		{
			if (a == null)
			{
				return null;
			}
			TNative[] array = new TNative[a.Length];
			for (int i = 0; i < a.Length; i++)
			{
				array[i] = DataConverter<T, TNative, TData>.instance.GetNativeRepresentation(a[i]);
			}
			return array;
		}

		// Token: 0x04000C59 RID: 3161
		private static T instance = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
	}
}
