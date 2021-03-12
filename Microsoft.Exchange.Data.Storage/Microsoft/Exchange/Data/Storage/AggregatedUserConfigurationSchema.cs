using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001C5 RID: 453
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregatedUserConfigurationSchema : IAggregatedUserConfigurationSchema
	{
		// Token: 0x06001861 RID: 6241 RVA: 0x00076D04 File Offset: 0x00074F04
		public AggregatedUserConfigurationSchema()
		{
			this.all = new List<AggregatedUserConfigurationDescriptor>();
			this.owaUserConfiguration = new AggregatedUserConfigurationDescriptor("Aggregated.OwaUserConfiguration", new UserConfigurationDescriptor[]
			{
				UserConfigurationDescriptor.CreateMailboxDescriptor("OWA.UserOptions", UserConfigurationTypes.Dictionary),
				UserConfigurationDescriptor.CreateDefaultFolderDescriptor("WorkHours", UserConfigurationTypes.XML, DefaultFolderType.Calendar),
				UserConfigurationDescriptor.CreateDefaultFolderDescriptor("Calendar", UserConfigurationTypes.Dictionary, DefaultFolderType.Calendar),
				UserConfigurationDescriptor.CreateMailboxDescriptor("OWA.ViewStateConfiguration", UserConfigurationTypes.Dictionary),
				UserConfigurationDescriptor.CreateDefaultFolderDescriptor("CategoryList", UserConfigurationTypes.XML, DefaultFolderType.Calendar),
				UserConfigurationDescriptor.CreateDefaultFolderDescriptor("MRM", UserConfigurationTypes.Stream | UserConfigurationTypes.XML | UserConfigurationTypes.Dictionary, DefaultFolderType.Inbox),
				UserConfigurationDescriptor.CreateDefaultFolderDescriptor("Inference.Settings", UserConfigurationTypes.Dictionary, DefaultFolderType.Inbox),
				UserConfigurationDescriptor.CreateMailboxDescriptor("AggregatedAccountList", UserConfigurationTypes.XML)
			});
			this.all.Add(this.owaUserConfiguration);
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x00076DC0 File Offset: 0x00074FC0
		public static AggregatedUserConfigurationSchema Instance
		{
			get
			{
				if (AggregatedUserConfigurationSchema.instance == null)
				{
					lock (AggregatedUserConfigurationSchema.instanceLock)
					{
						if (AggregatedUserConfigurationSchema.instance == null)
						{
							AggregatedUserConfigurationSchema.instance = new AggregatedUserConfigurationSchema();
						}
					}
				}
				return AggregatedUserConfigurationSchema.instance;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x00076E18 File Offset: 0x00075018
		public IEnumerable<AggregatedUserConfigurationDescriptor> All
		{
			get
			{
				return this.all;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x00076E20 File Offset: 0x00075020
		public AggregatedUserConfigurationDescriptor OwaUserConfiguration
		{
			get
			{
				return this.owaUserConfiguration;
			}
		}

		// Token: 0x04000CC6 RID: 3270
		private static object instanceLock = new object();

		// Token: 0x04000CC7 RID: 3271
		private static AggregatedUserConfigurationSchema instance;

		// Token: 0x04000CC8 RID: 3272
		private List<AggregatedUserConfigurationDescriptor> all;

		// Token: 0x04000CC9 RID: 3273
		private AggregatedUserConfigurationDescriptor owaUserConfiguration;
	}
}
