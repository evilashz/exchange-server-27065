using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DC1 RID: 3521
	[XmlRoot(ElementName = "ManagementRole", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class ManagementRoleType
	{
		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x0600598A RID: 22922 RVA: 0x001180CB File Offset: 0x001162CB
		// (set) Token: 0x0600598B RID: 22923 RVA: 0x001180D3 File Offset: 0x001162D3
		[XmlArray(Order = 0)]
		[XmlArrayItem("Role", IsNullable = false)]
		[DataMember(Name = "UserRoles", IsRequired = false)]
		public string[] UserRoles
		{
			get
			{
				return this.userRolesField;
			}
			set
			{
				this.userRolesField = value;
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x0600598C RID: 22924 RVA: 0x001180DC File Offset: 0x001162DC
		// (set) Token: 0x0600598D RID: 22925 RVA: 0x001180E4 File Offset: 0x001162E4
		[XmlArray(Order = 1)]
		[XmlArrayItem("Role", IsNullable = false)]
		[DataMember(Name = "ApplicationRoles", IsRequired = false)]
		public string[] ApplicationRoles
		{
			get
			{
				return this.applicationRolesField;
			}
			set
			{
				this.applicationRolesField = value;
			}
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x0600598E RID: 22926 RVA: 0x001180ED File Offset: 0x001162ED
		internal bool HasUserRoles
		{
			get
			{
				return this.UserRoleTypes != null && this.UserRoleTypes.Length > 0;
			}
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x0600598F RID: 22927 RVA: 0x00118104 File Offset: 0x00116304
		internal bool HasApplicationRoles
		{
			get
			{
				return this.ApplicationRoles != null && this.ApplicationRoles.Length > 0;
			}
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06005990 RID: 22928 RVA: 0x0011811B File Offset: 0x0011631B
		internal RoleType[] UserRoleTypes
		{
			get
			{
				this.ValidateAndConvert();
				return this.userRoleTypes;
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x06005991 RID: 22929 RVA: 0x00118129 File Offset: 0x00116329
		// (set) Token: 0x06005992 RID: 22930 RVA: 0x00118137 File Offset: 0x00116337
		internal RoleType[] ApplicationRoleTypes
		{
			get
			{
				this.ValidateAndConvert();
				return this.applicationRoleTypes;
			}
			set
			{
				this.applicationRoleTypes = value;
			}
		}

		// Token: 0x06005993 RID: 22931 RVA: 0x00118170 File Offset: 0x00116370
		internal void ValidateAndConvert()
		{
			if (this.validated)
			{
				return;
			}
			try
			{
				if (this.userRolesField != null)
				{
					this.userRoleTypes = (from role in this.userRolesField
					select (RoleType)Enum.Parse(typeof(RoleType), role, true)).Distinct<RoleType>().ToArray<RoleType>();
				}
				if (this.applicationRolesField != null)
				{
					this.applicationRoleTypes = (from role in this.applicationRolesField
					select (RoleType)Enum.Parse(typeof(RoleType), role, true)).Distinct<RoleType>().ToArray<RoleType>();
				}
				this.validated = true;
			}
			catch (ArgumentException arg)
			{
				ExTraceGlobals.AuthorizationTracer.TraceDebug<ArgumentException>(0L, "[ManagementRoleType.ValidateAndConvert] hit argument exception: {0}", arg);
				throw new InvalidManagementRoleHeaderException((CoreResources.IDs)2448725207U);
			}
		}

		// Token: 0x04003194 RID: 12692
		private bool validated;

		// Token: 0x04003195 RID: 12693
		private string[] userRolesField;

		// Token: 0x04003196 RID: 12694
		private string[] applicationRolesField;

		// Token: 0x04003197 RID: 12695
		private RoleType[] userRoleTypes;

		// Token: 0x04003198 RID: 12696
		private RoleType[] applicationRoleTypes;
	}
}
