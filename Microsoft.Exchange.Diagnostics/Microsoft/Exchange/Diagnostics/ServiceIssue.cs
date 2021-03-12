using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000157 RID: 343
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ServiceIssue
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x00024E33 File Offset: 0x00023033
		protected ServiceIssue(string error)
		{
			this.Error = error;
			this.Guid = Guid.NewGuid();
			this.CreatedTime = DateTime.UtcNow;
			this.LastUpdatedTime = DateTime.UtcNow;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00024E63 File Offset: 0x00023063
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x00024E6B File Offset: 0x0002306B
		public Guid Guid { get; private set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00024E74 File Offset: 0x00023074
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x00024E7C File Offset: 0x0002307C
		public string Error { get; private set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00024E85 File Offset: 0x00023085
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x00024E8D File Offset: 0x0002308D
		public DateTime CreatedTime { get; private set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00024E96 File Offset: 0x00023096
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x00024E9E File Offset: 0x0002309E
		public DateTime LastUpdatedTime { get; private set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060009DF RID: 2527
		public abstract string IdentifierString { get; }

		// Token: 0x060009E0 RID: 2528 RVA: 0x00024EA7 File Offset: 0x000230A7
		public virtual void DeriveFromIssue(ServiceIssue issue)
		{
			this.Guid = issue.Guid;
			this.CreatedTime = issue.CreatedTime;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00024EC4 File Offset: 0x000230C4
		public virtual XElement GetDiagnosticInfo(SICDiagnosticArgument arguments)
		{
			XElement xelement = new XElement("ServiceIssue");
			xelement.Add(new object[]
			{
				new XElement("Guid", this.Guid),
				new XElement("Error", this.Error),
				new XElement("Type", base.GetType().FullName),
				new XElement("IdentifierString", this.IdentifierString),
				new XElement("CreatedTime", this.CreatedTime),
				new XElement("LastUpdatedTime", this.LastUpdatedTime)
			});
			return xelement;
		}

		// Token: 0x02000158 RID: 344
		private static class ServiceIssueSchema
		{
			// Token: 0x040006BD RID: 1725
			public const string ElementName = "ServiceIssue";

			// Token: 0x040006BE RID: 1726
			public const string GuidElement = "Guid";

			// Token: 0x040006BF RID: 1727
			public const string ErrorElement = "Error";

			// Token: 0x040006C0 RID: 1728
			public const string TypeElement = "Type";

			// Token: 0x040006C1 RID: 1729
			public const string IdentifierStringElement = "IdentifierString";

			// Token: 0x040006C2 RID: 1730
			public const string CreatedTimeElement = "CreatedTime";

			// Token: 0x040006C3 RID: 1731
			public const string LastUpdatedTimeElement = "LastUpdatedTime";
		}
	}
}
