using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200026F RID: 623
	[DataContract]
	public class AllAssociatedRPTsFilter : AllRPTsFilter
	{
		// Token: 0x0600298F RID: 10639 RVA: 0x00082CC0 File Offset: 0x00080EC0
		public AllAssociatedRPTsFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x17001CB7 RID: 7351
		// (get) Token: 0x06002990 RID: 10640 RVA: 0x00082CE2 File Offset: 0x00080EE2
		// (set) Token: 0x06002991 RID: 10641 RVA: 0x00082CEA File Offset: 0x00080EEA
		public bool IncludeDefaultTags
		{
			get
			{
				return this.includeDefaultTags;
			}
			set
			{
				this.includeDefaultTags = value;
				if (this.includeDefaultTags)
				{
					base["Types"] = AllAssociatedRPTsFilter.retentionPolicyTagTypes;
					return;
				}
				base["Types"] = ElcFolderType.Personal;
			}
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x00082D1E File Offset: 0x00080F1E
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			base["Mailbox"] = RbacPrincipal.Current.ExecutingUserId;
		}

		// Token: 0x040020D8 RID: 8408
		public new const string RbacParameters = "?Types&Mailbox";

		// Token: 0x040020D9 RID: 8409
		private static readonly ElcFolderType[] retentionPolicyTagTypes = new ElcFolderType[]
		{
			ElcFolderType.Personal,
			ElcFolderType.All
		};

		// Token: 0x040020DA RID: 8410
		private bool includeDefaultTags;
	}
}
