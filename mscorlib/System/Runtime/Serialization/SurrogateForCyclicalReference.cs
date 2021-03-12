using System;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000703 RID: 1795
	internal sealed class SurrogateForCyclicalReference : ISerializationSurrogate
	{
		// Token: 0x06005083 RID: 20611 RVA: 0x0011B2A2 File Offset: 0x001194A2
		internal SurrogateForCyclicalReference(ISerializationSurrogate innerSurrogate)
		{
			if (innerSurrogate == null)
			{
				throw new ArgumentNullException("innerSurrogate");
			}
			this.innerSurrogate = innerSurrogate;
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x0011B2BF File Offset: 0x001194BF
		[SecurityCritical]
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			this.innerSurrogate.GetObjectData(obj, info, context);
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x0011B2CF File Offset: 0x001194CF
		[SecurityCritical]
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			return this.innerSurrogate.SetObjectData(obj, info, context, selector);
		}

		// Token: 0x04002384 RID: 9092
		private ISerializationSurrogate innerSurrogate;
	}
}
