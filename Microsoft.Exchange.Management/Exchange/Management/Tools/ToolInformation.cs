using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Tools
{
	// Token: 0x02000D06 RID: 3334
	[Serializable]
	public class ToolInformation : ConfigurableObject
	{
		// Token: 0x06008009 RID: 32777 RVA: 0x0020BB0D File Offset: 0x00209D0D
		internal ToolInformation(ToolId identity, ToolVersionStatus versionStatus, Version minimumSupportedVersion, Version latestVersion, Uri updateInformationUrl) : this()
		{
			this.Identity = identity;
			this.VersionStatus = versionStatus;
			this.MinimumSupportedVersion = minimumSupportedVersion;
			this.LatestVersion = latestVersion;
			this.UpdateInformationUrl = updateInformationUrl;
		}

		// Token: 0x0600800A RID: 32778 RVA: 0x0020BB3A File Offset: 0x00209D3A
		internal ToolInformation() : base(new SimpleProviderPropertyBag())
		{
			this.propertyBag.SetField(this.propertyBag.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x170027AF RID: 10159
		// (get) Token: 0x0600800B RID: 32779 RVA: 0x0020BB63 File Offset: 0x00209D63
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ToolInformation.schema;
			}
		}

		// Token: 0x170027B0 RID: 10160
		// (get) Token: 0x0600800C RID: 32780 RVA: 0x0020BB6A File Offset: 0x00209D6A
		// (set) Token: 0x0600800D RID: 32781 RVA: 0x0020BB81 File Offset: 0x00209D81
		public new ToolId Identity
		{
			get
			{
				return (ToolId)this.propertyBag[ToolInformationSchema.Identity];
			}
			private set
			{
				this.propertyBag[ToolInformationSchema.Identity] = value;
			}
		}

		// Token: 0x170027B1 RID: 10161
		// (get) Token: 0x0600800E RID: 32782 RVA: 0x0020BB99 File Offset: 0x00209D99
		// (set) Token: 0x0600800F RID: 32783 RVA: 0x0020BBB0 File Offset: 0x00209DB0
		public ToolVersionStatus VersionStatus
		{
			get
			{
				return (ToolVersionStatus)this.propertyBag[ToolInformationSchema.VersionStatus];
			}
			private set
			{
				this.propertyBag[ToolInformationSchema.VersionStatus] = value;
			}
		}

		// Token: 0x170027B2 RID: 10162
		// (get) Token: 0x06008010 RID: 32784 RVA: 0x0020BBC8 File Offset: 0x00209DC8
		// (set) Token: 0x06008011 RID: 32785 RVA: 0x0020BBDF File Offset: 0x00209DDF
		public Version MinimumSupportedVersion
		{
			get
			{
				return (Version)this.propertyBag[ToolInformationSchema.MinimumSupportedVersion];
			}
			private set
			{
				this.propertyBag[ToolInformationSchema.MinimumSupportedVersion] = value;
			}
		}

		// Token: 0x170027B3 RID: 10163
		// (get) Token: 0x06008012 RID: 32786 RVA: 0x0020BBF2 File Offset: 0x00209DF2
		// (set) Token: 0x06008013 RID: 32787 RVA: 0x0020BC09 File Offset: 0x00209E09
		public Version LatestVersion
		{
			get
			{
				return (Version)this.propertyBag[ToolInformationSchema.LatestVersion];
			}
			private set
			{
				this.propertyBag[ToolInformationSchema.LatestVersion] = value;
			}
		}

		// Token: 0x170027B4 RID: 10164
		// (get) Token: 0x06008014 RID: 32788 RVA: 0x0020BC1C File Offset: 0x00209E1C
		// (set) Token: 0x06008015 RID: 32789 RVA: 0x0020BC33 File Offset: 0x00209E33
		public Uri UpdateInformationUrl
		{
			get
			{
				return (Uri)this.propertyBag[ToolInformationSchema.UpdateInformationUrl];
			}
			private set
			{
				this.propertyBag[ToolInformationSchema.UpdateInformationUrl] = value;
			}
		}

		// Token: 0x04003ED3 RID: 16083
		private static ToolInformationSchema schema = ObjectSchema.GetInstance<ToolInformationSchema>();
	}
}
