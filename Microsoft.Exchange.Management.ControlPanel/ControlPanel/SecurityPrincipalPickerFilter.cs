using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000355 RID: 853
	[DataContract]
	public class SecurityPrincipalPickerFilter : SearchTextFilter
	{
		// Token: 0x06002FA9 RID: 12201 RVA: 0x0009149C File Offset: 0x0008F69C
		public SecurityPrincipalPickerFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x17001F0C RID: 7948
		// (get) Token: 0x06002FAA RID: 12202 RVA: 0x000914BE File Offset: 0x0008F6BE
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-SecurityPrincipal";
			}
		}

		// Token: 0x17001F0D RID: 7949
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x000914C5 File Offset: 0x0008F6C5
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x17001F0E RID: 7950
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x000914CC File Offset: 0x0008F6CC
		protected override string[] FilterableProperties
		{
			get
			{
				return SecurityPrincipalPickerFilter.filterableProperties;
			}
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000914D4 File Offset: 0x0008F6D4
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			SecurityPrincipalType[] value = new SecurityPrincipalType[]
			{
				SecurityPrincipalType.UniversalSecurityGroup,
				SecurityPrincipalType.User
			};
			base["Types"] = value;
			base["RoleGroupAssignable"] = true;
		}

		// Token: 0x17001F0F RID: 7951
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x0009150F File Offset: 0x0008F70F
		protected override SearchTextFilterType FilterType
		{
			get
			{
				if (DDIHelper.IsFFO())
				{
					return SearchTextFilterType.Equals;
				}
				return base.FilterType;
			}
		}

		// Token: 0x04002313 RID: 8979
		public new const string RbacParameters = "?ResultSize&Filter&Types&RoleGroupAssignable";

		// Token: 0x04002314 RID: 8980
		internal const string GetCmdlet = "Get-SecurityPrincipal";

		// Token: 0x04002315 RID: 8981
		private static string[] filterableProperties = new string[]
		{
			"Name",
			"DisplayName"
		};
	}
}
