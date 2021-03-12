using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System
{
	// Token: 0x020000D5 RID: 213
	[Serializable]
	internal sealed class DelegateSerializationHolder : IObjectReference, ISerializable
	{
		// Token: 0x06000DA7 RID: 3495 RVA: 0x00029F0C File Offset: 0x0002810C
		[SecurityCritical]
		internal static DelegateSerializationHolder.DelegateEntry GetDelegateSerializationInfo(SerializationInfo info, Type delegateType, object target, MethodInfo method, int targetIndex)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (!method.IsPublic || (method.DeclaringType != null && !method.DeclaringType.IsVisible))
			{
				new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
			}
			Type baseType = delegateType.BaseType;
			if (baseType == null || (baseType != typeof(Delegate) && baseType != typeof(MulticastDelegate)))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
			}
			if (method.DeclaringType == null)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GlobalMethodSerialization"));
			}
			DelegateSerializationHolder.DelegateEntry delegateEntry = new DelegateSerializationHolder.DelegateEntry(delegateType.FullName, delegateType.Module.Assembly.FullName, target, method.ReflectedType.Module.Assembly.FullName, method.ReflectedType.FullName, method.Name);
			if (info.MemberCount == 0)
			{
				info.SetType(typeof(DelegateSerializationHolder));
				info.AddValue("Delegate", delegateEntry, typeof(DelegateSerializationHolder.DelegateEntry));
			}
			if (target != null)
			{
				string text = "target" + targetIndex;
				info.AddValue(text, delegateEntry.target);
				delegateEntry.target = text;
			}
			string name = "method" + targetIndex;
			info.AddValue(name, method);
			return delegateEntry;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0002A078 File Offset: 0x00028278
		[SecurityCritical]
		private DelegateSerializationHolder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			bool flag = true;
			try
			{
				this.m_delegateEntry = (DelegateSerializationHolder.DelegateEntry)info.GetValue("Delegate", typeof(DelegateSerializationHolder.DelegateEntry));
			}
			catch
			{
				this.m_delegateEntry = this.OldDelegateWireFormat(info, context);
				flag = false;
			}
			if (flag)
			{
				DelegateSerializationHolder.DelegateEntry delegateEntry = this.m_delegateEntry;
				int num = 0;
				while (delegateEntry != null)
				{
					if (delegateEntry.target != null)
					{
						string text = delegateEntry.target as string;
						if (text != null)
						{
							delegateEntry.target = info.GetValue(text, typeof(object));
						}
					}
					num++;
					delegateEntry = delegateEntry.delegateEntry;
				}
				MethodInfo[] array = new MethodInfo[num];
				int i;
				for (i = 0; i < num; i++)
				{
					string name = "method" + i;
					array[i] = (MethodInfo)info.GetValueNoThrow(name, typeof(MethodInfo));
					if (array[i] == null)
					{
						break;
					}
				}
				if (i == num)
				{
					this.m_methods = array;
				}
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0002A190 File Offset: 0x00028390
		private void ThrowInsufficientState(string field)
		{
			throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientDeserializationState", new object[]
			{
				field
			}));
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0002A1AC File Offset: 0x000283AC
		private DelegateSerializationHolder.DelegateEntry OldDelegateWireFormat(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			string @string = info.GetString("DelegateType");
			string string2 = info.GetString("DelegateAssembly");
			object value = info.GetValue("Target", typeof(object));
			string string3 = info.GetString("TargetTypeAssembly");
			string string4 = info.GetString("TargetTypeName");
			string string5 = info.GetString("MethodName");
			return new DelegateSerializationHolder.DelegateEntry(@string, string2, value, string3, string4, string5);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0002A228 File Offset: 0x00028428
		[SecurityCritical]
		private Delegate GetDelegate(DelegateSerializationHolder.DelegateEntry de, int index)
		{
			Delegate @delegate;
			try
			{
				if (de.methodName == null || de.methodName.Length == 0)
				{
					this.ThrowInsufficientState("MethodName");
				}
				if (de.assembly == null || de.assembly.Length == 0)
				{
					this.ThrowInsufficientState("DelegateAssembly");
				}
				if (de.targetTypeName == null || de.targetTypeName.Length == 0)
				{
					this.ThrowInsufficientState("TargetTypeName");
				}
				RuntimeType type = (RuntimeType)Assembly.GetType_Compat(de.assembly, de.type);
				RuntimeType runtimeType = (RuntimeType)Assembly.GetType_Compat(de.targetTypeAssembly, de.targetTypeName);
				if (this.m_methods != null)
				{
					object firstArgument = (de.target != null) ? RemotingServices.CheckCast(de.target, runtimeType) : null;
					@delegate = Delegate.CreateDelegateNoSecurityCheck(type, firstArgument, this.m_methods[index]);
				}
				else if (de.target != null)
				{
					@delegate = Delegate.CreateDelegate(type, RemotingServices.CheckCast(de.target, runtimeType), de.methodName);
				}
				else
				{
					@delegate = Delegate.CreateDelegate(type, runtimeType, de.methodName);
				}
				if ((@delegate.Method != null && !@delegate.Method.IsPublic) || (@delegate.Method.DeclaringType != null && !@delegate.Method.DeclaringType.IsVisible))
				{
					new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
				}
			}
			catch (Exception ex)
			{
				if (ex is SerializationException)
				{
					throw ex;
				}
				throw new SerializationException(ex.Message, ex);
			}
			return @delegate;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0002A3B0 File Offset: 0x000285B0
		[SecurityCritical]
		public object GetRealObject(StreamingContext context)
		{
			int num = 0;
			for (DelegateSerializationHolder.DelegateEntry delegateEntry = this.m_delegateEntry; delegateEntry != null; delegateEntry = delegateEntry.Entry)
			{
				num++;
			}
			int num2 = num - 1;
			if (num == 1)
			{
				return this.GetDelegate(this.m_delegateEntry, 0);
			}
			object[] array = new object[num];
			for (DelegateSerializationHolder.DelegateEntry delegateEntry2 = this.m_delegateEntry; delegateEntry2 != null; delegateEntry2 = delegateEntry2.Entry)
			{
				num--;
				array[num] = this.GetDelegate(delegateEntry2, num2 - num);
			}
			return ((MulticastDelegate)array[0]).NewMulticastDelegate(array, array.Length);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0002A42D File Offset: 0x0002862D
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DelegateSerHolderSerial"));
		}

		// Token: 0x0400055D RID: 1373
		private DelegateSerializationHolder.DelegateEntry m_delegateEntry;

		// Token: 0x0400055E RID: 1374
		private MethodInfo[] m_methods;

		// Token: 0x02000AB3 RID: 2739
		[Serializable]
		internal class DelegateEntry
		{
			// Token: 0x060068DA RID: 26842 RVA: 0x001683B5 File Offset: 0x001665B5
			internal DelegateEntry(string type, string assembly, object target, string targetTypeAssembly, string targetTypeName, string methodName)
			{
				this.type = type;
				this.assembly = assembly;
				this.target = target;
				this.targetTypeAssembly = targetTypeAssembly;
				this.targetTypeName = targetTypeName;
				this.methodName = methodName;
			}

			// Token: 0x170011D7 RID: 4567
			// (get) Token: 0x060068DB RID: 26843 RVA: 0x001683EA File Offset: 0x001665EA
			// (set) Token: 0x060068DC RID: 26844 RVA: 0x001683F2 File Offset: 0x001665F2
			internal DelegateSerializationHolder.DelegateEntry Entry
			{
				get
				{
					return this.delegateEntry;
				}
				set
				{
					this.delegateEntry = value;
				}
			}

			// Token: 0x04003059 RID: 12377
			internal string type;

			// Token: 0x0400305A RID: 12378
			internal string assembly;

			// Token: 0x0400305B RID: 12379
			internal object target;

			// Token: 0x0400305C RID: 12380
			internal string targetTypeAssembly;

			// Token: 0x0400305D RID: 12381
			internal string targetTypeName;

			// Token: 0x0400305E RID: 12382
			internal string methodName;

			// Token: 0x0400305F RID: 12383
			internal DelegateSerializationHolder.DelegateEntry delegateEntry;
		}
	}
}
