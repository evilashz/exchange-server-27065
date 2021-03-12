using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000850 RID: 2128
	internal class ObjRefSurrogate : ISerializationSurrogate
	{
		// Token: 0x06005B3D RID: 23357 RVA: 0x0013EE4A File Offset: 0x0013D04A
		[SecurityCritical]
		public virtual void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			((ObjRef)obj).GetObjectData(info, context);
			info.AddValue("fIsMarshalled", 0);
		}

		// Token: 0x06005B3E RID: 23358 RVA: 0x0013EE81 File Offset: 0x0013D081
		[SecurityCritical]
		public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
		}
	}
}
