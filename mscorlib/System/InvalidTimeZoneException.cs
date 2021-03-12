using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System
{
	// Token: 0x02000102 RID: 258
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class InvalidTimeZoneException : Exception
	{
		// Token: 0x06000FBA RID: 4026 RVA: 0x00030281 File Offset: 0x0002E481
		[__DynamicallyInvokable]
		public InvalidTimeZoneException(string message) : base(message)
		{
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0003028A File Offset: 0x0002E48A
		[__DynamicallyInvokable]
		public InvalidTimeZoneException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00030294 File Offset: 0x0002E494
		protected InvalidTimeZoneException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0003029E File Offset: 0x0002E49E
		[__DynamicallyInvokable]
		public InvalidTimeZoneException()
		{
		}
	}
}
