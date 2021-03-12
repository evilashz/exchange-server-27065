using System;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x0200078B RID: 1931
	[Serializable]
	internal class TypeInfo : IRemotingTypeInfo
	{
		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x0600546C RID: 21612 RVA: 0x0012B2B8 File Offset: 0x001294B8
		// (set) Token: 0x0600546D RID: 21613 RVA: 0x0012B2C0 File Offset: 0x001294C0
		public virtual string TypeName
		{
			[SecurityCritical]
			get
			{
				return this.serverType;
			}
			[SecurityCritical]
			set
			{
				this.serverType = value;
			}
		}

		// Token: 0x0600546E RID: 21614 RVA: 0x0012B2CC File Offset: 0x001294CC
		[SecurityCritical]
		public virtual bool CanCastTo(Type castType, object o)
		{
			if (null != castType)
			{
				if (castType == typeof(MarshalByRefObject) || castType == typeof(object))
				{
					return true;
				}
				if (castType.IsInterface)
				{
					return this.interfacesImplemented != null && this.CanCastTo(castType, this.InterfacesImplemented);
				}
				if (castType.IsMarshalByRef)
				{
					if (this.CompareTypes(castType, this.serverType))
					{
						return true;
					}
					if (this.serverHierarchy != null && this.CanCastTo(castType, this.ServerHierarchy))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600546F RID: 21615 RVA: 0x0012B35B File Offset: 0x0012955B
		[SecurityCritical]
		internal static string GetQualifiedTypeName(RuntimeType type)
		{
			if (type == null)
			{
				return null;
			}
			return RemotingServices.GetDefaultQualifiedTypeName(type);
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x0012B370 File Offset: 0x00129570
		internal static bool ParseTypeAndAssembly(string typeAndAssembly, out string typeName, out string assemName)
		{
			if (typeAndAssembly == null)
			{
				typeName = null;
				assemName = null;
				return false;
			}
			int num = typeAndAssembly.IndexOf(',');
			if (num == -1)
			{
				typeName = typeAndAssembly;
				assemName = null;
				return true;
			}
			typeName = typeAndAssembly.Substring(0, num);
			assemName = typeAndAssembly.Substring(num + 1).Trim();
			return true;
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x0012B3B8 File Offset: 0x001295B8
		[SecurityCritical]
		internal TypeInfo(RuntimeType typeOfObj)
		{
			this.ServerType = TypeInfo.GetQualifiedTypeName(typeOfObj);
			RuntimeType runtimeType = (RuntimeType)typeOfObj.BaseType;
			int num = 0;
			while (runtimeType != typeof(MarshalByRefObject) && runtimeType != null)
			{
				runtimeType = (RuntimeType)runtimeType.BaseType;
				num++;
			}
			string[] array = null;
			if (num > 0)
			{
				array = new string[num];
				runtimeType = (RuntimeType)typeOfObj.BaseType;
				for (int i = 0; i < num; i++)
				{
					array[i] = TypeInfo.GetQualifiedTypeName(runtimeType);
					runtimeType = (RuntimeType)runtimeType.BaseType;
				}
			}
			this.ServerHierarchy = array;
			Type[] interfaces = typeOfObj.GetInterfaces();
			string[] array2 = null;
			bool isInterface = typeOfObj.IsInterface;
			if (interfaces.Length != 0 || isInterface)
			{
				array2 = new string[interfaces.Length + (isInterface ? 1 : 0)];
				for (int j = 0; j < interfaces.Length; j++)
				{
					array2[j] = TypeInfo.GetQualifiedTypeName((RuntimeType)interfaces[j]);
				}
				if (isInterface)
				{
					array2[array2.Length - 1] = TypeInfo.GetQualifiedTypeName(typeOfObj);
				}
			}
			this.InterfacesImplemented = array2;
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06005472 RID: 21618 RVA: 0x0012B4C7 File Offset: 0x001296C7
		// (set) Token: 0x06005473 RID: 21619 RVA: 0x0012B4CF File Offset: 0x001296CF
		internal string ServerType
		{
			get
			{
				return this.serverType;
			}
			set
			{
				this.serverType = value;
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06005474 RID: 21620 RVA: 0x0012B4D8 File Offset: 0x001296D8
		// (set) Token: 0x06005475 RID: 21621 RVA: 0x0012B4E0 File Offset: 0x001296E0
		private string[] ServerHierarchy
		{
			get
			{
				return this.serverHierarchy;
			}
			set
			{
				this.serverHierarchy = value;
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06005476 RID: 21622 RVA: 0x0012B4E9 File Offset: 0x001296E9
		// (set) Token: 0x06005477 RID: 21623 RVA: 0x0012B4F1 File Offset: 0x001296F1
		private string[] InterfacesImplemented
		{
			get
			{
				return this.interfacesImplemented;
			}
			set
			{
				this.interfacesImplemented = value;
			}
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x0012B4FC File Offset: 0x001296FC
		[SecurityCritical]
		private bool CompareTypes(Type type1, string type2)
		{
			Type right = RemotingServices.InternalGetTypeFromQualifiedTypeName(type2);
			return type1 == right;
		}

		// Token: 0x06005479 RID: 21625 RVA: 0x0012B518 File Offset: 0x00129718
		[SecurityCritical]
		private bool CanCastTo(Type castType, string[] types)
		{
			bool result = false;
			if (null != castType)
			{
				for (int i = 0; i < types.Length; i++)
				{
					if (this.CompareTypes(castType, types[i]))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x040026AA RID: 9898
		private string serverType;

		// Token: 0x040026AB RID: 9899
		private string[] serverHierarchy;

		// Token: 0x040026AC RID: 9900
		private string[] interfacesImplemented;
	}
}
