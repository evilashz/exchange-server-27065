using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200084A RID: 2122
	internal class SmuggledMethodCallMessage : MessageSmuggler
	{
		// Token: 0x06005B14 RID: 23316 RVA: 0x0013E76C File Offset: 0x0013C96C
		[SecurityCritical]
		internal static SmuggledMethodCallMessage SmuggleIfPossible(IMessage msg)
		{
			IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
			if (methodCallMessage == null)
			{
				return null;
			}
			return new SmuggledMethodCallMessage(methodCallMessage);
		}

		// Token: 0x06005B15 RID: 23317 RVA: 0x0013E78B File Offset: 0x0013C98B
		private SmuggledMethodCallMessage()
		{
		}

		// Token: 0x06005B16 RID: 23318 RVA: 0x0013E794 File Offset: 0x0013C994
		[SecurityCritical]
		private SmuggledMethodCallMessage(IMethodCallMessage mcm)
		{
			this._uri = mcm.Uri;
			this._methodName = mcm.MethodName;
			this._typeName = mcm.TypeName;
			ArrayList arrayList = null;
			IInternalMessage internalMessage = mcm as IInternalMessage;
			if (internalMessage == null || internalMessage.HasProperties())
			{
				this._propertyCount = MessageSmuggler.StoreUserPropertiesForMethodMessage(mcm, ref arrayList);
			}
			if (mcm.MethodBase.IsGenericMethod)
			{
				Type[] genericArguments = mcm.MethodBase.GetGenericArguments();
				if (genericArguments != null && genericArguments.Length != 0)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					this._instantiation = new MessageSmuggler.SerializedArg(arrayList.Count);
					arrayList.Add(genericArguments);
				}
			}
			if (RemotingServices.IsMethodOverloaded(mcm))
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._methodSignature = new MessageSmuggler.SerializedArg(arrayList.Count);
				arrayList.Add(mcm.MethodSignature);
			}
			LogicalCallContext logicalCallContext = mcm.LogicalCallContext;
			if (logicalCallContext == null)
			{
				this._callContext = null;
			}
			else if (logicalCallContext.HasInfo)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._callContext = new MessageSmuggler.SerializedArg(arrayList.Count);
				arrayList.Add(logicalCallContext);
			}
			else
			{
				this._callContext = logicalCallContext.RemotingData.LogicalCallID;
			}
			this._args = MessageSmuggler.FixupArgs(mcm.Args, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CrossAppDomainSerializer.SerializeMessageParts(arrayList);
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06005B17 RID: 23319 RVA: 0x0013E8DC File Offset: 0x0013CADC
		[SecurityCritical]
		internal ArrayList FixupForNewAppDomain()
		{
			ArrayList result = null;
			if (this._serializedArgs != null)
			{
				result = CrossAppDomainSerializer.DeserializeMessageParts(new MemoryStream(this._serializedArgs));
				this._serializedArgs = null;
			}
			return result;
		}

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06005B18 RID: 23320 RVA: 0x0013E90C File Offset: 0x0013CB0C
		internal string Uri
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06005B19 RID: 23321 RVA: 0x0013E914 File Offset: 0x0013CB14
		internal string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06005B1A RID: 23322 RVA: 0x0013E91C File Offset: 0x0013CB1C
		internal string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x06005B1B RID: 23323 RVA: 0x0013E924 File Offset: 0x0013CB24
		internal Type[] GetInstantiation(ArrayList deserializedArgs)
		{
			if (this._instantiation != null)
			{
				return (Type[])deserializedArgs[this._instantiation.Index];
			}
			return null;
		}

		// Token: 0x06005B1C RID: 23324 RVA: 0x0013E946 File Offset: 0x0013CB46
		internal object[] GetMethodSignature(ArrayList deserializedArgs)
		{
			if (this._methodSignature != null)
			{
				return (object[])deserializedArgs[this._methodSignature.Index];
			}
			return null;
		}

		// Token: 0x06005B1D RID: 23325 RVA: 0x0013E968 File Offset: 0x0013CB68
		[SecurityCritical]
		internal object[] GetArgs(ArrayList deserializedArgs)
		{
			return MessageSmuggler.UndoFixupArgs(this._args, deserializedArgs);
		}

		// Token: 0x06005B1E RID: 23326 RVA: 0x0013E978 File Offset: 0x0013CB78
		[SecurityCritical]
		internal LogicalCallContext GetCallContext(ArrayList deserializedArgs)
		{
			if (this._callContext == null)
			{
				return null;
			}
			if (this._callContext is string)
			{
				return new LogicalCallContext
				{
					RemotingData = 
					{
						LogicalCallID = (string)this._callContext
					}
				};
			}
			return (LogicalCallContext)deserializedArgs[((MessageSmuggler.SerializedArg)this._callContext).Index];
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06005B1F RID: 23327 RVA: 0x0013E9D5 File Offset: 0x0013CBD5
		internal int MessagePropertyCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x0013E9E0 File Offset: 0x0013CBE0
		internal void PopulateMessageProperties(IDictionary dict, ArrayList deserializedArgs)
		{
			for (int i = 0; i < this._propertyCount; i++)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)deserializedArgs[i];
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x040028E4 RID: 10468
		private string _uri;

		// Token: 0x040028E5 RID: 10469
		private string _methodName;

		// Token: 0x040028E6 RID: 10470
		private string _typeName;

		// Token: 0x040028E7 RID: 10471
		private object[] _args;

		// Token: 0x040028E8 RID: 10472
		private byte[] _serializedArgs;

		// Token: 0x040028E9 RID: 10473
		private MessageSmuggler.SerializedArg _methodSignature;

		// Token: 0x040028EA RID: 10474
		private MessageSmuggler.SerializedArg _instantiation;

		// Token: 0x040028EB RID: 10475
		private object _callContext;

		// Token: 0x040028EC RID: 10476
		private int _propertyCount;
	}
}
