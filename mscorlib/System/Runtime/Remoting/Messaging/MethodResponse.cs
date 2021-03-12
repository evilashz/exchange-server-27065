using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200083E RID: 2110
	[SecurityCritical]
	[CLSCompliant(false)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class MethodResponse : IMethodReturnMessage, IMethodMessage, IMessage, ISerializable, ISerializationRootObject, IInternalMessage
	{
		// Token: 0x06005A81 RID: 23169 RVA: 0x0013C9DC File Offset: 0x0013ABDC
		[SecurityCritical]
		public MethodResponse(Header[] h1, IMethodCallMessage mcm)
		{
			if (mcm == null)
			{
				throw new ArgumentNullException("mcm");
			}
			Message message = mcm as Message;
			if (message != null)
			{
				this.MI = message.GetMethodBase();
			}
			else
			{
				this.MI = mcm.MethodBase;
			}
			if (this.MI == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), mcm.MethodName, mcm.TypeName));
			}
			this._methodCache = InternalRemotingServices.GetReflectionCachedData(this.MI);
			this.argCount = this._methodCache.Parameters.Length;
			this.fSoap = true;
			this.FillHeaders(h1);
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x0013CA88 File Offset: 0x0013AC88
		[SecurityCritical]
		internal MethodResponse(IMethodCallMessage msg, SmuggledMethodReturnMessage smuggledMrm, ArrayList deserializedArgs)
		{
			this.MI = msg.MethodBase;
			this._methodCache = InternalRemotingServices.GetReflectionCachedData(this.MI);
			this.methodName = msg.MethodName;
			this.uri = msg.Uri;
			this.typeName = msg.TypeName;
			if (this._methodCache.IsOverloaded())
			{
				this.methodSignature = (Type[])msg.MethodSignature;
			}
			this.retVal = smuggledMrm.GetReturnValue(deserializedArgs);
			this.outArgs = smuggledMrm.GetArgs(deserializedArgs);
			this.fault = smuggledMrm.GetException(deserializedArgs);
			this.callContext = smuggledMrm.GetCallContext(deserializedArgs);
			if (smuggledMrm.MessagePropertyCount > 0)
			{
				smuggledMrm.PopulateMessageProperties(this.Properties, deserializedArgs);
			}
			this.argCount = this._methodCache.Parameters.Length;
			this.fSoap = false;
		}

		// Token: 0x06005A83 RID: 23171 RVA: 0x0013CB60 File Offset: 0x0013AD60
		[SecurityCritical]
		internal MethodResponse(IMethodCallMessage msg, object handlerObject, BinaryMethodReturnMessage smuggledMrm)
		{
			if (msg != null)
			{
				this.MI = msg.MethodBase;
				this._methodCache = InternalRemotingServices.GetReflectionCachedData(this.MI);
				this.methodName = msg.MethodName;
				this.uri = msg.Uri;
				this.typeName = msg.TypeName;
				if (this._methodCache.IsOverloaded())
				{
					this.methodSignature = (Type[])msg.MethodSignature;
				}
				this.argCount = this._methodCache.Parameters.Length;
			}
			this.retVal = smuggledMrm.ReturnValue;
			this.outArgs = smuggledMrm.Args;
			this.fault = smuggledMrm.Exception;
			this.callContext = smuggledMrm.LogicalCallContext;
			if (smuggledMrm.HasProperties)
			{
				smuggledMrm.PopulateMessageProperties(this.Properties);
			}
			this.fSoap = false;
		}

		// Token: 0x06005A84 RID: 23172 RVA: 0x0013CC33 File Offset: 0x0013AE33
		[SecurityCritical]
		internal MethodResponse(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.SetObjectData(info, context);
		}

		// Token: 0x06005A85 RID: 23173 RVA: 0x0013CC54 File Offset: 0x0013AE54
		[SecurityCritical]
		public virtual object HeaderHandler(Header[] h)
		{
			SerializationMonkey serializationMonkey = (SerializationMonkey)FormatterServices.GetUninitializedObject(typeof(SerializationMonkey));
			Header[] array;
			if (h != null && h.Length != 0 && h[0].Name == "__methodName")
			{
				if (h.Length > 1)
				{
					array = new Header[h.Length - 1];
					Array.Copy(h, 1, array, 0, h.Length - 1);
				}
				else
				{
					array = null;
				}
			}
			else
			{
				array = h;
			}
			Type type = null;
			MethodInfo methodInfo = this.MI as MethodInfo;
			if (methodInfo != null)
			{
				type = methodInfo.ReturnType;
			}
			ParameterInfo[] parameters = this._methodCache.Parameters;
			int num = this._methodCache.MarshalResponseArgMap.Length;
			if (!(type == null) && !(type == typeof(void)))
			{
				num++;
			}
			Type[] array2 = new Type[num];
			string[] array3 = new string[num];
			int num2 = 0;
			if (!(type == null) && !(type == typeof(void)))
			{
				array2[num2++] = type;
			}
			foreach (int num3 in this._methodCache.MarshalResponseArgMap)
			{
				array3[num2] = parameters[num3].Name;
				if (parameters[num3].ParameterType.IsByRef)
				{
					array2[num2++] = parameters[num3].ParameterType.GetElementType();
				}
				else
				{
					array2[num2++] = parameters[num3].ParameterType;
				}
			}
			((IFieldInfo)serializationMonkey).FieldTypes = array2;
			((IFieldInfo)serializationMonkey).FieldNames = array3;
			this.FillHeaders(array, true);
			serializationMonkey._obj = this;
			return serializationMonkey;
		}

		// Token: 0x06005A86 RID: 23174 RVA: 0x0013CDE6 File Offset: 0x0013AFE6
		[SecurityCritical]
		public void RootSetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			this.SetObjectData(info, ctx);
		}

		// Token: 0x06005A87 RID: 23175 RVA: 0x0013CDF0 File Offset: 0x0013AFF0
		[SecurityCritical]
		internal void SetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.fSoap)
			{
				this.SetObjectFromSoapData(info);
				return;
			}
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			bool flag = false;
			bool flag2 = false;
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("__return"))
				{
					flag = true;
					break;
				}
				if (enumerator.Name.Equals("__fault"))
				{
					flag2 = true;
					this.fault = (Exception)enumerator.Value;
					break;
				}
				this.FillHeader(enumerator.Name, enumerator.Value);
			}
			if (flag2 && flag)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
			}
		}

		// Token: 0x06005A88 RID: 23176 RVA: 0x0013CE94 File Offset: 0x0013B094
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x06005A89 RID: 23177 RVA: 0x0013CEA8 File Offset: 0x0013B0A8
		internal void SetObjectFromSoapData(SerializationInfo info)
		{
			Hashtable keyToNamespaceTable = (Hashtable)info.GetValue("__keyToNamespaceTable", typeof(Hashtable));
			ArrayList arrayList = (ArrayList)info.GetValue("__paramNameList", typeof(ArrayList));
			SoapFault soapFault = (SoapFault)info.GetValue("__fault", typeof(SoapFault));
			if (soapFault != null)
			{
				ServerFault serverFault = soapFault.Detail as ServerFault;
				if (serverFault != null)
				{
					if (serverFault.Exception != null)
					{
						this.fault = serverFault.Exception;
						return;
					}
					Type type = Type.GetType(serverFault.ExceptionType, false, false);
					if (type == null)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append("\nException Type: ");
						stringBuilder.Append(serverFault.ExceptionType);
						stringBuilder.Append("\n");
						stringBuilder.Append("Exception Message: ");
						stringBuilder.Append(serverFault.ExceptionMessage);
						stringBuilder.Append("\n");
						stringBuilder.Append(serverFault.StackTrace);
						this.fault = new ServerException(stringBuilder.ToString());
						return;
					}
					object[] args = new object[]
					{
						serverFault.ExceptionMessage
					};
					this.fault = (Exception)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, args, null, null);
					return;
				}
				else
				{
					if (soapFault.Detail != null && soapFault.Detail.GetType() == typeof(string) && ((string)soapFault.Detail).Length != 0)
					{
						this.fault = new ServerException((string)soapFault.Detail);
						return;
					}
					this.fault = new ServerException(soapFault.FaultString);
					return;
				}
			}
			else
			{
				MethodInfo methodInfo = this.MI as MethodInfo;
				int num = 0;
				if (methodInfo != null)
				{
					Type returnType = methodInfo.ReturnType;
					if (returnType != typeof(void))
					{
						num++;
						object value = info.GetValue((string)arrayList[0], typeof(object));
						if (value is string)
						{
							this.retVal = Message.SoapCoerceArg(value, returnType, keyToNamespaceTable);
						}
						else
						{
							this.retVal = value;
						}
					}
				}
				ParameterInfo[] parameters = this._methodCache.Parameters;
				object obj = (this.InternalProperties == null) ? null : this.InternalProperties["__UnorderedParams"];
				if (obj != null && obj is bool && (bool)obj)
				{
					for (int i = num; i < arrayList.Count; i++)
					{
						string text = (string)arrayList[i];
						int num2 = -1;
						for (int j = 0; j < parameters.Length; j++)
						{
							if (text.Equals(parameters[j].Name))
							{
								num2 = parameters[j].Position;
							}
						}
						if (num2 == -1)
						{
							if (!text.StartsWith("__param", StringComparison.Ordinal))
							{
								throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
							}
							num2 = int.Parse(text.Substring(7), CultureInfo.InvariantCulture);
						}
						if (num2 >= this.argCount)
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
						}
						if (this.outArgs == null)
						{
							this.outArgs = new object[this.argCount];
						}
						this.outArgs[num2] = Message.SoapCoerceArg(info.GetValue(text, typeof(object)), parameters[num2].ParameterType, keyToNamespaceTable);
					}
					return;
				}
				if (this.argMapper == null)
				{
					this.argMapper = new ArgMapper(this, true);
				}
				for (int k = num; k < arrayList.Count; k++)
				{
					string name = (string)arrayList[k];
					if (this.outArgs == null)
					{
						this.outArgs = new object[this.argCount];
					}
					int num3 = this.argMapper.Map[k - num];
					this.outArgs[num3] = Message.SoapCoerceArg(info.GetValue(name, typeof(object)), parameters[num3].ParameterType, keyToNamespaceTable);
				}
				return;
			}
		}

		// Token: 0x06005A8A RID: 23178 RVA: 0x0013D2A5 File Offset: 0x0013B4A5
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			if (this.callContext == null)
			{
				this.callContext = new LogicalCallContext();
			}
			return this.callContext;
		}

		// Token: 0x06005A8B RID: 23179 RVA: 0x0013D2C0 File Offset: 0x0013B4C0
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
		{
			LogicalCallContext result = this.callContext;
			this.callContext = ctx;
			return result;
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06005A8C RID: 23180 RVA: 0x0013D2DC File Offset: 0x0013B4DC
		// (set) Token: 0x06005A8D RID: 23181 RVA: 0x0013D2E4 File Offset: 0x0013B4E4
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06005A8E RID: 23182 RVA: 0x0013D2ED File Offset: 0x0013B4ED
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				return this.methodName;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06005A8F RID: 23183 RVA: 0x0013D2F5 File Offset: 0x0013B4F5
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06005A90 RID: 23184 RVA: 0x0013D2FD File Offset: 0x0013B4FD
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return this.methodSignature;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06005A91 RID: 23185 RVA: 0x0013D305 File Offset: 0x0013B505
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this.MI;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06005A92 RID: 23186 RVA: 0x0013D30D File Offset: 0x0013B50D
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return false;
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06005A93 RID: 23187 RVA: 0x0013D310 File Offset: 0x0013B510
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this.outArgs == null)
				{
					return 0;
				}
				return this.outArgs.Length;
			}
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x0013D324 File Offset: 0x0013B524
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			return this.outArgs[argNum];
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x0013D330 File Offset: 0x0013B530
		[SecurityCritical]
		public string GetArgName(int index)
		{
			if (!(this.MI != null))
			{
				return "__param" + index;
			}
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.MI);
			ParameterInfo[] parameters = reflectionCachedData.Parameters;
			if (index < 0 || index >= parameters.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return reflectionCachedData.Parameters[index].Name;
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06005A96 RID: 23190 RVA: 0x0013D391 File Offset: 0x0013B591
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return this.outArgs;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06005A97 RID: 23191 RVA: 0x0013D399 File Offset: 0x0013B599
		public int OutArgCount
		{
			[SecurityCritical]
			get
			{
				if (this.argMapper == null)
				{
					this.argMapper = new ArgMapper(this, true);
				}
				return this.argMapper.ArgCount;
			}
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x0013D3BB File Offset: 0x0013B5BB
		[SecurityCritical]
		public object GetOutArg(int argNum)
		{
			if (this.argMapper == null)
			{
				this.argMapper = new ArgMapper(this, true);
			}
			return this.argMapper.GetArg(argNum);
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x0013D3DE File Offset: 0x0013B5DE
		[SecurityCritical]
		public string GetOutArgName(int index)
		{
			if (this.argMapper == null)
			{
				this.argMapper = new ArgMapper(this, true);
			}
			return this.argMapper.GetArgName(index);
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06005A9A RID: 23194 RVA: 0x0013D401 File Offset: 0x0013B601
		public object[] OutArgs
		{
			[SecurityCritical]
			get
			{
				if (this.argMapper == null)
				{
					this.argMapper = new ArgMapper(this, true);
				}
				return this.argMapper.Args;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06005A9B RID: 23195 RVA: 0x0013D423 File Offset: 0x0013B623
		public Exception Exception
		{
			[SecurityCritical]
			get
			{
				return this.fault;
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06005A9C RID: 23196 RVA: 0x0013D42B File Offset: 0x0013B62B
		public object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this.retVal;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06005A9D RID: 23197 RVA: 0x0013D434 File Offset: 0x0013B634
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				IDictionary externalProperties;
				lock (this)
				{
					if (this.InternalProperties == null)
					{
						this.InternalProperties = new Hashtable();
					}
					if (this.ExternalProperties == null)
					{
						this.ExternalProperties = new MRMDictionary(this, this.InternalProperties);
					}
					externalProperties = this.ExternalProperties;
				}
				return externalProperties;
			}
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06005A9E RID: 23198 RVA: 0x0013D4A0 File Offset: 0x0013B6A0
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this.GetLogicalCallContext();
			}
		}

		// Token: 0x06005A9F RID: 23199 RVA: 0x0013D4A8 File Offset: 0x0013B6A8
		[SecurityCritical]
		internal void FillHeaders(Header[] h)
		{
			this.FillHeaders(h, false);
		}

		// Token: 0x06005AA0 RID: 23200 RVA: 0x0013D4B4 File Offset: 0x0013B6B4
		[SecurityCritical]
		private void FillHeaders(Header[] h, bool bFromHeaderHandler)
		{
			if (h == null)
			{
				return;
			}
			if (bFromHeaderHandler && this.fSoap)
			{
				foreach (Header header in h)
				{
					if (header.HeaderNamespace == "http://schemas.microsoft.com/clr/soap/messageProperties")
					{
						this.FillHeader(header.Name, header.Value);
					}
					else
					{
						string propertyKeyForHeader = LogicalCallContext.GetPropertyKeyForHeader(header);
						this.FillHeader(propertyKeyForHeader, header);
					}
				}
				return;
			}
			for (int j = 0; j < h.Length; j++)
			{
				this.FillHeader(h[j].Name, h[j].Value);
			}
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x0013D53C File Offset: 0x0013B73C
		[SecurityCritical]
		internal void FillHeader(string name, object value)
		{
			if (name.Equals("__MethodName"))
			{
				this.methodName = (string)value;
				return;
			}
			if (name.Equals("__Uri"))
			{
				this.uri = (string)value;
				return;
			}
			if (name.Equals("__MethodSignature"))
			{
				this.methodSignature = (Type[])value;
				return;
			}
			if (name.Equals("__TypeName"))
			{
				this.typeName = (string)value;
				return;
			}
			if (name.Equals("__OutArgs"))
			{
				this.outArgs = (object[])value;
				return;
			}
			if (name.Equals("__CallContext"))
			{
				if (value is string)
				{
					this.callContext = new LogicalCallContext();
					this.callContext.RemotingData.LogicalCallID = (string)value;
					return;
				}
				this.callContext = (LogicalCallContext)value;
				return;
			}
			else
			{
				if (name.Equals("__Return"))
				{
					this.retVal = value;
					return;
				}
				if (this.InternalProperties == null)
				{
					this.InternalProperties = new Hashtable();
				}
				this.InternalProperties[name] = value;
				return;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06005AA2 RID: 23202 RVA: 0x0013D644 File Offset: 0x0013B844
		// (set) Token: 0x06005AA3 RID: 23203 RVA: 0x0013D647 File Offset: 0x0013B847
		ServerIdentity IInternalMessage.ServerIdentityObject
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06005AA4 RID: 23204 RVA: 0x0013D649 File Offset: 0x0013B849
		// (set) Token: 0x06005AA5 RID: 23205 RVA: 0x0013D64C File Offset: 0x0013B84C
		Identity IInternalMessage.IdentityObject
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
			}
		}

		// Token: 0x06005AA6 RID: 23206 RVA: 0x0013D64E File Offset: 0x0013B84E
		[SecurityCritical]
		void IInternalMessage.SetURI(string val)
		{
			this.uri = val;
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x0013D657 File Offset: 0x0013B857
		[SecurityCritical]
		void IInternalMessage.SetCallContext(LogicalCallContext newCallContext)
		{
			this.callContext = newCallContext;
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x0013D660 File Offset: 0x0013B860
		[SecurityCritical]
		bool IInternalMessage.HasProperties()
		{
			return this.ExternalProperties != null || this.InternalProperties != null;
		}

		// Token: 0x040028B5 RID: 10421
		private MethodBase MI;

		// Token: 0x040028B6 RID: 10422
		private string methodName;

		// Token: 0x040028B7 RID: 10423
		private Type[] methodSignature;

		// Token: 0x040028B8 RID: 10424
		private string uri;

		// Token: 0x040028B9 RID: 10425
		private string typeName;

		// Token: 0x040028BA RID: 10426
		private object retVal;

		// Token: 0x040028BB RID: 10427
		private Exception fault;

		// Token: 0x040028BC RID: 10428
		private object[] outArgs;

		// Token: 0x040028BD RID: 10429
		private LogicalCallContext callContext;

		// Token: 0x040028BE RID: 10430
		protected IDictionary InternalProperties;

		// Token: 0x040028BF RID: 10431
		protected IDictionary ExternalProperties;

		// Token: 0x040028C0 RID: 10432
		private int argCount;

		// Token: 0x040028C1 RID: 10433
		private bool fSoap;

		// Token: 0x040028C2 RID: 10434
		private ArgMapper argMapper;

		// Token: 0x040028C3 RID: 10435
		private RemotingMethodCachedData _methodCache;
	}
}
