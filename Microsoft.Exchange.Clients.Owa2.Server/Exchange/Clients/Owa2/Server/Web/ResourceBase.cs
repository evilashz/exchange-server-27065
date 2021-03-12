using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200047A RID: 1146
	public abstract class ResourceBase
	{
		// Token: 0x060026CC RID: 9932 RVA: 0x0008CA4F File Offset: 0x0008AC4F
		public ResourceBase(string resourceName, ResourceTarget.Filter targetFilter, string currentOwaVersion, bool hasUserSpecificData)
		{
			if (resourceName != null)
			{
				this.resourceName = resourceName.ToLowerInvariant();
			}
			this.hasUserSpecificData = hasUserSpecificData;
			this.targetFilter = targetFilter;
			this.resourcesRelativeFolderPath = ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(currentOwaVersion);
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060026CD RID: 9933 RVA: 0x0008CA81 File Offset: 0x0008AC81
		public bool HasUserSpecificData
		{
			get
			{
				return this.hasUserSpecificData;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x0008CA89 File Offset: 0x0008AC89
		public ResourceTarget.Filter TargetFilter
		{
			get
			{
				return this.targetFilter;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x0008CA91 File Offset: 0x0008AC91
		public string ResourcesRelativeFolderPath
		{
			get
			{
				return this.resourcesRelativeFolderPath;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x0008CA99 File Offset: 0x0008AC99
		internal virtual string ResourceName
		{
			get
			{
				return this.resourceName;
			}
		}

		// Token: 0x060026D1 RID: 9937
		public abstract string GetResourcePath(IPageContext pageContext, bool isBootResourcePath);

		// Token: 0x060026D2 RID: 9938 RVA: 0x0008CAA4 File Offset: 0x0008ACA4
		protected static string CombinePath(params string[] parts)
		{
			for (int i = 0; i < parts.Length - 1; i++)
			{
				parts[i] = parts[i].TrimEnd(new char[]
				{
					'/'
				});
				parts[i + 1] = parts[i + 1].TrimStart(new char[]
				{
					'/'
				});
			}
			return string.Join("/", parts);
		}

		// Token: 0x040016AB RID: 5803
		private readonly string resourceName;

		// Token: 0x040016AC RID: 5804
		private readonly bool hasUserSpecificData;

		// Token: 0x040016AD RID: 5805
		private readonly ResourceTarget.Filter targetFilter;

		// Token: 0x040016AE RID: 5806
		private readonly string resourcesRelativeFolderPath;
	}
}
