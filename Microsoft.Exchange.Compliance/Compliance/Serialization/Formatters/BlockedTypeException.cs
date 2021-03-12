using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Compliance.Serialization.Formatters
{
	// Token: 0x02000002 RID: 2
	internal sealed class BlockedTypeException : SerializationException
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public BlockedTypeException(string type)
		{
			this.typeName = type;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020DF File Offset: 0x000002DF
		public override string Message
		{
			get
			{
				return "The type to be (de)serialized is not allowed: " + this.typeName;
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly string typeName;
	}
}
