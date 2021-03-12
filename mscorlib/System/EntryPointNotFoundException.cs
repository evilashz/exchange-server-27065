using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000DB RID: 219
	[ComVisible(true)]
	[Serializable]
	public class EntryPointNotFoundException : TypeLoadException
	{
		// Token: 0x06000E28 RID: 3624 RVA: 0x0002BBFF File Offset: 0x00029DFF
		public EntryPointNotFoundException() : base(Environment.GetResourceString("Arg_EntryPointNotFoundException"))
		{
			base.SetErrorCode(-2146233053);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0002BC1C File Offset: 0x00029E1C
		public EntryPointNotFoundException(string message) : base(message)
		{
			base.SetErrorCode(-2146233053);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0002BC30 File Offset: 0x00029E30
		public EntryPointNotFoundException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233053);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0002BC45 File Offset: 0x00029E45
		protected EntryPointNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
