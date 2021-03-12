using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200083C RID: 2108
	[SecurityCritical]
	[CLSCompliant(false)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class MethodCall : IMethodCallMessage, IMethodMessage, IMessage, ISerializable, IInternalMessage, ISerializationRootObject
	{
		// Token: 0x06005A45 RID: 23109 RVA: 0x0013B807 File Offset: 0x00139A07
		[SecurityCritical]
		public MethodCall(Header[] h1)
		{
			this.Init();
			this.fSoap = true;
			this.FillHeaders(h1);
			this.ResolveMethod();
		}

		// Token: 0x06005A46 RID: 23110 RVA: 0x0013B82C File Offset: 0x00139A2C
		[SecurityCritical]
		public MethodCall(IMessage msg)
		{
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			this.Init();
			IDictionaryEnumerator enumerator = msg.Properties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.FillHeader(enumerator.Key.ToString(), enumerator.Value);
			}
			IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
			if (methodCallMessage != null)
			{
				this.MI = methodCallMessage.MethodBase;
			}
			this.ResolveMethod();
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x0013B89C File Offset: 0x00139A9C
		[SecurityCritical]
		internal MethodCall(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Init();
			this.SetObjectData(info, context);
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x0013B8C0 File Offset: 0x00139AC0
		[SecurityCritical]
		internal MethodCall(SmuggledMethodCallMessage smuggledMsg, ArrayList deserializedArgs)
		{
			this.uri = smuggledMsg.Uri;
			this.typeName = smuggledMsg.TypeName;
			this.methodName = smuggledMsg.MethodName;
			this.methodSignature = (Type[])smuggledMsg.GetMethodSignature(deserializedArgs);
			this.args = smuggledMsg.GetArgs(deserializedArgs);
			this.instArgs = smuggledMsg.GetInstantiation(deserializedArgs);
			this.callContext = smuggledMsg.GetCallContext(deserializedArgs);
			this.ResolveMethod();
			if (smuggledMsg.MessagePropertyCount > 0)
			{
				smuggledMsg.PopulateMessageProperties(this.Properties, deserializedArgs);
			}
		}

		// Token: 0x06005A49 RID: 23113 RVA: 0x0013B94C File Offset: 0x00139B4C
		[SecurityCritical]
		internal MethodCall(object handlerObject, BinaryMethodCallMessage smuggledMsg)
		{
			if (handlerObject != null)
			{
				this.uri = (handlerObject as string);
				if (this.uri == null)
				{
					MarshalByRefObject marshalByRefObject = handlerObject as MarshalByRefObject;
					if (marshalByRefObject != null)
					{
						bool flag;
						this.srvID = (MarshalByRefObject.GetIdentity(marshalByRefObject, out flag) as ServerIdentity);
						this.uri = this.srvID.URI;
					}
				}
			}
			this.typeName = smuggledMsg.TypeName;
			this.methodName = smuggledMsg.MethodName;
			this.methodSignature = (Type[])smuggledMsg.MethodSignature;
			this.args = smuggledMsg.Args;
			this.instArgs = smuggledMsg.InstantiationArgs;
			this.callContext = smuggledMsg.LogicalCallContext;
			this.ResolveMethod();
			if (smuggledMsg.HasProperties)
			{
				smuggledMsg.PopulateMessageProperties(this.Properties);
			}
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x0013BA0B File Offset: 0x00139C0B
		[SecurityCritical]
		public void RootSetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			this.SetObjectData(info, ctx);
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x0013BA18 File Offset: 0x00139C18
		[SecurityCritical]
		internal void SetObjectData(SerializationInfo info, StreamingContext context)
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
			while (enumerator.MoveNext())
			{
				this.FillHeader(enumerator.Name, enumerator.Value);
			}
			if (context.State == StreamingContextStates.Remoting && context.Context != null)
			{
				Header[] array = context.Context as Header[];
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						this.FillHeader(array[i].Name, array[i].Value);
					}
				}
			}
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x0013BAB0 File Offset: 0x00139CB0
		private static Type ResolveTypeRelativeTo(string typeName, int offset, int count, Type serverType)
		{
			Type type = MethodCall.ResolveTypeRelativeToBaseTypes(typeName, offset, count, serverType);
			if (type == null)
			{
				Type[] interfaces = serverType.GetInterfaces();
				foreach (Type type2 in interfaces)
				{
					string fullName = type2.FullName;
					if (fullName.Length == count && string.CompareOrdinal(typeName, offset, fullName, 0, count) == 0)
					{
						return type2;
					}
				}
			}
			return type;
		}

		// Token: 0x06005A4D RID: 23117 RVA: 0x0013BB10 File Offset: 0x00139D10
		private static Type ResolveTypeRelativeToBaseTypes(string typeName, int offset, int count, Type serverType)
		{
			if (typeName == null || serverType == null)
			{
				return null;
			}
			string fullName = serverType.FullName;
			if (fullName.Length == count && string.CompareOrdinal(typeName, offset, fullName, 0, count) == 0)
			{
				return serverType;
			}
			return MethodCall.ResolveTypeRelativeToBaseTypes(typeName, offset, count, serverType.BaseType);
		}

		// Token: 0x06005A4E RID: 23118 RVA: 0x0013BB58 File Offset: 0x00139D58
		internal Type ResolveType()
		{
			Type type = null;
			if (this.srvID == null)
			{
				this.srvID = (IdentityHolder.CasualResolveIdentity(this.uri) as ServerIdentity);
			}
			if (this.srvID != null)
			{
				Type type2 = this.srvID.GetLastCalledType(this.typeName);
				if (type2 != null)
				{
					return type2;
				}
				int num = 0;
				if (string.CompareOrdinal(this.typeName, 0, "clr:", 0, 4) == 0)
				{
					num = 4;
				}
				int num2 = this.typeName.IndexOf(',', num);
				if (num2 == -1)
				{
					num2 = this.typeName.Length;
				}
				type2 = this.srvID.ServerType;
				type = MethodCall.ResolveTypeRelativeTo(this.typeName, num, num2 - num, type2);
			}
			if (type == null)
			{
				type = RemotingServices.InternalGetTypeFromQualifiedTypeName(this.typeName);
			}
			if (this.srvID != null)
			{
				this.srvID.SetLastCalledType(this.typeName, type);
			}
			return type;
		}

		// Token: 0x06005A4F RID: 23119 RVA: 0x0013BC2F File Offset: 0x00139E2F
		[SecurityCritical]
		public void ResolveMethod()
		{
			this.ResolveMethod(true);
		}

		// Token: 0x06005A50 RID: 23120 RVA: 0x0013BC38 File Offset: 0x00139E38
		[SecurityCritical]
		internal void ResolveMethod(bool bThrowIfNotResolved)
		{
			if (this.MI == null && this.methodName != null)
			{
				RuntimeType runtimeType = this.ResolveType() as RuntimeType;
				if (this.methodName.Equals(".ctor"))
				{
					return;
				}
				if (runtimeType == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), this.typeName));
				}
				if (this.methodSignature != null)
				{
					bool flag = false;
					int num = (this.instArgs == null) ? 0 : this.instArgs.Length;
					if (num == 0)
					{
						try
						{
							this.MI = runtimeType.GetMethod(this.methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, this.methodSignature, null);
							flag = true;
						}
						catch (AmbiguousMatchException)
						{
						}
					}
					if (!flag)
					{
						MemberInfo[] array = runtimeType.FindMembers(MemberTypes.Method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, Type.FilterName, this.methodName);
						int num2 = 0;
						for (int i = 0; i < array.Length; i++)
						{
							try
							{
								MethodInfo methodInfo = (MethodInfo)array[i];
								int num3 = methodInfo.IsGenericMethod ? methodInfo.GetGenericArguments().Length : 0;
								if (num3 == num)
								{
									if (num > 0)
									{
										methodInfo = methodInfo.MakeGenericMethod(this.instArgs);
									}
									array[num2] = methodInfo;
									num2++;
								}
							}
							catch (ArgumentException)
							{
							}
							catch (VerificationException)
							{
							}
						}
						MethodInfo[] array2 = new MethodInfo[num2];
						for (int j = 0; j < num2; j++)
						{
							array2[j] = (MethodInfo)array[j];
						}
						this.MI = Type.DefaultBinder.SelectMethod(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, array2, this.methodSignature, null);
					}
				}
				else
				{
					RemotingTypeCachedData remotingTypeCachedData = null;
					if (this.instArgs == null)
					{
						remotingTypeCachedData = InternalRemotingServices.GetReflectionCachedData(runtimeType);
						this.MI = remotingTypeCachedData.GetLastCalledMethod(this.methodName);
						if (this.MI != null)
						{
							return;
						}
					}
					bool flag2 = false;
					try
					{
						this.MI = runtimeType.GetMethod(this.methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
						if (this.instArgs != null && this.instArgs.Length != 0)
						{
							this.MI = ((MethodInfo)this.MI).MakeGenericMethod(this.instArgs);
						}
					}
					catch (AmbiguousMatchException)
					{
						flag2 = true;
						this.ResolveOverloadedMethod(runtimeType);
					}
					if (this.MI != null && !flag2 && remotingTypeCachedData != null)
					{
						remotingTypeCachedData.SetLastCalledMethod(this.methodName, this.MI);
					}
				}
				if (this.MI == null && bThrowIfNotResolved)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), this.methodName, this.typeName));
				}
			}
		}

		// Token: 0x06005A51 RID: 23121 RVA: 0x0013BED4 File Offset: 0x0013A0D4
		private void ResolveOverloadedMethod(RuntimeType t)
		{
			if (this.args == null)
			{
				return;
			}
			MemberInfo[] member = t.GetMember(this.methodName, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			int num = member.Length;
			if (num == 1)
			{
				this.MI = (member[0] as MethodBase);
				return;
			}
			if (num == 0)
			{
				return;
			}
			int num2 = this.args.Length;
			MethodBase methodBase = null;
			for (int i = 0; i < num; i++)
			{
				MethodBase methodBase2 = member[i] as MethodBase;
				if (methodBase2.GetParameters().Length == num2)
				{
					if (methodBase != null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_AmbiguousMethod"));
					}
					methodBase = methodBase2;
				}
			}
			if (methodBase != null)
			{
				this.MI = methodBase;
			}
		}

		// Token: 0x06005A52 RID: 23122 RVA: 0x0013BF74 File Offset: 0x0013A174
		private void ResolveOverloadedMethod(RuntimeType t, string methodName, ArrayList argNames, ArrayList argValues)
		{
			MemberInfo[] member = t.GetMember(methodName, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			int num = member.Length;
			if (num == 1)
			{
				this.MI = (member[0] as MethodBase);
				return;
			}
			if (num == 0)
			{
				return;
			}
			MethodBase methodBase = null;
			for (int i = 0; i < num; i++)
			{
				MethodBase methodBase2 = member[i] as MethodBase;
				ParameterInfo[] parameters = methodBase2.GetParameters();
				if (parameters.Length == argValues.Count)
				{
					bool flag = true;
					for (int j = 0; j < parameters.Length; j++)
					{
						Type type = parameters[j].ParameterType;
						if (type.IsByRef)
						{
							type = type.GetElementType();
						}
						if (type != argValues[j].GetType())
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						methodBase = methodBase2;
						break;
					}
				}
			}
			if (methodBase == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_AmbiguousMethod"));
			}
			this.MI = methodBase;
		}

		// Token: 0x06005A53 RID: 23123 RVA: 0x0013C051 File Offset: 0x0013A251
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x06005A54 RID: 23124 RVA: 0x0013C064 File Offset: 0x0013A264
		[SecurityCritical]
		internal void SetObjectFromSoapData(SerializationInfo info)
		{
			this.methodName = info.GetString("__methodName");
			ArrayList arrayList = (ArrayList)info.GetValue("__paramNameList", typeof(ArrayList));
			Hashtable keyToNamespaceTable = (Hashtable)info.GetValue("__keyToNamespaceTable", typeof(Hashtable));
			if (this.MI == null)
			{
				ArrayList arrayList2 = new ArrayList();
				ArrayList arrayList3 = arrayList;
				for (int i = 0; i < arrayList3.Count; i++)
				{
					arrayList2.Add(info.GetValue((string)arrayList3[i], typeof(object)));
				}
				RuntimeType runtimeType = this.ResolveType() as RuntimeType;
				if (runtimeType == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), this.typeName));
				}
				this.ResolveOverloadedMethod(runtimeType, this.methodName, arrayList3, arrayList2);
				if (this.MI == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), this.methodName, this.typeName));
				}
			}
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.MI);
			ParameterInfo[] parameters = reflectionCachedData.Parameters;
			int[] marshalRequestArgMap = reflectionCachedData.MarshalRequestArgMap;
			object obj = (this.InternalProperties == null) ? null : this.InternalProperties["__UnorderedParams"];
			this.args = new object[parameters.Length];
			if (obj != null && obj is bool && (bool)obj)
			{
				for (int j = 0; j < arrayList.Count; j++)
				{
					string text = (string)arrayList[j];
					int num = -1;
					for (int k = 0; k < parameters.Length; k++)
					{
						if (text.Equals(parameters[k].Name))
						{
							num = parameters[k].Position;
							break;
						}
					}
					if (num == -1)
					{
						if (!text.StartsWith("__param", StringComparison.Ordinal))
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
						}
						num = int.Parse(text.Substring(7), CultureInfo.InvariantCulture);
					}
					if (num >= this.args.Length)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
					}
					this.args[num] = Message.SoapCoerceArg(info.GetValue(text, typeof(object)), parameters[num].ParameterType, keyToNamespaceTable);
				}
				return;
			}
			for (int l = 0; l < arrayList.Count; l++)
			{
				string name = (string)arrayList[l];
				this.args[marshalRequestArgMap[l]] = Message.SoapCoerceArg(info.GetValue(name, typeof(object)), parameters[marshalRequestArgMap[l]].ParameterType, keyToNamespaceTable);
			}
			this.PopulateOutArguments(reflectionCachedData);
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x0013C32C File Offset: 0x0013A52C
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void PopulateOutArguments(RemotingMethodCachedData methodCache)
		{
			ParameterInfo[] parameters = methodCache.Parameters;
			foreach (int num in methodCache.OutOnlyArgMap)
			{
				Type elementType = parameters[num].ParameterType.GetElementType();
				if (elementType.IsValueType)
				{
					this.args[num] = Activator.CreateInstance(elementType, true);
				}
			}
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x0013C381 File Offset: 0x0013A581
		public virtual void Init()
		{
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06005A57 RID: 23127 RVA: 0x0013C383 File Offset: 0x0013A583
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this.args != null)
				{
					return this.args.Length;
				}
				return 0;
			}
		}

		// Token: 0x06005A58 RID: 23128 RVA: 0x0013C397 File Offset: 0x0013A597
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			return this.args[argNum];
		}

		// Token: 0x06005A59 RID: 23129 RVA: 0x0013C3A4 File Offset: 0x0013A5A4
		[SecurityCritical]
		public string GetArgName(int index)
		{
			this.ResolveMethod();
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.MI);
			return reflectionCachedData.Parameters[index].Name;
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06005A5A RID: 23130 RVA: 0x0013C3D0 File Offset: 0x0013A5D0
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return this.args;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06005A5B RID: 23131 RVA: 0x0013C3D8 File Offset: 0x0013A5D8
		public int InArgCount
		{
			[SecurityCritical]
			get
			{
				if (this.argMapper == null)
				{
					this.argMapper = new ArgMapper(this, false);
				}
				return this.argMapper.ArgCount;
			}
		}

		// Token: 0x06005A5C RID: 23132 RVA: 0x0013C3FA File Offset: 0x0013A5FA
		[SecurityCritical]
		public object GetInArg(int argNum)
		{
			if (this.argMapper == null)
			{
				this.argMapper = new ArgMapper(this, false);
			}
			return this.argMapper.GetArg(argNum);
		}

		// Token: 0x06005A5D RID: 23133 RVA: 0x0013C41D File Offset: 0x0013A61D
		[SecurityCritical]
		public string GetInArgName(int index)
		{
			if (this.argMapper == null)
			{
				this.argMapper = new ArgMapper(this, false);
			}
			return this.argMapper.GetArgName(index);
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06005A5E RID: 23134 RVA: 0x0013C440 File Offset: 0x0013A640
		public object[] InArgs
		{
			[SecurityCritical]
			get
			{
				if (this.argMapper == null)
				{
					this.argMapper = new ArgMapper(this, false);
				}
				return this.argMapper.Args;
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06005A5F RID: 23135 RVA: 0x0013C462 File Offset: 0x0013A662
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				return this.methodName;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06005A60 RID: 23136 RVA: 0x0013C46A File Offset: 0x0013A66A
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06005A61 RID: 23137 RVA: 0x0013C472 File Offset: 0x0013A672
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				if (this.methodSignature != null)
				{
					return this.methodSignature;
				}
				if (this.MI != null)
				{
					this.methodSignature = Message.GenerateMethodSignature(this.MethodBase);
				}
				return null;
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06005A62 RID: 23138 RVA: 0x0013C4A3 File Offset: 0x0013A6A3
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				if (this.MI == null)
				{
					this.MI = RemotingServices.InternalGetMethodBaseFromMethodMessage(this);
				}
				return this.MI;
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06005A63 RID: 23139 RVA: 0x0013C4C5 File Offset: 0x0013A6C5
		// (set) Token: 0x06005A64 RID: 23140 RVA: 0x0013C4CD File Offset: 0x0013A6CD
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

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06005A65 RID: 23141 RVA: 0x0013C4D6 File Offset: 0x0013A6D6
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return this.fVarArgs;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06005A66 RID: 23142 RVA: 0x0013C4E0 File Offset: 0x0013A6E0
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
						this.ExternalProperties = new MCMDictionary(this, this.InternalProperties);
					}
					externalProperties = this.ExternalProperties;
				}
				return externalProperties;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06005A67 RID: 23143 RVA: 0x0013C54C File Offset: 0x0013A74C
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this.GetLogicalCallContext();
			}
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x0013C554 File Offset: 0x0013A754
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			if (this.callContext == null)
			{
				this.callContext = new LogicalCallContext();
			}
			return this.callContext;
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x0013C570 File Offset: 0x0013A770
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
		{
			LogicalCallContext result = this.callContext;
			this.callContext = ctx;
			return result;
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06005A6A RID: 23146 RVA: 0x0013C58C File Offset: 0x0013A78C
		// (set) Token: 0x06005A6B RID: 23147 RVA: 0x0013C594 File Offset: 0x0013A794
		ServerIdentity IInternalMessage.ServerIdentityObject
		{
			[SecurityCritical]
			get
			{
				return this.srvID;
			}
			[SecurityCritical]
			set
			{
				this.srvID = value;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06005A6C RID: 23148 RVA: 0x0013C59D File Offset: 0x0013A79D
		// (set) Token: 0x06005A6D RID: 23149 RVA: 0x0013C5A5 File Offset: 0x0013A7A5
		Identity IInternalMessage.IdentityObject
		{
			[SecurityCritical]
			get
			{
				return this.identity;
			}
			[SecurityCritical]
			set
			{
				this.identity = value;
			}
		}

		// Token: 0x06005A6E RID: 23150 RVA: 0x0013C5AE File Offset: 0x0013A7AE
		[SecurityCritical]
		void IInternalMessage.SetURI(string val)
		{
			this.uri = val;
		}

		// Token: 0x06005A6F RID: 23151 RVA: 0x0013C5B7 File Offset: 0x0013A7B7
		[SecurityCritical]
		void IInternalMessage.SetCallContext(LogicalCallContext newCallContext)
		{
			this.callContext = newCallContext;
		}

		// Token: 0x06005A70 RID: 23152 RVA: 0x0013C5C0 File Offset: 0x0013A7C0
		[SecurityCritical]
		bool IInternalMessage.HasProperties()
		{
			return this.ExternalProperties != null || this.InternalProperties != null;
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x0013C5D5 File Offset: 0x0013A7D5
		[SecurityCritical]
		internal void FillHeaders(Header[] h)
		{
			this.FillHeaders(h, false);
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x0013C5E0 File Offset: 0x0013A7E0
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

		// Token: 0x06005A73 RID: 23155 RVA: 0x0013C668 File Offset: 0x0013A868
		[SecurityCritical]
		internal virtual bool FillSpecialHeader(string key, object value)
		{
			if (key != null)
			{
				if (key.Equals("__Uri"))
				{
					this.uri = (string)value;
				}
				else if (key.Equals("__MethodName"))
				{
					this.methodName = (string)value;
				}
				else if (key.Equals("__MethodSignature"))
				{
					this.methodSignature = (Type[])value;
				}
				else if (key.Equals("__TypeName"))
				{
					this.typeName = (string)value;
				}
				else if (key.Equals("__Args"))
				{
					this.args = (object[])value;
				}
				else
				{
					if (!key.Equals("__CallContext"))
					{
						return false;
					}
					if (value is string)
					{
						this.callContext = new LogicalCallContext();
						this.callContext.RemotingData.LogicalCallID = (string)value;
					}
					else
					{
						this.callContext = (LogicalCallContext)value;
					}
				}
			}
			return true;
		}

		// Token: 0x06005A74 RID: 23156 RVA: 0x0013C751 File Offset: 0x0013A951
		[SecurityCritical]
		internal void FillHeader(string key, object value)
		{
			if (!this.FillSpecialHeader(key, value))
			{
				if (this.InternalProperties == null)
				{
					this.InternalProperties = new Hashtable();
				}
				this.InternalProperties[key] = value;
			}
		}

		// Token: 0x06005A75 RID: 23157 RVA: 0x0013C780 File Offset: 0x0013A980
		[SecurityCritical]
		public virtual object HeaderHandler(Header[] h)
		{
			SerializationMonkey serializationMonkey = (SerializationMonkey)FormatterServices.GetUninitializedObject(typeof(SerializationMonkey));
			Header[] array;
			if (h != null && h.Length != 0 && h[0].Name == "__methodName")
			{
				this.methodName = (string)h[0].Value;
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
			this.FillHeaders(array, true);
			this.ResolveMethod(false);
			serializationMonkey._obj = this;
			if (this.MI != null)
			{
				ArgMapper argMapper = new ArgMapper(this.MI, false);
				serializationMonkey.fieldNames = argMapper.ArgNames;
				serializationMonkey.fieldTypes = argMapper.ArgTypes;
			}
			return serializationMonkey;
		}

		// Token: 0x0400289F RID: 10399
		private const BindingFlags LookupAll = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x040028A0 RID: 10400
		private const BindingFlags LookupPublic = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		// Token: 0x040028A1 RID: 10401
		private string uri;

		// Token: 0x040028A2 RID: 10402
		private string methodName;

		// Token: 0x040028A3 RID: 10403
		private MethodBase MI;

		// Token: 0x040028A4 RID: 10404
		private string typeName;

		// Token: 0x040028A5 RID: 10405
		private object[] args;

		// Token: 0x040028A6 RID: 10406
		private Type[] instArgs;

		// Token: 0x040028A7 RID: 10407
		private LogicalCallContext callContext;

		// Token: 0x040028A8 RID: 10408
		private Type[] methodSignature;

		// Token: 0x040028A9 RID: 10409
		protected IDictionary ExternalProperties;

		// Token: 0x040028AA RID: 10410
		protected IDictionary InternalProperties;

		// Token: 0x040028AB RID: 10411
		private ServerIdentity srvID;

		// Token: 0x040028AC RID: 10412
		private Identity identity;

		// Token: 0x040028AD RID: 10413
		private bool fSoap;

		// Token: 0x040028AE RID: 10414
		private bool fVarArgs;

		// Token: 0x040028AF RID: 10415
		private ArgMapper argMapper;
	}
}
