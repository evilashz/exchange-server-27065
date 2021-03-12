using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002BE RID: 702
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class IsolatedStoragePermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x0600251F RID: 9503 RVA: 0x00087228 File Offset: 0x00085428
		protected IsolatedStoragePermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_userQuota = long.MaxValue;
				this.m_machineQuota = long.MaxValue;
				this.m_expirationDays = long.MaxValue;
				this.m_permanentData = true;
				this.m_allowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_userQuota = 0L;
				this.m_machineQuota = 0L;
				this.m_expirationDays = 0L;
				this.m_permanentData = false;
				this.m_allowed = IsolatedStorageContainment.None;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000872B8 File Offset: 0x000854B8
		internal IsolatedStoragePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData)
		{
			this.m_userQuota = 0L;
			this.m_machineQuota = 0L;
			this.m_expirationDays = ExpirationDays;
			this.m_permanentData = PermanentData;
			this.m_allowed = UsageAllowed;
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000872E5 File Offset: 0x000854E5
		internal IsolatedStoragePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData, long UserQuota)
		{
			this.m_machineQuota = 0L;
			this.m_userQuota = UserQuota;
			this.m_expirationDays = ExpirationDays;
			this.m_permanentData = PermanentData;
			this.m_allowed = UsageAllowed;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x0008731B File Offset: 0x0008551B
		// (set) Token: 0x06002522 RID: 9506 RVA: 0x00087312 File Offset: 0x00085512
		public long UserQuota
		{
			get
			{
				return this.m_userQuota;
			}
			set
			{
				this.m_userQuota = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x0008732C File Offset: 0x0008552C
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x00087323 File Offset: 0x00085523
		public IsolatedStorageContainment UsageAllowed
		{
			get
			{
				return this.m_allowed;
			}
			set
			{
				this.m_allowed = value;
			}
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x00087334 File Offset: 0x00085534
		public bool IsUnrestricted()
		{
			return this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage;
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x00087343 File Offset: 0x00085543
		internal static long min(long x, long y)
		{
			if (x <= y)
			{
				return x;
			}
			return y;
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x0008734C File Offset: 0x0008554C
		internal static long max(long x, long y)
		{
			if (x >= y)
			{
				return x;
			}
			return y;
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x00087355 File Offset: 0x00085555
		public override SecurityElement ToXml()
		{
			return this.ToXml(base.GetType().FullName);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x00087368 File Offset: 0x00085568
		internal SecurityElement ToXml(string permName)
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, permName);
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Allowed", Enum.GetName(typeof(IsolatedStorageContainment), this.m_allowed));
				if (this.m_userQuota > 0L)
				{
					securityElement.AddAttribute("UserQuota", this.m_userQuota.ToString(CultureInfo.InvariantCulture));
				}
				if (this.m_machineQuota > 0L)
				{
					securityElement.AddAttribute("MachineQuota", this.m_machineQuota.ToString(CultureInfo.InvariantCulture));
				}
				if (this.m_expirationDays > 0L)
				{
					securityElement.AddAttribute("Expiry", this.m_expirationDays.ToString(CultureInfo.InvariantCulture));
				}
				if (this.m_permanentData)
				{
					securityElement.AddAttribute("Permanent", this.m_permanentData.ToString());
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00087450 File Offset: 0x00085650
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			this.m_allowed = IsolatedStorageContainment.None;
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_allowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
			}
			else
			{
				string text = esd.Attribute("Allowed");
				if (text != null)
				{
					this.m_allowed = (IsolatedStorageContainment)Enum.Parse(typeof(IsolatedStorageContainment), text);
				}
			}
			if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
			{
				this.m_userQuota = long.MaxValue;
				this.m_machineQuota = long.MaxValue;
				this.m_expirationDays = long.MaxValue;
				this.m_permanentData = true;
				return;
			}
			string text2 = esd.Attribute("UserQuota");
			this.m_userQuota = ((text2 != null) ? long.Parse(text2, CultureInfo.InvariantCulture) : 0L);
			text2 = esd.Attribute("MachineQuota");
			this.m_machineQuota = ((text2 != null) ? long.Parse(text2, CultureInfo.InvariantCulture) : 0L);
			text2 = esd.Attribute("Expiry");
			this.m_expirationDays = ((text2 != null) ? long.Parse(text2, CultureInfo.InvariantCulture) : 0L);
			text2 = esd.Attribute("Permanent");
			this.m_permanentData = (text2 != null && bool.Parse(text2));
		}

		// Token: 0x04000E15 RID: 3605
		internal long m_userQuota;

		// Token: 0x04000E16 RID: 3606
		internal long m_machineQuota;

		// Token: 0x04000E17 RID: 3607
		internal long m_expirationDays;

		// Token: 0x04000E18 RID: 3608
		internal bool m_permanentData;

		// Token: 0x04000E19 RID: 3609
		internal IsolatedStorageContainment m_allowed;

		// Token: 0x04000E1A RID: 3610
		private const string _strUserQuota = "UserQuota";

		// Token: 0x04000E1B RID: 3611
		private const string _strMachineQuota = "MachineQuota";

		// Token: 0x04000E1C RID: 3612
		private const string _strExpiry = "Expiry";

		// Token: 0x04000E1D RID: 3613
		private const string _strPermDat = "Permanent";
	}
}
