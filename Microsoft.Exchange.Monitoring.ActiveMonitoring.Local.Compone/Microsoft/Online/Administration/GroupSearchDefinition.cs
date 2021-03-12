using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003BE RID: 958
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GroupSearchDefinition", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	public class GroupSearchDefinition : SearchDefinition
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x0008C607 File Offset: 0x0008A807
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x0008C60F File Offset: 0x0008A80F
		[DataMember]
		public GroupType? GroupType
		{
			get
			{
				return this.GroupTypeField;
			}
			set
			{
				this.GroupTypeField = value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0008C618 File Offset: 0x0008A818
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x0008C620 File Offset: 0x0008A820
		[DataMember]
		public bool? HasErrorsOnly
		{
			get
			{
				return this.HasErrorsOnlyField;
			}
			set
			{
				this.HasErrorsOnlyField = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0008C629 File Offset: 0x0008A829
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x0008C631 File Offset: 0x0008A831
		[DataMember]
		public string[] IncludedProperties
		{
			get
			{
				return this.IncludedPropertiesField;
			}
			set
			{
				this.IncludedPropertiesField = value;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x0008C63A File Offset: 0x0008A83A
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x0008C642 File Offset: 0x0008A842
		[DataMember]
		public bool? IsAgentRole
		{
			get
			{
				return this.IsAgentRoleField;
			}
			set
			{
				this.IsAgentRoleField = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x0008C64B File Offset: 0x0008A84B
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x0008C653 File Offset: 0x0008A853
		[DataMember]
		public Guid? UserObjectId
		{
			get
			{
				return this.UserObjectIdField;
			}
			set
			{
				this.UserObjectIdField = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x0008C65C File Offset: 0x0008A85C
		// (set) Token: 0x0600172A RID: 5930 RVA: 0x0008C664 File Offset: 0x0008A864
		[DataMember]
		public string UserPrincipalName
		{
			get
			{
				return this.UserPrincipalNameField;
			}
			set
			{
				this.UserPrincipalNameField = value;
			}
		}

		// Token: 0x04001059 RID: 4185
		private GroupType? GroupTypeField;

		// Token: 0x0400105A RID: 4186
		private bool? HasErrorsOnlyField;

		// Token: 0x0400105B RID: 4187
		private string[] IncludedPropertiesField;

		// Token: 0x0400105C RID: 4188
		private bool? IsAgentRoleField;

		// Token: 0x0400105D RID: 4189
		private Guid? UserObjectIdField;

		// Token: 0x0400105E RID: 4190
		private string UserPrincipalNameField;
	}
}
