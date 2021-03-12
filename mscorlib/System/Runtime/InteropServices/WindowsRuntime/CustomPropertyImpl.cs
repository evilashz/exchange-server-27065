using System;
using System.Globalization;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E4 RID: 2532
	internal sealed class CustomPropertyImpl : ICustomProperty
	{
		// Token: 0x06006492 RID: 25746 RVA: 0x00154EB0 File Offset: 0x001530B0
		public CustomPropertyImpl(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentNullException("propertyInfo");
			}
			this.m_property = propertyInfo;
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06006493 RID: 25747 RVA: 0x00154ED3 File Offset: 0x001530D3
		public string Name
		{
			get
			{
				return this.m_property.Name;
			}
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06006494 RID: 25748 RVA: 0x00154EE0 File Offset: 0x001530E0
		public bool CanRead
		{
			get
			{
				return this.m_property.GetGetMethod() != null;
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x06006495 RID: 25749 RVA: 0x00154EF3 File Offset: 0x001530F3
		public bool CanWrite
		{
			get
			{
				return this.m_property.GetSetMethod() != null;
			}
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x00154F06 File Offset: 0x00153106
		public object GetValue(object target)
		{
			return this.InvokeInternal(target, null, true);
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x00154F11 File Offset: 0x00153111
		public object GetValue(object target, object indexValue)
		{
			return this.InvokeInternal(target, new object[]
			{
				indexValue
			}, true);
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x00154F25 File Offset: 0x00153125
		public void SetValue(object target, object value)
		{
			this.InvokeInternal(target, new object[]
			{
				value
			}, false);
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x00154F3A File Offset: 0x0015313A
		public void SetValue(object target, object value, object indexValue)
		{
			this.InvokeInternal(target, new object[]
			{
				indexValue,
				value
			}, false);
		}

		// Token: 0x0600649A RID: 25754 RVA: 0x00154F54 File Offset: 0x00153154
		[SecuritySafeCritical]
		private object InvokeInternal(object target, object[] args, bool getValue)
		{
			IGetProxyTarget getProxyTarget = target as IGetProxyTarget;
			if (getProxyTarget != null)
			{
				target = getProxyTarget.GetTarget();
			}
			MethodInfo methodInfo = getValue ? this.m_property.GetGetMethod(true) : this.m_property.GetSetMethod(true);
			if (methodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString(getValue ? "Arg_GetMethNotFnd" : "Arg_SetMethNotFnd"));
			}
			if (!methodInfo.IsPublic)
			{
				throw new MethodAccessException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Arg_MethodAccessException_WithMethodName"), methodInfo.ToString(), methodInfo.DeclaringType.FullName));
			}
			RuntimeMethodInfo runtimeMethodInfo = methodInfo as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
			}
			return runtimeMethodInfo.UnsafeInvoke(target, BindingFlags.Default, null, args, null);
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x0600649B RID: 25755 RVA: 0x00155012 File Offset: 0x00153212
		public Type Type
		{
			get
			{
				return this.m_property.PropertyType;
			}
		}

		// Token: 0x04002C88 RID: 11400
		private PropertyInfo m_property;
	}
}
