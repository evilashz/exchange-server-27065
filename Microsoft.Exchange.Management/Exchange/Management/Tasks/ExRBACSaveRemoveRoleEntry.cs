using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001004 RID: 4100
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExRBACSaveRemoveRoleEntry : LocalizedException
	{
		// Token: 0x0600AED1 RID: 44753 RVA: 0x0029376E File Offset: 0x0029196E
		public ExRBACSaveRemoveRoleEntry(string entryName, string roleId, string error) : base(Strings.ExRBACSaveRemoveRoleEntry(entryName, roleId, error))
		{
			this.entryName = entryName;
			this.roleId = roleId;
			this.error = error;
		}

		// Token: 0x0600AED2 RID: 44754 RVA: 0x00293793 File Offset: 0x00291993
		public ExRBACSaveRemoveRoleEntry(string entryName, string roleId, string error, Exception innerException) : base(Strings.ExRBACSaveRemoveRoleEntry(entryName, roleId, error), innerException)
		{
			this.entryName = entryName;
			this.roleId = roleId;
			this.error = error;
		}

		// Token: 0x0600AED3 RID: 44755 RVA: 0x002937BC File Offset: 0x002919BC
		protected ExRBACSaveRemoveRoleEntry(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.entryName = (string)info.GetValue("entryName", typeof(string));
			this.roleId = (string)info.GetValue("roleId", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600AED4 RID: 44756 RVA: 0x00293831 File Offset: 0x00291A31
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("entryName", this.entryName);
			info.AddValue("roleId", this.roleId);
			info.AddValue("error", this.error);
		}

		// Token: 0x170037DE RID: 14302
		// (get) Token: 0x0600AED5 RID: 44757 RVA: 0x0029386E File Offset: 0x00291A6E
		public string EntryName
		{
			get
			{
				return this.entryName;
			}
		}

		// Token: 0x170037DF RID: 14303
		// (get) Token: 0x0600AED6 RID: 44758 RVA: 0x00293876 File Offset: 0x00291A76
		public string RoleId
		{
			get
			{
				return this.roleId;
			}
		}

		// Token: 0x170037E0 RID: 14304
		// (get) Token: 0x0600AED7 RID: 44759 RVA: 0x0029387E File Offset: 0x00291A7E
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04006144 RID: 24900
		private readonly string entryName;

		// Token: 0x04006145 RID: 24901
		private readonly string roleId;

		// Token: 0x04006146 RID: 24902
		private readonly string error;
	}
}
