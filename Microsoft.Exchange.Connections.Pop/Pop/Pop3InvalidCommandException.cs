using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class Pop3InvalidCommandException : Exception
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x000056BC File Offset: 0x000038BC
		public Pop3InvalidCommandException()
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000056C4 File Offset: 0x000038C4
		public Pop3InvalidCommandException(string message) : base(message)
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000056CD File Offset: 0x000038CD
		public Pop3InvalidCommandException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000056D7 File Offset: 0x000038D7
		internal Pop3InvalidCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
