using System;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200084F RID: 2127
	internal class RemotingSurrogate : ISerializationSurrogate
	{
		// Token: 0x06005B3A RID: 23354 RVA: 0x0013EDE8 File Offset: 0x0013CFE8
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
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				realProxy.GetObjectData(info, context);
				return;
			}
			RemotingServices.GetObjectData(obj, info, context);
		}

		// Token: 0x06005B3B RID: 23355 RVA: 0x0013EE31 File Offset: 0x0013D031
		[SecurityCritical]
		public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
		}
	}
}
