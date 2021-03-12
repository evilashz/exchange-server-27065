using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007E2 RID: 2018
	[SecurityCritical]
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class ContextAttribute : Attribute, IContextAttribute, IContextProperty
	{
		// Token: 0x060057AA RID: 22442 RVA: 0x00134337 File Offset: 0x00132537
		public ContextAttribute(string name)
		{
			this.AttributeName = name;
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x060057AB RID: 22443 RVA: 0x00134346 File Offset: 0x00132546
		public virtual string Name
		{
			[SecurityCritical]
			get
			{
				return this.AttributeName;
			}
		}

		// Token: 0x060057AC RID: 22444 RVA: 0x0013434E File Offset: 0x0013254E
		[SecurityCritical]
		public virtual bool IsNewContextOK(Context newCtx)
		{
			return true;
		}

		// Token: 0x060057AD RID: 22445 RVA: 0x00134351 File Offset: 0x00132551
		[SecurityCritical]
		public virtual void Freeze(Context newContext)
		{
		}

		// Token: 0x060057AE RID: 22446 RVA: 0x00134354 File Offset: 0x00132554
		[SecuritySafeCritical]
		public override bool Equals(object o)
		{
			IContextProperty contextProperty = o as IContextProperty;
			return contextProperty != null && this.AttributeName.Equals(contextProperty.Name);
		}

		// Token: 0x060057AF RID: 22447 RVA: 0x0013437E File Offset: 0x0013257E
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			return this.AttributeName.GetHashCode();
		}

		// Token: 0x060057B0 RID: 22448 RVA: 0x0013438C File Offset: 0x0013258C
		[SecurityCritical]
		public virtual bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
		{
			if (ctx == null)
			{
				throw new ArgumentNullException("ctx");
			}
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			if (!ctorMsg.ActivationType.IsContextful)
			{
				return true;
			}
			object property = ctx.GetProperty(this.AttributeName);
			return property != null && this.Equals(property);
		}

		// Token: 0x060057B1 RID: 22449 RVA: 0x001343E0 File Offset: 0x001325E0
		[SecurityCritical]
		public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			ctorMsg.ContextProperties.Add(this);
		}

		// Token: 0x040027B9 RID: 10169
		protected string AttributeName;
	}
}
