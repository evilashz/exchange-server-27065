using System;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020000D9 RID: 217
	[Serializable]
	internal sealed class Empty : ISerializable
	{
		// Token: 0x06000DE6 RID: 3558 RVA: 0x0002A8E9 File Offset: 0x00028AE9
		private Empty()
		{
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0002A8F1 File Offset: 0x00028AF1
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0002A8F8 File Offset: 0x00028AF8
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 1, null, null);
		}

		// Token: 0x04000568 RID: 1384
		public static readonly Empty Value = new Empty();
	}
}
