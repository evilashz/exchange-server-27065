using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007DE RID: 2014
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ContextProperty
	{
		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x0600579D RID: 22429 RVA: 0x00134311 File Offset: 0x00132511
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x0600579E RID: 22430 RVA: 0x00134319 File Offset: 0x00132519
		public virtual object Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x0600579F RID: 22431 RVA: 0x00134321 File Offset: 0x00132521
		internal ContextProperty(string name, object prop)
		{
			this._name = name;
			this._property = prop;
		}

		// Token: 0x040027B7 RID: 10167
		internal string _name;

		// Token: 0x040027B8 RID: 10168
		internal object _property;
	}
}
