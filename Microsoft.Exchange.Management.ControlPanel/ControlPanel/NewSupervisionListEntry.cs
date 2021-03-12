using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000492 RID: 1170
	[DataContract]
	public class NewSupervisionListEntry : SetObjectProperties
	{
		// Token: 0x1700230F RID: 8975
		// (get) Token: 0x06003A55 RID: 14933 RVA: 0x000B0827 File Offset: 0x000AEA27
		public override string AssociatedCmdlet
		{
			get
			{
				return "Add-SupervisionListEntry";
			}
		}

		// Token: 0x17002310 RID: 8976
		// (get) Token: 0x06003A56 RID: 14934 RVA: 0x000B082E File Offset: 0x000AEA2E
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x17002311 RID: 8977
		// (get) Token: 0x06003A57 RID: 14935 RVA: 0x000B0835 File Offset: 0x000AEA35
		// (set) Token: 0x06003A58 RID: 14936 RVA: 0x000B0847 File Offset: 0x000AEA47
		public Identity Identity
		{
			get
			{
				return (Identity)base["Identity"];
			}
			set
			{
				base["Identity"] = value;
			}
		}

		// Token: 0x17002312 RID: 8978
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x000B0855 File Offset: 0x000AEA55
		// (set) Token: 0x06003A5A RID: 14938 RVA: 0x000B0867 File Offset: 0x000AEA67
		[DataMember]
		public string EntryName
		{
			get
			{
				return (string)base["Entry"];
			}
			set
			{
				base["Entry"] = value;
			}
		}

		// Token: 0x17002313 RID: 8979
		// (get) Token: 0x06003A5B RID: 14939 RVA: 0x000B0875 File Offset: 0x000AEA75
		// (set) Token: 0x06003A5C RID: 14940 RVA: 0x000B088C File Offset: 0x000AEA8C
		[DataMember]
		public string RecipientType
		{
			get
			{
				return (string)base[SupervisionListEntrySchema.RecipientType.Name];
			}
			set
			{
				base[SupervisionListEntrySchema.RecipientType.Name] = value;
			}
		}

		// Token: 0x17002314 RID: 8980
		// (get) Token: 0x06003A5D RID: 14941 RVA: 0x000B089F File Offset: 0x000AEA9F
		// (set) Token: 0x06003A5E RID: 14942 RVA: 0x000B08B6 File Offset: 0x000AEAB6
		[DataMember]
		public string Tag
		{
			get
			{
				return (string)base[SupervisionListEntrySchema.Tag.Name];
			}
			set
			{
				base[SupervisionListEntrySchema.Tag.Name] = value;
			}
		}
	}
}
