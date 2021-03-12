using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x0200060C RID: 1548
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_EventBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventBuilder : _EventBuilder
	{
		// Token: 0x06004943 RID: 18755 RVA: 0x0010897D File Offset: 0x00106B7D
		private EventBuilder()
		{
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x00108985 File Offset: 0x00106B85
		internal EventBuilder(ModuleBuilder mod, string name, EventAttributes attr, TypeBuilder type, EventToken evToken)
		{
			this.m_name = name;
			this.m_module = mod;
			this.m_attributes = attr;
			this.m_evToken = evToken;
			this.m_type = type;
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x001089B2 File Offset: 0x00106BB2
		public EventToken GetEventToken()
		{
			return this.m_evToken;
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x001089BC File Offset: 0x00106BBC
		[SecurityCritical]
		private void SetMethodSemantics(MethodBuilder mdBuilder, MethodSemanticsAttributes semantics)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_type.ThrowIfCreated();
			TypeBuilder.DefineMethodSemantics(this.m_module.GetNativeHandle(), this.m_evToken.Token, semantics, mdBuilder.GetToken().Token);
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x00108A12 File Offset: 0x00106C12
		[SecuritySafeCritical]
		public void SetAddOnMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.AddOn);
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x00108A1C File Offset: 0x00106C1C
		[SecuritySafeCritical]
		public void SetRemoveOnMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.RemoveOn);
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x00108A27 File Offset: 0x00106C27
		[SecuritySafeCritical]
		public void SetRaiseMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Fire);
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x00108A32 File Offset: 0x00106C32
		[SecuritySafeCritical]
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Other);
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x00108A3C File Offset: 0x00106C3C
		[SecuritySafeCritical]
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.m_type.ThrowIfCreated();
			TypeBuilder.DefineCustomAttribute(this.m_module, this.m_evToken.Token, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x00108AA3 File Offset: 0x00106CA3
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.m_type.ThrowIfCreated();
			customBuilder.CreateCustomAttribute(this.m_module, this.m_evToken.Token);
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x00108AD5 File Offset: 0x00106CD5
		void _EventBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x00108ADC File Offset: 0x00106CDC
		void _EventBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x00108AE3 File Offset: 0x00106CE3
		void _EventBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x00108AEA File Offset: 0x00106CEA
		void _EventBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001DF3 RID: 7667
		private string m_name;

		// Token: 0x04001DF4 RID: 7668
		private EventToken m_evToken;

		// Token: 0x04001DF5 RID: 7669
		private ModuleBuilder m_module;

		// Token: 0x04001DF6 RID: 7670
		private EventAttributes m_attributes;

		// Token: 0x04001DF7 RID: 7671
		private TypeBuilder m_type;
	}
}
