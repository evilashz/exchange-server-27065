using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000852 RID: 2130
	internal class MessageSurrogate : ISerializationSurrogate
	{
		// Token: 0x06005B48 RID: 23368 RVA: 0x0013F333 File Offset: 0x0013D533
		[SecurityCritical]
		internal MessageSurrogate(RemotingSurrogateSelector ss)
		{
			this._ss = ss;
		}

		// Token: 0x06005B49 RID: 23369 RVA: 0x0013F344 File Offset: 0x0013D544
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
			bool flag = false;
			bool flag2 = false;
			IMethodMessage methodMessage = obj as IMethodMessage;
			if (methodMessage != null)
			{
				IDictionaryEnumerator enumerator = methodMessage.Properties.GetEnumerator();
				if (methodMessage is IMethodCallMessage)
				{
					if (obj is IConstructionCallMessage)
					{
						flag2 = true;
					}
					info.SetType(flag2 ? MessageSurrogate._constructionCallType : MessageSurrogate._methodCallType);
				}
				else
				{
					IMethodReturnMessage methodReturnMessage = methodMessage as IMethodReturnMessage;
					if (methodReturnMessage == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_InvalidMsg"));
					}
					flag = true;
					info.SetType((obj is IConstructionReturnMessage) ? MessageSurrogate._constructionResponseType : MessageSurrogate._methodResponseType);
					if (((IMethodReturnMessage)methodMessage).Exception != null)
					{
						info.AddValue("__fault", ((IMethodReturnMessage)methodMessage).Exception, MessageSurrogate._exceptionType);
					}
				}
				while (enumerator.MoveNext())
				{
					if (obj != this._ss.GetRootObject() || this._ss.Filter == null || !this._ss.Filter((string)enumerator.Key, enumerator.Value))
					{
						if (enumerator.Value != null)
						{
							string text = enumerator.Key.ToString();
							if (text.Equals("__CallContext"))
							{
								LogicalCallContext logicalCallContext = (LogicalCallContext)enumerator.Value;
								if (logicalCallContext.HasInfo)
								{
									info.AddValue(text, logicalCallContext);
								}
								else
								{
									info.AddValue(text, logicalCallContext.RemotingData.LogicalCallID);
								}
							}
							else if (text.Equals("__MethodSignature"))
							{
								if (flag2 || RemotingServices.IsMethodOverloaded(methodMessage))
								{
									info.AddValue(text, enumerator.Value);
								}
							}
							else
							{
								flag = flag;
								info.AddValue(text, enumerator.Value);
							}
						}
						else
						{
							info.AddValue(enumerator.Key.ToString(), enumerator.Value, MessageSurrogate._objectType);
						}
					}
				}
				return;
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_InvalidMsg"));
		}

		// Token: 0x06005B4A RID: 23370 RVA: 0x0013F52F File Offset: 0x0013D72F
		[SecurityCritical]
		public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
		}

		// Token: 0x04002901 RID: 10497
		private static Type _constructionCallType = typeof(ConstructionCall);

		// Token: 0x04002902 RID: 10498
		private static Type _methodCallType = typeof(MethodCall);

		// Token: 0x04002903 RID: 10499
		private static Type _constructionResponseType = typeof(ConstructionResponse);

		// Token: 0x04002904 RID: 10500
		private static Type _methodResponseType = typeof(MethodResponse);

		// Token: 0x04002905 RID: 10501
		private static Type _exceptionType = typeof(Exception);

		// Token: 0x04002906 RID: 10502
		private static Type _objectType = typeof(object);

		// Token: 0x04002907 RID: 10503
		[SecurityCritical]
		private RemotingSurrogateSelector _ss;
	}
}
