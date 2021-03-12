using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000831 RID: 2097
	[Serializable]
	internal class Message : IMethodCallMessage, IMethodMessage, IMessage, IInternalMessage, ISerializable
	{
		// Token: 0x0600597B RID: 22907 RVA: 0x001393DB File Offset: 0x001375DB
		public virtual Exception GetFault()
		{
			return this._Fault;
		}

		// Token: 0x0600597C RID: 22908 RVA: 0x001393E3 File Offset: 0x001375E3
		public virtual void SetFault(Exception e)
		{
			this._Fault = e;
		}

		// Token: 0x0600597D RID: 22909 RVA: 0x001393EC File Offset: 0x001375EC
		internal virtual void SetOneWay()
		{
			this._flags |= 8;
		}

		// Token: 0x0600597E RID: 22910 RVA: 0x001393FC File Offset: 0x001375FC
		public virtual int GetCallType()
		{
			this.InitIfNecessary();
			return this._flags;
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x0013940A File Offset: 0x0013760A
		internal IntPtr GetFramePtr()
		{
			return this._frame;
		}

		// Token: 0x06005980 RID: 22912
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetAsyncBeginInfo(out AsyncCallback acbd, out object state);

		// Token: 0x06005981 RID: 22913
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern object GetThisPtr();

		// Token: 0x06005982 RID: 22914
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IAsyncResult GetAsyncResult();

		// Token: 0x06005983 RID: 22915 RVA: 0x00139412 File Offset: 0x00137612
		public void Init()
		{
		}

		// Token: 0x06005984 RID: 22916
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern object GetReturnValue();

		// Token: 0x06005985 RID: 22917 RVA: 0x00139414 File Offset: 0x00137614
		internal Message()
		{
		}

		// Token: 0x06005986 RID: 22918 RVA: 0x0013941C File Offset: 0x0013761C
		[SecurityCritical]
		internal void InitFields(MessageData msgData)
		{
			this._frame = msgData.pFrame;
			this._delegateMD = msgData.pDelegateMD;
			this._methodDesc = msgData.pMethodDesc;
			this._flags = msgData.iFlags;
			this._initDone = true;
			this._metaSigHolder = msgData.pSig;
			this._governingType = msgData.thGoverningType;
			this._MethodName = null;
			this._MethodSignature = null;
			this._MethodBase = null;
			this._URI = null;
			this._Fault = null;
			this._ID = null;
			this._srvID = null;
			this._callContext = null;
			if (this._properties != null)
			{
				((IDictionary)this._properties).Clear();
			}
		}

		// Token: 0x06005987 RID: 22919 RVA: 0x001394C8 File Offset: 0x001376C8
		private void InitIfNecessary()
		{
			if (!this._initDone)
			{
				this.Init();
				this._initDone = true;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06005988 RID: 22920 RVA: 0x001394DF File Offset: 0x001376DF
		// (set) Token: 0x06005989 RID: 22921 RVA: 0x001394E7 File Offset: 0x001376E7
		ServerIdentity IInternalMessage.ServerIdentityObject
		{
			[SecurityCritical]
			get
			{
				return this._srvID;
			}
			[SecurityCritical]
			set
			{
				this._srvID = value;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x0600598A RID: 22922 RVA: 0x001394F0 File Offset: 0x001376F0
		// (set) Token: 0x0600598B RID: 22923 RVA: 0x001394F8 File Offset: 0x001376F8
		Identity IInternalMessage.IdentityObject
		{
			[SecurityCritical]
			get
			{
				return this._ID;
			}
			[SecurityCritical]
			set
			{
				this._ID = value;
			}
		}

		// Token: 0x0600598C RID: 22924 RVA: 0x00139501 File Offset: 0x00137701
		[SecurityCritical]
		void IInternalMessage.SetURI(string URI)
		{
			this._URI = URI;
		}

		// Token: 0x0600598D RID: 22925 RVA: 0x0013950A File Offset: 0x0013770A
		[SecurityCritical]
		void IInternalMessage.SetCallContext(LogicalCallContext callContext)
		{
			this._callContext = callContext;
		}

		// Token: 0x0600598E RID: 22926 RVA: 0x00139513 File Offset: 0x00137713
		[SecurityCritical]
		bool IInternalMessage.HasProperties()
		{
			return this._properties != null;
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x0600598F RID: 22927 RVA: 0x0013951E File Offset: 0x0013771E
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					Interlocked.CompareExchange(ref this._properties, new MCMDictionary(this, null), null);
				}
				return (IDictionary)this._properties;
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06005990 RID: 22928 RVA: 0x00139547 File Offset: 0x00137747
		// (set) Token: 0x06005991 RID: 22929 RVA: 0x0013954F File Offset: 0x0013774F
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this._URI;
			}
			set
			{
				this._URI = value;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06005992 RID: 22930 RVA: 0x00139558 File Offset: 0x00137758
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				if ((this._flags & 16) == 0 && (this._flags & 32) == 0)
				{
					if (!this.InternalHasVarArgs())
					{
						this._flags |= 16;
					}
					else
					{
						this._flags |= 32;
					}
				}
				return 1 == (this._flags & 32);
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06005993 RID: 22931 RVA: 0x001395AF File Offset: 0x001377AF
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				return this.InternalGetArgCount();
			}
		}

		// Token: 0x06005994 RID: 22932 RVA: 0x001395B7 File Offset: 0x001377B7
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			return this.InternalGetArg(argNum);
		}

		// Token: 0x06005995 RID: 22933 RVA: 0x001395C0 File Offset: 0x001377C0
		[SecurityCritical]
		public string GetArgName(int index)
		{
			if (index >= this.ArgCount)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.GetMethodBase());
			ParameterInfo[] parameters = reflectionCachedData.Parameters;
			if (index < parameters.Length)
			{
				return parameters[index].Name;
			}
			return "VarArg" + (index - parameters.Length);
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06005996 RID: 22934 RVA: 0x00139617 File Offset: 0x00137817
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return this.InternalGetArgs();
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06005997 RID: 22935 RVA: 0x0013961F File Offset: 0x0013781F
		public int InArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, false);
				}
				return this._argMapper.ArgCount;
			}
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x00139641 File Offset: 0x00137841
		[SecurityCritical]
		public object GetInArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x06005999 RID: 22937 RVA: 0x00139664 File Offset: 0x00137864
		[SecurityCritical]
		public string GetInArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x0600599A RID: 22938 RVA: 0x00139687 File Offset: 0x00137887
		public object[] InArgs
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, false);
				}
				return this._argMapper.Args;
			}
		}

		// Token: 0x0600599B RID: 22939 RVA: 0x001396AC File Offset: 0x001378AC
		[SecurityCritical]
		private void UpdateNames()
		{
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.GetMethodBase());
			this._typeName = reflectionCachedData.TypeAndAssemblyName;
			this._MethodName = reflectionCachedData.MethodName;
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x0600599C RID: 22940 RVA: 0x001396DD File Offset: 0x001378DD
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				if (this._MethodName == null)
				{
					this.UpdateNames();
				}
				return this._MethodName;
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x0600599D RID: 22941 RVA: 0x001396F3 File Offset: 0x001378F3
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				if (this._typeName == null)
				{
					this.UpdateNames();
				}
				return this._typeName;
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x0600599E RID: 22942 RVA: 0x00139709 File Offset: 0x00137909
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				if (this._MethodSignature == null)
				{
					this._MethodSignature = Message.GenerateMethodSignature(this.GetMethodBase());
				}
				return this._MethodSignature;
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x0600599F RID: 22943 RVA: 0x0013972A File Offset: 0x0013792A
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this.GetLogicalCallContext();
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x060059A0 RID: 22944 RVA: 0x00139732 File Offset: 0x00137932
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this.GetMethodBase();
			}
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x0013973A File Offset: 0x0013793A
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x0013974C File Offset: 0x0013794C
		[SecurityCritical]
		internal MethodBase GetMethodBase()
		{
			if (null == this._MethodBase)
			{
				IRuntimeMethodInfo methodHandle = new RuntimeMethodInfoStub(this._methodDesc, null);
				this._MethodBase = RuntimeType.GetMethodBase(Type.GetTypeFromHandleUnsafe(this._governingType), methodHandle);
			}
			return this._MethodBase;
		}

		// Token: 0x060059A3 RID: 22947 RVA: 0x00139794 File Offset: 0x00137994
		[SecurityCritical]
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
		{
			LogicalCallContext callContext = this._callContext;
			this._callContext = callCtx;
			return callContext;
		}

		// Token: 0x060059A4 RID: 22948 RVA: 0x001397B0 File Offset: 0x001379B0
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			return this._callContext;
		}

		// Token: 0x060059A5 RID: 22949 RVA: 0x001397CC File Offset: 0x001379CC
		[SecurityCritical]
		internal static Type[] GenerateMethodSignature(MethodBase mb)
		{
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(mb);
			ParameterInfo[] parameters = reflectionCachedData.Parameters;
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			return array;
		}

		// Token: 0x060059A6 RID: 22950 RVA: 0x0013980C File Offset: 0x00137A0C
		[SecurityCritical]
		internal static object[] CoerceArgs(IMethodMessage m)
		{
			MethodBase methodBase = m.MethodBase;
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(methodBase);
			return Message.CoerceArgs(m, reflectionCachedData.Parameters);
		}

		// Token: 0x060059A7 RID: 22951 RVA: 0x00139833 File Offset: 0x00137A33
		[SecurityCritical]
		internal static object[] CoerceArgs(IMethodMessage m, ParameterInfo[] pi)
		{
			return Message.CoerceArgs(m.MethodBase, m.Args, pi);
		}

		// Token: 0x060059A8 RID: 22952 RVA: 0x00139848 File Offset: 0x00137A48
		[SecurityCritical]
		internal static object[] CoerceArgs(MethodBase mb, object[] args, ParameterInfo[] pi)
		{
			if (pi == null)
			{
				throw new ArgumentNullException("pi");
			}
			if (pi.Length != args.Length)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_ArgMismatch"), new object[]
				{
					mb.DeclaringType.FullName,
					mb.Name,
					args.Length,
					pi.Length
				}));
			}
			for (int i = 0; i < pi.Length; i++)
			{
				ParameterInfo parameterInfo = pi[i];
				Type parameterType = parameterInfo.ParameterType;
				object obj = args[i];
				if (obj != null)
				{
					args[i] = Message.CoerceArg(obj, parameterType);
				}
				else if (parameterType.IsByRef)
				{
					Type elementType = parameterType.GetElementType();
					if (elementType.IsValueType)
					{
						if (parameterInfo.IsOut)
						{
							args[i] = Activator.CreateInstance(elementType, true);
						}
						else if (!elementType.IsGenericType || !(elementType.GetGenericTypeDefinition() == typeof(Nullable<>)))
						{
							throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MissingArgValue"), elementType.FullName, i));
						}
					}
				}
				else if (parameterType.IsValueType && (!parameterType.IsGenericType || !(parameterType.GetGenericTypeDefinition() == typeof(Nullable<>))))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MissingArgValue"), parameterType.FullName, i));
				}
			}
			return args;
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x001399B8 File Offset: 0x00137BB8
		[SecurityCritical]
		internal static object CoerceArg(object value, Type pt)
		{
			object obj = null;
			if (value != null)
			{
				Exception innerException = null;
				try
				{
					if (pt.IsByRef)
					{
						pt = pt.GetElementType();
					}
					if (pt.IsInstanceOfType(value))
					{
						obj = value;
					}
					else
					{
						obj = Convert.ChangeType(value, pt, CultureInfo.InvariantCulture);
					}
				}
				catch (Exception ex)
				{
					innerException = ex;
				}
				if (obj == null)
				{
					string arg;
					if (RemotingServices.IsTransparentProxy(value))
					{
						arg = typeof(MarshalByRefObject).ToString();
					}
					else
					{
						arg = value.ToString();
					}
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_CoercionFailed"), arg, pt), innerException);
				}
			}
			return obj;
		}

		// Token: 0x060059AA RID: 22954 RVA: 0x00139A54 File Offset: 0x00137C54
		[SecurityCritical]
		internal static object SoapCoerceArg(object value, Type pt, Hashtable keyToNamespaceTable)
		{
			object obj = null;
			if (value != null)
			{
				try
				{
					if (pt.IsByRef)
					{
						pt = pt.GetElementType();
					}
					if (pt.IsInstanceOfType(value))
					{
						obj = value;
					}
					else
					{
						string text = value as string;
						if (text != null)
						{
							if (pt == typeof(double))
							{
								if (text == "INF")
								{
									obj = double.PositiveInfinity;
								}
								else if (text == "-INF")
								{
									obj = double.NegativeInfinity;
								}
								else
								{
									obj = double.Parse(text, CultureInfo.InvariantCulture);
								}
							}
							else if (pt == typeof(float))
							{
								if (text == "INF")
								{
									obj = float.PositiveInfinity;
								}
								else if (text == "-INF")
								{
									obj = float.NegativeInfinity;
								}
								else
								{
									obj = float.Parse(text, CultureInfo.InvariantCulture);
								}
							}
							else if (SoapType.typeofISoapXsd.IsAssignableFrom(pt))
							{
								if (pt == SoapType.typeofSoapTime)
								{
									obj = SoapTime.Parse(text);
								}
								else if (pt == SoapType.typeofSoapDate)
								{
									obj = SoapDate.Parse(text);
								}
								else if (pt == SoapType.typeofSoapYearMonth)
								{
									obj = SoapYearMonth.Parse(text);
								}
								else if (pt == SoapType.typeofSoapYear)
								{
									obj = SoapYear.Parse(text);
								}
								else if (pt == SoapType.typeofSoapMonthDay)
								{
									obj = SoapMonthDay.Parse(text);
								}
								else if (pt == SoapType.typeofSoapDay)
								{
									obj = SoapDay.Parse(text);
								}
								else if (pt == SoapType.typeofSoapMonth)
								{
									obj = SoapMonth.Parse(text);
								}
								else if (pt == SoapType.typeofSoapHexBinary)
								{
									obj = SoapHexBinary.Parse(text);
								}
								else if (pt == SoapType.typeofSoapBase64Binary)
								{
									obj = SoapBase64Binary.Parse(text);
								}
								else if (pt == SoapType.typeofSoapInteger)
								{
									obj = SoapInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapPositiveInteger)
								{
									obj = SoapPositiveInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNonPositiveInteger)
								{
									obj = SoapNonPositiveInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNonNegativeInteger)
								{
									obj = SoapNonNegativeInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNegativeInteger)
								{
									obj = SoapNegativeInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapAnyUri)
								{
									obj = SoapAnyUri.Parse(text);
								}
								else if (pt == SoapType.typeofSoapQName)
								{
									obj = SoapQName.Parse(text);
									SoapQName soapQName = (SoapQName)obj;
									if (soapQName.Key.Length == 0)
									{
										soapQName.Namespace = (string)keyToNamespaceTable["xmlns"];
									}
									else
									{
										soapQName.Namespace = (string)keyToNamespaceTable["xmlns:" + soapQName.Key];
									}
								}
								else if (pt == SoapType.typeofSoapNotation)
								{
									obj = SoapNotation.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNormalizedString)
								{
									obj = SoapNormalizedString.Parse(text);
								}
								else if (pt == SoapType.typeofSoapToken)
								{
									obj = SoapToken.Parse(text);
								}
								else if (pt == SoapType.typeofSoapLanguage)
								{
									obj = SoapLanguage.Parse(text);
								}
								else if (pt == SoapType.typeofSoapName)
								{
									obj = SoapName.Parse(text);
								}
								else if (pt == SoapType.typeofSoapIdrefs)
								{
									obj = SoapIdrefs.Parse(text);
								}
								else if (pt == SoapType.typeofSoapEntities)
								{
									obj = SoapEntities.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNmtoken)
								{
									obj = SoapNmtoken.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNmtokens)
								{
									obj = SoapNmtokens.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNcName)
								{
									obj = SoapNcName.Parse(text);
								}
								else if (pt == SoapType.typeofSoapId)
								{
									obj = SoapId.Parse(text);
								}
								else if (pt == SoapType.typeofSoapIdref)
								{
									obj = SoapIdref.Parse(text);
								}
								else if (pt == SoapType.typeofSoapEntity)
								{
									obj = SoapEntity.Parse(text);
								}
							}
							else if (pt == typeof(bool))
							{
								if (text == "1" || text == "true")
								{
									obj = true;
								}
								else
								{
									if (!(text == "0") && !(text == "false"))
									{
										throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_CoercionFailed"), text, pt));
									}
									obj = false;
								}
							}
							else if (pt == typeof(DateTime))
							{
								obj = SoapDateTime.Parse(text);
							}
							else if (pt.IsPrimitive)
							{
								obj = Convert.ChangeType(value, pt, CultureInfo.InvariantCulture);
							}
							else if (pt == typeof(TimeSpan))
							{
								obj = SoapDuration.Parse(text);
							}
							else if (pt == typeof(char))
							{
								obj = text[0];
							}
							else
							{
								obj = Convert.ChangeType(value, pt, CultureInfo.InvariantCulture);
							}
						}
						else
						{
							obj = Convert.ChangeType(value, pt, CultureInfo.InvariantCulture);
						}
					}
				}
				catch (Exception)
				{
				}
				if (obj == null)
				{
					string arg;
					if (RemotingServices.IsTransparentProxy(value))
					{
						arg = typeof(MarshalByRefObject).ToString();
					}
					else
					{
						arg = value.ToString();
					}
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_CoercionFailed"), arg, pt));
				}
			}
			return obj;
		}

		// Token: 0x060059AB RID: 22955
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool InternalHasVarArgs();

		// Token: 0x060059AC RID: 22956
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int InternalGetArgCount();

		// Token: 0x060059AD RID: 22957
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object InternalGetArg(int argNum);

		// Token: 0x060059AE RID: 22958
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object[] InternalGetArgs();

		// Token: 0x060059AF RID: 22959
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PropagateOutParameters(object[] OutArgs, object retVal);

		// Token: 0x060059B0 RID: 22960
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Dispatch(object target);

		// Token: 0x060059B1 RID: 22961 RVA: 0x0013A00C File Offset: 0x0013820C
		[SecurityCritical]
		[Conditional("_REMOTING_DEBUG")]
		public static void DebugOut(string s)
		{
			Message.OutToUnmanagedDebugger(string.Concat(new object[]
			{
				"\nRMTING: Thrd ",
				Thread.CurrentThread.GetHashCode(),
				" : ",
				s
			}));
		}

		// Token: 0x060059B2 RID: 22962
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void OutToUnmanagedDebugger(string s);

		// Token: 0x060059B3 RID: 22963 RVA: 0x0013A044 File Offset: 0x00138244
		[SecurityCritical]
		internal static LogicalCallContext PropagateCallContextFromMessageToThread(IMessage msg)
		{
			return CallContext.SetLogicalCallContext((LogicalCallContext)msg.Properties[Message.CallContextKey]);
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x0013A060 File Offset: 0x00138260
		[SecurityCritical]
		internal static void PropagateCallContextFromThreadToMessage(IMessage msg)
		{
			LogicalCallContext logicalCallContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			msg.Properties[Message.CallContextKey] = logicalCallContext;
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x0013A08E File Offset: 0x0013828E
		[SecurityCritical]
		internal static void PropagateCallContextFromThreadToMessage(IMessage msg, LogicalCallContext oldcctx)
		{
			Message.PropagateCallContextFromThreadToMessage(msg);
			CallContext.SetLogicalCallContext(oldcctx);
		}

		// Token: 0x04002852 RID: 10322
		internal const int Sync = 0;

		// Token: 0x04002853 RID: 10323
		internal const int BeginAsync = 1;

		// Token: 0x04002854 RID: 10324
		internal const int EndAsync = 2;

		// Token: 0x04002855 RID: 10325
		internal const int Ctor = 4;

		// Token: 0x04002856 RID: 10326
		internal const int OneWay = 8;

		// Token: 0x04002857 RID: 10327
		internal const int CallMask = 15;

		// Token: 0x04002858 RID: 10328
		internal const int FixedArgs = 16;

		// Token: 0x04002859 RID: 10329
		internal const int VarArgs = 32;

		// Token: 0x0400285A RID: 10330
		private string _MethodName;

		// Token: 0x0400285B RID: 10331
		private Type[] _MethodSignature;

		// Token: 0x0400285C RID: 10332
		private MethodBase _MethodBase;

		// Token: 0x0400285D RID: 10333
		private object _properties;

		// Token: 0x0400285E RID: 10334
		private string _URI;

		// Token: 0x0400285F RID: 10335
		private string _typeName;

		// Token: 0x04002860 RID: 10336
		private Exception _Fault;

		// Token: 0x04002861 RID: 10337
		private Identity _ID;

		// Token: 0x04002862 RID: 10338
		private ServerIdentity _srvID;

		// Token: 0x04002863 RID: 10339
		private ArgMapper _argMapper;

		// Token: 0x04002864 RID: 10340
		[SecurityCritical]
		private LogicalCallContext _callContext;

		// Token: 0x04002865 RID: 10341
		private IntPtr _frame;

		// Token: 0x04002866 RID: 10342
		private IntPtr _methodDesc;

		// Token: 0x04002867 RID: 10343
		private IntPtr _metaSigHolder;

		// Token: 0x04002868 RID: 10344
		private IntPtr _delegateMD;

		// Token: 0x04002869 RID: 10345
		private IntPtr _governingType;

		// Token: 0x0400286A RID: 10346
		private int _flags;

		// Token: 0x0400286B RID: 10347
		private bool _initDone;

		// Token: 0x0400286C RID: 10348
		internal static string CallContextKey = "__CallContext";

		// Token: 0x0400286D RID: 10349
		internal static string UriKey = "__Uri";
	}
}
