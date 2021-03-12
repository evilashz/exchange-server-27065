using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007A6 RID: 1958
	internal class RemotingTypeCachedData : RemotingCachedData
	{
		// Token: 0x060055B3 RID: 21939 RVA: 0x0012F9A3 File Offset: 0x0012DBA3
		internal RemotingTypeCachedData(RuntimeType ri)
		{
			this.RI = ri;
		}

		// Token: 0x060055B4 RID: 21940 RVA: 0x0012F9B4 File Offset: 0x0012DBB4
		internal override SoapAttribute GetSoapAttributeNoLock()
		{
			object[] customAttributes = this.RI.GetCustomAttributes(typeof(SoapTypeAttribute), true);
			SoapAttribute soapAttribute;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				soapAttribute = (SoapAttribute)customAttributes[0];
			}
			else
			{
				soapAttribute = new SoapTypeAttribute();
			}
			soapAttribute.SetReflectInfo(this.RI);
			return soapAttribute;
		}

		// Token: 0x060055B5 RID: 21941 RVA: 0x0012FA00 File Offset: 0x0012DC00
		internal MethodBase GetLastCalledMethod(string newMeth)
		{
			RemotingTypeCachedData.LastCalledMethodClass lastMethodCalled = this._lastMethodCalled;
			if (lastMethodCalled == null)
			{
				return null;
			}
			string methodName = lastMethodCalled.methodName;
			MethodBase mb = lastMethodCalled.MB;
			if (mb == null || methodName == null)
			{
				return null;
			}
			if (methodName.Equals(newMeth))
			{
				return mb;
			}
			return null;
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x0012FA44 File Offset: 0x0012DC44
		internal void SetLastCalledMethod(string newMethName, MethodBase newMB)
		{
			this._lastMethodCalled = new RemotingTypeCachedData.LastCalledMethodClass
			{
				methodName = newMethName,
				MB = newMB
			};
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x060055B7 RID: 21943 RVA: 0x0012FA6C File Offset: 0x0012DC6C
		internal TypeInfo TypeInfo
		{
			[SecurityCritical]
			get
			{
				if (this._typeInfo == null)
				{
					this._typeInfo = new TypeInfo(this.RI);
				}
				return this._typeInfo;
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x060055B8 RID: 21944 RVA: 0x0012FA8D File Offset: 0x0012DC8D
		internal string QualifiedTypeName
		{
			[SecurityCritical]
			get
			{
				if (this._qualifiedTypeName == null)
				{
					this._qualifiedTypeName = RemotingServices.DetermineDefaultQualifiedTypeName(this.RI);
				}
				return this._qualifiedTypeName;
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x060055B9 RID: 21945 RVA: 0x0012FAAE File Offset: 0x0012DCAE
		internal string AssemblyName
		{
			get
			{
				if (this._assemblyName == null)
				{
					this._assemblyName = this.RI.Module.Assembly.FullName;
				}
				return this._assemblyName;
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x060055BA RID: 21946 RVA: 0x0012FAD9 File Offset: 0x0012DCD9
		internal string SimpleAssemblyName
		{
			[SecurityCritical]
			get
			{
				if (this._simpleAssemblyName == null)
				{
					this._simpleAssemblyName = this.RI.GetRuntimeAssembly().GetSimpleName();
				}
				return this._simpleAssemblyName;
			}
		}

		// Token: 0x040026FD RID: 9981
		private RuntimeType RI;

		// Token: 0x040026FE RID: 9982
		private RemotingTypeCachedData.LastCalledMethodClass _lastMethodCalled;

		// Token: 0x040026FF RID: 9983
		private TypeInfo _typeInfo;

		// Token: 0x04002700 RID: 9984
		private string _qualifiedTypeName;

		// Token: 0x04002701 RID: 9985
		private string _assemblyName;

		// Token: 0x04002702 RID: 9986
		private string _simpleAssemblyName;

		// Token: 0x02000C39 RID: 3129
		private class LastCalledMethodClass
		{
			// Token: 0x040036FE RID: 14078
			public string methodName;

			// Token: 0x040036FF RID: 14079
			public MethodBase MB;
		}
	}
}
