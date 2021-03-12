using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020005B5 RID: 1461
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_EventInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class EventInfo : MemberInfo, _EventInfo
	{
		// Token: 0x06004496 RID: 17558 RVA: 0x000FC819 File Offset: 0x000FAA19
		[__DynamicallyInvokable]
		public static bool operator ==(EventInfo left, EventInfo right)
		{
			return left == right || (left != null && right != null && !(left is RuntimeEventInfo) && !(right is RuntimeEventInfo) && left.Equals(right));
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x000FC840 File Offset: 0x000FAA40
		[__DynamicallyInvokable]
		public static bool operator !=(EventInfo left, EventInfo right)
		{
			return !(left == right);
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x000FC84C File Offset: 0x000FAA4C
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x000FC855 File Offset: 0x000FAA55
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x000FC85D File Offset: 0x000FAA5D
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Event;
			}
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x000FC860 File Offset: 0x000FAA60
		public virtual MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600449C RID: 17564
		[__DynamicallyInvokable]
		public abstract MethodInfo GetAddMethod(bool nonPublic);

		// Token: 0x0600449D RID: 17565
		[__DynamicallyInvokable]
		public abstract MethodInfo GetRemoveMethod(bool nonPublic);

		// Token: 0x0600449E RID: 17566
		[__DynamicallyInvokable]
		public abstract MethodInfo GetRaiseMethod(bool nonPublic);

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x0600449F RID: 17567
		[__DynamicallyInvokable]
		public abstract EventAttributes Attributes { [__DynamicallyInvokable] get; }

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060044A0 RID: 17568 RVA: 0x000FC867 File Offset: 0x000FAA67
		[__DynamicallyInvokable]
		public virtual MethodInfo AddMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetAddMethod(true);
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060044A1 RID: 17569 RVA: 0x000FC870 File Offset: 0x000FAA70
		[__DynamicallyInvokable]
		public virtual MethodInfo RemoveMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetRemoveMethod(true);
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060044A2 RID: 17570 RVA: 0x000FC879 File Offset: 0x000FAA79
		[__DynamicallyInvokable]
		public virtual MethodInfo RaiseMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetRaiseMethod(true);
			}
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x000FC882 File Offset: 0x000FAA82
		public MethodInfo[] GetOtherMethods()
		{
			return this.GetOtherMethods(false);
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x000FC88B File Offset: 0x000FAA8B
		[__DynamicallyInvokable]
		public MethodInfo GetAddMethod()
		{
			return this.GetAddMethod(false);
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x000FC894 File Offset: 0x000FAA94
		[__DynamicallyInvokable]
		public MethodInfo GetRemoveMethod()
		{
			return this.GetRemoveMethod(false);
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x000FC89D File Offset: 0x000FAA9D
		[__DynamicallyInvokable]
		public MethodInfo GetRaiseMethod()
		{
			return this.GetRaiseMethod(false);
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x000FC8A8 File Offset: 0x000FAAA8
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public virtual void AddEventHandler(object target, Delegate handler)
		{
			MethodInfo addMethod = this.GetAddMethod();
			if (addMethod == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoPublicAddMethod"));
			}
			if (addMethod.ReturnType == typeof(EventRegistrationToken))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotSupportedOnWinRTEvent"));
			}
			addMethod.Invoke(target, new object[]
			{
				handler
			});
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x000FC910 File Offset: 0x000FAB10
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public virtual void RemoveEventHandler(object target, Delegate handler)
		{
			MethodInfo removeMethod = this.GetRemoveMethod();
			if (removeMethod == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoPublicRemoveMethod"));
			}
			ParameterInfo[] parametersNoCopy = removeMethod.GetParametersNoCopy();
			if (parametersNoCopy[0].ParameterType == typeof(EventRegistrationToken))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotSupportedOnWinRTEvent"));
			}
			removeMethod.Invoke(target, new object[]
			{
				handler
			});
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060044A9 RID: 17577 RVA: 0x000FC980 File Offset: 0x000FAB80
		[__DynamicallyInvokable]
		public virtual Type EventHandlerType
		{
			[__DynamicallyInvokable]
			get
			{
				MethodInfo addMethod = this.GetAddMethod(true);
				ParameterInfo[] parametersNoCopy = addMethod.GetParametersNoCopy();
				Type typeFromHandle = typeof(Delegate);
				for (int i = 0; i < parametersNoCopy.Length; i++)
				{
					Type parameterType = parametersNoCopy[i].ParameterType;
					if (parameterType.IsSubclassOf(typeFromHandle))
					{
						return parameterType;
					}
				}
				return null;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060044AA RID: 17578 RVA: 0x000FC9CD File Offset: 0x000FABCD
		[__DynamicallyInvokable]
		public bool IsSpecialName
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & EventAttributes.SpecialName) > EventAttributes.None;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x000FC9E0 File Offset: 0x000FABE0
		[__DynamicallyInvokable]
		public virtual bool IsMulticast
		{
			[__DynamicallyInvokable]
			get
			{
				Type eventHandlerType = this.EventHandlerType;
				Type typeFromHandle = typeof(MulticastDelegate);
				return typeFromHandle.IsAssignableFrom(eventHandlerType);
			}
		}

		// Token: 0x060044AC RID: 17580 RVA: 0x000FCA06 File Offset: 0x000FAC06
		Type _EventInfo.GetType()
		{
			return base.GetType();
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x000FCA0E File Offset: 0x000FAC0E
		void _EventInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x000FCA15 File Offset: 0x000FAC15
		void _EventInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044AF RID: 17583 RVA: 0x000FCA1C File Offset: 0x000FAC1C
		void _EventInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x000FCA23 File Offset: 0x000FAC23
		void _EventInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
