using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000123 RID: 291
	public class ExObjectDisposedException : ObjectDisposedException
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x00021919 File Offset: 0x0001FB19
		public ExObjectDisposedException(string objectName) : base(objectName)
		{
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00021922 File Offset: 0x0001FB22
		public ExObjectDisposedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002192C File Offset: 0x0001FB2C
		public ExObjectDisposedException(string objectName, string message) : base(objectName, message)
		{
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00021936 File Offset: 0x0001FB36
		protected ExObjectDisposedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
