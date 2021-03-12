using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x0200010A RID: 266
	[ComVisible(true)]
	[Serializable]
	public abstract class MarshalByRefObject
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x000308CB File Offset: 0x0002EACB
		// (set) Token: 0x06000FED RID: 4077 RVA: 0x000308D3 File Offset: 0x0002EAD3
		private object Identity
		{
			get
			{
				return this.__identity;
			}
			set
			{
				this.__identity = value;
			}
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x000308DC File Offset: 0x0002EADC
		[SecuritySafeCritical]
		internal IntPtr GetComIUnknown(bool fIsBeingMarshalled)
		{
			IntPtr result;
			if (RemotingServices.IsTransparentProxy(this))
			{
				result = RemotingServices.GetRealProxy(this).GetCOMIUnknown(fIsBeingMarshalled);
			}
			else
			{
				result = Marshal.GetIUnknownForObject(this);
			}
			return result;
		}

		// Token: 0x06000FEF RID: 4079
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetComIUnknown(MarshalByRefObject o);

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00030908 File Offset: 0x0002EB08
		internal bool IsInstanceOfType(Type T)
		{
			return T.IsInstanceOfType(this);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00030914 File Offset: 0x0002EB14
		internal object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			Type type = base.GetType();
			if (!type.IsCOMObject)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_InvokeMember"));
			}
			return type.InvokeMember(name, invokeAttr, binder, this, args, modifiers, culture, namedParameters);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00030954 File Offset: 0x0002EB54
		protected MarshalByRefObject MemberwiseClone(bool cloneIdentity)
		{
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)base.MemberwiseClone();
			if (!cloneIdentity)
			{
				marshalByRefObject.Identity = null;
			}
			return marshalByRefObject;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00030978 File Offset: 0x0002EB78
		[SecuritySafeCritical]
		internal static Identity GetIdentity(MarshalByRefObject obj, out bool fServer)
		{
			fServer = true;
			Identity result = null;
			if (obj != null)
			{
				if (!RemotingServices.IsTransparentProxy(obj))
				{
					result = (Identity)obj.Identity;
				}
				else
				{
					fServer = false;
					result = RemotingServices.GetRealProxy(obj).IdentityObject;
				}
			}
			return result;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x000309B4 File Offset: 0x0002EBB4
		internal static Identity GetIdentity(MarshalByRefObject obj)
		{
			bool flag;
			return MarshalByRefObject.GetIdentity(obj, out flag);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000309C9 File Offset: 0x0002EBC9
		internal ServerIdentity __RaceSetServerIdentity(ServerIdentity id)
		{
			if (this.__identity == null)
			{
				if (!id.IsContextBound)
				{
					id.RaceSetTransparentProxy(this);
				}
				Interlocked.CompareExchange(ref this.__identity, id, null);
			}
			return (ServerIdentity)this.__identity;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000309FC File Offset: 0x0002EBFC
		internal void __ResetServerIdentity()
		{
			this.__identity = null;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00030A05 File Offset: 0x0002EC05
		[SecurityCritical]
		public object GetLifetimeService()
		{
			return LifetimeServices.GetLease(this);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00030A0D File Offset: 0x0002EC0D
		[SecurityCritical]
		public virtual object InitializeLifetimeService()
		{
			return LifetimeServices.GetLeaseInitial(this);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00030A15 File Offset: 0x0002EC15
		[SecurityCritical]
		public virtual ObjRef CreateObjRef(Type requestedType)
		{
			if (this.__identity == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_NoIdentityEntry"));
			}
			return new ObjRef(this, requestedType);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00030A38 File Offset: 0x0002EC38
		[SecuritySafeCritical]
		internal bool CanCastToXmlType(string xmlTypeName, string xmlTypeNamespace)
		{
			Type type = SoapServices.GetInteropTypeFromXmlType(xmlTypeName, xmlTypeNamespace);
			if (type == null)
			{
				string text;
				string assemblyString;
				if (!SoapServices.DecodeXmlNamespaceForClrTypeNamespace(xmlTypeNamespace, out text, out assemblyString))
				{
					return false;
				}
				string name;
				if (text != null && text.Length > 0)
				{
					name = text + "." + xmlTypeName;
				}
				else
				{
					name = xmlTypeName;
				}
				try
				{
					Assembly assembly = Assembly.Load(assemblyString);
					type = assembly.GetType(name, false, false);
				}
				catch
				{
					return false;
				}
			}
			return type != null && type.IsAssignableFrom(base.GetType());
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00030AC8 File Offset: 0x0002ECC8
		[SecuritySafeCritical]
		internal static bool CanCastToXmlTypeHelper(RuntimeType castType, MarshalByRefObject o)
		{
			if (castType == null)
			{
				throw new ArgumentNullException("castType");
			}
			if (!castType.IsInterface && !castType.IsMarshalByRef)
			{
				return false;
			}
			string xmlTypeName = null;
			string xmlTypeNamespace = null;
			if (!SoapServices.GetXmlTypeForInteropType(castType, out xmlTypeName, out xmlTypeNamespace))
			{
				xmlTypeName = castType.Name;
				xmlTypeNamespace = SoapServices.CodeXmlNamespaceForClrTypeNamespace(castType.Namespace, castType.GetRuntimeAssembly().GetSimpleName());
			}
			return o.CanCastToXmlType(xmlTypeName, xmlTypeNamespace);
		}

		// Token: 0x040005BA RID: 1466
		private object __identity;
	}
}
