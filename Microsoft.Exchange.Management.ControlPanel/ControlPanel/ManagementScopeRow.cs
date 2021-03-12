using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000524 RID: 1316
	[DataContract]
	[KnownType(typeof(ManagementScopeRow))]
	public class ManagementScopeRow : DropDownItemData
	{
		// Token: 0x06003ED0 RID: 16080 RVA: 0x000BD44D File Offset: 0x000BB64D
		public static bool IsDefaultScope(string scopeId)
		{
			return scopeId == ManagementScopeRow.DefaultScopeId;
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x000BD45F File Offset: 0x000BB65F
		public static bool IsMultipleScope(string scopeId)
		{
			return scopeId == string.Empty;
		}

		// Token: 0x17002490 RID: 9360
		// (get) Token: 0x06003ED2 RID: 16082 RVA: 0x000BD471 File Offset: 0x000BB671
		public static ManagementScopeRow DefaultRow
		{
			get
			{
				return new ManagementScopeRow();
			}
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x000BD478 File Offset: 0x000BB678
		public ManagementScopeRow(ManagementScope managementScope) : base(managementScope)
		{
			this.ManagementScope = managementScope;
			base.Text = managementScope.Name;
			base.Value = managementScope.Name;
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x000BD4A0 File Offset: 0x000BB6A0
		private ManagementScopeRow()
		{
			base.Text = Strings.DefaultScope;
			base.Value = ManagementScopeRow.DefaultScopeId;
		}

		// Token: 0x17002491 RID: 9361
		// (get) Token: 0x06003ED5 RID: 16085 RVA: 0x000BD4C3 File Offset: 0x000BB6C3
		// (set) Token: 0x06003ED6 RID: 16086 RVA: 0x000BD4CB File Offset: 0x000BB6CB
		protected ManagementScope ManagementScope { get; set; }

		// Token: 0x17002492 RID: 9362
		// (get) Token: 0x06003ED7 RID: 16087 RVA: 0x000BD4D4 File Offset: 0x000BB6D4
		// (set) Token: 0x06003ED8 RID: 16088 RVA: 0x000BD4DC File Offset: 0x000BB6DC
		[DataMember]
		public string DisplayName
		{
			get
			{
				return base.Text;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002493 RID: 9363
		// (get) Token: 0x06003ED9 RID: 16089 RVA: 0x000BD4E3 File Offset: 0x000BB6E3
		// (set) Token: 0x06003EDA RID: 16090 RVA: 0x000BD4EB File Offset: 0x000BB6EB
		[DataMember]
		public string Name
		{
			get
			{
				return base.Text;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002494 RID: 9364
		// (get) Token: 0x06003EDB RID: 16091 RVA: 0x000BD4F2 File Offset: 0x000BB6F2
		// (set) Token: 0x06003EDC RID: 16092 RVA: 0x000BD509 File Offset: 0x000BB709
		public ScopeRestrictionType ScopeRestrictionType
		{
			get
			{
				if (this.ManagementScope != null)
				{
					return this.ManagementScope.ScopeRestrictionType;
				}
				return ScopeRestrictionType.RecipientScope;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040028AE RID: 10414
		public static readonly string DefaultScopeId = Guid.Empty.ToString();
	}
}
