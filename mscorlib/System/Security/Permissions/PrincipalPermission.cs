using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Security.Principal;
using System.Security.Util;
using System.Threading;

namespace System.Security.Permissions
{
	// Token: 0x020002D8 RID: 728
	[ComVisible(true)]
	[Serializable]
	public sealed class PrincipalPermission : IPermission, ISecurityEncodable, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002600 RID: 9728 RVA: 0x00088BE0 File Offset: 0x00086DE0
		public PrincipalPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_array = new IDRole[1];
				this.m_array[0] = new IDRole();
				this.m_array[0].m_authenticated = true;
				this.m_array[0].m_id = null;
				this.m_array[0].m_role = null;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_array = new IDRole[1];
				this.m_array[0] = new IDRole();
				this.m_array[0].m_authenticated = false;
				this.m_array[0].m_id = "";
				this.m_array[0].m_role = "";
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x00088C9C File Offset: 0x00086E9C
		public PrincipalPermission(string name, string role)
		{
			this.m_array = new IDRole[1];
			this.m_array[0] = new IDRole();
			this.m_array[0].m_authenticated = true;
			this.m_array[0].m_id = name;
			this.m_array[0].m_role = role;
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x00088CF4 File Offset: 0x00086EF4
		public PrincipalPermission(string name, string role, bool isAuthenticated)
		{
			this.m_array = new IDRole[1];
			this.m_array[0] = new IDRole();
			this.m_array[0].m_authenticated = isAuthenticated;
			this.m_array[0].m_id = name;
			this.m_array[0].m_role = role;
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x00088D4A File Offset: 0x00086F4A
		private PrincipalPermission(IDRole[] array)
		{
			this.m_array = array;
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x00088D5C File Offset: 0x00086F5C
		private bool IsEmpty()
		{
			for (int i = 0; i < this.m_array.Length; i++)
			{
				if (this.m_array[i].m_id == null || !this.m_array[i].m_id.Equals("") || this.m_array[i].m_role == null || !this.m_array[i].m_role.Equals("") || this.m_array[i].m_authenticated)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x00088DDE File Offset: 0x00086FDE
		private bool VerifyType(IPermission perm)
		{
			return perm != null && !(perm.GetType() != base.GetType());
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00088DFC File Offset: 0x00086FFC
		public bool IsUnrestricted()
		{
			for (int i = 0; i < this.m_array.Length; i++)
			{
				if (this.m_array[i].m_id != null || this.m_array[i].m_role != null || !this.m_array[i].m_authenticated)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x00088E4C File Offset: 0x0008704C
		public bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			bool result;
			try
			{
				PrincipalPermission principalPermission = (PrincipalPermission)target;
				if (principalPermission.IsUnrestricted())
				{
					result = true;
				}
				else if (this.IsUnrestricted())
				{
					result = false;
				}
				else
				{
					for (int i = 0; i < this.m_array.Length; i++)
					{
						bool flag = false;
						for (int j = 0; j < principalPermission.m_array.Length; j++)
						{
							if (principalPermission.m_array[j].m_authenticated == this.m_array[i].m_authenticated && (principalPermission.m_array[j].m_id == null || (this.m_array[i].m_id != null && this.m_array[i].m_id.Equals(principalPermission.m_array[j].m_id))) && (principalPermission.m_array[j].m_role == null || (this.m_array[i].m_role != null && this.m_array[i].m_role.Equals(principalPermission.m_array[j].m_role))))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							return false;
						}
					}
					result = true;
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			return result;
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x00088FB4 File Offset: 0x000871B4
		public IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (!this.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (this.IsUnrestricted())
			{
				return target.Copy();
			}
			PrincipalPermission principalPermission = (PrincipalPermission)target;
			if (principalPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			List<IDRole> list = null;
			for (int i = 0; i < this.m_array.Length; i++)
			{
				for (int j = 0; j < principalPermission.m_array.Length; j++)
				{
					if (principalPermission.m_array[j].m_authenticated == this.m_array[i].m_authenticated)
					{
						if (principalPermission.m_array[j].m_id == null || this.m_array[i].m_id == null || this.m_array[i].m_id.Equals(principalPermission.m_array[j].m_id))
						{
							if (list == null)
							{
								list = new List<IDRole>();
							}
							IDRole idrole = new IDRole();
							idrole.m_id = ((principalPermission.m_array[j].m_id == null) ? this.m_array[i].m_id : principalPermission.m_array[j].m_id);
							if (principalPermission.m_array[j].m_role == null || this.m_array[i].m_role == null || this.m_array[i].m_role.Equals(principalPermission.m_array[j].m_role))
							{
								idrole.m_role = ((principalPermission.m_array[j].m_role == null) ? this.m_array[i].m_role : principalPermission.m_array[j].m_role);
							}
							else
							{
								idrole.m_role = "";
							}
							idrole.m_authenticated = principalPermission.m_array[j].m_authenticated;
							list.Add(idrole);
						}
						else if (principalPermission.m_array[j].m_role == null || this.m_array[i].m_role == null || this.m_array[i].m_role.Equals(principalPermission.m_array[j].m_role))
						{
							if (list == null)
							{
								list = new List<IDRole>();
							}
							list.Add(new IDRole
							{
								m_id = "",
								m_role = ((principalPermission.m_array[j].m_role == null) ? this.m_array[i].m_role : principalPermission.m_array[j].m_role),
								m_authenticated = principalPermission.m_array[j].m_authenticated
							});
						}
					}
				}
			}
			if (list == null)
			{
				return null;
			}
			IDRole[] array = new IDRole[list.Count];
			IEnumerator enumerator = list.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				array[num++] = (IDRole)obj;
			}
			return new PrincipalPermission(array);
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x00089284 File Offset: 0x00087484
		public IPermission Union(IPermission other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			if (!this.VerifyType(other))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			PrincipalPermission principalPermission = (PrincipalPermission)other;
			if (this.IsUnrestricted() || principalPermission.IsUnrestricted())
			{
				return new PrincipalPermission(PermissionState.Unrestricted);
			}
			int num = this.m_array.Length + principalPermission.m_array.Length;
			IDRole[] array = new IDRole[num];
			int i;
			for (i = 0; i < this.m_array.Length; i++)
			{
				array[i] = this.m_array[i];
			}
			for (int j = 0; j < principalPermission.m_array.Length; j++)
			{
				array[i + j] = principalPermission.m_array[j];
			}
			return new PrincipalPermission(array);
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x0008934C File Offset: 0x0008754C
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			IPermission permission = obj as IPermission;
			return (obj == null || permission != null) && this.IsSubsetOf(permission) && (permission == null || permission.IsSubsetOf(this));
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x00089384 File Offset: 0x00087584
		[ComVisible(false)]
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.m_array.Length; i++)
			{
				num += this.m_array[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x000893B7 File Offset: 0x000875B7
		public IPermission Copy()
		{
			return new PrincipalPermission(this.m_array);
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000893C4 File Offset: 0x000875C4
		[SecurityCritical]
		private void ThrowSecurityException()
		{
			AssemblyName assemblyName = null;
			Evidence evidence = null;
			PermissionSet.s_fullTrust.Assert();
			try
			{
				Assembly callingAssembly = Assembly.GetCallingAssembly();
				assemblyName = callingAssembly.GetName();
				if (callingAssembly != Assembly.GetExecutingAssembly())
				{
					evidence = callingAssembly.Evidence;
				}
			}
			catch
			{
			}
			PermissionSet.RevertAssert();
			throw new SecurityException(Environment.GetResourceString("Security_PrincipalPermission"), assemblyName, null, null, null, SecurityAction.Demand, this, this, evidence);
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x00089434 File Offset: 0x00087634
		[SecuritySafeCritical]
		public void Demand()
		{
			new SecurityPermission(SecurityPermissionFlag.ControlPrincipal).Assert();
			IPrincipal currentPrincipal = Thread.CurrentPrincipal;
			if (currentPrincipal == null)
			{
				this.ThrowSecurityException();
			}
			if (this.m_array == null)
			{
				return;
			}
			int num = this.m_array.Length;
			bool flag = false;
			for (int i = 0; i < num; i++)
			{
				if (!this.m_array[i].m_authenticated)
				{
					flag = true;
					break;
				}
				IIdentity identity = currentPrincipal.Identity;
				if (identity.IsAuthenticated && (this.m_array[i].m_id == null || string.Compare(identity.Name, this.m_array[i].m_id, StringComparison.OrdinalIgnoreCase) == 0))
				{
					if (this.m_array[i].m_role == null)
					{
						flag = true;
					}
					else
					{
						WindowsPrincipal windowsPrincipal = currentPrincipal as WindowsPrincipal;
						if (windowsPrincipal != null && this.m_array[i].Sid != null)
						{
							flag = windowsPrincipal.IsInRole(this.m_array[i].Sid);
						}
						else
						{
							flag = currentPrincipal.IsInRole(this.m_array[i].m_role);
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			if (!flag)
			{
				this.ThrowSecurityException();
			}
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x0008954C File Offset: 0x0008774C
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Permissions.PrincipalPermission");
			securityElement.AddAttribute("version", "1");
			int num = this.m_array.Length;
			for (int i = 0; i < num; i++)
			{
				securityElement.AddChild(this.m_array[i].ToXml());
			}
			return securityElement;
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000895B0 File Offset: 0x000877B0
		public void FromXml(SecurityElement elem)
		{
			CodeAccessPermission.ValidateElement(elem, this);
			if (elem.InternalChildren != null && elem.InternalChildren.Count != 0)
			{
				int count = elem.InternalChildren.Count;
				int num = 0;
				this.m_array = new IDRole[count];
				IEnumerator enumerator = elem.Children.GetEnumerator();
				while (enumerator.MoveNext())
				{
					IDRole idrole = new IDRole();
					idrole.FromXml((SecurityElement)enumerator.Current);
					this.m_array[num++] = idrole;
				}
				return;
			}
			this.m_array = new IDRole[0];
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x0008963A File Offset: 0x0008783A
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x00089647 File Offset: 0x00087847
		int IBuiltInPermission.GetTokenIndex()
		{
			return PrincipalPermission.GetTokenIndex();
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x0008964E File Offset: 0x0008784E
		internal static int GetTokenIndex()
		{
			return 8;
		}

		// Token: 0x04000E67 RID: 3687
		private IDRole[] m_array;
	}
}
