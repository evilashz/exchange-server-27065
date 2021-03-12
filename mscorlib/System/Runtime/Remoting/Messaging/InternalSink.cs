using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000854 RID: 2132
	[Serializable]
	internal class InternalSink
	{
		// Token: 0x06005B5A RID: 23386 RVA: 0x0013FB88 File Offset: 0x0013DD88
		[SecurityCritical]
		internal static IMessage ValidateMessage(IMessage reqMsg)
		{
			IMessage result = null;
			if (reqMsg == null)
			{
				result = new ReturnMessage(new ArgumentNullException("reqMsg"), null);
			}
			return result;
		}

		// Token: 0x06005B5B RID: 23387 RVA: 0x0013FBAC File Offset: 0x0013DDAC
		[SecurityCritical]
		internal static IMessage DisallowAsyncActivation(IMessage reqMsg)
		{
			if (reqMsg is IConstructionCallMessage)
			{
				return new ReturnMessage(new RemotingException(Environment.GetResourceString("Remoting_Activation_AsyncUnsupported")), null);
			}
			return null;
		}

		// Token: 0x06005B5C RID: 23388 RVA: 0x0013FBD0 File Offset: 0x0013DDD0
		[SecurityCritical]
		internal static Identity GetIdentity(IMessage reqMsg)
		{
			Identity identity = null;
			if (reqMsg is IInternalMessage)
			{
				identity = ((IInternalMessage)reqMsg).IdentityObject;
			}
			else if (reqMsg is InternalMessageWrapper)
			{
				identity = (Identity)((InternalMessageWrapper)reqMsg).GetIdentityObject();
			}
			if (identity == null)
			{
				string uri = InternalSink.GetURI(reqMsg);
				identity = IdentityHolder.ResolveIdentity(uri);
				if (identity == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Remoting_ServerObjectNotFound", new object[]
					{
						uri
					}));
				}
			}
			return identity;
		}

		// Token: 0x06005B5D RID: 23389 RVA: 0x0013FC40 File Offset: 0x0013DE40
		[SecurityCritical]
		internal static ServerIdentity GetServerIdentity(IMessage reqMsg)
		{
			ServerIdentity serverIdentity = null;
			bool flag = false;
			IInternalMessage internalMessage = reqMsg as IInternalMessage;
			if (internalMessage != null)
			{
				serverIdentity = ((IInternalMessage)reqMsg).ServerIdentityObject;
				flag = true;
			}
			else if (reqMsg is InternalMessageWrapper)
			{
				serverIdentity = (ServerIdentity)((InternalMessageWrapper)reqMsg).GetServerIdentityObject();
			}
			if (serverIdentity == null)
			{
				string uri = InternalSink.GetURI(reqMsg);
				Identity identity = IdentityHolder.ResolveIdentity(uri);
				if (identity is ServerIdentity)
				{
					serverIdentity = (ServerIdentity)identity;
					if (flag)
					{
						internalMessage.ServerIdentityObject = serverIdentity;
					}
				}
			}
			return serverIdentity;
		}

		// Token: 0x06005B5E RID: 23390 RVA: 0x0013FCB4 File Offset: 0x0013DEB4
		[SecurityCritical]
		internal static string GetURI(IMessage msg)
		{
			string result = null;
			IMethodMessage methodMessage = msg as IMethodMessage;
			if (methodMessage != null)
			{
				result = methodMessage.Uri;
			}
			else
			{
				IDictionary properties = msg.Properties;
				if (properties != null)
				{
					result = (string)properties["__Uri"];
				}
			}
			return result;
		}
	}
}
