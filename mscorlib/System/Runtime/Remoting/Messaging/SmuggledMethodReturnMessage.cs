using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200084B RID: 2123
	internal class SmuggledMethodReturnMessage : MessageSmuggler
	{
		// Token: 0x06005B21 RID: 23329 RVA: 0x0013EA20 File Offset: 0x0013CC20
		[SecurityCritical]
		internal static SmuggledMethodReturnMessage SmuggleIfPossible(IMessage msg)
		{
			IMethodReturnMessage methodReturnMessage = msg as IMethodReturnMessage;
			if (methodReturnMessage == null)
			{
				return null;
			}
			return new SmuggledMethodReturnMessage(methodReturnMessage);
		}

		// Token: 0x06005B22 RID: 23330 RVA: 0x0013EA3F File Offset: 0x0013CC3F
		private SmuggledMethodReturnMessage()
		{
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x0013EA48 File Offset: 0x0013CC48
		[SecurityCritical]
		private SmuggledMethodReturnMessage(IMethodReturnMessage mrm)
		{
			ArrayList arrayList = null;
			ReturnMessage returnMessage = mrm as ReturnMessage;
			if (returnMessage == null || returnMessage.HasProperties())
			{
				this._propertyCount = MessageSmuggler.StoreUserPropertiesForMethodMessage(mrm, ref arrayList);
			}
			Exception exception = mrm.Exception;
			if (exception != null)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._exception = new MessageSmuggler.SerializedArg(arrayList.Count);
				arrayList.Add(exception);
			}
			LogicalCallContext logicalCallContext = mrm.LogicalCallContext;
			if (logicalCallContext == null)
			{
				this._callContext = null;
			}
			else if (logicalCallContext.HasInfo)
			{
				if (logicalCallContext.Principal != null)
				{
					logicalCallContext.Principal = null;
				}
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
			this._returnValue = MessageSmuggler.FixupArg(mrm.ReturnValue, ref arrayList);
			this._args = MessageSmuggler.FixupArgs(mrm.Args, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CrossAppDomainSerializer.SerializeMessageParts(arrayList);
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06005B24 RID: 23332 RVA: 0x0013EB48 File Offset: 0x0013CD48
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

		// Token: 0x06005B25 RID: 23333 RVA: 0x0013EB78 File Offset: 0x0013CD78
		[SecurityCritical]
		internal object GetReturnValue(ArrayList deserializedArgs)
		{
			return MessageSmuggler.UndoFixupArg(this._returnValue, deserializedArgs);
		}

		// Token: 0x06005B26 RID: 23334 RVA: 0x0013EB88 File Offset: 0x0013CD88
		[SecurityCritical]
		internal object[] GetArgs(ArrayList deserializedArgs)
		{
			return MessageSmuggler.UndoFixupArgs(this._args, deserializedArgs);
		}

		// Token: 0x06005B27 RID: 23335 RVA: 0x0013EBA3 File Offset: 0x0013CDA3
		internal Exception GetException(ArrayList deserializedArgs)
		{
			if (this._exception != null)
			{
				return (Exception)deserializedArgs[this._exception.Index];
			}
			return null;
		}

		// Token: 0x06005B28 RID: 23336 RVA: 0x0013EBC8 File Offset: 0x0013CDC8
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

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06005B29 RID: 23337 RVA: 0x0013EC25 File Offset: 0x0013CE25
		internal int MessagePropertyCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x06005B2A RID: 23338 RVA: 0x0013EC30 File Offset: 0x0013CE30
		internal void PopulateMessageProperties(IDictionary dict, ArrayList deserializedArgs)
		{
			for (int i = 0; i < this._propertyCount; i++)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)deserializedArgs[i];
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x040028ED RID: 10477
		private object[] _args;

		// Token: 0x040028EE RID: 10478
		private object _returnValue;

		// Token: 0x040028EF RID: 10479
		private byte[] _serializedArgs;

		// Token: 0x040028F0 RID: 10480
		private MessageSmuggler.SerializedArg _exception;

		// Token: 0x040028F1 RID: 10481
		private object _callContext;

		// Token: 0x040028F2 RID: 10482
		private int _propertyCount;
	}
}
