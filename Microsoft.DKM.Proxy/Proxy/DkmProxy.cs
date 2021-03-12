using System;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using Microsoft.Win32;

namespace Microsoft.Exchange.Security.Dkm.Proxy
{
	// Token: 0x02000003 RID: 3
	internal sealed class DkmProxy
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public DkmProxy(string groupName, string dkmPath = null, string dkmParentContainerDN = null)
		{
			if (DkmProxy.dkmAssembly == null)
			{
				if (!string.IsNullOrEmpty(dkmPath))
				{
					DkmProxy.dkmAssembly = Assembly.LoadFrom(Path.Combine(dkmPath, "Microsoft.Cryptography.DKM.dll"));
				}
				else
				{
					try
					{
						DkmProxy.dkmAssembly = Assembly.LoadFrom("Microsoft.Cryptography.DKM.dll");
					}
					catch (FileNotFoundException)
					{
						DkmProxy.dkmAssembly = Assembly.LoadFrom(Path.Combine(DkmProxy.GetExchangeInstallPath(), string.Format("Bin\\{0}", "Microsoft.Cryptography.DKM.dll")));
					}
				}
			}
			if (DkmProxy.typeGroupKey == null)
			{
				DkmProxy.typeGroupKey = DkmProxy.dkmAssembly.GetType("Microsoft.Incubation.Crypto.GroupKeys.GroupKey");
			}
			if (DkmProxy.typeDkmException == null)
			{
				DkmProxy.typeDkmException = DkmProxy.dkmAssembly.GetType("Microsoft.Incubation.Crypto.GroupKeys.DkmException");
			}
			if (DkmProxy.dkmObjectField == null)
			{
				DkmProxy.dkmObjectField = typeof(DkmProxy).GetField("instanceDkm", BindingFlags.Instance | BindingFlags.NonPublic);
			}
			if (DkmProxy.typeIADRepository == null)
			{
				DkmProxy.typeIADRepository = DkmProxy.dkmAssembly.GetType("Microsoft.Incubation.Crypto.GroupKeys.IADRepository");
			}
			object[] array = new object[2];
			array[0] = groupName;
			object[] args = array;
			this.instanceDkm = Activator.CreateInstance(DkmProxy.typeGroupKey, args);
			PropertyInfo property = DkmProxy.typeGroupKey.GetProperty("Repository", DkmProxy.typeIADRepository);
			this.encryptDelegate = (DkmProxy.EncryptDecryptDelegate)this.GetMethodDelegate(DkmProxy.typeGroupKey, "Protect", new Type[]
			{
				typeof(MemoryStream)
			}, typeof(DkmProxy.EncryptDecryptDelegate));
			this.decryptDelegate = (DkmProxy.EncryptDecryptDelegate)this.GetMethodDelegate(DkmProxy.typeGroupKey, "Unprotect", new Type[]
			{
				typeof(MemoryStream)
			}, typeof(DkmProxy.EncryptDecryptDelegate));
			this.instanceRepository = property.GetValue(this.instanceDkm, null);
			if (!string.IsNullOrEmpty(dkmParentContainerDN))
			{
				this.DkmParentContainerDN = dkmParentContainerDN;
			}
		}

		// Token: 0x17000001 RID: 1
		// (set) Token: 0x06000002 RID: 2 RVA: 0x000022B0 File Offset: 0x000004B0
		public string PreferredReplicaName
		{
			set
			{
				PropertyInfo propertyInfoOrFail = this.GetPropertyInfoOrFail(DkmProxy.typeIADRepository, "PreferredReplicaName");
				propertyInfoOrFail.SetValue(this.instanceRepository, value, null);
			}
		}

		// Token: 0x17000002 RID: 2
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000022DC File Offset: 0x000004DC
		public string DkmParentContainerDN
		{
			set
			{
				PropertyInfo propertyInfoOrFail = this.GetPropertyInfoOrFail(DkmProxy.typeIADRepository, "DkmParentContainerDN");
				propertyInfoOrFail.SetValue(this.instanceRepository, value, null);
			}
		}

		// Token: 0x17000003 RID: 3
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002308 File Offset: 0x00000508
		public string DkmContainerName
		{
			set
			{
				PropertyInfo propertyInfoOrFail = this.GetPropertyInfoOrFail(DkmProxy.typeIADRepository, "DkmContainerName");
				propertyInfoOrFail.SetValue(this.instanceRepository, value, null);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002334 File Offset: 0x00000534
		public void InitializeDkm()
		{
			MethodInfo methodInfoOrFail = this.GetMethodInfoOrFail(DkmProxy.typeGroupKey, "InitializeDkm", new Type[0]);
			try
			{
				methodInfoOrFail.Invoke(this.instanceDkm, null);
			}
			catch (TargetInvocationException ex)
			{
				if (ex.InnerException.GetType().Name == "ObjectAlreadyExistsException")
				{
					throw new ObjectAlreadyExistsException(ex.InnerException.Message);
				}
				throw;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000023A8 File Offset: 0x000005A8
		public void AddGroup()
		{
			MethodInfo methodInfoOrFail = this.GetMethodInfoOrFail(DkmProxy.typeGroupKey, "AddGroup", new Type[0]);
			try
			{
				methodInfoOrFail.Invoke(this.instanceDkm, null);
			}
			catch (TargetInvocationException ex)
			{
				if (ex.InnerException.GetType().Name == "ObjectAlreadyExistsException")
				{
					throw new ObjectAlreadyExistsException(ex.InnerException.Message);
				}
				throw;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000241C File Offset: 0x0000061C
		public void AddGroupMemberWithUpdateRights(IdentityReference identity)
		{
			object[] parameters = new object[]
			{
				identity
			};
			MethodInfo methodInfoOrFail = this.GetMethodInfoOrFail(this.instanceRepository.GetType(), "AddGroupMemberWithUpdateRights", new Type[]
			{
				typeof(IdentityReference)
			});
			methodInfoOrFail.Invoke(this.instanceRepository, parameters);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002470 File Offset: 0x00000670
		public void UninitializeDkm()
		{
			MethodInfo methodInfoOrFail = this.GetMethodInfoOrFail(DkmProxy.typeGroupKey, "UninitializeDkm", new Type[0]);
			try
			{
				methodInfoOrFail.Invoke(this.instanceDkm, null);
			}
			catch (TargetInvocationException ex)
			{
				if (ex.InnerException.GetType().Name == "ObjectNotFoundException")
				{
					throw new ObjectNotFoundException(ex.InnerException.Message);
				}
				throw;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000024E4 File Offset: 0x000006E4
		public bool IsDkmException(Exception ex)
		{
			return DkmProxy.typeDkmException.IsAssignableFrom(ex.GetType());
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024F8 File Offset: 0x000006F8
		public void AddGroupOwner(IdentityReference identity)
		{
			object[] parameters = new object[]
			{
				identity
			};
			MethodInfo methodInfoOrFail = this.GetMethodInfoOrFail(this.instanceRepository.GetType(), "AddGroupOwner", new Type[]
			{
				typeof(IdentityReference)
			});
			methodInfoOrFail.Invoke(this.instanceRepository, parameters);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000254C File Offset: 0x0000074C
		public void UpdateKey(bool forcedUpdate)
		{
			object[] parameters = new object[]
			{
				forcedUpdate
			};
			MethodInfo methodInfoOrFail = this.GetMethodInfoOrFail(DkmProxy.typeGroupKey, "UpdateKey", new Type[]
			{
				typeof(bool)
			});
			methodInfoOrFail.Invoke(this.instanceDkm, parameters);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000259F File Offset: 0x0000079F
		public MemoryStream Protect(MemoryStream inStream)
		{
			return this.encryptDelegate(inStream);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000025AD File Offset: 0x000007AD
		public MemoryStream Unprotect(MemoryStream inStream)
		{
			return this.decryptDelegate(inStream);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000025BC File Offset: 0x000007BC
		private static string GetExchangeInstallPath()
		{
			return (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiInstallPath", null);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000025E0 File Offset: 0x000007E0
		private PropertyInfo GetPropertyInfoOrFail(Type type, string propName)
		{
			PropertyInfo property = type.GetProperty(propName);
			if (property == null)
			{
				throw new InvalidOperationException();
			}
			return property;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002608 File Offset: 0x00000808
		private MethodInfo GetMethodInfoOrFail(Type type, string methodName, Type[] argTypes)
		{
			MethodInfo method = type.GetMethod(methodName, argTypes);
			if (method == null)
			{
				throw new InvalidOperationException();
			}
			return method;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002630 File Offset: 0x00000830
		private Delegate GetMethodDelegate(Type methodType, string methodName, Type[] methodParamTypes, Type delegateType)
		{
			MethodInfo method = methodType.GetMethod(methodName, methodParamTypes);
			return Delegate.CreateDelegate(delegateType, this.instanceDkm, method);
		}

		// Token: 0x04000005 RID: 5
		private const string AssemblyName = "Microsoft.Cryptography.DKM.dll";

		// Token: 0x04000006 RID: 6
		private const string DkmTypeFullName = "Microsoft.Incubation.Crypto.GroupKeys.GroupKey";

		// Token: 0x04000007 RID: 7
		private const string DkmExceptionTypeFullName = "Microsoft.Incubation.Crypto.GroupKeys.DkmException";

		// Token: 0x04000008 RID: 8
		private const string DkmRespositoryTypeFullName = "Microsoft.Incubation.Crypto.GroupKeys.IADRepository";

		// Token: 0x04000009 RID: 9
		private static Assembly dkmAssembly;

		// Token: 0x0400000A RID: 10
		private static Type typeGroupKey;

		// Token: 0x0400000B RID: 11
		private static Type typeDkmException;

		// Token: 0x0400000C RID: 12
		private static FieldInfo dkmObjectField;

		// Token: 0x0400000D RID: 13
		private static Type typeIADRepository;

		// Token: 0x0400000E RID: 14
		private readonly object instanceDkm;

		// Token: 0x0400000F RID: 15
		private readonly object instanceRepository;

		// Token: 0x04000010 RID: 16
		private readonly DkmProxy.EncryptDecryptDelegate encryptDelegate;

		// Token: 0x04000011 RID: 17
		private readonly DkmProxy.EncryptDecryptDelegate decryptDelegate;

		// Token: 0x02000004 RID: 4
		// (Invoke) Token: 0x06000013 RID: 19
		private delegate MemoryStream EncryptDecryptDelegate(MemoryStream memstream);
	}
}
