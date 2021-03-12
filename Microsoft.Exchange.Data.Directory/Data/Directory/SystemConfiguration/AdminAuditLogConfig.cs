using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200034D RID: 845
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class AdminAuditLogConfig : ADConfigurationObject
	{
		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002709 RID: 9993 RVA: 0x000A55B4 File Offset: 0x000A37B4
		internal override ADObjectSchema Schema
		{
			get
			{
				return AdminAuditLogConfig.schema;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x000A55BB File Offset: 0x000A37BB
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AdminAuditLogConfig.mostDerivedClass;
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x000A55C2 File Offset: 0x000A37C2
		internal override ADObjectId ParentPath
		{
			get
			{
				return AdminAuditLogConfig.parentPath;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x000A55C9 File Offset: 0x000A37C9
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x000A55CC File Offset: 0x000A37CC
		// (set) Token: 0x0600270E RID: 9998 RVA: 0x000A55DE File Offset: 0x000A37DE
		[Parameter(Mandatory = false)]
		public bool AdminAuditLogEnabled
		{
			get
			{
				return (bool)this[AdminAuditLogConfigSchema.AdminAuditLogEnabled];
			}
			set
			{
				this[AdminAuditLogConfigSchema.AdminAuditLogEnabled] = value;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x000A55F1 File Offset: 0x000A37F1
		// (set) Token: 0x06002710 RID: 10000 RVA: 0x000A55FE File Offset: 0x000A37FE
		[Parameter(Mandatory = false)]
		public AuditLogLevel LogLevel
		{
			get
			{
				if (this.CaptureDetailsEnabled)
				{
					return AuditLogLevel.Verbose;
				}
				return AuditLogLevel.None;
			}
			set
			{
				if (value == AuditLogLevel.None)
				{
					this.CaptureDetailsEnabled = false;
					return;
				}
				this.CaptureDetailsEnabled = true;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x000A5612 File Offset: 0x000A3812
		// (set) Token: 0x06002712 RID: 10002 RVA: 0x000A5624 File Offset: 0x000A3824
		internal bool CaptureDetailsEnabled
		{
			get
			{
				return (bool)this[AdminAuditLogConfigSchema.CaptureDetailsEnabled];
			}
			set
			{
				this[AdminAuditLogConfigSchema.CaptureDetailsEnabled] = value;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06002713 RID: 10003 RVA: 0x000A5637 File Offset: 0x000A3837
		// (set) Token: 0x06002714 RID: 10004 RVA: 0x000A5649 File Offset: 0x000A3849
		[Parameter(Mandatory = false)]
		public bool TestCmdletLoggingEnabled
		{
			get
			{
				return (bool)this[AdminAuditLogConfigSchema.TestCmdletLoggingEnabled];
			}
			set
			{
				this[AdminAuditLogConfigSchema.TestCmdletLoggingEnabled] = value;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x000A565C File Offset: 0x000A385C
		// (set) Token: 0x06002716 RID: 10006 RVA: 0x000A566E File Offset: 0x000A386E
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AdminAuditLogCmdlets
		{
			get
			{
				return (MultiValuedProperty<string>)this[AdminAuditLogConfigSchema.AdminAuditLogCmdlets];
			}
			set
			{
				this[AdminAuditLogConfigSchema.AdminAuditLogCmdlets] = value;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x000A567C File Offset: 0x000A387C
		// (set) Token: 0x06002718 RID: 10008 RVA: 0x000A568E File Offset: 0x000A388E
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AdminAuditLogParameters
		{
			get
			{
				return (MultiValuedProperty<string>)this[AdminAuditLogConfigSchema.AdminAuditLogParameters];
			}
			set
			{
				this[AdminAuditLogConfigSchema.AdminAuditLogParameters] = value;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x000A569C File Offset: 0x000A389C
		// (set) Token: 0x0600271A RID: 10010 RVA: 0x000A56AE File Offset: 0x000A38AE
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AdminAuditLogExcludedCmdlets
		{
			get
			{
				return (MultiValuedProperty<string>)this[AdminAuditLogConfigSchema.AdminAuditLogExcludedCmdlets];
			}
			set
			{
				this[AdminAuditLogConfigSchema.AdminAuditLogExcludedCmdlets] = value;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x000A56BC File Offset: 0x000A38BC
		// (set) Token: 0x0600271C RID: 10012 RVA: 0x000A56CE File Offset: 0x000A38CE
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan AdminAuditLogAgeLimit
		{
			get
			{
				return (EnhancedTimeSpan)this[AdminAuditLogConfigSchema.AdminAuditLogAgeLimit];
			}
			set
			{
				this[AdminAuditLogConfigSchema.AdminAuditLogAgeLimit] = value;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x000A56E1 File Offset: 0x000A38E1
		// (set) Token: 0x0600271E RID: 10014 RVA: 0x000A56F3 File Offset: 0x000A38F3
		internal SmtpAddress AdminAuditLogMailbox
		{
			get
			{
				return (SmtpAddress)this[AdminAuditLogConfigSchema.AdminAuditLogMailbox];
			}
			set
			{
				this[AdminAuditLogConfigSchema.AdminAuditLogMailbox] = value;
			}
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x000A5708 File Offset: 0x000A3908
		internal static ADObjectId GetWellKnownParentLocation(ADObjectId orgContainerId)
		{
			ADObjectId relativePath = AdminAuditLogConfig.parentPath;
			return orgContainerId.GetDescendantId(relativePath);
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x000A5724 File Offset: 0x000A3924
		internal static bool GetValueFromFlags(IPropertyBag propertyBag, AdminAuditLogFlags flag)
		{
			object obj = propertyBag[AdminAuditLogConfigSchema.AdminLogFlags];
			return flag == ((AdminAuditLogFlags)obj & flag);
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x000A574C File Offset: 0x000A394C
		internal static void SetFlags(IPropertyBag propertyBag, AdminAuditLogFlags flag, bool value)
		{
			AdminAuditLogFlags adminAuditLogFlags = (AdminAuditLogFlags)propertyBag[AdminAuditLogConfigSchema.AdminLogFlags];
			AdminAuditLogFlags adminAuditLogFlags2 = value ? (adminAuditLogFlags | flag) : (adminAuditLogFlags & ~flag);
			propertyBag[AdminAuditLogConfigSchema.AdminLogFlags] = adminAuditLogFlags2;
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x000A5788 File Offset: 0x000A3988
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x040017DC RID: 6108
		private static AdminAuditLogConfigSchema schema = ObjectSchema.GetInstance<AdminAuditLogConfigSchema>();

		// Token: 0x040017DD RID: 6109
		private static string mostDerivedClass = "msExchAdminAuditLogConfig";

		// Token: 0x040017DE RID: 6110
		private static ADObjectId parentPath = new ADObjectId("CN=Global Settings");
	}
}
