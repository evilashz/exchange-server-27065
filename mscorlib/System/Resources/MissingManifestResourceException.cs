using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Resources
{
	// Token: 0x02000364 RID: 868
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MissingManifestResourceException : SystemException
	{
		// Token: 0x06002BE1 RID: 11233 RVA: 0x000A56CB File Offset: 0x000A38CB
		[__DynamicallyInvokable]
		public MissingManifestResourceException() : base(Environment.GetResourceString("Arg_MissingManifestResourceException"))
		{
			base.SetErrorCode(-2146233038);
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000A56E8 File Offset: 0x000A38E8
		[__DynamicallyInvokable]
		public MissingManifestResourceException(string message) : base(message)
		{
			base.SetErrorCode(-2146233038);
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000A56FC File Offset: 0x000A38FC
		[__DynamicallyInvokable]
		public MissingManifestResourceException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233038);
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000A5711 File Offset: 0x000A3911
		protected MissingManifestResourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
