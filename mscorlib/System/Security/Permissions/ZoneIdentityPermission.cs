using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Permissions
{
	// Token: 0x020002E3 RID: 739
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06002677 RID: 9847 RVA: 0x0008B75C File Offset: 0x0008995C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				if (this.m_serializedPermission != null)
				{
					this.FromXml(SecurityElement.FromString(this.m_serializedPermission));
					this.m_serializedPermission = null;
					return;
				}
				this.SecurityZone = this.m_zone;
				this.m_zone = SecurityZone.NoZone;
			}
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x0008B7AC File Offset: 0x000899AC
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = this.ToXml().ToString();
				this.m_zone = this.SecurityZone;
			}
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x0008B7DA File Offset: 0x000899DA
		[OnSerialized]
		private void OnSerialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = null;
				this.m_zone = SecurityZone.NoZone;
			}
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x0008B7F9 File Offset: 0x000899F9
		public ZoneIdentityPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_zones = 31U;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_zones = 0U;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x0008B82F File Offset: 0x00089A2F
		public ZoneIdentityPermission(SecurityZone zone)
		{
			this.SecurityZone = zone;
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x0008B845 File Offset: 0x00089A45
		internal ZoneIdentityPermission(uint zones)
		{
			this.m_zones = (zones & 31U);
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x0008B860 File Offset: 0x00089A60
		internal void AppendZones(ArrayList zoneList)
		{
			int num = 0;
			for (uint num2 = 1U; num2 < 31U; num2 <<= 1)
			{
				if ((this.m_zones & num2) != 0U)
				{
					zoneList.Add((SecurityZone)num);
				}
				num++;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x0008B8B8 File Offset: 0x00089AB8
		// (set) Token: 0x0600267E RID: 9854 RVA: 0x0008B897 File Offset: 0x00089A97
		public SecurityZone SecurityZone
		{
			get
			{
				SecurityZone securityZone = SecurityZone.NoZone;
				int num = 0;
				for (uint num2 = 1U; num2 < 31U; num2 <<= 1)
				{
					if ((this.m_zones & num2) != 0U)
					{
						if (securityZone != SecurityZone.NoZone)
						{
							return SecurityZone.NoZone;
						}
						securityZone = (SecurityZone)num;
					}
					num++;
				}
				return securityZone;
			}
			set
			{
				ZoneIdentityPermission.VerifyZone(value);
				if (value == SecurityZone.NoZone)
				{
					this.m_zones = 0U;
					return;
				}
				this.m_zones = 1U << (int)value;
			}
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x0008B8EF File Offset: 0x00089AEF
		private static void VerifyZone(SecurityZone zone)
		{
			if (zone < SecurityZone.NoZone || zone > SecurityZone.Untrusted)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalZone"));
			}
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x0008B909 File Offset: 0x00089B09
		public override IPermission Copy()
		{
			return new ZoneIdentityPermission(this.m_zones);
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x0008B918 File Offset: 0x00089B18
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_zones == 0U;
			}
			ZoneIdentityPermission zoneIdentityPermission = target as ZoneIdentityPermission;
			if (zoneIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			return (this.m_zones & zoneIdentityPermission.m_zones) == this.m_zones;
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0008B978 File Offset: 0x00089B78
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ZoneIdentityPermission zoneIdentityPermission = target as ZoneIdentityPermission;
			if (zoneIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			uint num = this.m_zones & zoneIdentityPermission.m_zones;
			if (num == 0U)
			{
				return null;
			}
			return new ZoneIdentityPermission(num);
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x0008B9D4 File Offset: 0x00089BD4
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				if (this.m_zones == 0U)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				ZoneIdentityPermission zoneIdentityPermission = target as ZoneIdentityPermission;
				if (zoneIdentityPermission == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
					{
						base.GetType().FullName
					}));
				}
				return new ZoneIdentityPermission(this.m_zones | zoneIdentityPermission.m_zones);
			}
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x0008BA38 File Offset: 0x00089C38
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.ZoneIdentityPermission");
			if (this.SecurityZone != SecurityZone.NoZone)
			{
				securityElement.AddAttribute("Zone", Enum.GetName(typeof(SecurityZone), this.SecurityZone));
			}
			else
			{
				int num = 0;
				for (uint num2 = 1U; num2 < 31U; num2 <<= 1)
				{
					if ((this.m_zones & num2) != 0U)
					{
						SecurityElement securityElement2 = new SecurityElement("Zone");
						securityElement2.AddAttribute("Zone", Enum.GetName(typeof(SecurityZone), (SecurityZone)num));
						securityElement.AddChild(securityElement2);
					}
					num++;
				}
			}
			return securityElement;
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0008BAD4 File Offset: 0x00089CD4
		public override void FromXml(SecurityElement esd)
		{
			this.m_zones = 0U;
			CodeAccessPermission.ValidateElement(esd, this);
			string text = esd.Attribute("Zone");
			if (text != null)
			{
				this.SecurityZone = (SecurityZone)Enum.Parse(typeof(SecurityZone), text);
			}
			if (esd.Children != null)
			{
				foreach (object obj in esd.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					text = securityElement.Attribute("Zone");
					int num = (int)Enum.Parse(typeof(SecurityZone), text);
					if (num != -1)
					{
						this.m_zones |= 1U << num;
					}
				}
			}
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x0008BBA4 File Offset: 0x00089DA4
		int IBuiltInPermission.GetTokenIndex()
		{
			return ZoneIdentityPermission.GetTokenIndex();
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x0008BBAB File Offset: 0x00089DAB
		internal static int GetTokenIndex()
		{
			return 14;
		}

		// Token: 0x04000E9E RID: 3742
		private const uint AllZones = 31U;

		// Token: 0x04000E9F RID: 3743
		[OptionalField(VersionAdded = 2)]
		private uint m_zones;

		// Token: 0x04000EA0 RID: 3744
		[OptionalField(VersionAdded = 2)]
		private string m_serializedPermission;

		// Token: 0x04000EA1 RID: 3745
		private SecurityZone m_zone = SecurityZone.NoZone;
	}
}
