using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200026E RID: 622
	[DataContract]
	public class AllRPTsFilter : WebServiceParameters
	{
		// Token: 0x0600298B RID: 10635 RVA: 0x00082C7C File Offset: 0x00080E7C
		public AllRPTsFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x17001CB5 RID: 7349
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x00082C9E File Offset: 0x00080E9E
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-RetentionPolicyTag";
			}
		}

		// Token: 0x17001CB6 RID: 7350
		// (get) Token: 0x0600298D RID: 10637 RVA: 0x00082CA5 File Offset: 0x00080EA5
		public override string RbacScope
		{
			get
			{
				return "@C:OrganizationConfig";
			}
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x00082CAC File Offset: 0x00080EAC
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			base["Types"] = ElcFolderType.Personal;
		}

		// Token: 0x040020D7 RID: 8407
		public const string RbacParameters = "?Types";
	}
}
