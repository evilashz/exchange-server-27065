using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009C5 RID: 2501
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	internal sealed class ManagedActivationFactory : IActivationFactory, IManagedActivationFactory
	{
		// Token: 0x060063A3 RID: 25507 RVA: 0x00152754 File Offset: 0x00150954
		[SecurityCritical]
		internal ManagedActivationFactory(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType) || !type.IsExportedToWindowsRuntime)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TypeNotActivatableViaWindowsRuntime", new object[]
				{
					type
				}), "type");
			}
			this.m_type = type;
		}

		// Token: 0x060063A4 RID: 25508 RVA: 0x001527B4 File Offset: 0x001509B4
		public object ActivateInstance()
		{
			object result;
			try
			{
				result = Activator.CreateInstance(this.m_type);
			}
			catch (MissingMethodException)
			{
				throw new NotImplementedException();
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return result;
		}

		// Token: 0x060063A5 RID: 25509 RVA: 0x001527FC File Offset: 0x001509FC
		void IManagedActivationFactory.RunClassConstructor()
		{
			RuntimeHelpers.RunClassConstructor(this.m_type.TypeHandle);
		}

		// Token: 0x04002C41 RID: 11329
		private Type m_type;
	}
}
