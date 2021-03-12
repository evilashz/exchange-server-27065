using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System
{
	// Token: 0x0200007C RID: 124
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Exception))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class Exception : ISerializable, _Exception
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x00013F14 File Offset: 0x00012114
		private void Init()
		{
			this._message = null;
			this._stackTrace = null;
			this._dynamicMethods = null;
			this.HResult = -2146233088;
			this._xcode = -532462766;
			this._xptrs = (IntPtr)0;
			this._watsonBuckets = null;
			this._ipForWatsonBuckets = UIntPtr.Zero;
			this._safeSerializationManager = new SafeSerializationManager();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00013F75 File Offset: 0x00012175
		[__DynamicallyInvokable]
		public Exception()
		{
			this.Init();
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00013F83 File Offset: 0x00012183
		[__DynamicallyInvokable]
		public Exception(string message)
		{
			this.Init();
			this._message = message;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00013F98 File Offset: 0x00012198
		[__DynamicallyInvokable]
		public Exception(string message, Exception innerException)
		{
			this.Init();
			this._message = message;
			this._innerException = innerException;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00013FB4 File Offset: 0x000121B4
		[SecuritySafeCritical]
		protected Exception(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._className = info.GetString("ClassName");
			this._message = info.GetString("Message");
			this._data = (IDictionary)info.GetValueNoThrow("Data", typeof(IDictionary));
			this._innerException = (Exception)info.GetValue("InnerException", typeof(Exception));
			this._helpURL = info.GetString("HelpURL");
			this._stackTraceString = info.GetString("StackTraceString");
			this._remoteStackTraceString = info.GetString("RemoteStackTraceString");
			this._remoteStackIndex = info.GetInt32("RemoteStackIndex");
			this._exceptionMethodString = (string)info.GetValue("ExceptionMethod", typeof(string));
			this.HResult = info.GetInt32("HResult");
			this._source = info.GetString("Source");
			this._watsonBuckets = info.GetValueNoThrow("WatsonBuckets", typeof(byte[]));
			this._safeSerializationManager = (info.GetValueNoThrow("SafeSerializationManager", typeof(SafeSerializationManager)) as SafeSerializationManager);
			if (this._className == null || this.HResult == 0)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
			}
			if (context.State == StreamingContextStates.CrossAppDomain)
			{
				this._remoteStackTraceString += this._stackTraceString;
				this._stackTraceString = null;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00014144 File Offset: 0x00012344
		[__DynamicallyInvokable]
		public virtual string Message
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._message == null)
				{
					if (this._className == null)
					{
						this._className = this.GetClassName();
					}
					return Environment.GetResourceString("Exception_WasThrown", new object[]
					{
						this._className
					});
				}
				return this._message;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00014182 File Offset: 0x00012382
		[__DynamicallyInvokable]
		public virtual IDictionary Data
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				if (this._data == null)
				{
					if (Exception.IsImmutableAgileException(this))
					{
						this._data = new EmptyReadOnlyDictionaryInternal();
					}
					else
					{
						this._data = new ListDictionaryInternal();
					}
				}
				return this._data;
			}
		}

		// Token: 0x060005B2 RID: 1458
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsImmutableAgileException(Exception e);

		// Token: 0x060005B3 RID: 1459 RVA: 0x000141B4 File Offset: 0x000123B4
		[FriendAccessAllowed]
		internal void AddExceptionDataForRestrictedErrorInfo(string restrictedError, string restrictedErrorReference, string restrictedCapabilitySid, object restrictedErrorObject, bool hasrestrictedLanguageErrorObject = false)
		{
			IDictionary data = this.Data;
			if (data != null)
			{
				data.Add("RestrictedDescription", restrictedError);
				data.Add("RestrictedErrorReference", restrictedErrorReference);
				data.Add("RestrictedCapabilitySid", restrictedCapabilitySid);
				data.Add("__RestrictedErrorObject", (restrictedErrorObject == null) ? null : new Exception.__RestrictedErrorObject(restrictedErrorObject));
				data.Add("__HasRestrictedLanguageErrorObject", hasrestrictedLanguageErrorObject);
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001421C File Offset: 0x0001241C
		internal bool TryGetRestrictedLanguageErrorObject(out object restrictedErrorObject)
		{
			restrictedErrorObject = null;
			if (this.Data != null && this.Data.Contains("__HasRestrictedLanguageErrorObject"))
			{
				if (this.Data.Contains("__RestrictedErrorObject"))
				{
					Exception.__RestrictedErrorObject _RestrictedErrorObject = this.Data["__RestrictedErrorObject"] as Exception.__RestrictedErrorObject;
					if (_RestrictedErrorObject != null)
					{
						restrictedErrorObject = _RestrictedErrorObject.RealErrorObject;
					}
				}
				return (bool)this.Data["__HasRestrictedLanguageErrorObject"];
			}
			return false;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00014290 File Offset: 0x00012490
		private string GetClassName()
		{
			if (this._className == null)
			{
				this._className = this.GetType().ToString();
			}
			return this._className;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000142B4 File Offset: 0x000124B4
		[__DynamicallyInvokable]
		public virtual Exception GetBaseException()
		{
			Exception innerException = this.InnerException;
			Exception result = this;
			while (innerException != null)
			{
				result = innerException;
				innerException = innerException.InnerException;
			}
			return result;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x000142D9 File Offset: 0x000124D9
		[__DynamicallyInvokable]
		public Exception InnerException
		{
			[__DynamicallyInvokable]
			get
			{
				return this._innerException;
			}
		}

		// Token: 0x060005B8 RID: 1464
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IRuntimeMethodInfo GetMethodFromStackTrace(object stackTrace);

		// Token: 0x060005B9 RID: 1465 RVA: 0x000142E4 File Offset: 0x000124E4
		[SecuritySafeCritical]
		private MethodBase GetExceptionMethodFromStackTrace()
		{
			IRuntimeMethodInfo methodFromStackTrace = Exception.GetMethodFromStackTrace(this._stackTrace);
			if (methodFromStackTrace == null)
			{
				return null;
			}
			return RuntimeType.GetMethodBase(methodFromStackTrace);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00014308 File Offset: 0x00012508
		public MethodBase TargetSite
		{
			[SecuritySafeCritical]
			get
			{
				return this.GetTargetSiteInternal();
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00014310 File Offset: 0x00012510
		[SecurityCritical]
		private MethodBase GetTargetSiteInternal()
		{
			if (this._exceptionMethod != null)
			{
				return this._exceptionMethod;
			}
			if (this._stackTrace == null)
			{
				return null;
			}
			if (this._exceptionMethodString != null)
			{
				this._exceptionMethod = this.GetExceptionMethodFromString();
			}
			else
			{
				this._exceptionMethod = this.GetExceptionMethodFromStackTrace();
			}
			return this._exceptionMethod;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00014364 File Offset: 0x00012564
		[__DynamicallyInvokable]
		public virtual string StackTrace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetStackTrace(true);
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00014370 File Offset: 0x00012570
		private string GetStackTrace(bool needFileInfo)
		{
			string text = this._stackTraceString;
			string text2 = this._remoteStackTraceString;
			if (!needFileInfo)
			{
				text = this.StripFileInfo(text, false);
				text2 = this.StripFileInfo(text2, true);
			}
			if (text != null)
			{
				return text2 + text;
			}
			if (this._stackTrace == null)
			{
				return text2;
			}
			string stackTrace = Environment.GetStackTrace(this, needFileInfo);
			return text2 + stackTrace;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000143C4 File Offset: 0x000125C4
		[FriendAccessAllowed]
		internal void SetErrorCode(int hr)
		{
			this.HResult = hr;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x000143CD File Offset: 0x000125CD
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x000143D5 File Offset: 0x000125D5
		[__DynamicallyInvokable]
		public virtual string HelpLink
		{
			[__DynamicallyInvokable]
			get
			{
				return this._helpURL;
			}
			[__DynamicallyInvokable]
			set
			{
				this._helpURL = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x000143E0 File Offset: 0x000125E0
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x00014473 File Offset: 0x00012673
		[__DynamicallyInvokable]
		public virtual string Source
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._source == null)
				{
					StackTrace stackTrace = new StackTrace(this, true);
					if (stackTrace.FrameCount > 0)
					{
						StackFrame frame = stackTrace.GetFrame(0);
						MethodBase method = frame.GetMethod();
						Module module = method.Module;
						RuntimeModule runtimeModule = module as RuntimeModule;
						if (runtimeModule == null)
						{
							ModuleBuilder moduleBuilder = module as ModuleBuilder;
							if (!(moduleBuilder != null))
							{
								throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
							}
							runtimeModule = moduleBuilder.InternalModule;
						}
						this._source = runtimeModule.GetRuntimeAssembly().GetSimpleName();
					}
				}
				return this._source;
			}
			[__DynamicallyInvokable]
			set
			{
				this._source = value;
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001447C File Offset: 0x0001267C
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.ToString(true, true);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00014488 File Offset: 0x00012688
		private string ToString(bool needFileLineInfo, bool needMessage)
		{
			string text = needMessage ? this.Message : null;
			string text2;
			if (text == null || text.Length <= 0)
			{
				text2 = this.GetClassName();
			}
			else
			{
				text2 = this.GetClassName() + ": " + text;
			}
			if (this._innerException != null)
			{
				text2 = string.Concat(new string[]
				{
					text2,
					" ---> ",
					this._innerException.ToString(needFileLineInfo, needMessage),
					Environment.NewLine,
					"   ",
					Environment.GetResourceString("Exception_EndOfInnerExceptionStack")
				});
			}
			string stackTrace = this.GetStackTrace(needFileLineInfo);
			if (stackTrace != null)
			{
				text2 = text2 + Environment.NewLine + stackTrace;
			}
			return text2;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00014530 File Offset: 0x00012730
		[SecurityCritical]
		private string GetExceptionMethodString()
		{
			MethodBase targetSiteInternal = this.GetTargetSiteInternal();
			if (targetSiteInternal == null)
			{
				return null;
			}
			if (targetSiteInternal is DynamicMethod.RTDynamicMethod)
			{
				return null;
			}
			char value = '\n';
			StringBuilder stringBuilder = new StringBuilder();
			if (targetSiteInternal is ConstructorInfo)
			{
				RuntimeConstructorInfo runtimeConstructorInfo = (RuntimeConstructorInfo)targetSiteInternal;
				Type reflectedType = runtimeConstructorInfo.ReflectedType;
				stringBuilder.Append(1);
				stringBuilder.Append(value);
				stringBuilder.Append(runtimeConstructorInfo.Name);
				if (reflectedType != null)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(reflectedType.Assembly.FullName);
					stringBuilder.Append(value);
					stringBuilder.Append(reflectedType.FullName);
				}
				stringBuilder.Append(value);
				stringBuilder.Append(runtimeConstructorInfo.ToString());
			}
			else
			{
				RuntimeMethodInfo runtimeMethodInfo = (RuntimeMethodInfo)targetSiteInternal;
				Type declaringType = runtimeMethodInfo.DeclaringType;
				stringBuilder.Append(8);
				stringBuilder.Append(value);
				stringBuilder.Append(runtimeMethodInfo.Name);
				stringBuilder.Append(value);
				stringBuilder.Append(runtimeMethodInfo.Module.Assembly.FullName);
				stringBuilder.Append(value);
				if (declaringType != null)
				{
					stringBuilder.Append(declaringType.FullName);
					stringBuilder.Append(value);
				}
				stringBuilder.Append(runtimeMethodInfo.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001467C File Offset: 0x0001287C
		[SecurityCritical]
		private MethodBase GetExceptionMethodFromString()
		{
			string[] array = this._exceptionMethodString.Split(new char[]
			{
				'\0',
				'\n'
			});
			if (array.Length != 5)
			{
				throw new SerializationException();
			}
			SerializationInfo serializationInfo = new SerializationInfo(typeof(MemberInfoSerializationHolder), new FormatterConverter());
			serializationInfo.AddValue("MemberType", int.Parse(array[0], CultureInfo.InvariantCulture), typeof(int));
			serializationInfo.AddValue("Name", array[1], typeof(string));
			serializationInfo.AddValue("AssemblyName", array[2], typeof(string));
			serializationInfo.AddValue("ClassName", array[3]);
			serializationInfo.AddValue("Signature", array[4]);
			StreamingContext context = new StreamingContext(StreamingContextStates.All);
			MethodBase result;
			try
			{
				result = (MethodBase)new MemberInfoSerializationHolder(serializationInfo, context).GetRealObject(context);
			}
			catch (SerializationException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060005C7 RID: 1479 RVA: 0x0001476C File Offset: 0x0001296C
		// (remove) Token: 0x060005C8 RID: 1480 RVA: 0x0001477A File Offset: 0x0001297A
		protected event EventHandler<SafeSerializationEventArgs> SerializeObjectState
		{
			add
			{
				this._safeSerializationManager.SerializeObjectState += value;
			}
			remove
			{
				this._safeSerializationManager.SerializeObjectState -= value;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00014788 File Offset: 0x00012988
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			string text = this._stackTraceString;
			if (this._stackTrace != null)
			{
				if (text == null)
				{
					text = Environment.GetStackTrace(this, true);
				}
				if (this._exceptionMethod == null)
				{
					this._exceptionMethod = this.GetExceptionMethodFromStackTrace();
				}
			}
			if (this._source == null)
			{
				this._source = this.Source;
			}
			info.AddValue("ClassName", this.GetClassName(), typeof(string));
			info.AddValue("Message", this._message, typeof(string));
			info.AddValue("Data", this._data, typeof(IDictionary));
			info.AddValue("InnerException", this._innerException, typeof(Exception));
			info.AddValue("HelpURL", this._helpURL, typeof(string));
			info.AddValue("StackTraceString", text, typeof(string));
			info.AddValue("RemoteStackTraceString", this._remoteStackTraceString, typeof(string));
			info.AddValue("RemoteStackIndex", this._remoteStackIndex, typeof(int));
			info.AddValue("ExceptionMethod", this.GetExceptionMethodString(), typeof(string));
			info.AddValue("HResult", this.HResult);
			info.AddValue("Source", this._source, typeof(string));
			info.AddValue("WatsonBuckets", this._watsonBuckets, typeof(byte[]));
			if (this._safeSerializationManager != null && this._safeSerializationManager.IsActive)
			{
				info.AddValue("SafeSerializationManager", this._safeSerializationManager, typeof(SafeSerializationManager));
				this._safeSerializationManager.CompleteSerialization(this, info, context);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00014964 File Offset: 0x00012B64
		internal Exception PrepForRemoting()
		{
			string remoteStackTraceString;
			if (this._remoteStackIndex == 0)
			{
				remoteStackTraceString = string.Concat(new object[]
				{
					Environment.NewLine,
					"Server stack trace: ",
					Environment.NewLine,
					this.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Exception rethrown at [",
					this._remoteStackIndex,
					"]: ",
					Environment.NewLine
				});
			}
			else
			{
				remoteStackTraceString = string.Concat(new object[]
				{
					this.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Exception rethrown at [",
					this._remoteStackIndex,
					"]: ",
					Environment.NewLine
				});
			}
			this._remoteStackTraceString = remoteStackTraceString;
			this._remoteStackIndex++;
			return this;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00014A43 File Offset: 0x00012C43
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this._stackTrace = null;
			this._ipForWatsonBuckets = UIntPtr.Zero;
			if (this._safeSerializationManager == null)
			{
				this._safeSerializationManager = new SafeSerializationManager();
				return;
			}
			this._safeSerializationManager.CompleteDeserialization(this);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00014A78 File Offset: 0x00012C78
		internal void InternalPreserveStackTrace()
		{
			string stackTrace;
			if (AppDomain.IsAppXModel())
			{
				stackTrace = this.GetStackTrace(true);
				string source = this.Source;
			}
			else
			{
				stackTrace = this.StackTrace;
			}
			if (stackTrace != null && stackTrace.Length > 0)
			{
				this._remoteStackTraceString = stackTrace + Environment.NewLine;
			}
			this._stackTrace = null;
			this._stackTraceString = null;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00014ACF File Offset: 0x00012CCF
		internal UIntPtr IPForWatsonBuckets
		{
			get
			{
				return this._ipForWatsonBuckets;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00014AD7 File Offset: 0x00012CD7
		internal object WatsonBuckets
		{
			get
			{
				return this._watsonBuckets;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00014ADF File Offset: 0x00012CDF
		internal string RemoteStackTrace
		{
			get
			{
				return this._remoteStackTraceString;
			}
		}

		// Token: 0x060005D0 RID: 1488
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PrepareForForeignExceptionRaise();

		// Token: 0x060005D1 RID: 1489
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStackTracesDeepCopy(Exception exception, out object currentStackTrace, out object dynamicMethodArray);

		// Token: 0x060005D2 RID: 1490
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SaveStackTracesFromDeepCopy(Exception exception, object currentStackTrace, object dynamicMethodArray);

		// Token: 0x060005D3 RID: 1491
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object CopyStackTrace(object currentStackTrace);

		// Token: 0x060005D4 RID: 1492
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object CopyDynamicMethods(object currentDynamicMethods);

		// Token: 0x060005D5 RID: 1493
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string StripFileInfo(string stackTrace, bool isRemoteStackTrace);

		// Token: 0x060005D6 RID: 1494 RVA: 0x00014AE7 File Offset: 0x00012CE7
		[SecuritySafeCritical]
		internal object DeepCopyStackTrace(object currentStackTrace)
		{
			if (currentStackTrace != null)
			{
				return Exception.CopyStackTrace(currentStackTrace);
			}
			return null;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00014AF4 File Offset: 0x00012CF4
		[SecuritySafeCritical]
		internal object DeepCopyDynamicMethods(object currentDynamicMethods)
		{
			if (currentDynamicMethods != null)
			{
				return Exception.CopyDynamicMethods(currentDynamicMethods);
			}
			return null;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00014B01 File Offset: 0x00012D01
		[SecuritySafeCritical]
		internal void GetStackTracesDeepCopy(out object currentStackTrace, out object dynamicMethodArray)
		{
			Exception.GetStackTracesDeepCopy(this, out currentStackTrace, out dynamicMethodArray);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00014B0C File Offset: 0x00012D0C
		[SecuritySafeCritical]
		internal void RestoreExceptionDispatchInfo(ExceptionDispatchInfo exceptionDispatchInfo)
		{
			bool flag = !Exception.IsImmutableAgileException(this);
			if (flag)
			{
				try
				{
				}
				finally
				{
					object currentStackTrace = (exceptionDispatchInfo.BinaryStackTraceArray == null) ? null : this.DeepCopyStackTrace(exceptionDispatchInfo.BinaryStackTraceArray);
					object dynamicMethodArray = (exceptionDispatchInfo.DynamicMethodArray == null) ? null : this.DeepCopyDynamicMethods(exceptionDispatchInfo.DynamicMethodArray);
					object obj = Exception.s_EDILock;
					lock (obj)
					{
						this._watsonBuckets = exceptionDispatchInfo.WatsonBuckets;
						this._ipForWatsonBuckets = exceptionDispatchInfo.IPForWatsonBuckets;
						this._remoteStackTraceString = exceptionDispatchInfo.RemoteStackTrace;
						Exception.SaveStackTracesFromDeepCopy(this, currentStackTrace, dynamicMethodArray);
					}
					this._stackTraceString = null;
					Exception.PrepareForForeignExceptionRaise();
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00014BD4 File Offset: 0x00012DD4
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x00014BDC File Offset: 0x00012DDC
		[__DynamicallyInvokable]
		public int HResult
		{
			[__DynamicallyInvokable]
			get
			{
				return this._HResult;
			}
			[__DynamicallyInvokable]
			protected set
			{
				this._HResult = value;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00014BE8 File Offset: 0x00012DE8
		[SecurityCritical]
		internal virtual string InternalToString()
		{
			try
			{
				SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy);
				securityPermission.Assert();
			}
			catch
			{
			}
			bool needFileLineInfo = true;
			return this.ToString(needFileLineInfo, true);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00014C24 File Offset: 0x00012E24
		[__DynamicallyInvokable]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00014C2C File Offset: 0x00012E2C
		internal bool IsTransient
		{
			[SecuritySafeCritical]
			get
			{
				return Exception.nIsTransient(this._HResult);
			}
		}

		// Token: 0x060005DF RID: 1503
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool nIsTransient(int hr);

		// Token: 0x060005E0 RID: 1504 RVA: 0x00014C3C File Offset: 0x00012E3C
		[SecuritySafeCritical]
		internal static string GetMessageFromNativeResources(Exception.ExceptionMessageKind kind)
		{
			string result = null;
			Exception.GetMessageFromNativeResources(kind, JitHelpers.GetStringHandleOnStack(ref result));
			return result;
		}

		// Token: 0x060005E1 RID: 1505
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetMessageFromNativeResources(Exception.ExceptionMessageKind kind, StringHandleOnStack retMesg);

		// Token: 0x040002A3 RID: 675
		[OptionalField]
		private static object s_EDILock = new object();

		// Token: 0x040002A4 RID: 676
		private string _className;

		// Token: 0x040002A5 RID: 677
		private MethodBase _exceptionMethod;

		// Token: 0x040002A6 RID: 678
		private string _exceptionMethodString;

		// Token: 0x040002A7 RID: 679
		internal string _message;

		// Token: 0x040002A8 RID: 680
		private IDictionary _data;

		// Token: 0x040002A9 RID: 681
		private Exception _innerException;

		// Token: 0x040002AA RID: 682
		private string _helpURL;

		// Token: 0x040002AB RID: 683
		private object _stackTrace;

		// Token: 0x040002AC RID: 684
		[OptionalField]
		private object _watsonBuckets;

		// Token: 0x040002AD RID: 685
		private string _stackTraceString;

		// Token: 0x040002AE RID: 686
		private string _remoteStackTraceString;

		// Token: 0x040002AF RID: 687
		private int _remoteStackIndex;

		// Token: 0x040002B0 RID: 688
		private object _dynamicMethods;

		// Token: 0x040002B1 RID: 689
		internal int _HResult;

		// Token: 0x040002B2 RID: 690
		private string _source;

		// Token: 0x040002B3 RID: 691
		private IntPtr _xptrs;

		// Token: 0x040002B4 RID: 692
		private int _xcode;

		// Token: 0x040002B5 RID: 693
		[OptionalField]
		private UIntPtr _ipForWatsonBuckets;

		// Token: 0x040002B6 RID: 694
		[OptionalField(VersionAdded = 4)]
		private SafeSerializationManager _safeSerializationManager;

		// Token: 0x040002B7 RID: 695
		private const int _COMPlusExceptionCode = -532462766;

		// Token: 0x02000A9A RID: 2714
		[Serializable]
		internal class __RestrictedErrorObject
		{
			// Token: 0x060068AF RID: 26799 RVA: 0x0016808D File Offset: 0x0016628D
			internal __RestrictedErrorObject(object errorObject)
			{
				this._realErrorObject = errorObject;
			}

			// Token: 0x170011D6 RID: 4566
			// (get) Token: 0x060068B0 RID: 26800 RVA: 0x0016809C File Offset: 0x0016629C
			public object RealErrorObject
			{
				get
				{
					return this._realErrorObject;
				}
			}

			// Token: 0x04003009 RID: 12297
			[NonSerialized]
			private object _realErrorObject;
		}

		// Token: 0x02000A9B RID: 2715
		internal enum ExceptionMessageKind
		{
			// Token: 0x0400300B RID: 12299
			ThreadAbort = 1,
			// Token: 0x0400300C RID: 12300
			ThreadInterrupted,
			// Token: 0x0400300D RID: 12301
			OutOfMemory
		}
	}
}
