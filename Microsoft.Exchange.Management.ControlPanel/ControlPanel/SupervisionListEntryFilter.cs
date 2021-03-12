using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000493 RID: 1171
	[DataContract]
	public class SupervisionListEntryFilter : WebServiceParameters
	{
		// Token: 0x06003A60 RID: 14944 RVA: 0x000B08D4 File Offset: 0x000AEAD4
		public SupervisionListEntryFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000B08F6 File Offset: 0x000AEAF6
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.Tag = "allow";
		}

		// Token: 0x17002315 RID: 8981
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000B0903 File Offset: 0x000AEB03
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-SupervisionListEntry";
			}
		}

		// Token: 0x17002316 RID: 8982
		// (get) Token: 0x06003A63 RID: 14947 RVA: 0x000B090A File Offset: 0x000AEB0A
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}

		// Token: 0x17002317 RID: 8983
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x000B0911 File Offset: 0x000AEB11
		// (set) Token: 0x06003A65 RID: 14949 RVA: 0x000B0923 File Offset: 0x000AEB23
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

		// Token: 0x17002318 RID: 8984
		// (get) Token: 0x06003A66 RID: 14950 RVA: 0x000B0931 File Offset: 0x000AEB31
		// (set) Token: 0x06003A67 RID: 14951 RVA: 0x000B0948 File Offset: 0x000AEB48
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

		// Token: 0x040026F1 RID: 9969
		public const string RbacParameters = "?Tag&Identity";
	}
}
