using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005DD RID: 1501
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class Missing : ISerializable
	{
		// Token: 0x06004668 RID: 18024 RVA: 0x0010004F File Offset: 0x000FE24F
		private Missing()
		{
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x00100057 File Offset: 0x000FE257
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, this);
		}

		// Token: 0x04001CF2 RID: 7410
		[__DynamicallyInvokable]
		public static readonly Missing Value = new Missing();
	}
}
