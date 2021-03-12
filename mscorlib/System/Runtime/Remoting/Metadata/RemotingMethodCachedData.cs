using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007A7 RID: 1959
	internal class RemotingMethodCachedData : RemotingCachedData
	{
		// Token: 0x060055BB RID: 21947 RVA: 0x0012FAFF File Offset: 0x0012DCFF
		internal RemotingMethodCachedData(RuntimeMethodInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x0012FB0E File Offset: 0x0012DD0E
		internal RemotingMethodCachedData(RuntimeConstructorInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x0012FB20 File Offset: 0x0012DD20
		internal override SoapAttribute GetSoapAttributeNoLock()
		{
			object[] customAttributes = this.RI.GetCustomAttributes(typeof(SoapMethodAttribute), true);
			SoapAttribute soapAttribute;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				soapAttribute = (SoapAttribute)customAttributes[0];
			}
			else
			{
				soapAttribute = new SoapMethodAttribute();
			}
			soapAttribute.SetReflectInfo(this.RI);
			return soapAttribute;
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060055BE RID: 21950 RVA: 0x0012FB6B File Offset: 0x0012DD6B
		internal string TypeAndAssemblyName
		{
			[SecurityCritical]
			get
			{
				if (this._typeAndAssemblyName == null)
				{
					this.UpdateNames();
				}
				return this._typeAndAssemblyName;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060055BF RID: 21951 RVA: 0x0012FB81 File Offset: 0x0012DD81
		internal string MethodName
		{
			[SecurityCritical]
			get
			{
				if (this._methodName == null)
				{
					this.UpdateNames();
				}
				return this._methodName;
			}
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x0012FB98 File Offset: 0x0012DD98
		[SecurityCritical]
		private void UpdateNames()
		{
			MethodBase ri = this.RI;
			this._methodName = ri.Name;
			if (ri.DeclaringType != null)
			{
				this._typeAndAssemblyName = RemotingServices.GetDefaultQualifiedTypeName((RuntimeType)ri.DeclaringType);
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x060055C1 RID: 21953 RVA: 0x0012FBDC File Offset: 0x0012DDDC
		internal ParameterInfo[] Parameters
		{
			get
			{
				if (this._parameters == null)
				{
					this._parameters = this.RI.GetParameters();
				}
				return this._parameters;
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060055C2 RID: 21954 RVA: 0x0012FBFD File Offset: 0x0012DDFD
		internal int[] OutRefArgMap
		{
			get
			{
				if (this._outRefArgMap == null)
				{
					this.GetArgMaps();
				}
				return this._outRefArgMap;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060055C3 RID: 21955 RVA: 0x0012FC13 File Offset: 0x0012DE13
		internal int[] OutOnlyArgMap
		{
			get
			{
				if (this._outOnlyArgMap == null)
				{
					this.GetArgMaps();
				}
				return this._outOnlyArgMap;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060055C4 RID: 21956 RVA: 0x0012FC29 File Offset: 0x0012DE29
		internal int[] NonRefOutArgMap
		{
			get
			{
				if (this._nonRefOutArgMap == null)
				{
					this.GetArgMaps();
				}
				return this._nonRefOutArgMap;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060055C5 RID: 21957 RVA: 0x0012FC3F File Offset: 0x0012DE3F
		internal int[] MarshalRequestArgMap
		{
			get
			{
				if (this._marshalRequestMap == null)
				{
					this.GetArgMaps();
				}
				return this._marshalRequestMap;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060055C6 RID: 21958 RVA: 0x0012FC55 File Offset: 0x0012DE55
		internal int[] MarshalResponseArgMap
		{
			get
			{
				if (this._marshalResponseMap == null)
				{
					this.GetArgMaps();
				}
				return this._marshalResponseMap;
			}
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x0012FC6C File Offset: 0x0012DE6C
		private void GetArgMaps()
		{
			lock (this)
			{
				if (this._inRefArgMap == null)
				{
					int[] inRefArgMap = null;
					int[] outRefArgMap = null;
					int[] outOnlyArgMap = null;
					int[] nonRefOutArgMap = null;
					int[] marshalRequestMap = null;
					int[] marshalResponseMap = null;
					ArgMapper.GetParameterMaps(this.Parameters, out inRefArgMap, out outRefArgMap, out outOnlyArgMap, out nonRefOutArgMap, out marshalRequestMap, out marshalResponseMap);
					this._inRefArgMap = inRefArgMap;
					this._outRefArgMap = outRefArgMap;
					this._outOnlyArgMap = outOnlyArgMap;
					this._nonRefOutArgMap = nonRefOutArgMap;
					this._marshalRequestMap = marshalRequestMap;
					this._marshalResponseMap = marshalResponseMap;
				}
			}
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x0012FD00 File Offset: 0x0012DF00
		internal bool IsOneWayMethod()
		{
			if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedOneWay) == RemotingMethodCachedData.MethodCacheFlags.None)
			{
				RemotingMethodCachedData.MethodCacheFlags methodCacheFlags = RemotingMethodCachedData.MethodCacheFlags.CheckedOneWay;
				object[] customAttributes = this.RI.GetCustomAttributes(typeof(OneWayAttribute), true);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOneWay;
				}
				this.flags |= methodCacheFlags;
				return (methodCacheFlags & RemotingMethodCachedData.MethodCacheFlags.IsOneWay) > RemotingMethodCachedData.MethodCacheFlags.None;
			}
			return (this.flags & RemotingMethodCachedData.MethodCacheFlags.IsOneWay) > RemotingMethodCachedData.MethodCacheFlags.None;
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x0012FD5C File Offset: 0x0012DF5C
		internal bool IsOverloaded()
		{
			if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedOverloaded) == RemotingMethodCachedData.MethodCacheFlags.None)
			{
				RemotingMethodCachedData.MethodCacheFlags methodCacheFlags = RemotingMethodCachedData.MethodCacheFlags.CheckedOverloaded;
				MethodBase ri = this.RI;
				RuntimeMethodInfo runtimeMethodInfo;
				if ((runtimeMethodInfo = (ri as RuntimeMethodInfo)) != null)
				{
					if (runtimeMethodInfo.IsOverloaded)
					{
						methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOverloaded;
					}
				}
				else
				{
					RuntimeConstructorInfo runtimeConstructorInfo;
					if (!((runtimeConstructorInfo = (ri as RuntimeConstructorInfo)) != null))
					{
						throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_Method"));
					}
					if (runtimeConstructorInfo.IsOverloaded)
					{
						methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOverloaded;
					}
				}
				this.flags |= methodCacheFlags;
			}
			return (this.flags & RemotingMethodCachedData.MethodCacheFlags.IsOverloaded) > RemotingMethodCachedData.MethodCacheFlags.None;
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x060055CA RID: 21962 RVA: 0x0012FDE8 File Offset: 0x0012DFE8
		internal Type ReturnType
		{
			get
			{
				if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedForReturnType) == RemotingMethodCachedData.MethodCacheFlags.None)
				{
					MethodInfo methodInfo = this.RI as MethodInfo;
					if (methodInfo != null)
					{
						Type returnType = methodInfo.ReturnType;
						if (returnType != typeof(void))
						{
							this._returnType = returnType;
						}
					}
					this.flags |= RemotingMethodCachedData.MethodCacheFlags.CheckedForReturnType;
				}
				return this._returnType;
			}
		}

		// Token: 0x04002703 RID: 9987
		private MethodBase RI;

		// Token: 0x04002704 RID: 9988
		private ParameterInfo[] _parameters;

		// Token: 0x04002705 RID: 9989
		private RemotingMethodCachedData.MethodCacheFlags flags;

		// Token: 0x04002706 RID: 9990
		private string _typeAndAssemblyName;

		// Token: 0x04002707 RID: 9991
		private string _methodName;

		// Token: 0x04002708 RID: 9992
		private Type _returnType;

		// Token: 0x04002709 RID: 9993
		private int[] _inRefArgMap;

		// Token: 0x0400270A RID: 9994
		private int[] _outRefArgMap;

		// Token: 0x0400270B RID: 9995
		private int[] _outOnlyArgMap;

		// Token: 0x0400270C RID: 9996
		private int[] _nonRefOutArgMap;

		// Token: 0x0400270D RID: 9997
		private int[] _marshalRequestMap;

		// Token: 0x0400270E RID: 9998
		private int[] _marshalResponseMap;

		// Token: 0x02000C3A RID: 3130
		[Flags]
		[Serializable]
		private enum MethodCacheFlags
		{
			// Token: 0x04003701 RID: 14081
			None = 0,
			// Token: 0x04003702 RID: 14082
			CheckedOneWay = 1,
			// Token: 0x04003703 RID: 14083
			IsOneWay = 2,
			// Token: 0x04003704 RID: 14084
			CheckedOverloaded = 4,
			// Token: 0x04003705 RID: 14085
			IsOverloaded = 8,
			// Token: 0x04003706 RID: 14086
			CheckedForAsync = 16,
			// Token: 0x04003707 RID: 14087
			CheckedForReturnType = 32
		}
	}
}
